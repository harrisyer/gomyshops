using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Https.Internal;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.AspNetCore.Http.Extensions;
namespace GoMyShops.BAL.MVCFilters
{
    /// <summary>
    /// Use to prevent direct type partialview URL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string referer = filterContext.HttpContext.Request.Headers["Referer"].ToString();
            //var referer = ((FrameRequestHeaders)filterContext.HttpContext.Request.Headers).HeaderReferer.FirstOrDefault();

            if (referer == "" )
            {
                //TODO Harris Core-temp-off
                //if (!filterContext.IsChildAction)
                //{
                //    filterContext.Result = new RedirectToRouteResult(new
                //                   RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
                //}
            }


            //if (filterContext.HttpContext.Request.UrlReferrer == null ||
            //       filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            //{
            //    var result1 = new PartialViewResult
            //    {
            //        ViewName = "_RestrictedAccess"
            //    };

            //    filterContext.Result = result1;
            //    return;
            //}
        }
    }
}
