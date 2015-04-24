namespace LikeIt.Web.ViewModels.Contracts
{
    using System;
    using System.Web;

    public interface IHaveImage
    {
        HttpPostedFileBase UploadedImage { get; set; }
    }
}
