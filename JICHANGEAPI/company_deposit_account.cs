//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JichangeApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class company_deposit_account
    {
        public long comp_dep_acc_sno { get; set; }
        public Nullable<long> comp_mas_sno { get; set; }
        public string deposit_acc_no { get; set; }
        public string reason { get; set; }
        public string posted_by { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
    
        public virtual company_master company_master { get; set; }
    }
}