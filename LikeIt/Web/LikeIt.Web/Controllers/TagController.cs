namespace LikeIt.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.ViewModels.Tag;

    //TODO
    public class TagController : BaseController
    {
        public TagController(ILikeItData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var tags = this.data.Tags.All()
                .Project()
                .To<TagViewModel>();

            return View(tags);
        }

        public ActionResult GetByTag(string tag)
        {
            return this.Content(tag);
        }
    }
}