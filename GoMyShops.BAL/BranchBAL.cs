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
    public interface IBranchBAL
    {
        List<BranchListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total);
        BranchDetailsViewModels getDetail(string id, string id2);
        bool Create(BranchDetailsViewModels model, ModelStateDictionary modelState);
        bool Edit(BranchDetailsViewModels model, ModelStateDictionary modelState);
        bool Deactived(List<BranchListViewModels> model);
        List<SelectListItem> GetBranchList(string DistCode);
        List<SelectListItem> GetBranchList();
    }

    public class BranchBAL: BaseBAL, IBranchBAL
    {
        #region Definations
        private readonly ILogger<BranchBAL> _logger;
        //private UrlHelper _urlHelper;
        IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public BranchBAL(IHttpContextAccessor httpContextAccessor, ILogger<BranchBAL> logger, IUnitOfWorkFactory uowFactory) : base()
        {
            _uow = uowFactory.Create();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;         
        }
        #endregion
        #region Public Functions
        public List<BranchListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total)
        {

            var sessionclass = new SearchCriteria();
            try
            {
                sessionclass.srcBranchCode = param1;
                sessionclass.srcBranchName = param2;
                sessionclass.srcType = param3;
                sessionclass.srcStatus = param4;

                //var CodeAccess = System.Web.HttpContext.Current.Session["CodeAccess"] as UserAccessLevelsModels;
                UserName = _httpContextAccessor.HttpContext.User.Identity.Name;

                var branchList = new List<Branch>();
                var queryableDistributorList = (from branch in _uow.Repository<Branch>().GetAsQueryable()
                                                join userbranchLevel in _uow.Repository<UserAccessLevel>().GetAsQueryable().Where(x => x.Username == UserName) on branch.BranchCode equals userbranchLevel.BranchCode //into ual
                                                select branch
                                               )
                    .WhereIf(!sessionclass.srcBranchCode.IsNullOrEmptyString(), tags => tags.DistributorCode.Contains(sessionclass.srcBranchCode))
                    .WhereIf(!sessionclass.srcBranchName.IsNullOrEmptyString(), tags => tags.Name.Contains(sessionclass.srcBranchName))
                    .WhereIf(!sessionclass.srcStatus.IsNullOrEmptyString(), tags => tags.Status == sessionclass.srcStatus)
                    .WhereIf(!sessionclass.srcType.IsNullOrEmptyString(), tags => tags.Type == sessionclass.srcType)
                    ;



                if (!sort.IsNullOrEmptyString())
                {
                    branchList = CustomExpression.IQueryable<Branch>(queryableDistributorList, sort, "tags", order)
                             .Skip((offset / limit) * limit).Take(limit)
                             .ToList();
                }
                else
                {
                    branchList = queryableDistributorList//.AsQueryable()
                                      .OrderBy(tags => tags.BranchCode)
                                      .Skip((offset / limit) * limit).Take(limit)
                                      .ToList();

                }//end if-else

                total = queryableDistributorList//.AsQueryable()
                        .Count();



                List<BranchListViewModels> b = (from a in branchList
                                                     select new BranchListViewModels
                                                     {
                                                         IdKey = a.CompanyCode + a.DistributorCode,
                                                         DistributorCode = a.DistributorCode,
                                                         BranchCode =a.BranchCode,
                                                         Name = a.Name,
                                                         Status = a.Status,
                                                         DetailJson = new ActionsListDetails(a.DistributorCode, a.BranchCode, "", "", ""),
                                                         EditJson = new ActionsListDetails(a.DistributorCode, a.BranchCode, "", "", ""),
                                                        
                                                     }).ToList();
                return b;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return new List<BranchListViewModels>();

        }

        public BranchDetailsViewModels getDetail(string id, string id2)
        {
            BranchDetailsViewModels model = null;
            try
            {
                var statusRepo=_uow.Repository<StatusSU>();

                model = (from B in _uow.Repository<Branch>().GetAsQueryable()
                                       .Where(x => x.CompanyCode == "1" && x.DistributorCode == id && x.BranchCode==id2)
                         join company in _uow.Repository<Company>().GetAsQueryable() on B.CompanyCode equals company.CompanyCode into C
                         from companyLOJ in C.DefaultIfEmpty()
                         join distributor in _uow.Repository<Distributor>().GetAsQueryable() on B.DistributorCode equals distributor.DistributorCode into dist
                         from distributorLOJ in dist.DefaultIfEmpty()
                         let status = statusRepo.dbSet.FirstOrDefault(p2 => B.Status == p2.Status )
                         select new BranchDetailsViewModels(_httpContextAccessor)
                         {
                             Company = companyLOJ.Name,
                             CompCode = companyLOJ.CompanyCode,
                             Add1 = B.Address1,
                             Add2 = B.Address2,
                             Add3 = B.Address3,
                             City = B.City,
                             CountryCode = B.CountryCode,
                             BranchCode=B.BranchCode,
                             BranchName =B.Name,
                             DistCode = B.DistributorCode,
                             DistName = distributorLOJ.Name,
                             GSTRegID = B.GSTRegNo,
                             Email = B.Email,
                             PhoneNo = B.PhoneNo,
                             PostCode = B.PostCode,
                             FaxNo = B.FaxNo,
                             Type = B.Type,
                             StateCode=B.StateCode,  
                             Status = status==null? B.Status : status.StatusName,
                             CheckBoxStatus = B.Status == "1" ? true : false,
                             //company = x,
                             //distributor = D
                         }).FirstOrDefault();

                model.EditJson = JsonConvert.SerializeObject(new ActionsListDetails(model.DistCode, model.BranchCode, "", "", ""));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return model.IsNullThenNew(_httpContextAccessor);
        }

        public bool Create(BranchDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<Branch>().Get(filter: x => x.DistributorCode == model.DistCode && x.BranchCode==model.BranchCode).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("BranchCode", "Branch Code existed");
                }//end if

                if (modelState.IsValid)
                {

                    Branch insertRow = new Branch();
                    var insertRepo = _uow.Repository<Branch>();

                    insertRow.CompanyCode = "1";
                    insertRow.DistributorCode = model.DistCode;
                    insertRow.BranchCode = model.BranchCode;
                    insertRow.Name = model.BranchName;
                    insertRow.GSTRegNo = model.GSTRegID;
                    insertRow.Name = model.BranchName;
                    insertRow.Address1 = model.Add1;
                    insertRow.Address2 = model.Add2;
                    insertRow.Address3 = model.Add3;
                    insertRow.CountryCode = model.CountryCode;
                    insertRow.City = model.City;
                    insertRow.PostCode = model.PostCode;
                    insertRow.StateCode = model.StateCode;
                    insertRow.PhoneNo = model.PhoneNo;
                    insertRow.FaxNo = model.FaxNo;
                    insertRow.Email = model.Email;
                    insertRow.Status = "1";

                    insertRow.CreatedBy = "admin";//HttpContext.Current.User.Identity.Name;
                    insertRow.CreatedTime = DateTime.Now;

                    insertRepo.Insert(insertRow);

                    isError= _uow.Save();

                }

            }
            catch (Exception ex)
            {
                isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return isError;
        }

        public bool Edit(BranchDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                var infos = _uow.Repository<Branch>().GetAsQueryable(filter: x =>
                                     //x.CompanyCode == CompanyCode
                                     x.DistributorCode == model.DistCode
                                     && x.BranchCode==model.BranchCode
                                    ).FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    var entry = _uow.Context.Entry<Branch>(infos);
                    entry.Property(u => u.GSTRegNo).CurrentValue = model.GSTRegID;
                    entry.Property(u => u.Name).CurrentValue = model.BranchName;
                    entry.Property(u => u.Address1).CurrentValue = model.Add1;
                    entry.Property(u => u.Address2).CurrentValue = model.Add2;
                    entry.Property(u => u.Address3).CurrentValue = model.Add3;
                    entry.Property(u => u.CountryCode).CurrentValue = model.CountryCode;
                    entry.Property(u => u.City).CurrentValue = model.City;
                    entry.Property(u => u.PostCode).CurrentValue = model.PostCode;
                    entry.Property(u => u.StateCode).CurrentValue = model.StateCode;
                    entry.Property(u => u.PhoneNo).CurrentValue = model.PhoneNo;
                    entry.Property(u => u.FaxNo).CurrentValue = model.FaxNo;
                    entry.Property(u => u.Email).CurrentValue = model.Email;
                    entry.Property(u => u.ModifiedBy).CurrentValue = "admin";
                    entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                    entry.Property(u => u.Status).CurrentValue = CommonFunctions.Iif(model.CheckBoxStatus == true, CommonSetting.Status.Active, CommonSetting.Status.Inactive);
                    isError= _uow.Save();
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

        public bool Deactived(List<BranchListViewModels> model)
        {
            try
            {
                if (!model.IsNullOrEmpty())
                {
                    foreach (var b in model)
                    {
                        var infos = _uow.Repository<Branch>().GetAsQueryable(filter: x =>
                                     //x.CompanyCode == CompanyCode
                                     x.DistributorCode == b.DistributorCode
                                     && x.BranchCode == b.BranchCode
                                    && x.Status == "1").FirstOrDefault();
                        if (!infos.IsNullOrEmpty())
                        {
                            var entry = _uow.Context.Entry<Branch>(infos);
                            entry.Property(u => u.ModifiedBy).CurrentValue = "admin";
                            entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                            entry.Property(u => u.Status).CurrentValue = CommonSetting.Status.Inactive;

                            isError= _uow.Save();
                        }//end if
                    }//end foreach
                }//end if
            }
            catch (System.Exception ex)
            {
                isError = true;
                _logger.LogError("Error", ex);
                return true;
            }
            return isError;

        }

        public List<SelectListItem> GetBranchList(string DistCode)
        {
            List<SelectListItem> infos = null;
            try
            {
                infos = _uow.Repository<Branch>().GetAsQueryable()
                                                .Where(r=>r.Status==CommonSetting.Status.Active)
                                                .Select(r => new SelectListItem()
                                                {
                                                    Text = r.BranchCode + " - " + r.Name,
                                                    Value = r.BranchCode
                                                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetBranchList()
        {
            List<SelectListItem> infos = null;
            //SelectList sl=null;
            try
            {
                
                 infos = _uow.Repository<Branch>().GetAsQueryable()
                                                .Where(r => r.Status == CommonSetting.Status.Active)
                                                .Select(r => new SelectListItem()
                                                {
                                                    Text = r.BranchCode + " - " + r.Name,
                                                    Value = r.BranchCode
                                                }).ToList();
                //sl = infos.NewSelectList();
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
