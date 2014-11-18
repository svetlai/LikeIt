namespace LikeIt.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ListCategoriesViewModel
    {
        [Key]
        [UIHint("DropDownList")]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}