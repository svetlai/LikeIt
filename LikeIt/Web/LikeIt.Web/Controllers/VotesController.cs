namespace LikeIt.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using LikeIt.Common;
    using LikeIt.Data.Contracts;
    using LikeIt.Models;
    using LikeIt.Web.ViewModels;

    public class VotesController : BaseController
    {
        public VotesController(ILikeItData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Like(int id)
        {
            var page = this.data.Pages.Find(id);
            if (page == null)
            {
                throw new HttpException(404, GlobalConstants.PageNotFound);
            }

            var existingLike = this.data.Likes
                .All()
                .Where(l => l.UserId == this.CurrentUser.Id && l.PageId == page.Id && l.IsDeleted != true)
                .FirstOrDefault();

            var existingDislike = this.data.Dislikes
                .All()
                .Where(d => d.UserId == this.CurrentUser.Id && d.PageId == page.Id && d.IsDeleted != true)
                .FirstOrDefault();

            if (existingDislike != null)
            {
                this.data.Dislikes.Delete(existingDislike);
                page.Rating++;
                this.data.SaveChanges();
            }

            if (existingLike == null)
            {
                var like = new Like
                {
                    UserId = this.CurrentUser.Id,
                    CreatedOn = DateTime.Now
                };

                page.Likes.Add(like);
                page.Rating++;
                this.data.SaveChanges();
            }
            else
            {
                this.data.Likes.Delete(existingLike);
                page.Rating--;
                this.data.SaveChanges();
            }

            var viewModel = Mapper.Map<VotesViewModel>(page);

            return this.PartialView(GlobalConstants.RatingPartial, viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Dislike(int id)
        {
            var page = this.data.Pages.Find(id);
            if (page == null)
            {
                throw new HttpException(404, GlobalConstants.PageNotFound);
            }

            var existingLike = this.data.Likes
                .All()
                .Where(d => d.UserId == this.CurrentUser.Id && d.PageId == page.Id && d.IsDeleted != true)
                .FirstOrDefault();

            var existingDislike = this.data.Dislikes
                .All()
                .Where(d => d.UserId == this.CurrentUser.Id && d.PageId == page.Id && d.IsDeleted != true)
                .FirstOrDefault();

            if (existingLike != null)
            {
                this.data.Likes.Delete(existingLike);
                page.Rating--;
                this.data.SaveChanges();
            }

            if (existingDislike == null)
            {
                var dislike = new Dislike
                {
                    UserId = this.CurrentUser.Id,
                    CreatedOn = DateTime.Now
                };

                page.Dislikes.Add(dislike);
                page.Rating--;
                this.data.SaveChanges();
            }
            else
            {
                this.data.Dislikes.Delete(existingDislike);
                page.Rating++;
                this.data.SaveChanges();
            }

            var viewModel = Mapper.Map<VotesViewModel>(page);

            return this.PartialView(GlobalConstants.RatingPartial, viewModel);
        }
    }
}