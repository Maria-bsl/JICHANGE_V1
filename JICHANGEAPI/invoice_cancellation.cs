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
    
    public partial class invoice_cancellation
    {
        public long inv_can_sno { get; set; }
        public string control_no { get; set; }
        public Nullable<long> comp_mas_sno { get; set; }
        public Nullable<long> cust_mas_sno { get; set; }
        public Nullable<long> inv_mas_sno { get; set; }
        public Nullable<decimal> invoice_amount { get; set; }
        public string reason_for_cancel { get; set; }
        public string posted_by { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
    
        public virtual company_master company_master { get; set; }
        public virtual customer_master customer_master { get; set; }
        public virtual invoice_master invoice_master { get; set; }
    }
}