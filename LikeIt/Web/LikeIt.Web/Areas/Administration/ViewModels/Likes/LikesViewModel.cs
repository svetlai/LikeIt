namespace LikeIt.Web.Areas.Administration.ViewModels.Likes
{
    using System.Web.Mvc;

    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class LikesViewModel : AdministrationViewModel, IMapFrom<Like>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public int PageId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }
    }
}