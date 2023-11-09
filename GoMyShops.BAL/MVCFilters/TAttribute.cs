using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection.Metadata;

namespace GoMyShops.BAL.MVCFilters
{

    public interface IActionFilter<TAttribute> where TAttribute : Attribute
    {
        void OnActionExecuting(TAttribute attribute, ActionExecutingContext context);
    }

    public class ActionFilterDispatcher : IActionFilter
    {
        private readonly Func<Type, IEnumerable> container;

        public ActionFilterDispatcher(Func<Type, IEnumerable> container)
        {
            this.container = container;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var descriptor = context.ActionDescriptor;
            var descriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;
            //string actionName = descriptor.ActionName;
            //string controllerName = descriptor.ControllerName;

            if (descriptor != null)
            {
                var isDefined = descriptor.MethodInfo.GetCustomAttributes(inherit: true)
                    .Any(a => a.GetType().Equals(typeof(CustomAttribute)));

                foreach (var attribute in descriptor.MethodInfo.GetCustomAttributes(inherit: true))
                {
                    Type filterType = typeof(IActionFilter<>).MakeGenericType(attribute.GetType());
                    IEnumerable filters = this.container.Invoke(filterType);

                    foreach (dynamic actionFilter in filters)
                    {
                        actionFilter.OnActionExecuting((dynamic)attribute, context);
                    }
                }
            }
           
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) { }
    }//end class
}//end namespace