using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class IntroductionPage : Page
    {
        public IntroductionPage()
        {
            this.InitializeComponent();
            DataContext = ViewModelLocator.Current.IntroductionViewModel;
        }
    }
}
