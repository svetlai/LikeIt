namespace LikeIt.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Models;
    using LikeIt.Web.Infrastructure;
    using LikeIt.Web.Infrastructure.HtmlHelpers;
    using LikeIt.Web.Infrastructure.Populators;
    using LikeIt.Web.ViewModels;
    using LikeIt.Web.ViewModels.Categories;
    using LikeIt.Web.ViewModels.Page;
    using LikeIt.Web.ViewModels.Contracts;
    using System.IO;

    public class PageController : BaseController
    {
        private const int PageSize = 6;
        private const int AjaxSearchResult = 3;

        private readonly char[] tagSeparators = new char[] { ',', ';' };
        private readonly ISanitizer sanitizer;

        private IDropDownListPopulator populator;
        private Random random;
        private ImageController imageController;

        public PageController(ILikeItData data, IDropDownListPopulator populator, ISanitizer sanitizer, ImageController imageController)
            : base(data)
        {
            this.populator = populator;
            this.random = new Random();
            this.sanitizer = sanitizer;
            this.imageController = imageController;
        }

        [HttpGet]
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var pages = this.data.Pages.All();

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

            var viewModel = pages.OrderByDescending(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>();

            int pageNumber = page ?? 1;

            return this.View(viewModel.ToPagedList(pageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var page = this.data.Pages.All()
                .Where(x => x.Id == id)
                .Project()
                .To<DetailsPageViewModel>()
                .FirstOrDefault();

            if (page == null)
            {
                return this.HttpNotFound(GlobalConstants.PageNotFound);
            }

            page.Comments = page.Comments.OrderByDescending(c => c.CreatedOn).ToList();

            return this.View(page);
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
                page.Description = this.sanitizer.Sanitize(model.Description);
                page.UserId = this.CurrentUser.Id;
                page.CreatedOn = DateTime.Now;

                if (model.UploadedImage != null)
                {
                    page.Image = this.GetUploadedImage(model as IHaveImage);
                }

                if (model.CategoryId == -1)
                {
                    page.CategoryId = this.data.Categories.All().FirstOrDefault(c => c.Name == "Other").Id;
                }

                this.data.Pages.Add(page);
                this.data.SaveChanges();

                model.Id = page.Id;

                model.Categories = this.populator.GetCategories();

                if (model.TagsString != null)
                {
                    var tags = model.TagsString.Split(this.tagSeparators, StringSplitOptions.RemoveEmptyEntries);

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

                page.Rating = this.GetPageRating(page);
                this.data.SaveChanges();

                return this.RedirectToAction("Details", new { id = page.Id });
            }

            return this.View(model);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var page = this.data.Pages.Find(id);

            if (page != null && page.UserId == this.CurrentUser.Id)
            {
                this.data.Pages.Delete(page);
                this.data.SaveChanges();
            }

            return this.RedirectToAction("Index", "MyPages", new { area = "Private" });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var page = this.data.Pages.Find(id);

            var model = new EditPageViewModel
                {
                    Id = id,
                    Name = page.Name,
                    Description = page.Description,
                    ExternalWebsite = page.ExternalWebsite,
                    CategoryId = page.CategoryId,
                    CategoryName = page.Category.Name,
                    Categories = this.populator.GetCategories(),
                    Image = page.Image,
                };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditPageViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var page = this.data.Pages.Find(id);

                if (page != null && page.UserId == this.CurrentUser.Id)
                {
                    int categoryId = page.CategoryId;
                    var pageImage = page.Image;
                    Mapper.Map<EditPageViewModel, Page>(model, page);

                    // TODO : better way?            
                    if (model.CategoryId == -1)
                    {
                        page.CategoryId = categoryId;
                    }

                    if (model.UploadedImage != null)
                    {
                        page.Image = this.GetUploadedImage(model as IHaveImage);
                    }
                    else
                    {
                        page.Image = pageImage;
                    }

                    this.data.Pages.Update(page);
                    this.data.SaveChanges();
                }

                return this.RedirectToAction("Details", "Page", new { id = page.Id });
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Random()
        {
            var pages = this.data.Pages
                .All()
                .Project()
                .To<DetailsPageViewModel>()
                .ToList();

            var page = pages[this.random.Next(0, pages.Count())];

            return this.View(page);
        }

        [HttpGet]
        public ActionResult Search(string searchString)
        {
            var pages = this.data.Pages
                .All();

            pages = FilterHelper.FilterSearchString(searchString, pages);

            if (pages.Count() == 0)
            {
                return this.Content(GlobalConstants.NoPagesFound);
            }

            var viewModel = pages
                .OrderByDescending(p => p.Rating)
                .Take(AjaxSearchResult)
                .Project()
                .To<ListPagesViewModel>();

            return this.PartialView(GlobalConstants.PagesListPartial, viewModel.ToPagedList(1, int.MaxValue));
        }

        [HttpGet]
        public ActionResult FilterByCategory(int categoryId)
        {
            var pages = this.data.Pages
                 .All()
                 .OrderByDescending(p => p.Rating);

            if (categoryId > -1)
            {
                pages = this.data.Pages
               .All()
               .Where(p => p.CategoryId == categoryId)
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

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetVotesPartial(int id)
        {
            var page = this.data.Pages.Find(id);

            var viewModel = Mapper.Map<VotesViewModel>(page);

            return this.PartialView(GlobalConstants.VotePartial, viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetRatingPartial(int id)
        {
            var page = this.data.Pages.Find(id);

            var viewModel = Mapper.Map<VotesViewModel>(page);

            return this.PartialView(GlobalConstants.RatingPartial, viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetCategoriesPartial()
        {
            var viewModel = new ListCategoriesViewModel
            {
                Categories = this.populator.GetCategories(),
            };

            return this.PartialView(GlobalConstants.CategoriesPartial, viewModel);
        }

        private int GetPageRating(Page page)
        {
            return page.Likes.Where(l => !l.IsDeleted).Count() - page.Dislikes.Where(l => !l.IsDeleted).Count();
        }

        public Image GetUploadedImage(IHaveImage model)
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