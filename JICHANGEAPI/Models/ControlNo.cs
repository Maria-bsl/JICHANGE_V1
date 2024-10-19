using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class ControlNo
    {
        [Required(ErrorMessage = "Control Number is missing", AllowEmptyStrings = false)]
        public string control { get; set; }
    }
}