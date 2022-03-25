namespace CddaX
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.bClose = new System.Windows.Forms.Button();
            this.lGpl = new System.Windows.Forms.Label();
            this.lSoftwareName = new System.Windows.Forms.Label();
            this.lCopyright = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bClose
            // 
            resources.ApplyResources(this.bClose, "bClose");
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.Name = "bClose";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // lGpl
            // 
            resources.ApplyResources(this.lGpl, "lGpl");
            this.tableLayoutPanel1.SetColumnSpan(this.lGpl, 3);
            this.lGpl.Name = "lGpl";
            // 
            // lSoftwareName
            // 
            resources.ApplyResources(this.lSoftwareName, "lSoftwareName");
            this.lSoftwareName.Name = "lSoftwareName";
            // 
            // lCopyright
            // 
            resources.ApplyResources(this.lCopyright, "lCopyright");
            this.lCopyright.Name = "lCopyright";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.tableLayoutPanel1.SetColumnSpan(this.label4, 3);
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.tableLayoutPanel1.SetColumnSpan(this.label5, 3);
            this.label5.Name = "label5";
            // 
            // pbLogo
            // 
            resources.ApplyResources(this.pbLogo, "pbLogo");
            this.pbLogo.Name = "pbLogo";
            this.tableLayoutPanel1.SetRowSpan(this.pbLogo, 2);
            this.pbLogo.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.pbLogo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lGpl, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lSoftwareName, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lCopyright, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 5);
            this.tableLayoutPanel2.Controls.Add(this.bClose, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.bClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.bClose;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Label lGpl;
        private System.Windows.Forms.Label lSoftwareName;
        private System.Windows.Forms.Label lCopyright;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}