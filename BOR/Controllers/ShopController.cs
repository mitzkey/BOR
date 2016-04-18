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
    public class ShopController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Shop
        public ActionResult Index()
        {
            Click("Sklep");

            var viewModel = new ShopView();
            if (db.Articles.Where(a => a.Type == "Shop").Count() > 0)
            {
                viewModel.Shop = db.Articles.Where(a => a.Type == "Shop").Single();
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
            viewModel.Categories = db.Articles.Where(a => a.Type == "Category").ToList();
            return View(viewModel);
        }

        // GET: Shop/Details/5
        [Authorize]
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

        // GET: Shop/Create
        [Authorize]
        public ActionResult Create()
        {
            Article article = db.Articles.Where(a => a.Type == "Shop").SingleOrDefault();
            if (article != null)
            {
                return RedirectToAction("Edit", new { id = article.ArticleID });
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name");
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime")] Article article)
        {
            if (db.Articles.Where(a => a.Type == "Shop").Count() > 0)
            {
                return RedirectToAction("Delete", new { id = db.Articles.Where(a => a.Type == "Shop").Single().ArticleID });
            }
            if (ModelState.IsValid)
            {
                article.Type = "Shop";
                article.DateAdded = DateTime.Now;
                //google maps coordinates aquiring:
                string googleMapsAddress = "https://maps.googleapis.com/maps/api/geocode/json?address=" +
                    article.Street + "+" + article.HouseNumber + ",+" + article.City +
                    "&key=AIzaSyDICsovbKs7qwN0r6rqaQTW0-vksuLUaKw";
                var json = new WebClient().DownloadString(googleMapsAddress);
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                if (data.status != "OK")
                {
                    return RedirectToAction("Create", "Shop");
                }
                article.Latitude = data.results[0].geometry.location.lat;
                article.Longitude = data.results[0].geometry.location.lng;

                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }

            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: Shop/Edit/5
        [Authorize]
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

        // POST: Shop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Title,Text,Author,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "Shop";
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

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            return View(article);
        }

        // GET: Shop/Delete/5
        [Authorize]
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

        // POST: Shop/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            Medium medium = article.Images.FirstOrDefault();
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/Images/Shop/" + article.Title), true);
            }
            catch (System.IO.DirectoryNotFoundException)
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
    }
}
