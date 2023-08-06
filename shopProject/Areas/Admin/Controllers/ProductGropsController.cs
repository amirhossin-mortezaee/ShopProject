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
    public class ProductGropsController : Controller
    {
        private projectShopDBEntities db = new projectShopDBEntities();

        // GET: Admin/ProductGrops
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListProductGrops()
        {
            var productGrops = db.ProductGrops.Where(g => g.ParentId == null);
            return PartialView(productGrops.ToList());
        }

        // GET: Admin/ProductGrops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGrops productGrops = db.ProductGrops.Find(id);
            if (productGrops == null)
            {
                return HttpNotFound();
            }
            return View(productGrops);
        }

        // GET: Admin/ProductGrops/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/ProductGrops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,GroupTitle,ParentId")] ProductGrops productGrops)
        {
            if (ModelState.IsValid)
            {
                db.ProductGrops.Add(productGrops);
                db.SaveChanges();
                return PartialView("ListProductGrops", db.ProductGrops.Where(g => g.ParentId == null));
            }

            ViewBag.ParentId = new SelectList(db.ProductGrops, "GroupId", "GroupTitle", productGrops.ParentId);
            return View(productGrops);
        }

        // GET: Admin/ProductGrops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGrops productGrops = db.ProductGrops.Find(id);
            if (productGrops == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.ProductGrops, "GroupId", "GroupTitle", productGrops.ParentId);
            return View(productGrops);
        }

        // POST: Admin/ProductGrops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,GroupTitle,ParentId")] ProductGrops productGrops)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productGrops).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.ProductGrops, "GroupId", "GroupTitle", productGrops.ParentId);
            return View(productGrops);
        }

        // GET: Admin/ProductGrops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGrops productGrops = db.ProductGrops.Find(id);
            if (productGrops == null)
            {
                return HttpNotFound();
            }
            return View(productGrops);
        }

        // POST: Admin/ProductGrops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGrops productGrops = db.ProductGrops.Find(id);
            db.ProductGrops.Remove(productGrops);
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
