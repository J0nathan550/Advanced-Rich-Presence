using System.Windows;

namespace Advanced_Rich_Presence
{
    public partial class MainWindow : Window
    {
        public static MainWindow? MainWindowInstance { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainWindowInstance = this;
        }
    }
}