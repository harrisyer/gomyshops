using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GoMyShops.Models;
using GoMyShops.Commons;

namespace GoMyShops.Controllers
{
    public class AlertsController : GoMyShops.Web.Controllers.BaseController
    {
        // GET: Alerts
        public ActionResult Index()
        {
            //Auto Disappear-Click to close
            Success("Success-Auto Disappear-Click to close", true, true);
            Information("Information-Auto Disappear-Click to close", true, true);
            Warning("Warning-Auto Disappear-Click to close", true, true);
            Danger("Danger-Auto Disappear-Click to close", true, true);

            //Auto Disappear-Can not Close
            Success("Success-Auto Disappear-Can not Close", true,false);
            Information("Information-Auto Disappear-Can not Close", true,false);
            Warning("Warning-Auto Disappear-Can not Close", true,false);
            Danger("Danger-Auto Disappear-Can not Close", true,false);


            //Not Disappear-Click to close
            Success("Success-Not Disappear-Click to close", false,true);
            Information("Information-Not Disappear-Click to close", false, true);
            Warning("/Warning-Not Disappear-Click to close", false, true);
            Danger("Danger-Not Disappear-Click to close", false, true);

            return View();
        }

        public ActionResult showAlert1()
        {
            return PartialView();
        }

        public ActionResult showAlert()
        {
            AlertList? alerts = new AlertList();

            if (TempData.ContainsKey(Alert.TempDataJsonKey))
            {
                var json = TempData[Alert.TempDataJsonKey] as string;
                alerts = JsonConvert.DeserializeObject<AlertList>(json.IsNullThenEmpty());
                //alerts = TempData[Alert.TempDataJsonKey] as AlertList;
               
            }
           
            return PartialView(alerts);
        }


    }//end class
}//end namespace