namespace LikeIt.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Web.Areas.Private.Controllers.Base;
    using LikeIt.Web.Infrastructure.HtmlHelpers;
    using LikeIt.Web.Infrastructure.Populators;
    using LikeIt.Web.ViewModels.Page;

    public class MyPagesController : PrivateController
    {
        public const int PageSize = 6;

        public MyPagesController(ILikeItData data, IDropDownListPopulator populator)
            : base(data, populator)
        {
        }

        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var pages = this.data.Pages.All()
                .Where(p => p.UserId == this.CurrentUser.Id);

            if (string.IsNullOrEmpty(searchString))
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }

            pages = FilterHelper.FilterSearchString(searchString, pages);

            ViewBag.CurrentFilter = searchString;

            var viewModel = pages
                .OrderByDescending(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>();

            int pageNumber = page ?? 1;

            return this.View(viewModel.ToPagedList(pageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult Search(string searchString, string likes)
        {
             var pages = this.data.Pages
                .All()
                .Where(p => p.UserId == this.CurrentUser.Id);

             pages = FilterHelper.FilterSearchString(searchString, pages);

             if (pages.Count() == 0)
             {
                 return this.Content(GlobalConstants.NoPagesFound);
             }

             var viewModel = pages
                      .OrderByDescending(p => p.Rating)
                      .Project()
                      .To<ListPagesViewModel>();

             return this.PartialView(GlobalConstants.PagesListPartial, viewModel.ToPagedList(1, int.MaxValue));
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
                return this.Content(GlobalConstants.NoPagesFoundInCategory);
            }

            return this.PartialView(GlobalConstants.PagesListPartial, viewModel.ToPagedList(1, int.MaxValue));
        }
    }
}