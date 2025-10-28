using System.Globalization;

namespace AEGI_Game;

public static class Localization
{
    private const string FileName = "lang.txt";

    public static string CurrentCode { get; private set; } = "ru";

    public static void Load()
    {
        try
        {
            var dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AEGI_Game");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, FileName);
            if (File.Exists(path))
                CurrentCode = File.ReadAllText(path).Trim().ToLowerInvariant() is "en" ? "en" : "ru";

            Apply(CurrentCode);
        }
        catch { Apply("ru"); }
    }

    public static void Apply(string code)
    {
        CurrentCode = (code == "en") ? "en" : "ru";
        var ci = new CultureInfo(CurrentCode);
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;

        try
        {
            var dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AEGI_Game");
            Directory.CreateDirectory(dir);
            File.WriteAllText(Path.Combine(dir, FileName), CurrentCode);
        }
        catch { /* не критично */ }
    }

    public static string T(string key, params object[] args)
    {
        var s = Properties.Strings.ResourceManager.GetString(key) ?? key;
        return (args is { Length: > 0 }) ? string.Format(s, args) : s;
    }
}
