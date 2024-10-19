using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class TransactInvoiceNo
    {

        [Required(ErrorMessage = "Missing Invoice Number", AllowEmptyStrings = false)]
        public string invoice_sno { get; set; }
    }
}