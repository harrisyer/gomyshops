using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Web.Routing;
using GoMyShops.BAL;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoMyShops.BAL.MVCFilters
{

    public class AntiForgeryHandleErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //TODO Harris Core-temp-off
            //if (context.Exception is Exce
            //    //&& !context.IsChildAction)
            //{
            //    context.ExceptionHandled = true;

            //    //If user is already logged in, this will eventually redirect to Home,
            //    //Otherwise it will prompt user to relogin with new antiforgery token generated
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary()
            //                    {
            //                        { "client", context.RouteData.Values[ "client" ] },
            //                        { "controller", "Account" },
            //                        { "action", "Login" },
            //                        { "type", "12" }
            //                    });
            //}
            //else
            //{
            //    base.OnException(context);
            //}
        }
    }
}
