using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class BudgetPage : Page
    {
        private BudgetViewModel ViewModel
        {
            get => ViewModelLocator.Current.BudgetViewModel;
        }

        public BudgetPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
