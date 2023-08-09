using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InsertShowImage;
using KooyWebApp_MVC.Classes;
using ProjectShopCMS.DAL;

namespace shopProject.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private projectShopDBEntities db = new projectShopDBEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductGroups = db.ProductGrops.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductTitle,ShortDiscription,TextProduct,Price,ImageName,CreateDate")] Products products,HttpPostedFileBase imageUpload,List<int> selectedProductCategory, string tags)
        {
            if (ModelState.IsValid)
            {
                if(selectedProductCategory == null)
                {
                    ViewBag.ErrorselectedProductCategory = true;
                    ViewBag.ProductGroups = db.ProductGrops.ToList();
                    return View(products);
                }
                products.ImageName = "no-Product-Image.png";
                if(imageUpload != null && imageUpload.IsImage())
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/Images/ProductsImages/"+products.ImageName));

                    ImageResizer Image = new ImageResizer();
                    Image.Resize((Server.MapPath("/Images/ProductsImages/" + products.ImageName)),(Server.MapPath("/Images/ProductsImages/MiniProductImage/" + products.ImageName)));
                }
                products.CreateDate = DateTime.Now;
                db.Products.Add(products);
                foreach(int selected in selectedProductCategory)
                {
                    db.SelectedProductCateGory.Add(new SelectedProductCateGory()
                    {
                        ProductId = products.ProductId,
                        GroupId = selected
                    });
                }
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split('،');
                    foreach (string tagName in tag)
                    {
                        db.ProductTags.Add(new ProductTags()
                        {
                            ProductId=products.ProductId,
                            Tag=tagName.Trim()
                        });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductGroups = db.ProductGrops.ToList();
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductTitle,ShortDiscription,TextProduct,Price,ImageName,CreateDate")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
