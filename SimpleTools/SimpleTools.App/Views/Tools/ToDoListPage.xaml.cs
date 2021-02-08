using SimpleTools.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class ToDoListPage : Page
    {
        private ToDoListViewModel ViewModel
        {
            get => ViewModelLocator.Current.ToDoListViewModel;
        }

        public ToDoListPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void AddChildButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.AddChildCommand.Execute(null);
        }

        private void DeleteListItemButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.DeleteListItemCommand.Execute(null);
        }
    }
}
