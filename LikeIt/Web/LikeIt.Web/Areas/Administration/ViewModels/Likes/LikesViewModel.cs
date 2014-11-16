namespace LikeIt.Web.Areas.Administration.ViewModels.Likes
{
    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class LikesViewModel : AdministrationViewModel, IMapFrom<Like>
    {
        public int? Id { get; set; }

        public int PageId { get; set; }

        public string UserId { get; set; }

    }
}