namespace LikeIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LikeIt.Data.Common.Models;

    public class Image : AuditInfo, IDeletableEntity
    {
        private ICollection<Page> pages;

        public Image()
        {
            this.Id = Guid.NewGuid();
            this.pages = new HashSet<Page>();
        }

        [Key]
        public Guid Id { get; set; }

        public byte[] Content { get; set; }

        public string FileExtension { get; set; }

        public string Path { get; set; }

        public virtual ICollection<Page> Page 
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
