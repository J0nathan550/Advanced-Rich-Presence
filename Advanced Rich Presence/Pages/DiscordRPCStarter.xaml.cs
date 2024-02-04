using DiscordRPC;
using System.Windows.Controls;

namespace Advanced_Rich_Presence.Pages;

public partial class DiscordRPCStarter : Page
{
    //public DiscordRpcClient client;

    public DiscordRPCStarter()
    {
        InitializeComponent();
        //client = new DiscordRpcClient("1056335832518250496");

        ////Connect to the RPC
        //client.Initialize();

        ////Set the rich presence
        ////Call this as many times as you want and anywhere in your code.
        //client.SetPresence(new RichPresence()
        //{
        //    Details = "Example Project",
        //    State = "csharp example",
        //    Assets = new Assets()
        //    {
        //        LargeImageKey = "image_large",
        //        LargeImageText = "Lachee's Discord IPC Library",
        //        SmallImageKey = "image_small"
        //    }
        //});
    }

    private void CreateTemplateButton_Clicked(object sender, System.Windows.RoutedEventArgs e)
    {
        if (MainWindow.mainWindow != null)
        {
            if (MainWindow.mainWindow.mainPageView.CurrentSourcePageType == typeof(CreateDiscordRPCStatus)) return;
            MainWindow.mainWindow.mainPageView.Navigate(typeof(CreateDiscordRPCStatus));
        }
    }
}