using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BOR.Models
{
    public class Medium
    {
        public int MediumID { get; set; }
        public string Type { get; set; }
        [Display(Name = "Opis obrazu")]
        public string Name { get; set; }
        [Display(Name = "Wysokość")]
        public int Height { get; set; }
        [Display(Name = "Szerokość")]
        public int Width { get; set; }
        [Display(Name = "Ścieżka")]
        public string Link { get; set; }
        public string MiniatureLink { get; set; }
        [Display(Name = "Proporcje")]
        public string AspectRatio { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public int ArticleID { get; set; }
        public virtual Article Article { get; set; }
    }
}