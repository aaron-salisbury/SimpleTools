using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class DummyTranslatorPage : Page
    {
        public DummyTranslatorPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.DummyTranslatorViewModel;
        }
    }
}
