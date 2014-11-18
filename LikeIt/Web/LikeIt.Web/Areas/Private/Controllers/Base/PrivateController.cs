namespace LikeIt.Web.Areas.Private.Controllers.Base
{
    using System.Web.Mvc;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Controllers;

    [Authorize]
    public abstract class PrivateController : BaseController
    {
        public PrivateController(ILikeItData data)
            : base(data)
        {
        }
    }
}