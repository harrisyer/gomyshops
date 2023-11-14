using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoMyShops.Commons;
using GoMyShops.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoMyShops.Web.Controllers
{
    public class BaseController : Controller
    {
        public bool _isError { get; set; }
        public AppUser _currentUser
        {
            get; set;

        }

        public BaseController()
        {


        }

        public AppUser? CurrentUser
        {
            get
            {
                if (User == null)
                {
                    return null;
                }
                if (_currentUser == null)
                {
                    _currentUser = new AppUser(User as ClaimsPrincipal);
                }
                return _currentUser;
            }
            //get
            //{
            //    return new AppUser(this.User as ClaimsPrincipal);
            //}
        }


        [Microsoft.AspNetCore.Mvc.NonAction]
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            _currentUser = new AppUser(User as ClaimsPrincipal);
            if (_currentUser != null)
            {
                TempData[CommonSetting.TempDataKeys.UserType] = _currentUser.UserType;

                var descriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor;
                string actionName = descriptor.ActionName;
                string controllerName = descriptor.ControllerName;
                //var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
                //var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;

                if (_currentUser.MultiLogin)
                {
                    if (actionName != "Login" && controllerName != "Account")
                    {
                        //filterContext.Result = new RedirectToAction("~/Account/Login");
                        //HttpContext.Authentication..SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                        //                  DefaultAuthenticationTypes.ExternalCookie,
                        //                  DefaultAuthenticationTypes.TwoFactorCookie);
                        // Clear the existing external cookie to ensure a clean login process

                        //Todo Harris (Test) Modify Core
                        HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

                        filterContext.Result = RedirectToAction("Login", "Account", new { userName = _currentUser.Name, type = "5" });

                    }
                    return;
                }//end if


            }
        }

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Success(string message, bool autoDisappear, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, autoDisappear, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Information(string message, bool autoDisappear, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, autoDisappear, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Warning(string message, bool autoDisappear, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, autoDisappear, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        public void Danger(string message, bool autoDisappear, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, autoDisappear, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                AutoDisappear = false
            });

            TempDataExtensions.Set(TempData, Alert.TempDataKey, alerts);
            //TempData[Alert.TempDataKey] = alerts;

        }

        private void AddAlert(string alertStyle, bool autoDisappear, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                AutoDisappear = autoDisappear
            });

            TempDataExtensions.Set(TempData, Alert.TempDataKey, alerts);
            //TempData[Alert.TempDataKey] = alerts;

        }

        public AlertList MessageInformation(string message, bool autoDisappear, bool dismissable = false)
        {
            return MessageAddAlert(AlertStyles.Information, autoDisappear, message, dismissable);
        }

        public AlertList MessageWarning(string message, bool autoDisappear, bool dismissable = false)
        {
            return MessageAddAlert(AlertStyles.Warning, autoDisappear, message, dismissable);
        }

        public AlertList MessageDanger(string message, bool autoDisappear, bool dismissable = false)
        {
            return MessageAddAlert(AlertStyles.Danger, autoDisappear, message, dismissable);
        }

        public AlertList MessageSuccess(string message, bool autoDisappear, bool dismissable = false)
        {
            return MessageAddAlert(AlertStyles.Success, autoDisappear, message, dismissable);
        }

        private AlertList MessageAddAlert(string alertStyle, bool autoDisappear, string message, bool dismissable)
        {
            //var alerts = TempData.ContainsKey(Alert.TempDataKey)
            //    ? (List<Alert>)TempData[Alert.TempDataKey]
            //    : new List<Alert>();
            var alerts = new AlertList();
            if (!String.IsNullOrEmpty(message) && !String.IsNullOrWhiteSpace(message))
            {
                alerts.alerts.Add(new Alert
                {
                    AlertStyle = alertStyle,
                    Message = message,
                    Dismissable = dismissable,
                    AutoDisappear = autoDisappear
                });

                TempDataExtensions.Set(TempData, Alert.TempDataJsonKey, alerts);
                //[Alert.TempDataJsonKey] = alerts;
            }//end if

            return alerts;
        }
    }//end class
}//end namespace