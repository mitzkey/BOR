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
    public class OffRoadGirlsController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: OffRoadGirls
        public ActionResult Index()
        {
            Click("OffRoad Gilrs");

            return View(db.Articles.Where(i => i.Type == "OffRoadGirl").OrderByDescending(m => m.DateAdded).ToList());
        }

        // GET: OffRoadGirls/Details/5
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

        // GET: OffRoadGirls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OffRoadGirls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type,Title,Text,Author,DateAdded")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.DateAdded = DateTime.Now;
                article.Type = "OffRoadGirl";
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }

            return View(article);
        }

        // GET: OffRoadGirls/Edit/5
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
            return View(article);
        }

        // POST: OffRoadGirls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            return View(article);
        }

        // GET: OffRoadGirls/Delete/5
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

        // POST: OffRoadGirls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            IEnumerable<Medium> Media = db.Media.Where(i => i.ArticleID == article.ArticleID);
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/Images/OffRoadGirl/" + article.Title), true);
            }
            catch
            {
                ;
            }
            foreach (var item in Media.ToList())
            {
                foreach (var vote in item.Votes.ToList())
                {
                    db.Votes.Remove(vote);
                }
                db.Media.Remove(item);
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
