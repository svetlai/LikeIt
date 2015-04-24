namespace LikeIt.Data.Common.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    using LikeIt.Data.Common.Repositories;

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbContext db;
        private IDbSet<T> set;

        public GenericRepository(DbContext context)
        {
            this.db = context;
            this.set = context.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return this.set;
        }

        public virtual T Find(object id)
        {
            return this.set.Find(id);
        }

        public virtual void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public virtual void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public virtual T Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
            return entity;
        }

        public virtual T Delete(object id)
        {
            var entity = this.Find(id);
            this.Delete(entity);
            return entity;
        }

        public virtual void Detach(T entity)
        {
            var entry = this.db.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public int SaveChanges()
        {
            return this.db.SaveChanges();
        }

        public void Dispose()
        {
            this.db.Dispose();
        }

        protected void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.db.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
