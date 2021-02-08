using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class FinanceToolsPage : Page
    {
        private FinanceToolsViewModel ViewModel
        {
            get => ViewModelLocator.Current.FinanceToolsViewModel;
        }

        public FinanceToolsPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
