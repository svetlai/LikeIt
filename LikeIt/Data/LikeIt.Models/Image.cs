namespace LikeIt.Models
{
    using LikeIt.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image : AuditInfo, IDeletableEntity
    {
        private ICollection<Page> pages;

        public Image()
        {
            this.pages = new HashSet<Page>();
        }

        public int Id { get; set; }

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
