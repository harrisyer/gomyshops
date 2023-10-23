using GoMyShops.Commons;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoMyShops.BAL
{
    public interface ISystemSettingsBAL
    {
        List<SystemSettingsListModel> GetSystemSettingsList();
        List<SystemSettingsListModel> GetSystemSettingsList(int start, int pageSize,ref int total);
    }//end interface


    public class SystemSettingsBAL : ISystemSettingsBAL
    {
        #region Definations
        private readonly ILogger<SystemSettingsBAL> _logger;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public bool isError { get; set; }
        #endregion
        #region Constructor
        public SystemSettingsBAL(ILogger<SystemSettingsBAL> logger, IHttpContextAccessor httpContextAccessor, IUnitOfWorkFactory uowFactory)
        {
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }
        #endregion
        #region Public Functions
        public List<SystemSettingsListModel> GetSystemSettingsList()
        {
            List<SystemSettingsListModel> infos = new List<SystemSettingsListModel>();
            try
            {
                infos = _uow.Repository<SYS_Setting>().GetAsQueryable()
                                                // .Where(r => r.SettingName == settingName )
                                                .OrderBy(x => x.SettingName)
                                                 .Select(r => new SystemSettingsListModel()
                                                 {
                                                     SettingsType=r.SettingsType,
                                                     SettingName = r.SettingName,
                                                     SettingValue = r.SettingValue,
                                                     Id = r.Id,                                                  
                                                     DetailJson = new ActionsListDetails(r.SettingName, "", "", "", ""),
                                                     EditJson = new ActionsListDetails(r.SettingName, "", "", "", "")
                                                 }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SystemSettingsListModel> GetSystemSettingsList(int start, int pageSize, ref int total)
        {
            List<SystemSettingsListModel> infos = new List<SystemSettingsListModel>();
            try
            {
                total = _uow.Repository<SYS_Setting>().GetAsQueryable().Count(); 

                infos = _uow.Repository<SYS_Setting>().GetAsQueryable()
                                                // .Where(r => r.SettingName == settingName )
                                                .OrderBy(x => x.SettingName)
                                                 .Select(r => new SystemSettingsListModel()
                                                 {
                                                     SettingsType = r.SettingsType,
                                                     SettingName = r.SettingName,
                                                     SettingValue = r.SettingValue,
                                                     Id = r.Id,
                                                     DetailJson = new ActionsListDetails(r.SettingName, "", "", "", ""),
                                                     EditJson = new ActionsListDetails(r.SettingName, "", "", "", "")
                                                 }).Skip((start-1)* pageSize  ).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        #endregion
        #region Private Functions
        #endregion
    }//end class
}//end namespace