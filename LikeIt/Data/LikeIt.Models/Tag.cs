namespace LikeIt.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LikeIt.Data.Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tag : AuditInfo, IDeletableEntity
    {
        private ICollection<Like> likes;

        public Tag()
        {
            this.likes = new HashSet<Like>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Like> Likes
        {
            get
            {
                return this.likes;

            }
            set
            {
                this.likes = value;
            }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}
