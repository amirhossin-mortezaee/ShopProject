using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopProject.Controllers
{
    public class ManagementEmailController : Controller
    {
        // GET: ManagementEmail
        public ActionResult ActivationEmail()
        {
            return PartialView();
        }
    }
}