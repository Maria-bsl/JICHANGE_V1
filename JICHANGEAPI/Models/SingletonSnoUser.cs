using JichangeApi.Models.form;
using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class SingletonSnoUser : MainForm
    {

        [Required(ErrorMessage = "Missing Sno", AllowEmptyStrings = false)]
        public long? Sno { get; set; }
    }
}