namespace LikeIt.Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;

    public class ImageController : BaseController
    {
        public ImageController(ILikeItData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Image(Guid id)
        {
            var image = this.data.Images.Find(id);
            if (image == null)
            {
                throw new HttpException(404, GlobalConstants.ImageNotFound);
            }

            var contentType = string.Empty;
            if (image.FileExtension.ToLower() == "jpg")
            {
                contentType = "image/jpeg";
            }
            else
            {
                contentType = "image/" + image.FileExtension;
            }

            return this.File(image.Content, contentType);
        }
    }
}