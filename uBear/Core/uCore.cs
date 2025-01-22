using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UCNLDrivers;
using UCNLNav;
using UCNLNMEA;
using UCNLUI;

namespace uBear.Core
{
    #region Custom EventArgs

    public class RelativeLocationUpdatedEventArgs : EventArgs
    {
        #region Properties

        public string ID { get; set; }
        public double PRange_m { get; set; }
        public double Azimuth_deg { get; set; }

        public bool IsTimeout { get; set; }

        #endregion

        #region Constructor

        public RelativeLocationUpdatedEventArgs(string id, double pRange_m, double azimuth_deg, bool isTimeout)
        {
            ID = id;
            PRange_m = pRange_m;
            Azimuth_deg = azimuth_deg;
            IsTimeout = isTimeout;
        }

        #endregion
    }

    public class AbsoluteLocationUpdatedEventArgs : EventArgs
    {
        #region Properties
        public string ID { get; set; }

        public double Latitude_deg { get; set; }

        public double Longitude_deg { get; set; }

        public double Depth_m { get; set; }

        #endregion

        #region Constructor

        public AbsoluteLocationUpdatedEventArgs(string id, double lat_deg, double lon_deg, double dpt_m)
        {
            ID = id;
            Latitude_deg = lat_deg;
            Longitude_deg = lon_deg;
            Depth_m = dpt_m;
        }

        #endregion
    }    

    #endregion
    public class uCore : IDisposable
    {
        #region Properties

        bool disposed = false;

        uWaveSerialPort uPort;
        uGNSSSerialPort uGNSSPort;

        public bool IsActive
        {
            get { return uPort.IsActive; }
        }

        public bool uPortDetected
        {
            get { return uPort.Detected; }
        }

        public string uPortName
        {
            get { return uPort.PortName; }
        }

        public string uPortStatus
        {
            get { return uPort.ToString(); }
        }

        public bool IsUseGNSS { get; private set; }

        public bool GNSSPortDetected
        {
            get { return (uGNSSPort != null) && (uGNSSPort.Detected); }
        }

        public string GNSSPortStatus
        {
            get { return uGNSSPort == null ? string.Empty : uGNSSPort.ToString(); }
        }

        public double Heading
        {
            get { return (uGNSSPort == null) ? double.NaN : uGNSSPort.Heading; }
        }
                
        public double SoundSpeed
        {
            get => wpManager.SoundSpeed;
            set => wpManager.SoundSpeed = value;
        }

        public double Salinity
        {
            get => wpManager.Salinity;
            set => wpManager.Salinity = value;
        }

        public bool IsAutoSoundSpeed
        {
            get => wpManager.IsAutoSoundSpeed;
            set => wpManager.IsAutoSoundSpeed = value;
        }

        public bool IsAutoSalinity
        {
            get => wpManager.IsAutoSalinity;
            set => wpManager.IsAutoSalinity = value;
        }

        public bool IsAutoGravity
        {
            get => wpManager.IsAutoGravity;
            set => wpManager.IsAutoGravity = value;
        }

        public double GravityAcceleration
        {
            get => wpManager.GravityAcceleration;
            set => wpManager.GravityAcceleration = value;
        }

        public bool DeviceInfoValid
        {
            get => uPort.IsDeviceInfoValid;
        }

        public string DeviceInfo
        {
            get
            {
                if (uPort.IsDeviceInfoValid)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        "{0} v{1}\r\n{2} v{3}\r\nS/N: {4}\r\nPTS: {5}\r\nAc. Baudrate: {6} bps\r\n",
                        uPort.SystemMoniker, uPort.SystemVersion,
                        uPort.CoreMoniker, uPort.CoreVersion,
                        uPort.SerialNumber,
                        uPort.IsPTS,
                        uPort.AcousticBaudrate);

                    return sb.ToString();
                }
                else
                    return string.Empty;
            }
        }


        AgingValue<double> gnssLatitude;
        AgingValue<double> gnssLongitude;
        AgingValue<double> gnssCourse;
        AgingValue<double> gnssSpeed;
        AgingValue<double> gnssHeading;

        WPManager wpManager;

        int remoteIdx;
        List<RemoteAddress> remoteAddresses;
        Dictionary<RemoteAddress, RemoteDescriptor> remotes;

        readonly int txChID = 1;
        readonly int rxChID = 0;

        bool remote_polling_enabled = false;

        double phi_deg = 0;
        double x_offset_m = 0;
        double y_offset_m = 0;

        readonly string[] llSeparators = new string[] { ">>", " " };

        public double LatitudeOverride { get; private set; }
        public double LongitudeOverride { get; private set; }
        public double HeadingOverride { get; private set; }

        PrecisionTimer pTimer;

        public bool LocationOverrideEnabled { get => pTimer.IsRunning; }

        #endregion

        #region Constructor

        public uCore(HashSet<RemoteAddress> remotesInUse, double hdn_adj_deg, double gnss_x_offset_m, double gnss_y_offset_m)
        {
            #region Misc. parameters

            if ((hdn_adj_deg >= 0) && (hdn_adj_deg <= 360))
                phi_deg = hdn_adj_deg;
            else
                throw new ArgumentOutOfRangeException("hdn_adj_deg should be in a range [0..360] °");

            x_offset_m = gnss_x_offset_m;
            y_offset_m = gnss_y_offset_m;

            wpManager = new WPManager();
            wpManager.SalinityChanged += (o, e) => LogEventHandler.Rise(this,
                new LogEventArgs(LogLineType.INFO,
                string.Format("Salinity updated: {0:F01} PSU", Salinity)));

            wpManager.SoundSpeedChanged += (o, e) => LogEventHandler.Rise(this,
                new LogEventArgs(LogLineType.INFO,
                string.Format("Sound speed updated: {0:F01} m/s", SoundSpeed)));

            gnssHeading = new AgingValue<double>(3, 10, x => string.Format(CultureInfo.InvariantCulture, "{0:F01}°", x));
            gnssLatitude = new AgingValue<double>(3, 10, x => string.Format(CultureInfo.InvariantCulture, "{0:F06}°", x));
            gnssLongitude = new AgingValue<double>(3, 10, x => string.Format(CultureInfo.InvariantCulture, "{0:F06}°", x));
            gnssSpeed = new AgingValue<double>(3, 10, x => string.Format(CultureInfo.InvariantCulture, "{0:F01} km/h", x));
            gnssCourse = new AgingValue<double>(3, 10, x => string.Format(CultureInfo.InvariantCulture, "{0:F01}°", x));

            #endregion

            #region remoteAddresses

            if (remotesInUse == null)
                throw new ArgumentNullException("remoteIDs cannot be null or empty");

            remotes = new Dictionary<RemoteAddress, RemoteDescriptor>();

            remoteAddresses = new List<RemoteAddress>();
            foreach (var item in remotesInUse)
            {
                remoteAddresses.Add(item);
                remotes.Add(item, new RemoteDescriptor(item));
            }

            if (remoteAddresses.Count == 0)
                throw new ArgumentException("No remotes are in use");

            remoteIdx = 0;

            #endregion

            #region uPort

            uPort = new uWaveSerialPort(BaudRate.baudRate9600);
            uPort.IsTryAlways = true;
            uPort.IsLogIncoming = true;

            uPort.DetectedChanged += (o, e) =>
            {
                uPortDetectedChanged.Rise(o, e);

                if (uPort.Detected)
                {
                    if (IsUseGNSS && !uGNSSPort.IsActive)
                        uGNSSPort.Start();
                }
            };

            uPort.DeviceInfoValidChanged += (o, e) =>
            {
                remote_polling_enabled = false;

                if (uPort.IsDeviceInfoValid)
                {
                    // Check settings
                    if ((uPort.TxChID != txChID) ||
                        (uPort.RxChID != rxChID) ||
                        !uPort.IsCommandModeByDefault)
                    {
                        uPort.Query_SETTINGS_WRITE(txChID, rxChID, 0.0, true, false, UCNLPhysics.PHX.PHX_GRAVITY_ACC_MPS2);
                    }
                    else
                    {
                        uPort.Query_AMB_CFG_WRITE(true, 1, true, true, true, false);
                    }
                }
                else
                {
                    uPort.Query_AMB_CFG_WRITE(true, 1, true, true, true, false);
                }

                DeviceInfoValidChanged.Rise(this, e);
            };

            uPort.IsActiveChanged += (o, e) => IsActiveChanged.Rise(o, e);

            uPort.IsWaitingLocalChangedEventHandler += (o, e) =>
            {
                StateUpdateHandler.Rise(o, e);
            };

            uPort.IsWaitingRemoteChangedEventHandler += (o, e) =>
            {
                if (!uPort.IsWaitingRemote)
                    RemotePollingProcess();

                StateUpdateHandler.Rise(o, e);
            };

            uPort.LogEventHandler += (o, e) => LogEventHandler.Rise(o, e);

            uPort.PortTimeout += (o, e) =>
            {

            };

            uPort.ACKReceived += (o, e) =>
            {
                if (e.ErrorID == LOC_ERR_Enum.LOC_ERR_NO_ERROR)
                {
                    if (e.SentenceID == ICs.IC_H2D_SETTINGS_WRITE)
                    {
                        uPort.Query_AMB_CFG_WRITE(true, 1, true, true, true, false);
                    }
                    else if (e.SentenceID == ICs.IC_H2D_AMB_DTA_CFG)
                    {
                        uPort.Query_PTCROL_CFG_WRITE(true, 1);

                        //remote_polling_enabled = true;
                        //RemotePollingProcess();
                    }
                    else if (e.SentenceID == ICs.IC_H2D_INC_DTA_CFG)
                    {
                        remote_polling_enabled = true;
                        RemotePollingProcess();
                    }
                }
                else
                {
                    if ((e.SentenceID == ICs.IC_H2D_RC_REQUEST) &&
                        remote_polling_enabled)
                        RemotePollingProcess();
                    else
                        uPort.Query_DINFO();
                }
            };

            uPort.AMBDataUpdated += (o, e) =>
            {
                if (uPort.Temperature_C.IsInitializedAndNotObsolete &&
                    uPort.Pressure_mBar.IsInitializedAndNotObsolete)
                {
                    wpManager.Temperature = uPort.Temperature_C.Value;
                    wpManager.Pressure = uPort.Pressure_mBar.Value;
                }

                StateUpdateHandler.Rise(o, e);
            };

            uPort.PTCROLDataUpdated += (o, e) =>
            {
                StateUpdateHandler.Rise(o, e);
            };

            uPort.RCResponseReceived += (o, e) =>
            {
                RemoteAddress remID = (RemoteAddress)Enum.ToObject(typeof(RemoteAddress), e.TxChID);
                if (!remotes.ContainsKey(remID))
                    remotes.Add(remID, new RemoteDescriptor(remID));

                remotes[remID].IsTimeout = false;
                remotes[remID].MSR_dB.Value = e.MSR_db;

                double o_dpt_m = 0.0;
                if (uPort.Depth_m.IsInitializedAndNotObsolete)
                    o_dpt_m = uPort.Depth_m.Value;

                // Can the relative position be calculated?
                if (e.IsAzimuthPresent &&                       // Do we have the Azimuth?
                    !double.IsNaN(e.PropTime_sec) &&            // and the propagation time (slant range)?
                    e.IsValuePresent)                           // and a remote data value?
                {
                    double s_range_m = e.PropTime_sec * SoundSpeed;
                    double r_azimuth_deg = e.Azimuth;
                    //double r_azimuth_deg = Algorithms.Wrap360(450 - e.Azimuth);

                    remotes[remID].SRange_m.Value = s_range_m;
                    remotes[remID].RAzimuth_deg.Value = r_azimuth_deg;

                    if (e.RCCmdID == RC_CODES_Enum.RC_DPT_GET)  // and is it the depth reading?
                    {
                        double r_depth_m = e.Value;
                        double delta_dpt_m = Math.Abs(r_depth_m - o_dpt_m);
                        double p_range_m = s_range_m > delta_dpt_m ? Math.Sqrt(s_range_m * s_range_m - delta_dpt_m * delta_dpt_m) : 0.0;
                                                
                        remotes[remID].PRange_m.Value = p_range_m;
                        remotes[remID].Depth_m.Value = r_depth_m;

                        string rID = ((int)remID).ToString();
                        
                        if (gnssHeading.IsInitialized &&
                            gnssLatitude.IsInitialized &&
                            gnssLongitude.IsInitialized)
                        {
                            double o_hdn_deg = gnssHeading.Value;
                            double o_lat_deg = gnssLatitude.Value;
                            double o_lon_deg = gnssLongitude.Value;

                            double a_azimuth_deg = double.NaN;
                            double a_range_m = double.NaN;

                            PolarCS_ShiftRotate(o_hdn_deg, phi_deg,
                                r_azimuth_deg, p_range_m,
                                x_offset_m, y_offset_m,
                                out a_azimuth_deg, out a_range_m);                            

                            remotes[remID].AAzimuth_deg.Value = a_azimuth_deg;
                            remotes[remID].APRange_m.Value = a_range_m;

                            double o_lat_rad = Algorithms.Deg2Rad(o_lat_deg);
                            double o_lon_rad = Algorithms.Deg2Rad(o_lon_deg);
                            double a_azimuth_rad = Algorithms.Deg2Rad(a_azimuth_deg);

                            double r_lat_rad = double.NaN;
                            double r_lon_rad = double.NaN;
                            double r_rev_azm_rad = double.NaN;
                            int it_cnt = 0;

                            if (!Algorithms.VincentyDirect(o_lat_rad, o_lon_rad, a_azimuth_rad, a_range_m,
                                    Algorithms.WGS84Ellipsoid, 
                                    Algorithms.VNC_DEF_EPSILON, Algorithms.VNC_DEF_IT_LIMIT,
                                    out r_lat_rad, out r_lon_rad, out r_rev_azm_rad, out it_cnt))
                            {
                                Algorithms.HaversineDirect(o_lat_rad, o_lon_rad, a_range_m, a_azimuth_rad,
                                    Algorithms.WGS84Ellipsoid.MajorSemiAxis_m,
                                    out r_lat_rad, out r_lon_rad);
                            }

                            if (remotes[remID].FilterState == null)
                                remotes[remID].FilterState = new DHFilter(8, 1, 10);

                            DateTime ts = DateTime.Now;

                            if (remotes[remID].FilterState.Process(r_lat_rad, r_lon_rad, r_depth_m, ts,
                                out r_lat_rad, out r_lon_rad, out r_depth_m, out ts))
                            {
                                AbsoluteLocationUpdated.Rise(this,
                                new AbsoluteLocationUpdatedEventArgs(rID,
                                Algorithms.Rad2Deg(r_lat_rad), Algorithms.Rad2Deg(r_lon_rad), r_depth_m));
                            }

                            RelativeLocationUpdated.Rise(this,
                            new RelativeLocationUpdatedEventArgs(rID, a_range_m, a_azimuth_deg, false));                            
                        }
                        else
                        {
                            RelativeLocationUpdated.Rise(this,
                            new RelativeLocationUpdatedEventArgs(rID, p_range_m, r_azimuth_deg, false));
                        }

                    }
                    else if (e.RCCmdID == RC_CODES_Enum.RC_BAT_V_GET)
                    {
                        remotes[remID].VBat_V.Value = e.Value;
                    }
                }                               

                StateUpdateHandler.Rise(o, e);
            };

            uPort.RCTimeoutReceived += (o, e) =>
            {
                RemoteAddress remID = (RemoteAddress)Enum.ToObject(typeof(RemoteAddress), e.TxChID);
                if (!remotes.ContainsKey(remID))
                    remotes.Add(remID, new RemoteDescriptor(remID));

                remotes[remID].IsTimeout = true;

                if (remotes[remID].AAzimuth_deg.IsInitialized &&
                    remotes[remID].APRange_m.IsInitialized)
                {
                    RelativeLocationUpdated.Rise(this,
                        new RelativeLocationUpdatedEventArgs(((int)remID).ToString(),
                        remotes[remID].APRange_m.Value, remotes[remID].AAzimuth_deg.Value, true));
                }
                else if (remotes[remID].RAzimuth_deg.IsInitialized &&
                    remotes[remID].PRange_m.IsInitialized)
                {
                    RelativeLocationUpdated.Rise(this,
                        new RelativeLocationUpdatedEventArgs(((int)remID).ToString(),
                        remotes[remID].PRange_m.Value, remotes[remID].RAzimuth_deg.Value, true));
                }

                StateUpdateHandler.Rise(o, e);
            };

            #endregion

            #region timer

            pTimer = new PrecisionTimer();
            pTimer.Period = 1000;
            pTimer.Mode = Mode.Periodic;

            pTimer.Tick += (o, e) =>
            {
                gnssLatitude.Value = LatitudeOverride;
                gnssLongitude.Value = LongitudeOverride;
                gnssHeading.Value = HeadingOverride;
            };

            pTimer.Started += (o, e) => LocationOverrideEnabledChanged.Rise(this, new EventArgs());
            pTimer.Stopped += (o, e) => LocationOverrideEnabledChanged.Rise(this, new EventArgs());

            #endregion
        }

        #endregion

        #region Methods

        #region Public

        public void LocationOverrideEnable(double lat_deg, double lon_deg, double heading_deg)
        {
            if (pTimer.IsRunning)
                pTimer.Stop();

            LatitudeOverride = lat_deg;
            LongitudeOverride = lon_deg;
            HeadingOverride = heading_deg;

            pTimer.Start();
        }

        public void LocationOverrideDisable()
        {
            pTimer.Stop();
        }

        public void Emulate(string s)
        {
            string str = s.Trim() + NMEAParser.SentenceEndDelimiter;

            var splits = str.Split(llSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (splits.Length == 3)
            {
                if (splits[1] == "(GNSS)")
                {
                    if (uGNSSPort == null)
                        AuxGNSSInit(BaudRate.baudRate9600);

                    uGNSSPort.EmulateInput(splits[2]);
                }
                else if (splits[1] == "(UWV)")
                {
                    uPort.EmulateInput(splits[2]);
                }
            }
        }

        public string SystemDescriptionGet()
        {
            StringBuilder sb = new StringBuilder();

            if (uPort.Depth_m.IsInitialized)
                sb.AppendFormat("DPT: {0}\r\n", uPort.Depth_m.ToString());

            if (uPort.Temperature_C.IsInitialized)
                sb.AppendFormat("TMP: {0}\r\n", uPort.Temperature_C.ToString());

            if (uPort.Pitch_deg.IsInitialized)
                sb.AppendFormat("PTC: {0}\r\n", uPort.Pitch_deg.ToString());

            if (uPort.Roll_deg.IsInitialized)
                sb.AppendFormat("ROL: {0}\r\n", uPort.Roll_deg.ToString());

            if (uPort.IsWaitingLocal)
                sb.AppendLine("LOC WAIT...\r\n");

            if (uPort.IsWaitingRemote)
                sb.AppendLine("REM WAIT...\r\n");

            if (IsUseGNSS)
            {
                sb.AppendLine();
                if (gnssLatitude.IsInitialized)
                    sb.AppendFormat("LAT: {0}\r\n", gnssLatitude.ToString());

                if (gnssLongitude.IsInitialized)
                    sb.AppendFormat("LON: {0}\r\n", gnssLongitude.ToString());

                if (gnssHeading.IsInitialized)
                    sb.AppendFormat("HDN: {0}\r\n", gnssHeading.ToString());

                if (gnssCourse.IsInitialized)
                    sb.AppendFormat("CRS: {0}\r\n", gnssCourse.ToString());

                if (gnssSpeed.IsInitialized)
                    sb.AppendFormat("SPD: {0}\r\n", gnssSpeed.ToString());
            }

            return sb.ToString();
        }

        public Dictionary<string, Dictionary<string, string>> RemoteDescriptorsGet()
        {
            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();

            foreach (var remote in remotes)
                result.Add(remote.Key.ToString().Replace('_', ' '), remote.Value.ToDictionary());

            return result;
        }

        public void AuxGNSSInit(BaudRate baudrate)
        {
            if (!IsUseGNSS)
            {
                IsUseGNSS = true;

                uGNSSPort = new uGNSSSerialPort(baudrate);
                uGNSSPort.IsLogIncoming = true;
                uGNSSPort.IsTryAlways = true;

                uGNSSPort.DetectedChanged += (o, e) =>
                {
                    uGNSSPortDetectedChanged.Rise(o, e);
                };

                uGNSSPort.IsActiveChanged += (o, e) => IsGNSSActiveChanged.Rise(o, e);

                uGNSSPort.HeadingUpdated += (o, e) =>
                {
                    gnssHeading.Value = uGNSSPort.Heading;
                    HeadingUpdated.Rise(this, new EventArgs());
                };

                uGNSSPort.LocationUpdated += (o, e) =>
                {
                    gnssLatitude.Value = uGNSSPort.Latitude;
                    gnssLongitude.Value = uGNSSPort.Longitude;
                    gnssCourse.Value = uGNSSPort.CourseOverGround;
                    gnssSpeed.Value = uGNSSPort.GroundSpeed;

                    AbsoluteLocationUpdated.Rise(this,
                        new AbsoluteLocationUpdatedEventArgs("uBear",
                        uGNSSPort.Latitude,
                        uGNSSPort.Longitude,
                        uPort.Depth_m.Value));

                    wpManager.UpdateLocation(uGNSSPort.Latitude, uGNSSPort.Longitude);

                    StateUpdateHandler.Rise(o, e);
                };

                uGNSSPort.LogEventHandler += (o, e) => LogEventHandler.Rise(o, e);
            }
        }

        public void Start()
        {
            if (IsUseGNSS && uGNSSPort.IsActive)
                uGNSSPort.Stop();

            if (!uPort.IsActive)
                uPort.Start();
        }

        public void Stop()
        {
            if (uPort.IsActive)
                uPort.Stop();

            if (IsUseGNSS && uGNSSPort.IsActive)
                uGNSSPort.Stop();
        }

        #endregion

        #region Private      

        private void RemotePollingProcess()
        {
            // Do remote process polling
            var remID = remoteAddresses[remoteIdx];
            remoteIdx = (remoteIdx + 1) % remoteAddresses.Count;

            if (!remotes.ContainsKey(remID))
                remotes.Add(remID, new RemoteDescriptor(remID));

            RC_CODES_Enum remReq = RC_CODES_Enum.RC_DPT_GET;

            if (!remotes[remID].VBat_V.IsInitialized || remotes[remID].VBat_V.IsObsolete)
                remReq = RC_CODES_Enum.RC_BAT_V_GET;

            uPort.Query_RC((int)(remID), rxChID, remReq);
        }

        /// <summary>
        /// All angles clockwise from the North direction
        /// </summary>
        /// <param name="heading_deg">Compass reading, 0-360° clockwise from North direction</param>
        /// <param name="phi_deg">Antenna - comрass zero directions difference, °</param>
        /// <param name="bearing_deg">Bearing to a responder, 0-360° clockwise from North direction</param>
        /// <param name="r_m">slant range projection, m</param>
        /// <param name="xt">transversal GNSS/antenna offset</param>
        /// <param name="yt">longitudal GNSS/antenna offset</param>
        /// <param name="a_deg">Absolute azimuth to the responder</param>
        /// <param name="r_a">Range to the responder (from the GNSS position)</param>
        private static void PolarCS_ShiftRotate(double heading_deg, double phi_deg, double bearing_deg,
            double r_m, double xt, double yt,
            out double a_deg, out double r_a)
        {
            double teta = Algorithms.Wrap2PI(Algorithms.Deg2Rad(bearing_deg + phi_deg));

            double xr = xt + r_m * Math.Sin(teta);
            double yr = yt + r_m * Math.Cos(teta);

            r_a = Math.Sqrt(xr * xr + yr * yr);

            double a_r = Math.Atan2(xr, yr);
            if (a_r < 0)
                a_r += 2 * Math.PI;

            a_r += Algorithms.Deg2Rad(heading_deg);
            a_r = Algorithms.Wrap2PI(a_r);

            a_deg = Algorithms.Rad2Deg(a_r);
        }

        #endregion

        #endregion

        #region Handlers


        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (uPort != null)
                        uPort.Dispose();

                    if (uGNSSPort != null)
                        uGNSSPort.Dispose();
                }

                disposed = true;
            }
        }

        #endregion

        #region Events

        public EventHandler uPortDetectedChanged;
        public EventHandler DeviceInfoValidChanged;

        public EventHandler IsActiveChanged;
        public EventHandler<LogEventArgs> LogEventHandler;

        public EventHandler IsGNSSActiveChanged;
        public EventHandler uGNSSPortDetectedChanged;

        public EventHandler HeadingUpdated;
        public EventHandler LocationUpdated;

        public EventHandler StateUpdateHandler;

        public EventHandler<RelativeLocationUpdatedEventArgs> RelativeLocationUpdated;
        public EventHandler<AbsoluteLocationUpdatedEventArgs> AbsoluteLocationUpdated;

        public EventHandler LocationOverrideEnabledChanged;

        #endregion
    }
}
