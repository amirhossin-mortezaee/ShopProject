using ProjectShopCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        projectShopDBEntities db = new projectShopDBEntities();
        public ActionResult LastProduct()
        {
            var LaSTProduct = db.Products.OrderByDescending(x => x.CreateDate).Take(8); 
            return PartialView(LaSTProduct);
        }
    }
}