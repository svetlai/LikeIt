namespace LikeIt.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Models;
    using LikeIt.Data.Contracts;
    using LikeIt.Common;

    public sealed class Configuration : DbMigrationsConfiguration<LikeItDbContext>
    {
        private SeedData seeder;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.seeder = new SeedData();
        }

        protected override void Seed(LikeItDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            this.seeder.SeedRoles(context);
            this.seeder.SeedAdmin(context);
            this.seeder.SeedRandomUsers(context, 10);

            this.seeder.SeedCategories(context);

            var users = context.Users.Take(10).ToList();
            var categories = context.Categories.ToList();

            this.seeder.SeedRandomPages(context, categories, users, 10);

            this.seeder.SeedSinglePage(context,
                "Telerik Academy",
                "A free software academy, training hundreds of software engineers per year.",
                categories.Where(c => c.Name == "Education").FirstOrDefault(),
                new List<string> { "software", "developer", "web" },
                users[2],
                this.seeder.GetSampleImage("../../Images/imgs-seed/telerik-academy.jpg"),
                true, "Best academy ever.");

            this.seeder.SeedSinglePage(context,
                "BMW M3",
                "A high-performance version of the BMW 3-series.",
                categories.Where(c => c.Name == "Brands/Products").FirstOrDefault(),
                new List<string> { "bmw", "car", "fast" },
                users[4],
                this.seeder.GetSampleImage("../../Images/imgs-seed/bmw.jpg"),
                true, "Awesome car");

            this.seeder.SeedSinglePage(context,
                 "Cats",
                 "A small furry animal.",
                 categories.Where(c => c.Name == "Animals").FirstOrDefault(),
                 new List<string> { "cat", "animal", "meow" },
                 users[4],
                 this.seeder.GetSampleImage("../../Images/imgs-seed/cat.jpg"),
                 true);

            this.seeder.SeedSinglePage(context,
                 "Traffic",
                 "Morning traffic in the big city.",
                 categories.Where(c => c.Name == "Other").FirstOrDefault(),
                 new List<string> { "traffic", "city" },
                 users[3],
                 this.seeder.GetSampleImage("../../Images/imgs-seed/traffic.jpg"),
                 false);

            this.seeder.SeedSinglePage(context,
                "Mondays",
                "Every week starts with it.",
                categories.Where(c => c.Name == "Other").FirstOrDefault(),
                new List<string> { "monday", "week" },
                users[3],
                this.seeder.GetSampleImage("../../Images/imgs-seed/mondays.jpg"),
                false, "Grrrrrrr");

            this.seeder.SeedSinglePage(context,
                "Slow Computers",
                "When computers think slowlier than humans.",
                categories.Where(c => c.Name == "Other").FirstOrDefault(),
                new List<string> { "computer", "slow", "laptop" },
                users[3],
                this.seeder.GetSampleImage("../../Images/imgs-seed/slow-computers.jpg"),
                false);

            var pages = context.Pages.ToList();

            var customPage = pages.Where(p => p.Name == "Telerik Academy").FirstOrDefault();
            for (int i = 0; i < 50; i++)
            {
                this.seeder.AddLike(customPage, this.seeder.GetRandomUser(users));
            }

            customPage.Rating = this.seeder.GetPageRating(customPage);

            customPage = pages.Where(p => p.Name == "BMW M3").FirstOrDefault();
            for (int i = 0; i < 38; i++)
            {
                this.seeder.AddLike(customPage, this.seeder.GetRandomUser(users));
            }

            customPage.Rating = this.seeder.GetPageRating(customPage);

            customPage = pages.Where(p => p.Name == "Cats").FirstOrDefault();
            for (int i = 0; i < 25; i++)
            {
                this.seeder.AddLike(customPage, this.seeder.GetRandomUser(users));
            }

            customPage.Rating = this.seeder.GetPageRating(customPage);

            customPage = pages.Where(p => p.Name == "Traffic").FirstOrDefault();
            for (int i = 0; i < 37; i++)
            {
                this.seeder.AddDislike(customPage, this.seeder.GetRandomUser(users));
            }

            customPage.Rating = this.seeder.GetPageRating(customPage);

            customPage = pages.Where(p => p.Name == "Mondays").FirstOrDefault();
            for (int i = 0; i < 29; i++)
            {
                this.seeder.AddDislike(customPage, this.seeder.GetRandomUser(users));
            }

            customPage.Rating = this.seeder.GetPageRating(customPage);

            customPage = pages.Where(p => p.Name == "Slow Computers").FirstOrDefault();
            for (int i = 0; i < 42; i++)
            {
                this.seeder.AddDislike(customPage, this.seeder.GetRandomUser(users));
            }

            customPage.Rating = this.seeder.GetPageRating(customPage);

            context.SaveChanges();
        }
    }
}
