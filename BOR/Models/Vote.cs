using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR.Models
{
    public class Vote
    {
        public int VoteID { get; set; }
        public DateTime Date { get; set; }
        public int? MediumID { get; set; }
        public int? ArticleID { get; set; }
        public virtual Medium Medium { get; set;}
        public virtual Article Article { get; set; }
    }
}