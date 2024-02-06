using Advanced_Rich_Presence.Classes;
using System.Windows.Controls;

namespace Advanced_Rich_Presence.Pages
{
    public partial class SansFightPage : Page
    {
        public SansFightPage()
        {
            InitializeComponent();
            if (MainWindow.MainWindowInstance != null) MainWindow.MainWindowInstance.Title = "Advanced Rich Presence - Sans Fight - Press Escape to quit.";
        }

        private void Page_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                if (MainWindow.MainWindowInstance != null)
                {
                    MainWindow.MainWindowInstance.Title = "Advanced Rich Presence";
                    Helpers.IsEditing = false;
                    if (MainWindow.MainWindowInstance.mainPageView.CurrentSourcePageType == typeof(HelpPage)) return;
                    MainWindow.MainWindowInstance.mainPageView.Navigate(typeof(HelpPage));
                }
            }
        }
    }
}