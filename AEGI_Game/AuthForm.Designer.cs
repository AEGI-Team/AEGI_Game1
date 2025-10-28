namespace AEGI_Game
{
    partial class AuthForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnLang;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblUser = new Label();
            txtUser = new TextBox();
            lblPass = new Label();
            txtPass = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            lblError = new Label();
            btnLang = new Button();
            SuspendLayout();

            
            lblUser.AutoSize = true;
            lblUser.Location = new System.Drawing.Point(24, 28);
            lblUser.Name = "lblUser";
            lblUser.Size = new System.Drawing.Size(46, 20);
            lblUser.Text = "Логин";

            txtUser.Location = new System.Drawing.Point(24, 52);
            txtUser.Name = "txtUser";
            txtUser.Size = new System.Drawing.Size(260, 27);


            lblPass.AutoSize = true;
            lblPass.Location = new System.Drawing.Point(24, 92);
            lblPass.Name = "lblPass";
            lblPass.Size = new System.Drawing.Size(60, 20);
            lblPass.Text = "Пароль";

            txtPass.Location = new System.Drawing.Point(24, 116);
            txtPass.Name = "txtPass";
            txtPass.Size = new System.Drawing.Size(260, 27);
            txtPass.UseSystemPasswordChar = true;

            btnLogin.Location = new System.Drawing.Point(24, 162);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(120, 35);
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;

            btnRegister.Location = new System.Drawing.Point(164, 162);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new System.Drawing.Size(120, 35);
            btnRegister.Text = "Регистрация";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;

            lblError.AutoSize = true;
            lblError.ForeColor = System.Drawing.Color.IndianRed;
            lblError.Location = new System.Drawing.Point(24, 208);
            lblError.Name = "lblError";
            lblError.Size = new System.Drawing.Size(0, 20);

            btnLang.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLang.Location = new System.Drawing.Point(250, 10);
            btnLang.Name = "btnLang";
            btnLang.Size = new System.Drawing.Size(60, 28);
            btnLang.Text = "ENG";
            btnLang.UseVisualStyleBackColor = true;
            btnLang.Click += btnLang_Click;

            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(320, 250);
            Controls.Add(btnLang);
            Controls.Add(lblError);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(txtPass);
            Controls.Add(lblPass);
            Controls.Add(txtUser);
            Controls.Add(lblUser);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход / Регистрация";
            Load += AuthForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
