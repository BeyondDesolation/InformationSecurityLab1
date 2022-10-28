using D.IBlab1.ViewModels.Base;

namespace D.IBlab1.ViewModels.UserControlsViewModels
{
    internal class WelcomeControlViewModel : ViewModelBase
    {
        #region Свойства
        private string _welcomeText = "Привет";
        /// <summary>Текст приветствия </summary>
        public string WelcomeText
        {
            get => _welcomeText;
            set => Set(ref _welcomeText, value);
        }
        #endregion
    }
}
