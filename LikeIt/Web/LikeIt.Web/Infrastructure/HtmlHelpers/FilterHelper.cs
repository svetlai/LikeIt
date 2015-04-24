namespace LikeIt.Web.Infrastructure.HtmlHelpers
{
    using System.Linq;

    using LikeIt.Models;

    public class FilterHelper
    {
        public static IQueryable<Page> FilterSearchString(string searchString, IQueryable<Page> pages)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return pages
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()) || p.Tags.Any(t => t.Name.Contains(searchString.ToLower())));
            }
            else
            {
                return pages;
            }
        }
    }
}