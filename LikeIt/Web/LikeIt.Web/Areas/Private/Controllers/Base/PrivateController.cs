namespace LikeIt.Web.Areas.Private.Controllers.Base
{
    using System.Web.Mvc;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Controllers;
    using LikeIt.Web.ViewModels.Categories;
    using LikeIt.Web.Infrastructure.Populators;

    [Authorize]
    public abstract class PrivateController : BaseController
    {
        private IDropDownListPopulator populator;

        public PrivateController(ILikeItData data)
            : base(data)
        {
        }

        public PrivateController(ILikeItData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetCategoriesPartial()
        {
            var viewModel = new ListCategoriesViewModel
            {
                Categories = this.populator.GetCategories(),
            };

            return PartialView("~/Views/Shared/_CategoriesPartial.cshtml", viewModel);
        }
    }
}