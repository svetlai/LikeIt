namespace LikeIt.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Models;
    using System.Reflection;
    using System.IO;

    public class SeedData
    {
        private IRandomGenerator randomGenerator;

        public SeedData()
        {
            this.randomGenerator = new RandomGenerator();
        }

        public void SeedRoles(LikeItDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = GlobalConstants.AdminRole };
            roleManager.Create(adminRole);

            var userRole = new IdentityRole { Name = GlobalConstants.UserRole };
            roleManager.Create(userRole);

            context.SaveChanges();
        }

        public void SeedAdmin(LikeItDbContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var admin = new User()
            {
                Email = "admin@a.a",
                UserName = "admin",
            };

            userManager.Create(admin, "123456");
            userManager.AddToRole(admin.Id, "Administrator"); //GlobalConstants.AdminRoleName

            context.SaveChanges();
        }

        public void SeedUsers(LikeItDbContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Email = string.Format("{0}@{1}.com", this.randomGenerator.RandomString(3, 6), this.randomGenerator.RandomString(3, 6)),
                    UserName = this.randomGenerator.RandomString(6, 16),
                    FirstName = this.randomGenerator.RandomString(6, 16),
                    LastName = this.randomGenerator.RandomString(6, 16)
                };

                userManager.Create(user, "123456");
            }

            context.SaveChanges();
        }

        public Category[] GetCategories()
        {
            var categories = new List<Category>
            {
                new Category { Name = "Places"},
                new Category { Name = "Business"},
                new Category { Name = "Education"},
                new Category { Name = "Brands/Products"},
                new Category { Name = "Entertainment"},
                new Category { Name = "People"},
                new Category { Name = "Causes"},
                new Category { Name = "Food"},
                new Category { Name = "Other"},
            };

            return categories.ToArray();
        }

        public Tag[] GetTags()
        {
            var tags = new List<Tag>();

            for (int i = 0; i < 30; i++)
            {
                tags.Add(new Tag
                {
                   Name = this.randomGenerator.RandomString(3, 5)
                });
            }
            
            return tags.ToArray();
        }

        public Page[] GetPages(IList<Category> categories, IEnumerable<Tag> tags, User user, Image image)
        {
            var pages = new List<Page>();

            for (int i = 0; i < 10; i++)
            {
                var page = new Page
                {
                    Name = this.randomGenerator.RandomString(5, 10),
                    Category = categories[this.randomGenerator.RandomNumber(0, (categories.Count() - 1))],
                    Description = this.randomGenerator.RandomString(20, 150),
                    CreatedOn = DateTime.Now,
                    User = user,
                    Image = image,
                    Rating = this.randomGenerator.RandomNumber(-5, 15)
                };

                //for (int j = 0; j < 100; i++)
                //{
                //    page.Likes.Add(new Like());
                //}

                pages.Add(page);
            }

            return pages.ToArray();

            //var pages = new List<Page>
            //{
            //    new Page 
            //    {
            //        Name = "Telerik Academy",
            //        Category = categories[2],
            //        Description = "Students Integrated Learning System.",
            //        CreatedOn = DateTime.Now,
            //        User = user,
            //        Image = image,
            //    },
            //};
        }

        public Image GetSampleImage(string path)
        {
            var directory = AssemblyHelpers.GetDirectoryForAssembyl(Assembly.GetExecutingAssembly());
            var file = File.ReadAllBytes(directory + path);
            var image = new Image
            {
                Content = file,
                FileExtension = "jpg"
            };

            return image;
        }
    }
}
