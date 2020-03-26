using System;
using System.Drawing;
using System.Linq;
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
            fontFamilyNameCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            fontFamilyNameCmb.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var autoCompleteStringCollection = new AutoCompleteStringCollection();
            autoCompleteStringCollection.AddRange(FontFamily.Families.Select(f => f.Name).ToArray());
            fontFamilyNameCmb.AutoCompleteCustomSource = autoCompleteStringCollection;
        }

        private void fontFamilyNameCmb_TextChanged(object sender, EventArgs e)
        {
            if (!FontFamily.Families.Select(f => f.Name).Contains(fontFamilyNameCmb.Text))
                return;

            ExternalHelper.FontFamilyName = fontFamilyNameCmb.Text;
        }

    }
}
