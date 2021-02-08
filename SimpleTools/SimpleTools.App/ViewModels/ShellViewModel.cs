using SimpleTools.App.Base.Extensions;
using SimpleTools.Core.Base;
using Windows.UI.Xaml;

namespace SimpleTools.App.ViewModels
{
    public class ShellViewModel : BaseNavigableViewModel
    {
        public static Visibility IsDebug
        {
#if DEBUG
            get { return Visibility.Visible; }
#else
            get { return Visibility.Collapsed; }
#endif
        }

        public AppLogger AppLogger { get; set; }

        private string _appDisplayName;

        public string AppDisplayName
        {
            get => _appDisplayName;
            set => Set(ref _appDisplayName, value);
        }

        public ShellViewModel()
        {
            AppLogger = new AppLogger();
            AppDisplayName = "AppDisplayName".GetLocalized();
        }
    }
}
