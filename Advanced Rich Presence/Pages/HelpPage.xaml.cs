using Advanced_Rich_Presence.Classes;
using ModernWpf.Controls;
using System.Diagnostics;
using System.IO;
using System.Media;
using XamlAnimatedGif;

namespace Advanced_Rich_Presence.Pages
{
    public partial class HelpPage : System.Windows.Controls.Page
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        private void CloseHelpPage_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.MainWindowInstance != null)
            {
                Helpers.IsEditing = false;
                if (MainWindow.MainWindowInstance.mainPageView.CurrentSourcePageType == typeof(DiscordRPCStarter)) return;
                MainWindow.MainWindowInstance.mainPageView.Navigate(typeof(DiscordRPCStarter));
            }
        }

        private void KofiLink_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new() { UseShellExecute = true, FileName = "https://ko-fi.com/j0nathan550" };
                Process.Start(processStartInfo);
            }
            catch
            {
                ContentDialog dialog = new()
                {
                    Title = "What a shame!",
                    Content = "You don't have any browser installed on your computer!",
                    CloseButtonText = "Ok",
                };
                dialog.ShowAsync();
            }
        }

        private void DiscordDeveloperLink_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new() { UseShellExecute = true, FileName = "https://discord.com/developers" };
                Process.Start(processStartInfo);
            }
            catch
            {
                ContentDialog dialog = new()
                {
                    Title = "What a shame!",
                    Content = "You don't have any browser installed on your computer!",
                    CloseButtonText = "Ok",
                };
                dialog.ShowAsync();
            }
        }

        private void EmailLink_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new() { UseShellExecute = true, FileName = "mailto:jonathansoderstorm550@gmail.com" };
                Process.Start(processStartInfo);
            }
            catch
            {
                ContentDialog dialog = new()
                {
                    Title = "What a shame!",
                    Content = "You don't have any browser, or mail programs installed on your computer!",
                    CloseButtonText = "Ok",
                };
                dialog.ShowAsync();
            }
        }

        private void YouTubeVideo_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new() { UseShellExecute = true, FileName = "https://youtu.be/3cP8ylotnTI" };
                Process.Start(processStartInfo);
            }
            catch
            {
                ContentDialog dialog = new()
                {
                    Title = "What a shame!",
                    Content = "You don't have any browser installed on your computer!",
                    CloseButtonText = "Ok",
                };
                dialog.ShowAsync();
            }
        }

        private void SansFight_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MainWindow.MainWindowInstance != null)
            {
                Helpers.IsEditing = false;
                if (MainWindow.MainWindowInstance.mainPageView.CurrentSourcePageType == typeof(SansFightPage)) return;
                MainWindow.MainWindowInstance.mainPageView.Navigate(typeof(SansFightPage));
            }
        }

        private void SansFight_Hover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AnimationBehavior.SetSourceUri(sans, new Uri("pack://application:,,,/Images/sans-glowing.gif"));
        }

        private void SansFight_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AnimationBehavior.SetSourceUri(sans, new Uri("pack://application:,,,/Images/sans-idle.gif"));
        }
    }
}