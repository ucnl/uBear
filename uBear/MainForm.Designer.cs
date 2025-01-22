namespace uBear
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.uPortStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gnssPortStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.connectionBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.infoBtn = new System.Windows.Forms.ToolStripButton();
            this.settingsBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.logBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.logViewBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.logPlaybackBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.logClearAllEmptiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.logClearAllBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.utilsBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.trackBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.trackExportBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.trackClearBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.deviceBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceViewInfoBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.utilsLocationOverrideBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.noteTxb = new System.Windows.Forms.ToolStripTextBox();
            this.addNoteBtn = new System.Windows.Forms.ToolStripButton();
            this.plotPanel = new System.Windows.Forms.Panel();
            this.radialPlot = new UCNLUI.Controls.RadialPlot();
            this.plotToolStrip = new System.Windows.Forms.ToolStrip();
            this.colorSchemeBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.colorSchemeDefaultDarkBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.colorSchemeBrightBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.colorSchemeMonogreenBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.isLimboVisibleBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.isHistoryVisibleBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.isNotesVisibleBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.isMiscInfoVisibleBtn = new System.Windows.Forms.ToolStripButton();
            this.remotesViewPanel = new System.Windows.Forms.Panel();
            this.remotesTreeView = new System.Windows.Forms.TreeView();
            this.remotesViewToolStrip = new System.Windows.Forms.ToolStrip();
            this.treeExpandBtn = new System.Windows.Forms.ToolStripButton();
            this.treeCollapseBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mainStatusStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.plotPanel.SuspendLayout();
            this.plotToolStrip.SuspendLayout();
            this.remotesViewPanel.SuspendLayout();
            this.remotesViewToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uPortStatusLbl,
            this.toolStripStatusLabel1,
            this.gnssPortStatusLbl});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 519);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(999, 34);
            this.mainStatusStrip.TabIndex = 0;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // uPortStatusLbl
            // 
            this.uPortStatusLbl.BackColor = System.Drawing.SystemColors.Control;
            this.uPortStatusLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uPortStatusLbl.ForeColor = System.Drawing.Color.Gray;
            this.uPortStatusLbl.Name = "uPortStatusLbl";
            this.uPortStatusLbl.Size = new System.Drawing.Size(34, 28);
            this.uPortStatusLbl.Text = ". . .";
            this.uPortStatusLbl.ToolTipText = "Status of the base station ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(17, 28);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // gnssPortStatusLbl
            // 
            this.gnssPortStatusLbl.BackColor = System.Drawing.SystemColors.Control;
            this.gnssPortStatusLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gnssPortStatusLbl.ForeColor = System.Drawing.Color.Gray;
            this.gnssPortStatusLbl.Name = "gnssPortStatusLbl";
            this.gnssPortStatusLbl.Size = new System.Drawing.Size(34, 28);
            this.gnssPortStatusLbl.Text = ". . .";
            this.gnssPortStatusLbl.ToolTipText = "Status of the auxilary GNSS compass";
            this.gnssPortStatusLbl.Visible = false;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionBtn,
            this.toolStripSeparator1,
            this.infoBtn,
            this.settingsBtn,
            this.toolStripSeparator2,
            this.logBtn,
            this.toolStripSeparator3,
            this.utilsBtn,
            this.toolStripSeparator4,
            this.noteTxb,
            this.addNoteBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.mainToolStrip.Size = new System.Drawing.Size(999, 39);
            this.mainToolStrip.TabIndex = 1;
            // 
            // connectionBtn
            // 
            this.connectionBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.connectionBtn.Image = ((System.Drawing.Image)(resources.GetObject("connectionBtn.Image")));
            this.connectionBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectionBtn.Name = "connectionBtn";
            this.connectionBtn.Size = new System.Drawing.Size(94, 36);
            this.connectionBtn.Text = "🔌LINK";
            this.connectionBtn.ToolTipText = "Enables or disables connection to the base station and GNSS compass (if GNSS comp" +
    "ass is enabled in the settings). After the connection is established, remotes po" +
    "lling will start automatically";
            this.connectionBtn.Click += new System.EventHandler(this.connectionBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // infoBtn
            // 
            this.infoBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoBtn.Image = ((System.Drawing.Image)(resources.GetObject("infoBtn.Image")));
            this.infoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(107, 36);
            this.infoBtn.Text = "ℹ INFO";
            this.infoBtn.ToolTipText = "Additional information and links";
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingsBtn.Image")));
            this.settingsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(155, 36);
            this.settingsBtn.Text = "⚙ SETTINGS";
            this.settingsBtn.ToolTipText = "Opens the settings editor. To apply new settings the application should be restar" +
    "ted.";
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // logBtn
            // 
            this.logBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.logBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logViewBtn,
            this.logPlaybackBtn,
            this.toolStripSeparator5,
            this.logClearAllEmptiesBtn,
            this.logClearAllBtn});
            this.logBtn.Image = ((System.Drawing.Image)(resources.GetObject("logBtn.Image")));
            this.logBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logBtn.Name = "logBtn";
            this.logBtn.Size = new System.Drawing.Size(109, 36);
            this.logBtn.Text = "📖 LOG";
            this.logBtn.ToolTipText = "Log related menu";
            // 
            // logViewBtn
            // 
            this.logViewBtn.Name = "logViewBtn";
            this.logViewBtn.Size = new System.Drawing.Size(342, 36);
            this.logViewBtn.Text = "👁 View current log";
            this.logViewBtn.ToolTipText = "Opens the current log file in the system\'s default application associated with *." +
    "log files (e.g. Notepad)";
            this.logViewBtn.Click += new System.EventHandler(this.logViewBtn_Click);
            // 
            // logPlaybackBtn
            // 
            this.logPlaybackBtn.Name = "logPlaybackBtn";
            this.logPlaybackBtn.Size = new System.Drawing.Size(342, 36);
            this.logPlaybackBtn.Text = "▶ Playback...";
            this.logPlaybackBtn.ToolTipText = "Select a log-file to playback";
            this.logPlaybackBtn.Click += new System.EventHandler(this.logPlaybackBtn_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(339, 6);
            // 
            // logClearAllEmptiesBtn
            // 
            this.logClearAllEmptiesBtn.Name = "logClearAllEmptiesBtn";
            this.logClearAllEmptiesBtn.Size = new System.Drawing.Size(342, 36);
            this.logClearAllEmptiesBtn.Text = "🧹 Clear all empty logs";
            this.logClearAllEmptiesBtn.ToolTipText = "Clears all log files that has a size less than 1 kb";
            this.logClearAllEmptiesBtn.Click += new System.EventHandler(this.logClearAllEmptiesBtn_Click);
            // 
            // logClearAllBtn
            // 
            this.logClearAllBtn.Name = "logClearAllBtn";
            this.logClearAllBtn.Size = new System.Drawing.Size(342, 36);
            this.logClearAllBtn.Text = "🗑 Clear all entries";
            this.logClearAllBtn.ToolTipText = "Deletes all log entries. WARNING! This action cannot be undone";
            this.logClearAllBtn.Click += new System.EventHandler(this.logClearAllBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // utilsBtn
            // 
            this.utilsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.utilsBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackBtn,
            this.toolStripSeparator10,
            this.deviceBtn,
            this.toolStripSeparator11,
            this.utilsLocationOverrideBtn});
            this.utilsBtn.Image = ((System.Drawing.Image)(resources.GetObject("utilsBtn.Image")));
            this.utilsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.utilsBtn.Name = "utilsBtn";
            this.utilsBtn.Size = new System.Drawing.Size(123, 36);
            this.utilsBtn.Text = "🛠 UTILS";
            this.utilsBtn.ToolTipText = "Additional functions";
            // 
            // trackBtn
            // 
            this.trackBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackExportBtn,
            this.toolStripSeparator9,
            this.trackClearBtn});
            this.trackBtn.Enabled = false;
            this.trackBtn.Name = "trackBtn";
            this.trackBtn.Size = new System.Drawing.Size(293, 36);
            this.trackBtn.Text = "🗺 Tracks";
            this.trackBtn.ToolTipText = "Tracks submenu";
            // 
            // trackExportBtn
            // 
            this.trackExportBtn.Name = "trackExportBtn";
            this.trackExportBtn.Size = new System.Drawing.Size(204, 36);
            this.trackExportBtn.Text = "💾 Export";
            this.trackExportBtn.ToolTipText = "Press to save the current base station & remotes tracks as KML or CSV file";
            this.trackExportBtn.Click += new System.EventHandler(this.trackExportBtn_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(201, 6);
            // 
            // trackClearBtn
            // 
            this.trackClearBtn.Name = "trackClearBtn";
            this.trackClearBtn.Size = new System.Drawing.Size(204, 36);
            this.trackClearBtn.Text = "🧹 Clear";
            this.trackClearBtn.ToolTipText = "Clears the current tracks. WARNING! This action cannot be undone";
            this.trackClearBtn.Click += new System.EventHandler(this.trackClearBtn_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(290, 6);
            // 
            // deviceBtn
            // 
            this.deviceBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceViewInfoBtn});
            this.deviceBtn.Enabled = false;
            this.deviceBtn.Name = "deviceBtn";
            this.deviceBtn.Size = new System.Drawing.Size(293, 36);
            this.deviceBtn.Text = "🤖 Device";
            // 
            // deviceViewInfoBtn
            // 
            this.deviceViewInfoBtn.Name = "deviceViewInfoBtn";
            this.deviceViewInfoBtn.Size = new System.Drawing.Size(211, 36);
            this.deviceViewInfoBtn.Text = "View info...";
            this.deviceViewInfoBtn.Click += new System.EventHandler(this.deviceViewInfoBtn_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(290, 6);
            // 
            // utilsLocationOverrideBtn
            // 
            this.utilsLocationOverrideBtn.Name = "utilsLocationOverrideBtn";
            this.utilsLocationOverrideBtn.Size = new System.Drawing.Size(293, 36);
            this.utilsLocationOverrideBtn.Text = "Override location...";
            this.utilsLocationOverrideBtn.Click += new System.EventHandler(this.overrideLocation_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // noteTxb
            // 
            this.noteTxb.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.noteTxb.MaxLength = 1024;
            this.noteTxb.Name = "noteTxb";
            this.noteTxb.Size = new System.Drawing.Size(200, 39);
            this.noteTxb.ToolTipText = "To add a note to the current log simply type the text you want and press \'Enter\'." +
    " ";
            this.noteTxb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noteTxb_KeyDown);
            this.noteTxb.TextChanged += new System.EventHandler(this.noteTxb_TextChanged);
            // 
            // addNoteBtn
            // 
            this.addNoteBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addNoteBtn.Enabled = false;
            this.addNoteBtn.Image = ((System.Drawing.Image)(resources.GetObject("addNoteBtn.Image")));
            this.addNoteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNoteBtn.Name = "addNoteBtn";
            this.addNoteBtn.Size = new System.Drawing.Size(166, 35);
            this.addNoteBtn.Text = "📝 ADD NOTE";
            this.addNoteBtn.ToolTipText = "To add a note to the current log simply type the text you want and press \'Enter\'." +
    " ";
            this.addNoteBtn.Click += new System.EventHandler(this.addNoteBtn_Click);
            // 
            // plotPanel
            // 
            this.plotPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotPanel.Controls.Add(this.radialPlot);
            this.plotPanel.Controls.Add(this.plotToolStrip);
            this.plotPanel.Location = new System.Drawing.Point(0, 39);
            this.plotPanel.Margin = new System.Windows.Forms.Padding(1);
            this.plotPanel.Name = "plotPanel";
            this.plotPanel.Size = new System.Drawing.Size(754, 484);
            this.plotPanel.TabIndex = 2;
            // 
            // radialPlot
            // 
            this.radialPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radialPlot.AxisBackgroundLabelColor = System.Drawing.Color.Black;
            this.radialPlot.AxisColor = System.Drawing.Color.LightGray;
            this.radialPlot.AxisLabelColor = System.Drawing.Color.WhiteSmoke;
            this.radialPlot.AxisLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radialPlot.BackColor = System.Drawing.Color.Black;
            this.radialPlot.BoatColor = System.Drawing.Color.Yellow;
            this.radialPlot.BoatSize = 20;
            this.radialPlot.BoatVisible = true;
            this.radialPlot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.radialPlot.Heading = double.NaN;
            this.radialPlot.HistoryLinesNumber = 4;
            this.radialPlot.HistoryTextColor = System.Drawing.Color.SpringGreen;
            this.radialPlot.HistoryTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radialPlot.HistoryVisible = true;
            this.radialPlot.LeftUpperText = null;
            this.radialPlot.LeftUpperTextBackgoundColor = System.Drawing.Color.Black;
            this.radialPlot.LeftUpperTextColor = System.Drawing.Color.GreenYellow;
            this.radialPlot.LeftUpperTextFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radialPlot.LeftUpperTextVisible = true;
            this.radialPlot.LimboColor = System.Drawing.Color.LightSeaGreen;
            this.radialPlot.LimboThickness = 10;
            this.radialPlot.LimboTickAngleStep = 15F;
            this.radialPlot.LimboTickCap = System.Drawing.Drawing2D.LineCap.Triangle;
            this.radialPlot.LimboTickColor = System.Drawing.Color.LightGray;
            this.radialPlot.LimboTickThickness = 10;
            this.radialPlot.LimboVisible = true;
            this.radialPlot.Location = new System.Drawing.Point(2, 37);
            this.radialPlot.Margin = new System.Windows.Forms.Padding(2);
            this.radialPlot.MaxHistoryLineLength = 127;
            this.radialPlot.Name = "radialPlot";
            this.radialPlot.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.radialPlot.RightUpperTextBackgoundColor = System.Drawing.Color.Black;
            this.radialPlot.RightUpperTextColor = System.Drawing.Color.Wheat;
            this.radialPlot.RightUpperTextFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radialPlot.RightUpperTextVisible = true;
            this.radialPlot.Size = new System.Drawing.Size(750, 447);
            this.radialPlot.TabIndex = 1;
            this.radialPlot.TargetCircleColor = System.Drawing.Color.Goldenrod;
            this.radialPlot.TargetDistLineColor = System.Drawing.Color.Gold;
            this.radialPlot.TargetLabelBackgroundColor = System.Drawing.Color.Yellow;
            this.radialPlot.TargetLabelColor = System.Drawing.Color.Black;
            this.radialPlot.TargetLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // plotToolStrip
            // 
            this.plotToolStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plotToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.plotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorSchemeBtn,
            this.isLimboVisibleBtn,
            this.toolStripSeparator6,
            this.isHistoryVisibleBtn,
            this.toolStripSeparator7,
            this.isNotesVisibleBtn,
            this.toolStripSeparator8,
            this.isMiscInfoVisibleBtn});
            this.plotToolStrip.Location = new System.Drawing.Point(0, 0);
            this.plotToolStrip.Name = "plotToolStrip";
            this.plotToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.plotToolStrip.Size = new System.Drawing.Size(754, 35);
            this.plotToolStrip.TabIndex = 0;
            this.plotToolStrip.Text = "toolStrip2";
            // 
            // colorSchemeBtn
            // 
            this.colorSchemeBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.colorSchemeBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.colorSchemeBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorSchemeDefaultDarkBtn,
            this.colorSchemeBrightBtn,
            this.colorSchemeMonogreenBtn});
            this.colorSchemeBtn.Image = ((System.Drawing.Image)(resources.GetObject("colorSchemeBtn.Image")));
            this.colorSchemeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.colorSchemeBtn.Name = "colorSchemeBtn";
            this.colorSchemeBtn.Size = new System.Drawing.Size(199, 32);
            this.colorSchemeBtn.Text = "🎨 COLOR SCHEME";
            this.colorSchemeBtn.ToolTipText = "You can select a preferable color scheme";
            // 
            // colorSchemeDefaultDarkBtn
            // 
            this.colorSchemeDefaultDarkBtn.Checked = true;
            this.colorSchemeDefaultDarkBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colorSchemeDefaultDarkBtn.Name = "colorSchemeDefaultDarkBtn";
            this.colorSchemeDefaultDarkBtn.Size = new System.Drawing.Size(269, 32);
            this.colorSchemeDefaultDarkBtn.Text = "DefaultDark";
            this.colorSchemeDefaultDarkBtn.ToolTipText = "Default dark scheme for dark ambience";
            this.colorSchemeDefaultDarkBtn.Click += new System.EventHandler(this.colorSchemeSubItem_Click);
            // 
            // colorSchemeBrightBtn
            // 
            this.colorSchemeBrightBtn.Name = "colorSchemeBrightBtn";
            this.colorSchemeBrightBtn.Size = new System.Drawing.Size(269, 32);
            this.colorSchemeBrightBtn.Text = "Bright";
            this.colorSchemeBrightBtn.ToolTipText = "Brigth color scheme for outdoor";
            this.colorSchemeBrightBtn.Click += new System.EventHandler(this.colorSchemeSubItem_Click);
            // 
            // colorSchemeMonogreenBtn
            // 
            this.colorSchemeMonogreenBtn.Name = "colorSchemeMonogreenBtn";
            this.colorSchemeMonogreenBtn.Size = new System.Drawing.Size(269, 32);
            this.colorSchemeMonogreenBtn.Text = "MonochromeGreen";
            this.colorSchemeMonogreenBtn.ToolTipText = "Alternative high-contrast color scheme";
            this.colorSchemeMonogreenBtn.Click += new System.EventHandler(this.colorSchemeSubItem_Click);
            // 
            // isLimboVisibleBtn
            // 
            this.isLimboVisibleBtn.Checked = true;
            this.isLimboVisibleBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isLimboVisibleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isLimboVisibleBtn.Image = ((System.Drawing.Image)(resources.GetObject("isLimboVisibleBtn.Image")));
            this.isLimboVisibleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isLimboVisibleBtn.Name = "isLimboVisibleBtn";
            this.isLimboVisibleBtn.Size = new System.Drawing.Size(99, 32);
            this.isLimboVisibleBtn.Text = "⛯ LIMBO";
            this.isLimboVisibleBtn.ToolTipText = "Switches visibility of the external limbo";
            this.isLimboVisibleBtn.Click += new System.EventHandler(this.isLimboVisibleBtn_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 35);
            // 
            // isHistoryVisibleBtn
            // 
            this.isHistoryVisibleBtn.Checked = true;
            this.isHistoryVisibleBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isHistoryVisibleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isHistoryVisibleBtn.Image = ((System.Drawing.Image)(resources.GetObject("isHistoryVisibleBtn.Image")));
            this.isHistoryVisibleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isHistoryVisibleBtn.Name = "isHistoryVisibleBtn";
            this.isHistoryVisibleBtn.Size = new System.Drawing.Size(125, 32);
            this.isHistoryVisibleBtn.Text = "🕑 HISTORY";
            this.isHistoryVisibleBtn.ToolTipText = "Switches visibility of history (log)";
            this.isHistoryVisibleBtn.Click += new System.EventHandler(this.isHistoryVisibleBtn_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 35);
            // 
            // isNotesVisibleBtn
            // 
            this.isNotesVisibleBtn.Checked = true;
            this.isNotesVisibleBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isNotesVisibleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isNotesVisibleBtn.Image = ((System.Drawing.Image)(resources.GetObject("isNotesVisibleBtn.Image")));
            this.isNotesVisibleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isNotesVisibleBtn.Name = "isNotesVisibleBtn";
            this.isNotesVisibleBtn.Size = new System.Drawing.Size(101, 32);
            this.isNotesVisibleBtn.Text = "🗒 NOTES";
            this.isNotesVisibleBtn.ToolTipText = "Switches visibility of notes section (right top)";
            this.isNotesVisibleBtn.Click += new System.EventHandler(this.isNotesVisibleBtn_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 35);
            // 
            // isMiscInfoVisibleBtn
            // 
            this.isMiscInfoVisibleBtn.Checked = true;
            this.isMiscInfoVisibleBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isMiscInfoVisibleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isMiscInfoVisibleBtn.Image = ((System.Drawing.Image)(resources.GetObject("isMiscInfoVisibleBtn.Image")));
            this.isMiscInfoVisibleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isMiscInfoVisibleBtn.Name = "isMiscInfoVisibleBtn";
            this.isMiscInfoVisibleBtn.Size = new System.Drawing.Size(132, 32);
            this.isMiscInfoVisibleBtn.Text = "❕ MISC INFO.";
            this.isMiscInfoVisibleBtn.ToolTipText = "Switches visibility of additional base station information as depth, water temper" +
    "ature, pitch and roll (left top)";
            this.isMiscInfoVisibleBtn.Click += new System.EventHandler(this.isMiscInfoVisibleBtn_Click);
            // 
            // remotesViewPanel
            // 
            this.remotesViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remotesViewPanel.Controls.Add(this.remotesTreeView);
            this.remotesViewPanel.Controls.Add(this.remotesViewToolStrip);
            this.remotesViewPanel.Location = new System.Drawing.Point(760, 39);
            this.remotesViewPanel.Margin = new System.Windows.Forms.Padding(1);
            this.remotesViewPanel.Name = "remotesViewPanel";
            this.remotesViewPanel.Size = new System.Drawing.Size(239, 484);
            this.remotesViewPanel.TabIndex = 3;
            // 
            // remotesTreeView
            // 
            this.remotesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remotesTreeView.BackColor = System.Drawing.Color.Black;
            this.remotesTreeView.ForeColor = System.Drawing.Color.Lime;
            this.remotesTreeView.LineColor = System.Drawing.Color.PaleGreen;
            this.remotesTreeView.Location = new System.Drawing.Point(3, 38);
            this.remotesTreeView.Name = "remotesTreeView";
            this.remotesTreeView.Size = new System.Drawing.Size(233, 446);
            this.remotesTreeView.TabIndex = 1;
            // 
            // remotesViewToolStrip
            // 
            this.remotesViewToolStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.remotesViewToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.remotesViewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treeExpandBtn,
            this.treeCollapseBtn,
            this.toolStripLabel1});
            this.remotesViewToolStrip.Location = new System.Drawing.Point(0, 0);
            this.remotesViewToolStrip.Name = "remotesViewToolStrip";
            this.remotesViewToolStrip.Size = new System.Drawing.Size(239, 35);
            this.remotesViewToolStrip.TabIndex = 0;
            this.remotesViewToolStrip.Text = "toolStrip3";
            // 
            // treeExpandBtn
            // 
            this.treeExpandBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.treeExpandBtn.Image = ((System.Drawing.Image)(resources.GetObject("treeExpandBtn.Image")));
            this.treeExpandBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treeExpandBtn.Name = "treeExpandBtn";
            this.treeExpandBtn.Size = new System.Drawing.Size(33, 32);
            this.treeExpandBtn.Text = "▲";
            this.treeExpandBtn.ToolTipText = "Expand tree view";
            this.treeExpandBtn.Click += new System.EventHandler(this.treeExpandBtn_Click);
            // 
            // treeCollapseBtn
            // 
            this.treeCollapseBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.treeCollapseBtn.Image = ((System.Drawing.Image)(resources.GetObject("treeCollapseBtn.Image")));
            this.treeCollapseBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treeCollapseBtn.Name = "treeCollapseBtn";
            this.treeCollapseBtn.Size = new System.Drawing.Size(33, 32);
            this.treeCollapseBtn.Text = "▼";
            this.treeCollapseBtn.ToolTipText = "Collapse tree view";
            this.treeCollapseBtn.Click += new System.EventHandler(this.treeCollapseBtn_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(97, 32);
            this.toolStripLabel1.Text = "REMOTES";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 553);
            this.Controls.Add(this.remotesViewPanel);
            this.Controls.Add(this.plotPanel);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "uBear";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.plotPanel.ResumeLayout(false);
            this.plotPanel.PerformLayout();
            this.plotToolStrip.ResumeLayout(false);
            this.plotToolStrip.PerformLayout();
            this.remotesViewPanel.ResumeLayout(false);
            this.remotesViewPanel.PerformLayout();
            this.remotesViewToolStrip.ResumeLayout(false);
            this.remotesViewToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.Panel plotPanel;
        private System.Windows.Forms.Panel remotesViewPanel;
        private System.Windows.Forms.ToolStrip plotToolStrip;
        private System.Windows.Forms.ToolStrip remotesViewToolStrip;
        private System.Windows.Forms.ToolStripButton connectionBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton infoBtn;
        private System.Windows.Forms.TreeView remotesTreeView;
        private UCNLUI.Controls.RadialPlot radialPlot;
        private System.Windows.Forms.ToolStripDropDownButton colorSchemeBtn;
        private System.Windows.Forms.ToolStripButton settingsBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton logBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton treeExpandBtn;
        private System.Windows.Forms.ToolStripButton treeCollapseBtn;
        private System.Windows.Forms.ToolStripDropDownButton utilsBtn;
        private System.Windows.Forms.ToolStripMenuItem logViewBtn;
        private System.Windows.Forms.ToolStripMenuItem logPlaybackBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem logClearAllBtn;
        private System.Windows.Forms.ToolStripMenuItem colorSchemeDefaultDarkBtn;
        private System.Windows.Forms.ToolStripMenuItem colorSchemeBrightBtn;
        private System.Windows.Forms.ToolStripMenuItem colorSchemeMonogreenBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel uPortStatusLbl;
        private System.Windows.Forms.ToolStripButton isLimboVisibleBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton isHistoryVisibleBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton isMiscInfoVisibleBtn;
        private System.Windows.Forms.ToolStripTextBox noteTxb;
        private System.Windows.Forms.ToolStripButton addNoteBtn;
        private System.Windows.Forms.ToolStripButton isNotesVisibleBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem logClearAllEmptiesBtn;
        private System.Windows.Forms.ToolStripStatusLabel gnssPortStatusLbl;
        private System.Windows.Forms.ToolStripMenuItem trackBtn;
        private System.Windows.Forms.ToolStripMenuItem trackExportBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem trackClearBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem deviceBtn;
        private System.Windows.Forms.ToolStripMenuItem deviceViewInfoBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem utilsLocationOverrideBtn;
    }
}

