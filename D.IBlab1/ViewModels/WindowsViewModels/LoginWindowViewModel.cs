using D.IBlab1.Data.Storages;
using D.IBlab1.Infrastructure.Commands;
using D.IBlab1.ViewModels.Base;

namespace D.IBlab1.ViewModels.WindowsViewModels
{
    internal class LoginWindowViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;
        private readonly MemoryUserStorage? _userStorage;

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
        private bool CanLambdaCommandExecute(object p) => true;
        private void OnLambdaCommandExecuted(object p) 
        {
            IsFirstLogin = !IsFirstLogin;
        }

        /// <summary> Комманда закрытия приложения </summary>
        public LambdaCommand ExitCommand { get; }

        public LoginWindowViewModel(MainWindowViewModel main, MemoryUserStorage userStorage)
        {
            _mainWindowViewModel = main;
            _userStorage = userStorage;
            LoginCommand = new LambdaCommand(OnLambdaCommandExecuted, CanLambdaCommandExecute);
            ExitCommand = new LambdaCommand(p => App.Current.Shutdown());
        }
        /// <summary> Пустой конструктор для дизайнера </summary>
        public LoginWindowViewModel() : this(null, null) { }
    }
}
