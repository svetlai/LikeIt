namespace LikeIt.Data.Contracts
{
    using LikeIt.Data.Common.Repositories;
    using LikeIt.Models;

    public interface ILikeItData
    {
        IRepository<User> Users { get; }

        IRepository<Like> Likes { get; }

        void SaveChanges();
    }
}
