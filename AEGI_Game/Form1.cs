

namespace AEGI_Game
{
    public partial class FormOurGame : Form
    {
        public FormOurGame()
        {
            InitializeComponent();
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
            if((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && (player.Left > 480)) //��� ����������� � ������
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