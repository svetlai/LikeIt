﻿namespace LikeIt.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Web.Areas.Private.Controllers.Base;
    using LikeIt.Web.Infrastructure.Populators;
    using LikeIt.Web.ViewModels.Page;
    using LikeIt.Web.Infrastructure.HtmlHelpers;

    public class MyDislikesController : PrivateController
    {
        public const int PageSize = 6;

        public MyDislikesController(ILikeItData data, IDropDownListPopulator populator)
            : base(data, populator)
        {
        }

        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var pages = this.data.Dislikes.All()
                .Where(d => d.UserId == this.CurrentUser.Id)
                .Select(d => d.Page);

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
                .OrderBy(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>();

            int pageNumber = page ?? 1;

            return this.View(viewModel.ToPagedList(pageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult Search(string searchString, string likes)
        {
            var pages = this.data.Dislikes.All()
                       .Where(d => d.UserId == this.CurrentUser.Id)
                       .Select(d => d.Page);

            pages = FilterHelper.FilterSearchString(searchString, pages);

            if (pages.Count() == 0)
            {
                return this.Content(GlobalConstants.NoPagesFound);
            }

            var viewModel = pages
                       .OrderBy(p => p.Rating)
                       .Project()
                       .To<ListPagesViewModel>();

            return this.PartialView(GlobalConstants.PagesListPartial, viewModel.ToPagedList(1, int.MaxValue));
        }

        [HttpGet]
        public ActionResult FilterByCategory(int categoryId)
        {
            var pages = this.data.Dislikes.All()
                     .Where(d => d.UserId == this.CurrentUser.Id)
                     .Select(d => d.Page);

            if (categoryId > -1)
            {
                pages = pages
                    .Where(p => p.CategoryId == categoryId);
            }

            var viewModel = pages.OrderBy(p => p.Rating)
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