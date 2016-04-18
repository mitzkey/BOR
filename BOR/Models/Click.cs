using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BOR.Models
{
    public class Click
    {
        public int ClickID { get; set; }
        [Display(Name = "Liczba wejść")]
        public int Amount { get; set; }
        [Display(Name = "Dział")]
        public string ArticleType { get; set; }
    }
}