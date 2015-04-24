namespace LikeIt.Web.Infrastructure.HtmlHelpers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;

    public static class HtmlHelperExtensions
    {
        // using timjames' approach
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string rawHtml, string action, string controller, AjaxOptions ajaxOptions, object routeValues = null, object htmlAttributes = null)
        {
            string linkText = Guid.NewGuid().ToString();
            string originalActionLink = ajaxHelper.ActionLink(linkText, action, controller, routeValues, ajaxOptions, htmlAttributes).ToString();

            return MvcHtmlString.Create(originalActionLink.Replace(linkText, rawHtml));
        }
    }
}