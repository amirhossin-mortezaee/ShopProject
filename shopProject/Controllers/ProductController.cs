﻿using ProjectShopCMS.DAL;
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
            ViewBag.courentcomment = db.ProductComment.Count();
            return View(product);
        }

        public ActionResult showComments(int id)
        {
            var showComment = db.ProductComment.Where(c => c.ProductId == id);
            return PartialView(showComment);
        }
        [HttpGet]
        public ActionResult CreateComments(int id)
        
        {
            return PartialView(new ProductComment() { ProductId = id });
        }
        [HttpPost]
        public ActionResult CreateComments(ProductComment comments)
        {
            if (ModelState.IsValid)
            {
                comments.CreateDate = DateTime.Now;
                db.ProductComment.Add(comments);
                db.SaveChanges();
                return PartialView("ShowComments", db.ProductComment.Where(c => c.ProductId == comments.ProductId));

            }
            return PartialView(comments);
        }
    }
}