using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LikeIt.Web.Areas.Private.Controllers
{
    public class MyLikesController : Controller
    {
        // GET: Private/MyLikes
        public ActionResult Index()
        {
            return View();
        }
    }
}