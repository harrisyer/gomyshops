using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using GoMyShops.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GoMyShops.BAL.MVCFilters
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public  void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            //base.OnAuthorization(filterContext);

            if (filterContext == null)
            {
                return;
            }

            bool skipAuthorization=false;
            var httpContext = filterContext.HttpContext;

            foreach (var filterDescriptors in filterContext.ActionDescriptor.FilterDescriptors)
            {
                if (filterDescriptors.Filter.GetType() == typeof(AllowAnonymousFilter))
                {
                    skipAuthorization=true;
                }
            }

            //bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            //  || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            //if (!skipAuthorization)
            //{
            //    base.OnAuthorization(filterContext);
            //}

            if (!skipAuthorization)
            {
                if (httpContext.Session.GetString(CommonSetting.SessionId.LoginPhase) != null)
                {
                    if (httpContext.Session.GetString(CommonSetting.SessionId.LoginPhase) == "2")
                    {
                        var descriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor;
                        string actionName = descriptor.ActionName;
                        string controllerName = descriptor.ControllerName;

                        if (actionName != "EditLoginImage" && controllerName != "User" )
                        {
                            //if (actionName != "getData")
                            //{
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                                {
                                    { "client", filterContext.RouteData.Values[ "client" ] },
                                    { "controller", "User" },
                                    { "action", "EditLoginImage" },
                                    { "ReturnUrl", "" }
                                });

                                //RedirectToAction("EditLoginImage", "User");
                            //}//end if
                        }//end if                    
                    }//end if
                }//end if
            }//end if 

            //if (filterContext.Result is HttpUnauthorizedResult)
            //{
            //    string returnUrl = null;
            //    if (filterContext.HttpContext.Request.HttpMethod.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
            //        returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);

            //    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
            //    //{
            //    //    { "client", filterContext.RouteData.Values[ "client" ] },
            //    //    { "controller", "Security" },
            //    //    { "action", "Login" },
            //    //    { "ReturnUrl", returnUrl }
            //    //});
            //}
        }

        protected  void HandleUnauthorizedRequest(AuthorizationFilterContext filterContext)
        {
            bool isAjaxCall = filterContext.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                filterContext.Result = new ContentResult();
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 403;
                ////filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                //filterContext.Result = new RedirectResult("~/Account/Login");
                //filterContext.HttpContext.Response.AddHeader("REQUIRES_AUTH", "1");
            }
            else
            {               
                //base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }//end class
}//end namespace
