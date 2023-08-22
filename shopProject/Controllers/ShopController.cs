using ProjectShopCMS.DAL;
using ProjectShopCMS.DAL.Model;
using ProjectShopCMS.DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopProject.Controllers
{
    public class ShopController : Controller
    {
        projectShopDBEntities db = new projectShopDBEntities();
        // GET: Shop
        public ActionResult ShowCard()
        {
            List<ShopCardItemViewModel> list = new List<ShopCardItemViewModel>();
            if(Session["ShopCart"] != null)
            {
                List<ShopCartitem> listShop = (List<ShopCartitem>)Session["ShopCart"];
                foreach (var item in listShop)
                {
                    var Product = db.Products.Where(p => p.ProductId == item.ProductId).Select(p => new 
                    { 
                        p.ImageName,
                        p.ProductTitle
                    }).SingleOrDefault();

                    list.Add(new ShopCardItemViewModel()
                    {
                        Count = item.Count,
                        ImageName = Product.ImageName,
                        ProductId = item.ProductId,
                        Title = Product.ProductTitle
                    });
                }
            }
            return PartialView(list);
        }
    }
}