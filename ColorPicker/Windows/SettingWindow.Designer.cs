namespace ColorPicker.Windows
{
    partial class SettingWindow
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
            this.lLbl = new System.Windows.Forms.LinkLabel();
            this.cbBase_zoomout = new System.Windows.Forms.ComboBox();
            this.cbSub_zoomout = new System.Windows.Forms.ComboBox();
            this.cbBase_zoomin = new System.Windows.Forms.ComboBox();
            this.cbSub_zoomin = new System.Windows.Forms.ComboBox();
            this.cbBase_hex = new System.Windows.Forms.ComboBox();
            this.cbSub_hex = new System.Windows.Forms.ComboBox();
            this.cbBase_hsb = new System.Windows.Forms.ComboBox();
            this.cbSub_hsb = new System.Windows.Forms.ComboBox();
            this.cbBase_hsl = new System.Windows.Forms.ComboBox();
            this.cbSub_hsl = new System.Windows.Forms.ComboBox();
            this.cbBase_rgb = new System.Windows.Forms.ComboBox();
            this.cbSub_rgb = new System.Windows.Forms.ComboBox();
            this.cbBase_pause = new System.Windows.Forms.ComboBox();
            this.cbSub_pause = new System.Windows.Forms.ComboBox();
            this.chkZoomOut = new System.Windows.Forms.CheckBox();
            this.chkZoomIn = new System.Windows.Forms.CheckBox();
            this.chkHex = new System.Windows.Forms.CheckBox();
            this.chkHsb = new System.Windows.Forms.CheckBox();
            this.chkHsl = new System.Windows.Forms.CheckBox();
            this.chkRgb = new System.Windows.Forms.CheckBox();
            this.chkPause = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lLbl
            // 
            this.lLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lLbl.AutoSize = true;
            this.lLbl.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lLbl.LinkColor = System.Drawing.Color.DimGray;
            this.lLbl.Location = new System.Drawing.Point(15, 233);
            this.lLbl.Name = "lLbl";
            this.lLbl.Size = new System.Drawing.Size(269, 12);
            this.lLbl.TabIndex = 22;
            this.lLbl.TabStop = true;
            this.lLbl.Text = "세개이상 키조합은 세팅파일에서 수정가능합니다";
            this.lLbl.VisitedLinkColor = System.Drawing.Color.DimGray;
            this.lLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLbl_LinkClicked);
            // 
            // cbBase_zoomout
            // 
            this.cbBase_zoomout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase_zoomout.FormattingEnabled = true;
            this.cbBase_zoomout.Location = new System.Drawing.Point(138, 193);
            this.cbBase_zoomout.Name = "cbBase_zoomout";
            this.cbBase_zoomout.Size = new System.Drawing.Size(66, 20);
            this.cbBase_zoomout.TabIndex = 20;
            // 
            // cbSub_zoomout
            // 
            this.cbSub_zoomout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub_zoomout.FormattingEnabled = true;
            this.cbSub_zoomout.Location = new System.Drawing.Point(210, 193);
            this.cbSub_zoomout.Name = "cbSub_zoomout";
            this.cbSub_zoomout.Size = new System.Drawing.Size(70, 20);
            this.cbSub_zoomout.TabIndex = 19;
            // 
            // cbBase_zoomin
            // 
            this.cbBase_zoomin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase_zoomin.FormattingEnabled = true;
            this.cbBase_zoomin.Location = new System.Drawing.Point(138, 167);
            this.cbBase_zoomin.Name = "cbBase_zoomin";
            this.cbBase_zoomin.Size = new System.Drawing.Size(66, 20);
            this.cbBase_zoomin.TabIndex = 18;
            // 
            // cbSub_zoomin
            // 
            this.cbSub_zoomin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub_zoomin.FormattingEnabled = true;
            this.cbSub_zoomin.Location = new System.Drawing.Point(210, 167);
            this.cbSub_zoomin.Name = "cbSub_zoomin";
            this.cbSub_zoomin.Size = new System.Drawing.Size(70, 20);
            this.cbSub_zoomin.TabIndex = 17;
            // 
            // cbBase_hex
            // 
            this.cbBase_hex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase_hex.FormattingEnabled = true;
            this.cbBase_hex.Location = new System.Drawing.Point(138, 141);
            this.cbBase_hex.Name = "cbBase_hex";
            this.cbBase_hex.Size = new System.Drawing.Size(66, 20);
            this.cbBase_hex.TabIndex = 16;
            // 
            // cbSub_hex
            // 
            this.cbSub_hex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub_hex.FormattingEnabled = true;
            this.cbSub_hex.Location = new System.Drawing.Point(210, 141);
            this.cbSub_hex.Name = "cbSub_hex";
            this.cbSub_hex.Size = new System.Drawing.Size(70, 20);
            this.cbSub_hex.TabIndex = 15;
            // 
            // cbBase_hsb
            // 
            this.cbBase_hsb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase_hsb.FormattingEnabled = true;
            this.cbBase_hsb.Location = new System.Drawing.Point(138, 115);
            this.cbBase_hsb.Name = "cbBase_hsb";
            this.cbBase_hsb.Size = new System.Drawing.Size(66, 20);
            this.cbBase_hsb.TabIndex = 14;
            // 
            // cbSub_hsb
            // 
            this.cbSub_hsb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub_hsb.FormattingEnabled = true;
            this.cbSub_hsb.Location = new System.Drawing.Point(210, 115);
            this.cbSub_hsb.Name = "cbSub_hsb";
            this.cbSub_hsb.Size = new System.Drawing.Size(70, 20);
            this.cbSub_hsb.TabIndex = 13;
            // 
            // cbBase_hsl
            // 
            this.cbBase_hsl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase_hsl.FormattingEnabled = true;
            this.cbBase_hsl.Location = new System.Drawing.Point(138, 89);
            this.cbBase_hsl.Name = "cbBase_hsl";
            this.cbBase_hsl.Size = new System.Drawing.Size(66, 20);
            this.cbBase_hsl.TabIndex = 12;
            // 
            // cbSub_hsl
            // 
            this.cbSub_hsl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub_hsl.FormattingEnabled = true;
            this.cbSub_hsl.Location = new System.Drawing.Point(210, 89);
            this.cbSub_hsl.Name = "cbSub_hsl";
            this.cbSub_hsl.Size = new System.Drawing.Size(70, 20);
            this.cbSub_hsl.TabIndex = 11;
            // 
            // cbBase_rgb
            // 
            this.cbBase_rgb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase_rgb.FormattingEnabled = true;
            this.cbBase_rgb.Location = new System.Drawing.Point(138, 63);
            this.cbBase_rgb.Name = "cbBase_rgb";
            this.cbBase_rgb.Size = new System.Drawing.Size(66, 20);
            this.cbBase_rgb.TabIndex = 10;
            // 
            // cbSub_rgb
            // 
            this.cbSub_rgb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub_rgb.FormattingEnabled = true;
            this.cbSub_rgb.Location = new System.Drawing.Point(210, 63);
            this.cbSub_rgb.Name = "cbSub_rgb";
            this.cbSub_rgb.Size = new System.Drawing.Size(70, 20);
            this.cbSub_rgb.TabIndex = 9;
            // 
            // cbBase_pause
            // 
            this.cbBase_pause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase_pause.FormattingEnabled = true;
            this.cbBase_pause.Location = new System.Drawing.Point(138, 37);
            this.cbBase_pause.Name = "cbBase_pause";
            this.cbBase_pause.Size = new System.Drawing.Size(66, 20);
            this.cbBase_pause.TabIndex = 8;
            // 
            // cbSub_pause
            // 
            this.cbSub_pause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub_pause.FormattingEnabled = true;
            this.cbSub_pause.Location = new System.Drawing.Point(210, 37);
            this.cbSub_pause.Name = "cbSub_pause";
            this.cbSub_pause.Size = new System.Drawing.Size(70, 20);
            this.cbSub_pause.TabIndex = 7;
            // 
            // chkZoomOut
            // 
            this.chkZoomOut.AutoSize = true;
            this.chkZoomOut.Location = new System.Drawing.Point(19, 197);
            this.chkZoomOut.Name = "chkZoomOut";
            this.chkZoomOut.Size = new System.Drawing.Size(80, 16);
            this.chkZoomOut.TabIndex = 6;
            this.chkZoomOut.Text = "Zoom Out";
            this.chkZoomOut.UseVisualStyleBackColor = true;
            // 
            // chkZoomIn
            // 
            this.chkZoomIn.AutoSize = true;
            this.chkZoomIn.Location = new System.Drawing.Point(19, 171);
            this.chkZoomIn.Name = "chkZoomIn";
            this.chkZoomIn.Size = new System.Drawing.Size(71, 16);
            this.chkZoomIn.TabIndex = 5;
            this.chkZoomIn.Text = "Zoom In";
            this.chkZoomIn.UseVisualStyleBackColor = true;
            // 
            // chkHex
            // 
            this.chkHex.AutoSize = true;
            this.chkHex.Location = new System.Drawing.Point(19, 145);
            this.chkHex.Name = "chkHex";
            this.chkHex.Size = new System.Drawing.Size(76, 16);
            this.chkHex.TabIndex = 4;
            this.chkHex.Text = "HEX 복사";
            this.chkHex.UseVisualStyleBackColor = true;
            // 
            // chkHsb
            // 
            this.chkHsb.AutoSize = true;
            this.chkHsb.Location = new System.Drawing.Point(19, 119);
            this.chkHsb.Name = "chkHsb";
            this.chkHsb.Size = new System.Drawing.Size(76, 16);
            this.chkHsb.TabIndex = 3;
            this.chkHsb.Text = "HSB 복사";
            this.chkHsb.UseVisualStyleBackColor = true;
            // 
            // chkHsl
            // 
            this.chkHsl.AutoSize = true;
            this.chkHsl.Location = new System.Drawing.Point(19, 93);
            this.chkHsl.Name = "chkHsl";
            this.chkHsl.Size = new System.Drawing.Size(75, 16);
            this.chkHsl.TabIndex = 2;
            this.chkHsl.Text = "HSL 복사";
            this.chkHsl.UseVisualStyleBackColor = true;
            // 
            // chkRgb
            // 
            this.chkRgb.AutoSize = true;
            this.chkRgb.Location = new System.Drawing.Point(19, 67);
            this.chkRgb.Name = "chkRgb";
            this.chkRgb.Size = new System.Drawing.Size(77, 16);
            this.chkRgb.TabIndex = 1;
            this.chkRgb.Text = "RGB 복사";
            this.chkRgb.UseVisualStyleBackColor = true;
            // 
            // chkPause
            // 
            this.chkPause.AutoSize = true;
            this.chkPause.Location = new System.Drawing.Point(19, 41);
            this.chkPause.Name = "chkPause";
            this.chkPause.Size = new System.Drawing.Size(113, 16);
            this.chkPause.TabIndex = 0;
            this.chkPause.Text = "Pause/Resume";
            this.chkPause.UseVisualStyleBackColor = true;
            // 
            // SettingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 257);
            this.Controls.Add(this.lLbl);
            this.Controls.Add(this.cbBase_zoomout);
            this.Controls.Add(this.cbSub_zoomout);
            this.Controls.Add(this.cbBase_zoomin);
            this.Controls.Add(this.cbSub_zoomin);
            this.Controls.Add(this.cbBase_hex);
            this.Controls.Add(this.cbSub_hex);
            this.Controls.Add(this.cbBase_hsb);
            this.Controls.Add(this.cbSub_hsb);
            this.Controls.Add(this.cbBase_hsl);
            this.Controls.Add(this.cbSub_hsl);
            this.Controls.Add(this.cbBase_rgb);
            this.Controls.Add(this.cbSub_rgb);
            this.Controls.Add(this.cbBase_pause);
            this.Controls.Add(this.cbSub_pause);
            this.Controls.Add(this.chkZoomOut);
            this.Controls.Add(this.chkZoomIn);
            this.Controls.Add(this.chkHex);
            this.Controls.Add(this.chkHsb);
            this.Controls.Add(this.chkHsl);
            this.Controls.Add(this.chkRgb);
            this.Controls.Add(this.chkPause);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconVisible = false;
            this.MinimizeBox = false;
            this.Name = "SettingWindow";
            this.ResizeEnable = false;
            this.SettingBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ColorPicker Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPause;
        private System.Windows.Forms.CheckBox chkRgb;
        private System.Windows.Forms.CheckBox chkHsl;
        private System.Windows.Forms.CheckBox chkHsb;
        private System.Windows.Forms.CheckBox chkHex;
        private System.Windows.Forms.CheckBox chkZoomIn;
        private System.Windows.Forms.CheckBox chkZoomOut;
        private System.Windows.Forms.ComboBox cbSub_pause;
        private System.Windows.Forms.ComboBox cbBase_pause;
        private System.Windows.Forms.ComboBox cbBase_rgb;
        private System.Windows.Forms.ComboBox cbSub_rgb;
        private System.Windows.Forms.ComboBox cbBase_hsl;
        private System.Windows.Forms.ComboBox cbSub_hsl;
        private System.Windows.Forms.ComboBox cbBase_hsb;
        private System.Windows.Forms.ComboBox cbSub_hsb;
        private System.Windows.Forms.ComboBox cbBase_hex;
        private System.Windows.Forms.ComboBox cbSub_hex;
        private System.Windows.Forms.ComboBox cbBase_zoomin;
        private System.Windows.Forms.ComboBox cbSub_zoomin;
        private System.Windows.Forms.ComboBox cbBase_zoomout;
        private System.Windows.Forms.ComboBox cbSub_zoomout;
        private System.Windows.Forms.LinkLabel lLbl;
    }
}