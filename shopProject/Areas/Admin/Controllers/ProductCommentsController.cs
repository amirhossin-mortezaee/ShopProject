using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectShopCMS.DAL;

namespace shopProject.Areas.Admin.Controllers
{
    public class ProductCommentsController : Controller
    {
        private projectShopDBEntities db = new projectShopDBEntities();

        // GET: Admin/ProductComments
        public ActionResult Index()
        {
            var productComment = db.ProductComment.Include(p => p.ProductComment2).Include(p => p.Products);
            return View(productComment.ToList());
        }

        // GET: Admin/ProductComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductComment productComment = db.ProductComment.Find(id);
            if (productComment == null)
            {
                return HttpNotFound();
            }
            return View(productComment);
        }

        // GET: Admin/ProductComments/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.ProductComment, "CommentId", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductTitle");
            return View();
        }

        // POST: Admin/ProductComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,parentId,productId,Name,Email,CreateDate,Comment")] ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                db.ProductComment.Add(productComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parentId = new SelectList(db.ProductComment, "CommentId", "Name", productComment.ParentId);
            ViewBag.productId = new SelectList(db.Products, "ProductId", "ProductTitle", productComment.ProductId);
            return View(productComment);
        }

        // GET: Admin/ProductComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductComment productComment = db.ProductComment.Find(id);
            if (productComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.ProductComment, "CommentId", "Name", productComment.ParentId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductTitle", productComment.ProductId);
            return View(productComment);
        }

        // POST: Admin/ProductComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,ParentId,ProductId,Name,Email,CreateDate,Comment")] ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.ProductComment, "CommentId", "Name", productComment.ParentId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductTitle", productComment.ProductId);
            return View(productComment);
        }

        // GET: Admin/ProductComments/Delete/5
        public void Delete(int id)
        {
            var ParentComment = db.ProductComment.Find(id);
            if (ParentComment.ProductComment1.Any())
            {
                foreach (var ReplyComment in db.ProductComment.Where(c => c.ParentId == id))
                {
                    db.ProductComment.Remove(ReplyComment);
                }
            }
            db.ProductComment.Remove(ParentComment);
            db.SaveChanges();
        }

        // POST: Admin/ProductComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductComment productComment = db.ProductComment.Find(id);
            db.ProductComment.Remove(productComment);
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
