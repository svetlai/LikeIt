namespace LikeIt.Web.Areas.Administration.ViewModels.Dislikes
{
    using System.Web.Mvc;

    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class DislikesViewModel : AdministrationViewModel, IMapFrom<Dislike>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public int PageId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }
    }
}