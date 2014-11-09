namespace LikeIt.Models
{
    using LikeIt.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image : AuditInfo, IDeletableEntity
    {
        [Key, ForeignKey("Like")]
        public int LikeId { get; set; }

        public string Path { get; set; }

        public virtual Like Like { get; set; }
        
        [Index]
        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}
