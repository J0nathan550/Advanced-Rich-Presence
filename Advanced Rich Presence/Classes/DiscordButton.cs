namespace Advanced_Rich_Presence.Classes
{
    /// <summary>
    /// Replica of DiscordRPC.Button, with a difference of no exception if something is null.
    /// Problem was that DiscordRPC.Button has Url check for the Button, and if trying to save the string that is not Url
    /// It just throws exception. This class solves issue with allowing saving null urls and etc.
    /// </summary>
    public class DiscordButton
    {
        public string Label { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}