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
    
    public partial class grn_details
    {
        public long grn_det_id { get; set; }
        public Nullable<long> grn_id { get; set; }
        public Nullable<long> item_code { get; set; }
        public Nullable<decimal> pending_quantity { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public string item_vat_applicable { get; set; }
        public Nullable<decimal> item_price_with_vat { get; set; }
        public Nullable<decimal> item_vat_amount { get; set; }
        public string supplier_details { get; set; }
        public string item_status { get; set; }
        public string batch_no { get; set; }
    
        public virtual grn_master grn_master { get; set; }
    }
}