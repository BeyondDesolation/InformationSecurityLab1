using D.IBlab1.ViewModels.WindowsViewModels;
using System;
using System.Windows;

namespace D.IBlab1.View.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DataContext as LoginWindowViewModel;
            if (context.CloseWindowAction == null)
                context.CloseWindowAction = new Action(Close);
        }
    }
}
