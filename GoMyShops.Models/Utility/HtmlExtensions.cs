using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace GoMyShops.Models.Utility
{
    public static class HtmlExtensions
    {
        public static IHtmlContent ActionLinkWithSpan(this IHtmlHelper html,
                        string linkText,
                        string actionName,
                        string controllerName,
                        string SpanClass,
                        string iconClass,
                        bool topMenu,
                        object htmlAttributes)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            // System.Web.Mvc.RouteCollectionExtensions
            // RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            TagBuilder linkTag = new TagBuilder("a");
            TagBuilder spanTag = new TagBuilder("span");

            TagBuilder spanTagAText = new TagBuilder("span");
            //Todo Harris (Test) Modify Core
            spanTagAText.InnerHtml.AppendHtml(linkText);
            //spanTagAText.SetInnerText(linkText);

            //i tag
            TagBuilder iTagAText = new TagBuilder("i");
            iTagAText.AddCssClass(iconClass);
            iTagAText.Attributes.Add("aria-hidden","true");


            spanTagAText.AddCssClass(SpanClass);
            //spanTag.SetInnerText(linkText);

            //linkTag.SetInnerText(linkText);
            linkTag.MergeAttributes(attributes, false);

            //Todo Harris (Test) Modify Core
            //UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);

            if (topMenu)
            {
                linkTag.Attributes.Add("href", "#" + linkText.Replace(" ",""));
                linkTag.Attributes.Add("data-toggle", "collapse");
                linkTag.Attributes.Add("aria-expanded", "false");
            }
            else
            {
                //Todo Harris (Test) Modify Core
                var urlHelperFactory = html.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
                var urlHelper = urlHelperFactory.GetUrlHelper(html.ViewContext);
                var url = urlHelper.Action(actionName,controllerName,new { htmlAttributes });                
                linkTag.Attributes.Add("href", url);
            }


            //Todo Harris (Test) Modify Core
            linkTag.InnerHtml.AppendHtml(iTagAText );
            linkTag.InnerHtml.AppendHtml(spanTagAText);
            //linkTag.InnerHtml = iTagAText.ToString(TagRenderMode.Normal) + spanTagAText.ToString(TagRenderMode.Normal); /*+ spanTag.ToString(TagRenderMode.Normal);*/

            //Todo Harris (Test) Modify Core
            return linkTag;
            //return MvcHtmlString.Create(linkTag.ToString(TagRenderMode.Normal));
        }
    }//end class
}//end namespace
