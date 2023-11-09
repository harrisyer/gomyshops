using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoMyShops.Models
{
    public class AlertList
    {
        public AlertList()
        {
            alerts = new List<Alert>();
        }
        
        public List<Alert> alerts { get; set; }
    }

    public class Alert
    {
        public const string TempDataKey = "TempDataAlerts";
        public const string TempDataJsonKey = "TempDataJsonAlerts";
        //public const string TempDataKeyAutoDisappear = "AutoDisappear";
        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
        public bool AutoDisappear { get; set; }
    }//end class

    public static class AlertStyles
    {
        public const string Success = "success";
        public const string Information = "info";
        public const string Warning = "warning";
        public const string Danger = "danger";
    }//end class
}//end namespace