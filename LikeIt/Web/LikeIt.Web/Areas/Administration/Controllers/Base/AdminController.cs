namespace LikeIt.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
        public AdminController(ILikeItData data)
            : base(data)
        {
        }
    }
}