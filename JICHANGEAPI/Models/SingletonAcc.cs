using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonAcc
    {
        [Required(ErrorMessage = "Missing Account Number", AllowEmptyStrings = false)]
        public string acc { get; set; }
    }
}