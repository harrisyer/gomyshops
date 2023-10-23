using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoMyShops.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoMyShops.Web.Controllers
{
    public class BaseController : Controller
    {
        public AppUser _currentUser
        {
            get; set;

        }

        public BaseController()
        {


        }

        public AppUser CurrentUser
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
            //_currentUser = new AppUser(User as ClaimsPrincipal);
            //if (_currentUser != null)
            //{                  
            //}
        }
    }//end class
}//end namespace