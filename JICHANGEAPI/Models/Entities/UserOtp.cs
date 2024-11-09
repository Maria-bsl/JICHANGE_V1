using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.Entities
{
    public class UserOtp
    {
        public static UserOtp CreateUserOtp(string code,string mobileNumber)
        {
            UserOtp userOtp = new UserOtp();
            userOtp.code = code;
            userOtp.contactPoint = mobileNumber;
            userOtp.posted_date = DateTime.Now;
            return userOtp;
        }

        public static UserOtp CreateUserOtp(user_otp otp)
        {
            if (otp == null) return null;
            UserOtp userOtp = new UserOtp();
            userOtp.code = otp.code;
            userOtp.contactPoint = otp.contact_point;
            userOtp.user_otp_sno = otp.user_otp_sno;
            userOtp.posted_date = otp.posted_date;
            return userOtp;
        }

        public long user_otp_sno { get; set; }
        public string code { get; set; }
        public string contactPoint { get; set; }
        public DateTime? posted_date { get; set; }
    }
}