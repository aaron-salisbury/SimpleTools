using GalaSoft.MvvmLight;
using SimpleTools.App.Base.Extensions;

namespace SimpleTools.App.ViewModels
{
    public class IntroductionViewModel : ViewModelBase
    {
        private string _appDisplayName;

        public string AppDisplayName
        {
            get => _appDisplayName;
            set => Set(ref _appDisplayName, value);
        }

        public IntroductionViewModel()
        {
            AppDisplayName = "AppDisplayName".GetLocalized();
        }
    }
}
