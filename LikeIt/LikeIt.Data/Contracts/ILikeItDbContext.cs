namespace LikeIt.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using LikeIt.Models;

    public interface ILikeItDbContext : IDisposable
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Like> Likes { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
