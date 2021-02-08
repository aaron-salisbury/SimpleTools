using Serilog;
using SimpleTools.Core.Base;
using SimpleTools.Core.Base.Extensions;
using SimpleTools.Core.Base.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SimpleTools.Core.Tools.Game
{
    public class DiceRoller : ValidatableModel
    {
        //TODO: Future Considerations:
        // Add functionality to be able to roll multiple times and sum results together.
        // Repeater to string together multiple modifiers. Example; 3d6×10+3 means "roll three 6-sided dice, add them together, multiply the result by 10, and then add 3."
        // Implement %, h, and l.

        private ILogger _logger { get; set; }
        private Random _randomGenerator { get; set; }

        private ObservableCollection<Die> _dice;
        [Required]
        [EnsureOneElement(ErrorMessage = "At least one Die is required to roll.")]
        public ObservableCollection<Die> Dice
        {
            get => _dice;
            set
            {
                _dice = value;
                RaisePropertyChanged(nameof(Dice));
            }
        }

        private bool _dropLowestResult;
        [DiceRollerLowestOrHighest]
        public bool DropLowestResult
        {
            get => _dropLowestResult;
            set
            {
                _dropLowestResult = value;
                RaisePropertyChanged(nameof(DropLowestResult));
                if (DropLowestResult && DropHighestResult)
                {
                    DropHighestResult = false;
                }
            }
        }

        private bool _dropHighestResult;
        [DiceRollerLowestOrHighest]
        public bool DropHighestResult
        {
            get => _dropHighestResult;
            set
            {
                _dropHighestResult = value;
                RaisePropertyChanged(nameof(DropHighestResult));
                if (DropHighestResult && DropLowestResult)
                {
                    DropLowestResult = false;
                }
            }
        }

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                RaisePropertyChanged(nameof(Result));
            }
        }

        private string _modifier;
        [StringIsInt(ErrorMessage = "{0} must be an integer.")]
        [DiceRollerModifierRequiresMultiplierType]
        [DiceRollerModifierNotZeroWhenDividing]
        public string Modifier
        {
            get => _modifier;
            set
            {
                _modifier = value;
                RaisePropertyChanged(nameof(Modifier));
                BuildNotation();
            }
        }

        public enum MultiplierTypes
        {
            None,
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        private MultiplierTypes _multiplierType;
        public MultiplierTypes MultiplierType
        {
            get => _multiplierType;
            set
            {
                _multiplierType = value;
                RaisePropertyChanged(nameof(MultiplierType));
                BuildNotation();
            }
        }

        private string _notation;
        public string Notation
        {
            get => _notation;
            set
            {
                _notation = value;
                RaisePropertyChanged(nameof(Notation));
            }
        }

        private string _parserNotation
        {
            get => Notation
                    .Replace('×', '*')
                    .Replace('÷', '/')
                    .Replace("-", "+-"); // For some reason Alea won't do subtraction, but it can add a negative.
        }

        public DiceRoller(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
            _randomGenerator = new Random();

            MultiplierType = MultiplierTypes.None;
            Dice = new ObservableCollection<Die>();
        }

        public bool AddDie(int numberOfSides)
        {
            try
            {
                Dice.Add(new Die
                {
                    Sides = numberOfSides
                });

                BuildNotation();

                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"Unable to add a die with a number of sides of {numberOfSides}: {e.Message}");
                return false;
            }
        }

        public bool RemoveDie(Die die)
        {
            try
            {
                Dice.Remove(die);

                BuildNotation();

                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"Unable to delete die \"{die.Description}\": {e.Message}");
                return false;
            }
        }

        public bool Roll()
        {
            try
            {
                if (Validate())
                {
                    // Alea | github.com/malorisdead/Alea
                    Dice.IterateAndAct(d => d.Result = Alea.Dice.Roll(d.Notation, _randomGenerator));

                    double totalResult = Dice.Sum(d => d.Result);

                    if (!string.IsNullOrEmpty(Modifier) && int.TryParse(Modifier, out int modifierValue))
                    {
                        switch (MultiplierType)
                        {
                            case MultiplierTypes.Addition:
                                totalResult += modifierValue;
                                break;
                            case MultiplierTypes.Subtraction:
                                totalResult -= modifierValue;
                                break;
                            case MultiplierTypes.Multiplication:
                                totalResult *= modifierValue;
                                break;
                            case MultiplierTypes.Division:
                                totalResult /= modifierValue;
                                break;
                        }
                    }

                    Result = totalResult.ToString();

                    _logger.Information($"Rolled {totalResult}.");

                    return true;
                }
                else
                {
                    Dice.IterateAndAct(d => d.ResetResults());
                    Result = null;
                    _logger.Error($"Roll failed: Roll Settings are invalid.");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Roll failed: {e.Message}");
                return false;
            }
        }

        private void BuildNotation()
        {
            if (Validate())
            {
                StringBuilder stringBuilder = new StringBuilder();

                Dictionary<int/*Number of Sides*/, int/*Count of Dice w/ this Number of Sides*/> diceCountByNumSides = new Dictionary<int, int>();

                foreach (Die die in Dice)
                {
                    if (diceCountByNumSides.ContainsKey(die.Sides))
                    {
                        diceCountByNumSides[die.Sides]++;
                    }
                    else
                    {
                        diceCountByNumSides[die.Sides] = 1;
                    }
                }

                string prefix = string.Empty;
                foreach (KeyValuePair<int/*Number of Sides*/, int/*Count of Dice w/ this Number of Sides*/> dieCountBySides in diceCountByNumSides.OrderBy(dcbns => dcbns.Key))
                {
                    stringBuilder.Append($"{prefix}{dieCountBySides.Value}d{dieCountBySides.Key}");
                    prefix = "+";
                }

                if (!string.IsNullOrEmpty(Modifier) && MultiplierType != MultiplierTypes.None)
                {
                    switch (MultiplierType)
                    {
                        case MultiplierTypes.Addition:
                            stringBuilder.Append($"+{Modifier}");
                            break;
                        case MultiplierTypes.Subtraction:
                            stringBuilder.Append($"-{Modifier}");
                            break;
                        case MultiplierTypes.Multiplication:
                            stringBuilder.Append($"×{Modifier}");
                            break;
                        case MultiplierTypes.Division:
                            stringBuilder.Append($"÷{Modifier}");
                            break;
                    }
                }

                Notation = stringBuilder.ToString();
            }
            else
            {
                Notation = null;
            }
        }
    }

    public class Die : ValidatableModel
    {
        private int _sides;
        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "A die must have at least {1} sides.")]
        public int Sides
        {
            get => _sides;
            set
            {
                _sides = value;
                RaisePropertyChanged(nameof(Sides));
            }
        }

        private double _result;
        public double Result
        {
            get => _result;
            set
            {
                _result = value;
                RaisePropertyChanged(nameof(Result));
                ResultDescription = $"Result: {value}";
            }
        }

        private string _resultDescription;
        public string ResultDescription
        {
            get => _resultDescription;
            set
            {
                _resultDescription = value;
                RaisePropertyChanged(nameof(ResultDescription));
            }
        }

        public string Description
        { 
            get => $"{Sides} Sided";
        }

        public string Notation
        {
            // https://en.wikipedia.org/wiki/Dice_notation
            // For example, if a game calls for a roll of d4 or 1d4 this means "roll one 4-sided die." 
            get => $"1d{Sides}";
        }

        public void ResetResults()
        {
            Result = 0D;
            ResultDescription = null;
        }
    }
}
