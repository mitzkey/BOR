using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BOR.DAL;
using BOR.Models;

namespace BOR.Controllers {
    public class ClicksController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Clicks
        public ActionResult Index()
        {
            return View(db.Clicks.ToList());
        }

        // GET: Clicks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Click click = db.Clicks.Find(id);
            if (click == null)
            {
                return HttpNotFound();
            }
            return View(click);
        }

        // GET: Clicks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clicks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClickID,Amount,ArticleType")] Click click)
        {
            if (ModelState.IsValid)
            {
                db.Clicks.Add(click);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(click);
        }

        // GET: Clicks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Click click = db.Clicks.Find(id);
            if (click == null)
            {
                return HttpNotFound();
            }
            return View(click);
        }

        // POST: Clicks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClickID,Amount,ArticleType")] Click click)
        {
            if (ModelState.IsValid)
            {
                db.Entry(click).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(click);
        }

        // GET: Clicks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Click click = db.Clicks.Find(id);
            if (click == null)
            {
                return HttpNotFound();
            }
            return View(click);
        }

        // POST: Clicks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Click click = db.Clicks.Find(id);
            db.Clicks.Remove(click);
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
