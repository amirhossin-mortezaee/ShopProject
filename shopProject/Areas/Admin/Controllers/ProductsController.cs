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
        public ActionResult Create([Bind(Include = "ProductId,ProductTitle,ShortDiscription,TextProduct,Price,ImageName,CreateDate")] Products products, HttpPostedFileBase imageUpload, List<int> selectedProductCategory, string tags)
        {
            if (ModelState.IsValid)
            {
                if (selectedProductCategory == null)
                {
                    ViewBag.ErrorselectedProductCategory = true;
                    ViewBag.ProductGroups = db.ProductGrops.ToList();
                    return View(products);
                }
                products.ImageName = "no-Product-Image.png";
                if (imageUpload != null && imageUpload.IsImage())
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/Images/ProductsImages/" + products.ImageName));

                    ImageResizer Image = new ImageResizer();
                    Image.Resize((Server.MapPath("/Images/ProductsImages/" + products.ImageName)), (Server.MapPath("/Images/ProductsImages/MiniProductImage/" + products.ImageName)));
                }
                products.CreateDate = DateTime.Now;
                db.Products.Add(products);
                foreach (int selected in selectedProductCategory)
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
                            ProductId = products.ProductId,
                            Tag = tagName.Trim()
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
            ViewBag.tags = string.Join("،", products.ProductTags.Select(t => t.Tag).ToList());
            ViewBag.CategorySelected = products.SelectedProductCateGory.ToList();
            ViewBag.ProductGroups = db.ProductGrops.ToList();
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductTitle,ShortDiscription,TextProduct,Price,ImageName,CreateDate")] Products products, HttpPostedFileBase imageUpload, List<int> selectedProductCategory, string tags)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null && imageUpload.IsImage())
                {
                    if(products.ImageName != "no-Product-Image.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductsImages/" + products.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductsImages/MiniProductImage/" + products.ImageName));
                    }
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/Images/ProductsImages/" + products.ImageName));

                    ImageResizer Image = new ImageResizer();
                    Image.Resize((Server.MapPath("/Images/ProductsImages/" + products.ImageName)), (Server.MapPath("/Images/ProductsImages/MiniProductImage/" + products.ImageName)));
                }
                db.Entry(products).State = EntityState.Modified;
                db.ProductTags.Where(t => t.ProductId == products.ProductId).ToList().ForEach(t => db.ProductTags.Remove(t));
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split('،');
                    foreach (string tagName in tag)
                    {
                        db.ProductTags.Add(new ProductTags()
                        {
                            ProductId = products.ProductId,
                            Tag = tagName.Trim()
                        });
                    }
                }

                db.SelectedProductCateGory.Where(g => g.ProductId == products.ProductId).ToList().ForEach(g => db.SelectedProductCateGory.Remove(g));
                foreach (int selected in selectedProductCategory)
                {
                    db.SelectedProductCateGory.Add(new SelectedProductCateGory()
                    {
                        ProductId = products.ProductId,
                        GroupId = selected
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tags = string.Join("،", products.ProductTags.Select(t => t.Tag).ToList());
            ViewBag.CategorySelected = products.SelectedProductCateGory.ToList();
            ViewBag.ProductGroups = db.ProductGrops.ToList();
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

        [HttpGet]
        public ActionResult Gallery(int id)
        {
            ViewBag.Galleries = db.ProductGallery.Where(p => p.ProductId == id).ToList();
            return View(new ProductGallery() { ProductId = id });
        }

        [HttpPost]
        public ActionResult Gallery(ProductGallery gallery, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null && imageUpload.IsImage())
                {
                    gallery.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/Images/ProductsImages/" + gallery.ImageName));

                    ImageResizer Image = new ImageResizer();
                    Image.Resize((Server.MapPath("/Images/ProductsImages/" + gallery.ImageName)), (Server.MapPath("/Images/ProductsImages/MiniProductImage/" + gallery.ImageName)));
                    db.ProductGallery.Add(gallery);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Gallery", new { id = gallery.ProductId });
        }

        public ActionResult DeleteGallery(int id)
        {
            var gallery = db.ProductGallery.Find(id);
            System.IO.File.Delete(Server.MapPath("/Images/ProductsImages/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("/Images/ProductsImages/MiniProductImage/" + gallery.ImageName));
            db.ProductGallery.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("Gallery", new { id = gallery.ProductId });
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
