﻿using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Utilities;
using System;
using System.Collections.Generic;

namespace JichangeApi.Services.Companies
{
    public class CompanyUsersService
    {
        Payment pay = new Payment();
        private static readonly List<string> TABLE_COLUMNS = new List<string> { "comp_users_sno", "comp_mas_sno", "username",  "user_type", "created_date", "expiry_date",
             "posted_by", "posted_date"};
        public static readonly string TABLE_NAME = "Companyusers";
        public static void AppendInsertAuditTrail(long sno, CompanyUsers user, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), user.Compmassno.ToString(), user.Username, user.Usertype, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(), userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, CompanyUsersService.TABLE_NAME, CompanyUsersService.TABLE_COLUMNS, user.Compmassno);
        }
        public static void AppendUpdateAuditTrail(long sno, CompanyUsers oldUser, CompanyUsers newUser, long userid)
        {
            List<string> oldUserValues = new List<string> { sno.ToString(), oldUser.Compmassno.ToString(), oldUser.Username, oldUser.Usertype, oldUser.CreatedDate.ToString(), oldUser.ExpiryDate.ToString(), userid.ToString(), oldUser.PostedDate.ToString() };
            List<string> newUserValues = new List<string> { sno.ToString(), newUser.Compmassno.ToString(), newUser.Username, newUser.Usertype, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(), userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldUserValues, newUserValues, userid, CompanyUsersService.TABLE_NAME, CompanyUsersService.TABLE_COLUMNS, newUser.Compmassno);
        }
        public static void AppendDeleteAuditTrail(long sno, CompanyUsers user, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), user.Compmassno.ToString(), user.Username, user.Usertype, user.CreatedDate.ToString(), user.ExpiryDate.ToString(), user.PostedBy, user.PostedDate.ToString() };
            Auditlog.deleteAuditTrail(values, userid, CompanyUsersService.TABLE_NAME, CompanyUsersService.TABLE_COLUMNS, user.Compmassno);
        }

        private CompanyUsers CreateCompanyUser(AddCompanyUserForm addCompanyUserForm)
        {
            CompanyUsers user = new CompanyUsers
            {
                CompuserSno = (long)addCompanyUserForm.sno,
                Compmassno = (long)addCompanyUserForm.compid,
                Username = addCompanyUserForm.auname,
                Mobile = addCompanyUserForm.mob,
                Userpos = addCompanyUserForm.pos,
                Email = addCompanyUserForm.mail,
                Fullname = addCompanyUserForm.uname,
                Flogin = "false",
                CreatedDate = System.DateTime.Now,
                ExpiryDate = System.DateTime.Now.AddMonths(3),
                Usertype = addCompanyUserForm.chname
            };
            string password = PasswordGeneratorUtil.CreateRandomPassword(8);
            user.Password = PasswordGeneratorUtil.GetEncryptedData(password);
            user.PostedBy = addCompanyUserForm.userid.ToString();
            return user;
        }
        private List<string> CheckInsertCompanyUserErrors(CompanyUsers user)
        {
            if (user.ValidateduplicateEmail(user.Email))
            {
                return new List<string> { "Email already exist" };
            }
            else if (user.Validateduplicateuser(user.Username))
            {
                return new List<string> { "User already exist" };
            }
            else if (user.IsExistMobileNumber(user.Mobile))
            {
                return new List<string> { "Mobile number already exist" };
            }
            else { return new List<string>(); }
        }
        public List<CompanyUsers> GetCompanyUsersList(SingletonComp singletonComp)
        {
            try
            {
                CompanyUsers companyUsers = new CompanyUsers();
                List<CompanyUsers> result = companyUsers.GetCompanyUsers1((long)singletonComp.compid);
                return result ?? new List<CompanyUsers>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public CompanyUsers EditCompanyUser(long companyUserId)
        {
            try
            {
                CompanyUsers companyUsers = new CompanyUsers();
                CompanyUsers user = companyUsers.EditCompanyUsers(companyUserId);
                return user ?? null;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public CompanyUsers InsertCompanyUser(AddCompanyUserForm addCompanyUserForm)
        {
            try
            {
                CompanyUsers user = CreateCompanyUser(addCompanyUserForm);
                List<string> errors = CheckInsertCompanyUserErrors(user);
                if (errors.Count > 0) { throw new ArgumentException(errors[0]); }
                long addedUser = user.AddCompanyUsers(user);
                if (addedUser > 0)
                {
                    SmsService sms = new SmsService();
                    sms.SendWelcomeSmsToNewUser(user.Username, PasswordGeneratorUtil.DecodeFrom64(user.Password), user.Mobile);
                    EmailUtils.SendActivationEmail(user.Email, user.Fullname, PasswordGeneratorUtil.DecodeFrom64(user.Password), user.Username);
                    AppendInsertAuditTrail(addedUser, user, (long)addCompanyUserForm.userid);
                    return EditCompanyUser(addedUser);
                }
                else throw new ArgumentException("Failed to insert company user");
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);
                throw new Exception(ex.Message);
            }
        }

        public CompanyUsers UpdateCompanyUser(AddCompanyUserForm addCompanyUserForm)
        {
            try
            {
                CompanyUsers user = CreateCompanyUser(addCompanyUserForm);
                CompanyUsers found = EditCompanyUser((long)addCompanyUserForm.sno);
                user.UpdateCompanyUsers(user);
                AppendUpdateAuditTrail((long)addCompanyUserForm.sno, found, user, (long)addCompanyUserForm.userid);

                if (!String.IsNullOrEmpty(user.Email) && user.Email != found.Email)
                {
                    EmailUtils.SendActivationEmail(user.Email, user.Fullname, PasswordGeneratorUtil.DecodeFrom64(found.Password), user.Username);
                }

                if (user.Mobile != found.Mobile)
                {
                    SmsService sms = new SmsService();
                    sms.SendWelcomeSmsToNewUser(user.Username, PasswordGeneratorUtil.DecodeFrom64(found.Password), user.Mobile);
                }

                return EditCompanyUser((long)addCompanyUserForm.sno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public CompanyUsers ResendUserCredentials(string method, long companyUserId)
        {
            try
            {
                CompanyUsers found = EditCompanyUser(companyUserId);
                if (method.ToLower().Equals("email"))
                {
                    EmailUtils.SendActivationEmail(found.Email, found.Fullname, PasswordGeneratorUtil.DecodeFrom64(found.Password), found.Username);
                }
                else if (method.ToLower().Equals("mobile"))
                {
                    SmsService sms = new SmsService();
                    sms.SendWelcomeSmsToNewUser(found.Username, PasswordGeneratorUtil.DecodeFrom64(found.Password), found.Mobile);
                }
                else { }
                return found;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CompanyUsers UpdateCompanyUserPassword(CompanyUsers user)
        {
            try
            {
                var currentUser = EditCompanyUser((long)user.CompuserSno);
                var currentPasswordDecoded = PasswordGeneratorUtil.DecodeFrom64(currentUser.Password);
                var newPasswordDecoded = PasswordGeneratorUtil.DecodeFrom64(user.Password);
                if (currentPasswordDecoded == newPasswordDecoded)
                {
                    throw new ArgumentException("Old password cannot match new password.");
                }
                user.UpdateCompanyUsersP(user);
                AppendUpdateAuditTrail((long)currentUser.CompuserSno, currentUser, user, long.Parse(currentUser.PostedBy));
                return EditCompanyUser((long)user.CompuserSno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public EMP_DET UpdateBankUserPassword(EMP_DET user)
        {
            try
            {
                /* CompanyUsers user = CreateCompanyUser(addCompanyUserForm);
                 CompanyUsers found = user.EditCompanyUsers((long)addCompanyUserForm.sno);
                 if (found != null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                 AppendUpdateAuditTrail((long)addCompanyUserForm.sno, found, user, (long)addCompanyUserForm.userid);
                user.UpdateCompanyUsersP(user);
                return EditCompanyUser((long)user.CompuserSno);*/
                return null;
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public bool IsDuplicateEmail(string email)
        {
            try
            {
                CompanyUsers companyUser = new CompanyUsers();
                bool isDuplicate = companyUser.ValidateduplicateEmail(email);
                return isDuplicate;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public bool IsDuplicateUser(string username)
        {
            try
            {
                CompanyUsers companyUser = new CompanyUsers();
                bool isDuplicate = companyUser.Validateduplicateuser(username);
                return isDuplicate;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public long RemoveCompanyUser(DeleteCompanyUserForm form)
        {
            try
            {
                CompanyUsers companyUser = new CompanyUsers();
                CompanyUsers found = companyUser.EditCompanyUsers((long)form.sno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                AppendDeleteAuditTrail((long)form.sno, found, (long)form.userid);
                found.DeleteCompany((long)form.sno);
                return (long)form.sno;
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
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
