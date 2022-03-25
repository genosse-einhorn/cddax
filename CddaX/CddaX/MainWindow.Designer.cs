namespace CddaX
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiCD = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.metadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDownloadMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowOnMusicBrainz = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMbPrivacy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIaPrivacy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEjectCd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseTray = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiResetAllSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiErrorLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ssStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsslStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbDriveSelection = new System.Windows.Forms.ToolStripComboBox();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadMetadata = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRip = new System.Windows.Forms.ToolStripButton();
            this.dgvToc = new System.Windows.Forms.DataGridView();
            this.dgvcbcRip = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvtbcTrackNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbcTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbcArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbcComposer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbcLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsToc = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.tbDiscNo = new System.Windows.Forms.TextBox();
            this.bsDiscMeta = new System.Windows.Forms.BindingSource(this.components);
            this.tbDiscYear = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDiscComposer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDiscArtist = new System.Windows.Forms.TextBox();
            this.tbDiscTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbCoverArt = new System.Windows.Forms.PictureBox();
            this.pDiscMeta = new System.Windows.Forms.TableLayoutPanel();
            this.registrySettings = new CddaX.Util.RegistrySettings();
            this.mediaChangeNotificationHelper = new CddaX.Util.MediaChangeNotificationHelper();
            this.msMainMenu.SuspendLayout();
            this.ssStatusBar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsToc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDiscMeta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoverArt)).BeginInit();
            this.pDiscMeta.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainMenu
            // 
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCD,
            this.metadataToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.msMainMenu, "msMainMenu");
            this.msMainMenu.Name = "msMainMenu";
            // 
            // tsmiCD
            // 
            this.tsmiCD.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.tsmiRefresh,
            this.tsmiRip,
            this.toolStripMenuItem2,
            this.tsmiQuit});
            this.tsmiCD.Name = "tsmiCD";
            resources.ApplyResources(this.tsmiCD, "tsmiCD");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Name = "tsmiRefresh";
            resources.ApplyResources(this.tsmiRefresh, "tsmiRefresh");
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsmiRip
            // 
            this.tsmiRip.Name = "tsmiRip";
            resources.ApplyResources(this.tsmiRip, "tsmiRip");
            this.tsmiRip.Click += new System.EventHandler(this.tsmiRip_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // tsmiQuit
            // 
            this.tsmiQuit.Name = "tsmiQuit";
            resources.ApplyResources(this.tsmiQuit, "tsmiQuit");
            this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
            // 
            // metadataToolStripMenuItem
            // 
            this.metadataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDownloadMetadata,
            this.tsmiShowOnMusicBrainz,
            this.toolStripMenuItem5,
            this.tsmiMbPrivacy,
            this.tsmiIaPrivacy});
            this.metadataToolStripMenuItem.Name = "metadataToolStripMenuItem";
            resources.ApplyResources(this.metadataToolStripMenuItem, "metadataToolStripMenuItem");
            // 
            // tsmiDownloadMetadata
            // 
            this.tsmiDownloadMetadata.Name = "tsmiDownloadMetadata";
            resources.ApplyResources(this.tsmiDownloadMetadata, "tsmiDownloadMetadata");
            this.tsmiDownloadMetadata.Click += new System.EventHandler(this.tsmiDownloadMetadata_Click);
            // 
            // tsmiShowOnMusicBrainz
            // 
            this.tsmiShowOnMusicBrainz.Name = "tsmiShowOnMusicBrainz";
            resources.ApplyResources(this.tsmiShowOnMusicBrainz, "tsmiShowOnMusicBrainz");
            this.tsmiShowOnMusicBrainz.Click += new System.EventHandler(this.tsmiShowOnMusicBrainz_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // tsmiMbPrivacy
            // 
            this.tsmiMbPrivacy.Name = "tsmiMbPrivacy";
            resources.ApplyResources(this.tsmiMbPrivacy, "tsmiMbPrivacy");
            this.tsmiMbPrivacy.Click += new System.EventHandler(this.tsmiMbPrivacy_Click);
            // 
            // tsmiIaPrivacy
            // 
            this.tsmiIaPrivacy.Name = "tsmiIaPrivacy";
            resources.ApplyResources(this.tsmiIaPrivacy, "tsmiIaPrivacy");
            this.tsmiIaPrivacy.Click += new System.EventHandler(this.tsmiIaPrivacy_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEjectCd,
            this.tsmiCloseTray,
            this.toolStripMenuItem6,
            this.tsmiResetAllSettings});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // tsmiEjectCd
            // 
            this.tsmiEjectCd.Name = "tsmiEjectCd";
            resources.ApplyResources(this.tsmiEjectCd, "tsmiEjectCd");
            this.tsmiEjectCd.Click += new System.EventHandler(this.tsmiEjectCd_Click);
            // 
            // tsmiCloseTray
            // 
            this.tsmiCloseTray.Name = "tsmiCloseTray";
            resources.ApplyResources(this.tsmiCloseTray, "tsmiCloseTray");
            this.tsmiCloseTray.Click += new System.EventHandler(this.tsmiCloseTray_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // tsmiResetAllSettings
            // 
            this.tsmiResetAllSettings.Name = "tsmiResetAllSettings";
            resources.ApplyResources(this.tsmiResetAllSettings, "tsmiResetAllSettings");
            this.tsmiResetAllSettings.Click += new System.EventHandler(this.tsmiResetAllSettings_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiErrorLog,
            this.toolStripMenuItem3,
            this.tsmiWebsite,
            this.toolStripMenuItem4,
            this.tsmiAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // tsmiErrorLog
            // 
            this.tsmiErrorLog.Name = "tsmiErrorLog";
            resources.ApplyResources(this.tsmiErrorLog, "tsmiErrorLog");
            this.tsmiErrorLog.Click += new System.EventHandler(this.tsmiErrorLog_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // tsmiWebsite
            // 
            this.tsmiWebsite.Name = "tsmiWebsite";
            resources.ApplyResources(this.tsmiWebsite, "tsmiWebsite");
            this.tsmiWebsite.Click += new System.EventHandler(this.tsmiWebsite_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            resources.ApplyResources(this.tsmiAbout, "tsmiAbout");
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // ssStatusBar
            // 
            this.ssStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatusText});
            resources.ApplyResources(this.ssStatusBar, "ssStatusBar");
            this.ssStatusBar.Name = "ssStatusBar";
            // 
            // tsslStatusText
            // 
            this.tsslStatusText.Name = "tsslStatusText";
            resources.ApplyResources(this.tsslStatusText, "tsslStatusText");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbDriveSelection,
            this.tsbRefresh,
            this.tsbLoadMetadata,
            this.toolStripSeparator1,
            this.tsbRip});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // cbDriveSelection
            // 
            this.cbDriveSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDriveSelection.Name = "cbDriveSelection";
            resources.ApplyResources(this.cbDriveSelection, "cbDriveSelection");
            this.cbDriveSelection.SelectedIndexChanged += new System.EventHandler(this.cbDriveSelection_SelectedIndexChanged);
            // 
            // tsbRefresh
            // 
            resources.ApplyResources(this.tsbRefresh, "tsbRefresh");
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // tsbLoadMetadata
            // 
            resources.ApplyResources(this.tsbLoadMetadata, "tsbLoadMetadata");
            this.tsbLoadMetadata.Name = "tsbLoadMetadata";
            this.tsbLoadMetadata.Click += new System.EventHandler(this.tsbLoadMetadata_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbRip
            // 
            resources.ApplyResources(this.tsbRip, "tsbRip");
            this.tsbRip.Name = "tsbRip";
            this.tsbRip.Click += new System.EventHandler(this.tsbRip_Click);
            // 
            // dgvToc
            // 
            this.dgvToc.AllowUserToAddRows = false;
            this.dgvToc.AllowUserToDeleteRows = false;
            this.dgvToc.AllowUserToResizeRows = false;
            this.dgvToc.AutoGenerateColumns = false;
            this.dgvToc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvToc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvToc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvToc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcbcRip,
            this.dgvtbcTrackNo,
            this.dgvtbcTitle,
            this.dgvtbcArtist,
            this.dgvtbcComposer,
            this.dgvtbcLength});
            this.dgvToc.DataSource = this.bsToc;
            resources.ApplyResources(this.dgvToc, "dgvToc");
            this.dgvToc.Name = "dgvToc";
            this.dgvToc.RowHeadersVisible = false;
            this.dgvToc.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwToc_CellFormatting);
            this.dgvToc.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvToc_ColumnHeaderMouseClick);
            // 
            // dgvcbcRip
            // 
            this.dgvcbcRip.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvcbcRip.DataPropertyName = "Selected";
            resources.ApplyResources(this.dgvcbcRip, "dgvcbcRip");
            this.dgvcbcRip.Name = "dgvcbcRip";
            // 
            // dgvtbcTrackNo
            // 
            this.dgvtbcTrackNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvtbcTrackNo.DataPropertyName = "TrackNo";
            dataGridViewCellStyle1.Format = "D2";
            this.dgvtbcTrackNo.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dgvtbcTrackNo, "dgvtbcTrackNo");
            this.dgvtbcTrackNo.Name = "dgvtbcTrackNo";
            this.dgvtbcTrackNo.ReadOnly = true;
            this.dgvtbcTrackNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvtbcTrackNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvtbcTitle
            // 
            this.dgvtbcTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvtbcTitle.DataPropertyName = "Title";
            this.dgvtbcTitle.FillWeight = 150F;
            resources.ApplyResources(this.dgvtbcTitle, "dgvtbcTitle");
            this.dgvtbcTitle.Name = "dgvtbcTitle";
            this.dgvtbcTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvtbcArtist
            // 
            this.dgvtbcArtist.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvtbcArtist.DataPropertyName = "Artist";
            resources.ApplyResources(this.dgvtbcArtist, "dgvtbcArtist");
            this.dgvtbcArtist.Name = "dgvtbcArtist";
            this.dgvtbcArtist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvtbcComposer
            // 
            this.dgvtbcComposer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvtbcComposer.DataPropertyName = "Composer";
            resources.ApplyResources(this.dgvtbcComposer, "dgvtbcComposer");
            this.dgvtbcComposer.Name = "dgvtbcComposer";
            this.dgvtbcComposer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvtbcLength
            // 
            this.dgvtbcLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvtbcLength.DataPropertyName = "Length";
            resources.ApplyResources(this.dgvtbcLength, "dgvtbcLength");
            this.dgvtbcLength.Name = "dgvtbcLength";
            this.dgvtbcLength.ReadOnly = true;
            this.dgvtbcLength.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvtbcLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bsToc
            // 
            this.bsToc.DataSource = typeof(CddaX.MetaStore.TrackMeta);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tbDiscNo
            // 
            this.tbDiscNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDiscMeta, "DiscNo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
            resources.ApplyResources(this.tbDiscNo, "tbDiscNo");
            this.tbDiscNo.Name = "tbDiscNo";
            // 
            // bsDiscMeta
            // 
            this.bsDiscMeta.AllowNew = false;
            this.bsDiscMeta.DataSource = typeof(CddaX.MetaStore.DiscMeta);
            // 
            // tbDiscYear
            // 
            this.tbDiscYear.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDiscMeta, "Year", true));
            resources.ApplyResources(this.tbDiscYear, "tbDiscYear");
            this.tbDiscYear.Name = "tbDiscYear";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tbDiscComposer
            // 
            this.pDiscMeta.SetColumnSpan(this.tbDiscComposer, 4);
            this.tbDiscComposer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDiscMeta, "Composer", true));
            resources.ApplyResources(this.tbDiscComposer, "tbDiscComposer");
            this.tbDiscComposer.Name = "tbDiscComposer";
            this.tbDiscComposer.Validated += new System.EventHandler(this.tbDiscComposer_Validated);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tbDiscArtist
            // 
            this.pDiscMeta.SetColumnSpan(this.tbDiscArtist, 4);
            this.tbDiscArtist.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDiscMeta, "Artist", true));
            resources.ApplyResources(this.tbDiscArtist, "tbDiscArtist");
            this.tbDiscArtist.Name = "tbDiscArtist";
            this.tbDiscArtist.Validated += new System.EventHandler(this.tbDiscArtist_Validated);
            // 
            // tbDiscTitle
            // 
            this.pDiscMeta.SetColumnSpan(this.tbDiscTitle, 4);
            this.tbDiscTitle.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDiscMeta, "Title", true));
            resources.ApplyResources(this.tbDiscTitle, "tbDiscTitle");
            this.tbDiscTitle.Name = "tbDiscTitle";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pbCoverArt
            // 
            resources.ApplyResources(this.pbCoverArt, "pbCoverArt");
            this.pbCoverArt.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.bsDiscMeta, "CoverImage", true));
            this.pbCoverArt.MinimumSize = new System.Drawing.Size(10, 10);
            this.pbCoverArt.Name = "pbCoverArt";
            this.pDiscMeta.SetRowSpan(this.pbCoverArt, 4);
            this.pbCoverArt.TabStop = false;
            // 
            // pDiscMeta
            // 
            resources.ApplyResources(this.pDiscMeta, "pDiscMeta");
            this.pDiscMeta.Controls.Add(this.pbCoverArt, 6, 0);
            this.pDiscMeta.Controls.Add(this.label5, 3, 3);
            this.pDiscMeta.Controls.Add(this.label1, 0, 0);
            this.pDiscMeta.Controls.Add(this.tbDiscNo, 4, 3);
            this.pDiscMeta.Controls.Add(this.label2, 0, 1);
            this.pDiscMeta.Controls.Add(this.tbDiscYear, 1, 3);
            this.pDiscMeta.Controls.Add(this.label3, 0, 2);
            this.pDiscMeta.Controls.Add(this.tbDiscComposer, 1, 2);
            this.pDiscMeta.Controls.Add(this.label4, 0, 3);
            this.pDiscMeta.Controls.Add(this.tbDiscArtist, 1, 1);
            this.pDiscMeta.Controls.Add(this.tbDiscTitle, 1, 0);
            this.pDiscMeta.Name = "pDiscMeta";
            // 
            // registrySettings
            // 
            this.registrySettings.SoftwareName = "CddaX";
            // 
            // mediaChangeNotificationHelper
            // 
            this.mediaChangeNotificationHelper.MediumInserted += new System.EventHandler<CddaX.Util.MediaChangeNotificationEventArgs>(this.mediaChangeNotificationHelper_MediumInserted);
            this.mediaChangeNotificationHelper.MediumRemoved += new System.EventHandler<CddaX.Util.MediaChangeNotificationEventArgs>(this.mediaChangeNotificationHelper_MediumRemoved);
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.dgvToc);
            this.Controls.Add(this.pDiscMeta);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ssStatusBar);
            this.Controls.Add(this.msMainMenu);
            this.MainMenuStrip = this.msMainMenu;
            this.Name = "MainWindow";
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.ssStatusBar.ResumeLayout(false);
            this.ssStatusBar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsToc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDiscMeta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoverArt)).EndInit();
            this.pDiscMeta.ResumeLayout(false);
            this.pDiscMeta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiCD;
        private System.Windows.Forms.ToolStripMenuItem metadataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.StatusStrip ssStatusBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cbDriveSelection;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbLoadMetadata;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRip;
        private System.Windows.Forms.DataGridView dgvToc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbCoverArt;
        private System.Windows.Forms.TextBox tbDiscArtist;
        private System.Windows.Forms.TextBox tbDiscTitle;
        private System.Windows.Forms.BindingSource bsToc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDiscNo;
        private System.Windows.Forms.TextBox tbDiscYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDiscComposer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
        private System.Windows.Forms.ToolStripMenuItem tsmiDownloadMetadata;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowOnMusicBrainz;
        private System.Windows.Forms.ToolStripMenuItem tsmiEjectCd;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseTray;
        private System.Windows.Forms.ToolStripMenuItem tsmiWebsite;
        private System.Windows.Forms.ToolStripMenuItem tsmiErrorLog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmiMbPrivacy;
        private System.Windows.Forms.ToolStripMenuItem tsmiIaPrivacy;
        private System.Windows.Forms.BindingSource bsDiscMeta;
        private System.Windows.Forms.ToolStripMenuItem tsmiRip;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusText;
        private Util.RegistrySettings registrySettings;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetAllSettings;
        private Util.MediaChangeNotificationHelper mediaChangeNotificationHelper;
        private System.Windows.Forms.TableLayoutPanel pDiscMeta;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcbcRip;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcTrackNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcArtist;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcComposer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbcLength;
    }
}