using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.BAL.MVCFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PreventDuplicateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
          
            if (httpContext.Session.GetString("__RequestVerificationToken") == null)
            {
                return;
            }                

            var currentToken = httpContext.Session.GetString("__RequestVerificationToken");

            if (httpContext.Session.GetString("LastProcessedToken") == null)
            {
                httpContext.Session.SetString("LastProcessedToken", currentToken);
                return;
            }

            //Todo Harris (Test) Modify Core
            lock (httpContext.Session.GetString("LastProcessedToken"))
            {
                var lastToken = httpContext.Session.GetString("LastProcessedToken");

                if (lastToken == currentToken)
                {
                    filterContext.ModelState.AddModelError("", "Accidentally double click occur.");
                    return;
                }

                httpContext.Session.SetString("LastProcessedToken", currentToken);
            }
        }
    }//end class
}//end namespace
