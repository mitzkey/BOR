using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOR.Models;

namespace BOR.ViewModels
{
    public class WorkshopComment
    {
        public Article Workshop { get; set; }
        public Article Comment { get; set; }
    }
}