using System.IO;

namespace Advanced_Rich_Presence.Classes
{
    /// <summary>
    /// Helpful class to store some constant data that you will use around the program a lot, easier to change just this, then every single line of code.
    /// </summary>
    public static class Helpers
    {
        public static string SavePath { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "J0nathan550", "Advanced Rich Presence");
        public static string SaveFileName { get; set; } = "save.json";
        public static string FullSaveFilePath { get; set; } = Path.Combine(SavePath, SaveFileName);
        public static DiscordStatusModel discordStatusModel { get; set; } = new DiscordStatusModel();
        public static bool IsEditing { get; set; } = false;
    }
}