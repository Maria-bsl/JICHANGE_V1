using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonSno
    {
        [Required(ErrorMessage = "Missing Sno", AllowEmptyStrings = false)]
        public long? Sno { get; set; }
    }
}