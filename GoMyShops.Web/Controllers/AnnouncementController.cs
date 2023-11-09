using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using GoMyShops.BAL;
using GoMyShops.Commons;

using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.BAL.MVCFilters;
using GoMyShops.Models.Helpers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoMyShops.Controllers
{
    [CustomAuthorization]
    public class AnnouncementController : GoMyShops.Web.Controllers.BaseController
    {
        #region Definations
        private readonly ILogger<AnnouncementController> _logger; 
        IServicesBAL _servicesBAL;
        IAnnouncementBAL _announcementBAL;
        public readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public AnnouncementController(IHttpContextAccessor httpContextAccessor, ILogger<AnnouncementController> logger, IAnnouncementBAL announcementBAL, IServicesBAL servicesBAL)
        {
            _announcementBAL = announcementBAL;
            _servicesBAL = servicesBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions
        [Permissions]
        public ActionResult List(AnnouncementViewModels model)
        {
            model.AnnouncementTypeDDL = _servicesBAL.GetAnnouncementTypeList();
            model.DisplayFrequencyDDL = _servicesBAL.GetAnnouncementFrequencyList();
            model.TargetAudienceDDL = _servicesBAL.GetTargetAudienceList();
            model.StatusDDL = _servicesBAL.GetStatusList();
            return View(model);
        }

        public JsonResult getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8)
        {
            var totals = 0;

            var model = new
            {
                rows = _announcementBAL.getData(offset, limit, search, sort, order, param1, param2, param3, param4, param5, param6, param7, param8, ref totals),
                total = totals,
                totalNotFiltered =  totals

            };
            string output = JsonConvert.SerializeObject(model);
            return Json(model);

        }

        public async Task<ActionResult> PopulatePriorityList()
        {
            return PartialView();
        }

        //[AllowAnonymous]
        public async Task<ActionResult> MenuAnnouncement()
        {
            AnnouncementDisplayViewModels model = new AnnouncementDisplayViewModels();
            try
            {
                model =await _announcementBAL.GetAnnouncementLocalListAsync();
            }
            catch (Exception ex)
            {
               _logger.LogDebug ("Error", ex);
            }
            finally { }
            return View(model);
            //return PartialView(model);
        }

        public async Task<ActionResult> PopulateAnnouncementList(string AnnountcementId)
        {
            AnnouncementDisplayViewModels model = new AnnouncementDisplayViewModels();
            try
            {
                model =await _announcementBAL.GetAnnouncementListwithIDAsync(AnnountcementId);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
            }
            finally { }

            return PartialView(model);
        }

        [HttpPost]
        //[ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        [ValidateAntiForgeryTokenOnHeader]
        public ActionResult PopulatePriorityList(List<string> announcementIndexList)
        {
            bool isError = false;
            try
            {
                isError=_announcementBAL.EditPriority(announcementIndexList, ModelState);
                if (isError)
                {
                    return Json(new { Errors = CommonSetting.PleaseContactAdmin });
                }//end if

                return Json(new { Success = CommonSetting.SuccessModifyRecords });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);              
                return Json(new { Errors = CommonSetting.PleaseContactAdmin });
            }
        }

        public JsonResult GetPriorityList(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8)
        {
            var totals = 0;
            var rows = _announcementBAL.getPriorityData(offset, 100, search, sort, order, param1, param2, param3, param4, param5, param6, param7, param8, ref totals);
            //rows = rows.OrderBy(x => x.Priority).ToList();
            var model = new
            {
                rows = rows,
                total = totals,

            };

            return Json(model.rows);
        }

        [Permissions]
        public ActionResult Create()
        {
            var model = new AnnouncementDetailsViewModels(_httpContextAccessor);
            setInitDetailsData(model);
            return PartialView(model);
        }

        [Permissions]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnnouncementDetailsViewModels model)
        {

            bool isError = false;

            if (!ModelState.IsValid)
            {
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            isError = _announcementBAL.Create(model, ModelState);

            if (!ModelState.IsValid)
            {
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            if (isError)
            {
                MessageDanger(CommonSetting.PleaseContactAdmin, true, true);
                setAllDetailsData(model);
                return PartialView(model);
            }//end if

            MessageSuccess(string.Format(CommonSetting.SuccessCreateRecordsArgs, "this Announcement"), true, true);
            ModelState.Clear();
            var model1 = new AnnouncementDetailsViewModels(_httpContextAccessor);
            setInitDetailsData(model1);
            return PartialView(model1);

        }
        
        [Permissions]
        public ActionResult Details(string id, string id2, string id3)
        {
            var model = _announcementBAL.getDetail(id, id2);
            CommonFunctionsBAL.AssignEmptyProperty(model);
            return PartialView(model);
        }

        [Permissions]
        public ActionResult Edit(string id, string id2, string id3, string id4, string id5)
        {

            var model = _announcementBAL.getDetail(id, id2);
            if (!model.IsNullOrEmpty())
            {
                setAllDetailsData(model);
            }//end if

            return PartialView(model);
        }

        [Permissions]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnnouncementDetailsViewModels model)
        {
            bool isError = false;

            setAllDetailsData(model);

            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }//end if

            isError = _announcementBAL.Edit(model, ModelState);

            if (!ModelState.IsValid)
            {
                MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false), false, true);
                return PartialView(model);
            }//end if

            if (isError)
            {
                MessageDanger(CommonSetting.PleaseContactAdmin, true, true);
                return PartialView(model);
            }//end if

            MessageSuccess(string.Format(CommonSetting.SuccessModifyRecordsArgs, ""), true, true);
            return PartialView(model);
        }

        [Permissions]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactived(List<CommentChatListViewModels> model)
        {
            bool isError = false;
            try
            {
                //if (ModelState.IsValid)
                //{
                    //isError = _commentChatBAL.Deactived(model);
                    if (isError)
                    {
                        return Json(new { Errors = CommonSetting.PleaseContactAdmin });
                    }//end if
                //}//end if

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Json(new { Errors = CommonSetting.PleaseContactAdmin });
            }
            return Json(new { success = CommonSetting.SuccessModifyRecords });
        }
        #endregion
        #region Private Functions
        private void setAllDetailsData(AnnouncementDetailsViewModels model)
        {
            model.AnnouncementTypeDDL = _servicesBAL.GetAnnouncementTypeList();
            model.DisplayFrequencyDDL = _servicesBAL.GetAnnouncementFrequencyList();
        }

        private void setInitDetailsData(AnnouncementDetailsViewModels model)
        {
            model.AnnouncementTypeDDL = _servicesBAL.GetAnnouncementTypeList();
            model.IsMerchant = true;
            model.IsAdmin = true;
            model.IsPartner = true;
            model.DisplayFrequencyDDL = _servicesBAL.GetAnnouncementFrequencyList();
        }
        #endregion
    }//end class
}//end namespace