namespace LikeIt.Web.Areas.Administration.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class CategoriesViewModel : AdministrationViewModel, IMapFrom<Category>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [UIHint("String")]
        public string Name { get; set; }
    }
}