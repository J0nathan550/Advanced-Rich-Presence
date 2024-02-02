using Advanced_Rich_Presence.Classes;
using Windows.UI.Xaml.Controls;

namespace Advanced_Rich_Presence.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            TitleBar.SetTitleBar();
            InitializeComponent();
            pageNavigationViewChanger.SelectedItem = supportNavMenuItem;
        }

        private void PageNavigationViewChanger_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem == rpcStarterNavMenuItem)
            {
                currentPageFrame.Navigate(typeof(DiscordPageStart));
            }
            else if (args.SelectedItem == rpcPresetsNavMenuItem)
            {
                currentPageFrame.Navigate(typeof(DiscordPagePresets));
            }
            else if (args.SelectedItem == howtoUseNavMenuItem)
            {
                currentPageFrame.Navigate(typeof(HowToUsePage));
            }
            else if (args.SelectedItem == supportNavMenuItem)
            {
                currentPageFrame.Navigate(typeof(SupportAuthorPage));
            }
        }
    }
}