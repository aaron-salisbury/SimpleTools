using GalaSoft.MvvmLight.Command;
using SimpleTools.Core.Domains.ToDoListManager;
using SimpleTools.Core.Tools.Assistant;
using System.Windows.Input;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace SimpleTools.App.ViewModels
{
    public class ToDoListViewModel : BaseViewModel
    {
        public ToDoListManager ToDoListManager { get; set; }

        public RelayCommand AddCategoryCommand { get; }
        public RelayCommand AddChildCommand { get; }
        public RelayCommand DeleteListItemCommand { get; }
        public RelayCommand SaveCommand { get; }

        private ICommand _itemInvokedCommand;
        public ICommand ItemInvokedCommand => _itemInvokedCommand ?? (_itemInvokedCommand = new RelayCommand<WinUI.TreeViewItemInvokedEventArgs>(OnItemInvoked));

        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public ToDoListViewModel()
        {
            ToDoListManager = new ToDoListManager(AppLogger);
            AddCategoryCommand = new RelayCommand(() => ToDoListManager.ListCategories.Add(new ListCategory() { Rank = ToDoListManager.GetNextCategoryRank() }));
            AddChildCommand = new RelayCommand(() => AddChild());
            DeleteListItemCommand = new RelayCommand(() => DeleteListItem());
            SaveCommand = new RelayCommand(() => ToDoListManager.Save());
        }

        private void OnItemInvoked(WinUI.TreeViewItemInvokedEventArgs args) => SelectedItem = args.InvokedItem;

        private void AddChild()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem is ListCategory)
                {
                    ListCategory category = SelectedItem as ListCategory;
                    ToDoItem newItem = new ToDoItem() { Rank = ToDoListManager.GetNextItemRank(category) };
                    category.ToDoItems.Add(newItem);
                }
                else if (SelectedItem is ToDoItem)
                {
                    ToDoItem item = SelectedItem as ToDoItem;
                    ToDoItemStep newStep = new ToDoItemStep() { Rank = ToDoListManager.GetNextStepRank(item) };
                    item.ToDoItemSteps.Add(newStep);
                }
            }
        }

        private void DeleteListItem()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem is ListCategory)
                {
                    ToDoListManager.ListCategories.Remove(SelectedItem as ListCategory);
                }
                else if (SelectedItem is ToDoItem)
                {
                    foreach (ListCategory listCategory in ToDoListManager.ListCategories)
                    {
                        if(listCategory.ToDoItems.Remove(SelectedItem as ToDoItem))
                        {
                            break;
                        }
                    }
                }
                else if (SelectedItem is ToDoItemStep)
                {
                    foreach (ListCategory listCategory in ToDoListManager.ListCategories)
                    {
                        foreach (ToDoItem toDoItem in listCategory.ToDoItems)
                        {
                            if (toDoItem.ToDoItemSteps.Remove(SelectedItem as ToDoItemStep))
                            {
                                break;
                            }
                        }
                    }
                }

                SelectedItem = null;
            }
        }
    }
}
