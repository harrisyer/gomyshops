using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using GoMyShops.BAL;
using GoMyShops.Models;
using GoMyShops.Commons;
using GoMyShops.Models.Helpers;
using GoMyShops.Models.ViewModels;
using GoMyShops.BAL.MVCFilters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
//using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace GoMyShops.Web.Controllers
{

    [CustomAuthorization]
    public class UserController : BaseController
    {
        #region Definations
        private readonly ILogger<UserController> _logger;
        IUserGroupBAL _userGroupBAL;
        IUsersBAL _userBAL;
        IBranchBAL _branchBAL;
        ICompanyBAL _companyBAL;
        IDistributorBAL _distributorBAL;
        IServicesBAL _servicesBAL;
        ISmsBAL _smsBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        IWebHostEnvironment _hostingEnvironment;
        //ICompositeViewEngine _viewEngine;
        #endregion
        #region Constructor
        public UserController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger,  IUsersBAL userBAL, ICompanyBAL companyBAL, IDistributorBAL distributorBAL, IBranchBAL branchBAL,  IUserGroupBAL userGroupBAL, IServicesBAL servicesBAL, ISmsBAL smsBAL)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
           // _viewEngine = viewEngine;
            _userBAL = userBAL;
            _userGroupBAL = userGroupBAL;
            _companyBAL = companyBAL;
            _distributorBAL = distributorBAL;
            _branchBAL = branchBAL;
           // _locationBAL = locationBAL;
            _servicesBAL = servicesBAL;
            _smsBAL = smsBAL;
            _logger = logger;
        }
        #endregion
        #region Public Functions
        [Permissions]
        public ActionResult List(UserViewModels model)
        {
            model.UserGroupDDL = _userGroupBAL.GetActiveUserGroupList();
            model.StatusDDL = _servicesBAL.GetStatusList();
            return View(model);
        }
        public JsonResult GetStateByCountry(string CountryCode)
        {
            return Json(_servicesBAL.GetStateByCountry(CountryCode));
        }
        public JsonResult GetDistributorList(string CompanyCode)
        {
            return Json(_distributorBAL.GetDistributorList(CompanyCode));
        }

        public JsonResult GetBranchList(string DistributorCode)
        {
            return Json(_branchBAL.GetBranchList(DistributorCode));
        }

        //public JsonResult GetLocationList(string BranchCode)
        //{
        //    return Json(_locationBAL.GetLocationListByBranch(BranchCode));
        //}

        public JsonResult getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8)
        {
            var totals = 0;

            var model = new
            {
                rows = _userBAL.getData(offset, limit, search, sort, order, param1, param2, param3, param4, param5, param6, param7, param8, ref totals),
                total = totals,

            };
            return Json(model);

        }

        public ActionResult Create()
        {
            var model = new UserDetailsViewModels(_httpContextAccessor);
            setInitDetailsData(model);
            //model.UserGroupDDL = _userGroupBAL.GetActiveAdminUserGroupList();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserDetailsViewModels model)
        {
            //ModelState.Clear();

            bool isError;// = false;

            if (!ModelState.IsValid)
            {
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            isError =await _userBAL.Create(model, ModelState);

            if (!ModelState.IsValid)
            {
                MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false), false, true);
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            if (isError)
            {
                MessageDanger(CommonSetting.PleaseContactAdmin, false, true);

                if (model.CountryCode.IsNullOrEmptyString())
                {
                    setInitDetailsData(model);
                }
                else
                {
                    setAllDetailsData(model);
                }//end if-else

                return PartialView(model);
            }//end if

            MessageSuccess(string.Format(CommonSetting.SuccessCreateRecordsArgs, model.UserName), true, true);
            ModelState.Clear();
            var model1 = new UserDetailsViewModels(_httpContextAccessor);
            setInitDetailsData(model1);
            return PartialView(model1);

        }

        public async Task<ActionResult> Details(string id, string id2, string id3, string id4)
        {
            var model =await _userBAL.getDetailAsync(id, id2,id3,id4);
            CommonFunctionsBAL.AssignEmptyProperty(model);
            return PartialView(model);
        }

        public async Task<ActionResult> Edit(string id, string id2, string id3, string id4)
        {

            var model =await _userBAL.getDetailAsync(id, id2, id3, id4);
            if (!model.IsNullOrEmpty())
            {
                setAllDetailsData(model);
            }//end if

            if (model.GroupCodeSubAccess )
            {
                model.UserGroupDDL = _userGroupBAL.GetActiveUserGroupList(model.GroupType,model.CustCode);
            }
            else
            {
                model.UserGroupDDL = _userGroupBAL.GetActiveAdminUserGroupList();
            }
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactived(List<UserListViewModels> model)
        {
            bool isError = false;
            try
            {
                if (ModelState.IsValid)
                {
                    isError = _userBAL.Deactived(model);
                    if (isError)
                    {
                        return Json(new { Errors = CommonSetting.PleaseContactAdmin });
                    }//end if
                }//end if

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Json(new { Errors = CommonSetting.PleaseContactAdmin });
            }
            return Json(new { Success = CommonSetting.SuccessModifyRecords });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserDetailsViewModels model)
        {
            bool isError = false;

            if (model.GroupCodeSubAccess)
            {
                model.UserGroupDDL = _userGroupBAL.GetActiveUserGroupList(model.GroupType, model.CustCode);
            }
            else
            {
                model.UserGroupDDL = _userGroupBAL.GetActiveAdminUserGroupList();
            }

            if (!ModelState.IsValid)
            {
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            isError =await _userBAL.Edit(model, ModelState);

            if (!ModelState.IsValid)
            {
                MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false), false, true);
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            if (isError)
            {
                MessageDanger(CommonSetting.PleaseContactAdmin, false, true);
                setAllDetailsData(model);

                return PartialView(model);
            }//end if

            MessageSuccess(string.Format(CommonSetting.SuccessModifyRecordsArgs, model.UserName), true, true);
            setAllDetailsData(model);
            return PartialView(model);
        }

        //[ChildActionOnly]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword( string userName, string partial)
        {
            ResetPasswordViewModel a = new ResetPasswordViewModel();

            a.EmailCode = System.Web.HttpUtility.UrlEncode(await _userBAL.GetResetPasswordCodeAsync(userName, ModelState));

            a.ResetPasswordUserName = userName;

            if (a.ResetPasswordUserName.IsNullOrEmptyString())
            {
                a.ResetPasswordUserName = User.Identity.Name;
            }

            if (partial == "Y")
            {
                //a.Code = System.Web.HttpUtility.UrlDecode(code);
                return a.EmailCode == null ? View("Error") : View(a);
            }

            return a.EmailCode == null ? View("Error") : View(a);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryTokenOnHeader]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            //temp skip for Edit save
            if (model.Password.IsNullOrEmptyString() || model.ConfirmPassword.IsNullOrEmptyString())
            {
                //model.Password = "123";
                //model.ConfirmPassword = "123";
                //return PartialView("ResetPassword");
                return ViewComponent("ResetPassword", model);
            }//end if

            model.EmailCode = System.Web.HttpUtility.UrlDecode(model.EmailCode);
            if (!ModelState.IsValid)
            {
               // return PartialView(model);
                return ViewComponent("ResetPassword", model);
            }

            var lrm = new LoginRedirectModel(_httpContextAccessor);

            model.Email = model.ResetPasswordUserName;
            lrm = await _userBAL.ResetPasswordAdmin(model, ModelState);
            _isError = lrm.isError;

            model.EmailCode = System.Web.HttpUtility.HtmlEncode(model.EmailCode);

            if (lrm.isSuccess)
            {
               // _smsBAL.SendUserProfile(model.Email, CommonSetting.SmsMessage.TypePasswordChange);
                MessageSuccess(string.Format(CommonSetting.SuccessModifyRecordsArgs, model.Email),true,true);
                return ViewComponent("ResetPassword", model);
            }//end if

            if (lrm.IsRedirect)
            {
                //return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName });

            }//end if

            if (!ModelState.IsValid)
            {
                MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false,"The password you entered is invalid"), false, true);
                // return PartialView(model);
                return ViewComponent("ResetPassword", model);
            }//end if

            if (_isError)
            {
                Danger("User password reset fail.", false, true);
                return ViewComponent("ResetPassword", model);
               // return PartialView(model);
            }//end if

            MessageSuccess(CommonSetting.SuccessResetPassword, true, true);

            return ViewComponent("ResetPassword", model);
            //return PartialView(model);
        }

        public JsonResult PopulateUserAccessLevelList(string UserName)
        {
            List<TreeViewItemModel> tvtmList = new List<TreeViewItemModel>();
            try
            {
                _userBAL.PopulateUserAccessLevelList(tvtmList, UserName);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
            }
            finally { }

            return Json(tvtmList);

        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnHeader]
        public async Task<ActionResult> PopulateUserAccessLevelList(TreeViewSelectItemsModel tvsim)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {Errors = ModelStateHelper.Errorsstr(ModelState, true) });
            }//end if

            if (tvsim.nodes.IsNullOrEmpty())
            {
                return Json(new {Errors = string.Format(CommonSetting.TrySelectOneRecordsUserAccessLAyerArgs, tvsim.userName) });
            }//end if

            bool isError = false;
            isError = await _userBAL.SetUserAccessLevelAsync(tvsim, ModelState);

            if (!ModelState.IsValid)
            {
                return Json(new {Errors = ModelStateHelper.Errorsstr(ModelState, true) });
            }//end if

            if (isError)
            {
                return Json(new {Errors = CommonSetting.PleaseContactAdmin });
                //return PartialView(model);
            }//end if

            return Json(new { Success = string.Format(CommonSetting.SuccessModifyRecordsUserAccessLAyerArgs, tvsim.userName) });
        }

        //[AllowAnonymous]
        [HttpGet]
        public ActionResult EditLoginImage(string UserName,string type)
        {
            LoginImageViewModels model = new LoginImageViewModels();
            model.UserName = UserName;
            model.Type = type;
            //model.Phrase = "||||||";
            return View(model);                                                                                             
        }

        //[AllowAnonymous]
        public ActionResult EditLoginImagePartial(string type)
        {
            LoginImageViewModels model = new LoginImageViewModels();
            setLoginImages(model);
            if (type.IsNullOrEmptyString())
            {
                return PartialView(model);
            }//end if
            _userBAL.getPhraseDetail(model);
            // model.Password = "123456";
            model.Type = type;
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EditLoginImage(LoginImageViewModels model)
        {
            ////// SignInStatus status = new SignInStatus();
            //// bool isError = false;
            //// if (ModelState.IsErrors("Password"))
            //// //    if (!ModelState.IsValid)
            //// {
            ////     setLoginImages(model);
            ////     //return Json(new { Errors = "Invalid data.", OperationContainer = CommonFunctionsBAL.RenderRazorViewToString(ControllerContext, "EditLoginImagePartial", model) });

            ////     return Json(new { Errors = "Invalid data.", OperationContainer = CommonFunctionsBAL.RenderPartialViewToString(_hostingEnvironment, _viewEngine, ControllerContext, "EditLoginImagePartial", model) });


            ////     //return PartialView("EditLoginImagePartial", model);
            //// }//end if

            //// VerifyCodeViewModel vc = new VerifyCodeViewModel();
            //// vc.Code = model.Code;
            //// vc.UserName = CurrentUser.Name;
            //// var isTrue = await _userBAL.VerifyTwoFactorWithoutChangedAsync(vc);
            //// if (ModelState.IsErrors("Password"))
            //// {
            ////     setLoginImages(model);
            ////     return Json(new { Errors = "Invalid data.", OperationContainer = CommonFunctionsBAL.RenderPartialViewToString(_hostingEnvironment,_viewEngine,ControllerContext, "EditLoginImagePartial", model) });
            //// }

            //// if (isTrue)
            //// {
            ////     isError = _userBAL.EditPhrase(model, ModelState);

            ////     if (isError)
            ////     {
            ////         return Json(new { Errors = CommonSetting.PleaseContactAdmin });
            ////     }//end if

            ////     //MessageSuccess(string.Format(CommonSetting.SuccessModifyRecordsArgs, CurrentUser.Name), true, true);
            ////     //return PartialView("EditLoginImagePartial", model);

            ////     //if type=1, not modal dialog
            ////     if (model.Type!="1")
            ////     {
            ////         return Json(new { Success = CommonSetting.SuccessModifyRecords, Type="1"});

            ////     }//end if

            ////     setLoginImages(model);
            ////     return Json(new { Success = CommonSetting.SuccessModifyRecords, OperationContainer = CommonFunctionsBAL.RenderPartialViewToString(_hostingEnvironment,_viewEngine,ControllerContext, "EditLoginImagePartial", model) });

            //// }
            //// else
            //// {
            ////     return Json(new { Errors = "Invalid code." });
            ////     //ModelState.AddModelError("Code", "Invalid code.");
            //// }//end if-else
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetLoginImageTac()
        {
            bool isSuccess = false;
            try {

                string code = await _userBAL.GenerateTwoFactorTokenAsync(CommonSetting.TwoFactorProvider, CurrentUser.Name);
                isSuccess = _smsBAL.SendUserTac(CurrentUser.Name, code);
                if (isSuccess)
                {
                    return Json(new { Success = "TAC Sent." });
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
                return Json(new { Success = "TAC Sent with error." });
            }
            finally { }
            return Json(new { Success = "TAC Sent with error." });
        }

        #endregion
        #region Private Functions
        private void setAllDetailsData(UserDetailsViewModels model)
        {
            model.CompanyDDL = _companyBAL.GetActiveCompanyList();
            model.DistributorDDL = _distributorBAL.GetDistributorList(model.CompCode);
            model.BranchDDL = _branchBAL.GetBranchList();
            //model.LocationDDL = _locationBAL.GetLocationListByBranch(model.BranchCode);
            model.StateDDL = _servicesBAL.GetStateByCountry(model.CountryCode);
            model.CountryDDL = _servicesBAL.GetCountryList();
            model.UserTypeDDL = _servicesBAL.GetiParamWithoutCodeList(CommonSetting.ParamCodes.Designation);//_userGroupBAL.GetGroupType();
            //model.UserGroupDDL = _userGroupBAL.GetActiveUserGroupList();
            //model.UserGroupDDL = _userGroupBAL.GetActiveAdminUserGroupList();
        }
        private void setInitDetailsData(UserDetailsViewModels model)
        {
            model.CompanyDDL = _companyBAL.GetActiveCompanyList();
            model.DistributorDDL = new List<SelectListItem>();
            model.BranchDDL = _branchBAL.GetBranchList();
            //model.LocationDDL = new List<SelectListItem>();
            model.StateDDL = new List<SelectListItem>();
            model.CountryDDL = _servicesBAL.GetCountryList();
            model.UserTypeDDL = _servicesBAL.GetiParamWithoutCodeList(CommonSetting.ParamCodes.Designation);  //_userGroupBAL.GetGroupType();
            //model.UserGroupDDL = _userGroupBAL.GetActiveUserGroupList();
            model.UserGroupDDL = _userGroupBAL.GetActiveAdminUserGroupList();

        }
        private void setLoginImages(LoginImageViewModels model)
        {
            FileInfo[] Images;
            //List<LoginImageURLViewModels> LoginImages = new List<LoginImageURLViewModels>();
            int count = 0;
            try
            {
                //model.Code = "123456";

                model.LoginImages1 = new List<LoginImageURLViewModels>();
                model.LoginImages2 = new List<LoginImageURLViewModels>();
                model.LoginImages3 = new List<LoginImageURLViewModels>();

                var wwwroot = _hostingEnvironment.WebRootPath;

                DirectoryInfo directoryInfo = new DirectoryInfo(wwwroot + CommonSetting.LoginImagePath);

                //DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath("~" + CommonSetting.LoginImagePath));
                Images = directoryInfo.GetFiles();
                foreach (FileInfo fi in Images)
                {
                    count = count + 1;
                    LoginImageURLViewModels LI = new LoginImageURLViewModels();
                    LI.Src = String.Format(@"{0}{1}/{2}{3}", "~", CommonSetting.LoginImagePath, fi.Name, "");
                  
                    LI.Code = Path.GetFileNameWithoutExtension(fi.Name);
                    //LoginImages.Add(LI);

                    if (count <= 6)
                    {
                        model.LoginImages1.Add(LI);
                    }
                    else if (count > 6 && count <= 12)
                    {
                        model.LoginImages2.Add(LI);
                    }
                    else
                    {
                        model.LoginImages3.Add(LI);
                    }//end if-else 
                }//end foreach
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
            }
            finally { }
        }
        #endregion
    }//end class
}//end namespace