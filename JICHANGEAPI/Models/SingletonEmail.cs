using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonEmail
    {
        [Required(ErrorMessage = "Missing Email Address", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string email { get; set; }
    }
}