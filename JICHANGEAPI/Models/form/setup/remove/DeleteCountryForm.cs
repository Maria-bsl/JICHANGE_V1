using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form.setup.remove
{
    public class DeleteCountryForm : MainForm
    {
        [Required(ErrorMessage = "Missing country id.")]
        public long sno { get; set; }
    }
}