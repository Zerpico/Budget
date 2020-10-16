using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Threading.Tasks;

namespace Budget.Web.Helpers
{
    
    [HtmlTargetElement("input", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InvariantDecimalTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";

        private IHtmlGenerator _generator;

        [HtmlAttributeName("asp-is-invariant")]
        public bool IsInvariant { set; get; }

        public InvariantDecimalTagHelper(IHtmlGenerator generator) : base(generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (IsInvariant && output.TagName == "input" && For.Model != null && For.Model.GetType() == typeof(decimal))
            {
                decimal value = (decimal)(For.Model);
                var invariantValue = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                output.Attributes.SetAttribute(new TagHelperAttribute("value", invariantValue));
            }
        }
    }


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
