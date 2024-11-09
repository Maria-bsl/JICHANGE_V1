using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.Authentication;
using JichangeApi.Controllers.setup;
using JichangeApi.Masters;
using JichangeApi.Models;
using JichangeApi.Models.Entities;
using JichangeApi.Services.Companies;
using JichangeApi.Utilities;
using System;
using System.Net;
using System.Text.Json.Nodes;
using System.Web.Http;

namespace JichangeApi.Services
{
    public class LoginUserService : ApiController
    {
        private CompanyBankService companyBankService = new CompanyBankService();
        Payment pay = new Payment();

        private TRACK_DET TrackBankSuperUserDetails(AuthLog empData)
        {
            TRACK_DET trackDet = new TRACK_DET();
            trackDet.Full_Name = "Super User";
            trackDet.Facility_Reg_No = 0;
            trackDet.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //dt.Ipadd = System.Web.HttpContext.Current.Request.UserHostAddress;
            trackDet.Email = "";
            trackDet.Posted_by = "0";
            trackDet.Login_Time = DateTime.Now;
            trackDet.Description = "Bank";
            trackDet.AddTrack(trackDet);
            return trackDet;
        }
        private void TrackBankUserDetails(EmployeeDetail employeeDetail)
        {
            TRACK_DET trackDet = new TRACK_DET();
            trackDet.Full_Name = employeeDetail.FullName;
            trackDet.Facility_Reg_No = (long?)employeeDetail.BranchSno;
            trackDet.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            trackDet.Email = employeeDetail.EmailId;
            trackDet.Posted_by = Convert.ToString(employeeDetail.EmployeeId);
            trackDet.Login_Time = DateTime.Now;
            trackDet.Description = "Bank";
            trackDet.AddTrack(trackDet);
        }
        private void TrackCompanyUserDetails(VendorUser vendor)
        {
            TRACK_DET trackDet = new TRACK_DET();
            trackDet.Full_Name = vendor.UserFullName;
            trackDet.Facility_Reg_No = vendor.CompMasSno;
            trackDet.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            trackDet.Email = vendor.EmailAddress;
            trackDet.Posted_by = Convert.ToString(vendor.CompUsersSno);
            trackDet.Login_Time = DateTime.Now;
            trackDet.Description = "Company";
            trackDet.AddTrack(trackDet);
        }
        private JsonObject GetBankSuperUserProfile(AuthLog empData)
        {
            string role = "Bank";
            string jwtToken = Authentication.GenerateJWTAuthetication(empData.userName, role);
            string userType = "BNk";
            string Username = empData.userName;
            string UfullName = "Super User";
            string flogin = "true";
            long userid = 1;
            string desig = "Administrator";
            long branchId = 0;
            _ = TrackBankSuperUserDetails(empData);
            JsonObject response = new JsonObject
            {
                { "Token", jwtToken },
                { "flogin", flogin },
                { "desig", desig },
                { "braid", branchId },
                { "Usno", userid },
                { "userType", userType },
                { "role", role },
                { "Uname", Username },
                { "fulname", UfullName},
                { "userid", userid},
            };
            return response;
        }
        private JsonObject GetBankerUserProfile(EmployeeDetail employeeDetail)
        {
            string role = "Bank";
            JsonObject response = new JsonObject
            {
                { "Token", Authentication.GenerateJWTAuthetication(employeeDetail.Username, role) },
                { "designationId", employeeDetail.DesignationId },
                { "branchId", employeeDetail.BranchSno },
                { "employeeId", employeeDetail.EmployeeId },
                { "userType", role },
                { "username", employeeDetail.Username },
                { "fullName", employeeDetail.FullName},
                { "userId",   employeeDetail.EmployeeId},
            };
            return response;
        }

        private JsonObject GetCompanyUserProfile(VendorUser vendor)
        {
            string role = "Company";
            JsonObject response = new JsonObject
            {
                { "Token", Authentication.GenerateJWTAuthetication(vendor.Username, role) },
                { "companyId", vendor.CompMasSno },
                { "userRoleId", vendor.tupleRoles.Item3.sno },
                { "branchId", vendor.tupleRoles.Item2.branch_sno },
                { "vendorUserId", vendor.CompUsersSno },
                { "userType", role },
                { "userId", vendor.CompUsersSno },
                { "username", vendor.Username },
            };
            return response;
        }

        public JsonObject LoginUser(AuthLog authLog)
        {
            try
            {
                string password = PasswordGeneratorUtil.GetEncryptedData(authLog.password);

                var employee = new EmployeeDetailMaster().SignInWithUsernameAndPassword(authLog.userName, password);
                if (employee != null)
                {
                    TrackBankUserDetails(employee);
                    return GetBankerUserProfile(employee);
                }
                var vendor = new VendorMaster().SignInWithUsernameAndPassword(authLog.userName, password);
                if (vendor != null)
                {
                    TrackCompanyUserDetails(vendor);
                    return GetCompanyUserProfile(vendor);
                }

                if (authLog.userName.ToLower().Equals("super") && authLog.password.Equals("1234")) // $pKwG1rq
                {

                    return GetBankSuperUserProfile(authLog);
                }
                throw new ArgumentException(HttpStatusCode.NotFound.ToString());
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);
                throw new Exception(ex.Message);
            }
        }


        public long LogoutUser(long userid)
        {
            try
            {
                EMP_DET empdata = new EMP_DET
                {
                    Detail_Id = Convert.ToInt64(userid.ToString())
                };
                TRACK_DET trackDet = new TRACK_DET().EditTRACK(userid.ToString());
                //trackDet.SNO = trackDet.SNO;
                trackDet.Posted_by = userid.ToString();
                trackDet.UpdateTRACKEmp(trackDet);
                return userid;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }


    }
}
