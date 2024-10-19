using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonInvComp
    {
        [Required(ErrorMessage = "Missing Invoice Number", AllowEmptyStrings = false)]
        public string invno { get; set; }
        [Required(ErrorMessage = "Missing Company Id", AllowEmptyStrings = false)]
        public long? compid { get; set; }
    }
}