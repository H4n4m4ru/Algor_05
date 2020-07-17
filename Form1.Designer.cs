namespace Algor_05
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lifePoint_03 = new System.Windows.Forms.PictureBox();
            this.lifePoint_02 = new System.Windows.Forms.PictureBox();
            this.lifePoint_01 = new System.Windows.Forms.PictureBox();
            this.ShowScore = new System.Windows.Forms.Label();
            this.ShowLevel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lifePoint_03)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lifePoint_02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lifePoint_01)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Algor_05.Properties.Resources.presss;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1003, 612);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(26)))), ((int)(((byte)(24)))));
            this.panel1.Controls.Add(this.lifePoint_03);
            this.panel1.Controls.Add(this.lifePoint_02);
            this.panel1.Controls.Add(this.lifePoint_01);
            this.panel1.Controls.Add(this.ShowScore);
            this.panel1.Controls.Add(this.ShowLevel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1003, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 612);
            this.panel1.TabIndex = 1;
            // 
            // lifePoint_03
            // 
            this.lifePoint_03.BackgroundImage = global::Algor_05.Properties.Resources.hear3t;
            this.lifePoint_03.Location = new System.Drawing.Point(97, 517);
            this.lifePoint_03.Name = "lifePoint_03";
            this.lifePoint_03.Size = new System.Drawing.Size(32, 32);
            this.lifePoint_03.TabIndex = 5;
            this.lifePoint_03.TabStop = false;
            // 
            // lifePoint_02
            // 
            this.lifePoint_02.BackgroundImage = global::Algor_05.Properties.Resources.hear3t;
            this.lifePoint_02.Location = new System.Drawing.Point(59, 517);
            this.lifePoint_02.Name = "lifePoint_02";
            this.lifePoint_02.Size = new System.Drawing.Size(32, 32);
            this.lifePoint_02.TabIndex = 4;
            this.lifePoint_02.TabStop = false;
            // 
            // lifePoint_01
            // 
            this.lifePoint_01.BackgroundImage = global::Algor_05.Properties.Resources.hear3t;
            this.lifePoint_01.Location = new System.Drawing.Point(21, 517);
            this.lifePoint_01.Name = "lifePoint_01";
            this.lifePoint_01.Size = new System.Drawing.Size(32, 32);
            this.lifePoint_01.TabIndex = 3;
            this.lifePoint_01.TabStop = false;
            // 
            // ShowScore
            // 
            this.ShowScore.AutoSize = true;
            this.ShowScore.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowScore.ForeColor = System.Drawing.Color.GreenYellow;
            this.ShowScore.Location = new System.Drawing.Point(16, 469);
            this.ShowScore.Name = "ShowScore";
            this.ShowScore.Size = new System.Drawing.Size(84, 26);
            this.ShowScore.TabIndex = 2;
            this.ShowScore.Text = "Score : ";
            // 
            // ShowLevel
            // 
            this.ShowLevel.AutoSize = true;
            this.ShowLevel.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowLevel.ForeColor = System.Drawing.Color.GreenYellow;
            this.ShowLevel.Location = new System.Drawing.Point(16, 428);
            this.ShowLevel.Name = "ShowLevel";
            this.ShowLevel.Size = new System.Drawing.Size(79, 26);
            this.ShowLevel.TabIndex = 0;
            this.ShowLevel.Text = "Level : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(52)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(1171, 612);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "竜が我が敵を喰らう！";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lifePoint_03)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lifePoint_02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lifePoint_01)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ShowScore;
        private System.Windows.Forms.Label ShowLevel;
        private System.Windows.Forms.PictureBox lifePoint_03;
        private System.Windows.Forms.PictureBox lifePoint_02;
        private System.Windows.Forms.PictureBox lifePoint_01;



    }
}

