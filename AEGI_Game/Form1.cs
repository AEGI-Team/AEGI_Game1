

namespace AEGI_Game
{
    public partial class FormOurGame : Form
    {

        private Point position;
        private bool dragging; //переменная, чтобы знать передвигаем ли мы сейчас окно
        private bool lose = false; //проигрыш, делаем машинку неподвижной
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
            int speed = 5;
            pictureBox1.Top += speed;
            pictureBox3.Top += speed;

            int carspeed = 4;
            enemy1.Top += carspeed;//враги для нашей полосы
            enemy2.Top += carspeed;//враги для нашей полосы
            enemy3.Top += carspeed + 2;
            enemy4.Top += carspeed + 2;

            if (pictureBox1.Top >= 650)
            {
                pictureBox1.Top = 0;
                pictureBox3.Top = -650;
            }

            if (enemy1.Top >= 650)
            {
                enemy1.Top = -130;
                Random rand = new Random();
                enemy1.Left = rand.Next(430, 610);
            }
            if (enemy2.Top >= 650)
            {
                enemy2.Top = -400;
                Random rand = new Random();
                enemy2.Left = rand.Next(430, 610);
            }

            if (enemy3.Top >= 650)
            {
                enemy3.Top = -130;
                Random rand = new Random();
                enemy3.Left = rand.Next(185, 362);
            }
            if (enemy4.Top >= 650)
            {
                enemy4.Top = -400;
                Random rand = new Random();
                enemy4.Left = rand.Next(185, 362);
            }

            if (player.Bounds.IntersectsWith(enemy1.Bounds)
                || player.Bounds.IntersectsWith(enemy2.Bounds)
                || player.Bounds.IntersectsWith(enemy3.Bounds)
                || player.Bounds.IntersectsWith(enemy4.Bounds))
            {
                timer.Enabled = false;
                labelLose.Visible = true;
                buttonRestart.Visible = true;
                lose = true;
            }

            player.BringToFront();
        }
        /// <12314654

        private void FormOurGame_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
        }

        private void FormOurGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) { return; }
            int speed = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && (player.Left > 185)) //для ограничения в полосе
            {
                player.Left -= speed;
            }

            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && (player.Right < 654))
            {
                player.Left += speed;
            }
        }

        private void buttonRestart_Click_1(object sender, EventArgs e)
        {
            enemy1.Top = -130;
            enemy2.Top = -400;
            enemy3.Top = -130;
            enemy4.Top = -400;
            labelLose.Visible = false;
            buttonRestart.Visible = false;
            timer.Enabled = true;
            lose = false;
        }
    }
}
