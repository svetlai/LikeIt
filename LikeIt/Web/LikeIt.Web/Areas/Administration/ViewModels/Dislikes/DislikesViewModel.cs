namespace LikeIt.Web.Areas.Administration.ViewModels.Dislikes
{
    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class DislikesViewModel : AdministrationViewModel, IMapFrom<Dislike>
    {
        public int? Id { get; set; }

        public int PageId { get; set; }

        public string UserId { get; set; }
    }
}