namespace LikeIt.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LikeIt.Data.Common.Models;

    public class Category : AuditInfo, IDeletableEntity
    {
        private ICollection<Page> pages;

        public Category()
        {
            this.pages = new HashSet<Page>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Page> Pages
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
