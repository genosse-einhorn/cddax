namespace CddaX
{
    partial class MbPrivacyNoticeDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MbPrivacyNoticeDialog));
            this.bCancel = new System.Windows.Forms.Button();
            this.bContinue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.llMbPrivacy = new System.Windows.Forms.LinkLabel();
            this.llCaaPrivacy = new System.Windows.Forms.LinkLabel();
            this.cbNeverAgain = new System.Windows.Forms.CheckBox();
            this.pbMusicBrainzLogo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbMusicBrainzLogo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            resources.ApplyResources(this.bCancel, "bCancel");
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Name = "bCancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bContinue
            // 
            resources.ApplyResources(this.bContinue, "bContinue");
            this.bContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bContinue.Name = "bContinue";
            this.bContinue.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // llMbPrivacy
            // 
            resources.ApplyResources(this.llMbPrivacy, "llMbPrivacy");
            this.llMbPrivacy.Name = "llMbPrivacy";
            this.llMbPrivacy.TabStop = true;
            this.llMbPrivacy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMbPrivacy_LinkClicked);
            // 
            // llCaaPrivacy
            // 
            resources.ApplyResources(this.llCaaPrivacy, "llCaaPrivacy");
            this.llCaaPrivacy.Name = "llCaaPrivacy";
            this.llCaaPrivacy.TabStop = true;
            this.llCaaPrivacy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCaaPrivacy_LinkClicked);
            // 
            // cbNeverAgain
            // 
            resources.ApplyResources(this.cbNeverAgain, "cbNeverAgain");
            this.cbNeverAgain.Checked = true;
            this.cbNeverAgain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNeverAgain.Name = "cbNeverAgain";
            this.cbNeverAgain.UseVisualStyleBackColor = true;
            // 
            // pbMusicBrainzLogo
            // 
            resources.ApplyResources(this.pbMusicBrainzLogo, "pbMusicBrainzLogo");
            this.pbMusicBrainzLogo.Name = "pbMusicBrainzLogo";
            this.tableLayoutPanel2.SetRowSpan(this.pbMusicBrainzLogo, 4);
            this.pbMusicBrainzLogo.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.Controls.Add(this.bContinue, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bCancel, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.llMbPrivacy, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.llCaaPrivacy, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbNeverAgain, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.pbMusicBrainzLogo, 2, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // MbPrivacyNoticeDialog
            // 
            this.AcceptButton = this.bContinue;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.bCancel;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MbPrivacyNoticeDialog";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pbMusicBrainzLogo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llMbPrivacy;
        private System.Windows.Forms.LinkLabel llCaaPrivacy;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bContinue;
        private System.Windows.Forms.CheckBox cbNeverAgain;
        private System.Windows.Forms.PictureBox pbMusicBrainzLogo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}