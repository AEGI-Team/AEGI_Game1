namespace AEGI_Game
{
    partial class FormOurGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOurGame));
            timer = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBoxCar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCar).BeginInit();
            SuspendLayout();
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 20;
            timer.Tick += timer_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(840, 650);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(0, -650);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(840, 650);
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // pictureBoxCar
            // 
            pictureBoxCar.Image = (Image)resources.GetObject("pictureBoxCar.Image");
            pictureBoxCar.Location = new Point(548, 428);
            pictureBoxCar.Name = "pictureBoxCar";
            pictureBoxCar.Size = new Size(65, 128);
            pictureBoxCar.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCar.TabIndex = 1;
            pictureBoxCar.TabStop = false;
            // 
            // FormOurGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(840, 650);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBoxCar);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormOurGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OurGame";
            Load += FormOurGame_Load;
            KeyPress += FormOurGame_KeyPress;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCar).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBoxWithCar;
        private System.Windows.Forms.Timer timer;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBoxCar;
    }
}
