using System;
using UCNLDrivers;

namespace uBear
{
    public enum RemoteAddress
    {
        Remote_1 = 1,
        Remote_2 = 2,
        Remote_3 = 3,
        Remote_4 = 4,
        Remote_5 = 5,
        Remote_6 = 6,
        Remote_7 = 7,
        Remote_8 = 8
    }

    [Serializable]
    public class SettingsContainer : SimpleSettingsContainer
    {
        #region Properties

        public bool UseAUXGNSSCompas;
        public BaudRate AUXGNSSCompasBaudrate;

        public double HeadingAdjust_deg;
        public double LongitudalOffset_m;
        public double TransverseOffset_m;

        public bool IsAutoSalinity;
        public bool IsAutoSoundSpeed;
        public double WaterSalinity_PSU;
        public double SoundSpeed_mps;

        public RemoteAddress[] Remotes;

        #endregion

        #region SimpleSettingsContainer

        public override void SetDefaults()
        {
            UseAUXGNSSCompas = false;
            AUXGNSSCompasBaudrate = BaudRate.baudRate38400;

            HeadingAdjust_deg = 0;
            LongitudalOffset_m = 0;
            TransverseOffset_m = 0;

            IsAutoSoundSpeed = true;
            WaterSalinity_PSU = 0.0;
            SoundSpeed_mps = UCNLPhysics.PHX.PHX_FWTR_SOUND_SPEED_MPS;

            Remotes = new RemoteAddress[] { RemoteAddress.Remote_1 };
        }

        #endregion
    }
}
