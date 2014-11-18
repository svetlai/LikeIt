namespace LikeIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LikeIt.Data.Common.Models;

    public class Page : AuditInfo, IDeletableEntity
    {
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Dislike> dislikes;

        public Page()
        {
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
            this.dislikes = new HashSet<Dislike>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(150), MinLength(2)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000), MinLength(2)]
        public string Description { get; set; }

        public int Rating { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Tag> Tags 
        { 
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Like> Likes
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

        public virtual ICollection<Dislike> Dislikes
        {
            get
            {
                return this.dislikes;
            }
            set
            {
                this.dislikes = value;
            }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
