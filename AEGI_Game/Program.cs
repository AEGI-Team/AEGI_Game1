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

            Localization.Load(); 

            using var auth = new AuthForm();
            if (auth.ShowDialog() != DialogResult.OK || auth.LoggedInUser == null) return;

            Application.Run(new FormOurGame(auth.LoggedInUser));
        }
    }
}
