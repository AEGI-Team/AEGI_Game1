
using static AEGI_Game.Localization;
using AEGI_Game.Properties;
using System.Media;
using AEGI_Game.Auth;

namespace AEGI_Game
{
    public partial class FormOurGame : Form
    {
        private readonly User _user;
        private readonly Random rng = new Random(); // [Тимур] единственный генератор
        private Point position;
        private bool dragging;
        private GameState _state = GameState.Menu;
        private int countCoins = 0;
        int[] recordArray = new int[0];
        int record = 0;
        private SoundPlayer _soundPlayer;

        int speed = 5;
        int speedForControl;
        int coinSpeed = 1;
        int playerSpeed = 5;

        private readonly int baseSpeed = 5;
        private readonly int basePlayerSpeed = 5;
        private readonly int baseCoinSpeed = 1;

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

            SetStyle(ControlStyles.UserPaint
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
            labelPause.Visible = false;
            buttonResume.Visible = false;
            buttonPause.Visible = false;

            _soundPlayer = new SoundPlayer(Properties.Resources.music);
            _soundPlayer.LoadAsync();

            KeyPreview = true;
        }

        public FormOurGame(User user) : this()
        {
            _user = user;
            labelRecord.Visible = true;
            labelRecord.Text = T("Game_RecordFmt", _user.BestScore);
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
            if (_state != GameState.Playing) return;

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
            if (countCoins > 0)
            {
                bomb.Top += coinSpeed + playerSpeed;
                bomb1.Top += coinSpeed + playerSpeed;
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
                _soundPlayer.Stop();
                SetGameState(GameState.Lost);

                if (_user != null)
                {
                    if (countCoins > _user.BestScore)
                    {
                        _user.BestScore = countCoins;
                        UserStore.Save(_user);
                    }

                    labelRecord.Visible = true;
                    labelRecord.Text = T("Game_RecordFmt", _user.BestScore);
                }
                else
                {
                    record = FindMax(recordArray);
                    labelRecord.Visible = true;
                    labelRecord.Text = T("Game_RecordFmt", record);
                }

                _soundPlayer.Play();
                return;
            }

            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                countCoins++;
                recordArray = AddElement(recordArray, countCoins);
                labelcoins.Text = T("Game_CoinsFmt", countCoins);
                RespawnTop(coin, -500, 430, 610);
            }
            if (player.Bounds.IntersectsWith(coin1.Bounds))
            {
                countCoins++;
                recordArray = AddElement(recordArray, countCoins);
                labelcoins.Text = T("Game_CoinsFmt", countCoins);
                RespawnTop(coin1, -500, 185, 362);
            }

            if (player.Bounds.IntersectsWith(bomb.Bounds))
                RespawnTop(bomb, -500, 430, 610);
            if (player.Bounds.IntersectsWith(bomb1.Bounds))
                RespawnTop(bomb1, -500, 185, 362);

            // Check if enemies intersect with coins/bombs
            if (enemy1.Bounds.IntersectsWith(bomb.Bounds)) RespawnTop(bomb, -500, 430, 610);
            if (enemy2.Bounds.IntersectsWith(bomb.Bounds)) RespawnTop(bomb, -500, 430, 610);
            if (enemy3.Bounds.IntersectsWith(bomb.Bounds)) RespawnTop(bomb, -500, 430, 610);
            if (enemy4.Bounds.IntersectsWith(bomb.Bounds)) RespawnTop(bomb, -500, 430, 610);

            if (enemy1.Bounds.IntersectsWith(bomb1.Bounds)) RespawnTop(bomb1, -500, 185, 362);
            if (enemy2.Bounds.IntersectsWith(bomb1.Bounds)) RespawnTop(bomb1, -500, 185, 362);
            if (enemy3.Bounds.IntersectsWith(bomb1.Bounds)) RespawnTop(bomb1, -500, 185, 362);
            if (enemy4.Bounds.IntersectsWith(bomb1.Bounds)) RespawnTop(bomb1, -500, 185, 362);

            if (enemy1.Bounds.IntersectsWith(coin.Bounds)) RespawnTop(coin, -500, 430, 610);
            if (enemy2.Bounds.IntersectsWith(coin.Bounds)) RespawnTop(coin, -500, 430, 610);
            if (enemy3.Bounds.IntersectsWith(coin.Bounds)) RespawnTop(coin, -500, 430, 610);
            if (enemy4.Bounds.IntersectsWith(coin.Bounds)) RespawnTop(coin, -500, 430, 610);

            if (enemy1.Bounds.IntersectsWith(coin1.Bounds)) RespawnTop(coin1, -500, 185, 362);
            if (enemy2.Bounds.IntersectsWith(coin1.Bounds)) RespawnTop(coin1, -500, 185, 362);
            if (enemy3.Bounds.IntersectsWith(coin1.Bounds)) RespawnTop(coin1, -500, 185, 362);
            if (enemy4.Bounds.IntersectsWith(coin1.Bounds)) RespawnTop(coin1, -500, 185, 362);
        }

        private int UpdateBestForUser(int currentScore)
        {
            if (_user != null)
            {
                if (currentScore > _user.BestScore)
                {
                    _user.BestScore = currentScore;
                    AEGI_Game.Auth.UserStore.Save(_user);
                }
                return _user.BestScore;
            }
            return FindMax(recordArray);
        }

        private void ApplyStringsInGame()
        {
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
            labelPause.Text = T("Game_Pause");
            buttonResume.Text = T("Game_Resume");
            buttonPause.Text = T("Game_PauseButton");
        }

        private void FormOurGame_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            ApplyStringsInGame();
        }

        private void FormOurGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (_state != GameState.Playing) return;
            int speed1 = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && (player.Left > 185))
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

           private void buttonRestart_Click_1(object sender, EventArgs e)
        {
            // Сброс состояния игры
            countCoins = 0;
            labelcoins.Text = T("Game_CoinsFmt", countCoins);

            // Сброс позиции игрока
            player.Left = 524;
            player.Top = 485;

            // Сброс позиции врагов
            enemy1.Top = -130;
            enemy2.Top = 400;
            enemy3.Top = 12;
            enemy4.Top = 400;

            // Сброс позиции монет и бомб
            coin.Top = -550;
            coin.Left = rng.Next(430, 610);
            coin1.Top = -600;
            coin1.Left = rng.Next(185, 362);
            bomb.Top = -500;
            bomb.Left = rng.Next(430, 610);
            bomb1.Top = -500;
            bomb1.Left = rng.Next(185, 362);

            // Сброс скорости
            speed = baseSpeed;
            playerSpeed = basePlayerSpeed;
            coinSpeed = baseCoinSpeed;

            SetGameState(GameState.Menu);

            // Остановить музыку проигрыша
            _soundPlayer.Stop();
        }

        private void buttonStartplay_Click(object sender, EventArgs e)
        {
            StartGameplay();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (_state != GameState.Playing) return;
            _soundPlayer.Stop();
            SetGameState(GameState.Paused);
        }

        private void buttonResume_Click(object sender, EventArgs e)
        {
            if (_state != GameState.Paused) return;
            SetGameState(GameState.Playing);
            _soundPlayer.PlayLooping();
        }

        // Вспомогательные методы
        private void SetGameState(GameState newState)
        {
            _state = newState;
            timer.Enabled = newState == GameState.Playing;

            var isMenu = newState == GameState.Menu;
            var isPaused = newState == GameState.Paused;
            var isLost = newState == GameState.Lost;

            labelLose.Visible = isLost;
            buttonRestart.Visible = isLost;
            buttonExit.Visible = isLost;

            buttonStartplay.Visible = isMenu;
            buttonPause.Visible = newState == GameState.Playing;
            buttonResume.Visible = isPaused;
            labelPause.Visible = isPaused;
        }

        private void StartGameplay()
        {
            buttonExit.Visible = false;
            labelLose.Visible = false;
            buttonRestart.Visible = false;
            buttonStartplay.Visible = false;

            countCoins = 0;
            labelcoins.Text = T("Game_CoinsFmt", countCoins);

            SetGameState(GameState.Playing);
            _soundPlayer.PlayLooping();
        }

        private void RespawnTop(PictureBox obj, int top, int leftMin, int leftMax)
        {
            obj.Top = top;
            obj.Left = rng.Next(leftMin, leftMax);
        }

        private void RespawnEnemyNoOverlap(PictureBox enemy, PictureBox otherEnemy, int topMin, int topMax, int leftMin, int leftMax)
        {
            enemy.Top = rng.Next(topMin, topMax);
            int newLeft;
            do
            {
                newLeft = rng.Next(leftMin, leftMax);
            } while (Math.Abs(newLeft - otherEnemy.Left) < enemy.Width);
            enemy.Left = newLeft;
        }

        private int[] AddElement(int[] array, int element)
        {
            int[] newArray = new int[array.Length + 1];
            Array.Copy(array, newArray, array.Length);
            newArray[^1] = element;
            return newArray;
        }

        private int FindMax(int[] array)
        {
            if (array.Length == 0) return 0;
            int max = array[0];
            foreach (var val in array)
            {
                if (val > max) max = val;
            }
            return max;
        }
    }
}

