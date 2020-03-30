using Microsoft.VisualStudio.Shell;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

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

            fontFamilies = System.Drawing.FontFamily.Families.Select(f => f.Name).ToArray();
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
            LoadFocusedMaskBrush();
            LoadPrivateTextColor();
            LoadInternalTextColor();
            LoadProtectedTextColor();
            LoadPublicTextColor();
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
        /// 加载字体四个方向的 Margins。
        /// </summary>
        private void LoadFontMargins()
        {
            leftMarginTxtB.Text = ExternalHelper.LeftMargin.ToString();
            rightMarginTxtB.Text = ExternalHelper.RightMargin.ToString();
            topMarginTxtB.Text = ExternalHelper.TopMargin.ToString();
            bottomMarginTxtB.Text = ExternalHelper.BottomMargin.ToString();
        }

        /// <summary>
        /// 加载被选项目的高亮背景色。
        /// </summary>
        private void LoadFocusedMaskBrush()
        {
            if (ExternalHelper.MaskBrush is SolidColorBrush maskBrush)
            {
                focusedMaskColorAlphaTxtB.Text = maskBrush.Color.A.ToString();
                focusedMaskColorRedTxtB.Text = maskBrush.Color.R.ToString();
                focusedMaskColorGreenTxtB.Text = maskBrush.Color.G.ToString();
                focusedMaskColorBlueTxtB.Text = maskBrush.Color.B.ToString();
                focusedMaskColorShownBtn.BackColor = System.Drawing.Color.FromArgb(255, maskBrush.Color.R, maskBrush.Color.G, maskBrush.Color.B);
            }
        }

        /// <summary>
        /// 加载私有字体的显示颜色。
        /// </summary>
        private void LoadPrivateTextColor()
        {
            if (ExternalHelper.PrivateBrush is SolidColorBrush privateTextBrush)
            {
                privateTextAlphaTxtB.Text = privateTextBrush.Color.A.ToString();
                privateTextRedTxtB.Text = privateTextBrush.Color.R.ToString();
                privateTextGreenTxtB.Text = privateTextBrush.Color.G.ToString();
                privateTextBlueTxtB.Text = privateTextBrush.Color.B.ToString();
                privateTextColorShownBtn.BackColor = System.Drawing.Color.FromArgb(255, privateTextBrush.Color.R, privateTextBrush.Color.G, privateTextBrush.Color.B);
            }
        }

        /// <summary>
        /// 加载内部字体的显示颜色。
        /// </summary>
        private void LoadInternalTextColor()
        {
            if (ExternalHelper.InternalBrush is SolidColorBrush internalTextBrush)
            {
                internalTextAlphaTxtB.Text = internalTextBrush.Color.A.ToString();
                internalTextRedTxtB.Text = internalTextBrush.Color.R.ToString();
                internalTextGreenTxtB.Text = internalTextBrush.Color.G.ToString();
                internalTextBlueTxtB.Text = internalTextBrush.Color.B.ToString();
                internalTextColorShownBtn.BackColor = System.Drawing.Color.FromArgb(255, internalTextBrush.Color.R, internalTextBrush.Color.G, internalTextBrush.Color.B);
            }
        }

        /// <summary>
        /// 加载受保护字体的显示颜色。
        /// </summary>
        private void LoadProtectedTextColor()
        {
            if (ExternalHelper.ProtectedBrush is SolidColorBrush protectedTextBrush)
            {
                protectedTextAlphaTxtB.Text = protectedTextBrush.Color.A.ToString();
                protectedTextRedTxtB.Text = protectedTextBrush.Color.R.ToString();
                protectedTextGreenTxtB.Text = protectedTextBrush.Color.G.ToString();
                protectedTextBlueTxtB.Text = protectedTextBrush.Color.B.ToString();
                protectedTextColorShownBtn.BackColor = System.Drawing.Color.FromArgb(255, protectedTextBrush.Color.R, protectedTextBrush.Color.G, protectedTextBrush.Color.B);
            }
        }

        /// <summary>
        /// 加载公开字体的显示颜色。
        /// </summary>
        private void LoadPublicTextColor()
        {
            if (ExternalHelper.PublicBrush is SolidColorBrush publicTextBrush)
            {
                publicTextAlphaTxtB.Text = publicTextBrush.Color.A.ToString();
                publicTextRedTxtB.Text = publicTextBrush.Color.R.ToString();
                publicTextGreenTxtB.Text = publicTextBrush.Color.G.ToString();
                publicTextBlueTxtB.Text = publicTextBrush.Color.B.ToString();
                publicTextColorShownBtn.BackColor = System.Drawing.Color.FromArgb(255, publicTextBrush.Color.R, publicTextBrush.Color.G, publicTextBrush.Color.B);
            }
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
        /// 字体的上边缘距离改变事件。
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
        /// 字体的下边缘距离改变事件。
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
        /// 字体样式恢复默认样式。
        /// </summary>
        /// <param name="sender">resetFontStylesBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void resetFontStylesBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ExternalHelper.ResetFontStyles();
            LoadCodeMapConfigurations();
            codeMap?.ForceUpdateCodeMap();
        }

        /// <summary>
        /// SelectedColorShownBtn 控件点击事件。
        /// </summary>
        /// <param name="sender">selectedColorShownBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void focusedMaskColorShownBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ExternalHelper.MaskBrush = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
                    LoadFocusedMaskBrush();
                    codeMap?.ForceUpdateCodeMap();
                }
            }
        }

        /// <summary>
        /// selectedAlphaTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">selectedAlphaTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void focusedMaskColorAlphaTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(focusedMaskColorAlphaTxtB.Text, out var alpha) && ExternalHelper.MaskBrush is SolidColorBrush maskBrush && alpha != maskBrush.Color.A)
            {
                ExternalHelper.MaskBrush = new SolidColorBrush(Color.FromArgb(alpha, maskBrush.Color.R, maskBrush.Color.G, maskBrush.Color.B));
                LoadFocusedMaskBrush();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// selectedRedTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">selectedRedTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void focusedMaskColorRedTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(focusedMaskColorRedTxtB.Text, out var red) && ExternalHelper.MaskBrush is SolidColorBrush maskBrush && red != maskBrush.Color.R)
            {
                ExternalHelper.MaskBrush = new SolidColorBrush(Color.FromArgb(maskBrush.Color.A, red, maskBrush.Color.G, maskBrush.Color.B));
                LoadFocusedMaskBrush();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// selectedGreenTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">selectedGreenTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void focusedMaskColorGreenTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(focusedMaskColorGreenTxtB.Text, out var green) && ExternalHelper.MaskBrush is SolidColorBrush maskBrush && green != maskBrush.Color.G)
            {
                ExternalHelper.MaskBrush = new SolidColorBrush(Color.FromArgb(maskBrush.Color.A, maskBrush.Color.R, green, maskBrush.Color.B));
                LoadFocusedMaskBrush();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// selectedBlueTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">selectedBlueTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void focusedMaskColorBlueTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(focusedMaskColorBlueTxtB.Text, out var blue) && ExternalHelper.MaskBrush is SolidColorBrush maskBrush && blue != maskBrush.Color.B)
            {
                ExternalHelper.MaskBrush = new SolidColorBrush(Color.FromArgb(maskBrush.Color.A, maskBrush.Color.R, maskBrush.Color.G, blue));
                LoadFocusedMaskBrush();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// privateTextColorShownBtn 控件点击事件。
        /// </summary>
        /// <param name="sender">privateTextColorShownBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void privateTextColorShownBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ExternalHelper.PrivateBrush = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
                    LoadPrivateTextColor();
                    codeMap?.ForceUpdateCodeMap();
                }
            }
        }

        /// <summary>
        /// privateTextAlphaTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">privateTextAlphaTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void privateTextAlphaTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(privateTextAlphaTxtB.Text, out var alpha) && ExternalHelper.PrivateBrush is SolidColorBrush privateTextBrush && alpha != privateTextBrush.Color.A)
            {
                ExternalHelper.PrivateBrush = new SolidColorBrush(Color.FromArgb(alpha, privateTextBrush.Color.R, privateTextBrush.Color.G, privateTextBrush.Color.B));
                LoadPrivateTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// privateTextRedTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">privateTextRedTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void privateTextRedTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(privateTextRedTxtB.Text, out var red) && ExternalHelper.PrivateBrush is SolidColorBrush privateTextBrush && red != privateTextBrush.Color.R)
            {
                ExternalHelper.PrivateBrush = new SolidColorBrush(Color.FromArgb(privateTextBrush.Color.A, red, privateTextBrush.Color.G, privateTextBrush.Color.B));
                LoadPrivateTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// privateTextGreenTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">privateTextGreenTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void privateTextGreenTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(privateTextGreenTxtB.Text, out var green) && ExternalHelper.PrivateBrush is SolidColorBrush privateTextBrush && green != privateTextBrush.Color.G)
            {
                ExternalHelper.PrivateBrush = new SolidColorBrush(Color.FromArgb(privateTextBrush.Color.A, privateTextBrush.Color.R, green, privateTextBrush.Color.B));
                LoadPrivateTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// privateTextBlueTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">privateTextBlueTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void privateTextBlueTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(privateTextBlueTxtB.Text, out var blue) && ExternalHelper.PrivateBrush is SolidColorBrush privateTextBrush && blue != privateTextBrush.Color.B)
            {
                ExternalHelper.PrivateBrush = new SolidColorBrush(Color.FromArgb(privateTextBrush.Color.A, privateTextBrush.Color.R, privateTextBrush.Color.G, blue));
                LoadPrivateTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// internalTextColorShownBtn 控件点击事件。
        /// </summary>
        /// <param name="sender">internalTextColorShownBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void internalTextColorShownBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ExternalHelper.InternalBrush = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
                    LoadInternalTextColor();
                    codeMap?.ForceUpdateCodeMap();
                }
            }
        }

        /// <summary>
        /// internalTextAlphaTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">internalTextAlphaTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void internalTextAlphaTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(internalTextAlphaTxtB.Text, out var alpha) && ExternalHelper.InternalBrush is SolidColorBrush internalTextBrush && alpha != internalTextBrush.Color.A)
            {
                ExternalHelper.InternalBrush = new SolidColorBrush(Color.FromArgb(alpha, internalTextBrush.Color.R, internalTextBrush.Color.G, internalTextBrush.Color.B));
                LoadInternalTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// internalTextRedTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">internalTextRedTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void internalTextRedTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(internalTextRedTxtB.Text, out var red) && ExternalHelper.InternalBrush is SolidColorBrush internalTextBrush && red != internalTextBrush.Color.R)
            {
                ExternalHelper.InternalBrush = new SolidColorBrush(Color.FromArgb(internalTextBrush.Color.A, red, internalTextBrush.Color.G, internalTextBrush.Color.B));
                LoadInternalTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// internalTextGreenTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">internalTextGreenTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void internalTextGreenTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(internalTextGreenTxtB.Text, out var green) && ExternalHelper.InternalBrush is SolidColorBrush internalTextBrush && green != internalTextBrush.Color.G)
            {
                ExternalHelper.InternalBrush = new SolidColorBrush(Color.FromArgb(internalTextBrush.Color.A, internalTextBrush.Color.R, green, internalTextBrush.Color.B));
                LoadInternalTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// internalTextBlueTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">internalTextBlueTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void internalTextBlueTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(internalTextBlueTxtB.Text, out var blue) && ExternalHelper.InternalBrush is SolidColorBrush internalTextBrush && blue != internalTextBrush.Color.B)
            {
                ExternalHelper.InternalBrush = new SolidColorBrush(Color.FromArgb(internalTextBrush.Color.A, internalTextBrush.Color.R, internalTextBrush.Color.G, blue));
                LoadInternalTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// protectedTextColorShownBtn 控件点击事件。
        /// </summary>
        /// <param name="sender">protectedTextColorShownBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void protectedTextColorShownBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ExternalHelper.ProtectedBrush = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
                    LoadProtectedTextColor();
                    codeMap?.ForceUpdateCodeMap();
                }
            }
        }

        /// <summary>
        /// protectedTextAlphaTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">protectedTextAlphaTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void protectedTextAlphaTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(protectedTextAlphaTxtB.Text, out var alpha) && ExternalHelper.ProtectedBrush is SolidColorBrush protectedTextBrush && alpha != protectedTextBrush.Color.A)
            {
                ExternalHelper.ProtectedBrush = new SolidColorBrush(Color.FromArgb(alpha, protectedTextBrush.Color.R, protectedTextBrush.Color.G, protectedTextBrush.Color.B));
                LoadProtectedTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// protectedTextRedTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">protectedTextRedTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void protectedTextRedTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(protectedTextRedTxtB.Text, out var red) && ExternalHelper.ProtectedBrush is SolidColorBrush protectedTextBrush && red != protectedTextBrush.Color.R)
            {
                ExternalHelper.ProtectedBrush = new SolidColorBrush(Color.FromArgb(protectedTextBrush.Color.A, red, protectedTextBrush.Color.G, protectedTextBrush.Color.B));
                LoadProtectedTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// protectedTextGreenTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">protectedTextGreenTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void protectedTextGreenTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(protectedTextGreenTxtB.Text, out var green) && ExternalHelper.ProtectedBrush is SolidColorBrush protectedTextBrush && green != protectedTextBrush.Color.G)
            {
                ExternalHelper.ProtectedBrush = new SolidColorBrush(Color.FromArgb(protectedTextBrush.Color.A, protectedTextBrush.Color.R, green, protectedTextBrush.Color.B));
                LoadProtectedTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// protectedTextBlueTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">protectedTextBlueTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void protectedTextBlueTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(protectedTextBlueTxtB.Text, out var blue) && ExternalHelper.ProtectedBrush is SolidColorBrush protectedTextBrush && blue != protectedTextBrush.Color.B)
            {
                ExternalHelper.ProtectedBrush = new SolidColorBrush(Color.FromArgb(protectedTextBrush.Color.A, protectedTextBrush.Color.R, protectedTextBrush.Color.G, blue));
                LoadProtectedTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// publicTextColorShownBtn 控件点击事件。
        /// </summary>
        /// <param name="sender">publicTextColorShownBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void publicTextColorShownBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ExternalHelper.PublicBrush = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
                    LoadPublicTextColor();
                    codeMap?.ForceUpdateCodeMap();
                }
            }
        }

        /// <summary>
        /// publicTextAlphaTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">publicTextAlphaTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void publicTextAlphaTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(publicTextAlphaTxtB.Text, out var alpha) && ExternalHelper.PublicBrush is SolidColorBrush publicTextBrush && alpha != publicTextBrush.Color.A)
            {
                ExternalHelper.PublicBrush = new SolidColorBrush(Color.FromArgb(alpha, publicTextBrush.Color.R, publicTextBrush.Color.G, publicTextBrush.Color.B));
                LoadPublicTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// publicTextRedTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">publicTextRedTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void publicTextRedTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(publicTextRedTxtB.Text, out var red) && ExternalHelper.PublicBrush is SolidColorBrush publicTextBrush && red != publicTextBrush.Color.R)
            {
                ExternalHelper.PublicBrush = new SolidColorBrush(Color.FromArgb(publicTextBrush.Color.A, red, publicTextBrush.Color.G, publicTextBrush.Color.B));
                LoadPublicTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// publicTextGreenTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">publicTextGreenTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void publicTextGreenTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(publicTextGreenTxtB.Text, out var green) && ExternalHelper.PublicBrush is SolidColorBrush publicTextBrush && green != publicTextBrush.Color.G)
            {
                ExternalHelper.PublicBrush = new SolidColorBrush(Color.FromArgb(publicTextBrush.Color.A, publicTextBrush.Color.R, green, publicTextBrush.Color.B));
                LoadPublicTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// publicTextBlueTxtB 文本改变事件。
        /// </summary>
        /// <param name="sender">publicTextBlueTxtB 对象</param>
        /// <param name="e">文本改变事件参数</param>
        private void publicTextBlueTxtB_TextChanged(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (byte.TryParse(publicTextBlueTxtB.Text, out var blue) && ExternalHelper.PublicBrush is SolidColorBrush publicTextBrush && blue != publicTextBrush.Color.B)
            {
                ExternalHelper.PublicBrush = new SolidColorBrush(Color.FromArgb(publicTextBrush.Color.A, publicTextBrush.Color.R, publicTextBrush.Color.G, blue));
                LoadPublicTextColor();
                codeMap?.ForceUpdateCodeMap();
            }
        }

        /// <summary>
        /// 文字配色恢复默认样式。
        /// </summary>
        /// <param name="sender">resetColorsBtn 对象</param>
        /// <param name="e">按钮控件点击事件参数</param>
        private void resetColorsBtn_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ExternalHelper.ResetColors();
            LoadCodeMapConfigurations();
            codeMap?.ForceUpdateCodeMap();
        }
    }
}