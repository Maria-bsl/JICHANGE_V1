using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Web;

namespace JichangeApi.Models.Entities
{
    public class EmployeeDetail
    {
        public static EmployeeDetail CreateEmployeeDetail(emp_detail employeeDetail,designation_list designations)
        {
            if (employeeDetail == null) { return null; }
            EmployeeDetail detail = new EmployeeDetail();
            try
            {
                detail.EmployeeId = employeeDetail.emp_detail_id;
                detail.EmployeeNumber = employeeDetail.emp_id_no;
                detail.FullName = employeeDetail.full_name;
                detail.FirstName = employeeDetail.first_name;
                detail.MiddleName = employeeDetail.middle_name;
                detail.LastName = employeeDetail.last_name;
                detail.DesignationId = employeeDetail.desg_id;
                detail.EmailId = employeeDetail.email_id;
                detail.Password = employeeDetail.pwd;
                detail.MobileNumber = employeeDetail.mobile_no;
                detail.CreatedDate = employeeDetail.created_date;
                detail.ExpiryDate = employeeDetail.expiry_date;
                detail.FLogin = employeeDetail.f_login;
                detail.SNO = employeeDetail.sno;
                detail.QuestionName = employeeDetail.q_name;
                detail.QuestionAnswer = employeeDetail.q_ans;
                detail.AppStatus = employeeDetail.app_status;
                detail.LogAtt = employeeDetail.log_att;
                detail.LogTime = employeeDetail.log_time;
                detail.LogStatus = employeeDetail.log_status;
                detail.EmployeeStatus = employeeDetail.emp_status;
                detail.Username = employeeDetail.username;
                detail.PostedBy = employeeDetail.posted_by;
                detail.PostedDate = employeeDetail.posted_date;
                detail.Localization = employeeDetail.localization;
                detail.BranchSno = employeeDetail.branch_Sno;
                return detail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonObject toJson()
        {
            JsonObject employeeDetail = new JsonObject()
            {
                { "EmployeeId", EmployeeId },
                { "EmployeeNumber", EmployeeNumber },
                { "FullName", FullName },
                { "FirstName", FirstName },
                { "MiddleName", MiddleName },
                { "LastName", LastName },
                { "DesignationId", DesignationId },
                { "EmailId", EmailId },
                { "Password", Password },
                { "MobileNumber", MobileNumber },
                { "CreatedDate", CreatedDate },
                { "ExpiryDate", ExpiryDate },
                { "FLogin", FLogin },
                { "SNO", SNO },
                { "QuestionName", QuestionName },
                { "QuestionAnswer", QuestionAnswer },
                { "AppStatus", AppStatus },
                { "LogAtt", LogAtt },
                { "LogTime", LogTime },
                { "LogStatus", LogStatus },
                { "EmployeeStatus", EmployeeStatus },
                { "Username", Username },
                { "PostedBy", PostedBy },
                { "PostedDate", PostedDate },
                { "Localization", Localization },
                { "BranchSno", BranchSno },
            };
            return employeeDetail;
        }


        public long EmployeeId { get; set; }
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; } 
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<long> DesignationId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ExpiryDate { get; set; }
        public string FLogin { get; set; }
        public Nullable<long> SNO { get; set; }
        public string QuestionName { get; set; }
        public string QuestionAnswer { get; set; }
        public string AppStatus { get; set; }
        public Nullable<int> LogAtt { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public string LogStatus { get; set; }
        public string EmployeeStatus { get; set; }
        public string Username { get; set; }
        public string PostedBy { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public string Localization { get; set; }
        public Nullable<long> BranchSno { get; set; }
    }
}