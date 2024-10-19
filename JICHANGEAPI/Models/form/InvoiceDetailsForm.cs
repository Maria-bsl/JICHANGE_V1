using JichangeApi.Models.Validators;
using System.Collections.Generic;

namespace JichangeApi.Models.form
{
    public class InvoiceDetailsForm
    {
        [RequiredList(ErrorMessage = "Missing companyId")]
        public List<long> companyIds { get; set; }
        [RequiredList(ErrorMessage = "Missing customerId")]
        public List<long> customerIds { get; set; }
        public string stdate { get; set; }
        public string enddate { get; set; }
        public bool allowCancelInvoice { get; set; } = false;
    }
}
