using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMapForVisualStudio
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            BackColor = SystemColors.Control;
            LoadCodeMapConfigurations();
        }

        private void LoadCodeMapConfigurations()
        {
            // Load font styles
            foreach (var fontFamily in FontFamily.Families)
                fontFamilyNameCmb.Items.Add(fontFamily.Name);
            fontFamilyNameCmb.SelectedItem = ExternalHelper.FontFamilyName;
        }

        private void fontFamilyNameCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExternalHelper.FontFamilyName = fontFamilyNameCmb.SelectedText;
        }
    }
}
