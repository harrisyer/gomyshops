using GoMyShops.Commons;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.BAL
{
    public interface ICentralizeNameBAL
    {
        #region Interface Methods
        List<DataSettingListModel> GetCentralizeNameList();
        CentralizeDetailsNameModels GetCentralizeNameDetailByName(string settingName);
        bool SetCentralizeNameValue(ref CentralizeDetailsNameModels model, ModelStateDictionary modelState);
        Task<CentralizeFileModel> SetCentralizeNameValue(IFormFile file);
        #endregion
    }//end interface

    public class CentralizeNameBAL: ICentralizeNameBAL
    {
        #region Definitions
        private readonly ILogger<CentralizeNameBAL> _logger;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        IDataSettingBAL _dataSettingBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
    public bool isError { get; set; }
    #endregion
    #region Constructor
    public CentralizeNameBAL(ILogger<CentralizeNameBAL> logger,IHttpContextAccessor httpContextAccessor, IDataSettingBAL dataSettingBAL, IUnitOfWorkFactory uowFactory)
    {
        _uowFactory = uowFactory;
        _uow = uowFactory.Create();
        _dataSettingBAL = dataSettingBAL;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

        }
    #endregion
    #region Public Functions
    public List<DataSettingListModel> GetCentralizeNameList()
    {
        List<DataSettingListModel> infos = new List<DataSettingListModel>();
        try
        {
            infos = _uow.Repository<SYS_DataSetting>().GetAsQueryable()
                                            .Where(r => r.SettingName == "SQL File Running No")
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
                                             }).ToList();
        }
        catch (Exception ex)
        {
                _logger.LogError("Error", ex);
            }
        finally { }
        return infos.IsNullThenNew(_httpContextAccessor);
    }

    public CentralizeDetailsNameModels GetCentralizeNameDetailByName(string settingName)
    {
        CentralizeDetailsNameModels infos = new CentralizeDetailsNameModels();
        try
        {
                var dataSetingsRow = _dataSettingBAL.GetDataSettingByName(settingName);
                infos.Id = dataSetingsRow.Id;
                infos.SettingName = dataSetingsRow.SettingName;
                infos.SettingValue = dataSetingsRow.SettingValue;    
            }
        catch (Exception ex)
        {
                _logger.LogError("Error", ex);
            }
        finally { }
            return infos;
    }

    public bool SetCentralizeNameValue(ref CentralizeDetailsNameModels model, ModelStateDictionary modelState)
    {        
        var newValue = "";
        var id = model.Id;
        try
        {
            List<SYS_DataSetting> rows = _uow.Repository<SYS_DataSetting>().GetAsQueryable(x =>
                                x.SettingName == "Major Number" || x.SettingName == "Minor Number"
                                ).ToList ();

            string MajorValue = rows.Where(e => e.SettingName == "Major Number").FirstOrDefault().SettingValue;
            string MinorValue = rows.Where(e => e.SettingName == "Minor Number").FirstOrDefault().SettingValue;

            if (model.ExtraValue.IsNullOrEmptyString())
            {
                newValue = string.Format("{0}_{1}_{2}.sql", DateTime.Now.ToString("yyyyMMddHHmmss"), MajorValue, MinorValue);
            }
            else
            {
                newValue = string.Format("{0}_{1}_{2}_{3}.sql", DateTime.Now.ToString("yyyyMMddHHmmss"), MajorValue, MinorValue, model.ExtraValue);

            }//end if-else

            var row = _uow.Repository<SYS_DataSetting>().GetAsQueryable(x =>
                                x.Id == id
                                ).FirstOrDefault();
            if (!row.IsNullOrEmpty())
            {
                var entry = _uow.Context.Entry(row);
              
                entry.Property(u => u.SettingValue).CurrentValue = newValue;
                _uow.Repository<SYS_DataSetting>().Update(entry);          
                isError = _uow.Save();
            }//end if
           
            model.SettingValue = newValue;

            model.byteArray = System.Text.Encoding.UTF8.GetBytes(model.SQLText);
        }
        catch (Exception ex)
        {
            isError = true;
                _logger.LogError("Error", ex);
            }
        finally { }
        return isError;
    }

    public async Task<CentralizeFileModel> SetCentralizeNameValue(IFormFile file)
        {      
            var returnModel = new CentralizeFileModel();
            try
            {
                List<SYS_DataSetting> rows = _uow.Repository<SYS_DataSetting>().GetAsQueryable(x =>
                                   x.SettingName == "Major Number" || x.SettingName == "Minor Number"
                                 ).ToList();

                string MajorValue = rows.Where(e => e.SettingName == "Major Number").FirstOrDefault().SettingValue;
                string MinorValue = rows.Where(e => e.SettingName == "Minor Number").FirstOrDefault().SettingValue;

                //Read the uploaded File as Byte Array from FileUpload control.
                await Task.Run(() => ReadFileStream(file,returnModel, MajorValue, MinorValue)); 

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return returnModel;
        }


        #endregion
    #region Private Functions
    private static void ReadFileStream(IFormFile file, CentralizeFileModel returnModel, string MajorValue, string MinorValue)
    {
        Stream fs = file.OpenReadStream();
        BinaryReader br = new BinaryReader(fs);
        returnModel.ByteArray = br.ReadBytes((Int32)fs.Length);

            var a = file.FileName.Split(Convert.ToChar(".")).ToList();
            if (a.Count > 0)
            {
                returnModel.FileName = string.Format("{0}_{1}_{2}_{3}.sql", DateTime.Now.ToString("yyyyMMddHHmmss"), MajorValue, MinorValue, a.FirstOrDefault());

            }
            else
            {
                returnModel.FileName = string.Format("{0}.sql",  "wrong name");
            }//end if-else
        }

    #endregion
    }//end class
}//end namespace