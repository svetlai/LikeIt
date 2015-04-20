﻿namespace LikeIt.Web.Areas.Private.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Controllers;
    using LikeIt.Web.ViewModels.Page;
    using LikeIt.Web.Areas.Private.Controllers.Base;
    using System.Web;
    using LikeIt.Web.ViewModels.Categories;
    using LikeIt.Web.Infrastructure.Populators;

    public class MyLikesController : PrivateController
    {
        public const int PageSize = 6;

        public MyLikesController(ILikeItData data, IDropDownListPopulator populator)
            : base(data, populator)
        {
        }

        public ActionResult Index(int? page)
        {
            var pages = this.data.Likes.All()
                .Where(l => l.UserId == this.CurrentUser.Id)
                .Select(l => l.Page)
                .OrderByDescending(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>();

            int pageNumber = (page ?? 1);

            return View(pages.ToPagedList(pageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult Search(string searchString, string likes)
        {
            var pages = this.data.Likes.All()
                    .Where(l => l.UserId == this.CurrentUser.Id)
                    .Select(l => l.Page)
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()) || p.Tags.Any(t => t.Name.Contains(searchString.ToLower())))
                    .OrderBy(p => p.Rating)
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
            var pages = this.data.Likes.All()
                    .Where(l => l.UserId == this.CurrentUser.Id)
                    .Select(l => l.Page)
                    .OrderBy(p => p.Rating);

            if (categoryId > -1)
            {
                pages = this.data.Likes.All()
                   .Where(l => l.UserId == this.CurrentUser.Id)
                   .Select(l => l.Page)
                   .Where(p => p.CategoryId == categoryId)
                   .OrderBy(p => p.Rating);
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