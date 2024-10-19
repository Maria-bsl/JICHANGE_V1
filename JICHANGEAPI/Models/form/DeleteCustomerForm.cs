using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form
{
    public class DeleteCustomerForm : MainForm
    {
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
    }
}