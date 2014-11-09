using LikeIt.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LikeIt.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(ILikeItData data)
            : base(data)
        {
        }

        public ViewResult Index()
        {
            return View("Error");
        }

        public ViewResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ViewResult Error500()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}