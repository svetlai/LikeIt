namespace LikeIt.Web.Controllers
{
    using System.Web.Mvc;

    using LikeIt.Data.Contracts;

    public abstract class BaseController : Controller
    {
        protected ILikeItData data;

        public BaseController(ILikeItData data)
        {
            this.data = data;
        }
    }
}