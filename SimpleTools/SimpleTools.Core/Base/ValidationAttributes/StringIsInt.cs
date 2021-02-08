using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Base.ValidationAttributes
{
    public class StringIsInt : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()) && !int.TryParse(value.ToString(), out _))
            {
                return false;
            }

            return true;
        }
    }
}
