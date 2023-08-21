using ProjectShopCMS.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace shopProject.Controllers
{
    public class ShopCartController : ApiController
    {
        // GET: api/ShopCart
        public int Get()
        {
            List<ShopCartitem> list = new List<ShopCartitem>();
            var Session = HttpContext.Current.Session;
            if (Session["ShopCart"] != null)
            {
                list = Session["ShopCart"] as List<ShopCartitem>;
            }
            return list.Sum(x => x.Count);
        }

        // GET: api/ShopCart/5
        public int Get(int id)
        {
            List<ShopCartitem> list = new List<ShopCartitem>();
            var Session = HttpContext.Current.Session;
            if(Session["ShopCart"]!= null)
            {
                list = Session["ShopCart"] as List<ShopCartitem>;
            }
            if(list.Any(p => p.ProductId == id))
            {
                int index = list.FindIndex(p => p.ProductId == id);
                list[index].Count += 1;
            }
            else
            {
                list.Add(new ShopCartitem()
                {
                    ProductId = id,
                    Count = 1
                });
            }
            Session["ShopCart"] = list;
            return Get();
        }
    }
}
