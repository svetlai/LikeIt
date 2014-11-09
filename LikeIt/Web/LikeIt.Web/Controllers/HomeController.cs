namespace LikeIt.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        public HomeController(ILikeItData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var likes = this.data.Likes.All()
                .Project()
                .To<IndexLikesViewModel>();

            return View(likes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}