using JichangeApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Masters
{
    public class UserOtpMaster
    {
        public long AddOtp(UserOtp userOtp)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                user_otp ps = new user_otp()
                {
                    code = userOtp.code,
                    user_otp_sno = userOtp.user_otp_sno,
                    contact_point = userOtp.contactPoint,
                    posted_date = DateTime.Now
                };
                entities.user_otp.Add(ps);
                entities.SaveChanges();
                return ps.user_otp_sno;
            }
        }

        public UserOtp FindById(long userOtpSno)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var results = entities.user_otp.Find(userOtpSno);
                if (results == null) return null;
                else return UserOtp.CreateUserOtp(results);
            }
        }

        public UserOtp FindByContactPointAndCode(string contactPoint,string code)
        {
            var fiveMinutesAgo = DateTime.Now.AddMinutes(-5);
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var results = entities.user_otp
                    .Where(o => o.contact_point.Equals(contactPoint) 
                    && o.code.Equals(code) 
                    && o.posted_date >= fiveMinutesAgo)
                    .OrderByDescending(o => o.user_otp_sno)
                    .FirstOrDefault();
                if (results == null) return null;
                else return UserOtp.CreateUserOtp(results);
            }
        }
    }
}