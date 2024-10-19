using JichangeApi.Models.Validators;
using System.Collections.Generic;

namespace JichangeApi.Models.form
{
    public class InvoiceReportDetailsForm : InvoiceDetailsForm
    {
        [RequiredList(ErrorMessage = "Missing invoice ids")]
        public List<long> invoiceIds { get; set; }
    }
}
