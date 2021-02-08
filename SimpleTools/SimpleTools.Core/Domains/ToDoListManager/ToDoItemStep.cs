using SimpleTools.Core.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Domains.ToDoListManager
{
    [Serializable]
    public class ToDoItemStep : ValidatableModel
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

        private string _step;
        [Required]
        public string Step
        {
            get => _step;
            set { _step = value; RaisePropertyChanged(); }
        }

        private bool _isComplete;
        public bool IsComplete
        {
            get => _isComplete;
            set { _isComplete = value; RaisePropertyChanged(); }
        }

        private string _notes;
        [StringLength(21844, ErrorMessage = "The {0} may not be more than {1} characters long.")]
        public string Notes
        {
            get => _notes;
            set { _notes = value; RaisePropertyChanged(); }
        }

        public ToDoItemStep()
        {
            Id = Guid.NewGuid();
            Step = "New Step";
        }
    }
}
