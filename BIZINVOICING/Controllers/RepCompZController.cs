﻿using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Web.Mvc;
namespace BIZINVOICING.Controllers
{
    public class RepCompZController : LangcoController
    {
        // GET: RepCompZ
        INVOICE inv = new INVOICE();
        public ActionResult RepCompZ()
        {
            if (Session["sessComp"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetInvReport(long Inst, string stdate, string enddate)
        {

            try
            {

                var result = inv.GetZrep1(Inst, stdate, enddate);


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