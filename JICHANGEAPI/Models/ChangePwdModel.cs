using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class ChangePwdModel : SingletonMobile
    {
        [Required(ErrorMessage = "Missing Password", AllowEmptyStrings = false)]
        public string password { get; set; }
    }
}