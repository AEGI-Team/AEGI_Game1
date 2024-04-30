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

            pictureBoxCar.BringToFront();
        }
        /// <12314654

        private void FormOurGame_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
