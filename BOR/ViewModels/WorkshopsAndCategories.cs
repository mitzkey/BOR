using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using BOR.Models;

namespace BOR.ViewModels
{
    public class WorkshopsAndCategories
    {
        public PagedList.IPagedList<Article> Workshops { get; set; }
        public IEnumerable<Article> Categories { get; set; }
    }
}