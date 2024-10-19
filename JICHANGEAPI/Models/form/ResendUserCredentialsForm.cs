using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form
{
    public class ResendUserCredentialsForm
    {
        [Required(ErrorMessage = "Missing resend credentials method", AllowEmptyStrings = false)]
        public string resendCredentials { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Invalid vendor user id")]
        [Required(ErrorMessage = "Missing vendor user id")]
        public long? companyUserId { get; set; }
    }
}
