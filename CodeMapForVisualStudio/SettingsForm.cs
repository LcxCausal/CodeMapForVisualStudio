using Microsoft.VisualStudio.Shell;
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace CodeMapForVisualStudio
{
    public partial class SettingsForm : Form
    {
        private readonly string[] fontFamilies;
        private readonly string[] fontWeights;
        private readonly CodeMap codeMap;

        private SettingsForm()
            : this(null)
        { }

        public SettingsForm(CodeMap codeMap)
        {
            InitializeComponent();

            this.codeMap = codeMap;

            fontFamilies = FontFamily.Families.Select(f => f.Name).ToArray();
            fontWeights = typeof(FontWeights).GetProperties().Select(p => p.Name).ToArray();
            BackColor = System.Drawing.SystemColors.Control;

            var fontFamiliesAutoCompleteStringCollection = new AutoCompleteStringCollection();
            fontFamiliesAutoCompleteStringCollection.AddRange(fontFamilies);
            fontFamilyNameCmb.Items.AddRange(fontFamilies);
            fontFamilyNameCmb.AutoCompleteCustomSource = fontFamiliesAutoCompleteStringCollection;
            fontFamilyNameCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            fontFamilyNameCmb.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var fontWeightsAutoCompleteStringCollection = new AutoCompleteStringCollection();
            fontWeightsAutoCompleteStringCollection.AddRange(fontWeights);
            fontWeightCmd.Items.AddRange(fontWeights);
            fontWeightCmd.AutoCompleteCustomSource = fontWeightsAutoCompleteStringCollection;
            fontWeightCmd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            fontWeightCmd.AutoCompleteSource = AutoCompleteSource.CustomSource;

            LoadCodeMapConfigurations();
        }

        /// <summary>
        /// 加载 CodeMap 的配置信息。
        /// </summary>
        private void LoadCodeMapConfigurations()
        {
            LoadFontFamilyName();
            LoadFontSize();
            LoadFontWeight();
            LoadFontMargins();
        }

        /// <summary>
        /// 加载字体的 FamilyName.
        /// </summary>
        private void LoadFontFamilyName()
        {
            fontFamilyNameCmb.SelectedItem = ExternalHelper.FontFamilyName;
        }

        /// <summary>
        /// 加载字体的大小。
        /// </summary>
        private void LoadFontSize()
        {
            fontSizeTxtB.Text = ExternalHelper.FontSize.ToString();
        }

        /// <summary>
        /// 加载字体的 Weight。
        /// </summary>
        private void LoadFontWeight()
        {
            fontWeightCmd.SelectedItem = ExternalHelper.FontWeight.ToString();
        }

        /// <summary>
        /// 加载字体四个方向的 Margins
        /// </summary>
        private void LoadFontMargins()
        {
            leftMarginTxtB.Text = ExternalHelper.LeftMargin.ToString();
            rightMarginTxtB.Text = ExternalHelper.RightMargin.ToString();
            topMarginTxtB.Text = ExternalHelper.TopMargin.ToString();
            bottomMarginTxtB.Text = ExternalHelper.BottomMargin.ToString();
        }

        /// <summary>
        /// fontFamilyNameCmb 控件文本改变触发事件。
        /// </summary>
        /// <param name="sender">FontFamilyNameCmb 对象</param>
        /// <param name="e">文本改变的事件参数</param>
        private void fontFamilyNameCmb_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (fontFamilies.Contains(fontFamilyNameCmb.Text))
            {
                ExternalHelper.FontFamilyName = fontFamilyNameCmb.Text;
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// fontSizeTxtB 控件文本改变触发事件。
        /// </summary>
        /// <param name="sender">fontSizeTxtB 对象</param>
        /// <param name="e">文本改变的事件参数</param>
        private void fontSizeTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (double.TryParse(fontSizeTxtB.Text, out var fontSize) && fontSize > 0)
            {
                ExternalHelper.FontSize = fontSize;
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// fontWeightCmb 控件文本改变触发事件。
        /// </summary>
        /// <param name="sender">fontWeightCmb 对象</param>
        /// <param name="e">文本改变的事件参数</param>
        private void fontWeightCmd_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (fontWeights.Contains(fontWeightCmd.Text))
            {
                ExternalHelper.FontWeight = (FontWeight)new FontWeightConverter().ConvertFromInvariantString(fontWeightCmd.Text);
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// 字体的左边缘距离改变事件。
        /// </summary>
        /// <param name="sender">leftMarginTxtB 对象</param>
        /// <param name="e">文本改变的事件参数</param>
        private void leftMarginTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (double.TryParse(leftMarginTxtB.Text, out var leftMargin))
            {
                ExternalHelper.LeftMargin = leftMargin;
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// 字体的右边缘距离改变事件。
        /// </summary>
        /// <param name="sender">rightMarginTxtB 对象</param>
        /// <param name="e">文本改变的事件参数</param>
        private void rightMarginTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (double.TryParse(rightMarginTxtB.Text, out var rightMargin))
            {
                ExternalHelper.RightMargin = rightMargin;
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// 字体的上边缘距离改变事件
        /// </summary>
        /// <param name="sender">topMarginTxtB 对象</param>
        /// <param name="e">文本改变的事件参数</param>
        private void topMarginTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (double.TryParse(topMarginTxtB.Text, out var topMargin))
            {
                ExternalHelper.TopMargin = topMargin;
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// 字体的下边缘距离改变事件
        /// </summary>
        /// <param name="sender">bottomMarginTxtB 对象</param>
        /// <param name="e">文本改变的事件参数</param>
        private void bottomMarginTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (double.TryParse(bottomMarginTxtB.Text, out var bottomMargin))
            {
                ExternalHelper.BottomMargin = bottomMargin;
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// 字体样式恢复默认样式
        /// </summary>
        /// <param name="sender">resetBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void resetBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ExternalHelper.ResetFontStyles();
            LoadCodeMapConfigurations();
            codeMap?.ForceUpdateCodeMap();
        }
    }
}
