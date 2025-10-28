using System;
using System.Windows.Forms;

namespace AEGI_Game
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Загрузка локализации
            Localization.Load();

            // Авторизация пользователя
            using var auth = new AuthForm();
            if (auth.ShowDialog() != DialogResult.OK || auth.LoggedInUser == null) 
                return;

            // Запуск игры с авторизованным пользователем
            Application.Run(new FormOurGame(auth.LoggedInUser));
        }
    }
}
