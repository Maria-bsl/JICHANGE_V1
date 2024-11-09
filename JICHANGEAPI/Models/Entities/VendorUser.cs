using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Web;

namespace JichangeApi.Models.Entities
{
    public class VendorUser
    {
        public static VendorUser CreateVendor(company_users companyUser, company_master company, roles_master roles)
        {
            if (companyUser == null || company == null || roles == null) { return null; }
            VendorUser vendor = new VendorUser();
            vendor.tupleRoles = Tuple.Create(companyUser, company, roles);
            return CreateVendor(companyUser, vendor);
        }

        public JsonObject toJson()
        {
            JsonObject vendor = new JsonObject() { 
                { "PostedDate", PostedDate }, 
                { "PostedBy", PostedBy }, 
                { "MailStatus", MailStatus }, 
                { "UserPosition", UserPosition }, 
                { "CTime", CTime }, 
                { "Flag", Flag }, 
                { "Localization", Localization }, 
                { "MobileNumber", MobileNumber }, 
                { "EmailAddress", EmailAddress }, 
                { "UserFullName", UserFullName }, 
                { "LogStatus", LogStatus }, 
                { "LogTime", LogTime }, 
                { "QAns", QAns }, 
                { "QName", QName }, 
                { "SNO", SNO }, 
                { "FLogin", FLogin }, 
                { "ExpiryDate", ExpiryDate }, 
                { "CreatedDate", CreatedDate }, 
                { "UserType", UserType }, 
                { "Password", Password }, 
                { "Username", Username }, 
                { "CompMasSno", CompMasSno }, 
                { "CompUsersSno", CompUsersSno }, 
                { "LogAtt", LogAtt }, 
            };
            return vendor;
        }

        public static VendorUser CreateVendor(company_users companyUser, VendorUser vendor = null)
        {
            vendor = vendor ?? new VendorUser();
            try
            {
                vendor.PostedDate = companyUser.posted_date;
                vendor.PostedBy = companyUser.posted_by;
                vendor.MailStatus = companyUser.mail_status;
                vendor.UserPosition = companyUser.user_position;
                vendor.CTime = companyUser.ctime;
                vendor.Flag = companyUser.flag;
                vendor.Localization = companyUser.localization;
                vendor.MobileNumber = companyUser.mobile_no;
                vendor.EmailAddress = companyUser.email_address;
                vendor.UserFullName = companyUser.user_fullname;
                vendor.LogStatus = companyUser.log_status;
                vendor.LogTime = companyUser.log_time;
                vendor.QAns = companyUser.q_ans;
                vendor.QName = companyUser.q_name;
                vendor.SNO = companyUser.sno;
                vendor.FLogin = companyUser.f_login;
                vendor.ExpiryDate = companyUser.expiry_date;
                vendor.CreatedDate = companyUser.created_date;
                vendor.UserType = companyUser.user_type;
                vendor.Password = companyUser.password;
                vendor.Username = companyUser.username;
                vendor.CompMasSno = companyUser.comp_mas_sno;
                vendor.CompUsersSno = companyUser.comp_users_sno;
                vendor.LogAtt = companyUser.log_att;
                return vendor;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Tuple<company_users, company_master, roles_master> tupleRoles { get; set; }

        public DateTime? PostedDate { get; set; }
        public string PostedBy { get; set; }
        public int? MailStatus { get; set; }
        public long? UserPosition { get; set; }
        public DateTime? CTime { get; set; }
        public string Flag { get; set; }
        public string Localization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UserFullName { get; set; }
        public string LogStatus { get; set; }
        public DateTime? LogTime { get; set; }
        public string QAns { get; set; }
        public string QName { get; set; }
        public long? SNO { get; set; }
        public string FLogin { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public long? CompMasSno { get; set; }
        public long CompUsersSno { get; set; }
        public int? LogAtt { get; set; }
    }
}