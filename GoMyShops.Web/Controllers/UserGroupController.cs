using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using GoMyShops.BAL;
using GoMyShops.Commons;
using GoMyShops.Models;
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

namespace GoMyShops.Web.Controllers
{
    //[Authorize]
    [CustomAuthorization]
    public class UserGroupController : BaseController
    {
        #region Definations
        private readonly ILogger<UserGroupController> _logger;
        IUserGroupBAL _userGroupBAL;
        //IDistributorBAL _distributorBAL;
        ICompanyBAL _companyBAL;
        IServicesBAL _servicesBAL;
        ILoginBAL _loginBAL;
        IAppCtrlUserProfileBAL _appCtrlUserProfileBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public UserGroupController(IHttpContextAccessor httpContextAccessor,IUserGroupBAL userGroupBAL, IAppCtrlUserProfileBAL appCtrlUserProfileBAL, ICompanyBAL companyBAL, IServicesBAL servicesBAL, ILoginBAL loginBAL, ILogger<UserGroupController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _userGroupBAL = userGroupBAL;
            _companyBAL = companyBAL;
            _servicesBAL = servicesBAL;
            _loginBAL = loginBAL;
            _appCtrlUserProfileBAL = appCtrlUserProfileBAL;
            _logger = logger;
        }
        #endregion
        #region Public Functions
        [Permissions]
        public ActionResult List(UserGroupViewModels model)
        {
            model.GroupTypeDDL = _userGroupBAL.GetGroupType();
            model.StatusDDL = _servicesBAL.GetStatusList();
            return View(model);
        }

     [HttpGet]
        public JsonResult getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8)
        {
            var totals = 0;

            var model = new
            {
                rows = _userGroupBAL.getData(offset, limit, search, sort, order, param1, param2, param3, param4, param5, param6, param7, param8, ref totals),
                total = totals,

            };
            //return Json(model);
            return new JsonResult(model);

        }

        [Permissions]
        public ActionResult Details(string id, string id2, string id3)
        {
            var model = _userGroupBAL.getDetail(id, id2);
            CommonFunctionsBAL.AssignEmptyProperty(model);
            return PartialView(model);
        }

        [Permissions]
        public ActionResult Create()
        {
            var model = new UserGroupDetailsViewModels(_httpContextAccessor);
            //if (model.CurrentUser.UserType ==CommonSetting.UserType.Customer)
            //{

            //}//end if
            setInitDetailsData(model);
            return PartialView(model);
        }

        [Permissions]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserGroupDetailsViewModels model)
        {
            if (!ModelState.IsValid)
            {
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            //ModelState.Clear();

            bool isError = false;        

            isError = _userGroupBAL.Create(model, ModelState);

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

            MessageSuccess(string.Format(CommonSetting.SuccessCreateRecordsArgs, model.GroupCode), true, true);
            ModelState.Clear();
            var model1 = new UserGroupDetailsViewModels(_httpContextAccessor);
            setInitDetailsData(model1);
            return PartialView(model1);

        }

        [Permissions]
        public ActionResult Edit(string id, string id2, string id3)
        {
            //return PartialView("_RestrictedAccess");
            //var model = new UserGroupDetailsViewModels();
           // model.AppCtrDetailList = _appCtrlUserProfileBAL.GetAppCtrlSUList();
            var model = _userGroupBAL.getDetail(id, id2);
            if (!model.IsNullOrEmpty())
            {
                //setAllDetailsData(model);
            }//end if

            //if (HttpContext.Items["userAccess"] != null)
            //{
            //    if (HttpContext.Items["userAccess"].ToString().Length>0 )
            //    {
            //        MessageWarning(HttpContext.Items["userAccess"].ToString(), true, true);
            //    }
            //}

            return PartialView(model);
        }

        [Permissions]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserGroupDetailsViewModels model)
        {
            if (!ModelState.IsValid)
            {
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            bool isError = false;
            isError = _userGroupBAL.Edit(model, ModelState);

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

            MessageSuccess(string.Format(CommonSetting.SuccessModifyRecordsArgs, model.GroupCode), true, true);

            setAllDetailsData(model);

            return PartialView(model);
        }

        public JsonResult PopulateUserGroupAccessList(string UserGroup)
        {
            List<TreeViewItemModel> tvtmList = new List<TreeViewItemModel>();
            try
            {
                _userGroupBAL.PopulateUserGroupAccessList(tvtmList, UserGroup);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
            }
            finally { }

            return Json(tvtmList);

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PopulateUserGroupAccessList(string token,string tvsim1)
        {            
            if (!ModelState.IsValid)
            {
                return Json(new { Errors = ModelStateHelper.Errorsstr(ModelState, true) });
            }//end if

           // List<TreeViewItemModel> tvsim = new List<TreeViewItemModel>();
           var  tvsim = JsonConvert.DeserializeObject <TreeViewSelectItemsModel> (tvsim1);
            tvsim = tvsim.IsNullThenNew(_httpContextAccessor);

            if (tvsim.nodes.IsNullOrEmpty())
            {
                return Json(new { Errors = string.Format(CommonSetting.TrySelectOneRecordsUserAccessLAyerArgs, tvsim.userName) });
            }//end if

            bool isError = false;
            isError = await _userGroupBAL.SetUserGroupAccessAsync(tvsim, ModelState);

            if (!ModelState.IsValid)
            {
                return Json(new { Errors = ModelStateHelper.Errorsstr(ModelState, true) });
            }//end if

            if (isError)
            {
                return Json(new { Errors = CommonSetting.PleaseContactAdmin });
                //return PartialView(model);
            }//end if

            //return Json(new { Success = string.Format(CommonSetting.SuccessModifyRecordsUserAccessLAyerArgs, tvsim.userName) }, JsonRequestBehavior.AllowGet);
            return Json(new { Success = string.Format(CommonSetting.SuccessModifyRecordsUserAccessLAyerArgs, ""), Errors = "" });

        }

        #endregion
        #region Private Functions
        private void setAllDetailsData(UserGroupDetailsViewModels model)
        {
            //model.CompanyDDL = _companyBAL.GetActiveCompanyList();
            //model.GroupTypeDDL = _userGroupBAL.GetGroupType();
            //model.SecurityNameDDL = _loginBAL.GetSecuritySetupList();
            model.AppCtrDetailList = _appCtrlUserProfileBAL.GetAppCtrlSUList();
        }

        private void setInitDetailsData(UserGroupDetailsViewModels model)
        {
            //model.StateDDL = new List<SelectListItem>();
            //model.DistributorDDL = new List<SelectListItem>();
            //model.CompanyDDL = _companyBAL.GetActiveCompanyList();
            //model.GroupTypeDDL = _userGroupBAL.GetGroupType();
            //model.SecurityNameDDL = _loginBAL.GetSecuritySetupList();
            model.AppCtrDetailList = _appCtrlUserProfileBAL.GetAppCtrlSUList();
         
        }
        #endregion
    }//end class
}//end namespace