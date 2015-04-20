namespace LikeIt.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity.Migrations;
    using System.IO;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Models;
    using System.Reflection;

    public class SeedData
    {
        public const string DeafultImagePath = "../../Images/default.png";

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
                FirstName = "Adi",
                LastName = "Minkov"
            };

            userManager.Create(admin, "123456");
            userManager.AddToRole(admin.Id, GlobalConstants.AdminRole);

            context.SaveChanges();
        }

        public void SeedRandomUsers(LikeItDbContext context, int count)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            for (int i = 0; i < count; i++)
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

        public void SeedCategories(LikeItDbContext context)
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
                new Category { Name = "Animals"},
                new Category { Name = "Hobbies"},
                new Category { Name = "Other"},
            };

            context.Categories.AddOrUpdate(categories.ToArray());
            context.SaveChanges();
        }

        public void SeedRandomTags(LikeItDbContext context, int count)
        {
            var tags = new List<Tag>();

            for (int i = 0; i < count; i++)
            {
                tags.Add(new Tag
                {
                    Name = this.randomGenerator.RandomString(3, 5)
                });
            }

            context.Tags.AddOrUpdate(tags.ToArray());
            context.SaveChanges();
        }

        public void SeedRandomPages(LikeItDbContext context, IList<Category> categories, IList<User> users, int count)
        {
            var pages = new List<Page>();

            for (int i = 0; i < count; i++)
            {
                var page = new Page
                {
                    Name = this.randomGenerator.RandomString(5, 10),
                    Category = categories[this.randomGenerator.RandomNumber(0, (categories.Count - 1))],
                    Description = this.randomGenerator.RandomString(20, 150),
                    CreatedOn = DateTime.Now,
                    User = this.GetRandomUser(users),
                    Image = this.GetSampleImage(DeafultImagePath),
                };

                this.AddRandomTagsToPage(page, this.randomGenerator.RandomNumber(1, 5));
                this.AddInitialRandomLikeDislikeToPage(page);

                this.SeedRandomLikes(page, users, this.randomGenerator.RandomNumber(0, 20));
                this.SeedRandomDislikes(page, users, this.randomGenerator.RandomNumber(0, 20));
                this.SeedRandomComments(page, users, this.randomGenerator.RandomNumber(0, 4));

                page.Rating = this.GetPageRating(page);

                pages.Add(page);
            }

            context.Pages.AddOrUpdate(pages.ToArray());
            context.SaveChanges();
        }

        public void SeedRandomLikes(Page page, IList<User> users, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.AddLike(page, this.GetRandomUser(users));
            }
        }

        public void SeedRandomDislikes(Page page, IList<User> users, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.AddDislike(page, this.GetRandomUser(users));
            }
        }

        public void SeedRandomComments(Page page, IList<User> users, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.AddComment(page, this.GetRandomUser(users), this.randomGenerator.RandomString());
            }
        }

        public void SeedSinglePage(ILikeItDbContext context, string name, string description, Category category, IList<string> tags, User user, Image image, bool like, string comment = "")
        {
            var page = new Page
            {
                Name = name,
                Category = category,
                Description = description,
                CreatedOn = DateTime.Now,
                User = user,
                Image = image,
            };

            for (int i = 0; i < tags.Count; i++)
            {
                this.AddTag(page, tags[i]);
            }

            if (like)
            {
                this.AddLike(page, page.User);
            }
            else
            {
                this.AddDislike(page, page.User);
            }

            page.Rating = this.GetPageRating(page);

            if (!string.IsNullOrEmpty(comment))
            {
                this.AddComment(page, page.User, comment);
            }

            context.Pages.Add(page);
            context.SaveChanges();
        }

        public void AddTag(Page page, string tagName)
        {
            page.Tags.Add(new Tag
            {
                Name = tagName
            });
        }

        public void AddLike(Page page, User user)
        {
            page.Likes.Add(new Like
            {
                PageId = page.Id,
                UserId = user.Id
            });
        }

        public void AddDislike(Page page, User user)
        {
            page.Dislikes.Add(new Dislike
            {
                PageId = page.Id,
                UserId = user.Id
            });
        }

        public void AddComment(Page page, User user, string content)
        {
            page.Comments.Add(new Comment
            {
                AuthorId = user.Id,
                PageId = page.Id,
                Content = content
            });
        }

        public User GetRandomUser(IList<User> users)
        {
            return users[this.randomGenerator.RandomNumber(0, users.Count - 1)];
        }

        public Image GetSampleImage(string path)
        {
            var directory = AssemblyHelpers.GetDirectoryForAssembyl(Assembly.GetExecutingAssembly());
            var file = File.ReadAllBytes(directory + path);
            var image = new Image
            {
                Content = file,
                FileExtension = path.Split('.').Last()
            };

            return image;
        }

        public int GetPageRating(Page page)
        {
            return page.Likes.Where(l => !l.IsDeleted).Count() - page.Dislikes.Where(l => !l.IsDeleted).Count();
        }

        private void AddRandomTagsToPage(Page page, int count)
        {
            for (int j = 0; j < count; j++)
            {
                AddTag(page, this.randomGenerator.RandomString(3, 15));
            }
        }

        private void AddInitialRandomLikeDislikeToPage(Page page)
        {
            if (this.randomGenerator.RandomNumber(0, 2) == 0)
            {
                this.AddLike(page, page.User);
            }
            else
            {
                this.AddDislike(page, page.User);
            }
        }
    }
}
