namespace LikeIt.Data.Contracts
{
    using LikeIt.Data.Repositories;
    using LikeIt.Models;

    public interface ILikeItData
    {
        IRepository<User> Users { get; }

        IRepository<Like> Likes { get; }

        void SaveChanges();
    }
}
