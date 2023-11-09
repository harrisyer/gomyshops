using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Helpers;
//using System.Web.Mvc;
//using System.Web.WebPages;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Antiforgery;

namespace GoMyShops.BAL.MVCFilters
{
    [AttributeUsage(AttributeTargets.All)]
    public class ValidateAntiForgeryTokenOnHeader : AuthorizeAttribute
    {
        public  void OnAuthorization(AuthorizationFilterContext filterContext, IAntiforgery antiforgery)
        {
            var request = filterContext.HttpContext.Request;
            if (request.Method == WebRequestMethods.Http.Post)
            {
                bool isAjaxCall = request.Headers["x-requested-with"] == "XMLHttpRequest";
                if (isAjaxCall)
                {
                    var antiForgeryCookie = request.Cookies["f"];

                    //TODO Harris Core-temp-off
                    //var cookieValue = antiForgeryCookie != null
                    //    ? antiForgeryCookie.Value
                    //    : null;

                    //antiforgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                }
                ////else
                ////{
                ////    new ValidateAntiForgeryTokenAttribute()
                ////        .OnAuthorization(filterContext);
                ////}
            }
        }
    }//end class
}//end namespace
