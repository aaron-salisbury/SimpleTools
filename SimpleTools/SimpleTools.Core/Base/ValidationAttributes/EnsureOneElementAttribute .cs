using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Base.ValidationAttributes
{
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is IList list))
            {
                return true;
            }

            return list.Count > 0;
        }
    }
}
