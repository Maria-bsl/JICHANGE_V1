using JichangeApi.Models.Validators;
using System.Collections.Generic;

namespace JichangeApi.Models
{
    public class CustomerDetailsForm
    {
        [RequiredList(ErrorMessage = "Missing company Ids")]
        public List<long> companyIds { get; set; }
    }
}
