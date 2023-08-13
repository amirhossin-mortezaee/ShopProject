using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace shopProject.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [Route("logOFF")]
        public ActionResult logOFF()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}