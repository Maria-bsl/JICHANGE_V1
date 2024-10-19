using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class BranchRef
    {

        [Required(ErrorMessage = "Branch Sno is missing", AllowEmptyStrings = false)]
        public long? branch { get; set; }
    }
}