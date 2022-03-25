using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using CddaX.Util;

namespace CddaX
{
    public partial class MbReleaseSelectDialog : Form
    {
        private MusicBrainz.Release[] m_releaseList;
        private CddaLib.Toc m_toc;
        private string m_mcn;

        public MusicBrainz.Release SelectedRelease
        {
            get
            {
                if (dgvReleases.SelectedRows.Count > 0)
                {
                    return m_releaseList[dgvReleases.SelectedRows[0].Index];
                }
                else
                {
                    return null;
                }
            }
        }

        public MbReleaseSelectDialog(MusicBrainz.Release[] releases, CddaLib.Toc toc, string mcn)
        {
            InitializeComponent();
            
            m_toc = toc;
            m_mcn = mcn;
            m_releaseList = releases;
            this.bsReleaseList.DataSource = m_releaseList;

            FormHelper.ActivateSegoeUi(this);

            int pad = (this.Font.Height + 10) / 11;
            dgvReleases.DefaultCellStyle.Padding = new Padding(0, pad, 0, pad);
        }

        private void llAddYourself_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OSHelper.LaunchUrl(this, MusicBrainz.BrowserUrl.AttachCdStub(m_toc));
        }

        private void dgwReleases_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.AcceptButton.PerformClick();
            }
        }

        private void dgwReleases_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // workaround for mono bug(?)
            dgvReleases.AutoResizeColumns();
            
            for (int i = 0; i < m_releaseList.Length; ++i)
            {
                if (!string.IsNullOrEmpty(m_mcn)
                    && !string.IsNullOrEmpty(m_releaseList[i].Barcode)
                    && m_releaseList[i].Barcode.TrimStart('0') == m_mcn.TrimStart('0'))
                {
                    dgvReleases.CurrentCell = dgvReleases.Rows[i].Cells[0];
                    break;
                }
            }
        }

        private void MbReleaseSelectDialog_Shown(object sender, EventArgs e)
        {
            // HACK! fix broken rendering on first load on some systems
            // I'd really like to fix it at the source, but all attempts
            // at debugging it have been unsuccessful so far
            IdleRunner.DelayUntilIdle(() => dgvReleases.Refresh());
        }
    }
}
