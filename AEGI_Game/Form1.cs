

using AEGI_Game.Properties;
using Microsoft.VisualBasic.ApplicationServices;
using System.Media;

namespace AEGI_Game
  
{
    
    public partial class FormOurGame : Form
    {
        private Point position;
        private bool dragging; //переменная, чтобы знать передвигаем ли мы сейчас окно
        private bool lose = false; //проигрыш, делаем машинку неподвижной
        private int countCoins = 0;
        int[] recordArray=new int[0];
        int record = 0;
        private SoundPlayer _soundPlayer;

        int speed = 5; //теперь глобальная для ускорения машинки


        public FormOurGame()
        {
            InitializeComponent();

            pictureBox1.MouseDown += MouseClickDown;
            pictureBox1.MouseUp += MouseClickUp;
            pictureBox1.MouseMove += MouseClickMove;

            pictureBox3.MouseDown += MouseClickDown;
            pictureBox3.MouseUp += MouseClickUp;
            pictureBox3.MouseMove += MouseClickMove;

            labelLose.Visible = false;
            buttonRestart.Visible = false;
            buttonStartplay.Visible = true;
            buttonExit.Visible = false;
            labelRecord.Visible = false;
            timer.Enabled = false;
            _soundPlayer = new SoundPlayer("music.wav");
            KeyPreview = true;
        }



        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            position.X = e.X;
            position.Y = e.Y;
        }

        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point CurrentPoint = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(CurrentPoint.X - position.X, CurrentPoint.Y - position.Y + pictureBox1.Top);
            }
        }

        private void FormOurGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //int speed = 5;
            pictureBox1.Top += speed;
            pictureBox3.Top += speed;

            int carspeed = 4;
            enemy1.Top += carspeed;//враги для нашей полосы
            enemy2.Top += carspeed;//враги для нашей полосы
            enemy3.Top += carspeed + 2;
            enemy4.Top += carspeed + 2;
            coin.Top += speed + 3;
            coin1.Top += speed + 3;


            if (pictureBox1.Top >= 650)
            {
                pictureBox1.Top = 0;
                pictureBox3.Top = -650;
            }

            if (coin.Top >= 590)
            {
                coin.Top = -55;
                Random rand = new Random();
                coin.Left = rand.Next(430, 610);

            }
            if (coin1.Top >= 630)
            {
                coin1.Top = -55;
                Random rand = new Random();
                coin1.Left = rand.Next(185, 362);

            }

            if (enemy1.Top >= 650)
            {
                enemy1.Top = -130;
                Random rand = new Random();
                int newLeft = rand.Next(430, 610 - 45);
                int newTop = rand.Next(-300, -130);
                bool isPositionValid = false;
                while (!isPositionValid)
                {
                    if ((newLeft >= enemy2.Left - 48 && newLeft <= enemy2.Left + 48) && (newTop >= enemy2.Top - 128 && newTop <= enemy2.Top + 128))
                    {
                        newLeft = rand.Next(430, 610 - 45);
                        newTop = rand.Next(-300, -130);
                    }
                    else
                    {
                        isPositionValid = true;
                    }
                }
                enemy1.Left = newLeft;
                enemy1.Top = newTop;
            }

            if (enemy2.Top >= 650)
            {
                enemy2.Top = -400;
                Random rand = new Random();
                int newLeft = rand.Next(430, 610 - 45);
                int newTop = rand.Next(-600, -400);
                bool isPositionValid = false;
                while (!isPositionValid)
                {
                    if ((newLeft >= enemy1.Left - 48 && newLeft <= enemy1.Left + 48) && (newTop >= enemy1.Top - 128 && newTop <= enemy1.Top + 128))
                    {
                        newLeft = rand.Next(430, 610 - 45);
                        newTop = rand.Next(-600, -400);
                    }
                    else
                    {
                        isPositionValid = true;
                    }
                }
                enemy2.Left = newLeft;
                enemy2.Top = newTop;
            }

            if (enemy3.Top >= 650)
            {
                enemy3.Top = -130;
                Random rand = new Random();
                int newLeft = rand.Next(185, 362 - 45);
                int newTop = rand.Next(-300, -130);
                bool isPositionValid = false;
                while (!isPositionValid)
                {
                    if ((newLeft >= enemy4.Left - 48 && newLeft <= enemy4.Left + 48) && (newTop >= enemy4.Top - 128 && newTop <= enemy4.Top + 128))
                    {
                        newLeft = rand.Next(185, 362 - 45);
                        newTop = rand.Next(-300, -130);
                    }
                    else
                    {
                        isPositionValid = true;
                    }
                }
                enemy3.Left = newLeft;
                enemy3.Top = newTop;
            }

            if (enemy4.Top >= 650)
            {
                enemy4.Top = -400;
                Random rand = new Random();
                int newLeft = rand.Next(185, 362 - 45);
                int newTop = rand.Next(-600, -400);
                bool isPositionValid = false;
                while (!isPositionValid)
                {
                    if ((newLeft >= enemy3.Left - 48 && newLeft <= enemy3.Left + 48) && (newTop >= enemy3.Top - 128 && newTop <= enemy3.Top + 128))
                    {
                        newLeft = rand.Next(185, 362 - 45);
                        newTop = rand.Next(-600, -400);
                    }
                    else
                    {
                        isPositionValid = true;
                    }
                }
                enemy4.Left = newLeft;
                enemy4.Top = newTop;
            }

            if (player.Bounds.IntersectsWith(enemy1.Bounds)
                || player.Bounds.IntersectsWith(enemy2.Bounds)
                || player.Bounds.IntersectsWith(enemy3.Bounds)
                || player.Bounds.IntersectsWith(enemy4.Bounds))
            {
                timer.Enabled = false;
                labelLose.Visible = true;
                buttonRestart.Visible = true;
                buttonExit.Visible = true;
                lose = true;
                record=FindMax(recordArray);
                labelRecord.Text = "Рекорд: " + record.ToString();
                labelRecord.Visible = true;
                _soundPlayer.Play();

            }

            player.BringToFront();
            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                countCoins++;
                recordArray = AddElement(recordArray, countCoins);
                labelcoins.Text = "У вас монет:" + countCoins.ToString();
                coin.Top = -55;
                Random rand = new Random();
                coin.Left = rand.Next(430, 610);
            }
            if (player.Bounds.IntersectsWith(coin1.Bounds))
            {
                countCoins++;
                recordArray = AddElement(recordArray, countCoins);
                labelcoins.Text = "У вас монет:" + countCoins.ToString();
                coin1.Top = -55;
                Random rand = new Random();
                coin1.Left = rand.Next(430, 610);
            }
        }
        /// <12314654

        private void FormOurGame_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
        }

        private void FormOurGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) { return; }
            int speed1 = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && (player.Left > 185)) //для ограничения в полосе
            {
                player.Left -= speed1;
            }

            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && (player.Right < 654))
            {
                player.Left += speed1;
            }

            else if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && speed <= 40 )
            {

                speed += 5;
            }

            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && speed >= 5)
            {

                speed -= 5;
            }

        }

        private void buttonRestart_Click_1(object sender, EventArgs e)
        {
            _soundPlayer.Stop();
            enemy1.Top = -130;
            enemy2.Top = -400;
            enemy3.Top = -130;
            enemy4.Top = -400;
            labelLose.Visible = false;
            buttonRestart.Visible = false;
            timer.Enabled = true;
            lose = false;
            buttonExit.Visible = false;
            labelRecord.Visible = false;

            countCoins = 0;
            labelcoins.Text = "У вас монет: 0";
            coin.Top = -600;
        }

        private void buttonStartplay_Click(object sender, EventArgs e)
        {
            buttonStartplay.Visible = false;
            if (buttonStartplay.Visible==false)
            {
                timer.Enabled = true;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        static int[] AddElement(int[] array, int element)
        {
            int[] newArray = new int[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            newArray[newArray.Length - 1] = element;

            return newArray;
        }
        static int FindMax(int[] array)
        {
            if (array.Length==0) return 0;
            int max = array[0]; 
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }
    }
}
