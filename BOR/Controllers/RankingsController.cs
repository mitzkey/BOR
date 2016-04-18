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
    public class RankingsController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Rankings
        public ActionResult Index()
        {
            Click("Rankingi");

            var articles = db.Articles.Where(a => a.Type == "Ranking").Include(a => a.Category).Include(a => a.Voivodship).Include(a => a.RelatedArticles);
            return View(articles.ToList());
        }

        // GET: Rankings/Details/5
        public ActionResult Details(int? id)
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

        // GET: Rankings/Create
        public ActionResult Create()
        {
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name");
            return View();
        }

        // POST: Rankings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime,CategoryID")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "Ranking";
                article.DateAdded = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: Rankings/Edit/5
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

        // POST: Rankings/Edit/5
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
                return RedirectToAction("Index");
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: Rankings/Delete/5
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

        // POST: Rankings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            foreach (Article item in article.RelatedArticles)
            {
                db.Articles.Remove(item);
                //tu dodać usuwanie zdjęć artykułów powiązanych
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
