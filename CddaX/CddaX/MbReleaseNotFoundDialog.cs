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
    public partial class MbReleaseNotFoundDialog : Form
    {
        private CddaLib.Toc m_toc;

        public MbReleaseNotFoundDialog(CddaLib.Toc toc)
        {
            InitializeComponent();
            
            m_toc = toc;

            FormHelper.ActivateSegoeUi(this);
        }

        private void llAddYourself_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OSHelper.LaunchUrl(this, MusicBrainz.BrowserUrl.AttachCdStub(m_toc));
        }
    }
}
