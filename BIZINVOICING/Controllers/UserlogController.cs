﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class UserlogController : AdminBaseController
    {
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
        TRACK_DET td = new TRACK_DET();
        // GET: Userlog
        
        public ActionResult Userlog()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        [HttpPost]
        public ActionResult logtimeRep(string stdate, string enddate)
        {

            try
            {

                var result = td.Getfunctiontrackdet(stdate, enddate);
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
                //Ex.ToString();
               // long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
            }

            return returnNull;
        }







    }
}