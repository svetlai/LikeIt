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
            //if (context.Users.Any())
            //{
            //    return;
            //}

            //this.seeder.SeedRoles(context);
            //this.seeder.SeedAdmin(context);
            //this.seeder.SeedUsers(context);
            //var categories = this.seeder.GetCategories();

            //context.Categories.AddOrUpdate(categories);
            //context.SaveChanges();

            //var tags = this.seeder.GetTags();

            //context.Tags.AddOrUpdate(tags);
            //context.SaveChanges();

            var defaultPath = "../../Images/default.png";
            var defaultImage = this.seeder.GetSampleImage(defaultPath);

            var users = context.Users.Take(10).ToList();
            var tagsDb = context.Tags.Take(20).ToList();
            var categotiesDB = context.Categories.ToList();

            //var pages = this.seeder.GetPages(categotiesDB, tagsDb, users[0], defaultImage);
            //context.Pages.AddOrUpdate(pages);
            //context.SaveChanges();

            

            //if (context.Users.Any())
            //{
            //    return;
            //}

            ////TODO : refactor 
            //var roles = new List<IdentityRole>
            //{
            //    new IdentityRole(GlobalConstants.UserRole), 
            //    new IdentityRole(GlobalConstants.AdminRole)
            //};

            //context.Roles.AddOrUpdate(roles.ToArray());
            //context.SaveChanges();

            //User user = new User()
            //{
            //    UserName = "Anonymous"
            //};

            //this.CreateInitialAdmin(context);

            //context.Users.AddOrUpdate(user);
            //context.SaveChanges();

            //var categories = new List<Category>
            //{
            //    new Category { Name = "Food"},
            //    new Category { Name = "People"},
            //    new Category { Name = "Books"},
            //    new Category { Name = "Movies"},
            //    new Category { Name = "Fun"},
            //    new Category { Name = "Hobby"},
            //};

            //context.Categories.AddOrUpdate(categories.ToArray());
            //context.SaveChanges();

            //var tags = new List<Tag>
            //{
            //    new Tag
            //    {
            //        Name = "tag"
            //    },
            //    new Tag
            //    {
            //        Name = "another"
            //    }
            //};

            //context.Tags.AddOrUpdate(tags.ToArray());
            //context.SaveChanges();

            ////var images = new List<Image>
            ////{
            ////    new Image 
            ////    { 
            ////        Content = File.ReadAllBytes(""),
            ////        FileExtension = "png",
            ////        Path = "http://www.metalcuttingtools.eu/sites/default/files/default_images/thumbnail-default.jpg",
            ////    }
            ////};

            ////context.Images.AddOrUpdate(images.ToArray());
            ////context.SaveChanges();

            //var pages = new List<Page>
            //{
            //    new Page 
            //    {
            //        Name = "Apple",
            //        Category = categories[0],
            //        Description = "Best fruit ever!",
            //        User = user,
            //    },
            //                    new Page 
            //    {
            //        Name = "Apple",
            //        Category = categories[0],
            //        Description = "Best fruit ever!",
            //        User = user,
            //    },
            //                    new Page 
            //    {
            //        Name = "Orange",
            //        Category = categories[0],
            //        Description = "Best fruit ever!",
            //        User = user,
            //    },
            //                    new Page 
            //    {
            //        Name = "Banana",
            //        Category = categories[0],
            //        Description = "Best fruit ever!",
            //        User = user,
            //    },
            //                    new Page 
            //    {
            //        Name = "Dancing",
            //        Category = categories[5],
            //        Description = "Best fruit ever!",
            //        User = user,
            //    },

            //};

            //context.Pages.AddOrUpdate(pages.ToArray());
            //context.SaveChanges();

            //var comments = new List<Comment>
            //{
            //    new Comment
            //    {
            //        Content = "khahslh lhaljlkajlhl nkh lnlanl lajls jlaldnasldhl na,lhlajlj jlajsld",
            //        Author = user,
            //    },
            //             new Comment
            //    {
            //        Content = "khahslh lhaljlkajlhl nkh lnlanl lajls jlaldnasldhl na,lhlajlj jlajsld",
            //        Author = user,
            //    },
            //             new Comment
            //    {
            //        Content = "khahslh lhaljlkajlhl nkh lnlanl lajls jlaldnasldhl na,lhlajlj jlajsld",
            //        Author = user,
            //    }
            //};

            //context.Comments.AddOrUpdate(comments.ToArray());
            //context.SaveChanges();

            //var detailedPage = context.Pages.FirstOrDefault(p => p.Id == 2);
            //detailedPage.Tags.Add(tags[0]);
            //detailedPage.Tags.Add(tags[1]);
            //detailedPage.Comments.Add(comments[0]);
            //detailedPage.Comments.Add(comments[1]);
            //detailedPage.Comments.Add(comments[2]);
            //context.SaveChanges();
        }
    }
}
