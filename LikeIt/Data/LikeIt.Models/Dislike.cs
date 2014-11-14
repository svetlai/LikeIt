namespace LikeIt.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using LikeIt.Data.Common.Models;

    public class Dislike : AuditInfo, IDeletableEntity
    {
        public Dislike()
        {
        }

        public int Id { get; set; }

        public int PageId { get; set; }

        public virtual Page Page { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}
