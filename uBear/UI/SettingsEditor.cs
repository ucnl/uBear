using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UCNLDrivers;

namespace uBear.UI
{
    public partial class SettingsEditor : Form
    {
        #region Properties

        public SettingsContainer Value
        {
            get
            {
                SettingsContainer result = new SettingsContainer();

                result.UseAUXGNSSCompas = IsUseGNSSCompass;
                result.AUXGNSSCompasBaudrate = gnssCompassBaudrate;
                result.HeadingAdjust_deg = headingAdjust_deg;
                result.LongitudalOffset_m = yOffset_m;
                result.TransverseOffset_m = xOffset_m;

                result.Remotes = usedRemotes;

                result.IsAutoSalinity = IsAutoSalinity;
                result.WaterSalinity_PSU = SalinityPSU;

                result.IsAutoSoundSpeed = IsAutoSoundSpeed;
                result.SoundSpeed_mps = SoundSpeedMps;

                return result;
            }
            set
            {
                IsUseGNSSCompass = value.UseAUXGNSSCompas;
                gnssCompassBaudrate = value.AUXGNSSCompasBaudrate;
                headingAdjust_deg = value.HeadingAdjust_deg;
                yOffset_m = value.LongitudalOffset_m;
                xOffset_m = value.TransverseOffset_m;

                usedRemotes = value.Remotes;

                IsAutoSalinity = value.IsAutoSalinity;
                SalinityPSU = value.WaterSalinity_PSU;

                IsAutoSoundSpeed = value.IsAutoSoundSpeed;
                SoundSpeedMps = value.SoundSpeed_mps;
            }
        }

        bool IsUseGNSSCompass
        {
            get { return isGNSSCompassChb.Checked; }
            set { isGNSSCompassChb.Checked = value; }
        }

        BaudRate gnssCompassBaudrate
        {
            get
            {
                return (BaudRate)Enum.Parse(typeof(BaudRate), gnssCompassBaudrateCbx.SelectedItem.ToString());
            }
            set
            {
                int idx = gnssCompassBaudrateCbx.Items.IndexOf(value.ToString());
                if (idx >= 0)
                    gnssCompassBaudrateCbx.SelectedIndex = idx;
            }
        }

        double headingAdjust_deg
        {
            get { return Convert.ToDouble(headingAdjustEdit.Value); }
            set
            {
                decimal val = Convert.ToDecimal(value);
                if (val < headingAdjustEdit.Minimum) val = headingAdjustEdit.Minimum;
                if (val > headingAdjustEdit.Maximum) val = headingAdjustEdit.Maximum;
                headingAdjustEdit.Value = val;
            }
        }

        double xOffset_m
        {
            get { return Convert.ToDouble(xOffsetEdit.Value); }
            set
            {
                decimal val = Convert.ToDecimal(value);
                if (val < xOffsetEdit.Minimum) val = xOffsetEdit.Minimum;
                if (val > xOffsetEdit.Maximum) val = xOffsetEdit.Maximum;
                xOffsetEdit.Value = val;
            }
        }

        double yOffset_m
        {
            get { return Convert.ToDouble(yOffsetEdit.Value); }
            set
            {
                decimal val = Convert.ToDecimal(value);
                if (val < yOffsetEdit.Minimum) val = yOffsetEdit.Minimum;
                if (val > yOffsetEdit.Maximum) val = yOffsetEdit.Maximum;
                yOffsetEdit.Value = val;
            }
        }

        RemoteAddress[] usedRemotes
        {
            get
            {
                List<RemoteAddress> result = new List<RemoteAddress>();

                for (int i = 0; i < remotesClb.Items.Count; i++)
                    if (remotesClb.GetItemCheckState(i) == CheckState.Checked)
                        result.Add((RemoteAddress)Enum.Parse(typeof(RemoteAddress), remotesClb.Items[i].ToString()));

                return result.ToArray();
            }
            set
            {
                for (int i = 0; i < remotesClb.Items.Count; i++)
                    remotesClb.SetItemChecked(i, false);

                foreach (var item in value)
                {
                    int idx = remotesClb.Items.IndexOf(item.ToString());
                    if (idx >= 0)
                        remotesClb.SetItemChecked(idx, true);
                }
            }
        }

        bool IsAutoSalinity
        {
            get { return isAutoSalinityChb.Checked; }
            set { isAutoSalinityChb.Checked = value; }
        }

        double SalinityPSU
        {
            get { return Convert.ToDouble(salinityEdit.Value); }
            set
            {
                decimal val = Convert.ToDecimal(value);
                if (val < salinityEdit.Minimum) val = salinityEdit.Minimum;
                if (val > salinityEdit.Maximum) val = salinityEdit.Maximum;

                salinityEdit.Value = val;
            }
        }

        bool IsAutoSoundSpeed
        {
            get { return isAutoSOSChb.Checked; }
            set { isAutoSOSChb.Checked = value; }
        }

        double SoundSpeedMps
        {
            get { return Convert.ToDouble(sosEdit.Value); }
            set
            {
                decimal val = Convert.ToDecimal(value);
                if (val < sosEdit.Minimum) val = sosEdit.Minimum;
                if (val > sosEdit.Maximum) val = sosEdit.Maximum;

                sosEdit.Value = val;
            }
        }


        #endregion

        #region Constructor

        public SettingsEditor()
        {
            InitializeComponent();

            remotesClb.Items.Clear();
            remotesClb.Items.AddRange(Enum.GetNames(typeof(RemoteAddress)));

            gnssCompassBaudrateCbx.Items.Clear();
            gnssCompassBaudrateCbx.Items.AddRange(Enum.GetNames(typeof(BaudRate)));

            gnssCompassBaudrate = BaudRate.baudRate38400;

        }

        #endregion        

        #region Handlers

        private void uncheckAllBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < remotesClb.Items.Count; i++)
                remotesClb.SetItemChecked(i, false);
        }

        private void checkAllBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < remotesClb.Items.Count; i++)
                remotesClb.SetItemChecked(i, true);
        }

        private void inverseBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < remotesClb.Items.Count; i++)
                remotesClb.SetItemChecked(i, !remotesClb.GetItemChecked(i));
        }

        private void isGNSSCompassChb_CheckedChanged(object sender, EventArgs e)
        {
            gnssCompassGroup.Enabled = isGNSSCompassChb.Checked;

            if (!isGNSSCompassChb.Checked)
            {
                isAutoSalinityChb.Checked = false;
                isAutoSalinityChb.Enabled = false;
            }
            else
            {
                isAutoSalinityChb.Enabled = true;
            }
        }


        private void isAutoSalinityChb_CheckedChanged(object sender, EventArgs e)
        {
            salinityGroup.Enabled = !isAutoSalinityChb.Checked;
        }

        private void isAutoSOSChb_CheckedChanged(object sender, EventArgs e)
        {
            speedOfSoundGroup.Enabled = !isAutoSOSChb.Checked;
        }

        private void remotesClb_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            okBtn.Enabled = (e.NewValue == CheckState.Checked) || (remotesClb.CheckedItems.Count > 1);
        }


        private void defaultsBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset settings to defaults?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                Value = new SettingsContainer();
        }

        #endregion
    }
}
