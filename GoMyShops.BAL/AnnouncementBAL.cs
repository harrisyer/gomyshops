using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using System.Web;
using Newtonsoft.Json;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
//using System.Xml;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
//using HtmlAgilityPack;

namespace GoMyShops.BAL
{
    public interface IAnnouncementBAL
    {
        List<AnnouncementListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total);
        List<AnnouncementListViewModels> getPriorityData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total);
        bool Create(AnnouncementDetailsViewModels model, ModelStateDictionary modelState);
        AnnouncementDetailsViewModels getDetail(string id, string id2);
        bool Edit(AnnouncementDetailsViewModels model, ModelStateDictionary modelState);
        bool EditPriority(List<string> models, ModelStateDictionary modelState);
        Task<AnnouncementDisplayViewModels> GetAnnouncementListAsync(string Type);
        Task<AnnouncementDisplayViewModels> GetAnnouncementLocalListAsync();
        Task<AnnouncementDisplayViewModels> GetAnnouncementListwithIDAsync(string Id);
    }

    public class AnnouncementBAL : BaseBAL, IAnnouncementBAL
    {
        #region Definations
        private readonly ILogger<AnnouncementBAL> _logger;
        IUnitOfWork _uow;
        IServicesBAL _servicesBAL;
        IUsersBAL _userBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public AnnouncementBAL(IHttpContextAccessor httpContextAccessor, IUnitOfWorkFactory uowFactory, IServicesBAL servicesBAL, IUsersBAL userBAL, ILogger<AnnouncementBAL> logger) : base()
        {
            _uow = uowFactory.Create();
            _servicesBAL = servicesBAL;
            _userBAL = userBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            CurrentUser= CommonFunctionsBAL.GetCurrentUser(CurrentUser,_httpContextAccessor);
        }
        #endregion
        #region Public Functions
        public List<AnnouncementListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total)
        {
            try
            {
                string srcTitle = param1;
                string srcMessage = param2;
                string srcType = param3;
                string srcDisplayFrequency = param4;
                string srcTargetAudience = param5;
                string srcStartDate = param6;
                string srcStatus = param7;

                var StartDate = CommonFunctionsBAL.ParseStandardDateFormat(srcStartDate, false);

                var list = new List<AnnouncementListViewModels>();
                var queryableList = (from a in _uow.Repository<Announcement>().GetAsQueryable()
                                     select new AnnouncementListViewModels
                                     {
                                         CreatedBy =a.CreatedBy,
                                         CreateDate = a.CreatedTime,
                                         DisplayFrequency=a.DisplayFrequency,
                                         Id=a.Id,
                                         IsAdmin =a.IsAdmin,
                                         IsMerchant=a.IsMerchant,
                                         IsPartner=a.IsPartner,
                                         Priority=a.Priority,
                                         Title=a.Title,
                                         Message=a.Comment,
                                         Type =a.Type,                        
                                         Status = a.Status,
                                     }
                                    )
                    .WhereIf(!srcTitle.IsNullOrEmptyString(), tags => tags.Title.Contains(srcTitle))
                    .WhereIf(!srcMessage.IsNullOrEmptyString(), tags => tags.Message.Contains(srcMessage))
                    .WhereIf(!srcType.IsNullOrEmptyString(), tags => tags.Type == srcType)
                    .WhereIf(!srcDisplayFrequency.IsNullOrEmptyString(), tags => tags.DisplayFrequency == srcDisplayFrequency)
                    .WhereIf(!srcStatus.IsNullOrEmptyString(), tags => tags.Status == srcStatus)
                    .WhereIf(!srcStartDate.IsNullOrEmptyString(), tags => tags.CreateDate >= StartDate)
                    ;

                //check TargetAudience
                if (!srcTargetAudience.IsNullOrEmptyString())
                {
                    switch (srcTargetAudience)
                    {
                        case "2":
                            queryableList = queryableList.Where(tags => tags.IsMerchant == true);                        
                            break;
                        case "3":
                            queryableList = queryableList.Where(tags => tags.IsPartner == true);
                            break;
                        default:
                            queryableList = queryableList.Where(tags => tags.IsAdmin == true);
                            break;
                    }//end switch                       
                }//end if

               



                if (!sort.IsNullOrEmptyString())
                {
                    switch (sort)
                    {
                        case "sCreateDate":
                            sort = "CreateDate";
                            break;
                        default:
                            break;
                    }//end switch   

                    list = CustomExpression.IQueryable<AnnouncementListViewModels>(queryableList, sort, "tags", order)
                             .Skip((offset / limit) * limit).Take(limit)
                             .ToList();
                }
                else
                {
                    list = queryableList.OrderByDescending(tags => tags.CreateDate)
                                        .Skip((offset / limit) * limit).Take(limit)
                                        .ToList();

                }//end if-else

                total = queryableList.Count();

                //var iParamList = _servicesBAL.GetiParamList(CommonSetting.ParamCodes.AppStatus).ToList();
                var TypeParamList = _servicesBAL.GetAnnouncementTypeList().ToList();
                var FrequencyParamList = _servicesBAL.GetAnnouncementFrequencyList().ToList();
                List<AnnouncementListViewModels> b = (from a in list
                                                        select new AnnouncementListViewModels
                                                        {
                                                            CreatedBy = a.CreatedBy,
                                                            //CreateDate = a.CreatedTime,
                                                            sCreateDate =CommonFunctionsBAL.ParseStandardDateFormat(a.CreateDate, true, false) ,
                                                            DisplayFrequency = FrequencyParamList.Where(x => x.Value == a.DisplayFrequency).Select(x => x.Text).FirstOrDefault(),
                                                            Id = a.Id,
                                                            TargetAudience= ConvertTargetAudience(a.IsAdmin, a.IsMerchant, a.IsPartner),
                                                            //IsAdmin = a.IsAdmin,
                                                            //IsMerchant = a.IsMerchant,
                                                            //IsPartner = a.IsPartner,
                                                            Priority = a.Priority,
                                                            Title = ConventComment(a.Title),
                                                            Message = a.Message,
                                                            Type = TypeParamList.Where(x=>x.Value== a.Type).Select(x=>x.Text).FirstOrDefault(),
                                                            Status = a.Status,
                                                            DetailJson = new ActionsListDetails(a.Id.ToString(), "", "", "", ""),
                                                            EditJson = new ActionsListDetails(a.Id.ToString(), "", "", "", ""),

                                                        }).ToList();
                return b;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return new List<AnnouncementListViewModels>();
        }
        public List<AnnouncementListViewModels> getPriorityData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total)
        {
            try
            {
                //string srcTitle = param1;
                //string srcMessage = param2;
                //string srcType = param3;
                //string srcDisplayFrequency = param4;
                //string srcTargetAudience = param5;
                //string srcStartDate = param6;
                //string srcStatus = param7;

               

                var list = new List<AnnouncementListViewModels>();
                var queryableList = (from a in _uow.Repository<Announcement>().GetAsQueryable(
                                     x=>x.Status==CommonSetting.Status.Active &&
                                     x.EndTime > DateTime.Now)
                                     select new AnnouncementListViewModels
                                     {
                                         CreatedBy = a.CreatedBy,
                                         CreateDate = a.CreatedTime,
                                         StartDate=a.StartTime,
                                         EndDate =a.EndTime,
                                         DisplayFrequency = a.DisplayFrequency,
                                         Id = a.Id,
                                         IsAdmin = a.IsAdmin,
                                         IsMerchant = a.IsMerchant,
                                         IsPartner = a.IsPartner,
                                         Priority = a.Priority,
                                         Title = a.Title,
                                         Message = a.Comment,
                                         Type = a.Type,
                                         Status = a.Status,
                                     }
                                    )              
                    ;


                if (!sort.IsNullOrEmptyString())
                {
                    switch (sort)
                    {
                        case "sCreateDate":
                            sort = "CreateDate";
                            break;
                        default:
                            break;
                    }//end switch   

                    list = CustomExpression.IQueryable<AnnouncementListViewModels>(queryableList, sort, "tags", order)
                             .Skip((offset / limit) * limit).Take(limit)
                             .ToList();
                }
                else
                {
                    list = queryableList.OrderBy(tags => tags.Priority)
                                        .Skip((offset / limit) * limit).Take(limit)
                                        .ToList();

                }//end if-else

                total = queryableList.Count();

                //var iParamList = _servicesBAL.GetiParamList(CommonSetting.ParamCodes.AppStatus).ToList();
                var TypeParamList = _servicesBAL.GetAnnouncementTypeList().ToList();
                var FrequencyParamList = _servicesBAL.GetAnnouncementFrequencyList().ToList();
                List<AnnouncementListViewModels> b = (from a in list
                                                      select new AnnouncementListViewModels
                                                      {
                                                          CreatedBy = a.CreatedBy,
                                                          //CreateDate = a.CreatedTime,
                                                          CreateDate=a.CreateDate,
                                                          sCreateDate = CommonFunctionsBAL.ParseStandardDateFormat(a.CreateDate, true, false),
                                                          sStartDate = CommonFunctionsBAL.ParseStandardDateFormat(a.StartDate, true, false),
                                                          sEndDate = CommonFunctionsBAL.ParseStandardDateFormat(a.EndDate, true, false),
                                                          DisplayFrequency = FrequencyParamList.Where(x => x.Value == a.DisplayFrequency).Select(x => x.Text).FirstOrDefault(),
                                                          Id = a.Id,
                                                          TargetAudience = ConvertTargetAudience(a.IsAdmin, a.IsMerchant, a.IsPartner),
                                                          //IsAdmin = a.IsAdmin,
                                                          //IsMerchant = a.IsMerchant,
                                                          //IsPartner = a.IsPartner,
                                                          Priority=a.Priority,
                                                          Title = ConventComment(a.Title),
                                                          Message = a.Message,
                                                          Type = TypeParamList.Where(x => x.Value == a.Type).Select(x => x.Text).FirstOrDefault(),
                                                          Status = a.Status,
                                                          DetailJson = new ActionsListDetails(a.Id.ToString(), "", "", "", ""),
                                                          EditJson = new ActionsListDetails(a.Id.ToString(), "", "", "", ""),

                                                      })
                                                      .OrderBy(x => x.Priority)
                                                      .ToList();
                return b;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return new List<AnnouncementListViewModels>();
        }
        public bool Create(AnnouncementDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<Announcement>().Get(x => x.Title == model.Title).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("Title", string.Format(CommonSetting.Messages.CodeExistArgs, "Title", model.Title));
                }//end if

                model.StartDate = CommonFunctionsBAL.ParseStandardDateFormat(model.sStartDate, false);
                model.EndDate = CommonFunctionsBAL.ParseStandardDateFormat(model.sEndDate, false);
                model.EndDate = CommonFunctionsBAL.AddTimeEnd(model.EndDate);
                if (model.EndDate < model.StartDate)
                {
                    modelState.AddModelError("", string.Format("End Date must greater than Start Date."));
                }//end if

                if (modelState.IsValid)
                {
                    Announcement insertRow = new Announcement();
                    var insertRepo = _uow.Repository<Announcement>();
                 
                    insertRow.Title = model.Title;
                    insertRow.Type = model.Type.IsNullThenEmpty();
                    insertRow.Comment = model.Message.IsNullThenEmpty();
                    insertRow.IsAdmin = model.IsAdmin;
                    insertRow.IsMerchant = model.IsMerchant;
                    insertRow.IsPartner = model.IsPartner;
                    insertRow.Priority = 0;
                    insertRow.StartTime = model.StartDate;
                    insertRow.EndTime = model.EndDate;
                    insertRow.DisplayFrequency = model.DisplayFrequency.IsNullThenEmpty();
                    insertRow.Status = CommonSetting.Status.Active;
                    insertRow.CreatedBy = CurrentUser.Name;
                    insertRow.ModifiedBy= CurrentUser.Name;
                    insertRow.CreatedTime = DateTime.Now;
                    insertRow.ModifiedTime = DateTime.Now;
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
        public AnnouncementDetailsViewModels getDetail(string id, string id2)
        {
            AnnouncementDetailsViewModels model = new AnnouncementDetailsViewModels(_httpContextAccessor);
            try
            {
                int key = id.intParse();
                var statusRepo = _uow.Repository<StatusSU>();
                var data = (from a in _uow.Repository<Announcement>().GetAsQueryable(x => x.Id == key)
                         let status = statusRepo.dbSet.FirstOrDefault(p2 => a.Status == p2.Status)
                         select new AnnouncementDetailsViewModels(_httpContextAccessor)
                         {
                             DisplayFrequency = a.DisplayFrequency,
                             ID = a.Id,
                             IsAdmin = a.IsAdmin,
                             IsMerchant = a.IsMerchant,
                             IsPartner = a.IsPartner,
                             Title = a.Title,
                             Message = a.Comment,
                             Type = a.Type,
                             StartDate=a.StartTime,
                             EndDate=a.EndTime,
                             Status = status == null ? a.Status : status.StatusName,
                             CheckBoxStatus = a.Status == "1" ? true : false,
                             CreatedBy = a.CreatedBy,
                             CreatedTime = a.CreatedTime,
                             ModifiedBy = a.ModifiedBy,
                             ModifiedTime = a.ModifiedTime,
                         }).FirstOrDefault();

                if (data !=null)
                {
                    model = data;
                    model.sStartDate = CommonFunctionsBAL.ParseStandardDateFormat(model.StartDate, true, false);
                    model.sEndDate = CommonFunctionsBAL.ParseStandardDateFormat(model.EndDate, true, false);
                    model.sStartDateDisplay = CommonFunctionsBAL.ParseStandardDateFormat(model.StartDate, true, true);
                    model.sEndDateDisplay = CommonFunctionsBAL.ParseStandardDateFormat(model.EndDate, true, true);
                    model.sCreatedTime = CommonFunctionsBAL.ParseStandardDateFormat(model.CreatedTime, true, true);
                    model.sModifiedTime = model.ModifiedTime.HasValue ? CommonFunctionsBAL.ParseStandardDateFormat(model.ModifiedTime.GetValueOrDefault(), true, true) : " - ";

                    var TypeParamList = _servicesBAL.GetAnnouncementTypeList().ToList();
                    var FrequencyParamList = _servicesBAL.GetAnnouncementFrequencyList().ToList();
                    model.TypeDisplay = TypeParamList.Where(x => x.Value == model.Type).Select(x => x.Text).FirstOrDefault();
                    model.DisplayFrequencyDisplay = FrequencyParamList.Where(x => x.Value == model.DisplayFrequency).Select(x => x.Text).FirstOrDefault();
                    model.EditJson = JsonConvert.SerializeObject(new ActionsListDetails(model.ID.ToString(), "", "", "", ""));
                }//end if
               
            }
            catch (Exception ex)
            {
                 _logger.LogError("Error", ex);
            }
            finally { }
            return model.IsNullThenNew();
        }
        public bool Edit(AnnouncementDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<Announcement>().Get(x => x.Title == model.Title && x.Id!=model.ID).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("Title", string.Format(CommonSetting.Messages.CodeExistArgs, "Title", model.Title));
                }//end if

                model.StartDate = CommonFunctionsBAL.ParseStandardDateFormat(model.sStartDate, false);
                model.EndDate = CommonFunctionsBAL.ParseStandardDateFormat(model.sEndDate, false);

                if (model.EndDate < model.StartDate)
                {
                    modelState.AddModelError("", string.Format("End Date must greater than Start Date."));
                }//end if

                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                var infos = _uow.Repository<Announcement>().GetAsQueryable(x =>
                                     x.Id == model.ID
                                    ).FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    var entry = _uow.Context.Entry(infos);
                    entry.Property(u => u.Title).CurrentValue = model.Title;
                    entry.Property(u => u.Type).CurrentValue = model.Type.IsNullThenEmpty();
                    entry.Property(u => u.Comment).CurrentValue = model.Message;
                    // entry.Property(u => u.Priority).CurrentValue = model.Priority;
                    entry.Property(u => u.IsAdmin).CurrentValue = model.IsAdmin;
                    entry.Property(u => u.IsMerchant).CurrentValue = model.IsMerchant;
                    entry.Property(u => u.IsPartner).CurrentValue = model.IsPartner;
                    entry.Property(u => u.StartTime).CurrentValue = model.StartDate;
                    entry.Property(u => u.EndTime).CurrentValue = model.EndDate;
                    entry.Property(u => u.ModifiedBy).CurrentValue = CurrentUser.Name; 
                    entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                    entry.Property(u => u.Status).CurrentValue = CommonFunctions.Iif(model.CheckBoxStatus == true, CommonSetting.Status.Active, CommonSetting.Status.Inactive);
                    _uow.Repository<Announcement>().Update(entry);
                    _uow.Repository<Announcement>().Update(infos);
                    //_uow.AuditKey = CurrentUser.AuditKey;             
                    isError = _uow.Save();
                }//end if

            }
            catch (Exception ex)
            {
                isError = true;
                 _logger.LogError("Error", ex);
                return true;
            }
            finally { }
            return isError;
        }
        public bool EditPriority(List<string> models, ModelStateDictionary modelState)
        {
            int count = 1;
            try
            {
                //if (!modelState.IsValid)
                //{
                //    return false;
                //}//end if

                var infos = _uow.Repository<Announcement>().GetAsQueryable(
                                     x => x.Status == CommonSetting.Status.Active &&
                                     x.EndTime > DateTime.Now
                                    ).ToList();
                if (!infos.IsNullOrEmpty() && infos != null)
                {
                    //var y1 = JsonConvert.DeserializeObject<List<AnnouncementIndexList>>(models);
                   
                    foreach (var item in models)
                    {
                        var id = item.intParse();
                        var data = infos.Where(x =>
                                   x.Id == id
                                  ).FirstOrDefault();
                        if (data!=null)
                        {
                            var entry = _uow.Context.Entry(data);
                            entry.Property(u => u.Priority).CurrentValue = count;
                            _uow.Repository<Announcement>().Update(entry);
                            _uow.Repository<Announcement>().Update(data);
                        }//end if
                        
                        count = count + 1;
                    }//end foreach
                    isError = _uow.Save();
                }//end if

            }
            catch (Exception ex)
            {
                isError = true;
                 _logger.LogError("Error", ex);
                return true;
            }
            finally { }
            return isError;
        }
        public async Task<AnnouncementDisplayViewModels> GetAnnouncementListwithIDAsync(string Id)
        {
            var model = new AnnouncementDisplayViewModels();
            //List<AnnouncementDisplayDetailsViewModels> infos = null;
            try
            {
                int id = Id.intParse();
                model =await  GetAnnouncementListAsync("");

                if (model.advm!=null)
                {
                    var selectedAnnouncement = model.advm.Where(x=>x.ID==id).FirstOrDefault();
                    if (selectedAnnouncement != null)
                    {
                        selectedAnnouncement.CssClass = "active";
                    }
                }
                

            }
            catch (Exception ex)
            {
                 _logger.LogError("Error", ex);
            }
            finally { }
            return model;
        }
        public async Task<AnnouncementDisplayViewModels> GetAnnouncementListAsync(string Type)
        {
            var model = new AnnouncementDisplayViewModels();
            List<AnnouncementDisplayDetailsViewModels> infos = null;
            try
            {
                var queryList = _uow.Repository<Announcement>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active
                                && x.StartTime < DateTime.Now 
                                && x.EndTime > DateTime.Now);
        

                switch (Type)
                {
                    //case CommonSetting.AnnouncementType.InternallyAndPublic:
                    //    queryList = queryList.Where(x => (x.Type == CommonSetting.AnnouncementType.Public || x.Type == CommonSetting.AnnouncementType.InternallyAndPublic));
                    //    break;
                    case CommonSetting.AnnouncementType.Public:
                        queryList = queryList.Where(x => (x.Type == CommonSetting.AnnouncementType.Public || x.Type == CommonSetting.AnnouncementType.InternallyAndPublic));
                        break;
                    case CommonSetting.AnnouncementType.Internally:
                        queryList = queryList.Where(x => (x.Type == CommonSetting.AnnouncementType.Internally || x.Type == CommonSetting.AnnouncementType.InternallyAndPublic));
                        queryList = queryList.Where(x => x.DisplayFrequency != "2");
                        break;
                    default:
                        break;
                }//end switch   

               
                switch (CurrentUser.UserType)
                {
                    case CommonSetting.UserType.Admin:
                    case CommonSetting.UserType.AccountManager:
                         queryList = queryList.Where(x=>x.IsAdmin==true);
                        break;
                    case CommonSetting.UserType.Customer:
                        queryList = queryList.Where(x => x.IsMerchant == true);
                        break;
                    case CommonSetting.UserType.Partner:
                        queryList = queryList.Where(x => x.IsPartner == true);
                        break;
                    default:
                        break;
                }//end switch  

                infos = queryList.Select(a =>
                        new AnnouncementDisplayDetailsViewModels
                        {
                            DisplayFrequency = a.DisplayFrequency,
                            ID = a.Id,
                            IsAdmin = a.IsAdmin,
                            IsMerchant = a.IsMerchant,
                            IsPartner = a.IsPartner,
                            Title = a.Title,
                            Message = a.Comment,
                            Type = a.Type,
                            Priority = a.Priority,
                            StartDate = a.StartTime,
                            EndDate = a.EndTime,
                            CreatedTime = a.CreatedTime,
                            ModifiedTime = a.ModifiedTime,
                            CssClass = "",
                        }).ToList();

                //Check Frequency
                if (Type == CommonSetting.AnnouncementType.Internally)
                {
                    if (infos != null)
                    {  
                        DateTime lastLogin = await _userBAL.GetUserLastLoginDateAsync();

                        infos = ((from a in infos.Where(x => x.DisplayFrequency == "0")
                                    select a)
                                    .Union
                                   (from a in infos.Where(x => (x.StartDate > lastLogin
                                            && x.DisplayFrequency == "1") ||
                                            (x.StartDate < lastLogin && x.CreatedTime>lastLogin
                                            && x.DisplayFrequency == "1") ||
                                            (x.StartDate < lastLogin && x.ModifiedTime > lastLogin
                                            && x.DisplayFrequency == "1")
                                            )
                                    select a)).ToList();

                        //infos = infos.OrderBy(x => x.Priority).OrderByDescending(x => x.StartDate).ToList();
                        

                    }//end if
                }

                if (infos != null)
                {
                    infos = infos.OrderBy(x => x.Priority).ToList();
                }//end if
               
                model.advm = infos;
                model.AnnouncementCount = infos.Count();
            }
            catch (Exception ex)
            {
                 _logger.LogError("Error", ex);
            }
            finally { }
            return model;
        }

        public async Task<AnnouncementDisplayViewModels> GetAnnouncementLocalListAsync()
        {
            int count = 0;
            var model = new AnnouncementDisplayViewModels();
            try
            {
                model.advm = new List<AnnouncementDisplayDetailsViewModels>();
                model.advm2 = new List<AnnouncementDisplayDetailsViewModels>(); 
                var data =await GetAnnouncementListAsync("");
                foreach (var item in data.advm)
                {
                    item.Title = ConventComment(HttpUtility.HtmlDecode(item.Title).Trim(),200);
                    item.Message = ConventComment( HttpUtility.HtmlDecode(item.Message).Trim(),200);
                    if (count % 2 == 0)
                    {
                        model.advm.Add(item);
                    }
                    else
                    {
                        model.advm2.Add(item);
                    }//end if-else

                    count = count + 1;
                }//end foreach
            }
            catch (Exception ex)
            {         
                 _logger.LogError("Error", ex);         
            }
            finally { }
            return model;
        }

        #endregion
        #region Private Functions
        private string ConvertTargetAudience(bool IsAdmin, bool IsMerchant, bool IsPartner)
        {            
            List<string> TargetAudienceList = new List<string>(); 
            if (IsAdmin)
            {
                TargetAudienceList.Add("Admin") ;
            }
            if (IsMerchant)
            {
                TargetAudienceList.Add("Merchant");
            }
            if (IsPartner)
            {
                TargetAudienceList.Add("Partner");
            }
            return String.Join(",", TargetAudienceList);
        }
        private string ConventComment(string comment,int length=50)
        {
            if (comment.Length <= 50)
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(comment);
                string result = htmlDoc.DocumentNode.InnerText;
                return result;
            }
            else
            {
                if (comment.Length < length)
                {
                    length = comment.Length;
                }//end if

                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(comment.Substring(0, length) + "...");
                string result = htmlDoc.DocumentNode.InnerText;
                return result;
            }//end if-else
        }
        #endregion
    }//end class
}//end namespace