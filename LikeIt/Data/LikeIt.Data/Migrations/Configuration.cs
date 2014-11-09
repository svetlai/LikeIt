namespace LikeIt.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Models;
    using LikeIt.Data.Contracts;

    public sealed class Configuration : DbMigrationsConfiguration<LikeItDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LikeItDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            //Create roles
            string[] roles = new string[]
            {
                "user", 
                "administrator"
            };

            foreach (var role in roles)
            {
                this.CreateRole(context, role);
            }

            //Add initial admin
            this.CreateInitialAdmin(context);
        }

        private void CreateRole(LikeItDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        private void CreateInitialAdmin(LikeItDbContext context)
        {
            string username = "admin";
            string email = "admin@a.a";
            string password = "123456";
            string role = "administrator";

            var admin = new User()
            {
                UserName = username,
                Email = email,
            };         

            var userManager = new UserManager<User>(new UserStore<User>(context));

            if (!userManager.Users.Any(u => u.UserName == username))
            {
                userManager.Create(admin, password);
                userManager.AddToRole(admin.Id, role);
            }
        }
    }
}
