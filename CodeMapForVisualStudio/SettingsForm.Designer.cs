namespace CodeMapForVisualStudio
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.settingsTabC = new System.Windows.Forms.TabControl();
            this.fontStyleSettingsTabP = new System.Windows.Forms.TabPage();
            this.resetBtn = new System.Windows.Forms.Button();
            this.bottomMarginTxtB = new System.Windows.Forms.TextBox();
            this.rightMarginTxtB = new System.Windows.Forms.TextBox();
            this.topMarginTxtB = new System.Windows.Forms.TextBox();
            this.leftMarginTxtB = new System.Windows.Forms.TextBox();
            this.fontWeightCmd = new System.Windows.Forms.ComboBox();
            this.fontSizeTxtB = new System.Windows.Forms.TextBox();
            this.bottomMarginLab = new System.Windows.Forms.Label();
            this.topMarginLab = new System.Windows.Forms.Label();
            this.rightMarginLab = new System.Windows.Forms.Label();
            this.leftMarginLab = new System.Windows.Forms.Label();
            this.fontMarginsLab = new System.Windows.Forms.Label();
            this.fontWeightLab = new System.Windows.Forms.Label();
            this.fontSizeLab = new System.Windows.Forms.Label();
            this.fontFamilyNameCmb = new System.Windows.Forms.ComboBox();
            this.fontFamilyNameLab = new System.Windows.Forms.Label();
            this.colorsTabP = new System.Windows.Forms.TabPage();
            this.selectedColorLab = new System.Windows.Forms.Label();
            this.selectedAlphaTxtB = new System.Windows.Forms.TextBox();
            this.selectedAlphaLab = new System.Windows.Forms.Label();
            this.selectedRedLab = new System.Windows.Forms.Label();
            this.selectedRedTxtB = new System.Windows.Forms.TextBox();
            this.selectedGreenTxtB = new System.Windows.Forms.TextBox();
            this.selectedGreenLab = new System.Windows.Forms.Label();
            this.selectedBlueTxtB = new System.Windows.Forms.TextBox();
            this.selectedBlueLab = new System.Windows.Forms.Label();
            this.selectedColorShownBtn = new System.Windows.Forms.Button();
            this.settingsTabC.SuspendLayout();
            this.fontStyleSettingsTabP.SuspendLayout();
            this.colorsTabP.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabC
            // 
            this.settingsTabC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsTabC.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.settingsTabC.Controls.Add(this.fontStyleSettingsTabP);
            this.settingsTabC.Controls.Add(this.colorsTabP);
            this.settingsTabC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.settingsTabC.ItemSize = new System.Drawing.Size(200, 50);
            this.settingsTabC.Location = new System.Drawing.Point(0, 0);
            this.settingsTabC.Margin = new System.Windows.Forms.Padding(0);
            this.settingsTabC.Multiline = true;
            this.settingsTabC.Name = "settingsTabC";
            this.settingsTabC.SelectedIndex = 0;
            this.settingsTabC.Size = new System.Drawing.Size(779, 545);
            this.settingsTabC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.settingsTabC.TabIndex = 2;
            // 
            // fontStyleSettingsTabP
            // 
            this.fontStyleSettingsTabP.Controls.Add(this.resetBtn);
            this.fontStyleSettingsTabP.Controls.Add(this.bottomMarginTxtB);
            this.fontStyleSettingsTabP.Controls.Add(this.rightMarginTxtB);
            this.fontStyleSettingsTabP.Controls.Add(this.topMarginTxtB);
            this.fontStyleSettingsTabP.Controls.Add(this.leftMarginTxtB);
            this.fontStyleSettingsTabP.Controls.Add(this.fontWeightCmd);
            this.fontStyleSettingsTabP.Controls.Add(this.fontSizeTxtB);
            this.fontStyleSettingsTabP.Controls.Add(this.bottomMarginLab);
            this.fontStyleSettingsTabP.Controls.Add(this.topMarginLab);
            this.fontStyleSettingsTabP.Controls.Add(this.rightMarginLab);
            this.fontStyleSettingsTabP.Controls.Add(this.leftMarginLab);
            this.fontStyleSettingsTabP.Controls.Add(this.fontMarginsLab);
            this.fontStyleSettingsTabP.Controls.Add(this.fontWeightLab);
            this.fontStyleSettingsTabP.Controls.Add(this.fontSizeLab);
            this.fontStyleSettingsTabP.Controls.Add(this.fontFamilyNameCmb);
            this.fontStyleSettingsTabP.Controls.Add(this.fontFamilyNameLab);
            this.fontStyleSettingsTabP.Location = new System.Drawing.Point(4, 54);
            this.fontStyleSettingsTabP.Name = "fontStyleSettingsTabP";
            this.fontStyleSettingsTabP.Padding = new System.Windows.Forms.Padding(3);
            this.fontStyleSettingsTabP.Size = new System.Drawing.Size(771, 487);
            this.fontStyleSettingsTabP.TabIndex = 0;
            this.fontStyleSettingsTabP.Text = "Font Styles";
            this.fontStyleSettingsTabP.UseVisualStyleBackColor = true;
            // 
            // resetBtn
            // 
            this.resetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resetBtn.Location = new System.Drawing.Point(31, 428);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(694, 38);
            this.resetBtn.TabIndex = 15;
            this.resetBtn.Text = "Reset";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // bottomMarginTxtB
            // 
            this.bottomMarginTxtB.Location = new System.Drawing.Point(615, 367);
            this.bottomMarginTxtB.Name = "bottomMarginTxtB";
            this.bottomMarginTxtB.Size = new System.Drawing.Size(110, 35);
            this.bottomMarginTxtB.TabIndex = 14;
            this.bottomMarginTxtB.TextChanged += new System.EventHandler(this.bottomMarginTxtB_TextChanged);
            // 
            // rightMarginTxtB
            // 
            this.rightMarginTxtB.Location = new System.Drawing.Point(615, 318);
            this.rightMarginTxtB.Name = "rightMarginTxtB";
            this.rightMarginTxtB.Size = new System.Drawing.Size(110, 35);
            this.rightMarginTxtB.TabIndex = 13;
            this.rightMarginTxtB.TextChanged += new System.EventHandler(this.rightMarginTxtB_TextChanged);
            // 
            // topMarginTxtB
            // 
            this.topMarginTxtB.Location = new System.Drawing.Point(251, 367);
            this.topMarginTxtB.Name = "topMarginTxtB";
            this.topMarginTxtB.Size = new System.Drawing.Size(110, 35);
            this.topMarginTxtB.TabIndex = 12;
            this.topMarginTxtB.TextChanged += new System.EventHandler(this.topMarginTxtB_TextChanged);
            // 
            // leftMarginTxtB
            // 
            this.leftMarginTxtB.Location = new System.Drawing.Point(251, 318);
            this.leftMarginTxtB.Name = "leftMarginTxtB";
            this.leftMarginTxtB.Size = new System.Drawing.Size(110, 35);
            this.leftMarginTxtB.TabIndex = 11;
            this.leftMarginTxtB.TextChanged += new System.EventHandler(this.leftMarginTxtB_TextChanged);
            // 
            // fontWeightCmd
            // 
            this.fontWeightCmd.FormattingEnabled = true;
            this.fontWeightCmd.Location = new System.Drawing.Point(251, 186);
            this.fontWeightCmd.Name = "fontWeightCmd";
            this.fontWeightCmd.Size = new System.Drawing.Size(474, 32);
            this.fontWeightCmd.TabIndex = 10;
            this.fontWeightCmd.TextChanged += new System.EventHandler(this.fontWeightCmd_TextChanged);
            // 
            // fontSizeTxtB
            // 
            this.fontSizeTxtB.Location = new System.Drawing.Point(251, 112);
            this.fontSizeTxtB.Name = "fontSizeTxtB";
            this.fontSizeTxtB.Size = new System.Drawing.Size(474, 35);
            this.fontSizeTxtB.TabIndex = 9;
            this.fontSizeTxtB.TextChanged += new System.EventHandler(this.fontSizeTxtB_TextChanged);
            // 
            // bottomMarginLab
            // 
            this.bottomMarginLab.AutoSize = true;
            this.bottomMarginLab.Location = new System.Drawing.Point(412, 370);
            this.bottomMarginLab.Name = "bottomMarginLab";
            this.bottomMarginLab.Size = new System.Drawing.Size(192, 24);
            this.bottomMarginLab.TabIndex = 8;
            this.bottomMarginLab.Text = "Bottom Margin:";
            // 
            // topMarginLab
            // 
            this.topMarginLab.AutoSize = true;
            this.topMarginLab.Location = new System.Drawing.Point(68, 370);
            this.topMarginLab.Name = "topMarginLab";
            this.topMarginLab.Size = new System.Drawing.Size(153, 24);
            this.topMarginLab.TabIndex = 7;
            this.topMarginLab.Text = "Top Margin:";
            // 
            // rightMarginLab
            // 
            this.rightMarginLab.AutoSize = true;
            this.rightMarginLab.Location = new System.Drawing.Point(412, 321);
            this.rightMarginLab.Name = "rightMarginLab";
            this.rightMarginLab.Size = new System.Drawing.Size(179, 24);
            this.rightMarginLab.TabIndex = 6;
            this.rightMarginLab.Text = "Right Margin:";
            // 
            // leftMarginLab
            // 
            this.leftMarginLab.AutoSize = true;
            this.leftMarginLab.Location = new System.Drawing.Point(68, 321);
            this.leftMarginLab.Name = "leftMarginLab";
            this.leftMarginLab.Size = new System.Drawing.Size(166, 24);
            this.leftMarginLab.TabIndex = 5;
            this.leftMarginLab.Text = "Left Margin:";
            // 
            // fontMarginsLab
            // 
            this.fontMarginsLab.AutoSize = true;
            this.fontMarginsLab.Location = new System.Drawing.Point(27, 264);
            this.fontMarginsLab.Name = "fontMarginsLab";
            this.fontMarginsLab.Size = new System.Drawing.Size(179, 24);
            this.fontMarginsLab.TabIndex = 4;
            this.fontMarginsLab.Text = "Font Margins:";
            // 
            // fontWeightLab
            // 
            this.fontWeightLab.AutoSize = true;
            this.fontWeightLab.Location = new System.Drawing.Point(27, 189);
            this.fontWeightLab.Name = "fontWeightLab";
            this.fontWeightLab.Size = new System.Drawing.Size(166, 24);
            this.fontWeightLab.TabIndex = 3;
            this.fontWeightLab.Text = "Font Weight:";
            // 
            // fontSizeLab
            // 
            this.fontSizeLab.AutoSize = true;
            this.fontSizeLab.Location = new System.Drawing.Point(27, 115);
            this.fontSizeLab.Name = "fontSizeLab";
            this.fontSizeLab.Size = new System.Drawing.Size(140, 24);
            this.fontSizeLab.TabIndex = 2;
            this.fontSizeLab.Text = "Font Size:";
            // 
            // fontFamilyNameCmb
            // 
            this.fontFamilyNameCmb.FormattingEnabled = true;
            this.fontFamilyNameCmb.Location = new System.Drawing.Point(251, 39);
            this.fontFamilyNameCmb.Name = "fontFamilyNameCmb";
            this.fontFamilyNameCmb.Size = new System.Drawing.Size(474, 32);
            this.fontFamilyNameCmb.TabIndex = 1;
            this.fontFamilyNameCmb.TextChanged += new System.EventHandler(this.fontFamilyNameCmb_TextChanged);
            // 
            // fontFamilyNameLab
            // 
            this.fontFamilyNameLab.AutoSize = true;
            this.fontFamilyNameLab.Location = new System.Drawing.Point(27, 42);
            this.fontFamilyNameLab.Name = "fontFamilyNameLab";
            this.fontFamilyNameLab.Size = new System.Drawing.Size(218, 24);
            this.fontFamilyNameLab.TabIndex = 0;
            this.fontFamilyNameLab.Text = "Font FamilyName:";
            // 
            // colorsTabP
            // 
            this.colorsTabP.Controls.Add(this.selectedColorShownBtn);
            this.colorsTabP.Controls.Add(this.selectedBlueTxtB);
            this.colorsTabP.Controls.Add(this.selectedBlueLab);
            this.colorsTabP.Controls.Add(this.selectedGreenTxtB);
            this.colorsTabP.Controls.Add(this.selectedGreenLab);
            this.colorsTabP.Controls.Add(this.selectedRedTxtB);
            this.colorsTabP.Controls.Add(this.selectedRedLab);
            this.colorsTabP.Controls.Add(this.selectedAlphaLab);
            this.colorsTabP.Controls.Add(this.selectedAlphaTxtB);
            this.colorsTabP.Controls.Add(this.selectedColorLab);
            this.colorsTabP.Location = new System.Drawing.Point(4, 54);
            this.colorsTabP.Name = "colorsTabP";
            this.colorsTabP.Padding = new System.Windows.Forms.Padding(3);
            this.colorsTabP.Size = new System.Drawing.Size(771, 487);
            this.colorsTabP.TabIndex = 1;
            this.colorsTabP.Text = "Colors";
            this.colorsTabP.UseVisualStyleBackColor = true;
            // 
            // selectedColorLab
            // 
            this.selectedColorLab.AutoSize = true;
            this.selectedColorLab.Location = new System.Drawing.Point(39, 33);
            this.selectedColorLab.Name = "selectedColorLab";
            this.selectedColorLab.Size = new System.Drawing.Size(205, 24);
            this.selectedColorLab.TabIndex = 1;
            this.selectedColorLab.Text = "Selected Color:";
            // 
            // selectedAlphaTxtB
            // 
            this.selectedAlphaTxtB.Location = new System.Drawing.Point(124, 71);
            this.selectedAlphaTxtB.Name = "selectedAlphaTxtB";
            this.selectedAlphaTxtB.Size = new System.Drawing.Size(62, 35);
            this.selectedAlphaTxtB.TabIndex = 2;
            this.selectedAlphaTxtB.TextChanged += new System.EventHandler(this.selectedAlphaTxtB_TextChanged);
            // 
            // selectedAlphaLab
            // 
            this.selectedAlphaLab.AutoSize = true;
            this.selectedAlphaLab.Location = new System.Drawing.Point(82, 74);
            this.selectedAlphaLab.Name = "selectedAlphaLab";
            this.selectedAlphaLab.Size = new System.Drawing.Size(36, 24);
            this.selectedAlphaLab.TabIndex = 3;
            this.selectedAlphaLab.Text = "A:";
            // 
            // selectedRedLab
            // 
            this.selectedRedLab.AutoSize = true;
            this.selectedRedLab.Location = new System.Drawing.Point(259, 74);
            this.selectedRedLab.Name = "selectedRedLab";
            this.selectedRedLab.Size = new System.Drawing.Size(36, 24);
            this.selectedRedLab.TabIndex = 4;
            this.selectedRedLab.Text = "R:";
            // 
            // selectedRedTxtB
            // 
            this.selectedRedTxtB.Location = new System.Drawing.Point(301, 71);
            this.selectedRedTxtB.Name = "selectedRedTxtB";
            this.selectedRedTxtB.Size = new System.Drawing.Size(62, 35);
            this.selectedRedTxtB.TabIndex = 5;
            this.selectedRedTxtB.TextChanged += new System.EventHandler(this.selectedRedTxtB_TextChanged);
            // 
            // selectedGreenTxtB
            // 
            this.selectedGreenTxtB.Location = new System.Drawing.Point(473, 71);
            this.selectedGreenTxtB.Name = "selectedGreenTxtB";
            this.selectedGreenTxtB.Size = new System.Drawing.Size(62, 35);
            this.selectedGreenTxtB.TabIndex = 7;
            this.selectedGreenTxtB.TextChanged += new System.EventHandler(this.selectedGreenTxtB_TextChanged);
            // 
            // selectedGreenLab
            // 
            this.selectedGreenLab.AutoSize = true;
            this.selectedGreenLab.Location = new System.Drawing.Point(431, 74);
            this.selectedGreenLab.Name = "selectedGreenLab";
            this.selectedGreenLab.Size = new System.Drawing.Size(36, 24);
            this.selectedGreenLab.TabIndex = 6;
            this.selectedGreenLab.Text = "G:";
            // 
            // selectedBlueTxtB
            // 
            this.selectedBlueTxtB.Location = new System.Drawing.Point(640, 71);
            this.selectedBlueTxtB.Name = "selectedBlueTxtB";
            this.selectedBlueTxtB.Size = new System.Drawing.Size(62, 35);
            this.selectedBlueTxtB.TabIndex = 9;
            this.selectedBlueTxtB.TextChanged += new System.EventHandler(this.selectedBlueTxtB_TextChanged);
            // 
            // selectedBlueLab
            // 
            this.selectedBlueLab.AutoSize = true;
            this.selectedBlueLab.Location = new System.Drawing.Point(598, 74);
            this.selectedBlueLab.Name = "selectedBlueLab";
            this.selectedBlueLab.Size = new System.Drawing.Size(36, 24);
            this.selectedBlueLab.TabIndex = 8;
            this.selectedBlueLab.Text = "B:";
            // 
            // selectedColorShownBtn
            // 
            this.selectedColorShownBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.selectedColorShownBtn.Location = new System.Drawing.Point(250, 30);
            this.selectedColorShownBtn.Name = "selectedColorShownBtn";
            this.selectedColorShownBtn.Size = new System.Drawing.Size(452, 31);
            this.selectedColorShownBtn.TabIndex = 10;
            this.selectedColorShownBtn.UseVisualStyleBackColor = true;
            this.selectedColorShownBtn.Click += new System.EventHandler(this.selectedColorShownBtn_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.settingsTabC);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CodeMap Settings";
            this.settingsTabC.ResumeLayout(false);
            this.fontStyleSettingsTabP.ResumeLayout(false);
            this.fontStyleSettingsTabP.PerformLayout();
            this.colorsTabP.ResumeLayout(false);
            this.colorsTabP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTabC;
        private System.Windows.Forms.TabPage fontStyleSettingsTabP;
        private System.Windows.Forms.Label fontFamilyNameLab;
        private System.Windows.Forms.ComboBox fontFamilyNameCmb;
        private System.Windows.Forms.Label fontSizeLab;
        private System.Windows.Forms.Label fontWeightLab;
        private System.Windows.Forms.Label fontMarginsLab;
        private System.Windows.Forms.Label bottomMarginLab;
        private System.Windows.Forms.Label topMarginLab;
        private System.Windows.Forms.Label rightMarginLab;
        private System.Windows.Forms.Label leftMarginLab;
        private System.Windows.Forms.TextBox fontSizeTxtB;
        private System.Windows.Forms.ComboBox fontWeightCmd;
        private System.Windows.Forms.TextBox bottomMarginTxtB;
        private System.Windows.Forms.TextBox rightMarginTxtB;
        private System.Windows.Forms.TextBox topMarginTxtB;
        private System.Windows.Forms.TextBox leftMarginTxtB;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.TabPage colorsTabP;
        private System.Windows.Forms.Label selectedColorLab;
        private System.Windows.Forms.Label selectedRedLab;
        private System.Windows.Forms.Label selectedAlphaLab;
        private System.Windows.Forms.TextBox selectedAlphaTxtB;
        private System.Windows.Forms.TextBox selectedGreenTxtB;
        private System.Windows.Forms.Label selectedGreenLab;
        private System.Windows.Forms.TextBox selectedRedTxtB;
        private System.Windows.Forms.TextBox selectedBlueTxtB;
        private System.Windows.Forms.Label selectedBlueLab;
        private System.Windows.Forms.Button selectedColorShownBtn;
    }
}