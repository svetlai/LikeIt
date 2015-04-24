namespace LikeIt.Web.ViewModels.Tag
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LikeIt.Web.Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<LikeIt.Models.Tag>
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}