﻿namespace LikeIt.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using LikeIt.Models;

    public interface ILikeItDbContext : IDisposable
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        IDbSet<Image> Images { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}