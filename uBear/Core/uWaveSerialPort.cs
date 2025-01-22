using System;
using System.Globalization;
using UCNLDrivers;
using UCNLNav;
using UCNLNMEA;

namespace uBear.Core
{
    #region Miscelaneous EventArgs

    public class ACKReceivedEventArgs : EventArgs
    {
        #region Properties

        public ICs SentenceID { get; private set; }
        public LOC_ERR_Enum ErrorID { get; private set; }

        #endregion

        #region Constructor

        public ACKReceivedEventArgs(ICs sntID, LOC_ERR_Enum errID)
        {
            SentenceID = sntID;
            ErrorID = errID;
        }

        #endregion
    }

    public class RCTimeoutReceivedEventArgs : EventArgs
    {
        #region Properties

        public int TxChID { get; private set; }
        public int RxChID { get; private set; }
        public RC_CODES_Enum RCCmdID { get; private set; }

        #endregion

        #region Constructor

        public RCTimeoutReceivedEventArgs(int txChID, int rxChID, RC_CODES_Enum rcCmdID)
        {
            TxChID = txChID;
            RxChID = rxChID;
            RCCmdID = rcCmdID;
        }

        #endregion
    }

    public class RCResponseReceivedEventArgs : EventArgs
    {
        #region Properties

        public int TxChID { get; private set; }
        public int RxChID { get; private set; }
        public RC_CODES_Enum RCCmdID { get; private set; }
        public double PropTime_sec { get; private set; }
        public double MSR_db { get; private set; }
        public double Value { get; private set; }
        public double Azimuth { get; private set; }

        public bool IsValuePresent
        {
            get
            {
                return !double.IsNaN(Value);
            }
        }

        public bool IsAzimuthPresent
        {
            get
            {
                return !double.IsNaN(Azimuth);
            }
        }

        #endregion

        #region Constructor

        public RCResponseReceivedEventArgs(int txChID, int rxChID, RC_CODES_Enum rcCmdID, double pTime_sec, double snr_db)
            : this(txChID, rxChID, rcCmdID, pTime_sec, snr_db, double.NaN, double.NaN)
        {
        }

        public RCResponseReceivedEventArgs(int txChID, int rxChID, RC_CODES_Enum rcCmdID, double pTime_sec, double snr_db, double value)
            : this(txChID, rxChID, rcCmdID, pTime_sec, snr_db, value, double.NaN)
        {
        }

        public RCResponseReceivedEventArgs(int txChID, int rxChID, RC_CODES_Enum rcCmdID, double pTime_sec, double snr_db, double value, double azimuth)
        {
            TxChID = txChID;
            RxChID = rxChID;
            RCCmdID = rcCmdID;
            PropTime_sec = pTime_sec;
            MSR_db = snr_db;
            Value = value;
            Azimuth = azimuth;
        }

        #endregion
    }

    #endregion

    public class uWaveSerialPort : uSerialPort
    {
        #region Properties

        static bool nmeaSingleton = false;

        ICs lastQueryID = ICs.IC_INVALID;
        int rc_query_rxChID = -1;
        

        bool isWaitingLocal = false;
        public bool IsWaitingLocal
        {
            get { return isWaitingLocal; }
            private set
            {
                isWaitingLocal = value;
                IsWaitingLocalChangedEventHandler.Rise(this, new EventArgs());
            }
        }

        bool isWaitingRemote = false;
        public bool IsWaitingRemote
        {
            get { return isWaitingRemote; }
            private set
            {
                isWaitingRemote = value;
                IsWaitingRemoteChangedEventHandler.Rise(this, new EventArgs());
            }
        }

        bool deviceInfoValid = false;
        public bool IsDeviceInfoValid
        {
            get { return deviceInfoValid; }
            private set
            {
                deviceInfoValid = value;
                DeviceInfoValidChanged.Rise(this, new EventArgs());
            }
        }

        public string SerialNumber { get; private set; }
        public string SystemMoniker { get; private set; }
        public string SystemVersion { get; private set; }
        public string CoreMoniker { get; private set; }
        public string CoreVersion { get; private set; }
        public double AcousticBaudrate { get; private set; }
        public int RxChID { get; private set; }
        public int TxChID { get; private set; }
        public int TotalCodeChannels { get; private set; }
        public double SalinityPSU { get; private set; }
        public bool IsPTS { get; private set; }
        public bool IsCommandModeByDefault { get; private set; }

        public AgingValue<double> Pitch_deg { get; private set; }
        public AgingValue<double> Roll_deg { get; private set; }
        public AgingValue<double> Pressure_mBar { get; private set; }
        public AgingValue<double> Temperature_C { get; private set; }
        public AgingValue<double> Depth_m { get; private set; }
        public AgingValue<double> Voltage_V { get; private set; }

        #endregion

        #region Constructor

        public uWaveSerialPort(BaudRate baudRate)
            : base(baudRate)
        {
            base.PortDescription = "UWV";
            base.IsLogIncoming = true;
            base.IsTryAlways = true;

            NMEAInit();

            #region System state values

            Pitch_deg = new AgingValue<double>(3, 10, uWave.degrees1dec_fmtr);
            Roll_deg = new AgingValue<double>(3, 10, uWave.degrees1dec_fmtr);

            Pressure_mBar = new AgingValue<double>(3, 10, uWave.mBar_fmtr);
            Temperature_C = new AgingValue<double>(3, 10, uWave.degC_fmtr);
            Depth_m = new AgingValue<double>(3, 10, uWave.meters1dec_fmtr);
            Voltage_V = new AgingValue<double>(3, 10, uWave.v1dec_fmt);

            #endregion
        }

        #endregion

        #region Methods

        #region private

        private bool TrySend(string message, ICs queryID)
        {
            bool result = detected && !IsWaitingLocal && !IsWaitingRemote;

            if (result)
            {
                try
                {
                    Send(message);

                    if ((queryID == ICs.IC_H2D_SETTINGS_WRITE) ||
                        (queryID == ICs.IC_H2H_PT_SETTINGS_WRITE) ||
                        (queryID == ICs.IC_H2D_AMB_DTA_CFG) ||
                        (queryID == ICs.IC_H2D_INC_DTA_CFG))
                        StartTimer(4000);
                    else
                        StartTimer(1500);

                    IsWaitingLocal = true;

                    lastQueryID = queryID;
                    result = true;
                }
                catch (Exception ex)
                {
                    LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
                }
            }

            return result;
        }

        private void NMEAInit()
        {
            if (!nmeaSingleton)
            {
                nmeaSingleton = true;
                NMEAParser.AddManufacturerToProprietarySentencesBase(ManufacturerCodes.UWV);

                #region Common sentences

                // IC_D2H_ACK             $PUWV0,cmdID,errCode
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "0", "c--c,x");

                // IC_H2D_SETTINGS_WRITE  $PUWV1,rxChID,txChID,styPSU,isCmdMode,isACKOnTXFinished,gravityAcc
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "1", "x,x,x.x,x,x,x.x");

                #endregion

                #region Short code messages management sentences

                // IC_H2D_RC_REQUEST      $PUWV2,txChID,rxChID,rcCmdID
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "2", "x,x,x");

                // IC_D2H_RC_RESPONSE     $PUWV3,txChID,rcCmdID,propTime_seс,snr,[value],[azimuth]
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "3", "x,x,x.x,x.x,x.x,x.x");

                // IC_D2H_RC_TIMEOUT      $PUWV4,txChID,rcCmdID
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "4", "x,x");

                // IC_D2H_RC_ASYNC_IN     $PUWV5,rcCmdID,snr,[azimuth]
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "5", "x,x.x,x.x");

                #endregion

                #region Ambient data management sentences

                // IC_H2D_AMB_DTA_CFG     $PUWV6,isWriteInFlash,periodMs,isPrs,isTemp,isDpt,isBatV
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "6", "x,x,x,x,x,x");

                // IC_D2H_AMB_DTA         $PUWV7,prs_mBar,temp_C,dpt_m,batVoltage_V
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "7", "x.x,x.x,x.x,x.x");

                //
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "8", "x,x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "9", "x.x,x.x,x.x");

                #endregion

                #region Device info

                // IC_H2D_DINFO_GET       $PUWV?,reserved
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "?", "x");

                // IC_D2H_DINFO $PUWV!,serial_number,sys_moniker,sys_version,core_moniker [release],core_version,acBaudrate,rxChID,txChID,totalCh,styPSU,isPTS,isCmdModeDefault                
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UWV, "!", "c--c,c--c,x,c--c,x,x.x,x,x,x,x.x,x,x");

                #endregion
            }
        }

        #region Parsers

        private void Parse_ACK(object[] parameters)
        {
            try
            {
                ICs sntID = uWave.ICsByMessageID((string)parameters[0]);
                LOC_ERR_Enum errID = uWave.O2_LOC_ERR(parameters[1]);

                StopTimer();
                IsWaitingLocal = false;

                if ((sntID == ICs.IC_H2D_RC_REQUEST) ||
                    (sntID == ICs.IC_H2D_PT_ITG))
                {                    
                    IsWaitingRemote = true;
                    StartTimer(6000);
                }

                ACKReceived.Rise(this, new ACKReceivedEventArgs(sntID, errID));
            }
            catch (Exception ex)
            {
                LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        private void Parse_REM_RESPONSE(object[] parameters)
        {
            try
            {
                // IC_D2H_RC_RESPONSE     $PUWV3,txChID,rcCmdID,propTime_seс,snr,[value],[azimuth]
                int txChID = uWave.O2I(parameters[0]);
                RC_CODES_Enum rcCmdID = uWave.O2_RC_CODES(parameters[1]);
                double pTime = uWave.O2D(parameters[2]);
                double msr = uWave.O2D(parameters[3]);
                double value = uWave.O2D(parameters[4]);

                /// TODO: WARNING!!!!
                double azimuth = uWave.O2D(parameters[5]);

                StopTimer();

                RCResponseReceived.Rise(this, new RCResponseReceivedEventArgs(txChID, rc_query_rxChID, rcCmdID, pTime, msr, value, azimuth));

                IsWaitingRemote = false;
            }
            catch (Exception ex)
            {
                LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        private void Parse_REM_TIMEOUT(object[] parameters)
        {
            try
            {
                // IC_D2H_RC_TIMEOUT      $PUWV4,txChID,rcCmdID
                int txChID = uWave.O2I(parameters[0]);
                RC_CODES_Enum rcCmdID = uWave.O2_RC_CODES(parameters[1]);

                StopTimer();
                IsWaitingRemote = false;

                RCTimeoutReceived.Rise(this, new RCTimeoutReceivedEventArgs(txChID, rc_query_rxChID, rcCmdID));
            }
            catch (Exception ex)
            {
                LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        private void Parse_AMB_DTA(object[] parameters)
        {
            try
            {
                // IC_D2H_AMB_DTA         $PUWV7,prs_mBar,temp_C,dpt_m,batVoltage_V
                double prs = uWave.O2D(parameters[0]);
                double temp = uWave.O2D(parameters[1]);
                double dpt = uWave.O2D(parameters[2]);
                double vcc = uWave.O2D(parameters[3]);

                if (!double.IsNaN(prs))
                    Pressure_mBar.Value = prs;

                if (!double.IsNaN(temp))
                    Temperature_C.Value = temp;

                if (!double.IsNaN(dpt))
                    Depth_m.Value = dpt;

                if (!double.IsNaN(vcc))
                    Voltage_V.Value = vcc;

                AMBDataUpdated.Rise(this, new EventArgs());
            }
            catch (Exception ex)
            {
                LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        private void Parse_INC_DTA(object[] parameters)
        {
            try
            {
                // $PUWV9,[reserved empty field],pitch,roll
                double pitch = uWave.O2D(parameters[1]);
                double roll = uWave.O2D(parameters[2]);

                if (!double.IsNaN(pitch))
                    Pitch_deg.Value = pitch;

                if (!double.IsNaN(roll))
                    Roll_deg.Value = roll;

                PTCROLDataUpdated.Rise(this, new EventArgs());
            }
            catch (Exception ex)
            {
                LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        private void Parse_DINFO(object[] parameters)
        {
            try
            {
                // $PUWV!,serial,sys_moniker,sys_version,core_moniker [release],core_version,acBaudrate,rxChID,txChID,maxChannels,styPSU,isPTS,isCmdMode                
                var sn = uWave.O2S(parameters[0]);
                var sysMoniker = uWave.O2S(parameters[1]);
                var sysVersion = uWave.BCDVersionToStr(uWave.O2I(parameters[2]));
                var creMoniker = uWave.O2S(parameters[3]);
                var creVersion = uWave.BCDVersionToStr(uWave.O2I(parameters[4]));
                var acBaudrate = uWave.O2D(parameters[5]);
                var rxChID = uWave.O2I(parameters[6]);
                var txChID = uWave.O2I(parameters[7]);
                var totalCh = uWave.O2I(parameters[8]);
                var styPSU = uWave.O2D(parameters[9]);
                int isPTSFlag = uWave.O2I(parameters[10]);

                bool isPTS = true;
                if (isPTSFlag == 0)
                    isPTS = false;

                var isCmdMode = Convert.ToBoolean(uWave.O2I(parameters[11]));

                StopTimer();
                IsWaitingLocal = false;
                
                SerialNumber = sn;
                SystemMoniker = sysMoniker;
                SystemVersion = sysVersion;
                CoreMoniker = creMoniker;
                CoreVersion = creVersion;
                AcousticBaudrate = acBaudrate;
                RxChID = rxChID;
                TxChID = txChID;
                TotalCodeChannels = totalCh;
                SalinityPSU = styPSU;
                IsPTS = isPTS;
                IsCommandModeByDefault = isCmdMode;

                IsDeviceInfoValid = true;
            }
            catch (Exception ex)
            {
                LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        #endregion

        #endregion

        #region public

        public bool Query_DINFO()
        {
            var msg = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UWV, "?", new object[] { 0 });
            return TrySend(msg, ICs.IC_H2D_DINFO_GET);
        }

        public bool Query_SETTINGS_WRITE(int txChID, int rxChID, double salinityPSU, bool isCmdMode, bool isACKOnTXFinished, double gravityAcc)
        {
            var msg = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UWV, "1", new object[]
            {
                txChID,
                rxChID,
                salinityPSU,
                Convert.ToInt32(isCmdMode),
                Convert.ToInt32(isACKOnTXFinished),
                gravityAcc
            });

            return TrySend(msg, ICs.IC_H2D_SETTINGS_WRITE);
        }

        public bool Query_AMB_CFG_WRITE(bool isSaveToFlash, int periodMs, bool isPressure, bool isTemperature, bool isDepth, bool isVCC)
        {
            var msg = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UWV, "6", new object[]
            {
                Convert.ToInt32(isSaveToFlash),
                periodMs,
                Convert.ToInt32(isPressure),
                Convert.ToInt32(isTemperature),
                Convert.ToInt32(isDepth),
                Convert.ToInt32(isVCC)
            });

            return TrySend(msg, ICs.IC_H2D_AMB_DTA_CFG);
        }

        public bool Query_PTCROL_CFG_WRITE(bool isSaveToFlash, int periodMs)
        {
            var msg = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UWV, "8", new object[]
            {
                Convert.ToInt32(isSaveToFlash),
                periodMs
            });

            return TrySend(msg, ICs.IC_H2D_INC_DTA_CFG);
        }

        public bool Query_RC(int txChID, int rxChID, RC_CODES_Enum cmdID)
        {
            if (!IsWaitingRemote)
            {
                var msg = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UWV, "2", new object[]
                {
                    txChID,
                    rxChID,
                    (int)cmdID
                });

                rc_query_rxChID = rxChID;

                return TrySend(msg, ICs.IC_H2D_RC_REQUEST);
            }
            else
            {
                LogEventHandler.Rise(this, new LogEventArgs(LogLineType.ERROR, "Unable to perform a remote request due to waiting for previous"));
                return false;
            }
        }

        #endregion

        #endregion

        #region uSerialPort

        public override void InitQuerySend()
        {
            var msg = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UWV, "?", new object[] { 0 });
            Send(msg);
        }

        public override void OnClosed()
        {
            StopTimer();
            IsDeviceInfoValid = false;
            isWaitingLocal = false;
            isWaitingRemote = false;
        }

        public override void ProcessIncoming(NMEASentence sentence)
        {
            if (sentence is NMEAProprietarySentence)
            {
                NMEAProprietarySentence pSentence = (sentence as NMEAProprietarySentence);

                if (pSentence.Manufacturer == ManufacturerCodes.UWV)
                {
                    if (!detected)
                    {
                        detected = true;
                        StopTimer();
                    }

                    if (pSentence.SentenceIDString == "0")
                        Parse_ACK(pSentence.parameters);
                    else if (pSentence.SentenceIDString == "3")
                        Parse_REM_RESPONSE(pSentence.parameters);
                    else if (pSentence.SentenceIDString == "4")
                        Parse_REM_TIMEOUT(pSentence.parameters);
                    else if (pSentence.SentenceIDString == "7")
                        Parse_AMB_DTA(pSentence.parameters);
                    else if (pSentence.SentenceIDString == "9")
                        Parse_INC_DTA(pSentence.parameters);
                    else if (pSentence.SentenceIDString == "!")
                        Parse_DINFO(pSentence.parameters);
                }
            }
        }

        #endregion

        #region Events

        public EventHandler IsWaitingLocalChangedEventHandler;
        public EventHandler IsWaitingRemoteChangedEventHandler;

        public EventHandler<ACKReceivedEventArgs> ACKReceived;
        public EventHandler<RCResponseReceivedEventArgs> RCResponseReceived;
        public EventHandler<RCTimeoutReceivedEventArgs> RCTimeoutReceived;

        public EventHandler AMBDataUpdated;
        public EventHandler PTCROLDataUpdated;
        public EventHandler DeviceInfoValidChanged;

        #endregion
    }
}
