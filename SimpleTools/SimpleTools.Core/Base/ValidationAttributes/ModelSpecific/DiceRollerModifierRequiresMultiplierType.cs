using SimpleTools.Core.Tools.Game;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Base.ValidationAttributes
{
    public class DiceRollerModifierRequiresMultiplierType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;

            if (validationContext.ObjectInstance is DiceRoller diceRoller && 
                !string.IsNullOrEmpty(diceRoller.Modifier) && 
                int.TryParse(value.ToString(), out int parsedInteger) && 
                parsedInteger != 0 && 
                diceRoller.MultiplierType == DiceRoller.MultiplierTypes.None)
            {
                result = new ValidationResult("Must choose a Multiplier Type to use a Modifier.", new List<string> { validationContext.MemberName });
            }

            return result;
        }
    }
}
