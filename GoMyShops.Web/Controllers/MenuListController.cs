using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoMyShops.Commons;
using GoMyShops.Models;
using GoMyShops.BAL;
using System.Threading;
using System.Globalization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
namespace GoMyShops.Controllers
{
    public class MenuListController : Web.Controllers.BaseController
    {
        private readonly IModulesBAL _modulesBAL;
       
        public MenuListController(IModulesBAL modulesBAL)
        {
            _modulesBAL = modulesBAL;           
        }



        //
        // GET: /MenuList/
        //[OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            IList<IAdminMenuItem> a;

            //var InitLoadMenu = TempData[CommonSetting.TempDataKeys.InitLoadMenu];

            if (HttpContext.Session.GetString("UICulture") == null)
            {
                HttpContext.Session.SetString("UICulture", Thread.CurrentThread.CurrentUICulture.ThreeLetterWindowsLanguageName);
            }//end if

            //if (System.Web.HttpContext.Current.Session["UICulture"]==null)
            //{
            //    System.Web.HttpContext.Current.Session["UICulture"]=Thread.CurrentThread.CurrentUICulture.ThreeLetterWindowsLanguageName;
            //}//end if

            //// if (System.Web.HttpContext.Current.Cache["MainMenu"] == null)
            //if (System.Web.HttpContext.Current.Session["MainMenu"] == null)
            //{

            if (User.Identity.IsAuthenticated)
            {
               // a = _modulesBAL.SelectModules(false);
            }
            else
            {
               // a = _modulesBAL.SelectModules(true);
            }

           
                //System.Web.HttpContext.Current.Session["MainMenu"] = a;
            /// System.Web.HttpContext.Current.Cache.Insert("MainMenu", a);
            //}
            //else
            //{
            //    if (System.Web.HttpContext.Current.Session["UICulture"].ToString().Trim().ToUpper() != Thread.CurrentThread.CurrentUICulture.ThreeLetterWindowsLanguageName.Trim().ToUpper())
            //    {
            //        a = OMG.UDynamics.BAL.Modules.SelectModules();
            //    }
            //    else
            //    {
            //        a = (IList<MODELS.ViewModels.IAdminMenuItem>)System.Web.HttpContext.Current.Session["MainMenu"];
            //    }//end if-else

            //}//end if-else

            return PartialView(null);
        }

    }
}
