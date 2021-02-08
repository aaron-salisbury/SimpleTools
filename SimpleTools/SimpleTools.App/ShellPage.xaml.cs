using SimpleTools.App.Base.Helpers;
using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App
{
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel
        {
            get => ViewModelLocator.Current.ShellViewModel;
        }

        private CustomTitleBarHelper _customTitleBarHelper { get; set; }

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);

            _customTitleBarHelper = new CustomTitleBarHelper(AppTitleBar, AppTitleTextBlock);
        }
    }
}
