using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Web.Mvc;
namespace BIZINVOICING.Controllers
{
    public class RepCustomerController : AdminBaseController
    {
        // GET: RepCustomer
        CustomerMaster cm = new CustomerMaster();
        public ActionResult RepCustomer()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetcustDetReport(long Comp, long reg, long dist)
        {

            try
            {

                var result = cm.CustGetrep(Comp, reg, dist);
                if (result != null)
                {

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }










    }
}