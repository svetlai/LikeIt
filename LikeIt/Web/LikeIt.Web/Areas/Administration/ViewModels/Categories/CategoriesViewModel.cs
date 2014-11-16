using LikeIt.Models;
using LikeIt.Web.Areas.Administration.ViewModels.Base;
using LikeIt.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LikeIt.Web.Areas.Administration.ViewModels.Categories
{
    public class CategoriesViewModel : AdministrationViewModel, IMapFrom<Category>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [UIHint("String")]
        public string Name { get; set; }
    }
}