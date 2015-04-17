namespace LikeIt.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Models;

    public interface ILikeItDbContext : IDisposable
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Page> Pages { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Dislike> Dislikes { get; set; }

        IDbSet<Image> Images { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
