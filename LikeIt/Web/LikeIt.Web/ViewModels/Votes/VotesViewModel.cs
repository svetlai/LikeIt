namespace LikeIt.Web.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;

    public class VotesViewModel : IMapFrom<LikeIt.Models.Page>, IHaveCustomMappings
    {
        public VotesViewModel()
        {
        }

        public VotesViewModel(int pageId)
        {
            this.PageId = pageId;
        }

        public int Id { get; set; }

        public int PageId { get; set; }

        public int Rating { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<LikeIt.Models.Page, VotesViewModel>()
               .ForMember(m => m.Rating, opt => opt.MapFrom(x => x.Rating))
               .ForMember(m => m.LikesCount, opt => opt.MapFrom(x => x.Likes.Where(l => !l.IsDeleted).Count()))
               .ForMember(m => m.DislikesCount, opt => opt.MapFrom(x => x.Dislikes.Where(l => !l.IsDeleted).Count()))
               .ForMember(m => m.PageId, opt => opt.MapFrom(x => x.Id))
               .ReverseMap();
        }
    }
}