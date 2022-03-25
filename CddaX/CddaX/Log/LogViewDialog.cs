using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CddaX.Log
{
    public partial class LogViewDialog : Form
    {
        public LogViewDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            textBox1.Text = Logger.LogAsString();

            base.OnLoad(e);
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
