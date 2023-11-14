using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
//using System.Web.WebPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace GoMyShops.Models.Helpers
{
    public static class ViewPageExtensions
    {
        private const string SCRIPTBLOCK_BUILDER = "ScriptBlockBuilder";

        //TODO Harris Core-temp-off

        //public static HtmlString ScriptBlock(
        //    this WebViewPage webPage,
        //    Func<dynamic, HelperResult> template)
        //{
        //    if (!webPage.IsAjax)
        //    {
        //        var scriptBuilder = webPage.Context.Items[SCRIPTBLOCK_BUILDER]
        //                            as StringBuilder ?? new StringBuilder();

        //        scriptBuilder.Append(template(null).ToHtmlString());

        //        webPage.Context.Items[SCRIPTBLOCK_BUILDER] = scriptBuilder;

        //        return new HtmlString(string.Empty);
        //    }
        //    return new HtmlString(template(null).ToHtmlString());
        //}

        //public static HtmlString WriteScriptBlocks(this WebViewPage webPage)
        //{
        //    var scriptBuilder = webPage.Context.Items[SCRIPTBLOCK_BUILDER]
        //                        as StringBuilder ?? new StringBuilder();

        //    return new HtmlString(scriptBuilder.ToString());
        //}
    }//end class

    public static class HtmlHelperViewExtensions
    {
        public static async Task<IHtmlContent> RenderAction(this IHtmlHelper helper, string action, object parameters = null)
        {
            var controller = (string)helper.ViewContext.RouteData.Values["controller"];
            return await RenderAction(helper, action, controller, parameters);
        }

        public static async Task<IHtmlContent> RenderAction(this IHtmlHelper helper, string action, string controller, object parameters = null)
        {
            var area = (string)helper.ViewContext.RouteData.Values["area"];
            return await RenderAction(helper, action, controller, area, parameters);
        }

        public static async Task<IHtmlContent> RenderAction(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(controller));
            if (controller == null)
                throw new ArgumentNullException(nameof(action));

            var htmlContent = await RenderActionAsync(helper, action, controller, area, parameters);
            return htmlContent;
        }

        private static async Task<IHtmlContent> RenderActionAsync(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            // fetching required services for invocation
            var currentHttpContext = helper.ViewContext.HttpContext;
            var httpContextFactory = GetServiceOrFail<IHttpContextFactory>(currentHttpContext);
            var actionInvokerFactory = GetServiceOrFail<IActionInvokerFactory>(currentHttpContext);
            var actionSelector = GetServiceOrFail<IActionDescriptorCollectionProvider>(currentHttpContext);

            // creating new action invocation context
            var routeData = new RouteData();
            var routeParams = new RouteValueDictionary(parameters ?? new { });
            var routeValues = new RouteValueDictionary(new { area, controller, action });
            var newHttpContext = httpContextFactory.Create(currentHttpContext.Features);

            newHttpContext.Response.Body = new MemoryStream();

            foreach (var router in helper.ViewContext.RouteData.Routers)
                routeData.PushState(router, null, null);

            routeData.PushState(null, routeValues, null);
            routeData.PushState(null, routeParams, null);

            var actionDescriptor = actionSelector.ActionDescriptors.Items.First(i => i.RouteValues["Controller"] == controller && i.RouteValues["Action"] == action);
            var actionContext = new ActionContext(newHttpContext, routeData, actionDescriptor);

            // invoke action and retreive the response body
            var invoker = actionInvokerFactory.CreateInvoker(actionContext);
            string content = null;

            await invoker.InvokeAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    content = task.Exception.Message + " Error";
                }
                else if (task.IsCompleted)
                {
                    newHttpContext.Response.Body.Position = 0;
                    using (var reader = new StreamReader(newHttpContext.Response.Body))
                    {
                        content = reader.ReadToEnd();
                    }

                }
            });

            return new HtmlString(content);
        }

        private static TService GetServiceOrFail<TService>(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var service = httpContext.RequestServices.GetService(typeof(TService));

            if (service == null)
                throw new InvalidOperationException($"Could not locate service: {nameof(TService)}");

            return (TService)service;
        }
    }

}//end namespace
