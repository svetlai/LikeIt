namespace LikeIt.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LikeIt.Data.Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment : AuditInfo, IDeletableEntity
    {
        public Comment()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150), MinLength(2)]
        public string Content { get; set; }

        //[Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        //[Required]
        public int PageId { get; set; }

        public virtual Page Page { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
