using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoMyShops.BAL;
using GoMyShops.Models;
using Microsoft.AspNetCore.Http;
namespace GoMyShops.Web.Controllers
{
    public class SystemSettingsController : BaseController
    {
        #region Definitions
        private readonly ILogger<SystemSettingsController> _logger;
        IServicesBAL _servicesBAL;
        IDataSettingBAL _dataSettingBAL;
        public readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public SystemSettingsController(IHttpContextAccessor httpContextAccessor, IDataSettingBAL dataSettingBAL, IServicesBAL servicesBAL, ILogger<SystemSettingsController> logger)
        {
            _servicesBAL = servicesBAL;
            _dataSettingBAL = dataSettingBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions 
        public ActionResult List()
        {
            return View();
        }
        #endregion
        #region Private Functions
        #endregion
    }
}