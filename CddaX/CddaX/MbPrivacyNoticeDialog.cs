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
    public partial class MbPrivacyNoticeDialog : Form
    {
        public bool NeverAskAgain
        {
            get
            {
                return cbNeverAgain.Checked;
            }
        }

        public MbPrivacyNoticeDialog()
        {
            InitializeComponent();

            FormHelper.ActivateSegoeUi(this);

            using (Icon i = new Icon(Properties.Resources.MusicBrainzIcon, new Size(128, 128)))
            {
                pbMusicBrainzLogo.Image = i.ToBitmap();
            }
        }

        private void llMbPrivacy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OSHelper.LaunchUrl(this, MusicBrainz.BrowserUrl.MbPrivacyPolicy);
        }

        private void llCaaPrivacy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OSHelper.LaunchUrl(this, MusicBrainz.BrowserUrl.CoverArtArchivePrivacyPolicy);
        }
    }
}
