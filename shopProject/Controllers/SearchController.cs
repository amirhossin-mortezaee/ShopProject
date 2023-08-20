using ProjectShopCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopProject.Controllers
{
    public class SearchController : Controller
    {
        projectShopDBEntities db = new projectShopDBEntities();
        // GET: Search
        public ActionResult Index(string q)
        {
            List<Products> listSearch = new List<Products>();
            listSearch.AddRange(db.ProductTags.Where(t => t.Tag == q).Select(t => t.Products).ToList());
            listSearch.AddRange(db.Products.Where(t => t.ProductTitle.Contains(q)||t.TextProduct.Contains(q)||t.ShortDiscription.Contains(q)).ToList());
            ViewBag.SearchParameter = q;
            return View(listSearch.Distinct());
        }
    }
}