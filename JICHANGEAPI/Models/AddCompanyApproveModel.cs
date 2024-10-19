﻿using JichangeApi.Models.form;
using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models
{
    public class AddCompanyApproveModel : MainForm
    {
        [Required(ErrorMessage = "Missing Company id", AllowEmptyStrings = false)]
        public long compsno { get; set; }
        public string pfx { get; set; }
        public string pwd { get; set; }
        public long ssno { get; set; }


        [Required(ErrorMessage = "Missing Suspense Account", AllowEmptyStrings = false)]
        public long suspenseAccSno { get; set; }

        [Required(ErrorMessage = "Missing Deposit Account Sno", AllowEmptyStrings = false)]
        public string depositAccNo { get; set; }

    }
}