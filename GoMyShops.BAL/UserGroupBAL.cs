using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GoMyShops.BAL
{
    public interface IUserGroupBAL
    {
     
        Task<List<SelectListItem>> GetGroupTypeAsync();
        List<SelectListItem> GetActiveUserGroupList();
        List<SelectListItem> GetActiveUserGroupList(string GroupType, string Code);
        List<SelectListItem> GetActiveAdminUserGroupList();
        UserGroupDetailsViewModels getDetail(string id, string id2);
        List<UserGroupListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total);
        bool Create(UserGroupDetailsViewModels model, ModelStateDictionary modelState);
        bool Edit(UserGroupDetailsViewModels model, ModelStateDictionary modelState);
        void PopulateUserGroupAccessList(List<TreeViewItemModel> tvtmList, string UserGroup);
        Task<bool> SetUserGroupAccessAsync(TreeViewSelectItemsModel tvsims, ModelStateDictionary modelState);
    }

    public class UserGroupBAL: BaseBAL, IUserGroupBAL
    {
        #region Definations
        private readonly ILogger<UserGroupBAL> _logger;
        //private UrlHelper _urlHelper;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        IServicesBAL _servicesBAL;
        IAppCtrlUserProfileBAL _appCtrlUserProfileBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public UserGroupBAL(IHttpContextAccessor httpContextAccessor, IUnitOfWorkFactory uowFactory, IAppCtrlUserProfileBAL appCtrlUserProfileBAL, IServicesBAL servicesBAL):base()
        {
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _servicesBAL = servicesBAL;
            _appCtrlUserProfileBAL = appCtrlUserProfileBAL;
            _httpContextAccessor = httpContextAccessor;         
            CurrentUser = CommonFunctionsBAL.GetCurrentUser(CurrentUser, _httpContextAccessor);

        }
        #endregion
        #region Public Functions
        public List<UserGroupListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total)
        {

            var sessionclass = new SearchCriteria();
            try
            {
                sessionclass.srcGroupCode = param1;
                sessionclass.srcGroupType = "";
                sessionclass.srcStatus = param3;
                
                //if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                //{
                //    mCustomerCode
                //}//end if
                //string CustomerCode = 



                var List = new List<UserGroup>();
               // var queryableList1 = _uow.Repository<UserGroup>().GetAsQueryable().ToList();
                var queryableList = _uow.Repository<UserGroup>().GetAsQueryable()
                    .WhereIf(!sessionclass.srcGroupCode.IsNullOrEmptyString(), tags => tags.GroupCode.Contains(sessionclass.srcGroupCode))
                    .WhereIf(!sessionclass.srcGroupType.IsNullOrEmptyString(), tags => tags.GroupType == sessionclass.srcGroupType)
                    .WhereIf(!sessionclass.srcStatus.IsNullOrEmptyString(), tags => tags.Status == sessionclass.srcStatus)
                    ;

                //check Customer
                if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                {
                    mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
                    queryableList = queryableList.Where(tags => tags.CustomerCode == mCustomerCode);
                }//end if

                //check Partner
                if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                {
                    mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
                    queryableList = queryableList.Where(tags => tags.CustomerCode == mCustomerCode);
                }//end if

                if (!sort.IsNullOrEmptyString())
                {
                    List = CustomExpression.IQueryable<UserGroup>(queryableList, sort, "tags", order)
                             .Skip((offset / limit) * limit).Take(limit)
                             .ToList();
                }
                else
                {
                    List = queryableList//.AsQueryable()
                                      .OrderBy(tags => tags.GroupCode)
                                      .Skip((offset / limit) * limit).Take(limit)
                                      .ToList();

                }//end if-else

                total = queryableList//.AsQueryable()
                        .Count();



                List<UserGroupListViewModels> b = (from a in List
                                                   select new UserGroupListViewModels
                                                   {
                                                       GroupCode = a.GroupCode,
                                                       GroupName = a.GroupName,
                                                       GroupType = a.GroupType,
                                                       Description = a.Description,
                                                       SecurityId = a.SecurityId,
                                                       Status = a.Status,
                                                       DetailJson =new ActionsListDetails(a.GroupCode,"","","",""),  
                                                       EditJson = new ActionsListDetails(a.GroupCode, "", "", "", ""),
                                                   }).ToList();
                return b;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return new List<UserGroupListViewModels>();

        }

        public UserGroupDetailsViewModels getDetail(string id, string id2)
        {
            UserGroupDetailsViewModels model1 = null;
            try
            {
                var statusRepo = _uow.Repository<StatusSU>();
                var iparamRepo = _uow.Repository<Param>();
                model1 = (from B in _uow.Repository<UserGroup>().GetAsQueryable()
                                       .Where(x => x.CompanyCode == "1" && x.GroupCode == id )
                         join company in _uow.Repository<Company>().GetAsQueryable() on B.CompanyCode equals company.CompanyCode into C
                         from companyLOJ in C.DefaultIfEmpty()
                         join loginSU in _uow.Repository<LoginSU>().GetAsQueryable() on B.SecurityId equals loginSU.SecurityId into gloginSU
                         from loginSULOJ in gloginSU.DefaultIfEmpty()
                         let iparam = iparamRepo.dbSet.FirstOrDefault(p2 => B.GroupType == p2.ParamKey && p2.ParamCode == CommonSetting.ParamCodes.UserType)
                         let status = statusRepo.dbSet.FirstOrDefault(p2 => B.Status == p2.Status)
                         select new UserGroupDetailsViewModels(_httpContextAccessor)
                         {
                             CompanyCode= B.CompanyCode ,
                             Company = B.CompanyCode + " - " + companyLOJ.Name,
                             GroupCode =B.GroupCode,
                             GroupName =B.GroupName,
                             GroupTypeName = iparam == null ? B.GroupCode : B.GroupCode + " - " + iparam.ParamDesc,
                             GroupType =B.GroupType,
                             SecurityId=B.SecurityId,
                             SecurityName = B.SecurityId.ToString() + " - " + loginSULOJ.SecurityName,
                             Description =B.Description,
                             Status = status == null ? B.Status : status.StatusName,
                             CheckBoxStatus = B.Status == "1" ? true : false,
                             CreatedBy = B.CreatedBy,
                             CreatedTime = B.CreatedTime,
                             ModifiedBy = B.ModifiedBy,
                             ModifiedTime = B.ModifiedTime,
                         }).FirstOrDefault();

                if (model1!=null)
                {
                    //add to check IsInActiveable group
                    if (model1.GroupCode == "UGR0000001" || model1.GroupCode == "UGR0000002" || model1.GroupCode == "UGR0000003"
                        || model1.GroupCode == "UGR0000004" || model1.GroupCode == "UGR0000005" || model1.GroupCode == "UGR000006")
                    {
                        model1.IsInActive = true;
                    }//end if

                    //

                    model1.EditJson = JsonConvert.SerializeObject(new ActionsListDetails(model1.GroupCode, "", "", "", ""));
                    model1.sCreatedTime = CommonFunctionsBAL.ParseStandardDateFormat(model1.CreatedTime.IsNullTimeThenNew(), true, true);
                    model1.sModifiedTime = model1.ModifiedTime.HasValue ? CommonFunctionsBAL.ParseStandardDateFormat(model1.ModifiedTime.GetValueOrDefault(), true, true) : " - ";

                    //for Application Control Access right
                    model1.AppCtrDetailList = _appCtrlUserProfileBAL.GetAppCtrlSUList();


                    var bld = _uow.Repository<AppCtrlUserProfile>().GetAsQueryable(x => x.GroupCode == model1.GroupCode);
                    if (!bld.IsNullOrEmpty())
                    {
                        foreach (var item in model1.AppCtrDetailList)
                        {
                            if (bld.Where(x => x.AppCtrlID == item.AppCtrlID).Count() > 0)
                            {
                                item.Status = true;
                            }//end if
                        }//end foreach
                    }//end if
                }//end if

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return model1.IsNullThenNew(_httpContextAccessor);
        }

        public bool Create(UserGroupDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                string CustomerCode = "";
                //check Customer
                if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                {
                    CustomerCode = _servicesBAL.GetCustomerCodeFromUser();              
                }//end if

                //check Partner
                if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                {
                    CustomerCode = _servicesBAL.GetPartnerCodeFromUser();
                }//end if
                
                var TGcount = _uow.Repository<UserGroup>().Get(x => x.GroupName == model.GroupName && x.CustomerCode== CustomerCode).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("GroupName", string.Format(CommonSetting.Messages.CodeExistArgs, "Name", model.GroupName));
                }//end if



                if (modelState.IsValid)
                {
                    

                    UserGroup insertRow = new UserGroup();
                    var insertRepo = _uow.Repository<UserGroup>();
                    insertRow.CompanyCode = "1";
                    insertRow.GroupCode = _servicesBAL.GetUpdateDocCode(CommonSetting.DocCodes.UserGroup, "admin","1","0","0");
                    insertRow.GroupName = model.GroupName;
                    insertRow.CustomerCode = "";
                    insertRow.GroupType =  "W";// model.GroupType;
                    insertRow.Description = model.Description;
                    insertRow.SecurityId = 1;// model.SecurityId;
                    insertRow.Status = CommonSetting.Status.Active;

                    if (CurrentUser.UserType == CommonSetting.UserType.Customer )
                    {
                        insertRow.CustomerCode = CustomerCode;
                        insertRow.GroupType = "C";
                        insertRow.SecurityId = 2;
                    }//end if
                    if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                    {
                        insertRow.CustomerCode = CustomerCode;
                        insertRow.GroupType = "P";
                        insertRow.SecurityId = 2;
                    }//end if

                    insertRow.CreatedBy = CurrentUser.Name;
                    insertRow.CreatedTime = DateTime.Now;

                    insertRepo.Insert(insertRow);
                    model.GroupCode = insertRow.GroupCode;
                    //isError = _uow.Save();

                    //for Application Control Access right
                    if (model.AppCtrDetailList != null)
                    {
                        if (CurrentUser.UserType == CommonSetting.UserType.Customer || CurrentUser.UserType == CommonSetting.UserType.Partner)
                        {

                        }
                        else
                        {
                            var bld = _uow.Repository<AppCtrlUserProfile>().GetAsQueryable(x => x.GroupCode == model.GroupCode);
                            _uow.Repository<AppCtrlUserProfile>().DeleteAll(bld);

                            var insert1Repo = _uow.Repository<AppCtrlUserProfile>();
                            foreach (var item in model.AppCtrDetailList.Where(x => x.Status == true)
                               .Select((r, i) => new { Row = r, Index = i }))
                            {
                                AppCtrlUserProfile insertRowACUP = new AppCtrlUserProfile();
                                insertRowACUP.AppCtrlID = item.Row.AppCtrlID;
                                insertRowACUP.GroupCode = model.GroupCode;
                                insertRowACUP.CreatedBy = CurrentUser.Name;
                                insertRowACUP.CreatedTime = DateTime.Now;
                                insert1Repo.Insert(insertRowACUP);
                            }//end for            
                        }//end if-else
                    }//end if

                    
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

        public bool Edit(UserGroupDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                string CustomerCode = "";
                //check Customer
                if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                {
                    CustomerCode = _servicesBAL.GetCustomerCodeFromUser();
                }//end if

                //check Partner
                if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                {
                    CustomerCode = _servicesBAL.GetPartnerCodeFromUser();
                }//end if

                var TGcount = _uow.Repository<UserGroup>().Get(x => x.GroupName == model.GroupName && x.CustomerCode == CustomerCode && x.GroupCode != model.GroupCode).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("GroupName", string.Format(CommonSetting.Messages.CodeExistArgs, "Name", model.GroupName));
                }//end if

                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                var infos = _uow.Repository<UserGroup>().GetAsQueryable()
                                    .Where(x => x.CompanyCode == "1" && x.GroupCode == model.GroupCode)
                                    .FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    var entry = _uow.Context.Entry<UserGroup>(infos);
                    entry.Property(u => u.GroupName).CurrentValue = model.GroupName;
                    /*entry.Property(u => u.GroupType).CurrentValue = model.GroupType;*/
                    entry.Property(u => u.Description).CurrentValue = model.Description;
                    //entry.Property(u => u.SecurityId).CurrentValue = model.SecurityId;
                    entry.Property(u => u.ModifiedBy).CurrentValue = CurrentUser.Name;
                    entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                    //add to check IsInActiveable group
                    if (infos.GroupCode == "UGR0000001" || infos.GroupCode == "UGR0000002" || infos.GroupCode == "UGR0000003"
                        || infos.GroupCode == "UGR0000004" || infos.GroupCode == "UGR0000005" || infos.GroupCode == "UGR0000006")
                    {

                    }
                    else
                    {
                        entry.Property(u => u.Status).CurrentValue = CommonFunctions.Iif(model.CheckBoxStatus == true, CommonSetting.Status.Active, CommonSetting.Status.Inactive);
                    }

                }//end if

                //for Application Control Access right
                if (model.AppCtrDetailList!=null)
                {
                    var bld = _uow.Repository<AppCtrlUserProfile>().GetAsQueryable(x => x.GroupCode == model.GroupCode);
                    _uow.Repository<AppCtrlUserProfile>().DeleteAll(bld);

                    var insertRepo = _uow.Repository<AppCtrlUserProfile>();
                    foreach (var item in model.AppCtrDetailList.Where(x => x.Status == true)
                       .Select((r, i) => new { Row = r, Index = i }))
                    {
                        AppCtrlUserProfile insertRow = new AppCtrlUserProfile();
                        insertRow.AppCtrlID = item.Row.AppCtrlID;
                        insertRow.GroupCode = model.GroupCode;
                        insertRow.CreatedBy = CurrentUser.Name;
                        insertRow.CreatedTime = DateTime.Now;
                        insertRepo.Insert(insertRow);
                    }//end for
                }//end if             

                isError = _uow.Save();

                //
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

        public async Task<List<SelectListItem>> GetGroupTypeAsync()
        {
            List<SelectListItem> infos = new List<SelectListItem>();
            try
            {
                infos=await _servicesBAL.GetiParamListAsync(CommonSetting.ParamCodes.UserType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetActiveUserGroupList()
        {
            List<SelectListItem> infos = null;
            try
            {
                //check Customer
                if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                {
                    mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
                    if (mCustomerCode.IsNullOrEmptyString())
                    {
                        mCustomerCode = _servicesBAL.GetCustomerCodeFromUserEntity();              
                    }//end if
                }//end if

                //check Partner
                if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                {
                    mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
                    if (mCustomerCode.IsNullOrEmptyString())
                    {
                        mCustomerCode = _servicesBAL.GetPartnerCodeFromUserEntity();               
                    }//end if
                }//end if
             
                infos = _uow.Repository<UserGroup>().GetAsQueryable()
                             .Where(x => x.Status == CommonSetting.Status.Active)
                             //.Where(x=>x.GroupCode!=CommonSetting.GroupCode.Merchant)
                             //.Where(x => x.GroupCode != CommonSetting.GroupCode.Partner)
                             .WhereIf(!mCustomerCode.IsNullOrEmptyString(), tags => tags.CustomerCode == mCustomerCode)
                             .Select(r => new SelectListItem()
                             {
                                 Text = r.GroupName,
                                 Value = r.GroupCode
                             }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }
        public List<SelectListItem> GetActiveUserGroupList(string GroupType,string Code)
        {
            List<SelectListItem> infos = null;
            try
            {
                //check Customer
                if (CurrentUser.UserType == CommonSetting.UserType.Customer || GroupType == CommonSetting.UserType.Customer)
                {
                    mCustomerCode = Code;
                    //if (mCustomerCode.IsNullOrEmptyString())
                    //{
                    //    mCustomerCode = _servicesBAL.GetCustomerCodeFromUserEntity();
                    //}//end if
                }//end if

                //check Partner
                if (CurrentUser.UserType == CommonSetting.UserType.Partner || GroupType == CommonSetting.UserType.Partner)
                {
                    mCustomerCode = Code;
                    //if (mCustomerCode.IsNullOrEmptyString())
                    //{
                    //    mCustomerCode = _servicesBAL.GetPartnerCodeFromUserEntity();
                    //}//end if
                }//end if

                infos = _uow.Repository<UserGroup>().GetAsQueryable()
                             .Where(x => x.Status == CommonSetting.Status.Active)
                             //.Where(x=>x.GroupCode!=CommonSetting.GroupCode.Merchant)
                             //.Where(x => x.GroupCode != CommonSetting.GroupCode.Partner)
                             .WhereIf(!mCustomerCode.IsNullOrEmptyString(), tags => tags.CustomerCode == mCustomerCode)
                             .Select(r => new SelectListItem()
                             {
                                 Text = r.GroupName,
                                 Value = r.GroupCode
                             }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }
        public List<SelectListItem> GetActiveAdminUserGroupList()
        {
            List<SelectListItem> infos = null;
            try
            {
                //check Customer
                if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                {
                    mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
                }//end if

                //check Partner
                if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                {
                    mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
                }//end if

                infos = _uow.Repository<UserGroup>().GetAsQueryable()
                             .Where(x => x.Status == CommonSetting.Status.Active)
                             .Where(x=>x.GroupCode!=CommonSetting.GroupCode.Merchant)
                             .Where(x => x.GroupCode != CommonSetting.GroupCode.Partner)
                             .Where(x => x.GroupCode != CommonSetting.GroupCode.OnBoarding)
                             .WhereIf(!mCustomerCode.IsNullOrEmptyString(), tags => tags.CustomerCode == mCustomerCode)
                             .Select(r => new SelectListItem()
                             {
                                 Text = r.GroupName,
                                 Value = r.GroupCode
                             }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public void PopulateUserGroupAccessList(List<TreeViewItemModel> tvtmList, string UserGroup)
        {
            //int NodeIdCount = 1;
            int menuDetailCount = 0;
            int menuDetailTrueCount = 0;
            int moduleCount = 0;
            int moduleTrueCount = 0;
            try
            {
                //Check user group type
                var GroupType = (from a in _uow.Repository<UserGroup>().GetAsQueryable(x => x.GroupCode == UserGroup)
                             select a.GroupType
                               ).FirstOrDefault();

                //


                var iparamRepo = _uow.Repository<Param>();
                var moduleRepo = _uow.Repository<ModuleSU>();
                var uga = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == UserGroup).ToList();
                var moduleActionSUList = (from M in _uow.Repository<ModuleActionSU>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active)
                                          let iparam = iparamRepo.dbSet.FirstOrDefault(p2 => M.ModuleActionType == p2.ParamKey && p2.ParamCode == CommonSetting.ParamCodes.UserGroupModuleAccessType)
                                          let module = moduleRepo.dbSet.FirstOrDefault(p2 => M.ModuleID == p2.ModuleID )
                                          select new ModuleActionModel
                                          {
                                              ModuleActionType = iparam == null ? M.ModuleActionType : iparam.ParamDesc,
                                              ModuleName= module == null ? "" : module.ModuleName,
                                              ParentID=(int)module.ParentModuleID,
                                              SortSequence = (int)module.ModuleSequence,
                                              ApproveFlag = M.ApproveFlag,
                                              CreateFlag=M.CreateFlag,
                                              DeleteFlag=M.DeleteFlag,
                                              DetailFlag=M.DetailFlag,
                                              EditFlag=M.EditFlag,
                                              MenuFlag=M.MenuFlag,
                                              ModuleID=M.ModuleID,
                                          }).OrderBy(x=>x.ModuleID).ToList();

                if (UserGroup !=CommonSetting.GroupCode.AccountManager && UserGroup != CommonSetting.GroupCode.Administrator &&
                    UserGroup != CommonSetting.GroupCode.Merchant && UserGroup != CommonSetting.GroupCode.OnBoarding &&
                    UserGroup != CommonSetting.GroupCode.Partner )
                {
                    if (CurrentUser.UserType == CommonSetting.UserType.Customer || GroupType == CommonSetting.UserType.Customer)
                    {
                        var intUsergroup = _servicesBAL.GetCustomerGroupCode();
                        var ugaCustomer = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == intUsergroup).ToList();

                        var ugaModuleList = ugaCustomer.Select(x => x.ModuleID).ToList();
                        moduleActionSUList = moduleActionSUList.Where(x => ugaModuleList.Contains(x.ModuleID)).ToList();

                        if (!uga.IsNullOrEmpty() && uga != null)
                        {

                        }
                        else
                        {
                        }//end if-else


                    }//end if

                    if (CurrentUser.UserType == CommonSetting.UserType.Partner || GroupType == CommonSetting.UserType.Partner)
                    {
                        var intUsergroup = _servicesBAL.GetPartnerGroupCode();
                        var ugaCustomer = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == intUsergroup).ToList();

                        var ugaModuleList = ugaCustomer.Select(x => x.ModuleID).ToList();
                        moduleActionSUList = moduleActionSUList.Where(x => ugaModuleList.Contains(x.ModuleID)).ToList();

                        if (!uga.IsNullOrEmpty() && uga != null)
                        {

                        }
                        else
                        {
                        }//end if-else


                    }//end if
                }//end if

               

                //add by harris for order by top menu
                List<ModuleActionModel> MenuList = new List<ModuleActionModel>();

                var ModuleList = (from a in _uow.Repository<ModuleSU>().GetAsQueryable(x => x.ModuleStatus == 1 && x.Type == "N" && x.Default!="Y")
                                   //where a.ParentModuleID == 0
                                   select new ModuleActionModel
                                   {
                                       ParentID = (int)a.ParentModuleID,
                                       SortSequence = (int)a.ModuleSequence,
                                       ModuleID = a.ModuleID,
                                       ModuleName = a.ModuleName,
                                   }).ToList();
                //TODO
                ModuleList= ModuleList.Where(x => moduleActionSUList.Select(y=>y.ModuleID).Contains(x.ModuleID)).ToList();
                //

                var topMenuList = (from a in ModuleList
                                   where a.ParentID == 0
                                   select a).ToList();

                foreach (ModuleActionModel a in topMenuList)
                {
                    a.TopMenu = true;
                    a.isChecked = true;
                }
                MenuList.AddRange(topMenuList.OrderBy(s => s.SortSequence));


                var uncheckList = from a in ModuleList
                                  where a.isChecked == false
                                  select a;

                foreach (ModuleActionModel c in MenuList)
                {
                    TreeViewItemModel tvimMenu = new TreeViewItemModel();
                    tvimMenu.text = c.ModuleName;
                    tvimMenu.nodeId = c.ModuleID.ToString();
                    tvimMenu.nodeCode = c.ModuleID.ToString();
                    tvimMenu.parentId = "0";
                    tvimMenu.type = "DM";
                    tvimMenu.selectable = true;
                    tvimMenu.color = "#000000";
                    tvimMenu.backColor = "#9589FF";
                    tvimMenu.icon = "glyphicon";

                    var gotChildCount = uga.Where(x =>x.ModuleID==c.ModuleID   &&  (x.MenuFlag == true
                                                     || x.ApproveFlag == true
                                                     || x.CreateFlag == true
                                                     || x.DeleteFlag == true
                                                     || x.DetailFlag == true
                                                     || x.EditFlag == true)
                                                     ).Count();
                    if (gotChildCount >= 1)
                    {
                        tvimMenu.checkedByDefault = true;
                    }


                    List<ModuleActionModel> matchList = new List<ModuleActionModel>();

                    matchList = (from b in uncheckList
                                 where b.ParentID == c.ModuleID && b.isChecked != true
                                 select b).ToList();
                    matchList.ToList().ForEach(d => d.isChecked = true);

                    if (c.Children == null)
                    {
                        c.Children = new List<ModuleActionModel>();
                    }
                    c.Children.AddRange(matchList.ToList());
                    moduleCount = c.Children.Count();
                    moduleTrueCount = 0;

                    if (c.Children.Count > 0)
                    {
                        foreach (var mm in matchList)
                        {
                            if (tvimMenu.nodes == null)
                            {
                                tvimMenu.nodes = new List<TreeViewItemModel>();
                            }//end if

                            TreeViewItemModel tvimmodule = new TreeViewItemModel();

                            tvimmodule.text = mm.ModuleName;
                            tvimmodule.nodeId = mm.ModuleID.ToString();
                            tvimmodule.nodeCode = mm.ModuleID.ToString();
                            tvimmodule.parentId = tvimMenu.nodeId;
                            tvimmodule.type = "S";
                            tvimmodule.selectable = true;
                            tvimmodule.color = "#000000";
                            tvimmodule.backColor = "#e8e6ff";
                            tvimmodule.icon = "glyphicon";
                            //tvimMenu.nodes.Add(tvimmodule);


                            var gotmoduleChildCount = uga.Where(x => x.ModuleID == mm.ModuleID && (x.MenuFlag == true
                                                    || x.ApproveFlag == true
                                                    || x.CreateFlag == true
                                                    || x.DeleteFlag == true
                                                    || x.DetailFlag == true
                                                    || x.EditFlag == true)
                                                    ).Count();
                            if (gotmoduleChildCount >= 1)
                            {
                                tvimmodule.checkedByDefault = true;
                            }

                            var MenuDetailModel = moduleActionSUList.Where(x => x.ModuleName == mm.ModuleName)
                                                                 .ToList();


                            menuDetailCount = 0;
                            menuDetailTrueCount = 0;
                            foreach (var d in MenuDetailModel)
                            {
                                if (tvimmodule.nodes == null)
                                {
                                    tvimmodule.nodes = new List<TreeViewItemModel>();
                                }//end if

                                AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.MenuFlag, ref menuDetailCount,ref menuDetailTrueCount, d, "Menu View", "DM");
                                AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.CreateFlag, ref menuDetailCount, ref menuDetailTrueCount, d, "Creating", "DC");
                                AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.EditFlag, ref menuDetailCount, ref menuDetailTrueCount, d, "Editing", "DE");
                                //AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.DeleteFlag, ref menuDetailCount,ref menuDetailTrueCount, d, "Deleting", "DD");
                                AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.DetailFlag, ref menuDetailCount, ref menuDetailTrueCount, d, "Details", "DT");
                                //AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.ApproveFlag, ref menuDetailCount,ref menuDetailTrueCount, d, "Approving", "DA");

                            }//end foreach

                            //tvimmodule.tags = new string[] { (menuDetailCount).ToString() };
                            tvimmodule.tags = new string[] { (menuDetailTrueCount).ToString() + "/" + (menuDetailCount).ToString() };
                            tvimMenu.nodes.Add(tvimmodule);

                        }//foreach                   

                        if (tvimMenu.nodes.Where(x=>x.checkedByDefault==true).Count() >= 1)
                        {
                            tvimMenu.checkedByDefault = true;
                            moduleTrueCount = tvimMenu.nodes.Where(x => x.checkedByDefault == true).Count();
                        }
                    }
                    else
                    {
                        if (tvimMenu.nodes == null)
                        {
                            tvimMenu.nodes = new List<TreeViewItemModel>();
                        }//end if

                        var MenuModel = moduleActionSUList.Where(x => x.ModuleName == c.ModuleName)
                                                                .FirstOrDefault();
                        
                        if (MenuModel!=null)
                        {                 

                            AssignUserGroupAccessListDetail(tvimMenu, MenuModel.ModuleID, uga, MenuModel.MenuFlag, ref moduleCount,ref moduleTrueCount, MenuModel, "Menu View", "DM");
                            AssignUserGroupAccessListDetail(tvimMenu, MenuModel.ModuleID, uga, MenuModel.CreateFlag, ref moduleCount, ref moduleTrueCount, MenuModel, "Creating", "DC");
                            AssignUserGroupAccessListDetail(tvimMenu, MenuModel.ModuleID, uga, MenuModel.EditFlag, ref moduleCount, ref moduleTrueCount, MenuModel, "Editing", "DE");
                            //AssignUserGroupAccessListDetail(tvimMenu, c.ModuleID, uga, c.DeleteFlag, ref menuDetailCount, moduleCount, "Deleting", "DD");
                            AssignUserGroupAccessListDetail(tvimMenu, MenuModel.ModuleID, uga, MenuModel.DetailFlag, ref moduleCount, ref moduleTrueCount, MenuModel, "Details", "DT");
                            //AssignUserGroupAccessListDetail(tvimMenu, c.ModuleID, uga, c.ApproveFlag, ref menuDetailCount,moduleCount, "Approving", "DA");

                        }//end if

                    }//end if-else

                    tvimMenu.tags = new string[] { (moduleTrueCount).ToString() + "/" + (moduleCount).ToString() };
                    tvtmList.Add(tvimMenu);
                    //

                    ////var groupList = moduleActionSUList.GroupBy(x => x.ModuleActionType)
                    ////                                .Select(o => new { ModuleActionType = o.Key });

                    ////foreach (var c in groupList)
                    ////{
                    ////    TreeViewItemModel tvimMenu = new TreeViewItemModel();
                    ////    tvimMenu.text = c.ModuleActionType;
                    ////    tvimMenu.nodeId = c.ModuleActionType;
                    ////    tvimMenu.nodeCode = "0";
                    ////    tvimMenu.parentId = "0";
                    ////    tvimMenu.type = "M";
                    ////    tvimMenu.selectable = true;


                    ////    tvimMenu.color = "#000000";
                    ////    tvimMenu.backColor = "#9589FF";
                    ////    tvimMenu.icon = "glyphicon";

                    ////    var gotChildCount = uga.Where(x =>  x.MenuFlag == true
                    ////                                     || x.ApproveFlag == true
                    ////                                     || x.CreateFlag == true
                    ////                                     || x.DeleteFlag == true
                    ////                                     || x.DetailFlag == true
                    ////                                     || x.EditFlag == true
                    ////                                     ).Count();
                    ////    if (gotChildCount >= 1)
                    ////    {
                    ////        tvimMenu.checkedByDefault = true;
                    ////    }





                    ////    var groupModuleList = moduleActionSUList.Where(x => x.ModuleActionType==c.ModuleActionType)                                                
                    ////                                 .Select(o => new { ModuleName = o.ModuleName }).ToList();
                    ////    moduleCount = groupModuleList.Count();
                    ////    foreach (var mm in groupModuleList)
                    ////    {
                    ////        if (tvimMenu.nodes == null)
                    ////        {
                    ////            tvimMenu.nodes = new List<TreeViewItemModel>();
                    ////        }//end if

                    ////        TreeViewItemModel tvimmodule = new TreeViewItemModel();

                    ////        tvimmodule.text = mm.ModuleName;
                    ////        tvimmodule.nodeId = mm.ModuleName.ToString();
                    ////        tvimmodule.nodeCode = "0";
                    ////        tvimmodule.parentId = tvimMenu.nodeId;
                    ////        tvimmodule.type = "S";
                    ////        tvimmodule.selectable = true;
                    ////        tvimmodule.color = "#000000";
                    ////        tvimmodule.backColor = "#ADA4FF";
                    ////        tvimmodule.icon = "glyphicon";
                    ////        //tvimMenu.nodes.Add(tvimmodule);

                    ////        if (gotChildCount >= 1)
                    ////        {
                    ////            tvimmodule.checkedByDefault = true;
                    ////        }

                    ////        var MenuDetailModel = moduleActionSUList.Where(x => x.ModuleName == mm.ModuleName)
                    ////                                             .ToList();


                    ////        menuDetailCount = 0;
                    ////        foreach (var d in MenuDetailModel)
                    ////        {
                    ////            if (tvimmodule.nodes == null)
                    ////            {
                    ////                tvimmodule.nodes = new List<TreeViewItemModel>();
                    ////            }//end if

                    ////            AssignUserGroupAccessListDetail(tvimmodule,d.ModuleID, uga, d.MenuFlag,ref menuDetailCount, d, "Menu View","DM");
                    ////            AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.CreateFlag, ref menuDetailCount, d, "Creating", "DC");
                    ////            AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.EditFlag, ref menuDetailCount, d, "Editing", "DE");
                    ////            AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.DeleteFlag, ref menuDetailCount, d, "Deleting", "DD");
                    ////            AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.DetailFlag, ref menuDetailCount, d, "Details", "DT");
                    ////            AssignUserGroupAccessListDetail(tvimmodule, d.ModuleID, uga, d.ApproveFlag, ref menuDetailCount, d, "Approving", "DA");

                    ////        }//end foreach


                    ////        tvimmodule.tags = new string[] { (menuDetailCount).ToString() };
                    ////        tvimMenu.nodes.Add(tvimmodule);



                    ////    }//end foreach


                    ////    tvimMenu.tags = new string[] { (moduleCount).ToString() };
                    ////    tvtmList.Add(tvimMenu);



                }//end foreach

            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
            finally { }



        }

        private static void AssignUserGroupAccessListDetail(TreeViewItemModel tvimmodule,int ModuleID,List<UserGroupAccess> uga, bool flag,ref int recordsCount, ref int recordsTrueCount, ModuleActionModel d,string DisplayName,string type)
        {
            int count = 0;

            switch (type)
            {
                case "DM":
                    count = uga.Where(x => x.MenuFlag == true && x.ModuleID==ModuleID).Count();
                    break;
                case "DC":
                    count = uga.Where(x => x.CreateFlag == true && x.ModuleID == ModuleID).Count();
                    break;
                case "DE":
                    count = uga.Where(x => x.EditFlag == true && x.ModuleID == ModuleID).Count();
                    break;
                case "DD":
                    count = uga.Where(x => x.DeleteFlag == true && x.ModuleID == ModuleID).Count();
                    break;
                case "DT":
                    count = uga.Where(x => x.DetailFlag == true && x.ModuleID == ModuleID).Count();
                    break;
                case "DA":
                    count = uga.Where(x => x.ApproveFlag == true && x.ModuleID == ModuleID).Count();
                    break;
                default:
                    break;
            }//end switch   
            

            if (flag)
            {
                TreeViewItemModel tvimDetail = new TreeViewItemModel();

                if (count >= 1)
                {
                    tvimDetail.checkedByDefault = true;
                    recordsTrueCount = recordsTrueCount + 1;
                }

                tvimDetail.text = DisplayName;// "Menu View";
                tvimDetail.nodeId = d.ModuleID.ToString();
                tvimDetail.nodeCode = d.ModuleID.ToString();
                tvimDetail.parentId = tvimmodule.nodeId;
                tvimDetail.type = type;
                tvimDetail.selectable = true;
                tvimDetail.color = "#000000";
                tvimDetail.backColor = "#CCC6FF";
                tvimDetail.icon = "glyphicon";
                tvimmodule.nodes.Add(tvimDetail);
                recordsCount = recordsCount + 1;
            }//end if
        }

        public async Task<bool> SetUserGroupAccessAsync(TreeViewSelectItemsModel tvsims, ModelStateDictionary modelState)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                //using (var uow = this._uowFactory.Create())
                //{
                    var uga = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == tvsims.userName);
                    _uow.Repository<UserGroupAccess>().DeleteAll(uga);
                    var insertRepo = _uow.Repository<UserGroupAccess>();

                    //var groupModuleList = tvsims.nodes.Where(x=>x.nodeparentId == "0").GroupBy(x => x.nodeCode)
                    //                             .Select(o => new { ModuleID = o.Key }).ToList();
                    var groupModuleList = tvsims.nodes.GroupBy(x => x.nodeCode)
                                                .Select(o => new { ModuleID = o.Key }).ToList();
                    foreach (var group in groupModuleList)
                    {
                        UserGroupAccess insertRow = new UserGroupAccess();
                        insertRow.GroupCode = tvsims.userName;
                        insertRow.ModuleID = group.ModuleID.intParse();

                        if (tvsims.nodes!=null && tvsims.nodes.Count()>0)
                        {
                            foreach (TreeViewSelectItemModel tvsim in tvsims.nodes.Where(x => x.nodeCode == group.ModuleID))
                            {
                                switch (tvsim.nodeType)
                                {
                                    //case "M":
                                    //    insertRow.MenuFlag = true;
                                    //    break;
                                    case "DM":
                                        insertRow.MenuFlag = true;
                                        break;
                                    case "DC":
                                        insertRow.CreateFlag = true;
                                        break;
                                    case "DE":
                                        insertRow.EditFlag = true;
                                        break;
                                    case "DD":
                                        insertRow.DeleteFlag = true;
                                        break;
                                    case "DT":
                                        insertRow.DetailFlag = true;
                                        break;
                                    case "DA":
                                        insertRow.ApproveFlag = true;
                                        break;
                                    default:
                                        break;
                                }//end switch   
                            }//end foreach
                        }
                        else
                        {

                        }//end if-else

                      
                        insertRepo.Insert(insertRow);
                    }//end foreach
                    isError = await _uow.SaveAsync();
                ////}//end using
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

        #endregion
    }//end class
}//end namespace
