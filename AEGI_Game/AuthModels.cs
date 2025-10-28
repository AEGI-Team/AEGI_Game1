using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AEGI_Game.Auth
{
    public class User
    {
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = ""; // PBKDF2
        public string Salt { get; set; } = "";
        public int BestScore { get; set; } = 0;
    }

    public static class UserStore
    {
        private static readonly string Dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AEGI_Game", "users");

        public static string GetUserFile(string username)
            => Path.Combine(Dir, $"{username.ToLowerInvariant()}.json");

        public static void EnsureDir() => Directory.CreateDirectory(Dir);

        public static bool Exists(string username) => File.Exists(GetUserFile(username));

        public static void Save(User u)
        {
            EnsureDir();
            File.WriteAllText(GetUserFile(u.Username),
                JsonSerializer.Serialize(u, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static User? Load(string username)
        {
            var path = GetUserFile(username);
            if (!File.Exists(path)) return null;
            return JsonSerializer.Deserialize<User>(File.ReadAllText(path));
        }

        // === PBKDF2 ===
        public static string NewSalt(int bytes = 16)
        {
            var buff = new byte[bytes];
            RandomNumberGenerator.Fill(buff);
            return Convert.ToBase64String(buff);
        }

        public static string Hash(string password, string saltBase64, int iterations = 100_000)
        {
            var salt = Convert.FromBase64String(saltBase64);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }

        public static bool Verify(string password, string salt, string hash)
            => Hash(password, salt) == hash;
    }
}
