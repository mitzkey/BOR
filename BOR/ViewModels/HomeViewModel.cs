using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOR.Models;

namespace BOR.ViewModels
{
    public class HomeViewModel
    {
        public Article Article { get; set; }
        public Article Ranking { get; set; }
        public Medium OffRoadGirl { get; set; }
    }
}