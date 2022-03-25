using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CddaX.Log;
using CddaX.Util;

namespace CddaX
{
    public partial class RipProgressDialog : Form
    {
        private BackgroundWorker m_worker;
        private object m_workerRunArg;

        public RipProgressDialog(BackgroundWorker worker, object workerStartArg)
        {
            this.m_worker = worker;
            this.m_workerRunArg = workerStartArg;

            InitializeComponent();

            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            FormHelper.ActivateSegoeUi(this);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Logger.Exception(e.Error, "Rip worker returned exception");
                taskbarProgressHelper.SetProgressState(this.Owner, TaskbarProgressFlag.Error);
                MessageBox.Show(this, e.Error.Message, CddaX.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (!e.Cancelled)
            {
                Logger.Info("Rip worker returned success");
                taskbarProgressHelper.SetProgressState(this.Owner, TaskbarProgressFlag.NoProgress);
                MessageBox.Show(this, CddaX.Properties.Resources.RipCompleteMessage, CddaX.Properties.Resources.Success, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.Close();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lProgressText.Text = e.UserState.ToString();
            taskbarProgressHelper.SetProgressValue(this.Owner, (ulong)e.ProgressPercentage, 100);
        }

        private void RipProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_worker.IsBusy)
            {
                e.Cancel = true;
                m_worker.CancelAsync();
                bCancel.Enabled = false;
            }
            else
            {
                taskbarProgressHelper.SetProgressState(this.Owner, TaskbarProgressFlag.NoProgress);
            }
        }

        private void RipProgressDialog_Shown(object sender, EventArgs e)
        {
            taskbarProgressHelper.SetProgressState(this.Owner, TaskbarProgressFlag.Indeterminate);
            m_worker.RunWorkerAsync(m_workerRunArg);
        }
    }
}
