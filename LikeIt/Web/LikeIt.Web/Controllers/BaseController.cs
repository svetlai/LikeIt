namespace LikeIt.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.AspNet.Identity;

    using LikeIt.Data.Contracts;
    using LikeIt.Models;

    [HandleError]
    public abstract class BaseController : Controller
    {
        protected ILikeItData data;
       
        public BaseController(ILikeItData data)
        {
            this.data = data;
        }

        protected User CurrentUser { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.CurrentUser = this.data.Users.All().Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name).FirstOrDefault();
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}