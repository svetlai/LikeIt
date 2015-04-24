namespace LikeIt.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
   
    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Web.ViewModels.Home;
    using LikeIt.Web.ViewModels.Page;

    public class HomeController : BaseController
    {
        private const int LikesCount = 3;

        public HomeController(ILikeItData data)
            : base(data)
        {
        }

        // [OutputCache(Duration = 60 * 15)]
        public ActionResult Index()
        {
            var likes = this.data.Pages.All()
                .OrderByDescending(p => p.Rating)
                .Take(10)
                .Project()
                .To<IndexPagesViewModel>();

            return this.View(likes);
        }
        
        [ChildActionOnly]
        [OutputCache(Duration = 60 * 15)]
        public ActionResult GetTrendingLikes()
        {
            var trendingLikes = this.data.Pages.All()
                .OrderByDescending(p => p.Rating)
                .Take(LikesCount)
                .Project()
                .To<ListPagesViewModel>();

            return this.PartialView(GlobalConstants.TrendingLikesPartial, trendingLikes);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60 * 15)]
        public ActionResult GetTrendingDislikes()
        {
            var trendingDislikes = this.data.Pages.All()
               .OrderBy(p => p.Rating)
               .Take(LikesCount)
               .Project()
               .To<ListPagesViewModel>();

            return this.PartialView(GlobalConstants.TrendingDislikesPartial, trendingDislikes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}