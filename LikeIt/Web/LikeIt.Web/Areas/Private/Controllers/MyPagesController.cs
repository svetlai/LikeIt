namespace LikeIt.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Areas.Private.Controllers.Base;
    using LikeIt.Web.Infrastructure.Populators;
    using LikeIt.Web.ViewModels.Page;

    public class MyPagesController : PrivateController
    {
        public const int PageSize = 6;

        public MyPagesController(ILikeItData data, IDropDownListPopulator populator)
            : base(data, populator)
        {
        }

        public ActionResult Index(int? page)
        {
            var pages = this.data.Pages.All()
                .Where(p => p.UserId == this.CurrentUser.Id)
                .OrderByDescending(p => p.Rating)

                .Project()
                .To<ListPagesViewModel>();

            int pageNumber = (page ?? 1);

            return View(pages.ToPagedList(pageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult Search(string searchString, string likes)
        {
            var pages = this.data.Pages.All()
                    .Where(p => p.UserId == this.CurrentUser.Id && (p.Name.ToLower().Contains(searchString.ToLower()) || p.Tags.Any(t => t.Name.Contains(searchString.ToLower()))))
                    .OrderByDescending(p => p.Rating)
                    .Project()
                    .To<ListPagesViewModel>();

            if (pages.Count() == 0)
            {
                return Content("No pages found");
            }

            return PartialView("_PagesListPartial", pages.ToPagedList(1, int.MaxValue));
        }

        [HttpGet]
        public ActionResult FilterByCategory(int categoryId)
        {
            var pages = this.data.Pages.All()
                .Where(p => p.UserId == this.CurrentUser.Id)
                .OrderByDescending(p => p.Rating);

            if (categoryId > -1)
            {
                pages = this.data.Pages.All()
                    .Where(p => p.UserId == this.CurrentUser.Id && p.CategoryId == categoryId)
                    .OrderByDescending(p => p.Rating);
            }

            var viewModel = pages
                    .Project()
                    .To<ListPagesViewModel>();

            if (pages.Count() == 0)
            {
                return Content("No pages found in this category");
            }

            return PartialView("_PagesListPartial", viewModel.ToPagedList(1, int.MaxValue));
        }
    }
}