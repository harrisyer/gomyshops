using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoMyShops.BAL;
using GoMyShops.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoMyShops.Web.Controllers
{
    public class DataSettingController : BaseController
    {
        #region Definitions
        private readonly ILogger<DataSettingController> _logger;
        IServicesBAL _servicesBAL;
        IDataSettingBAL _dataSettingBAL;
        public readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public DataSettingController(IHttpContextAccessor httpContextAccessor, IDataSettingBAL dataSettingBAL, IServicesBAL servicesBAL, ILogger<DataSettingController> logger)
        {
            _servicesBAL = servicesBAL;
            _dataSettingBAL = dataSettingBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions      
        [HttpGet(Name = "GetDataSettingList")]        
        public async Task<List<DataSettingListModel>> GetDataSettingListAsync(
           )
        {
            var data = await _dataSettingBAL.GetDataSettingListAsync();
            return data;
        }


        public ActionResult Details(string name)
        {
            DataDetailsSettingModels model = _dataSettingBAL.GetDataSettingByName(name);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new DataDetailsSettingModels(_httpContextAccessor);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataDetailsSettingModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if

            bool isError = false;

            isError = _dataSettingBAL.Create(model, ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            DataDetailsSettingModels model = _dataSettingBAL.GetDataSettingByID(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(DataDetailsSettingModels model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if

            bool isError = false;

            isError = await _dataSettingBAL.EditAsync(model, ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }//end if

            return View(model);
        }

        #endregion
        #region Private Functions
        #endregion
    }//end class
}//end namespace