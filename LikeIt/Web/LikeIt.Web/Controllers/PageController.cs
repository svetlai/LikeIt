﻿namespace LikeIt.Web.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Web.ViewModels.Page;
    using LikeIt.Models;
    using LikeIt.Data.Contracts;
    using LikeIt.Web.ViewModels.Home;
    using LikeIt.Web.Infrastructure.Populators;
    using LikeIt.Web.ViewModels;
    using LikeIt.Web.ViewModels.Categories;

    public class PageController : BaseController
    {
        private IDropDownListPopulator populator;

        private Random random;

        public PageController(ILikeItData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
            this.random = new Random();
        }

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

        public ActionResult Details(int? id)
        {
            var page = this.data.Pages.All()
                .Where(x => x.Id == id)
                .Project()
                .To<DetailsPageViewModel>()
                .FirstOrDefault();

            if (page == null)
            {
                return this.HttpNotFound("The page you're looking for wasn't found.");
            }

            return View(page);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new AddPageViewModel
                {
                    Categories = this.populator.GetCategories()
                };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(AddPageViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var page = AutoMapper.Mapper.Map<Page>(model);
                page.UserId = base.CurrentUser.Id;
                page.CreatedOn = DateTime.Now;

                if (model.UploadedImage != null)
                {
                    page.Image = this.GetUploadedImage(model);
                }

                this.data.Pages.Add(page);
                this.data.SaveChanges();

                model.Id = page.Id;

                model.Categories = this.populator.GetCategories();

                if (model.TagsString != null)
                {
                    var tags = model.TagsString.Split(' ');

                    foreach (var tag in tags)
                    {
                        page.Tags.Add(new Tag
                        {
                            Name = tag
                        });
                    }
                }

                if (model.IsLike == true)
                {
                    page.Likes.Add(new Like
                    {
                        UserId = page.UserId
                    });
                }
                else
                {
                    page.Dislikes.Add(new Dislike
                    {
                        UserId = page.UserId
                    });
                }

                page.Rating = page.Likes.Where(l => !l.IsDeleted).Count() - page.Dislikes.Where(l => !l.IsDeleted).Count();

                this.data.SaveChanges();

                return this.RedirectToAction("Details", new { id = page.Id });
            }

            return View(model);
        }

        public ActionResult Random()
        {
            this.random = new Random();

            var pages = this.data.Pages
                .All()
                .Project()
                .To<DetailsPageViewModel>()
                .ToList();

            var page = pages[random.Next(0, pages.Count())];

            return View(page);
        }

        [HttpPost]
        public ActionResult FilterByCategory(int categoryId)
        {
            var pages = this.data.Pages
                .All().Where(p => p.CategoryId == categoryId)
                .Project()
                .To<ListPagesViewModel>();

            return PartialView("~/Views/Shared/_PagesListPartial.cshtml", pages);
            //return this.View(pages);
        }

        [ChildActionOnly]
        public ActionResult GetVotesPartial(int id)
        {
            var page = this.data.Pages.Find(id);

            var viewModel = Mapper.Map<VotesViewModel>(page);

            return PartialView("~/Views/Shared/_VotePartial.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult GetCategoriesPartial()
        {
            var viewModel = new ListCategoriesViewModel
            {
                Categories = this.populator.GetCategories()
            };

            return PartialView("~/Views/Shared/_CategoriesPartial.cshtml", viewModel);
        }

        public ActionResult Image (int id)
        {
            var image = this.data.Images.Find(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            var contentType = string.Empty;
            if (image.FileExtension.ToLower() == "jpg")
            {
                contentType = "image/jpeg";
            }
            else
            {
                contentType = "image/" + image.FileExtension;
            }

            return File(image.Content, contentType);
        }

        private Image GetUploadedImage(AddPageViewModel model)
        {
            using (var memory = new MemoryStream())
            {
                model.UploadedImage.InputStream.CopyTo(memory);
                var content = memory.GetBuffer();

                var fileExtension = model.UploadedImage.FileName.Split('.').Last();

                var image = new Image
                {
                    Content = content,
                    FileExtension = fileExtension,
                    CreatedOn = DateTime.Now,
                };

                this.data.Images.Add(image);
                this.data.SaveChanges();

                return image;
            }
        }
    }
}