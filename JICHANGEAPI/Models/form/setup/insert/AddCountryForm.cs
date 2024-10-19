﻿using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddCountryForm : MainForm
    {
        [Required(ErrorMessage = "Missing name", AllowEmptyStrings = false)]
        public string Country_Name { get; set; }
        [Required(ErrorMessage = "Missing dummy")]
        public bool dummy { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long sno { get; set; }
        [Required(ErrorMessage = "Missing Audit By")]
        public string AuditBy { get; set; }



    }
}