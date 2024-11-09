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
    
    public partial class invoice_master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public invoice_master()
        {
            this.invoice_ammendment = new HashSet<invoice_ammendment>();
            this.invoice_cancellation = new HashSet<invoice_cancellation>();
            this.invoice_details = new HashSet<invoice_details>();
        }
    
        public long inv_mas_sno { get; set; }
        public string invoice_no { get; set; }
        public Nullable<System.DateTime> invoice_date { get; set; }
        public Nullable<long> comp_mas_sno { get; set; }
        public Nullable<long> cust_mas_sno { get; set; }
        public string currency_code { get; set; }
        public Nullable<decimal> total_without_vat { get; set; }
        public Nullable<decimal> vat_amount { get; set; }
        public Nullable<decimal> total_amount { get; set; }
        public string inv_remarks { get; set; }
        public string posted_by { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
        public string warrenty { get; set; }
        public string goods_status { get; set; }
        public string delivery_status { get; set; }
        public Nullable<long> grand_count { get; set; }
        public Nullable<int> daily_count { get; set; }
        public string approval_status { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public string customer_id_type { get; set; }
        public string customer_id_no { get; set; }
        public Nullable<System.DateTime> p_date { get; set; }
        public string control_no { get; set; }
        public string payment_type { get; set; }
        public Nullable<System.DateTime> due_date { get; set; }
        public Nullable<System.DateTime> invoice_expired { get; set; }
    
        public virtual company_master company_master { get; set; }
        public virtual currency_master currency_master { get; set; }
        public virtual customer_master customer_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invoice_ammendment> invoice_ammendment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invoice_cancellation> invoice_cancellation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invoice_details> invoice_details { get; set; }
    }
}