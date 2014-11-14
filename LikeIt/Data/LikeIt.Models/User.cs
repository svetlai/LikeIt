namespace LikeIt.Models
{
    using System;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using LikeIt.Data.Common.Models;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<Page> pages;
        private ICollection<Like> ratings;

        public User()
        {
            this.pages = new HashSet<Page>();
            this.ratings = new HashSet<Like>();
            this.CreatedOn = DateTime.Now;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

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

        public virtual ICollection<Like> Ratings
        {
            get
            {
                return this.ratings;
            }
            set
            {
                this.ratings = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
