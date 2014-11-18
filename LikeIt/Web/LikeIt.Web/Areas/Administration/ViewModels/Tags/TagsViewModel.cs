namespace LikeIt.Web.Areas.Administration.ViewModels.Tags
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class TagsViewModel : AdministrationViewModel, IMapFrom<Tag>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [StringLength(20), MinLength(2)]
        public string Name { get; set; }
    }
}