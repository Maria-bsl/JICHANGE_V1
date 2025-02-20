using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.form
{
    public class DownloadInvoice
    {
        [Required(ErrorMessage = "Missing company id")]
        public long? compid { get; set; } 
        [Required(ErrorMessage = "Missing invoice id")]
        public long? invId { get; set; }
        [Required(ErrorMessage = "Missing Language", AllowEmptyStrings = false)]
        public string lang { get; set; }
    }
}
