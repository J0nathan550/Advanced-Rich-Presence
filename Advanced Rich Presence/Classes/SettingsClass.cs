using System.IO;
using System.Text.Json;

namespace Advanced_Rich_Presence.Classes
{
    public class SettingsClass
    {
        public static SettingsClass? Settings { get; set; }
        public List<DiscordStatusModel> DiscordStatusTemplates { get; set; } = [];

        public static void LoadSettings()
        {
            if (!Directory.Exists(Helpers.SavePath)) Directory.CreateDirectory(Helpers.SavePath);    
            if (!File.Exists(Helpers.FullSaveFilePath))
            {
                SaveSettings();
                return;
            }
            try
            {
                string json = File.ReadAllText(Helpers.FullSaveFilePath);
                Settings = JsonSerializer.Deserialize<SettingsClass>(json);    
            }
            catch
            {
                File.Delete(Helpers.FullSaveFilePath);
                SaveSettings();
            }
        }

        public static void SaveSettings()
        {
            Settings ??= new();
            string json = JsonSerializer.Serialize(Settings);
            if (!Directory.Exists(Helpers.SavePath)) Directory.CreateDirectory(Helpers.SavePath);
            File.WriteAllText(Helpers.FullSaveFilePath, json);  
        }
    }
}