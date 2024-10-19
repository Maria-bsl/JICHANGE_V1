using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonControl
    {
        [Required(ErrorMessage = "Missing Control number", AllowEmptyStrings = false)]
        public string control { get; set; }
    }
}