using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonMobile
    {
        [Required(ErrorMessage = "Missing Mobile Number", AllowEmptyStrings = false)]
        public string mobile { get; set; }
    }
}