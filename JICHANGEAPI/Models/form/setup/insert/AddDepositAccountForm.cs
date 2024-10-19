using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddDepositAccountForm : MainForm
    {
        [Required(ErrorMessage = "Missing account number", AllowEmptyStrings = false)]
        public string account { get; set; }
    }
}