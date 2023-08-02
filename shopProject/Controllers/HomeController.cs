using MD.PersianDateTime.Standard;
using ProjectShopCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopProject.Controllers
{
    public class HomeController : Controller
    {
        projectShopDBEntities db = new projectShopDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult slider()
        {
            DateTime datetime = new DateTime(PersianDateTime.Now.Year,PersianDateTime.Now.Month,PersianDateTime.Now.Day, 0, 0, 0);

            var getSlider = db.Slider.Where(s => s.IsActive && s.StartSliderDate <= datetime && s.EndSliderDate >= datetime);
            return PartialView(getSlider);
        }
    }
}