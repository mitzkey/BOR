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
using BOR.ViewModels;

namespace BOR.Controllers
{
    public class GalleriesController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Galleries
        public ActionResult Index()
        {
            Click("Galerie zdjęć");

            return View(db.Articles.Where(i => i.Type == "Gallery").OrderByDescending(m => m.DateAdded).ToList());
        }

        // GET: Galleries/Details/5
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

        // GET: Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type,Title,Text,Author,DateAdded")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.DateAdded = DateTime.Now;
                article.Type = "Gallery";
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }

            return View(article);
        }

        // GET: Galleries/Edit/5
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

        // POST: Galleries/Edit/5
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

        // GET: Galleries/Delete/5
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

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            IEnumerable<Medium> Media = db.Media.Where(i => i.ArticleID == article.ArticleID);
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/BOR/Images/Gallery/" + article.Title), true);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                ;
            }
            
            foreach (var item in Media)
            {
                db.Media.Remove(item);
            }
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Main()
        {
            var model = new GalleryMainView();
            try
            {
                model.Image = db.Articles.Where(a => a.Type == "Gallery").OrderByDescending(g => g.DateAdded).First().Images.First();
                model.Movie = db.Articles.Where(a => a.Type == "CommonMovie").OrderByDescending(m => m.DateAdded).First();
            }
            catch (InvalidOperationException e)
            {
                ViewBag.error = "Nie można znaleźć galerii, zdjęcia lub filmu";
            }
            return View(model);
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
