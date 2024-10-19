using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.Validators
{
    public class RequiredList : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            return list != null && list.Count > 0;
        }
    }
}
