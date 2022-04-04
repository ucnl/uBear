using System;
using System.Globalization;
using UCNLDrivers;
using UCNLNav;
using UCNLNMEA;

namespace uBear.Core
{
    public class uGNSSSerialPort : uSerialPort
    {
        #region Properties

        public AgingValue<double> Heading { get; private set; }
        public AgingValue<double> Latitude { get; private set; }
        public AgingValue<double> Longitude { get; private set; }
        public AgingValue<double> GroundSpeed { get; private set; }
        public AgingValue<double> CourseOverGround { get; private set; }

        public AgingValue<DateTime> GNSSTime { get; private set; }

        #endregion

        #region Constructor

        public uGNSSSerialPort(BaudRate baudRate)
            : base(baudRate)
        {
            base.PortDescription = "GNSS";
            base.IsLogIncoming = true;
            base.IsTryAlways = true;

            Heading = new AgingValue<double>(2, 4, uWave.degrees1dec_fmtr);
            Latitude = new AgingValue<double>(2, 4, uWave.latlon_fmtr);
            Longitude = new AgingValue<double>(2, 4, uWave.latlon_fmtr);
            GroundSpeed = new AgingValue<double>(2, 4, x => string.Format("{0:F01} m/s ({1:F01} km/h)", x / 3.6, x));
            CourseOverGround = new AgingValue<double>(2, 4, uWave.degrees1dec_fmtr);
            GNSSTime = new AgingValue<DateTime>(2, 4, x => x.ToString(CultureInfo.InvariantCulture));
        }

        #endregion

        #region uSerialPort

        public override void InitQuerySend()
        {
            // no request needed for a GNSS compass
        }

        public override void OnClosed()
        {
            // ?
        }

        public override void ProcessIncoming(NMEASentence sentence)
        {
            if (sentence is NMEAStandartSentence)
            {
                NMEAStandartSentence nSentence = (sentence as NMEAStandartSentence);

                if (detected)
                    ResetTimer();

                if (nSentence.SentenceID == SentenceIdentifiers.HDT)
                {
                    if (!detected)
                        detected = true;

                    double hdn = uWave.O2D(nSentence.parameters[0]);
                    if (!double.IsNaN(hdn))
                    {
                        Heading.Value = hdn;
                        HeadingUpdated.Rise(this, new EventArgs());
                    }
                }
                else if (nSentence.SentenceID == SentenceIdentifiers.RMC)
                {
                    DateTime tStamp = nSentence.parameters[0] == null ? DateTime.MinValue : (DateTime)nSentence.parameters[0];

                    var latitude = uWave.O2D(nSentence.parameters[2]);
                    var longitude = uWave.O2D(nSentence.parameters[4]);
                    var groundSpeed = uWave.O2D(nSentence.parameters[6]);
                    var courseOverGround = uWave.O2D(nSentence.parameters[7]);
                    DateTime dateTime = nSentence.parameters[8] == null ? DateTime.MinValue : (DateTime)nSentence.parameters[8];

                    bool isValid = (nSentence.parameters[1].ToString() != "Invalid") &&
                                   (!double.IsNaN(latitude)) && latitude.IsValidLatDeg() &&
                                   (!double.IsNaN(longitude)) && longitude.IsValidLonDeg() &&
                                   (!double.IsNaN(groundSpeed)) &&
                                   (nSentence.parameters[11].ToString() != "N");

                    if (isValid)
                    {
                        dateTime = dateTime.AddHours(tStamp.Hour);
                        dateTime = dateTime.AddMinutes(tStamp.Minute);
                        dateTime = dateTime.AddSeconds(tStamp.Second);
                        dateTime = dateTime.AddMilliseconds(tStamp.Millisecond);
                        groundSpeed = 3.6 * NMEAParser.Bend2MpS(groundSpeed);

                        if (nSentence.parameters[3].ToString() == "S") latitude = -latitude;
                        if (nSentence.parameters[5].ToString() == "W") longitude = -longitude;

                        Latitude.Value = latitude;
                        Longitude.Value = longitude;
                        GroundSpeed.Value = groundSpeed;
                        CourseOverGround.Value = courseOverGround;
                        GNSSTime.Value = dateTime;

                        LocationUpdated.Rise(this, new EventArgs());
                    }
                }
            }
        }

        #endregion

        #region Events

        public EventHandler HeadingUpdated;
        public EventHandler LocationUpdated;

        #endregion
    }
}
