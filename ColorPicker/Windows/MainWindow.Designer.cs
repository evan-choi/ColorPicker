namespace ColorPicker.Windows
{
    partial class MainWindow
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.viewBox = new System.Windows.Forms.PictureBox();
            this.colorBox = new System.Windows.Forms.PictureBox();
            this.zoomSlider = new ColorPicker.Controls.Slider();
            this.lbl_RGB = new ColorPicker.Controls.SelectableLabel();
            this.lbl_HSL = new ColorPicker.Controls.SelectableLabel();
            this.lbl_HSV = new ColorPicker.Controls.SelectableLabel();
            this.lbl_HTML = new ColorPicker.Controls.SelectableLabel();
            this.ldcPlate = new ColorPicker.Controls.LDColorPlate();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.chkSemi = new System.Windows.Forms.CheckBox();
            this.recordView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.extender = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.viewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.extender)).BeginInit();
            this.SuspendLayout();
            // 
            // viewBox
            // 
            this.viewBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.viewBox.Location = new System.Drawing.Point(12, 44);
            this.viewBox.Name = "viewBox";
            this.viewBox.Size = new System.Drawing.Size(100, 100);
            this.viewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.viewBox.TabIndex = 1;
            this.viewBox.TabStop = false;
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.colorBox.Location = new System.Drawing.Point(120, 44);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(36, 36);
            this.colorBox.TabIndex = 2;
            this.colorBox.TabStop = false;
            // 
            // zoomSlider
            // 
            this.zoomSlider.ControlStyle = ColorPicker.Controls.Slider.ControlStyles.Blue;
            this.zoomSlider.DropShadow = false;
            this.zoomSlider.Location = new System.Drawing.Point(12, 150);
            this.zoomSlider.Maximum = 40;
            this.zoomSlider.Minimum = 1;
            this.zoomSlider.Name = "zoomSlider";
            this.zoomSlider.Size = new System.Drawing.Size(100, 20);
            this.zoomSlider.SliderStyle = ColorPicker.Controls.Slider.SliderStyles.Rectangle;
            this.zoomSlider.SlideStyle = ColorPicker.Controls.Slider.TrackBarStyles.Horizontal;
            this.zoomSlider.TabIndex = 5;
            this.zoomSlider.Value = 1;
            this.zoomSlider.WheelChange = 1;
            // 
            // lbl_RGB
            // 
            this.lbl_RGB.BackColor = System.Drawing.Color.White;
            this.lbl_RGB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_RGB.Location = new System.Drawing.Point(123, 114);
            this.lbl_RGB.Name = "lbl_RGB";
            this.lbl_RGB.ReadOnly = true;
            this.lbl_RGB.Size = new System.Drawing.Size(129, 14);
            this.lbl_RGB.TabIndex = 13;
            this.lbl_RGB.TabStop = false;
            this.lbl_RGB.Text = "RGB (255, 255, 255)";
            // 
            // lbl_HSL
            // 
            this.lbl_HSL.BackColor = System.Drawing.Color.White;
            this.lbl_HSL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_HSL.Location = new System.Drawing.Point(123, 136);
            this.lbl_HSL.Name = "lbl_HSL";
            this.lbl_HSL.ReadOnly = true;
            this.lbl_HSL.Size = new System.Drawing.Size(129, 14);
            this.lbl_HSL.TabIndex = 14;
            this.lbl_HSL.TabStop = false;
            this.lbl_HSL.Text = "HSL (360, 100%, 100%)";
            // 
            // lbl_HSV
            // 
            this.lbl_HSV.BackColor = System.Drawing.Color.White;
            this.lbl_HSV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_HSV.Location = new System.Drawing.Point(123, 158);
            this.lbl_HSV.Name = "lbl_HSV";
            this.lbl_HSV.ReadOnly = true;
            this.lbl_HSV.Size = new System.Drawing.Size(129, 14);
            this.lbl_HSV.TabIndex = 16;
            this.lbl_HSV.TabStop = false;
            this.lbl_HSV.Text = "HSV (360, 100%, 100%)";
            // 
            // lbl_HTML
            // 
            this.lbl_HTML.BackColor = System.Drawing.Color.White;
            this.lbl_HTML.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_HTML.Location = new System.Drawing.Point(123, 92);
            this.lbl_HTML.Name = "lbl_HTML";
            this.lbl_HTML.ReadOnly = true;
            this.lbl_HTML.Size = new System.Drawing.Size(75, 14);
            this.lbl_HTML.TabIndex = 17;
            this.lbl_HTML.TabStop = false;
            this.lbl_HTML.Text = "#000000";
            // 
            // ldcPlate
            // 
            this.ldcPlate.BackColor = System.Drawing.Color.Red;
            this.ldcPlate.BaseColor = System.Drawing.Color.White;
            this.ldcPlate.Location = new System.Drawing.Point(258, 44);
            this.ldcPlate.Name = "ldcPlate";
            this.ldcPlate.SelectedIndex = -1;
            this.ldcPlate.Size = new System.Drawing.Size(30, 154);
            this.ldcPlate.TabIndex = 18;
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(12, 182);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(48, 16);
            this.chkGrid.TabIndex = 19;
            this.chkGrid.Text = "격자";
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // chkSemi
            // 
            this.chkSemi.AutoSize = true;
            this.chkSemi.Location = new System.Drawing.Point(66, 182);
            this.chkSemi.Name = "chkSemi";
            this.chkSemi.Size = new System.Drawing.Size(72, 16);
            this.chkSemi.TabIndex = 20;
            this.chkSemi.Text = "미세조정";
            this.chkSemi.UseVisualStyleBackColor = true;
            this.chkSemi.CheckedChanged += new System.EventHandler(this.chkSemi_CheckedChanged);
            // 
            // recordView
            // 
            this.recordView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recordView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.recordView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.recordView.FullRowSelect = true;
            this.recordView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.recordView.Location = new System.Drawing.Point(12, 220);
            this.recordView.Name = "recordView";
            this.recordView.Size = new System.Drawing.Size(276, 163);
            this.recordView.TabIndex = 21;
            this.recordView.UseCompatibleStateImageBehavior = false;
            this.recordView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 170;
            // 
            // extender
            // 
            this.extender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.extender.Image = global::ColorPicker.Properties.Resources.arrow_down;
            this.extender.Location = new System.Drawing.Point(135, 389);
            this.extender.Name = "extender";
            this.extender.Size = new System.Drawing.Size(30, 7);
            this.extender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.extender.TabIndex = 22;
            this.extender.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.Controls.Add(this.extender);
            this.Controls.Add(this.recordView);
            this.Controls.Add(this.chkSemi);
            this.Controls.Add(this.chkGrid);
            this.Controls.Add(this.ldcPlate);
            this.Controls.Add(this.lbl_HTML);
            this.Controls.Add(this.lbl_HSV);
            this.Controls.Add(this.lbl_HSL);
            this.Controls.Add(this.lbl_RGB);
            this.Controls.Add(this.zoomSlider);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.viewBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IconVisible = false;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 400);
            this.MinimumSize = new System.Drawing.Size(300, 210);
            this.Name = "MainWindow";
            this.ResizeEnable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ColorPicker";
            ((System.ComponentModel.ISupportInitialize)(this.viewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.extender)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox viewBox;
        private System.Windows.Forms.PictureBox colorBox;
        private Controls.Slider zoomSlider;
        private Controls.SelectableLabel lbl_RGB;
        private Controls.SelectableLabel lbl_HSL;
        private Controls.SelectableLabel lbl_HSV;
        private Controls.SelectableLabel lbl_HTML;
        private Controls.LDColorPlate ldcPlate;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.CheckBox chkSemi;
        private System.Windows.Forms.ListView recordView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.PictureBox extender;
    }
}

