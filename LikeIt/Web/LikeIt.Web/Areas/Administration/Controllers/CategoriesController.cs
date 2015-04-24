namespace LikeIt.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Areas.Administration.Controllers.Base;
    using LikeIt.Web.Infrastructure.Caching;

    using Model = LikeIt.Models.Category;
    using ViewModel = LikeIt.Web.Areas.Administration.ViewModels.Categories.CategoriesViewModel;

    public class CategoriesController : KendoGridAdministrationController
    {
        private readonly ICacheService service;

        public CategoriesController(ILikeItData data, ICacheService service)
            : base(data)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.data
                .Categories
                .All()
                .Project()
                .To<ViewModel>();
        }

        protected override T Find<T>(object id)
        {
            return this.data.Categories.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
            {
                model.Id = dbModel.Id;
            }

            this.ClearCategoryCache();
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            this.ClearCategoryCache();
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null) // && ModelState.IsValid
            {
                var category = this.data.Categories.Find(model.Id.Value);

                foreach (var pageId in category.Pages.Select(t => t.Id).ToList())
                {
                    var comments = this.data
                        .Comments
                        .All()
                        .Where(c => c.PageId == pageId)
                        .Select(c => c.Id)
                        .ToList();

                    foreach (var commentId in comments)
                    {
                        this.data.Comments.Delete(commentId);
                    }

                    this.data.SaveChanges();

                    this.data.Pages.Delete(pageId);
                }

                this.data.SaveChanges();

                this.data.Categories.Delete(category);
                this.data.SaveChanges();
            }

            this.ClearCategoryCache();
            return this.GridOperation(model, request);
        }

        private void ClearCategoryCache()
        {
            this.service.Clear("categories");
        }
    }
}