﻿namespace LikeIt.Data.Common.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using LikeIt.Data.Common.Models;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override T Delete(T entity)
        {
            entity.DeletedOn = DateTime.Now;
            entity.IsDeleted = true;
            this.ChangeEntityState(entity, EntityState.Modified);
            return entity;
        }

        public override T Delete(object id)
        {
            var entity = this.Find(id);
            entity.DeletedOn = DateTime.Now;
            entity.IsDeleted = true;
            this.ChangeEntityState(entity, EntityState.Modified);
            return entity;
        }
    }
}
