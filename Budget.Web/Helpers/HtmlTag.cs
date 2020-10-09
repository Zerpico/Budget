using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.Web.Helpers
{
    public static class HtmlTag
    {
        public static HtmlString IsActive(this IHtmlHelper html, string action, string controller)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = string.IsNullOrEmpty(action) ? null : (string)routeData.Values["action"];
            var routeController = (string)routeData.Values["controller"];

            var isActive = string.Equals(controller, routeController, StringComparison.InvariantCultureIgnoreCase)
                           && string.Equals(action, routeAction, StringComparison.InvariantCultureIgnoreCase);

            return isActive ? new HtmlString("active") : null;
        }

        public static HtmlString CreateList(this IHtmlHelper html, string[] items)
        {
            string result = "<ul>";
            foreach (string item in items)
            {
                result = $"{result}<li>{item}</li>";
            }
            result = $"{result}</ul>";
            return new HtmlString(result);
        }
    }
}
