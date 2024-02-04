using System.Windows;

namespace Advanced_Rich_Presence
{
    public partial class MainWindow : Window
    {
        public static MainWindow? mainWindow { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
        }
    }
}