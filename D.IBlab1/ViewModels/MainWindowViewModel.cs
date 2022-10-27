using D.IBlab1.ViewModels.Base;

namespace D.IBlab1.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Главное окно";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
