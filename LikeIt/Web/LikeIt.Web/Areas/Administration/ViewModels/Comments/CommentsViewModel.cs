namespace LikeIt.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;

    public class CommentsViewModel : AdministrationViewModel, IMapFrom<Comment>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [UIHint("MultiLineText")]
        [StringLength(150), MinLength(2)]
        public string Content { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentsViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(x => x.Author.UserName))
                .ReverseMap();
        }
    }
}