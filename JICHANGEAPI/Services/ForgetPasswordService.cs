using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Masters;
using JichangeApi.Models;
using JichangeApi.Models.Entities;
using JichangeApi.Utilities;
using Swashbuckle.Swagger;
using System;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Web;

namespace JichangeApi.Services
{
    public class ForgetPasswordService
    {
        public bool IsEmployeeContactPoint(string contactPoint)
        {
            return contactPoint.Contains("@");
        }

        public string GetCurrentUrlBase()
        {
            string scheme = HttpContext.Current.Request.Url.Scheme;
            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;
            return $"{scheme}://{host}:{port}";
        }

        public UserOtp CreateVendorUserOtpCode(string mobileNumber)
        {
            VendorMaster vendorMaster = new VendorMaster();
            VendorUser vendor = vendorMaster.FindVendorByMobileNumber(mobileNumber);
            if (vendor == null) throw new ArgumentException(HttpStatusCode.NotFound.ToString());
            int optLength = 6;
            var code = OTP.GenerateOTP(optLength);
            UserOtp userOtp = UserOtp.CreateUserOtp(code, mobileNumber);
            UserOtpMaster userOtpMaster = new UserOtpMaster();
            long insertedSno = userOtpMaster.AddOtp(userOtp);
            new SmsService().SendOTPSmsToDeliveryCustomer(mobileNumber, code); //send sms message
            return userOtpMaster.FindById(insertedSno);
        }

        public UserOtp CreateEmployeeDetailOtpCode(string email)
        {
            EmployeeDetailMaster employeeDetailMaster = new EmployeeDetailMaster();
            EmployeeDetail employee = employeeDetailMaster.FindByEmail(email);
            if (employee == null) throw new ArgumentException(HttpStatusCode.NotFound.ToString());
            int optLength = 6;
            var code = OTP.GenerateOTP(optLength);
            UserOtp userOtp = UserOtp.CreateUserOtp(code, email);
            UserOtpMaster userOtpMaster = new UserOtpMaster();
            long insertedSno = userOtpMaster.AddOtp(userOtp);
            string emailEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(email));
            string url = GetCurrentUrlBase() + $"/auth/otp?e={emailEncoded}";
            EmailUtils.SendSubjectTextBodyEmail(email, "Verification code", SmsService.FormatOtpMessageBody(employee.FullName, code, url));
            return userOtpMaster.FindById(insertedSno);
        }

        public EmployeeDetail ValidateEmployeeDetailOtpCode(string email,string code)
        {
            UserOtpMaster userOtpMaster = new UserOtpMaster();
            UserOtp userOtp = userOtpMaster.FindByContactPointAndCode(email, code);
            if (userOtp == null) throw new ArgumentException(HttpStatusCode.NotFound.ToString());
            EmployeeDetail employee = new EmployeeDetailMaster().FindByEmail(email);
            if (employee == null) throw new Exception(HttpStatusCode.InternalServerError.ToString());
            return employee;
        }

        public VendorUser ValidateVendorOtpCode(string mobile,string code)
        {
            UserOtp userOtp = new UserOtpMaster().FindByContactPointAndCode(mobile, code);
            if (userOtp == null) throw new ArgumentException(HttpStatusCode.NotFound.ToString());
            VendorMaster vendorMaster = new VendorMaster();
            VendorUser vendor = vendorMaster.FindVendorByMobileNumber(mobile);
            if (vendor == null) throw new Exception(HttpStatusCode.InternalServerError.ToString());
            return vendor;
        }

        public EmployeeDetail UpdateEmployeeDetailPassword(string contactPoint,string password)
        {
            EmployeeDetailMaster employeeDetailMaster = new EmployeeDetailMaster();
            EmployeeDetail employeeDetail = employeeDetailMaster.FindByEmail(contactPoint);
            if (employeeDetail == null) throw new ArgumentException(HttpStatusCode.NotFound.ToString());
            string encodedPassword = PasswordGeneratorUtil.GetEncryptedData(password);
            if (encodedPassword.Equals(employeeDetail.Password)) throw new ArgumentException(HttpStatusCode.NotAcceptable.ToString());
            employeeDetail = employeeDetailMaster.UpdatePassword(employeeDetail.EmployeeId, encodedPassword);
            return employeeDetail;
        }

        public VendorUser UpdateVendorUserPassword(string contactPoint,string password)
        {
            VendorMaster vendorMaster = new VendorMaster();
            VendorUser vendorUser = vendorMaster.FindVendorByMobileNumber(contactPoint);
            if (vendorUser == null) throw new ArgumentException(HttpStatusCode.NotFound.ToString());
            string encodedPassword = PasswordGeneratorUtil.GetEncryptedData(password);
            if (encodedPassword.Equals(vendorUser.Password)) throw new ArgumentException(HttpStatusCode.NotAcceptable.ToString());
            vendorUser = vendorMaster.UpdatePassword(vendorUser.CompUsersSno, encodedPassword);
            return vendorUser;
        }
    }
}