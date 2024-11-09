using JichangeApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Masters
{
    public class EmployeeDetailMaster
    {
        public EmployeeDetail SignInWithUsernameAndPassword(string username,string password)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1()) 
            {
                var results = entities.emp_detail.AsEnumerable()
                        .Join(entities.designation_list,
                        e => e.desg_id,
                        d => d.desg_id,
                        (e, d) => new { BankUser = e, Designation = d })
                        .Where(x => x.BankUser.username.Equals(username)
                        && x.BankUser.pwd.Equals(password))
                        .FirstOrDefault();
                if (results == null) return null;
                else return EmployeeDetail.CreateEmployeeDetail(results.BankUser, results.Designation);
            }
        }

        public EmployeeDetail FindByEmail(string email)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var results = entities.emp_detail.AsEnumerable()
                        .Join(entities.designation_list,
                        e => e.desg_id,
                        d => d.desg_id,
                        (e, d) => new { BankUser = e, Designation = d })
                        .Where(x => x.BankUser.email_id.Equals(email))
                        .FirstOrDefault();
                if (results == null) return null;
                return EmployeeDetail.CreateEmployeeDetail(results.BankUser, results.Designation);
            }
        }

        public EmployeeDetail FindById(long employeeId)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var results = entities.emp_detail.AsEnumerable()
                        .Join(entities.designation_list,
                        e => e.desg_id,
                        d => d.desg_id,
                        (e, d) => new { BankUser = e, Designation = d })
                        .Where(x => x.BankUser.emp_detail_id == employeeId)
                        .FirstOrDefault();
                if (results == null) return null;
                return EmployeeDetail.CreateEmployeeDetail(results.BankUser, results.Designation);
            }
        }

        public EmployeeDetail UpdatePassword(long employeeId,string password)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var result = entities.emp_detail.Find(employeeId);
                if (result == null) return null;
                result.pwd = password;
                entities.SaveChanges();
                return FindById(employeeId);
            }
        }
    }
}