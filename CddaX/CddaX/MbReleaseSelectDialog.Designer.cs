namespace CddaX
{
    partial class MbReleaseSelectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MbReleaseSelectDialog));
            this.dgvReleases = new System.Windows.Forms.DataGridView();
            this.bsReleaseList = new System.Windows.Forms.BindingSource(this.components);
            this.bConfirm = new System.Windows.Forms.Button();
            this.cCancel = new System.Windows.Forms.Button();
            this.llAddYourself = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ArtistsStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReleases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReleaseList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReleases
            // 
            resources.ApplyResources(this.dgvReleases, "dgvReleases");
            this.dgvReleases.AllowUserToAddRows = false;
            this.dgvReleases.AllowUserToDeleteRows = false;
            this.dgvReleases.AllowUserToResizeColumns = false;
            this.dgvReleases.AllowUserToResizeRows = false;
            this.dgvReleases.AutoGenerateColumns = false;
            this.dgvReleases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReleases.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReleases.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvReleases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReleases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArtistsStr,
            this.titleDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.countryDataGridViewTextBoxColumn,
            this.barcodeDataGridViewTextBoxColumn});
            this.dgvReleases.DataSource = this.bsReleaseList;
            this.dgvReleases.MultiSelect = false;
            this.dgvReleases.Name = "dgvReleases";
            this.dgvReleases.ReadOnly = true;
            this.dgvReleases.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvReleases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReleases.StandardTab = true;
            this.dgvReleases.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgwReleases_DataBindingComplete);
            this.dgvReleases.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwReleases_KeyDown);
            // 
            // bsReleaseList
            // 
            this.bsReleaseList.AllowNew = false;
            this.bsReleaseList.DataSource = typeof(CddaX.MusicBrainz.Release);
            // 
            // bConfirm
            // 
            resources.ApplyResources(this.bConfirm, "bConfirm");
            this.bConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bConfirm.Name = "bConfirm";
            this.bConfirm.UseVisualStyleBackColor = true;
            // 
            // cCancel
            // 
            resources.ApplyResources(this.cCancel, "cCancel");
            this.cCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cCancel.Name = "cCancel";
            this.cCancel.UseVisualStyleBackColor = true;
            // 
            // llAddYourself
            // 
            resources.ApplyResources(this.llAddYourself, "llAddYourself");
            this.llAddYourself.Name = "llAddYourself";
            this.llAddYourself.TabStop = true;
            this.llAddYourself.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAddYourself_LinkClicked);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.Controls.Add(this.cCancel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.bConfirm, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.dgvReleases, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.llAddYourself, 1, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // ArtistsStr
            // 
            this.ArtistsStr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ArtistsStr.DataPropertyName = "ArtistsStr";
            resources.ApplyResources(this.ArtistsStr, "ArtistsStr");
            this.ArtistsStr.Name = "ArtistsStr";
            this.ArtistsStr.ReadOnly = true;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            resources.ApplyResources(this.titleDataGridViewTextBoxColumn, "titleDataGridViewTextBoxColumn");
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            resources.ApplyResources(this.dateDataGridViewTextBoxColumn, "dateDataGridViewTextBoxColumn");
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countryDataGridViewTextBoxColumn
            // 
            this.countryDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.countryDataGridViewTextBoxColumn.DataPropertyName = "Country";
            resources.ApplyResources(this.countryDataGridViewTextBoxColumn, "countryDataGridViewTextBoxColumn");
            this.countryDataGridViewTextBoxColumn.Name = "countryDataGridViewTextBoxColumn";
            this.countryDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodeDataGridViewTextBoxColumn
            // 
            this.barcodeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.barcodeDataGridViewTextBoxColumn.DataPropertyName = "Barcode";
            resources.ApplyResources(this.barcodeDataGridViewTextBoxColumn, "barcodeDataGridViewTextBoxColumn");
            this.barcodeDataGridViewTextBoxColumn.Name = "barcodeDataGridViewTextBoxColumn";
            this.barcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MbReleaseSelectDialog
            // 
            this.AcceptButton = this.bConfirm;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.cCancel;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MbReleaseSelectDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.MbReleaseSelectDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReleases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReleaseList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReleases;
        private System.Windows.Forms.BindingSource bsReleaseList;
        private System.Windows.Forms.Button bConfirm;
        private System.Windows.Forms.Button cCancel;
        private System.Windows.Forms.LinkLabel llAddYourself;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArtistsStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeDataGridViewTextBoxColumn;
    }
}