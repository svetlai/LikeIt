﻿namespace LikeIt.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Areas.Administration.Controllers.Base;

    using Model = LikeIt.Models.Comment;
    using ViewModel = LikeIt.Web.Areas.Administration.ViewModels.Comments.CommentsViewModel;

    public class CommentsController : KendoGridAdministrationController
    {
        public CommentsController(ILikeItData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Comments.All()
                .Project()
                .To<ViewModel>();
        }

        protected override T Find<T>(object id)
        {
            return this.data.Comments.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
            {
                model.Id = dbModel.Id;
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id.Value);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null) // && ModelState.IsValid
            {
                this.data.Comments.Delete(model.Id.Value);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}