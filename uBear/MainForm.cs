using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using uBear.Core;
using uBear.UI;
using UCNLDrivers;
using UCNLUI;
using UCNLUI.Controls;
using UCNLUI.Dialogs;

namespace uBear
{
    public partial class MainForm : Form
    {
        enum ColorScheme
        {
            DefaultDark,
            Bright,
            MonochromeGreen,
            Default
        }

        #region Properties

        string logPath;
        string logFileName;
        string settingsFileName;
        string snapshotsPath;

        TSLogProvider logger;
        SimpleSettingsProviderXML<SettingsContainer> sProvider;

        LogPlayer lPlayer;

        bool isRestart = false;

        uCore core;
        TrackManager tManager;

        static readonly string appicon = "🐻";

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            #region Early init

            string vString = string.Format("{0} {1} v{2} {3}",
                appicon,
                Application.ProductName, 
                Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                MDates.GetReferenceNote());
            this.Text = vString;

            #endregion

            #region Misc.

            lPlayer = new LogPlayer();
            tManager = new TrackManager();
            tManager.IsEmptyChanged += (o, e) => UIHelpers.InvokeSetEnabledState(mainToolStrip, trackBtn, !tManager.IsEmpty);

            #endregion

            #region paths & filenames

            DateTime startTime = DateTime.Now;
            settingsFileName = Path.ChangeExtension(Application.ExecutablePath, "settings");
            logPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LOG");
            logFileName = StrUtils.GetTimeDirTreeFileName(startTime, Application.ExecutablePath, "LOG", "log", true);
            snapshotsPath = StrUtils.GetTimeDirTree(startTime, Application.ExecutablePath, "SNAPSHOTS", false);

            #endregion

            #region logger

            logger = new TSLogProvider(logFileName);
            logger.WriteStart();
            logger.Write(vString);
            logger.TextAddedEvent += (o, e) => InvokeAppendHistoryLine(e.Text);

            #endregion

            #region settings

            sProvider = new SimpleSettingsProviderXML<SettingsContainer>
            {
                isSwallowExceptions = false
            };

            logger.Write(string.Format("Loading settings from {0}", settingsFileName));

            try
            {
                sProvider.Load(settingsFileName);
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            logger.Write("Current application settings:");
            logger.Write(sProvider.Data.ToString());

            #endregion

            #region core

            core = new uCore(sProvider.Data.Remotes.ToHashSet<RemoteAddress>(),
                sProvider.Data.HeadingAdjust_deg,
                sProvider.Data.TransverseOffset_m,
                sProvider.Data.LongitudalOffset_m);

            if (!sProvider.Data.IsAutoSalinity)
                core.Salinity = sProvider.Data.WaterSalinity_PSU;
            core.IsAutoSalinity = sProvider.Data.IsAutoSalinity;

            if (!sProvider.Data.IsAutoSoundSpeed)
                core.SoundSpeed = sProvider.Data.SoundSpeed_mps;
            core.IsAutoSoundSpeed = sProvider.Data.IsAutoSoundSpeed;
            
            core.IsAutoGravity = true;

            core.LogEventHandler += (o, e) => logger.Write(string.Format("{0}: {1}", e.EventType, e.LogString));

            InvokeUpdatePortStatusLbl(mainStatusStrip, uPortStatusLbl, core.IsActive, core.uPortDetected, core.uPortStatus);

            if (sProvider.Data.UseAUXGNSSCompas)
            {
                gnssPortStatusLbl.Visible = true;
                core.AuxGNSSInit(sProvider.Data.AUXGNSSCompasBaudrate);
                InvokeUpdatePortStatusLbl(mainStatusStrip, gnssPortStatusLbl, core.IsActive, core.GNSSPortDetected, core.GNSSPortStatus);
            }

            core.IsActiveChanged += (o, e) =>
            {
                UIHelpers.InvokeSetCheckedState(mainToolStrip, connectionBtn, core.IsActive);
                UIHelpers.InvokeSetEnabledState(mainToolStrip, settingsBtn, !core.IsActive);
                UIHelpers.InvokeSetEnabledState(mainToolStrip, logPlaybackBtn, !core.IsActive);
                InvokeUpdatePortStatusLbl(mainStatusStrip, uPortStatusLbl, core.IsActive, core.uPortDetected, core.uPortStatus);
                logger.Write(string.Format("Core.IsActive={0}", core.IsActive));
            };

            core.uPortDetectedChanged += (o, e) => InvokeUpdatePortStatusLbl(mainStatusStrip, uPortStatusLbl, core.IsActive, core.uPortDetected, core.uPortStatus);
            core.uGNSSPortDetectedChanged += (o, e) => InvokeUpdatePortStatusLbl(mainStatusStrip, gnssPortStatusLbl, core.IsActive, core.GNSSPortDetected, core.GNSSPortStatus);
            core.DeviceInfoValidChanged += (o, e) => UIHelpers.InvokeSetEnabledState(mainToolStrip, deviceBtn, core.DeviceInfoValid);
            core.IsGNSSActiveChanged += (o, e) => InvokeUpdatePortStatusLbl(mainStatusStrip, gnssPortStatusLbl, core.IsActive, core.GNSSPortDetected, core.GNSSPortStatus);       
            core.HeadingUpdated += (o, e) => InvokeSetHeading(core.Heading);
            core.AbsoluteLocationUpdated += (o, e) => tManager.AddPoint(e.ID, e.Latitude_deg, e.Longitude_deg, e.Depth_m, DateTime.Now);
            core.RelativeLocationUpdated += (o, e) => InvokeSetTarget(e.ID, e.PRange_m, e.Azimuth_deg, e.IsTimeout);
            core.StateUpdateHandler += (o, e) =>
            {
                InvokeSetLeftTopText(core.SystemDescriptionGet());
                InvokeSynchRemotes(core.RemoteDescriptorsGet());
            };

            core.LocationOverrideEnabledChanged += (o, e) => 
            {
                UIHelpers.InvokeSetCheckedState(mainToolStrip, utilsLocationOverrideBtn, core.LocationOverrideEnabled);

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("core.LocationOverrideEnabled = {0}", core.LocationOverrideEnabled);

                if (core.LocationOverrideEnabled)
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        " (Lat={0:F06}°, Lon={1:F06}, Hdn={2:F01}°)",
                        core.LatitudeOverride,
                        core.LongitudeOverride,
                        core.HeadingOverride);
                }

                InvokeAppendHistoryLine(sb.ToString());
            };

            #endregion            
        }

        #endregion

        #region Methods

        #region Custom UI invokers

        private void InvokeAppendHistoryLine(string line)
        {
            if (radialPlot.InvokeRequired)
                radialPlot.Invoke((MethodInvoker)delegate
                {
                    radialPlot.AppendHistoryLine(line);
                    radialPlot.Invalidate();
                });
            else
            {
                radialPlot.AppendHistoryLine(line);
                radialPlot.Invalidate();
            }
        }

        private void InvokeSetNoteLine(string line)
        {
            if (radialPlot.InvokeRequired)
                radialPlot.Invoke((MethodInvoker)delegate { radialPlot.RightUpperTextSet(line); radialPlot.Invalidate(); });
            else
            {
                radialPlot.RightUpperTextSet(line);
                radialPlot.Invalidate();
            }
        }

        private void InvokeSetLeftTopText(string line)
        {
            if (radialPlot.InvokeRequired)
                radialPlot.Invoke((MethodInvoker)delegate { radialPlot.LeftUpperText = line; radialPlot.Invalidate(); });
            else
            {
                radialPlot.LeftUpperText = line;
                radialPlot.Invalidate();
            }
        }

        private void InvokeSetHeading(double heading)
        {
            if (radialPlot.InvokeRequired)
                radialPlot.Invoke((MethodInvoker)delegate 
                { 
                    radialPlot.Heading = heading; 
                    radialPlot.Invalidate(); 
                });
            else
            {
                radialPlot.Heading = heading;
                radialPlot.Invalidate();
            }
        }

        private void InvokeSynchRemotes(Dictionary<string, Dictionary<string, string>> rDescriptors)
        {
            if (remotesTreeView.InvokeRequired)
                remotesTreeView.Invoke((MethodInvoker)delegate { SynchRemotes(rDescriptors); });
            else
                SynchRemotes(rDescriptors);
        }

        private void SynchRemotes(Dictionary<string, Dictionary<string, string>> rDescriptors)
        {
            bool isNeedExpandOnFirst = (remotesTreeView.Nodes.Count == 0) ||
                ((remotesTreeView.Nodes.Count > 0) && (remotesTreeView.Nodes[0].Nodes.Count == 0));

            foreach (var remote in rDescriptors)
            {
                string rKey = remote.Key;
                TreeNode bNode;

                if (!remotesTreeView.Nodes.ContainsKey(rKey))
                    bNode = remotesTreeView.Nodes.Add(rKey, rKey);
                else
                    bNode = remotesTreeView.Nodes[rKey];

                for (int i = bNode.Nodes.Count - 1; i >= 0; i--)
                {
                    if (!remote.Value.ContainsKey(bNode.Nodes[i].Name))
                        bNode.Nodes.RemoveByKey(bNode.Nodes[i].Name);
                }

                foreach (var rItem in remote.Value)
                {
                    if (!bNode.Nodes.ContainsKey(rItem.Key))
                        bNode.Nodes.Add(rItem.Key, string.Format("{0}: {1}", rItem.Key, rItem.Value));
                    else
                        bNode.Nodes[rItem.Key].Text = string.Format("{0}: {1}", rItem.Key, rItem.Value);
                }
            }

            if (isNeedExpandOnFirst)
                remotesTreeView.ExpandAll();

            remotesTreeView.Invalidate();
        }

        private void InvokeSetTarget(string ID, double range_m, double azimuth_deg, bool isTimeout)
        {
            if (radialPlot.InvokeRequired)
                radialPlot.Invoke((MethodInvoker)delegate { radialPlot.SetTarget(ID, range_m, azimuth_deg, isTimeout); });
            else
                radialPlot.SetTarget(ID, range_m, azimuth_deg, isTimeout);
        }

        #endregion

        #region Utils

        private void InvokeUpdatePortStatusLbl(StatusStrip strip, ToolStripStatusLabel lbl, bool active, bool detected, string text)
        {
            Color backColor = Color.FromKnownColor(KnownColor.Control);
            Color foreColor = Color.FromKnownColor(KnownColor.ControlText);

            if (active)
            {
                foreColor = Color.Yellow;
                if (!detected)
                {
                    backColor = Color.Red;
                }
                else
                {
                    backColor = Color.Green;
                }
            }

            UIHelpers.InvokeSetText(strip, lbl, text, foreColor, backColor);
        }

        private void ProcessException(Exception ex, bool isMsgBox)
        {
            logger.Write(ex);

            if (isMsgBox)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ApplyColorScheme(ColorScheme cSchemeName)
        {
            if (cSchemeName == ColorScheme.Bright)
            {
                radialPlot.LeftUpperTextColor = Color.DarkGreen;
                radialPlot.LeftUpperTextBackgoundColor = Color.Wheat;

                radialPlot.RightUpperTextColor = Color.DarkGreen;
                radialPlot.RightUpperTextBackgoundColor = Color.Wheat;

                radialPlot.BackColor = Color.Snow;
                radialPlot.AxisColor = Color.Black;

                radialPlot.AxisLabelColor = Color.DarkBlue;
                radialPlot.AxisBackgroundLabelColor = Color.Snow;

                radialPlot.LimboColor = Color.DarkGray;
                radialPlot.LimboTickColor = Color.DarkBlue;

                radialPlot.BoatColor = Color.Green;

                radialPlot.HistoryTextColor = Color.Brown;

                radialPlot.TargetCircleColor = Color.Black;
                radialPlot.TargetDistLineColor = Color.Black;
                radialPlot.TargetLabelColor = Color.Black;
                radialPlot.TargetLabelBackgroundColor = Color.White;

                remotesTreeView.ForeColor = radialPlot.LeftUpperTextColor;
                remotesTreeView.BackColor = radialPlot.BackColor;
                remotesTreeView.LineColor = radialPlot.AxisColor;
            }
            else if (cSchemeName == ColorScheme.MonochromeGreen)
            {
                radialPlot.LeftUpperTextColor = Color.Lime;
                radialPlot.LeftUpperTextBackgoundColor = Color.Black;

                radialPlot.RightUpperTextColor = Color.Lime;
                radialPlot.RightUpperTextBackgoundColor = Color.Black;

                radialPlot.BackColor = Color.Black;
                radialPlot.AxisColor = Color.Lime;

                radialPlot.AxisLabelColor = Color.Lime;
                radialPlot.AxisBackgroundLabelColor = Color.Black;

                radialPlot.LimboColor = Color.Green;
                radialPlot.LimboTickColor = Color.Lime;

                radialPlot.BoatColor = Color.Lime;

                radialPlot.HistoryTextColor = Color.Lime;

                radialPlot.TargetCircleColor = Color.Green;
                radialPlot.TargetDistLineColor = Color.LightGreen;
                radialPlot.TargetLabelColor = Color.Lime;
                radialPlot.TargetLabelBackgroundColor = Color.Black;

                radialPlot.Invalidate();

                remotesTreeView.ForeColor = radialPlot.LeftUpperTextColor;
                remotesTreeView.BackColor = radialPlot.BackColor;
                remotesTreeView.LineColor = radialPlot.AxisColor;

            }
            else
            {
                radialPlot.LeftUpperTextColor = RadialPlot.DefaultLeftUpperTextColor;
                radialPlot.LeftUpperTextBackgoundColor = RadialPlot.DefaultLeftUpperTextBackGroundColor;

                radialPlot.RightUpperTextColor = RadialPlot.DefaultRightUpperTextColor;
                radialPlot.RightUpperTextBackgoundColor = RadialPlot.DefaultRightUpperTextBackGroundColor;

                radialPlot.BackColor = RadialPlot.DefaultBackgoundColor;
                radialPlot.AxisColor = RadialPlot.DefaultAxisColor;

                radialPlot.AxisLabelColor = RadialPlot.DefaultAxisLabelColor;
                radialPlot.AxisBackgroundLabelColor = RadialPlot.DefaultAxisBackgroundLabelColor;

                radialPlot.LimboColor = RadialPlot.DefaultLimboColor;
                radialPlot.LimboTickColor = RadialPlot.DefaultLimboTickColor;

                radialPlot.BoatColor = RadialPlot.DefaultBoatColor;

                radialPlot.HistoryTextColor = RadialPlot.DefaultHistoryTextColor;

                radialPlot.TargetCircleColor = RadialPlot.DefaultTargetCircleColor;
                radialPlot.TargetDistLineColor = RadialPlot.DefaultTargetDistLineColor;
                radialPlot.TargetLabelColor = RadialPlot.DefaultTargetLabelColor;
                radialPlot.TargetLabelBackgroundColor = RadialPlot.DefaultTargetLabelBackgoundColor;

                remotesTreeView.ForeColor = radialPlot.LeftUpperTextColor;
                remotesTreeView.BackColor = radialPlot.BackColor;
                remotesTreeView.LineColor = radialPlot.AxisColor;
            }

            radialPlot.Invalidate();
        }

        #endregion

        #endregion

        #region Handlers

        #region UI

        #region mainToolStrip

        private void connectionBtn_Click(object sender, EventArgs e)
        {
            if (core.IsActive)
                core.Stop();
            else
                core.Start();
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            bool isSaved = false;

            using (SettingsEditor sDialog = new SettingsEditor())
            {
                sDialog.Text = string.Format("{0} {1} - Settings", appicon, Application.ProductName);
                sDialog.Value = sProvider.Data;

                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    sProvider.Data = sDialog.Value;

                    try
                    {
                        sProvider.Save(settingsFileName);
                        isSaved = true;
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }
            }

            if (isSaved &&
                MessageBox.Show("Settings has been updated. Restart application to apply new settings?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                isRestart = true;
                Application.Restart();
            }
        }

        #region LOG

        private void logViewBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(logFileName);
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        private void logPlaybackBtn_Click(object sender, EventArgs e)
        {
            if (lPlayer.IsRunning)
            {
                if (MessageBox.Show("Log is currently playing, do you want to abort it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    lPlayer.RequestToStop();
            }
            else
            {
                using (OpenFileDialog oDialog = new OpenFileDialog())
                {
                    oDialog.Title = "Select a log file to playback...";
                    oDialog.DefaultExt = "log";
                    oDialog.Filter = "Log files (*.log)|*.log";

                    if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        lPlayer.NewLogLineHandler += (o, ee) =>
                            {
                                if (ee.Line.StartsWith("INFO"))
                                {
                                    int idx = ee.Line.IndexOf(' ');
                                    if (idx >= 0)
                                    {
                                        core.Emulate(ee.Line.Substring(idx));
                                    }
                                }
                                else if (ee.Line.StartsWith("NOTE"))
                                {
                                    var match = Regex.Match(ee.Line, "\"[^\" ][^\"]*\"");
                                    if (match.Success)
                                        InvokeSetNoteLine(match.ToString().Trim('"'));
                                }
                            };
                        lPlayer.LogPlaybackFinishedHandler += (o, ee) =>
                            {
                                core.Stop();

                                this.Invoke((MethodInvoker)delegate
                                {
                                    settingsBtn.Enabled = true;
                                    connectionBtn.Enabled = true;
                                    logPlaybackBtn.Text = "▶ Playback...";
                                    MessageBox.Show(string.Format("Log file \"{0}\" playback is finished", oDialog.FileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                });
                            };

                        lPlayer.Playback(oDialog.FileName);

                        logPlaybackBtn.Text = "⏹ Stop playback";
                        settingsBtn.Enabled = false;
                        connectionBtn.Enabled = false;
                    }
                }
            }
        }

        private void logClearAllEmptiesBtn_Click(object sender, EventArgs e)
        {
            string logRootPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LOG");
            var dirs = Directory.GetDirectories(logRootPath);
            int fNum = 0;
            foreach (var item in dirs)
            {
                var fNames = Directory.GetFiles(item);

                foreach (var fName in fNames)
                {
                    FileInfo fInfo = new FileInfo(fName);
                    if (fInfo.Length <= 2048)
                    {
                        try
                        {
                            File.Delete(fName);
                            fNum++;
                        }
                        catch (Exception ex)
                        {
                            ProcessException(ex, true);
                        }
                    }
                }

                fNames = Directory.GetFiles(item);
                if (fNames.Length == 0)
                {
                    try
                    {
                        Directory.Delete(item);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }
            }

            MessageBox.Show(string.Format("{0} File(s) was/were deleted.", fNum),
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void logClearAllBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete all log entries? (Action cannot be undone!)",
                                "Warning!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                string logRootPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LOG");
                var dirs = Directory.GetDirectories(logRootPath);
                int dirNum = 0;
                foreach (var item in dirs)
                {
                    try
                    {
                        Directory.Delete(item, true);
                        dirNum++;
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }

                MessageBox.Show(string.Format("{0} Entries was/were deleted.", dirNum),
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        #endregion

        #region UTILS

        private void trackClearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to clear all tracks obtained during the current session?",
                "Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tManager.Clear();
            }
        }

        private void trackExportBtn_Click(object sender, EventArgs e)
        {
            bool saved = false;
            using (SaveFileDialog sDialog = new SaveFileDialog())
            {
                sDialog.Title = "Export tracks to...";
                sDialog.Filter = "KML (*.kml)|*.kml|CSV (*.csv)|*.csv";
                sDialog.FileName = StrUtils.GetHMSString();

                if (sDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // KML
                        if (sDialog.FilterIndex == 1)
                        {
                            tManager.ExportToKML(sDialog.FileName);
                            saved = true;
                        }
                        // CSV
                        else if (sDialog.FilterIndex == 2)
                        {
                            tManager.ExportToCSV(sDialog.FileName);
                            saved = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }
            }

            if (saved &&
                (MessageBox.Show("Tracks are saved, clear all tracks data?",
                "Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes))
                tManager.Clear();
        }

        private void deviceViewInfoBtn_Click(object sender, EventArgs e)
        {
            using (InfoViewDialog iDialog = new InfoViewDialog())
            {
                iDialog.Text = "Device information";
                iDialog.InfoText = core.DeviceInfo;
                iDialog.ShowDialog();
            }
        }

        //!!!
        //
        private void overrideLocation_Click(object sender, EventArgs e)
        {            
            if (core.LocationOverrideEnabled)
            {
                core.LocationOverrideDisable();
            }
            else
            {
                using (LocationOverrideDialog lDialog = new LocationOverrideDialog())
                {
                    lDialog.Latitude_deg = core.LatitudeOverride;
                    lDialog.Longitude_deg = core.LongitudeOverride;
                    lDialog.Heading_deg = core.HeadingOverride;

                    if (lDialog.ShowDialog() == DialogResult.OK)
                    {
                        core.LocationOverrideEnable(lDialog.Latitude_deg, lDialog.Longitude_deg, lDialog.Heading_deg);
                    }
                }
            }
        }

        #endregion

        private void noteTxb_TextChanged(object sender, EventArgs e)
        {
            addNoteBtn.Enabled = !string.IsNullOrWhiteSpace(noteTxb.Text);
        }

        private void noteTxb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addNoteBtn_Click(null, null);
        }

        private void addNoteBtn_Click(object sender, EventArgs e)
        {
            logger.Write(string.Format("NOTE: \"{0}\"", noteTxb.Text));
            radialPlot.RightUpperTextSet(noteTxb.Text);
            noteTxb.Clear();
        }


        private void infoBtn_Click(object sender, EventArgs e)
        {
            using (AboutBox aDialog = new AboutBox())
            {
                aDialog.ApplyAssembly(Assembly.GetExecutingAssembly());

#if IS_DIVENET
                aDialog.Weblink = "www.divenetgps.com";                
#else
                aDialog.Weblink = "www.unavlab.com";
#endif

                aDialog.ShowDialog();
            }
        }

        #endregion

        #region plotToolStrip
        private void isLimboVisibleBtn_Click(object sender, EventArgs e)
        {
            radialPlot.LimboVisible = !radialPlot.LimboVisible;
            isLimboVisibleBtn.Checked = radialPlot.LimboVisible;
            radialPlot.Invalidate();
        }

        private void isHistoryVisibleBtn_Click(object sender, EventArgs e)
        {
            radialPlot.HistoryVisible = !radialPlot.HistoryVisible;
            isHistoryVisibleBtn.Checked = radialPlot.HistoryVisible;
            radialPlot.Invalidate();
        }

        private void isNotesVisibleBtn_Click(object sender, EventArgs e)
        {
            radialPlot.RightUpperTextVisible = !radialPlot.RightUpperTextVisible;
            isNotesVisibleBtn.Checked = radialPlot.RightUpperTextVisible;
            radialPlot.Invalidate();
        }

        private void isMiscInfoVisibleBtn_Click(object sender, EventArgs e)
        {
            radialPlot.LeftUpperTextVisible = !radialPlot.LeftUpperTextVisible;
            isMiscInfoVisibleBtn.Checked = radialPlot.LeftUpperTextVisible;
            radialPlot.Invalidate();
        }

        private void colorSchemeSubItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem currentItem = sender as ToolStripMenuItem;
            
            if (currentItem != null)
            {
                ((ToolStripDropDownButton)currentItem.OwnerItem).DropDownItems
                    .OfType<ToolStripMenuItem>().ToList()
                    .ForEach(item =>
                    {
                        item.Checked = false;
                    });

                ColorScheme cSchemeID = ColorScheme.Default;

                if (Enum.TryParse<ColorScheme>(currentItem.Text, out cSchemeID))
                {
                    ApplyColorScheme(cSchemeID);
                    currentItem.Checked = true;
                }
                else
                {
                    colorSchemeDefaultDarkBtn.Checked = true;
                }
            }
        }

        #endregion

        #region remotesToolStrip
        private void treeExpandBtn_Click(object sender, EventArgs e)
        {
            remotesTreeView.ExpandAll();
        }

        private void treeCollapseBtn_Click(object sender, EventArgs e)
        {
            remotesTreeView.CollapseAll();
        }

        #endregion

        #region mainForm

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            core.Stop();
            core.Dispose();

            logger.FinishLog();
            logger.Flush();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isRestart && (MessageBox.Show(string.Format("Close {0}?", Application.ProductName),
                                                      "Question",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!noteTxb.Focused)
                noteTxb.Focus();
        }

        #endregion

        #endregion

        #endregion
    }
}
