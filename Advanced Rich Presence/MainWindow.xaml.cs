using Advanced_Rich_Presence.Pages;
using System.Windows;

namespace Advanced_Rich_Presence
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuClickButton_Click(object sender, RoutedEventArgs e)
        {
            menuView.IsPaneOpen = !menuView.IsPaneOpen;
        }

        private void DiscordRPCMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainPageView.CurrentSourcePageType == typeof(DiscordRPCStarter)) return;
            mainPageView.Navigate(typeof(DiscordRPCStarter));
        }

        private void HelpMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainPageView.CurrentSourcePageType == typeof(HelpPage)) return;
            mainPageView.Navigate(typeof(HelpPage));
        }

        private void AuthorMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainPageView.CurrentSourcePageType == typeof(AuthorPage)) return;
            mainPageView.Navigate(typeof(AuthorPage));
        }
    }
}