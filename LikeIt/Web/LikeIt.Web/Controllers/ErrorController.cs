namespace LikeIt.Web.Controllers
{
    using System.Web.Mvc;

    using LikeIt.Data.Contracts;

    public class ErrorController : BaseController
    {
        public ErrorController(ILikeItData data)
            : base(data)
        {
        }

        public ViewResult Index()
        {
            return this.View("Error");
        }

        public ViewResult Error404()
        {
            Response.StatusCode = 404;
            return this.View();
        }

        public ViewResult Error500()
        {
            Response.StatusCode = 500;
            return this.View();
        }
    }
}