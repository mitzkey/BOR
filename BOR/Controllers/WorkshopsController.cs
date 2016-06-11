using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BOR.DAL;
using BOR.Models;
using System.IO;
using PagedList;
using BOR.ViewModels;
using BOR.Helpers;

namespace BOR.Controllers
{
    public class WorkshopsController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Workshops
        public ActionResult Index(string searchString, string voivodships, string currentFilter, int? page, int? categoryID)
        {
            Click("Warsztaty");

            WorkshopsAndCategories model = new WorkshopsAndCategories();

            var workshops = from a in db.Articles
                            select a;
            workshops = workshops.Where(i => i.Type == "Workshop");

            var categories = from a in db.Articles
                             select a;
            categories = categories.Where(i => i.Type == "WorkshopCategory");

            var voivodshipsQuery = from v in db.Voivodships
                                   orderby v.Name
                                   select v;
            ViewBag.Voivodships = new SelectList(voivodshipsQuery, "Name", "Name", null);

            if (!String.IsNullOrEmpty(voivodships))
            {
                workshops = workshops.Where(i => i.Voivodship.Name == voivodships);
            }

            //filtering searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                workshops = workshops.Where(i => i.Title.Contains(searchString)
                    || i.Text.Contains(searchString)
                    || i.City.Contains(searchString)
                    || i.Email.Contains(searchString)
                    || i.Street.Contains(searchString)
                    || i.Voivodship.Name.Contains(searchString)
                    || i.Www.Contains(searchString));
            }

            //filtering category
            if (categoryID != null)
            {
                workshops = workshops.Where(w => w.CategoryID == categoryID);
            }

            //paging code
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.CurrentCategory = categoryID;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            model.Workshops = workshops.OrderByDescending(s => s.DateAdded).ToPagedList(pageNumber, pageSize);
            model.Categories = categories.ToList();
            
            return View(model);
        }

        // GET: Workshops/Create
        public ActionResult Create()
        {
            PopulateVoivodshipsDropdownList();
            PopulateCategoriesDropdownList();
            return View();
        }

        // POST: Workshops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,CategoryID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,Longitude,Latitude,Www,Email")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "Workshop";
                article.DateAdded = DateTime.Now;
                article.Category = db.Articles.Where(a => a.ArticleID == article.CategoryID).SingleOrDefault();

                //google maps coordinates aquiring:
                string googleMapsAddress = "https://maps.googleapis.com/maps/api/geocode/json?address=" +
                    article.Street + "+" + article.HouseNumber + ",+" + article.City +
                    "&key=AIzaSyDICsovbKs7qwN0r6rqaQTW0-vksuLUaKw";
                var json = new WebClient().DownloadString(googleMapsAddress);
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                if (data.status != "OK")
                {
                    return RedirectToAction("Create", "Workshops");
                }
                article.Latitude = data.results[0].geometry.location.lat;
                article.Longitude = data.results[0].geometry.location.lng;

                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            PopulateVoivodshipsDropdownList();
            PopulateCategoriesDropdownList();
            return View(article);
        }

        // GET: Workshops/Edit/5
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
            PopulateCategoriesDropdownList();
            return View(article);
        }

        // POST: Workshops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,CategoryID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,Longitude,Latitude,Www,Email")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                var article2 = db.Articles.Include("Category").Where(a => a.ArticleID == article.ArticleID).FirstOrDefault();
                //google maps coordinates aquiring:
                string googleMapsAddress = "https://maps.googleapis.com/maps/api/geocode/json?address=" +
                    article.Street + "+" + article.HouseNumber + ",+" + article.City +
                    "&key=AIzaSyDICsovbKs7qwN0r6rqaQTW0-vksuLUaKw";
                var json = new WebClient().DownloadString(googleMapsAddress);
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                if (data.status != "OK")
                {
                    return RedirectToAction("Create", "Workshops");
                }
                article.Latitude = data.results[0].geometry.location.lat;
                article.Longitude = data.results[0].geometry.location.lng;
                article.Category = db.Articles.Where(a => a.ArticleID == article.CategoryID).SingleOrDefault();

                
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            PopulateVoivodshipsDropdownList();
            return View(article);
        }

        // GET: Workshops/Delete/5
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

        // POST: Workshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            Medium media = db.Media.Where(i => i.ArticleID == article.ArticleID).SingleOrDefault();
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/Images/Workshop/" + article.Title), true);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                ;
            }
            if (media != null)
            {
                db.Media.Remove(media);
            }
            foreach (var item in article.Comments)
            {
                Article comment = db.Articles.Find(item.ArticleID);
                db.Articles.Find(id).Comments.Remove(comment);
                db.Articles.Remove(comment);
            }
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Maps show
        public ActionResult Map(int? id)
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

        private void PopulateCategoriesDropdownList(object selectedCategory = null)
        {
            var categoriesQuery = from c in db.Articles.Where(a => a.Type == "WorkshopCategory")
                                  orderby c.Title
                                  select c;
            ViewBag.CategoryID = new SelectList(categoriesQuery, "ArticleID", "Title", selectedCategory);
        }

        public ActionResult Comments(int? id)
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
            WorkshopComment model = new WorkshopComment();
            model.Workshop = article;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(WorkshopComment model, int workshopID)
        {
            if (ModelState.IsValid)
            {
                Censor censor = new Censor();
                model.Comment.Text = censor.CensorText(model.Comment.Text);
                model.Comment.Type = "Comment";
                model.Comment.DateAdded = DateTime.Now;
                db.Articles.Add(model.Comment);
                db.Articles.Find(workshopID).Comments.Add(model.Comment);
                db.SaveChanges();
            }
            return RedirectToAction("Comments", "Workshops", new { id = workshopID });
        }

        public ActionResult DeleteComment(int commentID, int workshopID)
        {
            Article comment = db.Articles.Find(commentID);
            db.Articles.Find(workshopID).Comments.Remove(comment);
            db.Articles.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Comments", "Workshops", new { id = workshopID });
        }

        public ActionResult MainMap()
        {
            return View();
        }
    }
}
