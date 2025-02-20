using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;

namespace JichangeApi.Services
{
    public class RoleService
    {
        Payment pay = new Payment();
        public List<Roles> GetRoleList()
        {
            try
            {
                List<Roles> roles = new Roles().GetRole();
                return roles ?? new List<Roles>();
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
