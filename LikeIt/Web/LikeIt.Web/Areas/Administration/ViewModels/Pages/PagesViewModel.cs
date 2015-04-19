namespace LikeIt.Web.Areas.Administration.ViewModels.Pages
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class PagesViewModel : AdministrationViewModel, IMapFrom<Page>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Name { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [StringLength(500), MinLength(10)]
        public string Description { get; set; }

        [Required]
        //[UIHint("DropDownList")]
        public int CategoryId { get; set; }

        //public int Rating { get; set; }
    }
}