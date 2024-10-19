using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonGetCustDetRepModel
    {
        [Required(ErrorMessage = "Missing Company Id", AllowEmptyStrings = false)]
        public long Comp { get; set; }
        [Required(ErrorMessage = "Missing Region", AllowEmptyStrings = true)]
        public long reg { get; set; }
        [Required(ErrorMessage = "Missing District", AllowEmptyStrings = true)]
        public long dist { get; set; }
    }
}