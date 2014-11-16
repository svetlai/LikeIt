namespace LikeIt.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LikeIt.Data.Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tag : AuditInfo, IDeletableEntity
    {
        private ICollection<Page> pages;

        public Tag()
        {
            this.pages = new HashSet<Page>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20), MinLength(3)]
        public string Name { get; set; }

        public ICollection<Page> Pages
        {
            get
            {
                return this.pages;

            }
            set
            {
                this.pages = value;
            }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}
