using D.IBlab1.ViewModels.WindowsViewModels;
using System.Windows;

namespace D.IBlab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var contex = DataContext as MainWindowViewModel;
            if(contex != null)
                contex.OnWindowLoaded();
        }
    }
}
