using System;
using System.Windows.Forms;
using AEGI_Game.Auth;
using static AEGI_Game.Localization;

namespace AEGI_Game
{
    public partial class AuthForm : Form
    {
        public User? LoggedInUser { get; private set; }

        public AuthForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            var name = txtUser.Text.Trim();
            var pass = txtPass.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(pass))
            {
                lblError.Text = "Введите логин и пароль.";
                return;
            }

            var u = UserStore.Load(name);
            if (u == null || !UserStore.Verify(pass, u.Salt, u.PasswordHash))
            {
                lblError.Text = "Неверные логин или пароль.";
                return;
            }

            LoggedInUser = u;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ApplyStrings()
        {
            Text = T("Login_Title");
            lblUser.Text = T("Login_Username");
            lblPass.Text = T("Login_Password");
            btnLogin.Text = T("Login_Button");
            btnRegister.Text = T("Register_Button");
            btnLang.Text = T("Lang_Button");
            
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            ApplyStrings();
        }

        private void btnLang_Click(object sender, EventArgs e)
        {
            var next = (Localization.CurrentCode == "ru") ? "en" : "ru";
            Localization.Apply(next);
            ApplyStrings();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            var name = txtUser.Text.Trim();
            var pass = txtPass.Text;

            if (string.IsNullOrWhiteSpace(name) || pass.Length < 4)
            {
                lblError.Text = "Логин и пароль (≥4 символов).";
                return;
            }
            if (UserStore.Exists(name))
            {
                lblError.Text = "Такой пользователь уже есть.";
                return;
            }

            var salt = UserStore.NewSalt();
            var hash = UserStore.Hash(pass, salt);

            var u = new User { Username = name, Salt = salt, PasswordHash = hash, BestScore = 0 };
            UserStore.Save(u);

            LoggedInUser = u;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
