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
    
    public partial class payment_details
    {
        public long sno { get; set; }
        public string payment_sno { get; set; }
        public System.DateTime payment_date { get; set; }
        public System.DateTime payment_time { get; set; }
        public string control_no { get; set; }
        public string trans_channel { get; set; }
        public string payment_type { get; set; }
        public string payment_desc { get; set; }
        public string payer_id { get; set; }
        public string institution_id { get; set; }
        public string currency_code { get; set; }
        public Nullable<long> requested_amount { get; set; }
        public Nullable<long> paid_amount { get; set; }
        public string pay_trans_no { get; set; }
        public string receipt_no { get; set; }
        public string message { get; set; }
        public string batch_no { get; set; }
        public string authorize_id { get; set; }
        public string secure_hash { get; set; }
        public string response_code { get; set; }
        public string merchant { get; set; }
        public string card { get; set; }
        public string token { get; set; }
        public string chksum { get; set; }
        public string status { get; set; }
        public string response { get; set; }
        public string posted_by { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
        public string payer_name { get; set; }
        public string amount_type { get; set; }
        public Nullable<long> comp_mas_sno { get; set; }
        public Nullable<long> cust_mas_sno { get; set; }
        public string invoice_sno { get; set; }
        public Nullable<long> amount30 { get; set; }
        public Nullable<long> amount70 { get; set; }
    }
}