namespace BOR.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections;
    using BOR.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<BOR.DAL.BORcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BOR.DAL.BORcontext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var voivodships = new List<Voivodship> {
                new Voivodship { Name = "dolnoœl¹skie" },
                new Voivodship { Name = "kujawsko-pomorskie" },
                new Voivodship { Name = "lubelskie" },
                new Voivodship { Name = "lubuskie" },
                new Voivodship { Name = "³ódzkie" },
                new Voivodship { Name = "ma³opolskie" },
                new Voivodship { Name = "mazowieckie" },
                new Voivodship { Name = "opolskie" },
                new Voivodship { Name = "podkarpackie" },
                new Voivodship { Name = "podlaskie" },
                new Voivodship { Name = "pomorskie" },
                new Voivodship { Name = "œl¹skie" },
                new Voivodship { Name = "œwiêtokrzyskie" },
                new Voivodship { Name = "warmiñsko-mazurskie" },
                new Voivodship { Name = "wielkopolskie" },
                new Voivodship { Name = "zachodniopomorskie" }
            };
            voivodships.ForEach(s => context.Voivodships.AddOrUpdate(i => i.Name, s));
            context.SaveChanges();

        }
    }
}
