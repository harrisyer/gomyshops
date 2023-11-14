using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models
{

    public interface IExceptions
    {
        bool isError { get; set; }
    }

    public abstract class BaseBAL
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseBAL()
        {
            //_httpContextAccessor = httpContextAccessor;
        }


        public AppUser? CurrentUser
        {
            get; set;

        }

        //public AppUser? CurrentUser
        //{
        //    get
        //    {
              
        //        //if (_httpContextAccessor!=null)
        //        //{
        //        //    if (_httpContextAccessor.HttpContext != null)
        //        //    {
        //        //        if (_httpContextAccessor.HttpContext.User == null)
        //        //        {
        //        //            return null;
        //        //        }
        //        //        if (_currentUser == null)
        //        //        {
        //        //            _currentUser = new AppUser(_httpContextAccessor.HttpContext.User as ClaimsPrincipal);
        //        //        }
        //        //        return _currentUser;
        //        //    }//end if

        //        //    return null;
        //        //}//end if
               
        //        //return null;
        //    }
        //    set { }
        //}

        

        public bool isError { get; set; }
        public bool isTrue { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UsersType
        {
            get
            {
                if (CurrentUser != null)
                {
                    return CurrentUser.UserType;
                }//end if
                    return string.Empty;
            }
            set
            {
         
            }

        }
        public string mCustomerCode { get; set; } = string.Empty;       
        public string returnString { get; set; } = string.Empty;
    }//end class

    public abstract class ListBAL
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ListBAL()
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        public AppUser ListCurrentUser
        {
            get; set;

        }

        //public AppUser ListCurrentUser
        //{
        //    get
        //    {
        //        if (_httpContextAccessor != null)
        //        {
        //            if (_httpContextAccessor.HttpContext != null)
        //            {
        //                if (_httpContextAccessor.HttpContext.User == null)
        //                {
        //                    return null;
        //                }
        //                if (_ListCurrentUser == null)
        //                {
        //                    _ListCurrentUser = new AppUser(_httpContextAccessor.HttpContext.User as ClaimsPrincipal);
        //                }
        //                return _ListCurrentUser;
        //            }//end if

        //            return null;
        //        }//end if

        //        return null;
        //    }
        //}

        public string ListUsersType
        {
            get
            {
                if (ListCurrentUser != null)
                {
                    return ListCurrentUser.UserType;
                }//end if
                return "";
            }
            set
            {

            }

        }
    }//end class

}//end namespace
