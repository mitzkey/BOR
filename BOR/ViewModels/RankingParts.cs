using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOR.Models;

namespace BOR.ViewModels
{
    public class RankingParts
    {
        public Article Ranking { get; set; }
        public IEnumerable<Article> Parts { get; set; }
    }
}