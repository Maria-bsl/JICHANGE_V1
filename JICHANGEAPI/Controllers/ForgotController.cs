﻿using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models;
using JichangeApi.Services;
using JichangeApi.Services.Companies;
using JichangeApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ForgotController : SetupBaseController
    {
        private readonly dynamic returnNull = null;
        // GET: Forgot
        EMP_DET emp = new EMP_DET();
        User_otp ota = new User_otp();
        CompanyUsers cus = new CompanyUsers();
        SmsService sms = new SmsService();
        Payment pay = new Payment();
        
        private readonly CompanyUsersService companyUsersService = new CompanyUsersService();
        private readonly ForgetPasswordService forgetPasswordservice = new ForgetPasswordService();


        [HttpPost]
        public HttpResponseMessage Getemail(String Sno)
        {
            try
            {
                // GET mobile
                var result = emp.FPassword(Sno);
                if (result == null)
                {
                    result = emp.FPasswordE(Sno);

                }
                if (result != null)
                {
                    //SendActivationEmail(result.Email_Address, result.Full_Name, DecodeFrom64(result.Password), result.User_name);
                    /*ch.User_SNO = result.User_SNO;
                    ch.Email = Sno;
                    ch.UpdateUsers(ch);*/
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });

                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> {"Failed" } });
                }
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Ex.ToString();
            }

            return returnNull;
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage GetMobile(SingletonMobile m)
        {

            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try { 
                    var result1 = cus.CheckUser(m.mobile);

                    if (result1 != null)
                    {
                        var otp = Services.OTP.GenerateOTP(6);
                        ota.mobile_no = m.mobile;
                        ota.code = otp;
                        ota.AddOtp(ota);

                         sms.SendOTPSmsToDeliveryCustomer(m.mobile, otp);

                        return GetSuccessResponse(ota);

                    }
            }catch(Exception ex) 
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                ex.ToString();

            }

            return returnNull;
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage OtpValidate(SingletonVOtp m)
        {

            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                User_otp user_Otp = new User_otp();
                user_Otp = forgetPasswordservice.ValidateOtpHandler(m);
                if(user_Otp != null) { return GetSuccessResponse(user_Otp); }else { return GetCustomErrorMessageResponse(new List<string> { "User Token has expired, Try Again"}); }
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                ex.ToString();
                return GetServerErrorResponse(ex.ToString());

            }

            
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage ChangePwd(ChangePwdModel m)
        {
           
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            var checkuser = cus.CheckUser(m.mobile);
            var empuser = emp.CheckUserBank(m.mobile);
            CompanyUsers user = new CompanyUsers();
            user.Password = PasswordGeneratorUtil.GetEncryptedData(m.password);
            user.Mobile = m.mobile;
            user.CompuserSno = checkuser.CompuserSno;
            var result = companyUsersService.UpdateCompanyUserPassword(user);
            return GetSuccessResponse(result);
           

        }



        /*private void SendActivationEmail(String email, String auname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = stp.getSMTPText();
                    if (m != null)
                    {
                        //var data = em.getEMAILst("2");
                        mm.To.Add(email);
                        mm.From = new MailAddress(m.From_Address);
                        //mm.Subject = data.Subject;
                        mm.Subject = "Forgot Password Details";
                        //drt = data.Subject;
                        */
        /*var urlBuilder =
                       new System.UriBuilder(Request.Url.AbsoluteUri)
                       {
                           Path = Url.Action("Loginnew", "Loginnew"),
                           Query = null,
                       };

                        Uri uri = urlBuilder.Uri;
                        string url = urlBuilder.ToString();*//*
                        //String body = data.Email_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");
                        String body = "Dear " + auname + "<br>";
                        body += "User Name:" + uname + "<br>";
                        body += "Password:" + pwd + "<br><br>";
                        body += "Admin" + "<br>";
                        body += "Invoicing Portal" + "<br>";
                        mm.Body = body;
                        mm.IsBodyHtml = true;

                        smtp.Host = m.SMTP_Address;
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.From_Address, m.SMTP_Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Send(mm);
                    }
                }
            }
            catch (Exception Ex)
            {
                Utilites.logfile("Forgot", drt, Ex.ToString());
            }

        }*/


        public static string DecodeFrom64(string password)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }


    }
}