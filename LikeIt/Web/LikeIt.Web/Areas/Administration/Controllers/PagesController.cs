namespace LikeIt.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Areas.Administration.Controllers.Base;

    using Model = LikeIt.Models.Page;
    using ViewModel = LikeIt.Web.Areas.Administration.ViewModels.Pages.PagesViewModel;

    public class PagesController : KendoGridAdministrationController
    {
        public PagesController(ILikeItData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Pages.All()
                .Project()
                .To<ViewModel>();
        }

        protected override T Find<T>(object id)
        {
            return this.data.Pages.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Pages.Delete(model.Id);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}