using ProjectShopCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShopCMS;
using ProjectShopCMS.DAL.ViewModel;

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
        [Route("showProductDetails/{id}")]
        public ActionResult showProductDetails(int id)
        {
            var product = db.Products.Find(id);
            ViewBag.category = product.SelectedProductCateGory.ToList();
            ViewBag.ProductFeature = product.ProductFeature.DistinctBy(f => f.FeatureId).Select(f => new showProductViewModel()
            {
                FeatureTitle = f.Feature.FetureTitle,
                Values = product.ProductFeature.Where(fe => fe.FeatureId == f.FeatureId).Select(fe => fe.Value).ToList()
            }).ToList();
            return View(product);
        }
    }
}