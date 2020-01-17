using ServiceBehavior.ViewModels;
using System.Windows;

namespace ServiceBehavior
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
