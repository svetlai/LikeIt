namespace LikeIt.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PagedList;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Web.ViewModels.Tag;
    using LikeIt.Web.ViewModels.Page;

    public class TagController : BaseController
    {
        public TagController(ILikeItData data)
            : base(data)
        {
        }

        public ActionResult Index(string tag)
        {
            var tags = this.data.Tags.All()
                .Project()
                .To<TagViewModel>()
                .ToList();

            if (string.IsNullOrEmpty(tag))
            {
                return this.View(new TagsPagesViewModel { Tags = tags, Pages = null });
            }

            ViewBag.CurrentTag = tag;

            var pages = this.data.Pages.All()
                .Where(p => p.Tags.Any(t => t.Name.Contains(tag)))
                .OrderByDescending(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>()
                .ToPagedList(1, int.MaxValue);

            return this.View(new TagsPagesViewModel { Tags = tags, Pages = pages });          
        }

        public ActionResult GetPagesByTag(string tag)
        {
            var pages = this.data.Pages.All()
                .Where(p => p.Tags.Any(t => t.Name.Contains(tag)))
                .OrderByDescending(p => p.Rating)
                .Project()
                .To<ListPagesViewModel>();

            ViewBag.CurrentTag = tag;

            if (pages.Count() == 0)
            {
                return this.Content(GlobalConstants.NoPagesFound);
            }

            return this.PartialView(GlobalConstants.PagesListPartial, pages.ToPagedList(1, int.MaxValue));
        }
    }
}