using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOR.Models;

namespace BOR.ViewModels
{
    public class ArticleMedia
    {
        public Article Article { get; set; }
        public IEnumerable<Medium> Media { get; set; }
    }
}