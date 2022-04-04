using System.Collections.Generic;
using System.Globalization;
using UCNLNav;

namespace uBear.Core
{
    public class RemoteDescriptor
    {
        #region Properties

        public RemoteAddress Address { get; private set; }

        public bool IsTimeout { get; set; }

        /// <summary>
        /// Time-Of-Flight, seconds
        /// </summary>
        public AgingValue<double> TOF_sec { get; private set; }

        /// <summary>
        /// Slant range, m
        /// </summary>
        public AgingValue<double> SRange_m { get; private set; }

        /// <summary>
        /// Slant range projection, m
        /// </summary>
        public AgingValue<double> PRange_m { get; private set; }

        /// <summary>
        /// Depth, m
        /// </summary>
        public AgingValue<double> Depth_m { get; private set; }

        /// <summary>
        /// Relative azimuth angle, deg
        /// </summary>
        public AgingValue<double> RAzimuth_deg { get; private set; }

        /// <summary>
        /// Main peak to side lobe ratio, dB
        /// </summary>
        public AgingValue<double> MSR_dB { get; private set; }

        /// <summary>
        /// Battery voltage, V
        /// </summary>
        public AgingValue<double> VBat_V { get; private set; }

        /// <summary>
        /// Absolute azimuth, deg
        /// </summary>
        public AgingValue<double> AAzimuth_deg { get; private set; }

        /// <summary>
        /// Absolute range projection (from the GNSS point), m
        /// </summary>
        public AgingValue<double> APRange_m { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AgingValue<double> Latitude_deg { get; private set; }

        public AgingValue<double> Longitude_deg { get; private set; }


        public DHFilter FilterState;

        #endregion

        #region Constructor

        public RemoteDescriptor(RemoteAddress _address)
        {
            Address = _address;

            TOF_sec = new AgingValue<double>(1, 300, x => string.Format("{0:F05} sec", x, CultureInfo.InvariantCulture));
            SRange_m = new AgingValue<double>(1, 300, uWave.meters1dec_fmtr);
            PRange_m = new AgingValue<double>(1, 300, uWave.meters1dec_fmtr);
            Depth_m = new AgingValue<double>(1, 300, uWave.meters1dec_fmtr);
            RAzimuth_deg = new AgingValue<double>(1, 300, uWave.degrees1dec_fmtr);
            MSR_dB = new AgingValue<double>(1, 300, uWave.db_fmtr);
            VBat_V = new AgingValue<double>(1, 300, uWave.v1dec_fmt);
            AAzimuth_deg = new AgingValue<double>(1, 300, uWave.degrees1dec_fmtr);
            APRange_m = new AgingValue<double>(1, 300, uWave.meters1dec_fmtr);
            Latitude_deg = new AgingValue<double>(1, 300, uWave.latlon_fmtr);
            Longitude_deg = new AgingValue<double>(1, 300, uWave.latlon_fmtr);
        }

        #endregion

        #region Methods

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (IsTimeout)
                result.Add("TIMEOUT", "TRUE");

            if (APRange_m.IsInitialized)
                result.Add("APR", APRange_m.ToString());
            else if (PRange_m.IsInitialized)
                result.Add("PRN", PRange_m.ToString());
            else 
                result.Add("SRN", SRange_m.ToString());

            if (AAzimuth_deg.IsInitialized)
                result.Add("AAZM", AAzimuth_deg.ToString());
            else if (RAzimuth_deg.IsInitialized)
                result.Add("RAZM", RAzimuth_deg.ToString());
            
            if (Depth_m.IsInitialized)
                result.Add("DPT", Depth_m.ToString());

            if (VBat_V.IsInitialized)
                result.Add("BAT", VBat_V.ToString());

            if (MSR_dB.IsInitialized)
                result.Add("MSR", MSR_dB.ToString());

            if (Latitude_deg.IsInitialized)
                result.Add("LAT", Latitude_deg.ToString());

            if (Longitude_deg.IsInitialized)
                result.Add("LON", Longitude_deg.ToString());

            return result;
        }


        #endregion
    }
}
