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

namespace BOR.Controllers
{
    public class WorkshopCategoriesController : Controller
    {
        private BORcontext db = new BORcontext();

        // GET: WorkshopCategories
        public ActionResult Index()
        {
            var categories = db.Articles.Where(a => a.Type == "WorkshopCategory").Include(a => a.Category);
            return View(categories.ToList());
        }


        // GET: WorkshopCategories/Create
        public ActionResult Create()
        {
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name");
            return View();
        }

        // POST: WorkshopCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime,CategoryID")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "WorkshopCategory";
                article.DateAdded = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }

            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: WorkshopCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // POST: WorkshopCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime,CategoryID")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: WorkshopCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: WorkshopCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            Medium media = db.Media.Where(i => i.ArticleID == article.ArticleID).SingleOrDefault();
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/Images/Category/" + article.Title), true);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                ;
            }
            if (media != null)
            {
                db.Media.Remove(media);
            }
            db.Articles.Remove(article);
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
