using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOR.DAL;
using BOR.Models;

namespace BOR.Controllers
{
    public class ParentController : Controller
    {
        private BORcontext db = new BORcontext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ViewBag.ads = db.Articles.Where(a => a.Type == "Ad");
            ViewBag.bottomAd = db.Articles.Where(a => a.Type == "BottomAd").SingleOrDefault();
            if (db.Articles.Where(a => a.Type == "Contact").Count() > 1)
            {
                ViewBag.marcinContact = db.Articles.Where(a => a.Type == "Contact").ToList().ElementAt(0);
                ViewBag.michalContact = db.Articles.Where(a => a.Type == "Contact").ToList().ElementAt(1);
            }
            
        }

        public void Click(string articleType)
        {
            Click click = db.Clicks.Where(c => c.ArticleType == articleType).SingleOrDefault();
            if (click != null)
            {
                click.Amount = click.Amount + 1;
            }
            else
            {
                click = new Click();
                click.ArticleType = articleType;
                click.Amount = 1;
                db.Clicks.Add(click);
            }
            db.SaveChanges();
        }
    }
}