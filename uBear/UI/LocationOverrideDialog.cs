using System;
using System.Windows.Forms;

namespace uBear.UI
{
    public partial class LocationOverrideDialog : Form
    {
        #region Properties

        public double Latitude_deg
        {
            get { return Convert.ToDouble(latDegEdit.Value); }
            set { UCNLUI.UIHelpers.SetNumericEditValue(latDegEdit, value); }
        }

        public double Longitude_deg
        {
            get { return Convert.ToDouble(lonDegEdit.Value); }
            set { UCNLUI.UIHelpers.SetNumericEditValue(lonDegEdit, value); }
        }

        public double Heading_deg
        {
            get { return Convert.ToDouble(headingEdit.Value); }
            set { UCNLUI.UIHelpers.SetNumericEditValue(headingEdit, value); }
        }

        #endregion

        public LocationOverrideDialog()
        {
            InitializeComponent();
        }
    }
}
