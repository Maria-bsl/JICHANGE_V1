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
    
    public partial class item_sub_groups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public item_sub_groups()
        {
            this.item_master = new HashSet<item_master>();
        }
    
        public long sub_group_code { get; set; }
        public Nullable<long> group_code { get; set; }
        public string sub_group_name { get; set; }
        public string posted_by { get; set; }
        public System.DateTime posted_date { get; set; }
    
        public virtual item_groups item_groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item_master> item_master { get; set; }
    }
}
