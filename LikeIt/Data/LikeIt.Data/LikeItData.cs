namespace LikeIt.Data
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Data.Contracts;
    using LikeIt.Data.Common.Repositories;
    using LikeIt.Models;
    using LikeIt.Data.Common.Models;

    public class LikeItData : ILikeItData
    {
        private ILikeItDbContext db;
        private IDictionary<Type, object> repositories;

        public LikeItData(ILikeItDbContext db)
        {
            this.db = db;
            this.repositories = new Dictionary<Type, object>();
        }

        public IDeletableEntityRepository<User> Users
        {
            get
            {
                return this.GetDeletableEntityRepository<User>();
            }
        }

        public IDeletableEntityRepository<Like> Likes
        {
            get
            {
                return this.GetDeletableEntityRepository<Like>();
            }
        }

        public IDeletableEntityRepository<Category> Categories
        {
            get
            {
                return this.GetDeletableEntityRepository<Category>();
            }
        }

        public IDeletableEntityRepository<Tag> Tags
        {
            get
            {
                return this.GetDeletableEntityRepository<Tag>();
            }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get
            {
                return this.GetDeletableEntityRepository<Comment>();
            }
        }

        public IDeletableEntityRepository<Rating> Ratings
        {
            get
            {
                return this.GetDeletableEntityRepository<Rating>();
            }
        }

        public IDeletableEntityRepository<Image> Images
        {
            get
            {
                return this.GetDeletableEntityRepository<Image>();
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

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                var newRepository = Activator.CreateInstance(type, this.db);
                this.repositories.Add(typeof(T), newRepository);
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
