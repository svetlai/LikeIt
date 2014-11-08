namespace LikeIt.Data
{
    using System;
    using System.Collections.Generic;

    using LikeIt.Data.Contracts;
    using LikeIt.Data.Repositories;
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

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var type = typeof(EFRepository<T>);
                var newRepository = Activator.CreateInstance(type, this.db);
                
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
