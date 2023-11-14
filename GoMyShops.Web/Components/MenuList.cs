using GoMyShops.BAL;
using GoMyShops.Controllers;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Text.Encodings.Web;

using System.Text;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http.Extensions;

namespace GoMyShops.Web.Components
{
    public class MenuList : ViewComponent
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IModulesBAL _modulesBAL;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelper _iUrlHelper;
        public MenuList(IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, IModulesBAL modulesBAL)
        {

            _httpContextAccessor = httpContextAccessor;
            _modulesBAL = modulesBAL;
            _actionContextAccessor = actionContextAccessor;
            _iUrlHelper =  urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IList<IAdminMenuItem> adminMenuList ;
            //List<IAdminMenuItem> MenuList = new List<IAdminMenuItem>();

            if (_httpContextAccessor.HttpContext.Session.GetString("UICulture") == null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UICulture" ,Thread.CurrentThread.CurrentUICulture.ThreeLetterWindowsLanguageName);
            }//end if

            if (User.Identity.IsAuthenticated)
            {
                adminMenuList = await _modulesBAL.SelectModules(false);
            }
            else
            {
                adminMenuList = await _modulesBAL.SelectModules(true);
            }

            var a =new HtmlString(BuildStringMenu( adminMenuList));
    
            return new HtmlContentViewComponentResult(a);
           // return View("Default", adminMenuList);
        }

        private  string BuildStringMenu( IList<IAdminMenuItem> menu)
        {
            var sb1 = new StringBuilder();
            sb1.Append("<ul class=\"list-unstyled components\">");
            sb1.Append(BuildStringMenuSub( menu, 1));
            sb1.Append("</ul>");
            return sb1.ToString();
        }

        //public  string GetString( IHtmlContent content)
        //{
        //    var writer = new System.IO.StringWriter();
        //    content.WriteTo(writer, HtmlEncoder.Default);
        //    return writer.ToString();
        //}

        private  string BuildStringMenuSub( IList<IAdminMenuItem> menu, int level)
        {
            var sb = new StringBuilder();
            if ((menu != null) && (menu.Count > 0))
            {

                foreach (var item in menu)
                {
                    if (item.TopMenu == true)
                    {
                        sb.Append("<li class=\"\">");

                        var writer = new System.IO.StringWriter();
                        if ((item.Children != null) && (item.Children.Count > 0))
                        {
                            var output = GenerateActionLink( item.DisplayText, item.ActionName, item.ControllerName, "", "", item.IconName, true);
                            
                            output.WriteTo(writer, HtmlEncoder.Default);
                            sb.Append(writer.ToString());

                        }
                        else
                        {
                            var output = GenerateActionLink(item.DisplayText, item.ActionName, item.ControllerName, "", "top_link", item.IconName, false);
                            output.WriteTo(writer, HtmlEncoder.Default);
                            sb.Append(writer.ToString());
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
                                sb.Append(GetString(GenerateActionLink( item.DisplayText, item.ActionName, item.ControllerName, string.Empty, "top_link", item.IconName, true)));
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
                                var returnLink=GenerateActionLink(item.DisplayText, item.ActionName, item.ControllerName, string.Empty, string.Empty, item.IconName, false);
                                sb.Append(GetString( returnLink));
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
        private  IHtmlContent GenerateActionLink( string DisplayText, string ActionName, string ControllerName, string SpanClass, string HTMLClass, string IconClass, bool topMenu)
        {
            //TagHelperOutput output;

            if (HTMLClass == string.Empty)
            {
                //return MvcHtmlString.Create("@Html.ActionLink(\"" + DisplayText +"\",\"" + ActionName +"\",\"" + ControllerName + "\")");
                //return _htmlHelper.ActionLink(DisplayText, ActionName, ControllerName);
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
                // return _htmlHelper.ActionLink(DisplayText, ActionName, ControllerName, new { @class =  HTMLClass });
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
                linkTag.Attributes.Add("data-bs-toggle", "collapse");
                linkTag.Attributes.Add("aria-expanded", "false");
            }
            else
            {
                var url = _iUrlHelper.Action(actionName, controllerName);
                //var url =_iUrlHelper.Action(actionName, controllerName, new { htmlAttributes });
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

        public string GetString(IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }//end class
}//end namespace