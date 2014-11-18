namespace LikeIt.Web.ViewModels.Page
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;
    using LikeIt.Web.ViewModels.Comment;
    using LikeIt.Web.ViewModels.Tag;
    using LikeIt.Web.Areas.Private.ViewModels.Comments;

    public class DetailsPageViewModel : IMapFrom<Page>, IHaveCustomMappings
    {
        [HiddenInput()]
        public int Id { get; set; }

        [UIHint("PageName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        [UIHint("CategoryName")]
        public string CategoryName { get; set; }

        public virtual IEnumerable<TagViewModel> Tags { get; set; }

        public virtual ICollection<CommentViewModel> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Dislike> Dislikes { get; set; }

        public Image Image { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<LikeIt.Models.Page, DetailsPageViewModel>()
                .ForMember(m => m.Rating, opt => opt.MapFrom(x => x.Rating))
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(x => x.User.UserName))
                .ReverseMap();
        }
    }
}