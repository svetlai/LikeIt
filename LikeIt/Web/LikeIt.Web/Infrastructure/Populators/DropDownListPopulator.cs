namespace LikeIt.Web.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using LikeIt.Data.Contracts;
    using LikeIt.Web.Infrastructure.Caching;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private ILikeItData data;
        private ICacheService cache;

        public DropDownListPopulator(ILikeItData data, ICacheService cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            var categories = this.cache.Get<IEnumerable<SelectListItem>>("categories",
                () =>
                {
                    var categoriesList = new List<SelectListItem>
                    {
                          { new SelectListItem{ Text="Select category", Value="-1", Selected = true } },
                    };

                    categoriesList.AddRange(this.data.Categories
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Value = c.Id.ToString(),
                           Text = c.Name
                       }));

                    return categoriesList;
                });

            return categories;
        }
    }
}