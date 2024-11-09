using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Masters;
using JichangeApi.Models.Entities;
using JichangeApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Services.setup
{
    public class EmployeeDetailService
    {
        /*public UserOtp CreateEmployeeDetailOtpCode(string email)
        {
            try
            {
                EmployeeDetailMaster employeeDetailMaster = new EmployeeDetailMaster();
                EmployeeDetail employee = employeeDetailMaster.FindByEmail(email);
                if (employee == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                int optLength = 6;
                var code = OTP.GenerateOTP(optLength);
                UserOtp userOtp = UserOtp.CreateUserOtp(code, email);
                UserOtpMaster userOtpMaster = new UserOtpMaster();
                long insertedSno = userOtpMaster.AddOtp(userOtp);
                EmailUtils.SendSubjectTextBodyEmail(email, "Verification code",SmsService.FormatOtpMessageBody(email,code));
                return userOtpMaster.FindById(insertedSno);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/

    }
}