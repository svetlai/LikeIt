namespace LikeIt.Web.Areas.Private.ViewModels.Comments
{
    using System;
    using System.Web.Mvc;

    using AutoMapper;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        [HiddenInput()]
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(x => x.Author.UserName))
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ReverseMap();
        }
    }
}