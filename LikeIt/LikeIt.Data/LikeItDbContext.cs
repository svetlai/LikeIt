namespace LikeIt.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using LikeIt.Data.Contracts;
    using LikeIt.Data.Migrations;
    using LikeIt.Models;

    public class LikeItDbContext : IdentityDbContext<User>, ILikeItDbContext
    {
        public LikeItDbContext()
            : base("LikeItConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LikeItDbContext, Configuration>());
        }

        public static LikeItDbContext Create()
        {
            return new LikeItDbContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public IDbSet<Like> Likes { get; set; }
    }
}
