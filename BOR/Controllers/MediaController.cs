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
using System.Data.Entity.Validation;
using System.IO;
using System.Web.Helpers;

namespace BOR.Controllers
{
    public class MediaController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Media
        public ActionResult Index(int? articleID)
        {
            var viewModel = new ArticleMedia();
            Article article = db.Articles.Find(articleID);
            viewModel.Article = article;
            viewModel.Media = db.Media.Where(i => i.ArticleID == articleID.Value).ToList();

            return View(viewModel);
        }

        // GET: Media/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medium medium = db.Media.Find(id);
            if (medium == null)
            {
                return HttpNotFound();
            }
            return View(medium);
        }

        // GET: Media/Create
        public ActionResult Create(int? articleID, int? relatedArticleID)
        {
            var model = new Medium();
            model.ArticleID = articleID.Value;
            model.Article = db.Articles.Where(i => i.ArticleID == articleID).Single();
            if (relatedArticleID != null)
            {
                ViewBag.relatedArticle = db.Articles.Find(relatedArticleID);
            }
            return View(model);
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type,Name,Height,Width,Link,AspectRatio,ArticleID,Article")] Medium medium,
            HttpPostedFileBase upload, int? relatedArticleID)
        {
            if (ModelState.IsValid && upload != null && upload.ContentLength > 0)
            {
                medium.Article = db.Articles.Where(i => i.ArticleID == medium.ArticleID).Single();

                var serverFileName = Guid.NewGuid().ToString() + "_" + upload.FileName;
                string directory = "/Images/" + medium.Article.Type + "/" + medium.Article.Title + "/";
                medium.Link = Path.Combine(directory, serverFileName);
                Directory.CreateDirectory(Server.MapPath("/" + directory));
                upload.SaveAs(Server.MapPath("/" + medium.Link));

                var webImage = new WebImage(Server.MapPath("/" + medium.Link));
                webImage.Resize(webImage.Width / 3, webImage.Height / 3);
                medium.MiniatureLink = Path.Combine(directory, "MIN_" + serverFileName);
                webImage.Save(Server.MapPath("/" + medium.MiniatureLink));

                db.Media.Add(medium);
                medium.Article.Images.Add(medium);
                db.SaveChanges();
                if (medium.Article.Type == "Workshop")
                {
                    return RedirectToAction("Index", "Workshops", null);
                }
                else if (medium.Article.Type == "Event")
                {
                    return RedirectToAction("Index", "Events", null);
                }
                else if (medium.Article.Type == "Ad")
                {
                    return RedirectToAction("Index", "Ads", null);
                }
                else if (medium.Article.Type == "BottomAd")
                {
                    return RedirectToAction("Index", "BottomAd", null);
                }
                else if (medium.Article.Type == "Shop" || medium.Article.Type == "Part")
                {
                    return RedirectToAction("Index", "Parts", null);
                }
                else if (medium.Article.Type == "RankingPart") {
                    return RedirectToAction("Index", "RankingParts", new { RankingID = relatedArticleID });
                }
                else if (medium.Article.Type == "Service")
                {
                    return RedirectToAction("Index", "Services", null);
                }
                else if (medium.Article.Type == "Driving course")
                {
                    return RedirectToAction("DrivingCourse", "Services", null);
                }
                return RedirectToAction("Index", new { articleID = medium.ArticleID });
            }
            return RedirectToAction("Create", new { articleID = medium.ArticleID });
        }

        // GET: Media/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medium medium = db.Media.Find(id);
            if (medium == null)
            {
                return HttpNotFound();
            }
            return View(medium);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MediumID,Type,Name,Height,Width,Link,AspectRation,MiniatureLink,ArticleID")] Medium medium)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medium).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { articleID = medium.ArticleID });
            }
            return View(medium);
        }

        // GET: Media/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medium medium = db.Media.Find(id);
            if (medium == null)
            {
                return HttpNotFound();
            }
            return View(medium);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medium medium = db.Media.Find(id);
            System.IO.File.Delete(Server.MapPath(medium.Link));
            System.IO.File.Delete(Server.MapPath(medium.MiniatureLink));
            int articleID = medium.ArticleID;
            foreach (var vote in medium.Votes)
            {
                db.Votes.Remove(vote);
            }
            db.Media.Remove(medium);
            db.SaveChanges();
            return RedirectToAction("Index", new { articleID = articleID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Vote(int id)
        {
            //HttpCookie cookie = new HttpCookie("DidVote");
            //cookie.Value = "true";
            //cookie.Expires = DateTime.Now.AddMonths(1);
            //Response.SetCookie(cookie);

            Vote vote = new Vote();
            Medium medium = db.Media.Find(id);
            vote.Date = DateTime.Now;
            vote.Medium = medium;
            db.Votes.Add(vote);
            medium.Votes.Add(vote);
            db.SaveChanges();

            return RedirectToAction("Index", new { articleID = medium.ArticleID });
        }
    }
}
