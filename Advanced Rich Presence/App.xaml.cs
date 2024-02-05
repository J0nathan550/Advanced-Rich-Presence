using ModernWpf.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Advanced_Rich_Presence
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private async void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Prevent default unhandled exception processing
            e.Handled = true;

            TextBox messageTextBox = new()
            {
                IsReadOnly = true,
                Foreground = new SolidColorBrush(Colors.Red),
                Text = $"Unhandled exception occurred: {e.Exception}\n{e.Exception.StackTrace}"
            };

            // Display the exception message in a message box
            ContentDialog exceptionDialog = new()
            {
                Title = "Error occured in the program! Please copy and report to developer!",
                Content = messageTextBox,
                UseLayoutRounding = true,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,   
                PrimaryButtonText = "Ok"
            };
            await exceptionDialog.ShowAsync();
        }
    }
}