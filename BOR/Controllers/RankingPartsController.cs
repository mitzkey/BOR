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
    public class RankingPartsController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: RankingParts
        public ActionResult Index(int RankingID)
        {
            var model = new RankingParts();
            model.Ranking = db.Articles.Find(RankingID);
            model.Parts = model.Ranking.RelatedArticles;
            return View(model);
        }

        // GET: RankingParts/Details/5
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

        // GET: RankingParts/Create
        public ActionResult Create(int rankingID)
        {
            ViewBag.ranking = db.Articles.Find(rankingID);
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name");
            return View();
        }

        // POST: RankingParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime,CategoryID")] Article article,
            int rankingID)
        {
            if (ModelState.IsValid)
            {
                article.Type = "RankingPart";
                article.DateAdded = DateTime.Now;
                db.Articles.Add(article);
                db.Articles.Find(rankingID).RelatedArticles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Create", "Media", new { articleID = article.ArticleID, relatedArticleID = db.Articles.Find(rankingID).ArticleID });
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: RankingParts/Edit/5
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

        // POST: RankingParts/Edit/5
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

        // GET: RankingParts/Delete/5
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

        // POST: RankingParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            Medium medium = article.Images.FirstOrDefault();
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/BOR/Images/RankingPart/" + article.Title), true);
            }
            catch
            {
                ;
            }
            if (medium != null)
            {
                db.Media.Remove(medium);
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

        public ActionResult Vote(int id, int relatedArticle)
        {
            //HttpCookie cookie = new HttpCookie("DidVote");
            //cookie.Value = "true";
            //cookie.Expires = DateTime.Now.AddMonths(1);
            //Response.SetCookie(cookie);

            Vote vote = new Vote();
            Article rankingPart = db.Articles.Find(id);
            vote.Date = DateTime.Now;
            vote.Article = rankingPart;
            db.Votes.Add(vote);
            rankingPart.Votes.Add(vote);
            db.SaveChanges();

            return RedirectToAction("Index", "Rankings", new { RankingID = relatedArticle });
        }
    }
}
