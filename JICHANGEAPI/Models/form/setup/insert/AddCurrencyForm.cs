﻿using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddCurrencyForm : MainForm
    {
        [Required(ErrorMessage = "Missing code")]
        public string code { get; set; }
        [Required(ErrorMessage = "Missing currency")]
        public string cname { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        public bool dummy { get; set; }
    }
}