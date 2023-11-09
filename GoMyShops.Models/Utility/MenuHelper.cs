using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Web.Mvc.Html;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace GoMyShops.Models.Utility
{

    public interface IMenuCreation
    {
        IHtmlContent BuildMenu(List<IAdminMenuItem> menu);
    }

    public  class MenuCreation: IMenuCreation
    {
        //IUrlHelper _urlHelper ;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public MenuCreation(IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = contextAccessor;
            _linkGenerator = linkGenerator;
            //_urlHelper = contextAccessor.HttpContext.RequestServices.GetRequiredService<IUrlHelper>();
        }

        public  IHtmlContent BuildMenu( List<IAdminMenuItem> menu)
        {
            //Todo Harris (Test) Modify Core
            return new HtmlString(BuildStringMenu(menu));
        }

        private  IHtmlContent GenerateActionLink(string DisplayText, string ActionName, string ControllerName, string SpanClass, string HTMLClass,string IconClass,bool topMenu)
        {

            if (HTMLClass == string.Empty)
            {
                //return MvcHtmlString.Create("@Html.ActionLink(\"" + DisplayText +"\",\"" + ActionName +"\",\"" + ControllerName + "\")");
                //return helper.ActionLink(DisplayText, ActionName, ControllerName);
                //if (ControllerName != string.Empty)
                //{
                return ActionLinkWithSpan( DisplayText, ActionName, ControllerName, SpanClass, IconClass, topMenu, new { });
                //}
                //else 
                //{
                //    return MvcHtmlString.Create("<span>" + DisplayText + "</span>");
                //}
            }
            else
            {
                //return MvcHtmlString.Create("@Html.ActionLink(\"" + DisplayText +"\",\"" + ActionName +"\",\"" + ControllerName + "\",new { @class = \"" + HTMLClass +"\"})");
                // return helper.ActionLink(DisplayText, ActionName, ControllerName, new { @class =  HTMLClass });
                //if (ControllerName != string.Empty)
                //{
                return ActionLinkWithSpan( DisplayText, ActionName, ControllerName, SpanClass, IconClass, topMenu, new { @class = HTMLClass });
                //}
                //else
                //{
                //    return MvcHtmlString.Create("<span class=\"" +   HTMLClass + "\">" + DisplayText + "</span>");
                //}
            }//end if-else  

        }

        private  string BuildStringMenu( List<IAdminMenuItem> menu)
        {
            var sb1 = new StringBuilder();
            sb1.Append("<ul class=\"list-unstyled components\">");
            sb1.Append(BuildStringMenuSub( menu, 1));
            sb1.Append("</ul>");
            return sb1.ToString();
        }

        public  string GetString( IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        private  string BuildStringMenuSub( List<IAdminMenuItem> menu, int level)
        {
            var sb = new StringBuilder();
            if ((menu != null) && (menu.Count > 0))
            {

                foreach (var item in menu)
                {
                    if (item.TopMenu == true)
                    {
                        sb.Append("<li class=\"\">");

                        if ((item.Children != null) && (item.Children.Count > 0))
                        {
                            var output = GenerateActionLink( item.DisplayText, item.ActionName, item.ControllerName, "", "", item.IconName, true);
                            sb.Append(GetString(output));

                        }
                        else
                        {                  
                            var output = GenerateActionLink( item.DisplayText, item.ActionName, item.ControllerName, string.Empty, "top_link", item.IconName, false);
                            sb.Append(GetString(output));
                        }//end if-else
                    }
                    else
                    {
                        if (item.ModuleType != "L")
                        {
                            if (level >= 2 && ((item.Children != null) && (item.Children.Count > 0)))
                            {
                                sb.Append("<li class=''>");
                            }
                            else
                            {
                                sb.Append("<li>");
                            }
                        }

                            
                        if ((item.Children != null) && (item.Children.Count > 0))
                        {
                            if (item.ModuleType == "L")
                            {
                                sb.Append("<li class='divider'/>");
                            }
                            else
                            {
                                var gal = GenerateActionLink(item.DisplayText, item.ActionName, item.ControllerName, string.Empty, "top_link", item.IconName, true);
                                sb.Append(GetString(gal));
                            }

                  
                        }
                        else
                        {
                            if (item.ModuleType == "L")
                            {
                                sb.Append("<li class='divider'/>");
                            }
                            else
                            {
                                var gal = GenerateActionLink(item.DisplayText, item.ActionName, item.ControllerName, string.Empty, string.Empty, item.IconName, false);
                                sb.Append(GetString(gal));
                            }
                        }//end if-else
                        //sb.Append("</li>");
                    }
                    //
                    if ((item.Children != null) && (item.Children.Count > 0))
                    {
                        if (level >= 2)
                        {
                            //sb.Append("<ul>");
                            sb.Append("<ul class=\"list-unstyled collapse\" id=\"" + item.DisplayText.Replace(" ", "") + "\">");
                        }
                        else
                        {
                            sb.Append("<ul class=\"list-unstyled collapse\" id=\"" + item.DisplayText.Replace(" ", "") + "\">");
                        }//end if-else

                        //sb.Append("<li>");
                        sb.Append(BuildStringMenuSub( item.Children, 2));
                        sb.Append("</ul>");
                        sb.Append("</li>");
                    }
                    else
                    {
                        sb.Append("</li>");
                    }
                }
            }
            return (sb.ToString());
        }

        public  IHtmlContent ActionLinkWithSpan(
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
            iTagAText.Attributes.Add("aria-hidden", "true");


            spanTagAText.AddCssClass(SpanClass);
            //spanTag.SetInnerText(linkText);

            //linkTag.SetInnerText(linkText);
            linkTag.MergeAttributes(attributes, false);

            //Todo Harris (Test) Modify Core
            //UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);

            if (topMenu)
            {
                linkTag.Attributes.Add("href", "#" + linkText.Replace(" ", ""));
                linkTag.Attributes.Add("data-toggle", "collapse");
                linkTag.Attributes.Add("aria-expanded", "false");
            }
            else
            {
                //Todo Harris (Test) Modify Core
                //var urlHelperFactory = html.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
                //var urlHelper = urlHelperFactory.GetUrlHelper(html.ViewContext);
               // var url = _urlHelper.Action(actionName, controllerName, new { htmlAttributes });
                var url = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, actionName, controllerName, new { htmlAttributes });

                linkTag.Attributes.Add("href", url);
            }


            //Todo Harris (Test) Modify Core
            linkTag.InnerHtml.AppendHtml(iTagAText);
            linkTag.InnerHtml.AppendHtml(spanTagAText);
            //linkTag.InnerHtml = iTagAText.ToString(TagRenderMode.Normal) + spanTagAText.ToString(TagRenderMode.Normal); /*+ spanTag.ToString(TagRenderMode.Normal);*/

            //Todo Harris (Test) Modify Core
            return linkTag;
            //return MvcHtmlString.Create(linkTag.ToString(TagRenderMode.Normal));
        }
    }//end class
}//end namespace
