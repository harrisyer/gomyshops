using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Web.Mvc.Html;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace GoMyShops.Models.Helpers
{
    public static class MyHelpers
    {
        public static IHtmlContent BuildMenu(this IHtmlHelper helper, List<IAdminMenuItem> menu)
        {
            //Todo Harris (Test) Modify Core
            return new HtmlString(BuildStringMenu(helper, menu));
        }

        private static IHtmlContent GenerateActionLink(IHtmlHelper helper, string DisplayText, string ActionName, string ControllerName, string SpanClass, string HTMLClass,string IconClass,bool topMenu)
        {

            if (HTMLClass == string.Empty)
            {
                //return MvcHtmlString.Create("@Html.ActionLink(\"" + DisplayText +"\",\"" + ActionName +"\",\"" + ControllerName + "\")");
                //return helper.ActionLink(DisplayText, ActionName, ControllerName);
                //if (ControllerName != string.Empty)
                //{
                return Utility.HtmlExtensions.ActionLinkWithSpan(helper, DisplayText, ActionName, ControllerName, SpanClass, IconClass, topMenu, new { });
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
                return Utility.HtmlExtensions.ActionLinkWithSpan(helper, DisplayText, ActionName, ControllerName, SpanClass, IconClass, topMenu, new { @class = HTMLClass });
                //}
                //else
                //{
                //    return MvcHtmlString.Create("<span class=\"" +   HTMLClass + "\">" + DisplayText + "</span>");
                //}
            }//end if-else  

        }

        private static string BuildStringMenu(IHtmlHelper helper, List<IAdminMenuItem> menu)
        {
            var sb1 = new StringBuilder();
            sb1.Append("<ul class=\"list-unstyled components\">");
            sb1.Append(BuildStringMenuSub(helper, menu, 1));
            sb1.Append("</ul>");
            return sb1.ToString();
        }

        public static string GetString(this IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        private static string BuildStringMenuSub(IHtmlHelper helper, List<IAdminMenuItem> menu, int level)
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
                            var output = GenerateActionLink(helper, item.DisplayText, item.ActionName, item.ControllerName, "", "", item.IconName, true);
                            sb.Append(GetString(output));

                        }
                        else
                        {                  
                            var output = GenerateActionLink(helper, item.DisplayText, item.ActionName, item.ControllerName, string.Empty, "top_link", item.IconName, false);
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
                                sb.Append(GenerateActionLink(helper, item.DisplayText, item.ActionName, item.ControllerName, string.Empty, "top_link", item.IconName, true));
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
                                sb.Append(GenerateActionLink(helper, item.DisplayText, item.ActionName, item.ControllerName, string.Empty, string.Empty, item.IconName, false));
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
                        sb.Append(BuildStringMenuSub(helper, item.Children, 2));
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
    }//end class
}//end namespace
