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
            this.fontFamilyNameCmb = new System.Windows.Forms.ComboBox();
            this.fontFamilyNameLab = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.settingsTabC.SuspendLayout();
            this.fontStyleSettingsTabP.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabC
            // 
            this.settingsTabC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsTabC.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.settingsTabC.Controls.Add(this.fontStyleSettingsTabP);
            this.settingsTabC.Controls.Add(this.tabPage2);
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 54);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(771, 487);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CodeMap Settings";
            this.settingsTabC.ResumeLayout(false);
            this.fontStyleSettingsTabP.ResumeLayout(false);
            this.fontStyleSettingsTabP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTabC;
        private System.Windows.Forms.TabPage fontStyleSettingsTabP;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label fontFamilyNameLab;
        private System.Windows.Forms.ComboBox fontFamilyNameCmb;
    }
}