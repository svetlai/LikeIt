namespace LikeIt.Data.Contracts
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Data.Common.Repositories;
    using LikeIt.Models;

    public interface ILikeItData
    {
        IDeletableEntityRepository<User> Users { get; }

        IDeletableEntityRepository<Like> Likes { get; }

        IDeletableEntityRepository<Category> Categories { get; }

        IDeletableEntityRepository<Tag> Tags { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Rating> Ratings { get; }

        IDeletableEntityRepository<Image> Images { get; }

        IRepository<IdentityRole> IdentityRoles { get; }

        void SaveChanges();
    }
}
