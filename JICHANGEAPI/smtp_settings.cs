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
    
    public partial class smtp_settings
    {
        public long sno { get; set; }
        public string from_address { get; set; }
        public string smtp_address { get; set; }
        public string smtp_port { get; set; }
        public string username { get; set; }
        public string smtp_password { get; set; }
        public string ssl_enable { get; set; }
        public Nullable<System.DateTime> effective_date { get; set; }
        public string posted_by { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
    }
}
