using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models;
using JichangeApi.Models.Entities;
using JichangeApi.Services;
using JichangeApi.Services.setup;
using JichangeApi.Services.Companies;
using JichangeApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net;
using System.Web;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ForgotController : SetupBaseController
    {
        private readonly ForgetPasswordService forgetPasswordservice = new ForgetPasswordService();


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Getemail(SingletonEmail singleton)
        {
            List<string> modelStateErrors = ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                UserOtp userOtp = forgetPasswordservice.CreateEmployeeDetailOtpCode(singleton.email);
                return GetSuccessResponse(userOtp);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage GetMobile(SingletonMobile m)
        {

            List<string> modelStateErrors = ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                UserOtp userOtp = forgetPasswordservice.CreateVendorUserOtpCode(m.mobile);
                return GetSuccessResponse(userOtp);
            }
            catch(ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage OtpValidate(ValidateOtpForm m)
        {

            List<string> modelStateErrors = ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (forgetPasswordservice.IsEmployeeContactPoint(m.mobile))
                {
                    EmployeeDetail employee = forgetPasswordservice.ValidateEmployeeDetailOtpCode(m.mobile, m.otp_code);
                    return GetSuccessResponse(employee);
                }
                else
                {
                    VendorUser vendor = forgetPasswordservice.ValidateVendorOtpCode(m.mobile, m.otp_code);
                    return SuccessJsonResponse(vendor.toJson());
                }
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.ToString());
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage ChangePwd(ChangePwdModel m)
        {
            List<string> modelStateErrors = ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (forgetPasswordservice.IsEmployeeContactPoint(m.mobile))
                {
                    EmployeeDetail employeeDetail = forgetPasswordservice.UpdateEmployeeDetailPassword(m.mobile, m.password);
                    return GetSuccessResponse(employeeDetail);
                }
                else
                {
                    VendorUser vendorUser = forgetPasswordservice.UpdateVendorUserPassword(m.mobile, m.password);
                    return GetSuccessResponse(vendorUser);
                }
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.ToString());
            }
        }
    }
}
