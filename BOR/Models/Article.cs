using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BOR.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Type { get; set; }
        [Display(Name = "Nazwa")]
        public string Title { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Opis")]
        public string Text { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data dodania")]
        [Column(TypeName = "DateTime2")]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Województwo")]
        public int? VoivodshipID { get; set; }
        public virtual Voivodship Voivodship { get; set; }
        [Display(Name="Kod pocztowy")]
        public String Zipcode { get; set; }
        [Display(Name = "Miejscowość")]
        public String City { get; set; }
        [Display(Name = "Ulica")]
        public String Street { get; set; }
        [Display(Name = "Numer budynku")]
        public String HouseNumber { get; set; }
        [Display(Name = "Numer lokalu")]
        public String ApartmentNumber { get; set; }
        [Display(Name = "Telefon")]
        public String PhoneNo { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        [DataType(DataType.Url)]
        public String Www { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dzień rozpoczęcia")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Godzina rozpoczęcia")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dzień zakończenia")]
        public DateTime? End { get; set; }
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Godzina zakończenia")]
        public DateTime? EndTime { get; set; }
        public int? CategoryID { get; set; }
        public virtual Article Category { get; set; }
        public virtual ICollection<Medium> Images { get; set; }
        public virtual ICollection<Medium> Videos { get; set; }
        public virtual ICollection<Article> Comments { get; set; }
        public virtual ICollection<Article> Galleries { get; set; }
        public virtual ICollection<Article> RelatedArticles { get; set; }
        public virtual ICollection<Article> Tutorials { get; set; }
        public virtual ICollection<Article> OffRoadGirls { get; set; }
        public virtual ICollection<Article> Workshops { get; set; }
        public virtual ICollection<Article> Services { get; set; }
        public virtual ICollection<Article> Events { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}