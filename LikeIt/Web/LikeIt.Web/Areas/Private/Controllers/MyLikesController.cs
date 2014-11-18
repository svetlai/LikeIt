

namespace LikeIt.Web.Areas.Private.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Controllers;
    using LikeIt.Web.ViewModels.Page;
    public class MyLikesController : BaseController
    {
        public MyLikesController(ILikeItData data)
            : base(data)
        {
        }

        [Authorize]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pages = this.data.Pages.All()
                .Where(p => p.UserId == this.CurrentUser.Id)
                .OrderByDescending(p => p.Rating)

                .Project()
                .To<ListPagesViewModel>();

            if (!String.IsNullOrEmpty(searchString))
            {
                pages = pages
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(pages.ToPagedList(pageNumber, pageSize));

        }

        [Authorize]
        public ActionResult LikedByMe(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pages = this.data.Likes.All()
                .Where(l => l.UserId == this.CurrentUser.Id)
                .Select(l => l.Page)
                .OrderBy(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>();

            if (!String.IsNullOrEmpty(searchString))
            {
                pages = pages
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(pages.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult DislikedByMe(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pages = this.data.Dislikes.All()
                .Where(l => l.UserId == this.CurrentUser.Id)
                .Select(l => l.Page)
                .OrderBy(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>();

            if (!String.IsNullOrEmpty(searchString))
            {
                pages = pages
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(pages.ToPagedList(pageNumber, pageSize));
        }
    }
}