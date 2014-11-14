namespace LikeIt.Web.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class AddCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public AddCommentViewModel()
        {
        }

        public AddCommentViewModel(int pageId)
        {
            this.PageId = pageId;
        }

        [HiddenInput()]
        public int Id { get; set; }

        [Required]
        [UIHint("MultiLineText")]
        [StringLength(150), MinLength(3)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public int PageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, AddCommentViewModel>()
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ReverseMap();
        }
    }
}