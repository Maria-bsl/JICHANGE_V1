using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : SetupController
    {
        private readonly RoleService roleService = new RoleService();
        Payment pay = new Payment();

        public HttpResponseMessage GetRolesAct()
        {
            try
            {
                List<Roles> roles = roleService.GetRoleList();
                return GetSuccessResponse(roles);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }
    }
}
