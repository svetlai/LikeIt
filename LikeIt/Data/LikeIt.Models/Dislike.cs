﻿namespace LikeIt.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LikeIt.Data.Common.Models;

    public class Dislike : AuditInfo, IDeletableEntity
    {
        public Dislike()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Page")]
        public int PageId { get; set; }

        public virtual Page Page { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}
