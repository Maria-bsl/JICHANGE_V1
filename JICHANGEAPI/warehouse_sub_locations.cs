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
    
    public partial class warehouse_sub_locations
    {
        public Nullable<long> warehouse_id { get; set; }
        public long location_id { get; set; }
        public string location_name { get; set; }
    
        public virtual warehouse_master warehouse_master { get; set; }
    }
}
