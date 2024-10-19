using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form
{
    public class GetEmployeeForm
    {
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
    }
}