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
using BOR.Helpers;

namespace BOR.Controllers
{
    public class EventsController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Events
        public ActionResult Index()
        {
            Click("Imprezy");

            return View(db.Articles.Where(a => a.Type == "Event").OrderByDescending(e => e.DateAdded).ToList());
        }

        // GET: Events/Details/5
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

        // GET: Events/Create
        public ActionResult Create()
        {
            PopulateVoivodshipsDropdownList();
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,Www,Email" +
            "StartDate,StartTime,End,EndTime,City,Street,HouseNumber,VoivodshipID,Start,End")] Article article)
        {
            if (ModelState.IsValid)
            {
                Censor censor = new Censor();
                if (article.Text != null)
                {
                    article.Text = censor.CensorText(article.Text);
                }
                article.Type = "Event";
                article.DateAdded = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }

            return View(article);
        }

        // GET: Events/Edit/5
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
            PopulateVoivodshipsDropdownList();
            return View(article);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,Www,"
            + "StartDate,StartTime,End,EndTime,City,Street,HouseNumber,VoivodshipID,Start,End,Email")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            return View(article);
        }

        // GET: Events/Delete/5
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

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            IEnumerable<Medium> media = db.Media.Where(i => i.ArticleID == article.ArticleID);
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/BOR/Images/Event/" + article.Title), true);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                ;
            }
            foreach (var item in media)
            {
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

        private void PopulateVoivodshipsDropdownList(object selectedVoivodship = null)
        {
            var voivodshipsQuery = from v in db.Voivodships
                                   orderby v.Name
                                   select v;
            ViewBag.VoivodshipID = new SelectList(voivodshipsQuery, "VoivodshipID", "Name", selectedVoivodship);
        }
    }
}
