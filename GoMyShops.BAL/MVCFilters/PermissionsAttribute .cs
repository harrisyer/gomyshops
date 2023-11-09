using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace GoMyShops.BAL.MVCFilters
{
    public class PermissionsAttribute : Attribute
    {
    
    }//end class

    public class PermissionsActionFilter : IActionFilter<PermissionsAttribute>
    {
        IUsersBAL _usersBAL;
        IUserGroupBAL _userGroupBAL;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;

        public PermissionsActionFilter(IUnitOfWorkFactory uowFactory,IUsersBAL usersBAL, IUserGroupBAL userGroupBAL)
        {
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _usersBAL = usersBAL;
            _userGroupBAL = userGroupBAL;
        }

        public void OnActionExecuting(PermissionsAttribute attribute, ActionExecutingContext filterContext)
        {
            string usergroup = "";

            var user = filterContext.HttpContext.User;
            if (user == null)
            {
                //if (filterContext.IsChildAction)
                //{
                //    var result1 = new PartialViewResult
                //    {
                //        ViewName = "_RestrictedAccess"
                //    };

                //    filterContext.Result = result1;
                //}
                //else
                //{
                //send them off to the login page
                //Todo Harris (Test) Modify Core
                var url = new UrlHelper(filterContext);
                var loginUrl = url.Content("~/Account/Login");
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
                //}
            }
            else
            {
                var descriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor;
                string actionName = descriptor.ActionName;
                string controllerName = descriptor.ControllerName;
                //string actionName = filterContext.ActionDescriptor.ActionName;
                //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                //check is Sign Up User
                var signUpRow = _uow.Repository<SignUp>().GetAsQueryable(x =>
                          x.SignUpName == user.Identity.Name
                      ).Count();

                if (signUpRow > 0)
                {
                    usergroup = CommonSetting.GroupCode.OnBoarding;
                }
                else
                {
                    usergroup = _usersBAL.GetUserGroup(user.Identity.Name);
                }//end if-else

                var uga = (from u in _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == usergroup)
                           join m in _uow.Repository<ModuleSU>().GetAsQueryable(x => x.ModuleStatus == 1) on u.ModuleID equals m.ModuleID //into module
                           where m.TargetController == controllerName
                           select new { U = u, M = m }
                         ).ToList();

                var moduleId = _uow.Repository<ModuleSU>().GetAsQueryable(x => x.ModuleStatus == 1)
                                                          .Where(x => x.TargetController == controllerName
                                                                 && x.TargetAction == actionName)
                                                          .Select(x => x.ModuleID).FirstOrDefault();


                if (uga.IsNullOrEmpty())
                {
                    // filterContext.HttpContext.Items["userAccess"] = "You do not have the necessary permission to perform this action.";

                    var result1 = new PartialViewResult
                    {
                        ViewName = "_RestrictedAccess"
                    };

                    filterContext.Result = result1;
                    return;
                }//end if


                int intFound = 0;


                //Harris Add for get moduleID if not match List,Create Edit
                if (moduleId.IsNullOrEmpty() || moduleId == 0)
                {
                    //add for handle all Start for detail
                    if (actionName.Length >= 7)
                    {
                        if (actionName.Substring(0, 7) == "Details")
                        {
                            var SearchDetails = uga.Where(x => x.M.ModuleStatus == 1 &&
                                                  x.M.TargetController == controllerName).FirstOrDefault();
                            if (SearchDetails != null)
                            {
                                intFound = uga.Where(x => x.U.DetailFlag == true).Count();
                            }//end if
                        }//end if
                    }//end if

                    //add for handle all Start for create
                    if (actionName.Length >= 6)
                    {
                        if (actionName.Substring(0, 6) == "Create")
                        {
                            var SearchCreate = uga.Where(x => x.M.ModuleStatus == 1 &&
                                                  x.M.TargetController == controllerName).FirstOrDefault();
                            if (SearchCreate != null)
                            {
                                intFound = uga.Where(x => x.U.CreateFlag == true).Count();
                            }//end if
                        }//end if
                    }//end if
                     //
                     //add for handle all Start for Edit
                    if (actionName.Length >= 4)
                    {
                        if (actionName.Substring(0, 4) == "List")
                        {
                            var SearchList = uga.Where(x => x.M.ModuleStatus == 1 &&
                                                     x.M.TargetController == controllerName).FirstOrDefault();
                            if (SearchList != null)
                            {
                                intFound = uga.Where(x => x.U.MenuFlag == true).Count();
                            }//end if


                        }//end if

                        if (actionName.Substring(0, 4) == "Edit")
                        {
                            var SearchEdit = uga.Where(x => x.M.ModuleStatus == 1 &&
                                                    x.M.TargetController == controllerName).FirstOrDefault();
                            if (SearchEdit != null)
                            {
                                intFound = uga.Where(x => x.U.EditFlag == true).Count();
                            }//end if

                        }//end if
                    }//end if
                     //
                }
                else
                {
                    //Standard ActionsType
                    switch (actionName)
                    {
                        case CommonSetting.ActionsType.List:
                            intFound = uga.Where(x => x.U.MenuFlag == true).Count();
                            break;
                        case CommonSetting.ActionsType.Edit:
                            intFound = uga.Where(x => x.U.EditFlag == true).Count();
                            break;
                        case CommonSetting.ActionsType.Create:
                            intFound = uga.Where(x => x.U.CreateFlag == true).Count();//  && !x.M.TargetAction.Contains("Create")).Count();
                            break;
                        case CommonSetting.ActionsType.Details:
                            intFound = uga.Where(x => x.U.DetailFlag == true).Count();
                            break;
                        case CommonSetting.ActionsType.Deactived:
                            intFound = uga.Where(x => x.U.DeleteFlag == true).Count();
                            break;
                        default:
                            break;
                    }//end switch   

                    //add for handle all Start for Details
                    if (actionName.Length > 7)
                    {
                        if (actionName.Substring(0, 7) == "Details")
                        {
                            var SearchDetails = uga.Where(x => x.M.ModuleStatus == 1 &&
                                               x.M.TargetController == controllerName &&
                                               x.M.ModuleID == moduleId).FirstOrDefault();
                            if (SearchDetails != null)
                            {
                                intFound = uga.Where(x => x.U.DetailFlag == true).Count();
                            }//end if
                            //intFound = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == usergroup)
                            //               .Where(x => x.CreateFlag == true && x.ModuleID == moduleId).Count();
                        }//end if
                    }//end if

                    //add for handle all Start for create
                    if (actionName.Length > 6)
                    {
                        if (actionName.Substring(0, 6) == "Create")
                        {
                            var SearchCreate = uga.Where(x => x.M.ModuleStatus == 1 &&
                                               x.M.TargetController == controllerName &&
                                               x.M.ModuleID == moduleId).FirstOrDefault();
                            if (SearchCreate != null)
                            {
                                intFound = uga.Where(x => x.U.CreateFlag == true).Count();
                            }//end if
                            //intFound = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == usergroup)
                            //               .Where(x => x.CreateFlag == true && x.ModuleID == moduleId).Count();
                        }//end if
                    }//end if
                     //
                     //add for handle all Start for Edit
                    if (actionName.Length > 4)
                    {
                        if (actionName.Substring(0, 4) == "List")
                        {
                            var SearchList = uga.Where(x => x.M.ModuleStatus == 1 &&
                                              x.M.TargetController == controllerName &&
                                              x.M.ModuleID == moduleId).FirstOrDefault();
                            if (SearchList != null)
                            {
                                intFound = uga.Where(x => x.U.MenuFlag == true).Count();
                            }//end if
                            //intFound = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == usergroup)
                            //            .Where(x => x.MenuFlag == true && x.ModuleID == moduleId).Count();

                        }//end if

                        if (actionName.Substring(0, 4) == "Edit")
                        {
                            var SearchEdit = uga.Where(x => x.M.ModuleStatus == 1 &&
                                             x.M.TargetController == controllerName &&
                                             x.M.ModuleID == moduleId).FirstOrDefault();
                            if (SearchEdit != null)
                            {
                                intFound = uga.Where(x => x.U.EditFlag == true).Count();
                            }//end if
                            //intFound = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == usergroup)
                            //            .Where(x => x.EditFlag == true && x.ModuleID == moduleId).Count();                         

                        }//end if
                    }//end if
                     //


                }//end if-else
                //

                if (intFound <= 0)
                {
                    //filterContext.HttpContext.Items["userAccess"] = "You do not have the necessary permission to perform this action.";

                    var result = new PartialViewResult
                    {
                        ViewName = "_RestrictedAccess"
                    };

                    filterContext.Result = result;
                    return;
                }//end if



                //filterContext.Result = new PartialViewResult();// ("~/Account/Login");
                //filterContext.Current.Response.Redirect("_RestrictedAccess", true);
                //filterContext.Result = new RedirectResult("~/_RestrictedAccess");
                //if (!user.HasPermissions(required))
                //{
                //    throw new AuthenticationException("You do not have the necessary permission to perform this action");
                //}
            }
        }


    }//end class
}//end namespace