using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class UUIDGeneratorPage : Page
    {
        public UUIDGeneratorPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.UUIDGeneratorViewModel;
        }
    }
}
