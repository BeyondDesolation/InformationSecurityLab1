using D.IBlab1.Models;
using D.IBlab1.Services;
using D.IBlab1.View.Windows;
using D.IBlab1.ViewModels.WindowsViewModels;
using System.Collections.Generic;
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
            //loginWindow.Show();


            var users = new List<User>()
            {
                new User
                {
                    Login = "admin",
                    Role = 1
                },
                new User
                {
                    Login = "dusk73",
                    Role = 1
                }
            };

             DataFileService.EncryptToFile("we", "d1.json", users);
             var res = DataFileService.DecryptFromFile("we", "d1.json");
        }
    }
}
