namespace LikeIt.Web.Areas.Administration.ViewModels.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public abstract class AdministrationViewModel
    {
        [Display(Name = "Added on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Is Deleted?")]
        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? DeletedOn { get; set; }
    }
}