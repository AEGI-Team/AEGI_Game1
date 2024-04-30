

namespace AEGI_Game
{
    public partial class FormOurGame : Form
    {

        private Point position;
        private bool dragging; //переменная, чтобы знать передвигаем ли мы сейчас окно
        public FormOurGame()
        {
            InitializeComponent();

            pictureBox1.MouseDown += MouseClickDown;
            pictureBox1.MouseUp += MouseClickUp;
            pictureBox1.MouseMove += MouseClickMove;

            pictureBox3.MouseDown += MouseClickDown;
            pictureBox3.MouseUp += MouseClickUp;
            pictureBox3.MouseMove += MouseClickMove;
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



        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
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
            int speed = 10;
            pictureBox1.Top += speed;
            pictureBox3.Top += speed;

            if (pictureBox1.Top >= 650)
            {
                pictureBox1.Top = 0;
                pictureBox3.Top = -650;
            }

            player.BringToFront();
        }
        /// <12314654

        private void FormOurGame_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormOurGame_KeyDown(object sender, KeyEventArgs e)
        {
            int speed = 10;
            if((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && (player.Left > 480)) //для ограничения в полосе
            {
                player.Left -= speed; 
            }

            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && (player.Right < 670))
            {
                player.Left += speed;
            }
        }
    }
}
