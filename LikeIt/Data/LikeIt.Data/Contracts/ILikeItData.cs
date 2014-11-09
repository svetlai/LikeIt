namespace LikeIt.Data.Contracts
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Data.Common.Repositories;
    using LikeIt.Models;

    public interface ILikeItData
    {
        IRepository<User> Users { get; }

        IRepository<Like> Likes { get; }

        IRepository<Category> Categories { get; }

        IRepository<Tag> Tags { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Rating> Ratings { get; }

        IRepository<Image> Images { get; }

        IRepository<IdentityRole> IdentityRoles { get; }

        void SaveChanges();
    }
}
