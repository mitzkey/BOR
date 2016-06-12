using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BOR.DAL;
using BOR.Models;
using BOR.ViewModels;
using PagedList;
using System.Net.Mail;
using System.Data.Entity.Infrastructure;

namespace BOR.Controllers
{
    public class PartsController : ParentController
    {
        private BORcontext db = new BORcontext();

        // GET: Parts
        public ActionResult Index(string searchString, string categories, string currentFilter, int? page)
        {
            Article category = null;
            var parts = from a in db.Articles.Where(i => i.Type == "Part")
                            select a;

            var categoriesQuery = from c in db.Articles.Where(a => a.Type == "Category" || a.Type == "Subcategory")
                                   orderby c.Title
                                   select c;

            ViewBag.Categories = new SelectList(categoriesQuery, "Title", "Title", null);

            if (!String.IsNullOrEmpty(categories))
            {
                category = db.Articles.Where(a => a.Title == categories).SingleOrDefault();
                List<string> categoriesList = new List<string>();
                fillSubcategoriesList(category, categoriesList);

                parts = parts.Where(i => categoriesList.Contains(i.Category.Title));
                
            }

            //filtering searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                parts = parts.Where(i => i.Title.Contains(searchString)
                    || i.Text.Contains(searchString)
                    || i.Category.Title.Contains(searchString));
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
            int pageSize = parts.Count();
            if (categories != "OPONY")
            {
                pageSize = 15;
            }
            int pageNumber = (page ?? 1);

            var viewModel = new ShopView();
            viewModel.Parts = parts.OrderByDescending(p => p.DateAdded).ToPagedList(pageNumber, pageSize);
            if (db.Articles.Count() > 0)
            {
                viewModel.Shop = db.Articles.Where(a => a.Type == "Shop").Single();
            }
            if (!String.IsNullOrEmpty(categories))
            {
                viewModel.Categories = db.Articles.Where(a => a.Type == "Subcategory").Where(a => a.Category.Title == categories).ToList();
            }
            else
            {
                viewModel.Categories = db.Articles.Where(a => a.Type == "Category").ToList();
            }

            ViewBag.currentCategory = categories;

            Article superiorCategory = category == null ? null : category.Category;

            if (categories == "OPONY")
            {
                return View("TiresCategoriesIndex", viewModel);
            }
            else if (superiorCategory != null && superiorCategory.Title == "OPONY")
            {
                return View("TiresIndex", viewModel);
            }
            else
            {
                return View(viewModel);
            }
        }

        // GET: Parts/Details/5
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

        // GET: Parts/Create
        public ActionResult Create()
        {
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name");
            PopulateCategoriesDropdownList();
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Title,Text,Author,CategoryID,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = "Part";
                article.DateAdded = DateTime.Now;
                article.Category = db.Articles.Where(a => a.ArticleID == article.CategoryID).Single();
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }

            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            PopulateCategoriesDropdownList();
            return View(article);
        }

        // GET: Parts/Edit/5
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
            PopulateCategoriesDropdownList(article.CategoryID);
            return View(article);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Title,Text,Author,CategoryID,Category,DateAdded,VoivodshipID,Zipcode,City,Street,HouseNumber,ApartmentNumber,PhoneNo,Longitude,Latitude,Www,Email,StartDate,StartTime,End,EndTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                var article3 = db.Articles.Include("Category").Where(a => a.ArticleID == article.ArticleID).FirstOrDefault();
                article.Category = db.Articles.Find(article.CategoryID);
                article.Type = "Part";
                article.DateAdded = DateTime.Now;
                
                db.SaveChanges();
                return RedirectToAction("Index", "Media", new { articleID = article.ArticleID });
            }
            ViewBag.VoivodshipID = new SelectList(db.Voivodships, "VoivodshipID", "Name", article.VoivodshipID);
            PopulateCategoriesDropdownList(article.CategoryID);
            return View(article);
        }

        // GET: Parts/Delete/5
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

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            Medium medium = article.Images.FirstOrDefault();
            try
            {
                System.IO.Directory.Delete(Server.MapPath("/Images/Part/" + article.Title), true);
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

        public ActionResult AddToCart(string category)
        {
            foreach(string partID in Request.Form.Keys) {
                int amount = int.Parse(Request[partID]);
                if (amount > 0)
                {
                    Session[partID] = (Session[partID] != null ? (int) Session[partID] : 0) + amount;
                }
            }
            return RedirectToAction("Index", new { categories = category });
        }

        public ActionResult Cart(bool? clear, bool? orderSent)
        {
            if (clear != null)
            {
                if ((bool)clear)
                {
                    Session.RemoveAll();
                }
            }
            if (orderSent != null)
            {
                if ((bool)orderSent)
                {
                    ViewBag.orderSent = "Twoje zamówienie zostało wysłane";
                }
            }
            return View(fillCart());
        }

        [HttpPost]
        public ActionResult Order()
        {
            Cart cart = fillCart();
            SmtpClient smtpClient = new SmtpClient();
            Article data = db.Articles.Where(a => a.Type == "ZamowieniaData").SingleOrDefault();
            NetworkCredential basicCredential = new NetworkCredential(data.Email, data.Zipcode);
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(data.Email);

            smtpClient.Host = "smtp.webio.pl";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;
            message.Subject = "Zamówienie";
            message.IsBodyHtml = true;

            string name = Request["name"];
            string email = Request["email"];
            string address = Request["address"];
            string text = Request["text"];
            string phone = Request["phone"];

            message.Body = "<h2>" + name + "</h2>" + "<h3>" + email + "</h3>" + "<h3>" + address + "</h3>" + 
                "<h3>Telefon: " + phone + "</h3>" + "Uwagi:<br />" + text + "<br />";

            message.Body += "<table>";
            for (int i = 0; i < cart.articles.Count; i++)
            {
                message.Body += "<tr><td>" + cart.articles.ElementAt(i).Title +
                    "    </td><td>" + cart.amounts.ElementAt(i) +
                    " szt.    </td><td>" + cart.sums.ElementAt(i) + " zł</td></tr>";
            }
            message.Body += "</table>";
            message.Body += "<h3>Suma: " + cart.sum + " zł</h3>";

            message.To.Add(db.Articles.Where(a => a.Type == "Shop").SingleOrDefault().Email);

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + ex.InnerException);
            }

            return RedirectToAction("Cart", new { clear = true, orderSent = true } );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateCategoriesDropdownList(object selectedCategory = null)
        {
            var categoriesQuery = from c in db.Articles.Where(a => a.Type == "Category" || a.Type == "Subcategory")
                                   orderby c.Title
                                   select c;
            ViewBag.CategoryID = new SelectList(categoriesQuery, "ArticleID", "Title", selectedCategory);
        }

        private void fillSubcategoriesList(Article category, List<string> categories)
        {
            categories.Add(category.Title);
            foreach (Article subcategory in category.RelatedArticles)
            {
                fillSubcategoriesList(subcategory, categories);
            }
        }

        private Cart fillCart()
        {
            Cart cart = new Cart();
            cart.articles = new List<Article>();
            cart.amounts = new List<int>();
            cart.sums = new List<double>();
            foreach (string partID in Session.Keys)
            {
                int intID = int.Parse(partID);
                Article article = db.Articles.Where(a => a.ArticleID == intID).SingleOrDefault();
                cart.articles.Add(article);
                int amount = (int)Session[partID];
                cart.amounts.Add(amount);
                double smallSum = (double)amount * (double)article.Longitude;
                cart.sums.Add(smallSum);
            }
            double sum = 0.0;
            foreach (double element in cart.sums)
            {
                sum += element;
            }
            cart.sum = sum;
            return cart;
        }
    }
}
