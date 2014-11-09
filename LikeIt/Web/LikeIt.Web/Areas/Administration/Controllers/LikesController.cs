using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LikeIt.Web.Areas.Administration.Controllers
{
    public class LikesController : Controller
    {
        // GET: Administration/Likes
        public ActionResult Index()
        {
            return View();
        }
    }
}