using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BOR.Models;

namespace BOR.DAL
{
    public class BORcontext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Medium> Media { get; set; }
        public DbSet<Voivodship> Voivodships { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Click> Clicks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.Images)
                .WithMany()
                .Map(x => x.ToTable("Articles_Images"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Videos)
                .WithMany()
                .Map(x => x.ToTable("Articles_Videos"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Comments)
                .WithMany()
                .Map(x => x.ToTable("Articles_Comments"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Galleries)
                .WithMany()
                .Map(x => x.ToTable("Articles_Galleries"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.RelatedArticles)
                .WithMany()
                .Map(x => x.ToTable("Articles_RelatedArticles"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Tutorials)
                .WithMany()
                .Map(x => x.ToTable("Articles_Tutorials"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.OffRoadGirls)
                .WithMany()
                .Map(x => x.ToTable("Articles_OffRoadGirls"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Workshops)
                .WithMany()
                .Map(x => x.ToTable("Articles_Workshops"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Services)
                .WithMany()
                .Map(x => x.ToTable("Articles_Services"));
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Events)
                .WithMany()
                .Map(x => x.ToTable("Articles_Events"));
        }
    }
}