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
    [Authorize]
    public class CategoriesController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Categories
        public ActionResult Index(int? superCategoryID)
        {
            IQueryable<Article> categories = null;
            if (superCategoryID != null)
            {
                categories = db.Articles.Where(a => a.Type == "Subcategory").Where(a => a.CategoryID == superCategoryID);
                ViewBag.superCategory = db.Articles.Where(a => a.ArticleID == superCategoryID).SingleOrDefault();
            }
            else
            {
                categories = db.Articles.Where(a => a.Type == "Category");
            }
            return View(categories.ToList());
        }

        // GET: Categories/Create
        public ActionResult Create(int? superCategoryID)
        {
            ViewBag.superCategory = db.Articles.Where(a => a.ArticleID == superCategoryID).SingleOrDefault();
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime,CategoryID")] Article article,
            int? superCategoryID)
        {
            if (ModelState.IsValid)
            {
                if (superCategoryID != null)
                {
                    article.Type = "Subcategory";
                    article.CategoryID = superCategoryID;
                    article.Category = db.Articles.Where(a => a.ArticleID == superCategoryID).SingleOrDefault();
                    article.Category.RelatedArticles.Add(article);
                }
                else
                {
                    article.Type = "Category";
                }
                article.DateAdded = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }

            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: Categories/Edit/5
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

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime,CategoryID")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "Category";
                article.DateAdded = DateTime.Now;
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: Categories/Delete/5
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

        // POST: Categories/Delete/5
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
