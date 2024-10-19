using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonComp
    {

        [Required(ErrorMessage = "Company ID is missing", AllowEmptyStrings = false)]
        public long? compid { get; set; }
    }
}