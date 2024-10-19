﻿using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class ReportDates
    {

        [Required(ErrorMessage = "Start Date is missing", AllowEmptyStrings = true)]
        public string stdate { get; set; }

        [Required(ErrorMessage = "End Date is missing", AllowEmptyStrings = true)]
        public string enddate { get; set; }
    }
}