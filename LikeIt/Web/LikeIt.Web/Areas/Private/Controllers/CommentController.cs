namespace LikeIt.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using LikeIt.Data.Contracts;
    using LikeIt.Models;
    using LikeIt.Web.Areas.Private.ViewModels.Comments;
    using LikeIt.Web.Controllers;
    using LikeIt.Web.ViewModels.Comment;
    using LikeIt.Web.Areas.Private.Controllers.Base;

    public class CommentController : PrivateController
    {
        public CommentController(ILikeItData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var comment = Mapper.Map<Comment>(model);
                comment.AuthorId = base.CurrentUser.Id;

                var page = this.data.Pages.Find(model.PageId);
                if (page == null)
                {
                    throw new HttpException(404, "Page not found");
                }

                page.Comments.Add(comment);
                this.data.SaveChanges();

                var viewModel = Mapper.Map<CommentViewModel>(comment);

                return PartialView("~/Areas/Private/Views/Shared/_CommentsPartial.cshtml", viewModel);
            }

            throw new HttpException(400, "Invalid comment");
        }

        public ActionResult MyComments()
        {
            var comments = this.data.Comments.All()
                .Where(c => c.AuthorId == this.CurrentUser.Id)
                //.OrderBy(c => c.PageId)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<CommentViewModel>();

            return View(comments);
        }
    }
}