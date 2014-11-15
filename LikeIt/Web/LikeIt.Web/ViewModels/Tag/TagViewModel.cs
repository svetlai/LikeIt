namespace LikeIt.Web.ViewModels.Tag
{
    using LikeIt.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class TagViewModel : IMapFrom<LikeIt.Models.Tag>
    {
        [Key]
        [HiddenInput()]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}