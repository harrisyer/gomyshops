using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoMyShops.BAL;
using GoMyShops.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoMyShops.Web.Controllers
{
    public class CentralizeNameController : BaseController
    {
        #region Definations
        ILogger<CentralizeNameController> _logger;
        IServicesBAL _servicesBAL;
        ICentralizeNameBAL _centralizeNameBAL;
        public readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public CentralizeNameController(IHttpContextAccessor httpContextAccessor, ICentralizeNameBAL centralizeNameBAL, IServicesBAL servicesBAL, ILogger<CentralizeNameController> logger)
        {
            _servicesBAL = servicesBAL;
            _centralizeNameBAL = centralizeNameBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions      
        public ActionResult List(List<DataSettingListModel> model)
        {
            model = _centralizeNameBAL.GetCentralizeNameList();

            //try
            //{
            //    //var a = 0;
            //    //var b = 10 / a;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error("Error", ex);
            //}
            //finally { }
            return View(model);
        }

        public ActionResult Details(string name)
        {
            CentralizeDetailsNameModels model = _centralizeNameBAL.GetCentralizeNameDetailByName(name);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(CentralizeDetailsNameModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if

            bool isError = false;

            isError = _centralizeNameBAL.SetCentralizeNameValue(ref model, ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if
        
            return File(model.byteArray, "text/sql",model.SettingValue);

           // return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateFile(CentralizeDetailsNameModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if

            bool isError = false;

            isError = _centralizeNameBAL.SetCentralizeNameValue(ref model, ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if


            return File(model.byteArray, "text/sql", model.SettingValue);
   
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file is null)
            {
                return BadRequest();
            }//end if
          
            var returnModel =await _centralizeNameBAL.SetCentralizeNameValue(file);

            if (returnModel == null)
            {
                return BadRequest();
            }//end if

            return File(returnModel.ByteArray, "text/sql", returnModel.FileName); 
        }


        #endregion
        #region Private Functions
        #endregion
    }//end class
}//end namespace