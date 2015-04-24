namespace LikeIt.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Models;
    using LikeIt.Web.Areas.Private.ViewModels.Comments;
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
                comment.AuthorId = this.CurrentUser.Id;

                var page = this.data.Pages.Find(model.PageId);
                if (page == null)
                {
                    throw new HttpException(404, GlobalConstants.PageNotFound);
                }

                page.Comments.Add(comment);
                this.data.SaveChanges();

                var viewModel = Mapper.Map<CommentViewModel>(comment);

                return this.PartialView(GlobalConstants.SingleCommentPartialPrivate, viewModel);
            }

            throw new HttpException(400, GlobalConstants.InvalidComment);
        }

        [Authorize]
        public ActionResult Delete(int id, string userName)
        {
            var comment = this.data.Comments.Find(id);

            if (comment != null && comment.AuthorId == this.CurrentUser.Id)
            {
                this.data.Comments.Delete(comment);
                this.data.SaveChanges();
            }
            IQueryable<CommentViewModel> comments;

            if (!string.IsNullOrEmpty(userName))
            {
                comments = this.GetMyComments();
                return this.PartialView(GlobalConstants.MyCommentsPartialPrivate, comments);
            }

            comments = this.GetPageComments(comment.PageId);
            return this.PartialView(GlobalConstants.PageCommentsPartialPrivate, comments);
        }

        public ActionResult MyComments()
        {
            var comments = this.GetMyComments();

            return this.View(comments);
        }

        public ActionResult GetPageCommentsPartial(int pageId)
        {
            var comments = this.GetPageComments(pageId);

            return this.PartialView(GlobalConstants.PageCommentsPartialPrivate, comments);
        }

        private IQueryable<CommentViewModel> GetPageComments(int pageId)
        {
            return this.data.Comments.All()
                .Where(c => c.PageId == pageId && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<CommentViewModel>();
        }

        private IQueryable<CommentViewModel> GetMyComments()
        {
            return this.data.Comments.All()
                .Where(c => c.AuthorId == this.CurrentUser.Id && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<CommentViewModel>();
        }
    }
}