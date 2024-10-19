using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonInv
    {
        [Required(ErrorMessage = "Missing Invoice Id", AllowEmptyStrings = false)]
        public int invid { get; set; }
    }
}