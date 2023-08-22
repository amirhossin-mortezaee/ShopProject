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
                        p.ProductTitle,
                        p.Price
                    }).SingleOrDefault();

                    list.Add(new ShopCardItemViewModel()
                    {
                        Count = item.Count,
                        ImageName = Product.ImageName,
                        ProductId = item.ProductId,
                        Title = Product.ProductTitle,
                        sum = item.Count * Product.Price
                    });
                }
            }
            return PartialView(list);
        }
        List<ShopCardItemViewModel> getremoveproduct()
        {
            List<ShopCardItemViewModel> list = new List<ShopCardItemViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartitem> listShop = (List<ShopCartitem>)Session["ShopCart"];
                foreach (var item in listShop)
                {
                    var Product = db.Products.Where(p => p.ProductId == item.ProductId).Select(p => new
                    {
                        p.ImageName,
                        p.ProductTitle,
                        p.Price
                    }).SingleOrDefault();

                    list.Add(new ShopCardItemViewModel()
                    {
                        Count = item.Count,
                        ImageName = Product.ImageName,
                        ProductId = item.ProductId,
                        Title = Product.ProductTitle,
                        sum = item.Count * Product.Price
                    });
                }
            }
            return list;
        }
        public ActionResult removeProduct(int id, int count)
        {
            List<ShopCartitem> listShop = (List<ShopCartitem>)Session["ShopCart"];
            int index = listShop.FindIndex(p => p.ProductId == id);
            if (count == 0)
            {
                listShop.RemoveAt(index);
            }
            Session["ShopCart"] = listShop;
            return PartialView("ShowCard", getremoveproduct());
        }
        public ActionResult index()
        {
            return View();
        }

        List<ShowOrderDetailViewModel> getListOrderDetail()
        {
            List<ShowOrderDetailViewModel> list = new List<ShowOrderDetailViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartitem> listShop = (List<ShopCartitem>)Session["ShopCart"];
                foreach (var item in listShop)
                {
                    var Product = db.Products.Where(p => p.ProductId == item.ProductId).Select(p => new
                    {
                        p.ImageName,
                        p.ProductTitle,
                        p.Price
                    }).SingleOrDefault();

                    list.Add(new ShowOrderDetailViewModel()
                    {
                        Count = item.Count,
                        ImageName = Product.ImageName,
                        ProductId = item.ProductId,
                        Title = Product.ProductTitle,
                        Price = Product.Price,
                        sum = item.Count * Product.Price
                    });
                }
            }
            return list;
        }

        public ActionResult OrderDetail()
        {
            return PartialView(getListOrderDetail());
        }

        public ActionResult AddOrRemoveItemFromOrderList(int id,int count)
        {
            List<ShopCartitem> listShop = (List<ShopCartitem>)Session["ShopCart"];
            int index = listShop.FindIndex(p => p.ProductId == id);
            if (count == 0)
            {
                listShop.RemoveAt(index);
            }
            else
            {
                listShop[index].Count = count;
            }
            Session["ShopCart"] = listShop;
            return PartialView("OrderDetail", getListOrderDetail());
        }
    }
}