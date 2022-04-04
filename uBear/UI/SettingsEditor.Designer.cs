namespace uBear.UI
{
    partial class SettingsEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsEditor));
            this.remotesGroup = new System.Windows.Forms.GroupBox();
            this.remotesClb = new System.Windows.Forms.CheckedListBox();
            this.remotesToolStrip = new System.Windows.Forms.ToolStrip();
            this.uncheckAllBtn = new System.Windows.Forms.ToolStripButton();
            this.checkAllBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.inverseBtn = new System.Windows.Forms.ToolStripButton();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.defaultsBtn = new System.Windows.Forms.Button();
            this.isGNSSCompassChb = new System.Windows.Forms.CheckBox();
            this.gnssCompassGroup = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.yOffsetEdit = new System.Windows.Forms.NumericUpDown();
            this.xOffsetEdit = new System.Windows.Forms.NumericUpDown();
            this.headingAdjustEdit = new System.Windows.Forms.NumericUpDown();
            this.gnssCompassBaudrateCbx = new System.Windows.Forms.ComboBox();
            this.isAutoSalinityChb = new System.Windows.Forms.CheckBox();
            this.salinityGroup = new System.Windows.Forms.GroupBox();
            this.salinityEdit = new System.Windows.Forms.NumericUpDown();
            this.speedOfSoundGroup = new System.Windows.Forms.GroupBox();
            this.sosEdit = new System.Windows.Forms.NumericUpDown();
            this.isAutoSOSChb = new System.Windows.Forms.CheckBox();
            this.remotesGroup.SuspendLayout();
            this.remotesToolStrip.SuspendLayout();
            this.gnssCompassGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yOffsetEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xOffsetEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingAdjustEdit)).BeginInit();
            this.salinityGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).BeginInit();
            this.speedOfSoundGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sosEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // remotesGroup
            // 
            this.remotesGroup.Controls.Add(this.remotesClb);
            this.remotesGroup.Controls.Add(this.remotesToolStrip);
            this.remotesGroup.Location = new System.Drawing.Point(12, 12);
            this.remotesGroup.Name = "remotesGroup";
            this.remotesGroup.Size = new System.Drawing.Size(231, 362);
            this.remotesGroup.TabIndex = 0;
            this.remotesGroup.TabStop = false;
            this.remotesGroup.Text = "REMOTES";
            // 
            // remotesClb
            // 
            this.remotesClb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remotesClb.CheckOnClick = true;
            this.remotesClb.FormattingEnabled = true;
            this.remotesClb.IntegralHeight = false;
            this.remotesClb.Location = new System.Drawing.Point(6, 58);
            this.remotesClb.Name = "remotesClb";
            this.remotesClb.Size = new System.Drawing.Size(219, 296);
            this.remotesClb.TabIndex = 1;
            this.remotesClb.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.remotesClb_ItemCheck);
            // 
            // remotesToolStrip
            // 
            this.remotesToolStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.remotesToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.remotesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uncheckAllBtn,
            this.checkAllBtn,
            this.toolStripSeparator1,
            this.inverseBtn});
            this.remotesToolStrip.Location = new System.Drawing.Point(3, 25);
            this.remotesToolStrip.Name = "remotesToolStrip";
            this.remotesToolStrip.Size = new System.Drawing.Size(225, 30);
            this.remotesToolStrip.TabIndex = 0;
            this.remotesToolStrip.Text = "toolStrip1";
            // 
            // uncheckAllBtn
            // 
            this.uncheckAllBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uncheckAllBtn.Image = ((System.Drawing.Image)(resources.GetObject("uncheckAllBtn.Image")));
            this.uncheckAllBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uncheckAllBtn.Name = "uncheckAllBtn";
            this.uncheckAllBtn.Size = new System.Drawing.Size(29, 27);
            this.uncheckAllBtn.Text = "☐";
            this.uncheckAllBtn.ToolTipText = "Unselect all items";
            this.uncheckAllBtn.Click += new System.EventHandler(this.uncheckAllBtn_Click);
            // 
            // checkAllBtn
            // 
            this.checkAllBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkAllBtn.Image = ((System.Drawing.Image)(resources.GetObject("checkAllBtn.Image")));
            this.checkAllBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkAllBtn.Name = "checkAllBtn";
            this.checkAllBtn.Size = new System.Drawing.Size(29, 27);
            this.checkAllBtn.Text = "🗹";
            this.checkAllBtn.ToolTipText = "Select all items";
            this.checkAllBtn.Click += new System.EventHandler(this.checkAllBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // inverseBtn
            // 
            this.inverseBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.inverseBtn.Image = ((System.Drawing.Image)(resources.GetObject("inverseBtn.Image")));
            this.inverseBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inverseBtn.Name = "inverseBtn";
            this.inverseBtn.Size = new System.Drawing.Size(31, 27);
            this.inverseBtn.Text = "🗘";
            this.inverseBtn.ToolTipText = "Inverse selection";
            this.inverseBtn.Click += new System.EventHandler(this.inverseBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(417, 399);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(89, 30);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "CANCEL";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Enabled = false;
            this.okBtn.Location = new System.Drawing.Point(294, 399);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(89, 30);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // defaultsBtn
            // 
            this.defaultsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.defaultsBtn.Location = new System.Drawing.Point(12, 399);
            this.defaultsBtn.Name = "defaultsBtn";
            this.defaultsBtn.Size = new System.Drawing.Size(75, 30);
            this.defaultsBtn.TabIndex = 3;
            this.defaultsBtn.Text = "DEFAULTS";
            this.defaultsBtn.UseVisualStyleBackColor = true;
            this.defaultsBtn.Click += new System.EventHandler(this.defaultsBtn_Click);
            // 
            // isGNSSCompassChb
            // 
            this.isGNSSCompassChb.AutoSize = true;
            this.isGNSSCompassChb.Location = new System.Drawing.Point(257, 12);
            this.isGNSSCompassChb.Name = "isGNSSCompassChb";
            this.isGNSSCompassChb.Size = new System.Drawing.Size(179, 27);
            this.isGNSSCompassChb.TabIndex = 4;
            this.isGNSSCompassChb.Text = "Use GNSS compass";
            this.isGNSSCompassChb.UseVisualStyleBackColor = true;
            this.isGNSSCompassChb.CheckedChanged += new System.EventHandler(this.isGNSSCompassChb_CheckedChanged);
            // 
            // gnssCompassGroup
            // 
            this.gnssCompassGroup.Controls.Add(this.label3);
            this.gnssCompassGroup.Controls.Add(this.label2);
            this.gnssCompassGroup.Controls.Add(this.label1);
            this.gnssCompassGroup.Controls.Add(this.yOffsetEdit);
            this.gnssCompassGroup.Controls.Add(this.xOffsetEdit);
            this.gnssCompassGroup.Controls.Add(this.headingAdjustEdit);
            this.gnssCompassGroup.Controls.Add(this.gnssCompassBaudrateCbx);
            this.gnssCompassGroup.Enabled = false;
            this.gnssCompassGroup.Location = new System.Drawing.Point(257, 39);
            this.gnssCompassGroup.Name = "gnssCompassGroup";
            this.gnssCompassGroup.Size = new System.Drawing.Size(237, 147);
            this.gnssCompassGroup.TabIndex = 5;
            this.gnssCompassGroup.TabStop = false;
            this.gnssCompassGroup.Text = "GNSS compass";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y offset, m";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "X offset, m";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Heading adjust, °";
            // 
            // yOffsetEdit
            // 
            this.yOffsetEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yOffsetEdit.DecimalPlaces = 3;
            this.yOffsetEdit.Location = new System.Drawing.Point(160, 116);
            this.yOffsetEdit.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.yOffsetEdit.Name = "yOffsetEdit";
            this.yOffsetEdit.Size = new System.Drawing.Size(71, 29);
            this.yOffsetEdit.TabIndex = 3;
            // 
            // xOffsetEdit
            // 
            this.xOffsetEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOffsetEdit.DecimalPlaces = 3;
            this.xOffsetEdit.Location = new System.Drawing.Point(160, 86);
            this.xOffsetEdit.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.xOffsetEdit.Name = "xOffsetEdit";
            this.xOffsetEdit.Size = new System.Drawing.Size(71, 29);
            this.xOffsetEdit.TabIndex = 2;
            // 
            // headingAdjustEdit
            // 
            this.headingAdjustEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.headingAdjustEdit.DecimalPlaces = 1;
            this.headingAdjustEdit.Location = new System.Drawing.Point(160, 55);
            this.headingAdjustEdit.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.headingAdjustEdit.Name = "headingAdjustEdit";
            this.headingAdjustEdit.Size = new System.Drawing.Size(71, 29);
            this.headingAdjustEdit.TabIndex = 1;
            // 
            // gnssCompassBaudrateCbx
            // 
            this.gnssCompassBaudrateCbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gnssCompassBaudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gnssCompassBaudrateCbx.FormattingEnabled = true;
            this.gnssCompassBaudrateCbx.Location = new System.Drawing.Point(6, 24);
            this.gnssCompassBaudrateCbx.Name = "gnssCompassBaudrateCbx";
            this.gnssCompassBaudrateCbx.Size = new System.Drawing.Size(225, 29);
            this.gnssCompassBaudrateCbx.TabIndex = 0;
            // 
            // isAutoSalinityChb
            // 
            this.isAutoSalinityChb.AutoSize = true;
            this.isAutoSalinityChb.Enabled = false;
            this.isAutoSalinityChb.Location = new System.Drawing.Point(257, 192);
            this.isAutoSalinityChb.Name = "isAutoSalinityChb";
            this.isAutoSalinityChb.Size = new System.Drawing.Size(126, 27);
            this.isAutoSalinityChb.TabIndex = 6;
            this.isAutoSalinityChb.Text = "Auto salinity";
            this.isAutoSalinityChb.UseVisualStyleBackColor = true;
            this.isAutoSalinityChb.CheckedChanged += new System.EventHandler(this.isAutoSalinityChb_CheckedChanged);
            // 
            // salinityGroup
            // 
            this.salinityGroup.Controls.Add(this.salinityEdit);
            this.salinityGroup.Location = new System.Drawing.Point(257, 219);
            this.salinityGroup.Name = "salinityGroup";
            this.salinityGroup.Size = new System.Drawing.Size(237, 61);
            this.salinityGroup.TabIndex = 6;
            this.salinityGroup.TabStop = false;
            this.salinityGroup.Text = "Salinity, PSU";
            // 
            // salinityEdit
            // 
            this.salinityEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.salinityEdit.DecimalPlaces = 1;
            this.salinityEdit.Location = new System.Drawing.Point(6, 24);
            this.salinityEdit.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.salinityEdit.Name = "salinityEdit";
            this.salinityEdit.Size = new System.Drawing.Size(225, 29);
            this.salinityEdit.TabIndex = 0;
            // 
            // speedOfSoundGroup
            // 
            this.speedOfSoundGroup.Controls.Add(this.sosEdit);
            this.speedOfSoundGroup.Location = new System.Drawing.Point(257, 313);
            this.speedOfSoundGroup.Name = "speedOfSoundGroup";
            this.speedOfSoundGroup.Size = new System.Drawing.Size(237, 61);
            this.speedOfSoundGroup.TabIndex = 7;
            this.speedOfSoundGroup.TabStop = false;
            this.speedOfSoundGroup.Text = "Speed of sound, m/s";
            // 
            // sosEdit
            // 
            this.sosEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sosEdit.DecimalPlaces = 1;
            this.sosEdit.Location = new System.Drawing.Point(6, 24);
            this.sosEdit.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this.sosEdit.Minimum = new decimal(new int[] {
            1300,
            0,
            0,
            0});
            this.sosEdit.Name = "sosEdit";
            this.sosEdit.Size = new System.Drawing.Size(225, 29);
            this.sosEdit.TabIndex = 1;
            this.sosEdit.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // isAutoSOSChb
            // 
            this.isAutoSOSChb.AutoSize = true;
            this.isAutoSOSChb.Location = new System.Drawing.Point(257, 286);
            this.isAutoSOSChb.Name = "isAutoSOSChb";
            this.isAutoSOSChb.Size = new System.Drawing.Size(191, 27);
            this.isAutoSOSChb.TabIndex = 8;
            this.isAutoSOSChb.Text = "Auto speed of sound";
            this.isAutoSOSChb.UseVisualStyleBackColor = true;
            this.isAutoSOSChb.CheckedChanged += new System.EventHandler(this.isAutoSOSChb_CheckedChanged);
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(518, 441);
            this.Controls.Add(this.speedOfSoundGroup);
            this.Controls.Add(this.isAutoSOSChb);
            this.Controls.Add(this.salinityGroup);
            this.Controls.Add(this.isAutoSalinityChb);
            this.Controls.Add(this.gnssCompassGroup);
            this.Controls.Add(this.isGNSSCompassChb);
            this.Controls.Add(this.defaultsBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.remotesGroup);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SettingsEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.remotesGroup.ResumeLayout(false);
            this.remotesGroup.PerformLayout();
            this.remotesToolStrip.ResumeLayout(false);
            this.remotesToolStrip.PerformLayout();
            this.gnssCompassGroup.ResumeLayout(false);
            this.gnssCompassGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yOffsetEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xOffsetEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingAdjustEdit)).EndInit();
            this.salinityGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).EndInit();
            this.speedOfSoundGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sosEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox remotesGroup;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.CheckedListBox remotesClb;
        private System.Windows.Forms.ToolStrip remotesToolStrip;
        private System.Windows.Forms.ToolStripButton uncheckAllBtn;
        private System.Windows.Forms.ToolStripButton checkAllBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton inverseBtn;
        private System.Windows.Forms.Button defaultsBtn;
        private System.Windows.Forms.CheckBox isGNSSCompassChb;
        private System.Windows.Forms.GroupBox gnssCompassGroup;
        private System.Windows.Forms.ComboBox gnssCompassBaudrateCbx;
        private System.Windows.Forms.CheckBox isAutoSalinityChb;
        private System.Windows.Forms.GroupBox salinityGroup;
        private System.Windows.Forms.GroupBox speedOfSoundGroup;
        private System.Windows.Forms.CheckBox isAutoSOSChb;
        private System.Windows.Forms.NumericUpDown salinityEdit;
        private System.Windows.Forms.NumericUpDown sosEdit;
        private System.Windows.Forms.NumericUpDown yOffsetEdit;
        private System.Windows.Forms.NumericUpDown xOffsetEdit;
        private System.Windows.Forms.NumericUpDown headingAdjustEdit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}