using SimpleTools.Core.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Domains.ToDoListManager
{
    [Serializable]
    public class ListCategory : ValidatableModel
    {
        [Required]
        public Guid Id { get; }

        private int _rank;
        [Required]
        public int Rank
        {
            get => _rank;
            set { _rank = value; RaisePropertyChanged(); }
        }

        private string _category;
        public string Category
        {
            get => _category;
            set { _category = value; RaisePropertyChanged(); }
        }

        private string _notes;
        [StringLength(21844, ErrorMessage = "The {0} may not be more than {1} characters long.")]
        public string Notes
        {
            get => _notes;
            set { _notes = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoItem> _toDoItems;
        public ObservableCollection<ToDoItem> ToDoItems
        {
            get => _toDoItems;
            set { _toDoItems = value; RaisePropertyChanged(); }
        }

        public ListCategory()
        {
            Id = Guid.NewGuid();
            Category = "New Category";
            ToDoItems = new ObservableCollection<ToDoItem>();
        }
    }
}
