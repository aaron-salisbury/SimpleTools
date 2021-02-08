using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using SimpleTools.App.Base.Helpers;
using SimpleTools.Core.Tools.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTools.App.ViewModels
{
    public class DiceRollerViewModel : BaseViewModel
    {
        public DiceRoller DiceRoller { get; set; }

        public RelayCommand AddDieCommand { get; }
        public RelayCommand RollTaskCommand { get; }

        private List<ComboBoxEnumItem> _multiplierTypes;
        public List<ComboBoxEnumItem> MultiplierTypes
        {
            get => _multiplierTypes;
            set
            {
                _multiplierTypes = value;
                RaisePropertyChanged(nameof(MultiplierTypes));
            }
        }

        private ComboBoxEnumItem _multiplierType;
        public ComboBoxEnumItem MultiplierType
        {
            get => _multiplierType;
            set
            {
                _multiplierType = value;
                RaisePropertyChanged(nameof(MultiplierType));
                DiceRoller.MultiplierType = (DiceRoller.MultiplierTypes)value.Value;
            }
        }

        private int _newDieSides;
        public int NewDieSides
        {
            get => _newDieSides;
            set
            {
                _newDieSides = value;
                RaisePropertyChanged(nameof(NewDieSides));
                NewDieHeader = $"{NewDieSides} Sides";
            }
        }

        private string _newDieHeader;
        public string NewDieHeader
        {
            get => _newDieHeader;
            set
            {
                _newDieHeader = $"{NewDieSides} Sides";
                RaisePropertyChanged(nameof(NewDieHeader));
            }
        }

        private object _selectedDie;
        public object SelectedDie
        {
            get => _selectedDie;
            set
            {
                _selectedDie = value;
                RaisePropertyChanged(nameof(SelectedDie));
            }
        }

        private bool _isDiceRollerValid;
        public bool IsDiceRollerValid
        {
            get => _isDiceRollerValid;
            set
            {
                _isDiceRollerValid = value;
                RaisePropertyChanged(nameof(IsDiceRollerValid));
            }
        }

        public DiceRollerViewModel()
        {
            DiceRoller = new DiceRoller(AppLogger);
            DiceRoller.MultiplierType = DiceRoller.MultiplierTypes.None;
            NewDieSides = 6;
            AddDieCommand = new RelayCommand(AddDie);

            bool rollfunction() => DiceRoller.Roll();
            RollTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(rollfunction, RollTaskCommand), () => !IsBusy);

            MultiplierTypes = Enum.GetValues(typeof(DiceRoller.MultiplierTypes))
                .Cast<DiceRoller.MultiplierTypes>()
                .Select(mt => new ComboBoxEnumItem() { Value = (int)mt, Text = mt.ToString() })
                .ToList();

            MultiplierType = MultiplierTypes
                .Where(cbi => cbi.Value == (int)DiceRoller.MultiplierType)
                .First();

            IsDiceRollerValid = !DiceRoller.HasErrors;
            DiceRoller.ErrorsChanged += (sender, args) => IsDiceRollerValid = !DiceRoller.HasErrors;
        }

        private void DiceRoller_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            IsDiceRollerValid = !DiceRoller.HasErrors;
        }

        private void AddDie()
        {
            if (NewDieSides > 1)
            {
                DiceRoller.AddDie(NewDieSides);
            }
        }

        internal void DeleteDie()
        {
            if (SelectedDie != null && SelectedDie is Die)
            {
                DiceRoller.RemoveDie(SelectedDie as Die);
            }
        }
    }
}
