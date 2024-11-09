using JichangeApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Masters
{
    public class VendorMaster
    {
        private class UserCompanyRole
        {
            public company_users User { get; set; }
            public company_master Company { get; set; }
            public roles_master Role { get; set; }
        }

        public VendorUser SignInWithUsernameAndPassword(string username, string password)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var result = entities.company_users.AsEnumerable()
                        .Join(entities.company_master,
                              c => c.comp_mas_sno,
                              c1 => c1.comp_mas_sno,
                              (c, c1) => new { User = c, Company = c1 })
                        .Join(entities.roles_master,
                              uc => uc.User.user_position,
                              ra => ra.sno,
                              (uc, ra) => new UserCompanyRole { User = uc.User, Company = uc.Company, Role = ra })
                        .Where(x => x.User.username == username
                                 && x.User.password == password
                                 && x.Company.status.ToLower() == "approved"
                                 && x.User.log_status == null)
                        .FirstOrDefault();
                if (result == null) return null;
                return VendorUser.CreateVendor(result.User, result.Company, result.Role);
            }
        }

        public VendorUser FindVendorByMobileNumber(string mobileNumber)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var result = entities.company_users.AsEnumerable()
                        .Join(entities.company_master,
                              c => c.comp_mas_sno,
                              c1 => c1.comp_mas_sno,
                              (c, c1) => new { User = c, Company = c1 })
                        .Join(entities.roles_master,
                              uc => uc.User.user_position,
                              ra => ra.sno,
                              (uc, ra) => new UserCompanyRole { User = uc.User, Company = uc.Company, Role = ra })
                        .Where(x => x.User.mobile_no == mobileNumber)
                        .FirstOrDefault();
                if (result == null) return null;
                return VendorUser.CreateVendor(result.User, result.Company, result.Role);
            }
        }

        public VendorUser FindById(long companyId)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var result = entities.company_users.AsEnumerable()
                        .Join(entities.company_master,
                              c => c.comp_mas_sno,
                              c1 => c1.comp_mas_sno,
                              (c, c1) => new { User = c, Company = c1 })
                        .Join(entities.roles_master,
                              uc => uc.User.user_position,
                              ra => ra.sno,
                              (uc, ra) => new UserCompanyRole { User = uc.User, Company = uc.Company, Role = ra })
                        .Where(x => x.User.comp_users_sno == companyId)
                        .FirstOrDefault();
                if (result == null) return null;
                return VendorUser.CreateVendor(result.User, result.Company, result.Role);
            }
        }

        public VendorUser UpdatePassword(long companyId, string password)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var result = entities.company_users.Find(companyId);
                if (result == null) return null;
                result.password = password;
                entities.SaveChanges();
                return FindById(companyId);
            }
        }
    }
}