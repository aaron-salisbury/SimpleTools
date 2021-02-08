using SimpleTools.Core.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Domains.ToDoListManager
{
    [Serializable]
    public class ToDoItem : ValidatableModel
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

        private string _item;
        [Required]
        public string Item
        {
            get => _item;
            set { _item = value; RaisePropertyChanged(); }
        }

        private string _notes;
        [StringLength(21844, ErrorMessage = "The {0} may not be more than {1} characters long.")]
        public string Notes
        {
            get => _notes;
            set { _notes = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoItemStep> _toDoItemSteps;
        public ObservableCollection<ToDoItemStep> ToDoItemSteps
        {
            get => _toDoItemSteps;
            set { _toDoItemSteps = value; RaisePropertyChanged(); }
        }

        public ToDoItem()
        {
            Id = Guid.NewGuid();
            Item = "New Item";
            ToDoItemSteps = new ObservableCollection<ToDoItemStep>();
        }
    }
}
