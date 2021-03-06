﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOR.Models;

namespace BOR.ViewModels
{
    public class ShopView
    {
        public Article Shop { get; set; }
        public IEnumerable<Article> Parts { get; set; }
        public IEnumerable<Article> Categories { get; set; }
    }
}