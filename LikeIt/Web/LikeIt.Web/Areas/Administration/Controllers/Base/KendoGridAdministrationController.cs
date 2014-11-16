namespace LikeIt.Web.Areas.Administration.Controllers.Base
{
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;

    using AutoMapper;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using LikeIt.Data.Contracts;

    public abstract class KendoGridAdministrationController : AdminController
    {
        public KendoGridAdministrationController(ILikeItData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T Find<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var pages =
                this.GetData()
                .ToDataSourceResult(request);

            return this.Json(pages, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.Find<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.data.Db.Entry(dbModel);
            entry.State = state;
            this.data.SaveChanges();
        }
    }
}