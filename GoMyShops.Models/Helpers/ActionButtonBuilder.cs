using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
namespace GoMyShops.Models.Helpers
{
    public static class ActionButtonBuilder
    {
        public static IHtmlContent BuildActionLinkButton(this IHtmlHelper helper, UrlHelper url, ActionButtonHelper albutton)
        {
            var menu = "\n";
            menu += "<a href=\"";
            menu += url.Action(albutton.Action, albutton.Controller, albutton.RouteValues);
            menu += "\">";
            menu += "<input class='btn btn-primary' type=\"submit\"";
            menu += "value=\"" + albutton.Value + "\"";
            menu += "name=\"" + albutton.Name + "\"";
            menu += "onclick=\"location.href=this.parentNode.href; return false;\"/></a>";

            //return menu;
            return new HtmlString(menu);
        }

        public static string BuildActionLinkButtonWithID(this HtmlHelper helper, UrlHelper url, ActionButtonHelper albutton)
        {
            var menu = "\n";
            menu += "<a href=\"";
            menu += url.Action(albutton.Action, albutton.Controller, albutton.RouteValues);
            menu += "\">";
            menu += "<input class='btn btn-primary' type=\"submit\"";
            menu += "value=\"" + albutton.Value + "\"";
            menu += "name=\"" + albutton.Name + "\"";
            menu += "id=\"" + albutton.Name + "\"";
            menu += "onclick=\"location.href=this.parentNode.href; return false;\"/></a>";

            return menu;
        }

        public static string BuildActionLinklikeButton(this HtmlHelper helper, UrlHelper url, ActionButtonHelper albutton)
        {
            var menu = "\n";
            menu += "<a id='btnApprove' class=\"linkbutton btn btn-primary \" " + " href=\"";
            menu += url.Action(albutton.Action, albutton.Controller, albutton.RouteValues);
            menu += "\">";
            //menu += "<input class='btn btn-primary' type=\"submit\"";
            //menu += "value=\"" + albutton.Value + "\"";
            //menu += "name=\"" + albutton.Name + "\"";
            //menu += "/></a>";
            menu += albutton.Value + "</a>";

            return menu;
        }


        public static IHtmlContent BuildCreateLinkButton(this HtmlHelper helper, UrlHelper url, ActionButtonHelper albutton)
        {
            var menu = "\n";
            menu += "<a href=\"";
            menu += url.Action(albutton.Action, albutton.Controller, albutton.RouteValues);
            menu += "\">";
            menu += "<input class='btn btn-primary' type=\"submit\"";
            menu += "value=\"" + albutton.Value + "\"";
            menu += "name=\"" + albutton.Name + "\"";
            menu += "onclick=\"location.href=this.parentNode.href; return false;\"/></a>";

            //return menu;
            return new HtmlString(menu);
        }

    }//end class
}//end namespace
