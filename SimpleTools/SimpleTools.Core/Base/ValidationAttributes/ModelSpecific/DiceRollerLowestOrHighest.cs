using SimpleTools.Core.Tools.Game;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Base.ValidationAttributes
{
    public class DiceRollerLowestOrHighest : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;

            if (validationContext.ObjectInstance is DiceRoller diceRoller && diceRoller.DropHighestResult && diceRoller.DropLowestResult)
            {
                result = new ValidationResult("Cannot use both lowest and highest in a dice roll.", new List<string> { validationContext.MemberName });
            }

            return result;
        }
    }
}
