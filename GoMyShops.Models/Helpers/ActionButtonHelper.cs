using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models.Helpers
{
    public class ActionButtonHelper
    {
        public String Action { get; private set; }
        public String Controller { get; private set; }
        public String Value { get; private set; }
        public String Name { get; private set; }
        public Object RouteValues { get; private set; }

        private ActionButtonHelper(string action, string controller,
            object routeVal, string value, string name)
        {
            Action = action;
            Controller = controller;
            RouteValues = routeVal;
            Value = value;
            Name = name;
        }

        public static ActionButtonHelper Create(string action, string controller,
            object routeVal, string value, string name)
        {
            return new ActionButtonHelper(action, controller, routeVal, value, name);
        }
    }//end class
}//end namespace
