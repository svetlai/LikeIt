﻿namespace LikeIt.Web.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;

    public class AddCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public AddCommentViewModel()
        {
        }

        public AddCommentViewModel(int pageId)
        {
            this.PageId = pageId;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("MultiLineText")]
        [StringLength(150), MinLength(2)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        // [Required]
        public string AuthorId { get; set; }

        // [Required]
        public int PageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, AddCommentViewModel>()
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ReverseMap();
        }
    }
}