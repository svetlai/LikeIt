using AutoMapper;
using LikeIt.Data.Contracts;
using LikeIt.Models;
using LikeIt.Web.Areas.Private.ViewModels.Comments;
using LikeIt.Web.Controllers;
using LikeIt.Web.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LikeIt.Web.Areas.Private.Controllers
{
    public class CommentsController : BaseController
    {
        public CommentsController(ILikeItData data)
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
    }
}