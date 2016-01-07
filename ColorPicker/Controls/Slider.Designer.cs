namespace ColorPicker.Controls
{
    partial class Slider
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.wheel = new System.Windows.Forms.Button();
            this.pSlider = new System.Windows.Forms.PictureBox();
            this.board = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
            this.SuspendLayout();
            // 
            // wheel
            // 
            this.wheel.Location = new System.Drawing.Point(-14, 47);
            this.wheel.Name = "wheel";
            this.wheel.Size = new System.Drawing.Size(15, 14);
            this.wheel.TabIndex = 5;
            this.wheel.Text = "Button1";
            this.wheel.UseVisualStyleBackColor = true;
            // 
            // pSlider
            // 
            this.pSlider.Location = new System.Drawing.Point(18, 0);
            this.pSlider.MinimumSize = new System.Drawing.Size(20, 20);
            this.pSlider.Name = "pSlider";
            this.pSlider.Size = new System.Drawing.Size(20, 20);
            this.pSlider.TabIndex = 3;
            this.pSlider.TabStop = false;
            this.pSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pSlider_MouseDown);
            this.pSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pSlider_MouseUp);
            // 
            // board
            // 
            this.board.BackColor = System.Drawing.Color.Transparent;
            this.board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board.Location = new System.Drawing.Point(0, 0);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(205, 50);
            this.board.TabIndex = 4;
            this.board.TabStop = false;
            // 
            // Slider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wheel);
            this.Controls.Add(this.pSlider);
            this.Controls.Add(this.board);
            this.Name = "Slider";
            this.Size = new System.Drawing.Size(205, 50);
            ((System.ComponentModel.ISupportInitialize)(this.pSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button wheel;
        private System.Windows.Forms.PictureBox pSlider;
        private System.Windows.Forms.PictureBox board;
    }
}
