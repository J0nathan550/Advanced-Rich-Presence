using DiscordRPC;

namespace Advanced_Rich_Presence.Classes
{
    /// <summary>
    /// Model that contains all of the needed variables from library by Lachee, and aditionally the name of the template. To store specific sets of RPC statuses.
    /// </summary>
    public class DiscordStatusModel : BaseRichPresence
    {
        /// <summary>
        /// Name of template, used as the unique ID in JSON to store specific statuses that you like to use.
        /// </summary>
        public int TemplateID { get; set; } = 0;
        public string NameOfTemplate { get; set; } = string.Empty;
        public string ApplicationID { get; set; } = string.Empty;
        public DiscordButton[] Buttons { get; set; } =
        [
            new DiscordButton()
            {
                
            },
            new DiscordButton()
            {

            }
        ];
    }
}