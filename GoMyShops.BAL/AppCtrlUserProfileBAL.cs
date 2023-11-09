using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using System.Web;
using Newtonsoft.Json;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using Microsoft.Extensions.Logging;

namespace GoMyShops.BAL
{
    public interface IAppCtrlUserProfileBAL
    {
        List<AppCtrDetailViewModels> GetAppCtrlSUList();
    }

    public class AppCtrlUserProfileBAL : BaseBAL, IAppCtrlUserProfileBAL
    {
        #region Definations
        private readonly ILogger<AppCtrlUserProfileBAL> _logger;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        IServicesBAL _servicesBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public AppCtrlUserProfileBAL(IHttpContextAccessor httpContextAccessor,IUnitOfWorkFactory uowFactory, IServicesBAL servicesBAL, ILogger<AppCtrlUserProfileBAL> logger) : base()
        {
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _servicesBAL = servicesBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions
        public List<AppCtrDetailViewModels> GetAppCtrlSUList()
        {
            List<AppCtrDetailViewModels> infos = null;
            try
            {
                infos = (from a in _uow.Repository<AppCtrlSU>().GetAsQueryable()
                        .Where(r => r.Status == CommonSetting.Status.Active)
                         select new AppCtrDetailViewModels
                         {
                             AppCtrlID = a.AppCtrlID,
                             AppCtrName = a.AppCtrName,
                             AppCtrType = a.AppCtrType,
                             SortOrder = a.SortOrder,
                         }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }
        #endregion
    }//end class
}//end namespace
