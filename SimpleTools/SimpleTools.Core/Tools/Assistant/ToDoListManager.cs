using Serilog;
using SimpleTools.Core.Base;
using SimpleTools.Core.Domains.ToDoListManager;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleTools.Core.Tools.Assistant
{
    public class ToDoListManager : ValidatableModel
    {
        private ILogger _logger { get; set; }

        public ObservableCollection<ListCategory> ListCategories { get; set; } = new ObservableCollection<ListCategory>();

        public ToDoListManager(AppLogger appLogger)
        {
            _logger = appLogger.Logger;

            Load();
        }

        public bool Load()
        {
            try
            {
                _logger.Information("Loading To Do List.");

                ListCategories = new ObservableCollection<ListCategory>(System.Threading.Tasks.Task.Run(() => DataAccess.CRUD.ReadToDoListAsync()).Result);

                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"Failed to load To Do List: {e.Message}");
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                _logger.Information("Saving To Do List.");

                System.Threading.Tasks.Task.Run(() => DataAccess.CRUD.UpdateToDoListAsync(ListCategories)).Wait();

                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"Failed to save To Do List: {e.Message}");
                return false;
            }
        }

        public int GetNextCategoryRank()
        {
            return ListCategories.OrderByDescending(lc => lc.Rank).FirstOrDefault()?.Rank + 1 ?? 1;
        }

        public int GetNextItemRank(ListCategory category)
        {
            return category.ToDoItems.OrderByDescending(tdi => tdi.Rank).FirstOrDefault()?.Rank + 1 ?? 1;
        }

        public int GetNextStepRank(ToDoItem item)
        {
            return item.ToDoItemSteps.OrderByDescending(tdis => tdis.Rank).FirstOrDefault()?.Rank + 1 ?? 1;
        }

        private void LoadTestData()
        {
            ListCategories.Add(BuildTestCetegory(1, "Home", "Things to get done at home."));
            ListCategories.Add(BuildTestCetegory(2, "Work"));
        }

        private ListCategory BuildTestCetegory(int rank, string categoryName, string notes = null)
        {
            ListCategory listCategory = new ListCategory()
            {
                Rank = rank,
                Category = categoryName,
                Notes = notes,
                ToDoItems = new ObservableCollection<ToDoItem>()
            };

            for (int i = 0; i < 11; i++)
            {
                ToDoItem item = new ToDoItem()
                {
                    Rank = i,
                    Item = $"Test Data {i}",
                    //ListCategory = listCategory,
                    ToDoItemSteps = new ObservableCollection<ToDoItemStep>()
                };

                for (int j = 0; j < 11; j++)
                {
                    ToDoItemStep step = new ToDoItemStep()
                    {
                        Rank = j,
                        Step = $"Test Data {j}",
                        //ToDoItem = item
                    };

                    item.ToDoItemSteps.Add(step);
                }

                listCategory.ToDoItems.Add(item);
            }

            return listCategory;
        }
    }
}
