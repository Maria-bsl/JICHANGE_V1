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
    
    public partial class item_groups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public item_groups()
        {
            this.item_master = new HashSet<item_master>();
            this.item_sub_groups = new HashSet<item_sub_groups>();
        }
    
        public long group_code { get; set; }
        public string group_name { get; set; }
        public string posted_by { get; set; }
        public System.DateTime posted_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item_master> item_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item_sub_groups> item_sub_groups { get; set; }
    }
}
