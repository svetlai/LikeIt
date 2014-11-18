﻿namespace LikeIt.Web.ViewModels.Page
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using LikeIt.Web.Infrastructure.Mapping;

    public class AddPageViewModel : IMapFrom<LikeIt.Models.Page>, IHaveCustomMappings
    {
        [HiddenInput()]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Name { get; set; }

        [UIHint("MultiLineText")]
        [StringLength(500), MinLength(20)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        [Required]
        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        public bool IsLike { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        [Display(Name = "Tags")]
        public string TagsString { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<LikeIt.Models.Page, AddPageViewModel>()
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ReverseMap();
        }
    }
}