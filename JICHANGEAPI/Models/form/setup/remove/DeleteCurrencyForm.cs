using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form.setup.remove
{
    public class DeleteCurrencyForm : MainForm
    {
        [Required(ErrorMessage = "Missing currency code")]
        public string code { get; set; }
    }
}