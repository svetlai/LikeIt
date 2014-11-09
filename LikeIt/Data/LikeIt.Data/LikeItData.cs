namespace LikeIt.Data
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Data.Contracts;
    using LikeIt.Data.Common.Repositories;
    using LikeIt.Models;

    public class LikeItData : ILikeItData
    {
        private ILikeItDbContext db;
        private IDictionary<Type, object> repositories;

        public LikeItData(ILikeItDbContext db)
        {
            this.db = db;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Like> Likes
        {
            get
            {
                return this.GetRepository<Like>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                return this.GetRepository<Tag>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Rating> Ratings
        {
            get
            {
                return this.GetRepository<Rating>();
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                return this.GetRepository<Image>();
            }
        }

        public IRepository<IdentityRole> IdentityRoles
        {
            get
            {
                return this.GetRepository<IdentityRole>();
            }
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var type = typeof(GenericRepository<T>);
                var newRepository = Activator.CreateInstance(type, this.db);
                
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
