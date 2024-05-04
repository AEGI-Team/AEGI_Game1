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
            player = new PictureBox();
            enemy1 = new PictureBox();
            enemy2 = new PictureBox();
            enemy3 = new PictureBox();
            enemy4 = new PictureBox();
            labelLose = new Label();
            buttonRestart = new Button();
            coin = new PictureBox();
            labelcoins = new Label();
            coin1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)coin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)coin1).BeginInit();
            SuspendLayout();
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 15;
            timer.Tick += timer_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(844, 652);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(0, -650);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(844, 652);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // player
            // 
            player.Image = Properties.Resources.mycar_fotor_bg_remover_20240430212132;
            player.Location = new Point(524, 485);
            player.Name = "player";
            player.Size = new Size(45, 128);
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.TabIndex = 1;
            player.TabStop = false;
            // 
            // enemy1
            // 
            enemy1.BackColor = Color.Gray;
            enemy1.Image = (Image)resources.GetObject("enemy1.Image");
            enemy1.Location = new Point(598, -130);
            enemy1.Name = "enemy1";
            enemy1.Size = new Size(45, 128);
            enemy1.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy1.TabIndex = 3;
            enemy1.TabStop = false;
            // 
            // enemy2
            // 
            enemy2.BackColor = Color.Gray;
            enemy2.Image = (Image)resources.GetObject("enemy2.Image");
            enemy2.Location = new Point(438, 400);
            enemy2.Name = "enemy2";
            enemy2.Size = new Size(45, 128);
            enemy2.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy2.TabIndex = 4;
            enemy2.TabStop = false;
            // 
            // enemy3
            // 
            enemy3.BackColor = Color.Gray;
            enemy3.Image = (Image)resources.GetObject("enemy3.Image");
            enemy3.Location = new Point(355, 12);
            enemy3.Name = "enemy3";
            enemy3.Size = new Size(45, 128);
            enemy3.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy3.TabIndex = 7;
            enemy3.TabStop = false;
            // 
            // enemy4
            // 
            enemy4.BackColor = Color.Gray;
            enemy4.Image = (Image)resources.GetObject("enemy4.Image");
            enemy4.Location = new Point(273, 397);
            enemy4.Name = "enemy4";
            enemy4.Size = new Size(45, 128);
            enemy4.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy4.TabIndex = 8;
            enemy4.TabStop = false;
            // 
            // labelLose
            // 
            labelLose.AutoSize = true;
            labelLose.BackColor = Color.IndianRed;
            labelLose.Font = new Font("Times New Roman", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelLose.ForeColor = SystemColors.Control;
            labelLose.Location = new Point(244, 236);
            labelLose.Name = "labelLose";
            labelLose.Size = new Size(396, 51);
            labelLose.TabIndex = 9;
            labelLose.Text = "ВЫ ПРОИГРАЛИ!";
            // 
            // buttonRestart
            // 
            buttonRestart.BackColor = Color.IndianRed;
            buttonRestart.FlatStyle = FlatStyle.Flat;
            buttonRestart.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonRestart.ForeColor = SystemColors.Control;
            buttonRestart.Location = new Point(294, 290);
            buttonRestart.Name = "buttonRestart";
            buttonRestart.Size = new Size(297, 55);
            buttonRestart.TabIndex = 10;
            buttonRestart.Text = "Попробовать заново";
            buttonRestart.UseVisualStyleBackColor = false;
            buttonRestart.Click += buttonRestart_Click_1;
            // 
            // coin
            // 
            coin.BackColor = Color.DimGray;
            coin.Image = (Image)resources.GetObject("coin.Image");
            coin.Location = new Point(442, -550);
            coin.Name = "coin";
            coin.Size = new Size(34, 34);
            coin.SizeMode = PictureBoxSizeMode.StretchImage;
            coin.TabIndex = 11;
            coin.TabStop = false;
            // 
            // labelcoins
            // 
            labelcoins.AutoSize = true;
            labelcoins.BackColor = Color.IndianRed;
            labelcoins.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelcoins.ForeColor = SystemColors.Control;
            labelcoins.Location = new Point(663, 615);
            labelcoins.Name = "labelcoins";
            labelcoins.Size = new Size(177, 25);
            labelcoins.TabIndex = 12;
            labelcoins.Text = "У ВАС МОНЕТ: 0";
            // 
            // coin1
            // 
            coin1.BackColor = Color.DimGray;
            coin1.Image = (Image)resources.GetObject("coin1.Image");
            coin1.Location = new Point(198, -600);
            coin1.Name = "coin1";
            coin1.Size = new Size(34, 34);
            coin1.SizeMode = PictureBoxSizeMode.StretchImage;
            coin1.TabIndex = 14;
            coin1.TabStop = false;
            // 
            // FormOurGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(840, 650);
            Controls.Add(coin1);
            Controls.Add(labelcoins);
            Controls.Add(coin);
            Controls.Add(buttonRestart);
            Controls.Add(labelLose);
            Controls.Add(enemy4);
            Controls.Add(enemy3);
            Controls.Add(enemy2);
            Controls.Add(enemy1);
            Controls.Add(pictureBox3);
            Controls.Add(player);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormOurGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OurGame";
            Load += FormOurGame_Load;
            KeyDown += FormOurGame_KeyDown;
            KeyPress += FormOurGame_KeyPress;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy3).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy4).EndInit();
            ((System.ComponentModel.ISupportInitialize)coin).EndInit();
            ((System.ComponentModel.ISupportInitialize)coin1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBoxWithCar;
        private System.Windows.Forms.Timer timer;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox player;
        private PictureBox enemy1;
        private PictureBox enemy2;
        private PictureBox enemy3;
        private PictureBox enemy4;
        private Label labelLose;
        private Button buttonRestart;
        private PictureBox coin;
        private Label labelcoins;
        private PictureBox coin1;
    }
}
