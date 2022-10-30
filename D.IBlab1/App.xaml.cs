using D.IBlab1.Data.Storages;
using D.IBlab1.Services;
using D.IBlab1.View.Windows;
using D.IBlab1.ViewModels.WindowsViewModels;
using System.Windows;

namespace D.IBlab1
{
    public partial class App : Application
    {
        private MemoryUserStorage? _userStorage;
        public App() { }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO: Вынести ввод ключа в отдельную форму
            var data = DataFileService.DecryptFromFile("key1", "d.data");
            _userStorage = new MemoryUserStorage(data);

            var mainWM = new MainWindowViewModel(_userStorage);
            var loginWM = new LoginWindowViewModel(mainWM, _userStorage);

            var loginWindow = new LoginWindow()
            {
                DataContext = loginWM
            };
            loginWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (_userStorage == null)
                return;

            DataFileService.EncryptToFile("key1", "d.data", _userStorage.GetAll());
        }
    }
}
