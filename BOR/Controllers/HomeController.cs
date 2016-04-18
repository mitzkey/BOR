using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOR.Models;
using BOR.ViewModels;
using BOR.DAL;

namespace BOR.Controllers
{
    public class HomeController : ParentController
    {
        private BORcontext db = new BORcontext();
        public ActionResult Index()
        {
            return View();
        }
            
    }
}