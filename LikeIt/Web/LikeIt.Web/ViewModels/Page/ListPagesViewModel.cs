﻿namespace LikeIt.Web.ViewModels.Page
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;
    using LikeIt.Web.ViewModels.Tag;

    public class ListPagesViewModel : IMapFrom<Page>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public Image Image { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<TagViewModel> Tags { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Page, ListPagesViewModel>()
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ReverseMap();
        }
    }
}