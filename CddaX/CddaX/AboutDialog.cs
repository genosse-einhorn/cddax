using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CddaX.Util;

namespace CddaX
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();

            object[] attributes = this.GetType().Assembly.GetCustomAttributes(false);
            foreach (object o in attributes)
            {
                if (o is AssemblyTitleAttribute)
                {
                    lSoftwareName.Text = string.Format("{0} v{1}",
                        ((AssemblyTitleAttribute)o).Title,
                        this.GetType().Assembly.GetName().Version.ToString());
                }
                if (o is AssemblyCopyrightAttribute)
                {
                    lCopyright.Text = ((AssemblyCopyrightAttribute)o).Copyright;
                }
            }

            using (Icon i = new Icon(CddaX.Properties.Resources.LogoIcon, pbLogo.Size))
            {
                pbLogo.Image = i.ToBitmap();
            }

            FormHelper.ActivateSegoeUi(this);
        }
    }
}
