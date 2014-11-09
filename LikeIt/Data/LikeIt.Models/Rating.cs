namespace LikeIt.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using LikeIt.Data.Common.Models;

    public class Rating : AuditInfo, IDeletableEntity
    {
        public Rating()
        {
        }

        public int Id { get; set; }

        public int LikeId { get; set; }

        public virtual Like Like { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}
