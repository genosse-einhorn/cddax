namespace CddaX
{
    partial class RipParametersDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RipParametersDialog));
            this.tbSubfolder = new System.Windows.Forms.TextBox();
            this.bsRipParameters = new System.Windows.Forms.BindingSource(this.components);
            this.cbCreateSubfolder = new System.Windows.Forms.CheckBox();
            this.bBrowse = new System.Windows.Forms.Button();
            this.tbBaseFolder = new System.Windows.Forms.TextBox();
            this.lFolder = new System.Windows.Forms.Label();
            this.cbMp3Quality = new System.Windows.Forms.ComboBox();
            this.rbFlac = new System.Windows.Forms.RadioButton();
            this.rbMp3 = new System.Windows.Forms.RadioButton();
            this.rbWav = new System.Windows.Forms.RadioButton();
            this.bRip = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lTargetFolderHeader = new System.Windows.Forms.Label();
            this.lFileFormatHeader = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.bsRipParameters)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSubfolder
            // 
            resources.ApplyResources(this.tbSubfolder, "tbSubfolder");
            this.tbSubfolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRipParameters, "TargetSubDirectory", true));
            this.tbSubfolder.Name = "tbSubfolder";
            // 
            // bsRipParameters
            // 
            this.bsRipParameters.AllowNew = false;
            this.bsRipParameters.DataSource = typeof(CddaX.Ripper.RipParameters);
            // 
            // cbCreateSubfolder
            // 
            resources.ApplyResources(this.cbCreateSubfolder, "cbCreateSubfolder");
            this.cbCreateSubfolder.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsRipParameters, "SubDirectoryEnabled", true));
            this.cbCreateSubfolder.Name = "cbCreateSubfolder";
            this.cbCreateSubfolder.UseVisualStyleBackColor = true;
            this.cbCreateSubfolder.CheckedChanged += new System.EventHandler(this.cbCreateSubfolder_CheckedChanged);
            // 
            // bBrowse
            // 
            resources.ApplyResources(this.bBrowse, "bBrowse");
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tbBaseFolder
            // 
            resources.ApplyResources(this.tbBaseFolder, "tbBaseFolder");
            this.tbBaseFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRipParameters, "TargetBaseDirectory", true));
            this.tbBaseFolder.Name = "tbBaseFolder";
            // 
            // lFolder
            // 
            resources.ApplyResources(this.lFolder, "lFolder");
            this.lFolder.Name = "lFolder";
            // 
            // cbMp3Quality
            // 
            this.cbMp3Quality.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bsRipParameters, "Mp3Quality", true));
            this.cbMp3Quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbMp3Quality, "cbMp3Quality");
            this.cbMp3Quality.FormattingEnabled = true;
            this.cbMp3Quality.Name = "cbMp3Quality";
            // 
            // rbFlac
            // 
            resources.ApplyResources(this.rbFlac, "rbFlac");
            this.tableLayoutPanel5.SetColumnSpan(this.rbFlac, 2);
            this.rbFlac.Name = "rbFlac";
            // 
            // rbMp3
            // 
            resources.ApplyResources(this.rbMp3, "rbMp3");
            this.rbMp3.Name = "rbMp3";
            this.rbMp3.TabStop = true;
            this.rbMp3.UseVisualStyleBackColor = true;
            this.rbMp3.CheckedChanged += new System.EventHandler(this.rbMp3_CheckedChanged);
            // 
            // rbWav
            // 
            resources.ApplyResources(this.rbWav, "rbWav");
            this.tableLayoutPanel5.SetColumnSpan(this.rbWav, 2);
            this.rbWav.Name = "rbWav";
            this.rbWav.TabStop = true;
            this.rbWav.UseVisualStyleBackColor = true;
            // 
            // bRip
            // 
            this.bRip.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.bRip, "bRip");
            this.bRip.Name = "bRip";
            this.bRip.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.bCancel, "bCancel");
            this.bCancel.Name = "bCancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // lTargetFolderHeader
            // 
            resources.ApplyResources(this.lTargetFolderHeader, "lTargetFolderHeader");
            this.lTargetFolderHeader.Name = "lTargetFolderHeader";
            // 
            // lFileFormatHeader
            // 
            resources.ApplyResources(this.lFileFormatHeader, "lFileFormatHeader");
            this.lFileFormatHeader.Name = "lFileFormatHeader";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.Controls.Add(this.bRip, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bCancel, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lTargetFolderHeader, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lFileFormatHeader, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 1, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.lFolder, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbBaseFolder, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.bBrowse, 2, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.cbCreateSubfolder, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbSubfolder, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.rbFlac, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbMp3Quality, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.rbMp3, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.rbWav, 0, 2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // RipParametersDialog
            // 
            this.AcceptButton = this.bRip;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RipParametersDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.bsRipParameters)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSubfolder;
        private System.Windows.Forms.CheckBox cbCreateSubfolder;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.TextBox tbBaseFolder;
        private System.Windows.Forms.Label lFolder;
        private System.Windows.Forms.ComboBox cbMp3Quality;
        private System.Windows.Forms.RadioButton rbFlac;
        private System.Windows.Forms.RadioButton rbMp3;
        private System.Windows.Forms.RadioButton rbWav;
        private System.Windows.Forms.Button bRip;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.BindingSource bsRipParameters;
        private System.Windows.Forms.Label lTargetFolderHeader;
        private System.Windows.Forms.Label lFileFormatHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}