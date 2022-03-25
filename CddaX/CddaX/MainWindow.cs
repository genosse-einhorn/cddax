using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CddaX.CddaLib;
using System.Threading;
using CddaX.Util;
using System.Diagnostics;
using System.IO;
using CddaX.Log;
using CddaX.MetaStore;

namespace CddaX
{
    public partial class MainWindow : Form
    {
        private string[] m_drives = null;
        private string m_currentDrive = null;
        private DiscMeta m_currentMeta = null;

        public MainWindow()
        {
            InitializeComponent();

            FormHelper.SetToolstripIcon(tsbRefresh, CddaX.Properties.Resources.RefreshIcon);
            FormHelper.SetToolstripIcon(tsmiRefresh, CddaX.Properties.Resources.RefreshIcon);
            FormHelper.SetToolstripIcon(tsbLoadMetadata, CddaX.Properties.Resources.MusicBrainzIcon);
            FormHelper.SetToolstripIcon(tsmiDownloadMetadata, CddaX.Properties.Resources.MusicBrainzIcon);
            FormHelper.SetToolstripIcon(tsbRip, CddaX.Properties.Resources.SaveIcon);
            FormHelper.SetToolstripIcon(tsmiRip, CddaX.Properties.Resources.SaveIcon);
            FormHelper.SetToolstripIcon(tsmiQuit, CddaX.Properties.Resources.LeaveIcon);
            FormHelper.SetToolstripIcon(tsmiAbout, CddaX.Properties.Resources.HelpIcon);
            FormHelper.SetToolstripIcon(tsmiResetAllSettings, CddaX.Properties.Resources.ClearIcon);
            FormHelper.SetToolstripIcon(tsmiWebsite, CddaX.Properties.Resources.WebIcon);
            FormHelper.SetToolstripIcon(tsmiShowOnMusicBrainz, CddaX.Properties.Resources.WebIcon);

            FormHelper.MakeToolStripBold(tsbRip);
            FormHelper.MakeToolStripBold(tsmiRip);

            if (OSHelper.IsWin10OrLater)
            {
                // HACK: make menu bar less ugly on Win10+
                msMainMenu.BackColor = SystemColors.Window;
            }

            FormHelper.ActivateSegoeUi(this);

            m_drives = ScsiHandle.CdDriveList();

            // re-select previously selected drive
            int selectedDriveIndex = 0;
            string selectedDriveStr = registrySettings.LoadString("CdDrive", "");
            for (int i = 0; i < m_drives.Length; ++i)
            {
                if (m_drives[i].Equals(selectedDriveStr, StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedDriveIndex = i;
                    break;
                }
            }

            foreach (string drive in m_drives)
            {
                cbDriveSelection.Items.Add(drive);
            }
            if (cbDriveSelection.Items.Count > 0)
            {
                cbDriveSelection.SelectedIndex = selectedDriveIndex;
            }

            for (int i = 0; i < m_drives.Length; ++i)
            {
                var driveItem = new CdDriveToolStripMenuItem(m_drives[i]);
                driveItem.Click += HandleDriveMenuItemClick;
                driveItem.Checked = i == selectedDriveIndex;
                tsmiCD.DropDownItems.Insert(i, driveItem);
            }

            FormHelper.AutoPadAllLabelsToEdits(pDiscMeta);

            // HACK! album art picture box sizing
            int wh = tbDiscNo.Bottom - tbDiscTitle.Top;
            pbCoverArt.Width = wh;
            pbCoverArt.Height = wh;

            int pad = (this.Font.Height + 10) / 11;
            dgvToc.DefaultCellStyle.Padding = new Padding(0, pad, 0, pad);

            UpdateUiStuff();
        }

        void HandleDriveMenuItemClick(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;

            HandleCdDriveSelected(item.Text);
        }

        void HandleCdDriveSelected(string drive)
        {
            if (m_currentDrive == drive)
                return;

            m_currentDrive = drive;
            cbDriveSelection.SelectedItem = drive;

            registrySettings.SaveString("CdDrive", m_currentDrive);

            foreach (var item in tsmiCD.DropDownItems)
            {
                var tsmi = item as CdDriveToolStripMenuItem;
                if (tsmi != null)
                {
                    tsmi.Checked = drive == tsmi.Drive;
                }
            }

            if (this.Visible)
            {
                ClearTocData();
                IdleRunner.DelayUntilIdle(RefreshToc);
            }
        }



        private void bRefresh_Click(object sender, EventArgs e)
        {
            RefreshToc();
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            RefreshToc();
        }

        private void ClearTocData()
        {
            m_currentMeta = null;
            bsToc.DataSource = typeof(TrackMeta);
            bsDiscMeta.DataSource = typeof(DiscMeta);
        }

        private void UpdateUiStuff()
        {
            bool haveToc = m_currentMeta != null && m_currentMeta.Tracks.Length > 0;
            bool haveCdDrive = m_drives.Length > 0;

            cbDriveSelection.Enabled = haveCdDrive;
            tsbRefresh.Enabled = haveCdDrive;
            tsmiRefresh.Enabled = haveCdDrive;
            tsbLoadMetadata.Enabled = haveToc;
            tsmiDownloadMetadata.Enabled = haveToc;
            tsmiShowOnMusicBrainz.Enabled = haveToc;
            tsbRip.Enabled = haveToc;
            tsmiRip.Enabled = haveToc;
            tbDiscTitle.Enabled = haveToc;
            tbDiscArtist.Enabled = haveToc;
            tbDiscComposer.Enabled = haveToc;
            tbDiscYear.Enabled = haveToc;
            tbDiscNo.Enabled = haveToc;

            if (!haveCdDrive)
            {
                tsslStatusText.Text = CddaX.Properties.Resources.NoCdDriveFoundStatusLabel;
            }
            else if (!haveToc)
            {
                tsslStatusText.Text = CddaX.Properties.Resources.NoCdInsertedStatusLabel;
            }
            else
            {
                tsslStatusText.Text = string.Format(CddaX.Properties.Resources.ReadyStatusLabel, m_currentMeta.Tracks.Length);
            }
        }

        private void RefreshToc()
        {
            if (m_currentDrive == null)
            {
                MessageBox.Show(this, Properties.Resources.NoCdDriveFoundStatusLabel, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            tsslStatusText.Text = CddaX.Properties.Resources.LoadingTocStatusLabel;
            ssStatusBar.Update();

            try
            {
                using (new CursorChanger(Cursors.WaitCursor))
                using (IScsiHandle h = ScsiHandle.Create(m_currentDrive))
                {
                    Toc t = CddaOperations.ReadToc(h);
                    if (m_currentMeta == null || t != m_currentMeta.Toc)
                    {
                        Logger.Info("Found new CD with {0} tracks", t.LastTrackNo - t.FirstTrackNo + 1);

                        ClearTocData();
                        m_currentMeta = new DiscMeta(t);
                        m_currentMeta.Mcn = CddaOperations.ReadMcnNofail(h);

                        foreach (TrackMeta m in m_currentMeta.Tracks)
                        {
                            if (m.TrackNo > 0 && m.TrackNo < 100)
                            {
                                m.Isrc = CddaOperations.ReadIsrcNofail(h, m.TrackNo);
                            }
                        }

                        CdTextData cdtext = CddaOperations.ReadCdTextNofail(h);
                        m_currentMeta.MergeCdText(cdtext);

                        bsDiscMeta.DataSource = new DiscMeta[1] { m_currentMeta };
                        bsToc.DataSource = m_currentMeta.Tracks;
                    }
                }
            }
            catch (Win32Exception x)
            {
                MessageBox.Show(this, x.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Logger.Exception(x, "Reading CD TOC");
            }
            catch (ScsiException x)
            {
                if (x.IsEmptyDrive)
                {
                    ClearTocData();
                }
                else
                {
                    MessageBox.Show(this, x.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                
                Logger.Exception(x, "Reading CD TOC");
            }

            UpdateUiStuff();
        }

        private void dgwToc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvToc.Columns[e.ColumnIndex].DataPropertyName == "Artist")
            {
                if (!dgvToc.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
                {
                    string artist = e.Value as string;
                    string albumArtist = m_currentMeta != null ? m_currentMeta.Artist : null;
                    if (string.IsNullOrEmpty(artist))
                    {
                        if (string.IsNullOrEmpty(albumArtist))
                            e.Value = string.Format("({0})", CddaX.Properties.Resources.UnknownArtist);
                        else
                            e.Value = albumArtist;

                        e.CellStyle.ForeColor = SystemColors.GrayText;
                    }
                }
            }

            if (dgvToc.Columns[e.ColumnIndex].DataPropertyName == "Title")
            {
                if (!dgvToc.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
                {
                    string title = e.Value as string;
                    if (string.IsNullOrEmpty(title))
                    {
                        e.Value = string.Format(CddaX.Properties.Resources.TrackNoMessage, dgvToc.Rows[e.RowIndex].Cells[dgvtbcTrackNo.Index].Value);
                        e.CellStyle.ForeColor = SystemColors.GrayText;
                    }
                }
            }

            if (dgvToc.Columns[e.ColumnIndex].DataPropertyName == "Composer")
            {
                if (!dgvToc.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
                {
                    string composer = e.Value as string;
                    string albumComposer = m_currentMeta != null ? m_currentMeta.Composer : null;
                    if (string.IsNullOrEmpty(composer))
                    {
                        if (string.IsNullOrEmpty(albumComposer))
                            e.Value = string.Format("({0})", CddaX.Properties.Resources.UnknownComposer);
                        else
                            e.Value = albumComposer;

                        e.CellStyle.ForeColor = SystemColors.GrayText;
                    }
                }
            }
        }

        private void cbDriveSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleCdDriveSelected((string)cbDriveSelection.SelectedItem);
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            IdleRunner.DelayUntilIdle(RefreshToc);
        }

        private void tsmiQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiShowOnMusicBrainz_Click(object sender, EventArgs e)
        {
            if (m_currentMeta != null && m_currentMeta.Toc != null)
            {
                OSHelper.LaunchUrl(this, MusicBrainz.BrowserUrl.ShowCdToc(m_currentMeta.Toc));
            }
        }

        private void tsbLoadMetadata_Click(object sender, EventArgs e)
        {
            LoadMetadata();
        }

        private void LoadMetadata()
        {
            if (m_currentMeta == null)
                return;

            this.Validate();

            if (!registrySettings.LoadBool("HideMbPrivacyDialog", false))
            {
                using (var d = new MbPrivacyNoticeDialog())
                {
                    if (d.ShowDialog(this) != DialogResult.OK)
                        return;

                    registrySettings.SaveBool("HideMbPrivacyDialog", d.NeverAskAgain);
                }
            }

            MusicBrainz.Release[] releases;

            tsslStatusText.Text = CddaX.Properties.Resources.LoadingMetadataStatusLabel;
            ssStatusBar.Update();
            try
            {
                using (MusicBrainz.ApiClient client = new MusicBrainz.ApiClient())
                {

                    try
                    {
                        using (new CursorChanger(Cursors.WaitCursor))
                        {
                            //Thread.Sleep(2000); // DEBUG!
                            releases = client.ReleasesForToc(m_currentMeta.Toc);
                        }
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(this, x.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        Logger.Exception(x, "Searching releases on MusicBrainz");
                        return;
                    }

                    if (releases == null || releases.Length == 0)
                    {
                        using (var d = new MbReleaseNotFoundDialog(m_currentMeta.Toc))
                        {
                            d.ShowDialog(this);
                            return;
                        }
                    }

                    var dialog = new MbReleaseSelectDialog(releases, m_currentMeta.Toc, m_currentMeta.Mcn);

                    if (dialog.ShowDialog(this) == DialogResult.OK && dialog.SelectedRelease != null)
                    {
                        try
                        {
                            using (new CursorChanger(Cursors.WaitCursor))
                            {
                                //Thread.Sleep(2000); // DEBUG!

                                MusicBrainz.Release fr = client.FullReleaseById(dialog.SelectedRelease.Id);
                                m_currentMeta.MergeMusicBrainz(fr);

                            }
                        }
                        catch (Exception x)
                        {
                            MessageBox.Show(this, x.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            Logger.Exception(x, "Loading release {0}", dialog.SelectedRelease.Id);
                            return;
                        }

                        try
                        {
                            using (new CursorChanger(Cursors.WaitCursor))
                            {
                                byte[] coverart = client.CoverArtById(dialog.SelectedRelease.Id);
                                m_currentMeta.CoverBytes = coverart;
                            }
                        }
                        catch (Exception x)
                        {
                            // fail silently
                            Logger.Exception(x, "Loading cover art for {0}", dialog.SelectedRelease.Id);
                            m_currentMeta.CoverBytes = null;
                        }

                        bsDiscMeta.ResetBindings(false);
                        bsToc.ResetBindings(false);
                    }
                }
            }
            finally
            {
                UpdateUiStuff();
            }
        }

        private void tsmiEjectCd_Click(object sender, EventArgs e)
        {
            try
            {
                using (new CursorChanger(Cursors.WaitCursor))
                using (IScsiHandle h = ScsiHandle.Create(m_currentDrive))
                {
                    h.EjectMedia();
                }
            }
            catch (Exception x)
            {
                Logger.Exception(x, "Ejecting CD");
                MessageBox.Show(this, x.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void tsmiCloseTray_Click(object sender, EventArgs e)
        {
            try
            {
                using (new CursorChanger(Cursors.WaitCursor))
                using (IScsiHandle h = ScsiHandle.Create(m_currentDrive))
                {
                    h.LoadMedia();
                }
            }
            catch (Exception x)
            {
                Logger.Exception(x, "Closing Tray");
                MessageBox.Show(this, x.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void tsmiErrorLog_Click(object sender, EventArgs e)
        {
            using (LogViewDialog d = new LogViewDialog())
            {
                d.ShowDialog(this);
            }
        }

        private void tbDiscArtist_Validated(object sender, EventArgs e)
        {
            dgvToc.InvalidateColumn(dgvtbcArtist.Index);
        }

        private void tbDiscComposer_Validated(object sender, EventArgs e)
        {
            dgvToc.InvalidateColumn(dgvtbcComposer.Index);
        }

        private void Rip()
        {
            this.Validate();

            Ripper.RipParameters p = new Ripper.RipParameters(m_currentDrive, m_currentMeta);

            p.LoadFromRegistry(registrySettings);

            using (var d = new RipParametersDialog(p))
            {
                if (d.ShowDialog(this) != DialogResult.OK)
                    return;
            }

            p.SaveToRegistry(registrySettings);
            
            using (var w = new Ripper.RipWorker())
            using (var d = new RipProgressDialog(w, p))
            {
                d.ShowDialog(this);
            }
        }

        private void tsbRip_Click(object sender, EventArgs e)
        {
            Rip();
        }

        private void tsmiRip_Click(object sender, EventArgs e)
        {
            Rip();
        }

        private void tsmiDownloadMetadata_Click(object sender, EventArgs e)
        {
            LoadMetadata();
        }

        private void tsmiMbPrivacy_Click(object sender, EventArgs e)
        {
            OSHelper.LaunchUrl(this, MusicBrainz.BrowserUrl.MbPrivacyPolicy);
        }

        private void tsmiIaPrivacy_Click(object sender, EventArgs e)
        {
            OSHelper.LaunchUrl(this, MusicBrainz.BrowserUrl.CoverArtArchivePrivacyPolicy);
        }

        private void tsmiResetAllSettings_Click(object sender, EventArgs e)
        {
            registrySettings.RemoveAllSettings();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            using (var d = new AboutDialog())
            {
                d.ShowDialog();
            }
        }

        protected override void WndProc(ref Message m)
        {
            mediaChangeNotificationHelper.ProcessMessage(ref m);

            base.WndProc(ref m);
        }

        private void mediaChangeNotificationHelper_MediumInserted(object sender, MediaChangeNotificationEventArgs e)
        {
            if (e.Drive == m_currentDrive)
            {
                RefreshToc();
            }
        }

        private void mediaChangeNotificationHelper_MediumRemoved(object sender, MediaChangeNotificationEventArgs e)
        {
            if (e.Drive == m_currentDrive)
            {
                ClearTocData();
                UpdateUiStuff();
            }
        }

        private void dgvToc_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvcbcRip.Index)
            {
                bool foundUnselected = false;
                for (int i = 0; i < dgvToc.Rows.Count; ++i)
                {
                    if (!(bool)dgvToc.Rows[i].Cells[dgvcbcRip.Index].Value)
                    {
                        foundUnselected = true;
                        dgvToc.Rows[i].Cells[dgvcbcRip.Index].Value = true;
                    }
                }

                if (!foundUnselected)
                {
                    // if every row was selected previously, unselect them all
                    for (int i = 0; i < dgvToc.Rows.Count; ++i)
                    {
                        dgvToc.Rows[i].Cells[dgvcbcRip.Index].Value = false;
                    }
                }
            }
        }

        private void tsmiWebsite_Click(object sender, EventArgs e)
        {
            OSHelper.LaunchUrl(this, "https://cddax.genosse-einhorn.de/");
        }
    }

    class CdDriveToolStripMenuItem : ToolStripMenuItem
    {
        public string Drive { get; set; }

        public CdDriveToolStripMenuItem(string drive) : base(drive)
        {
            this.Drive = drive;
        }
    }
}
