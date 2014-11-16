namespace LikeIt.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Controllers;
    using LikeIt.Common;

    //[Authorize(Roles = GlobalConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
        public AdminController(ILikeItData data)
            : base(data)
        {
        }
    }
}