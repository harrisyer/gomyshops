using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using GoMyShops.BAL;
using GoMyShops.Commons;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.BAL.MVCFilters;
using GoMyShops.Models.Helpers;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Protocols;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Hosting;
namespace GoMyShops.Controllers
{
    [CustomAuthorization]
    public class SignUpController :Web.Controllers.BaseController
    {
        #region Definations
        private readonly ILogger<SignUpController> _logger;
        public IConfiguration _configuration { get; private set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        ISignUpBAL _signUpBAL;
        IServicesBAL _servicesBAL;
        #endregion
        #region Constructor
        public SignUpController(IHttpContextAccessor httpContextAccessor, ISignUpBAL signUpBAL, IServicesBAL servicesBAL, IConfiguration configuration, ILogger<SignUpController> logger)
        {
            _signUpBAL = signUpBAL;
            _servicesBAL = servicesBAL;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions
        [Permissions]
        public ActionResult List(SignUpViewModels model)
        {
            model.StatusDDL = _servicesBAL.GetStatusList();
            return View(model);
        }

        [HttpGet]
        public JsonResult getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8)
        {
            var totals = 0;

            var model = new
            {
                rows = _signUpBAL.getData(offset, limit, search, sort, order, param1, param2, param3, param4, param5, param6, param7, param8, ref totals),
                total = totals,

            };
            return Json(model);

        }

        [AllowAnonymous]       
        public ActionResult Create()
        {
            if (_configuration["AppSettings:SignUp"] == "1")
            {
                var model = new SignUpDetailsViewModels();
                setInitDetailsData(model);
                return View(model);
            }
            else
            {
                return NotFound();
            }//end if-else
           
        }

        [AllowAnonymous]
       
        public ActionResult PopulateCreate()
        {
            var model = new SignUpDetailsViewModels();
            return PartialView(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SignUpDetailsViewModels model)
        {
            ModelState.Remove("IsSignUpSuccess");
            model.IsSignUpSuccess = false;
            if (_configuration["AppSettings:SignUp"] == "1")
            {
                bool isError = false;

                if (!ModelState.IsValid)
                {
                    //setAllDetailsData(model);
                    return ViewComponent("PopulateCreate",model);
                    //return PartialView("PopulateCreate", model);

                }//end if

                //CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
                //if (response.Success && ModelState.IsValid)
                //{

                    isError = await _signUpBAL.Create(model, ModelState);

                    if (!ModelState.IsValid)
                    {
                        MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false, "The password you entered is invalid"), false, true);
                        setAllDetailsData(model);
                    return ViewComponent("PopulateCreate", model);

                }//end if

                    if (isError)
                    {
                        setAllDetailsData(model);
                        MessageDanger(CommonSetting.PleaseContactAdmin, true, true);
                    return ViewComponent("PopulateCreate", model);

                }//end if

                //return RedirectToAction("Login", "Account", new { type = "7" });
                MessageSuccess(string.Format(CommonSetting.SuccessCreateRecords, ""), true, true);
                setAllDetailsData(model);
                    model.IsSignUpSuccess = true;
                return ViewComponent("PopulateCreate", model);
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Captcha Error");// + response.ErrorMessage[0].ToString());
                //    MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false), false, true);
                //    return PartialView("PopulateCreate", model);
                //    //return Json(new { Success = CommonSetting.SuccessModifyRecords, OperationContainer = CommonFunctionsBAL.RenderRazorViewToString(ControllerContext, "PopulateCreate", model) }, JsonRequestBehavior.AllowGet);

                //    //return Content("Error From Captcha : " + response.ErrorMessage[0].ToString());
                //}




                //MessageSuccess(CommonSetting.SuccessCreateRecords, true, true);
                //ModelState.Clear();
                //var model1 = new SignUpDetailsViewModels();
                //setAllDetailsData(model1);
                //return View(model1);
            }
            else
            {
                return NotFound();
            }//end if-else

        }

        [Permissions]
        public ActionResult Details(string id, string id2, string id3)
        {
            var model = _signUpBAL.getDetail(id, id2);
            CommonFunctionsBAL.AssignEmptyProperty(model);
            return PartialView(model);
        }

        [Permissions]
        public ActionResult Edit(string id, string id2, string id3, string id4, string id5)
        {
            var model = _signUpBAL.getDetail(id, "");
            if (!model.IsNullOrEmpty())
            {
                setAllDetailsData(model);
            }//end if

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SignUpDetailsViewModels model)
        {
            bool isError = false;

            setAllDetailsData(model);
            if (ModelState.IsErrors("Password", "ConfirmPassword"))
            {
                return PartialView(model);
            }//end if

            isError = _signUpBAL.Edit(model, ModelState);

            if (ModelState.IsErrors("Password", "ConfirmPassword"))
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
        #endregion
        #region Private Functions
        private void setAllDetailsData(AccountManagerDetailsViewModels model)
        {
            model.UserCodeDDL = _servicesBAL.GetAccountManagerUserList(model.AccountManagerUserCode);
        }

        public async Task< CaptchaResponse> ValidateCaptcha(string response)
        {
            string secret = _configuration["AppSettings:recaptchaPrivatekey"].IsNullThenEmpty();
            var client = new HttpClient();
            var jsonResult =await client.GetStringAsync(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }

        #endregion
        #region Private Functions
        private void setAllDetailsData(SignUpDetailsViewModels model)
        {
      
        }

        private void setInitDetailsData(SignUpDetailsViewModels model)
        {
     
        }

        #endregion
    }//end class
}//end namespace