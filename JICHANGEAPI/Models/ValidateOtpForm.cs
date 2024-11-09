using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class ValidateOtpForm : SingletonMobile
    {

        [Required(ErrorMessage = "Missing Otp Code", AllowEmptyStrings = false)]
        public string otp_code { get; set; }
    }
}