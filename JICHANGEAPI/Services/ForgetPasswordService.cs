using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Services.Companies;
using JichangeApi.Utilities;
using System;

namespace JichangeApi.Services
{
    public class ForgetPasswordService
    {
        Payment pay = new Payment();

        User_otp ota = new User_otp();
        CompanyUsers cus = new CompanyUsers();
        EMP_DET emp = new EMP_DET();

        public User_otp ValidateOtpHandler(SingletonVOtp m)
        {
            try
            {
                var result1 = cus.CheckUser(m.mobile);

                if (result1 != null)
                {
                    var validateotp = ota.ValidateUser_otp(m.otp_code);
                    var dets = ota.GetDetails(m.otp_code);
                    if (dets != null && (validateotp != false || DateTime.Now > dets.posted_date))
                    {
                        return dets;
                    }
                    else
                    {
                        return null;
                    }
                }
                if (result1 == null)
                {

                    var result = emp.CheckUserBank(m.mobile);
                    if (result != null)
                    {

                        var validateotp = ota.ValidateUser_otp(m.otp_code);
                        var dets = ota.GetDetails(m.otp_code);
                        if (validateotp != false || DateTime.Now > dets.posted_date)
                        {
                            return dets;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }
                return null;

            }
            catch (Exception ex)
            {

                pay.Error_Text = ex.ToString();
                pay.AddErrorLogs(pay);

                return null;
            }
        }

        public CompanyUsers UpdateCompanyUserPassword(string mobile,string password)
        {
            var service = new CompanyUsersService();
            var checkuser = cus.CheckUser(mobile);
            if (checkuser != null)
            {
                CompanyUsers user = new CompanyUsers();
                user.Password = PasswordGeneratorUtil.GetEncryptedData(password);
                user.Mobile = mobile;
                user.CompuserSno = checkuser.CompuserSno;
                CompanyUsers result = service.UpdateCompanyUserPassword(user);
                return result;
            }
            return null;
        }

        public EMP_DET UpdateBankUserPassword(string mobile,string password)
        {
            var empuser = emp.CheckUserBank(mobile);
            if (empuser != null)
            {
                var currentPassword = PasswordGeneratorUtil.DecodeFrom64(empuser.Password);
                if (password == currentPassword) throw new ArgumentException("Old password cannot match new password.");
                
                var user = new EMP_DET();
                user.Detail_Id = empuser.Detail_Id;
                user.Password = PasswordGeneratorUtil.GetEncryptedData(password);
                user.UpdateOnlypwd(user);
                return user.EditEMP(empuser.Detail_Id);
            }
            return null;
        }
    }
}