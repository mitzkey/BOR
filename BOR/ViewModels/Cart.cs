using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOR.Models;

namespace BOR.ViewModels
{
    public class Cart
    {
        public List<Article> articles { get; set; }
        public List<int> amounts { get; set; }
        public List<double> sums { get; set; }
        public double sum { get; set; }
    }
}