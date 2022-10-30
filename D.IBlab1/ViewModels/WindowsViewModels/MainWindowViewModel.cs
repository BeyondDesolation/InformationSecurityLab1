using D.IBlab1.Data.Storages;
using D.IBlab1.ViewModels.Base;
using D.IBlab1.ViewModels.UserControlsViewModels;

namespace D.IBlab1.ViewModels.WindowsViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly MemoryUserStorage _userStorage; 

        #region Свойства
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

        public MainWindowViewModel(MemoryUserStorage userStorage)
        {
            _userStorage = userStorage;
            _mainContent = new WelcomeControlViewModel();
        }
    }
}
