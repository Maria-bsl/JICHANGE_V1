﻿using DaL.BIZINVOICING.EDMX;
using System;
using System.Linq;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class TableDetails
    {

        #region Properties

        public string tab_name { get; set; }
        public long Sno { get; set; }
        public String Relation { get; set; }
        #endregion Properties
        #region Method
        public TableDetails Getlog(string tab)
        {
            //DateTime add = to.AddDays(1);
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from mr in context.table_details
                                where !string.IsNullOrEmpty(mr.table_name) && mr.table_name.ToLower().Equals(tab.ToLower())
                                select new TableDetails
                                {
                                    tab_name = mr.table_name,
                                    Sno = mr.sno,
                                    Relation = mr.table_relation

                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        #endregion Method

    }
}
