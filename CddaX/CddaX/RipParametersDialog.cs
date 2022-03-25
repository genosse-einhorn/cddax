using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CddaX.Util;

namespace CddaX
{
    public partial class RipParametersDialog : Form
    {
        private Ripper.RipParameters m_parameters;

        public RipParametersDialog(Ripper.RipParameters parameters)
        {
            InitializeComponent();

            AddRadioCheckedBinding(rbFlac, bsRipParameters, "FileFormat", Ripper.RipParameters.FileFormats.Flac);
            AddRadioCheckedBinding(rbMp3, bsRipParameters, "FileFormat", Ripper.RipParameters.FileFormats.Mp3);
            AddRadioCheckedBinding(rbWav, bsRipParameters, "FileFormat", Ripper.RipParameters.FileFormats.Wav);

            m_parameters = parameters;
            bsRipParameters.DataSource = new Ripper.RipParameters[] { m_parameters };

            cbMp3Quality.DataSource = Ripper.Mp3Quality.SupportedQualities;

            FormHelper.ActivateSegoeUi(this);
            FormHelper.PadLabelToEdit(lFolder);
            FormHelper.TopAlignCheckboxToEditByMargin(cbCreateSubfolder, tbSubfolder);
            FormHelper.TopAlignComboToCheckboxByMargin(cbMp3Quality, rbMp3);
            FormHelper.FormatHeaderLabel(lTargetFolderHeader, lFileFormatHeader);
            FormHelper.VerticallyCenterByMargin(bBrowse, tbBaseFolder);

            UpdateUiStates();
        }

        private void AddRadioCheckedBinding<T>(RadioButton radio, object dataSource, string dataMember, T trueValue)
        {
            var binding = new Binding("Checked", dataSource, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);
            binding.FormattingEnabled = true; // Mono BUG: ignores formattingEnabled in constructor
            binding.Parse += (s, a) => { if ((bool)a.Value) a.Value = trueValue; };
            binding.Format += (s, a) => a.Value = ((T)a.Value).Equals(trueValue);
            radio.DataBindings.Add(binding);
        }

        private void UpdateUiStates()
        {
            tbSubfolder.Enabled = cbCreateSubfolder.Checked;
            cbMp3Quality.Enabled = rbMp3.Checked;
        }

        private void cbCreateSubfolder_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUiStates();
        }

        private void rbMp3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUiStates();
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = tbBaseFolder.Text;
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    tbBaseFolder.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
