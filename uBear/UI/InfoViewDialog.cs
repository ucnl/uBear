using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uBear.UI
{
    public partial class InfoViewDialog : Form
    {
        #region Properties

        public string InfoText
        {
            get => txb.Text;
            set => txb.Text = value;
        }

        #endregion

        #region Constructor

        public InfoViewDialog()
        {
            InitializeComponent();
        }

        #endregion
    }
}
