using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SessionBModel
    {
        [Required(ErrorMessage = "Missing Session ID", AllowEmptyStrings = false)]
        public string sessB { get; set; }
    }
}