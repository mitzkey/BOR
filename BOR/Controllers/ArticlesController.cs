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
    public class ArticlesController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Articles
        public ActionResult Index()
        {
            Click("Artykuły");
            return View(db.Articles.Where(a => a.Type == "Article").OrderByDescending(m => m.DateAdded).ToList());
        }

        [Authorize]

        // GET: Articles/Details/5
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

        [Authorize]
        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "Article";
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Articles");
            }

            return View(article);
        }

        [Authorize]
        // GET: Articles/Edit/5
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

        [Authorize]
        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "Article";
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Articles");
            }
            return View(article);
        }

        [Authorize]
        // GET: Articles/Delete/5
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

        // POST: Articles/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            IEnumerable<Medium> media = article.Images;
            foreach (var medium in media)
            {
                System.IO.Directory.Delete(Server.MapPath("/Images/Article/" + article.Title), true);
            }
            db.Media.RemoveRange(media);
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
