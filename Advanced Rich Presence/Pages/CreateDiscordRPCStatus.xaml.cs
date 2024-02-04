using System.Windows.Controls;

namespace Advanced_Rich_Presence.Pages
{
    public partial class CreateDiscordRPCStatus : Page
    {
        public CreateDiscordRPCStatus()
        {
            InitializeComponent();
        }

        private void CancelButton_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.mainWindow != null)
            {
                if (MainWindow.mainWindow.mainPageView.CurrentSourcePageType == typeof(DiscordRPCStarter)) return;
                MainWindow.mainWindow.mainPageView.Navigate(typeof(DiscordRPCStarter));
            }
        }

        private void NameOfTemplate_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void ApplicationID_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void StateStatus_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void DetailsStatus_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void LargeImageLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void LargeImageText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void SmallImageLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void SmallImageText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void FirstButtonText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void FirstButtonLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void SecondButtonText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void SecondButtonLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }
    }
}