using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class Desibraid
    {

        [Required(ErrorMessage = "Missing designation", AllowEmptyStrings = false)]
        public string design { get; set; }

        [Required(ErrorMessage = "Missing branch id", AllowEmptyStrings = false)]
        public long? braid { get; set; }


    }
}