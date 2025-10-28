
using static AEGI_Game.Localization;
using AEGI_Game.Properties;
using System.Media;
using AEGI_Game.Auth;

namespace AEGI_Game
  
{

    public partial class FormOurGame : Form



    {
        private readonly User _user;
        private readonly Random rng = new Random(); // [Тимур] Добавил единственный генератор, чтобы каждый раз не вызывать рандом.
        private Point position;
        private bool dragging; //переменная, чтобы знать передвигаем ли мы сейчас окно
        private bool lose = false; //проигрыш, делаем машинку неподвижной
        private int countCoins = 0;
        int[] recordArray = new int[0];
        int record = 0;
        private SoundPlayer _soundPlayer;

        int speed = 5; //теперь глобальная для ускорения машинки
        int speedForControl;
        int coinSpeed = 1;
        int playerSpeed = 5;


        public FormOurGame()
        {
            InitializeComponent();

            pictureBox1.Top = 0;
            pictureBox3.Top = -pictureBox1.Height;

            Bitmap road1 = new Bitmap(Properties.Resources.ROAD, pictureBox1.Size);
            Bitmap road2 = new Bitmap(Properties.Resources.ROAD, pictureBox3.Size);


            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox3.SizeMode = PictureBoxSizeMode.Normal;

            pictureBox1.Image = road1;
            pictureBox3.Image = road2;

            SetStyle(ControlStyles.UserPaint // [Тимур] Включил двойную буферизацию формы + 1 раз указал что игрок должен быть поверх, а не каждый тик. Это очень дорого...
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            player.BringToFront();


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
            _soundPlayer = new SoundPlayer(Properties.Resources.music); // [Тимур ]имя ресурса = имя файла без расширения
            _soundPlayer.LoadAsync();  // [Тимур] чтобы не блокировать UI при загрузке
            KeyPreview = true;
        }


        public FormOurGame(User user) : this()
        {
            _user = user;
            labelRecord.Visible = true;
            labelRecord.Text = $"Рекорд: {_user.BestScore}";
            this.Text = $"OurGame — {_user.Username}";
        }

        private static string GetRecordFilePath()
        {
            var dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AEGI_Game");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "result.txt");
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
            int H = pictureBox1.Height;
            pictureBox1.Top += speed;
            pictureBox3.Top += speed;

            int carspeed = 4;
            enemy1.Top += carspeed;
            enemy2.Top += carspeed;
            enemy3.Top += 3 + playerSpeed;
            enemy4.Top += 3 + playerSpeed;
            coin.Top += coinSpeed + playerSpeed;
            coin1.Top += coinSpeed + playerSpeed;
            if (countCoins>0)
            {
                bomb.Top += coinSpeed + playerSpeed;
                bomb1.Top+= coinSpeed + playerSpeed;
            }


            if (pictureBox1.Top >= H)
                pictureBox1.Top = pictureBox3.Top - H;

            if (pictureBox3.Top >= H)
                pictureBox3.Top = pictureBox1.Top - H;

            if (bomb.Top >= 590)
            {
                bomb.Top = -500;
                
                bomb.Left = rng.Next(430, 610);

            }
            if (bomb1.Top >= 630)
            {
                bomb1.Top = -500;
              
                bomb1.Left = rng.Next(185, 362);

            }

            if (coin.Top >= 590)
            {
                coin.Top = -500;
                
                coin.Left = rng.Next(430, 610);

            }
            if (coin1.Top >= 630)
            {
                coin1.Top = -500;
                
                coin1.Left = rng.Next(185, 362);

            }

            if (enemy1.Top >= 650)
                RespawnEnemyNoOverlap(enemy1, enemy2, -300, -130, 430, 610);


            if (enemy2.Top >= 650)
                RespawnEnemyNoOverlap(enemy2, enemy1, -600, -400, 430, 610);

            if (enemy3.Top >= 650)
                RespawnEnemyNoOverlap(enemy3, enemy4, -300, -130, 185, 362);

            if (enemy4.Top >= 650)
                RespawnEnemyNoOverlap(enemy4, enemy3, -600, -400, 185, 362);

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
                if (_user != null)
                {
                    if (countCoins > _user.BestScore)
                    {
                        _user.BestScore = countCoins;
                        UserStore.Save(_user);          
                    }

                    labelRecord.Visible = true;
                    labelRecord.Text = $"Рекорд: {_user.BestScore}";
                }
                else
                {
                    
                    record = FindMax(recordArray);
                    labelRecord.Visible = true;
                    labelRecord.Text = "Рекорд: " + record;
                }
                

                _soundPlayer.Play();

            }

            
            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                countCoins++;
                recordArray = AddElement(recordArray, countCoins);
                labelcoins.Text = "У вас монет:" + countCoins.ToString();
                coin.Top = -500;
                
                coin.Left = rng.Next(430, 610);
            }
            if (player.Bounds.IntersectsWith(bomb.Bounds))
            {
                countCoins--;
                labelcoins.Text = "У вас монет:" + countCoins.ToString();
                bomb.Top = -500;
               
                bomb.Left = rng.Next(430, 610);
            }

            if (player.Bounds.IntersectsWith(bomb1.Bounds))
            {
                countCoins--;
                labelcoins.Text = "У вас монет:" + countCoins.ToString();
                bomb1.Top = -500;
                
                bomb1.Left = rng.Next(430, 610);
            }
            if (enemy1.Bounds.IntersectsWith(bomb.Bounds))
            {
                bomb.Top = -500;
                
                bomb.Left = rng.Next(430, 610);
            }

            if (enemy2.Bounds.IntersectsWith(bomb.Bounds))
            {
                bomb.Top = -500;
                
                bomb.Left = rng.Next(430, 610);
            }

            if (enemy3.Bounds.IntersectsWith(bomb.Bounds))
            {
                bomb.Top = -500;
                
                bomb.Left = rng.Next(430, 610);
            }

            if (enemy4.Bounds.IntersectsWith(bomb.Bounds))
            {
                bomb.Top = -500;
                
                bomb.Left = rng.Next(430, 610);
            }
            if (enemy1.Bounds.IntersectsWith(bomb1.Bounds))
            {
                bomb1.Top = -500;
                
                bomb1.Left = rng.Next(430, 610);
            }

            if (enemy2.Bounds.IntersectsWith(bomb1.Bounds))
            {
                bomb1.Top = -500;
                
                bomb1.Left = rng.Next(430, 610);
            }

            if (enemy3.Bounds.IntersectsWith(bomb1.Bounds))
            {
                bomb1.Top = -500;
                
                bomb1.Left = rng.Next(430, 610);
            }

            if (enemy4.Bounds.IntersectsWith(bomb1.Bounds))
            {
                bomb1.Top = -500;
                
                bomb1.Left = rng.Next(430, 610);
            }
            // Check if enemies intersect with coins
            if (enemy1.Bounds.IntersectsWith(coin.Bounds))
            {
                coin.Top = -500;
                
                coin.Left = rng.Next(430, 610);
            }

            if (enemy2.Bounds.IntersectsWith(coin.Bounds))
            {
                coin.Top = -500;
                
                coin.Left = rng.Next(430, 610);
            }

            if (enemy3.Bounds.IntersectsWith(coin.Bounds))
            {
                coin.Top = -500;
                
                coin.Left = rng.Next(430, 610);
            }

            if (enemy4.Bounds.IntersectsWith(coin.Bounds))
            {
                coin.Top = -500;
                
                coin.Left = rng.Next(430, 610);
            }
            if (player.Bounds.IntersectsWith(coin1.Bounds))
            {
                countCoins++;
                recordArray = AddElement(recordArray, countCoins);
                labelcoins.Text = "У вас монет:" + countCoins.ToString();
                coin1.Top = -500;
                
                coin1.Left = rng.Next(430, 610);
            }

            if (enemy1.Bounds.IntersectsWith(coin.Bounds))
            {
                coin1.Top = -500;
                
                coin1.Left = rng.Next(430, 610);
            }

            if (enemy2.Bounds.IntersectsWith(coin.Bounds))
            {
                coin1.Top = -500;
                
                coin1.Left = rng.Next(430, 610);
            }

            if (enemy3.Bounds.IntersectsWith(coin.Bounds))
            {
                coin1.Top = -500;
                
                coin1.Left = rng.Next(430, 610);
            }

            if (enemy4.Bounds.IntersectsWith(coin.Bounds))
            {
                coin1.Top = -500;
                
                coin1.Left = rng.Next(430, 610);
            }
        }
        /// <12314654

        private int UpdateBestForUser(int currentScore)
        {
            
            if (_user != null)
            {
                if (currentScore > _user.BestScore)
                {
                    _user.BestScore = currentScore;
                    AEGI_Game.Auth.UserStore.Save(_user); // персистим пользователя с новым рекордом
                }
                return _user.BestScore;
            }

            
            return FindMax(recordArray);
        }


        private void ApplyStringsInGame()
        {
            // Заголовок окна (с именем пользователя, если есть)
            if (_user != null)
                Text = $"{T("App_Title")} — {_user.Username}";
            else
                Text = T("App_Title");

            labelLose.Text = T("Game_Lose");
            buttonRestart.Text = T("Game_TryAgain");
            buttonStartplay.Text = T("Game_Start");
            buttonExit.Text = T("Game_Exit");
            labelRecord.Text = T("Game_RecordFmt", _user?.BestScore ?? record);
            labelcoins.Text = T("Game_CoinsFmt", countCoins);
        }

        private void FormOurGame_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            ApplyStringsInGame();
        }

        private void FormOurGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) { return; }
            int speed1 = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && (player.Left > 185)) //для ограничения в полосе
            {
                player.Left -= speed1;
                speedForControl = 15;
            }

            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && (player.Right < 654))
            {
                player.Left += speed1;
                speedForControl = 15;
            }

            else if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && speed <= 15)
            {

                speed += 5;
                playerSpeed += 5;
            }

            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && speed >= 10)
            {

                speed -= 5;
                playerSpeed -= 5;
            }

        }


        private void btnLangGame_Click(object sender, EventArgs e)
        {
            var next = (Localization.CurrentCode == "ru") ? "en" : "ru";
            Localization.Apply(next);
            ApplyStringsInGame();     
        }



        // [Тимур] После проигрыша не возвращалось speed, playerSpeed, coinSpeed к исходным, и на новый игре значения могли съехать. Тут чисто минутка корректности
        private readonly int baseSpeed = 5;
        private readonly int basePlayerSpeed = 5;
        private readonly int baseCoinSpeed = 1;

        private void buttonRestart_Click_1(object sender, EventArgs e)
        {
            _soundPlayer.Stop();
            enemy1.Top = -130;
            enemy2.Top = -400;
            enemy3.Top = -130;
            enemy4.Top = -400;

            speed = baseSpeed;
            playerSpeed = basePlayerSpeed;
            coinSpeed = baseCoinSpeed;

            labelLose.Visible = false;
            buttonRestart.Visible = false;
            timer.Enabled = true;
            lose = false;
            buttonExit.Visible = false;
            labelRecord.Visible = false;

            countCoins = 0;
            labelcoins.Text = "У вас монет: 0";
            RespawnTop(coin, -600, 430, 610);
            RespawnTop(coin1, -700, 185, 362);
        }


        private void buttonStartplay_Click(object sender, EventArgs e)
        {
            buttonStartplay.Visible = false;
            if (buttonStartplay.Visible == false)
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
            if (array.Length == 0) array = new[] { 0 };
            int max = array.Max();

            string filePath = GetRecordFilePath();

            if (File.Exists(filePath))
            {
                var existing = File.ReadAllText(filePath);
                if (int.TryParse(existing, out int val))
                    max = Math.Max(max, val);
            }

            File.WriteAllText(filePath, max.ToString());
            return max;
        }

        private void RespawnTop(Control c, int top, int leftMin, int leftMax)
        {
            c.Top = top;
            c.Left = rng.Next(leftMin, leftMax);
        }


        private void RespawnEnemyNoOverlap(PictureBox who, PictureBox other, // [Тимур] Микро-оптимизации в тике
                                   int topMin, int topMax, int leftMin, int leftMax)
        {
            int newLeft, newTop;
            do
            {
                newLeft = rng.Next(leftMin, leftMax - who.Width);
                newTop = rng.Next(topMin, topMax);
            } while (Math.Abs(newLeft - other.Left) <= who.Width + 3
                  && Math.Abs(newTop - other.Top) <= who.Height + 3);

            who.Left = newLeft;
            who.Top = newTop;
        }
    }
}
