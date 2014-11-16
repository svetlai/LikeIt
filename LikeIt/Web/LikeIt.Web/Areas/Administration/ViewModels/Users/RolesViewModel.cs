namespace LikeIt.Web.Areas.Administration.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LikeIt.Web.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RolesViewModel : IMapFrom<IdentityRole>
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}