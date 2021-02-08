using SimpleTools.Core.Tools.Game;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Base.ValidationAttributes
{
    public class DiceRollerModifierNotZeroWhenDividing : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;

            if (validationContext.ObjectInstance is DiceRoller diceRoller && 
                !string.IsNullOrEmpty(diceRoller.Modifier) && 
                int.TryParse(diceRoller.Modifier, out int parsedInteger) && 
                parsedInteger == 0 && 
                diceRoller.MultiplierType == DiceRoller.MultiplierTypes.Division)
            {
                result = new ValidationResult("Modifier must not equal 0 when Multiplier Type is set to Division.", new List<string> { validationContext.MemberName });
            }

            return result;
        }
    }
}
