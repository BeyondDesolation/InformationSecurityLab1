using D.IBlab1.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.IBlab1.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _title = "";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
