namespace LikeIt.Web.ViewModels.Page
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;
    using LikeIt.Web.ViewModels.Contracts;

    public class EditPageViewModel : IMapFrom<LikeIt.Models.Page>, IHaveCustomMappings, IHaveImage
    {
        [HiddenInput]
        public int Id { get; set; }

        [AllowHtml]
        [Required]
        [StringLength(150), MinLength(2)]
        public string Name { get; set; }

        [AllowHtml]
        [UIHint("MultiLineText")]
        [Required]
        [StringLength(1000), MinLength(10)]
        public string Description { get; set; }

        [UIHint("Website")]
        [StringLength(200), MinLength(8)]
        public string ExternalWebsite { get; set; }

        [Required]
        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [UIHint("CategoryName")]
        public string CategoryName { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        // [AllowHtml]
        // [Display(Name = "Tags")]
        // public string TagsString { get; set; }

        public Image Image { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<LikeIt.Models.Page, EditPageViewModel>()
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ReverseMap();
        }
    }
}