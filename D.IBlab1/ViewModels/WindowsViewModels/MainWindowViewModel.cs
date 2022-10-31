using D.IBlab1.Data.Storages;
using D.IBlab1.Infrastructure.Commands;
using D.IBlab1.Models;
using D.IBlab1.View.Windows;
using D.IBlab1.ViewModels.Base;
using D.IBlab1.ViewModels.UserControlsViewModels;
using System.Windows;

namespace D.IBlab1.ViewModels.WindowsViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly MemoryUserStorage _userStorage;
        private UserManagementControlViewModel? _userManagementControlViewModel;

        #region Свойства

        /// <summary> Текущий пользователь </summary>
        public User? CurrentUser { private get; set; }

        public bool IsCurrentUserAdmin => CurrentUser?.Role == 0;


        private string _title = "Главное окно";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }


        private ViewModelBase _mainContent;
        /// <summary>
        /// View model userControl'a, что в данный момент отображается в основной части экрана
        /// </summary>
        public ViewModelBase MainContent
        {
            get => _mainContent;
            set => Set(ref _mainContent, value);
        }

        #endregion

        #region Комманды

        /// <summary> Команда, устанавливающая основным содержимым окна контрол со списком пользователей /// </summary>
        public LambdaCommand OpenUserManagmentCommand { get; }

        /// <summary> Команда, открывающая окно смены пароля, 
        /// и в случае подтверждения смены сохраняет новый пароль в хранилище /// </summary>
        public LambdaCommand ChangePasswordCommand { get; }

        private bool CanChangePasswordCommandExecute(object p) => CurrentUser != null;

        private void OnChangePasswordCommandExecuted(object p)
        {
            var changePasswordWindow = new ChangePasswordWindow(CurrentUser);
            if(changePasswordWindow.ShowDialog().Value == true)
            {
                if(_userStorage.Edit(changePasswordWindow.User, CurrentUser.Login))
                {
                    ShowInfo("Пароль успешно изменен");
                }
                else
                {
                    ShowWarning("Что-то пошло не так при изменении пользователя в хранилище");
                }
            }
        }

        #endregion

        public MainWindowViewModel(MemoryUserStorage userStorage)
        {
            _userStorage = userStorage;
            _mainContent = new WelcomeControlViewModel();
            _userManagementControlViewModel = new UserManagementControlViewModel(_userStorage);

            OpenUserManagmentCommand = new LambdaCommand(p => MainContent = _userManagementControlViewModel, p => IsCurrentUserAdmin);
            ChangePasswordCommand = new LambdaCommand(OnChangePasswordCommandExecuted, CanChangePasswordCommandExecute);
        }

        /// <summary> Пустой конструктор для дизайнера </summary>
        public MainWindowViewModel() : this(new MemoryUserStorage()) { }

        private void ShowInfo(string message, string caption = "Успех")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ShowWarning(string message, string caption = "Предупреждение")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
