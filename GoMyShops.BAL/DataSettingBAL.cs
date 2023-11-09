using GoMyShops.Commons;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace GoMyShops.BAL
{
    public interface IDataSettingBAL
    {
        Task<List<DataSettingListModel>> GetDataSettingListAsync();
       // List<DataSettingListModel> GetDataSettingList();
        DataDetailsSettingModels GetDataSettingByName(string settingName);
        DataDetailsSettingModels GetDataSettingByID(int id);

        Task<bool> CreateAsync(DataDetailsSettingModels model, ModelStateDictionary modelState);
        bool Create(DataDetailsSettingModels model, ModelStateDictionary modelState);
        Task<bool> EditAsync(DataDetailsSettingModels model, ModelStateDictionary modelState);

    }//end interface
  
    public class DataSettingBAL: IDataSettingBAL
    {
        #region Definitions
        private readonly ILogger<DataSettingBAL> _logger;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public bool isError { get; set; }
        #endregion
        #region Constructor
        public DataSettingBAL(ILogger<DataSettingBAL> logger, IHttpContextAccessor httpContextAccessor, IUnitOfWorkFactory uowFactory, IMapper mapper)
        {
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion
        #region Public Functions
        public async Task<List<DataSettingListModel>> GetDataSettingListAsync()
        {
            List<DataSettingListModel>infos = new List<DataSettingListModel>();
           
            try
            {
                  infos =await _uow.Repository<SYS_DataSetting>().GetAsQueryable().AsNoTracking()
                                                // .Where(r => r.SettingName == settingName )
                                                .OrderBy(x => x.SettingName)
                                                 .Select(r => new DataSettingListModel()
                                                 {
                                                     SettingName = r.SettingName,
                                                     SettingValue = r.SettingValue,
                                                     Id = r.Id,
                                                     IsReadOnly = r.IsReadOnly,
                                                     ReadOnly = r.IsReadOnly == true ? "Yes" : "No",
                                                     DetailJson = new ActionsListDetails(r.SettingName, "", "", "", ""),
                                                     EditJson = new ActionsListDetails(r.SettingName, "", "", "", "")
                                                 }).ToListAsync();

                return infos.IsNullThenNew(_httpContextAccessor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public DataDetailsSettingModels GetDataSettingByName(string settingName)
        {
            DataDetailsSettingModels infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                infos = _uow.Repository<SYS_DataSetting>().GetAsQueryable()
                                                .Where(r => r.SettingName == settingName)
                                                .OrderBy(x => x.SettingName)
                                                 .Select(r => new DataDetailsSettingModels()
                                                 {
                                                     SettingName = r.SettingName,
                                                     SettingValue = r.SettingValue,
                                                     IsReadOnly = r.IsReadOnly,
                                                     Id = r.Id
                                                 }).FirstOrDefault();
                //}//end using

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }
  
        public DataDetailsSettingModels GetDataSettingByID(int id)
        {
            DataDetailsSettingModels infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                infos = _uow.Repository<SYS_DataSetting>().GetAsQueryable()
                                                .Where(r => r.Id == id)
                                                 .Select(r => new DataDetailsSettingModels()
                                                 {
                                                     SettingName = r.SettingName,
                                                     SettingValue = r.SettingValue,
                                                     IsReadOnly = r.IsReadOnly,
                                                     Id = r.Id
                                                 }).FirstOrDefault();
                //}//end using

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public async Task<bool> CreateAsync(DataDetailsSettingModels model, ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<SYS_DataSetting>().Get(x => x.SettingName == model.SettingName).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("SettingName", string.Format(CommonSetting.Messages.CodeExistArgs, "Name", model.SettingName));
                }//end if

                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                if (modelState.IsValid)
                {
                    SYS_DataSetting insertRow = new SYS_DataSetting();
                    var insertRepo = _uow.Repository<SYS_DataSetting>();

                    //Test AutoMapper
                    insertRow = _mapper.Map<SYS_DataSetting>(model);

                    //insertRow.SettingName = model.SettingName;
                    //insertRow.SettingValue = model.SettingValue;
                    //insertRow.IsReadOnly = model.IsReadOnly;
                    insertRepo.Insert(insertRow);
                    isError =await _uow.SaveAsync();
                }//end if

            }
            catch (Exception ex)
            {
                isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return isError;
        }
        public bool Create(DataDetailsSettingModels model, ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<SYS_DataSetting>().Get(x => x.SettingName == model.SettingName).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("SettingName", string.Format(CommonSetting.Messages.CodeExistArgs, "Name", model.SettingName));
                }//end if

                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                if (modelState.IsValid)
                {
                    SYS_DataSetting insertRow = new SYS_DataSetting();
                    var insertRepo = _uow.Repository<SYS_DataSetting>();
                    insertRow.SettingName = model.SettingName;
                    insertRow.SettingValue = model.SettingValue;
                    insertRow.IsReadOnly = model.IsReadOnly;
                    insertRepo.Insert(insertRow);
                    isError = _uow.Save();
                }//end if

            }
            catch (Exception ex)
            {
                isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return isError;
        }

        public async Task<bool> EditAsync(DataDetailsSettingModels model, ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<SYS_DataSetting>().Get(x => x.SettingName == model.SettingName && x.Id != model.Id).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("SettingName", string.Format(CommonSetting.Messages.CodeExistArgs, "Name", model.SettingName));
                }//end if

                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                var infos = _uow.Repository<SYS_DataSetting>().GetAsQueryable(x =>
                                      x.Id == model.Id
                                    ).FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    var entry = _uow.Context.Entry(infos);

                    entry.Property(u => u.SettingName).CurrentValue = model.SettingName;
                    entry.Property(u => u.SettingValue).CurrentValue = model.SettingValue;
                    _uow.Repository<SYS_DataSetting>().Update(entry);                
                    isError = await _uow.SaveAsync();              
                }//end if

            }
            catch (Exception ex)
            {
                isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return isError;
        }

        #endregion
        #region Private Functions
        #endregion
    }//end class
}//end namespace