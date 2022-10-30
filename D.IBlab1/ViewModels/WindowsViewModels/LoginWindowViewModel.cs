using D.IBlab1.Data.Storages;
using D.IBlab1.Infrastructure.Commands;
using D.IBlab1.Models;
using D.IBlab1.ViewModels.Base;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace D.IBlab1.ViewModels.WindowsViewModels
{
    internal class LoginWindowViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly MemoryUserStorage _userStorage;

        public Action? CloseWindowAction;
        
        private string _userName = "";
        /// <summary> Логин пользователя </summary>
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        private bool _isFirstLogin = false;
        /// <summary> Флаг на первый логин, чтобы попросить подтвердить пароль </summary>
        public bool IsFirstLogin
        {
            get => _isFirstLogin;
            set => Set(ref _isFirstLogin, value);
        }

        /// <summary> Комманда запроса на вход </summary>
        public LambdaCommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => true;
        private void OnLoginCommandExecuted(object p) 
        {
            var user = _userStorage?.Get(UserName);

            if (user == null)
            {
                ShowWarning("Пользователь не найден", "Предупреждение");
                IsFirstLogin = false;
                return;
            }

            if (string.IsNullOrEmpty(user.Password) && IsFirstLogin == false)
            {
                IsFirstLogin = true;
                return;
            }

            var passwordBoxes = ((object[])p).Select(p => p as PasswordBox).ToArray();

            if (IsFirstLogin)
            {
                if (passwordBoxes[0].Password.Equals(passwordBoxes[1].Password))
                {
                    // TODO: Захешировать
                    user.Password = passwordBoxes[0].Password;

                    if (_userStorage.Edit(user, user.Login))
                    {
                        ShowInfo("Пароль успешно применен", "Инфо");
                        OpenMainWindow(user);
                    }
                    else
                    {
                        ShowWarning("Если вы видите это сообщение, произошло что-то очень странное", "Ой");
                    }
                }
                else
                {
                    ShowWarning("Пароли не совпадают", "Ошибка");
                }
            }
            else if (passwordBoxes[0].Password.Equals(user.Password)) // TODO: Хеширование
            {
                OpenMainWindow(user);
            }
            else
            {
                ShowWarning("Неверный пароль", "Ошибка");
            }
        }

        /// <summary> Комманда закрытия приложения </summary>
        public LambdaCommand ExitCommand { get; }

        public LoginWindowViewModel(MainWindowViewModel main, MemoryUserStorage userStorage)
        {
            _mainWindowViewModel = main;
            _userStorage = userStorage;

            LoginCommand = new LambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
            ExitCommand = new LambdaCommand(p => App.Current.Shutdown());
        }

        /// <summary> Пустой конструктор для дизайнера </summary>
        public LoginWindowViewModel() : this(null, new MemoryUserStorage()) { }

        private void ShowWarning(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ShowInfo(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenMainWindow(User currentUser)
        {
            _mainWindowViewModel.CurrentUser = currentUser;

            var mainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel
            };
            mainWindow.Show();
            CloseWindowAction();
            CloseWindowAction = null;
        }
    }
}
