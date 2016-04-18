using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR.Models
{
    public class Voivodship
    {

        public int VoivodshipID { get; set; }
        public String Name { get; set; }
        public virtual ICollection<Article> Workshops { get; set; }
    }
}