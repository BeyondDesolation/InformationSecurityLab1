using D.IBlab1.View.Windows;
using D.IBlab1.ViewModels.WindowsViewModels;
using System.Windows;

namespace D.IBlab1
{
    public partial class App : Application
    {
        public App()
        {
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWM = new MainWindowViewModel();
            var loginWM = new LoginWindowViewModel(mainWM);
            var loginWindow = new LoginWindow()
            {
                DataContext = loginWM
            };
            loginWindow.Show();
        }
    }
}
