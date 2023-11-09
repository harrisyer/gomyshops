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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace GoMyShops.BAL
{
    public interface IDistributorBAL
    {
        List<DistributorListViewModels> getData(int offset, int limit, string search, string sort, string order,string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total);
        DistributorDetailsViewModels getDetail(string id, string id2);
        bool Create(DistributorDetailsViewModels model, ModelStateDictionary modelState);
        bool Edit(DistributorDetailsViewModels model, ModelStateDictionary modelState);
        bool Deactived(List<DistributorListViewModels> model);
        List<SelectListItem> GetDistributorList(string CompanyCode);
    }

    public class DistributorBAL: IDistributorBAL
    {
        #region Definations
        private readonly ILogger<DistributorBAL> _logger;
        //private UrlHelper _urlHelper;
        IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public DistributorBAL(IHttpContextAccessor httpContextAccessor, ILogger<DistributorBAL> logger, IUnitOfWorkFactory uowFactory)
        {
            _uow = uowFactory.Create();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion

        public List<DistributorListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total)
        {
          
            var sessionclass = new SearchCriteria();
            try
            {
                sessionclass.srcDistributorCode = param1;
                sessionclass.srcDistributorName = param2;
                sessionclass.srcType = param3;
                sessionclass.srcStatus = param4;
                var pageNumber = (offset / limit) + 1;              
                _httpContextAccessor.HttpContext.Session.SetString(CommonSetting.SessionId.ListPageNumberSessionID, pageNumber.ToString());
                //var sessionValue = System.Web.HttpContext.Current.Session[CommonSetting.menuPageSessionId.Distributor] as Dictionary<string, string>;
                //if (!CommonFunctions.IsNullOrEmpty(sessionValue))
                //{
                //    if (!CommonFunctions.IsDictionaryEmptyValues(sessionValue))
                //    {
                //        sessionclass = CommonFunctions.GetObject<SearchCriteria>(sessionValue);
                //    }//end if
                //}//end if

                // var CodeAccess = System.Web.HttpContext.Current.Session["CodeAccess"] as CodeAccess;

                var DistributorList = new List<Distributor>();
                var queryableDistributorList = _uow.Repository<Distributor>().GetAsQueryable()
                    .WhereIf(!sessionclass.srcDistributorCode.IsNullOrEmptyString(), tags => tags.DistributorCode.Contains(sessionclass.srcDistributorCode))
                    .WhereIf(!sessionclass.srcDistributorName.IsNullOrEmptyString(), tags => tags.Name.Contains(sessionclass.srcDistributorName))
                    .WhereIf(!sessionclass.srcStatus.IsNullOrEmptyString(), tags => tags.Status == sessionclass.srcStatus)
                    .WhereIf(!sessionclass.srcType.IsNullOrEmptyString(), tags => tags.Type == sessionclass.srcType)
                    //.WhereIf(!CodeAccess.CompanyCodeAccess.IsNullOrEmpty(), tags => CodeAccess.CompanyCodeAccess.Contains(tags.CompanyCode))
                    //.WhereIf(!CodeAccess.DistributorCodeAccess.IsNullOrEmpty(), tags => CodeAccess.DistributorCodeAccess.Contains(tags.DistributorCode))
                    ;

               

                if (!sort.IsNullOrEmptyString())
                {
                    DistributorList = CustomExpression.IQueryable<Distributor>(queryableDistributorList, sort, "tags", order)
                             .Skip((offset / limit) * limit).Take(limit)
                             .ToList();
                }
                else
                {
                    DistributorList = queryableDistributorList//.AsQueryable()
                                      .OrderBy(tags => tags.DistributorCode)
                                      .Skip((offset / limit) * limit).Take(limit)
                                      .ToList();

                }//end if-else

                total = queryableDistributorList//.AsQueryable()
                        .Count();

               

                List<DistributorListViewModels> b = (from a in DistributorList
                                                     select new DistributorListViewModels
                                                     {
                                                         IdKey = a.CompanyCode + a.DistributorCode,
                                                         DistributorCode = a.DistributorCode,
                                                         Name = a.Name,//a.DistributorCode + " - " + a.Name,
                                                         Status = a.Status,
                                                         DetailJson = new ActionsListDetails("", a.DistributorCode, "", "", ""),
                                                         EditJson = new ActionsListDetails("", a.DistributorCode, "", "", ""),

                                                     }).ToList();
                return b ;
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return new List<DistributorListViewModels>();

        }

        public DistributorDetailsViewModels getDetail(string id, string id2)
        {
            DistributorDetailsViewModels model=null;// = new DistributorViewModels();
            try
            {


                model = (from D in _uow.Repository<Distributor>().GetAsQueryable()
                                       .Where(x => x.CompanyCode == "1" && x.DistributorCode == id2)
                                       join CO in _uow.Repository<Company>().GetAsQueryable() on D.CompanyCode equals CO.CompanyCode into U1
                                       from x in U1.DefaultIfEmpty()
                                       //let country = productRepo.dbSet.FirstOrDefault(p2 => r.CompanyCode == p2.CompanyCode && r.ProductCode == p2.ProductCode)
                                       select new DistributorDetailsViewModels(_httpContextAccessor)
                                       {
                                            Company=x.Name,
                                            CompCode=D.CompanyCode,
                                            Add1=D.Address1,
                                            Add2=D.Address2,
                                            Add3=D.Address3,
                                            City=D.City,
                                            CountryCode=D.CountryCode,
                                            StateCode=D.StateCode,
                                            DistCode=D.DistributorCode,
                                            DistName=D.Name,
                                            GSTRegID=D.GSTRegNo,
                                            Email =D.Email,
                                            PhoneNo=D.PhoneNo,
                                            PostCode =D.PostCode,
                                            FaxNo=D.FaxNo,
                                            Type=D.Type,
                                            Status =D.Status,
                                            CheckBoxStatus = D.Status=="1"? true : false,
                                           //company = x,
                                           //distributor = D
                                       }).FirstOrDefault();

                model.EditJson = JsonConvert.SerializeObject(new ActionsListDetails("", model.DistCode, "", "", ""));
                           }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return model.IsNullThenNew(_httpContextAccessor);
        }

        public bool Create(DistributorDetailsViewModels model,ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<Distributor>().Get(filter: x => x.DistributorCode == model.DistCode).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("DistCode", "Distributor Code existed");
                }//end if

                if (modelState.IsValid)
                {

                    Distributor distributor = new Distributor();
                    var DistRepo = _uow.Repository<Distributor>();

                    //string DistCode = DocSeqHelper.GetUpdateDocCode("DIST", User.Identity.Name, "0", "0"); //model.Branch);
                    distributor.CompanyCode = model.CompCode;
                    distributor.DistributorCode = model.DistCode;
                    distributor.GSTRegNo = model.GSTRegID;
                    distributor.Name = model.DistName;
                    distributor.Address1 = model.Add1;
                    distributor.Address2 = model.Add2;
                    distributor.Address3 = model.Add3;
                    distributor.CountryCode = model.CountryCode;
                    distributor.City = model.City;
                    distributor.PostCode = model.PostCode;
                    distributor.StateCode = model.StateCode;
                    distributor.PhoneNo = model.PhoneNo;
                    distributor.FaxNo = model.FaxNo;
                    distributor.Email = model.Email;
                    distributor.Status = "1";

                    distributor.CreatedBy = "admin";//HttpContext.Current.User.Identity.Name;
                    distributor.CreatedTime = DateTime.Now;

                    DistRepo.Insert(distributor);

                    _uow.Save();

                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return true;
            }
            finally { }
            return false;
        }

        public bool Edit(DistributorDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                var infos = _uow.Repository<Distributor>().GetAsQueryable(filter: x =>
                                     //x.CompanyCode == CompanyCode
                                     x.DistributorCode == model.DistCode
                                    ).FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    var entry = _uow.Context.Entry<Distributor>(infos);
                    entry.Property(u => u.GSTRegNo).CurrentValue = model.GSTRegID;
                    entry.Property(u => u.Name).CurrentValue = model.DistName;
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
                    entry.Property(u => u.Status).CurrentValue =CommonFunctions.Iif(model.CheckBoxStatus==true,CommonSetting.Status.Active, CommonSetting.Status.Inactive);
                    _uow.Save();
                }//end if

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return true;
            }
            finally { }
            return false;
        }

        public bool Deactived(List<DistributorListViewModels> model)
        {
            try            
            {
                if (!model.IsNullOrEmpty())
                {
                    foreach (var b in model)
                    {
                        var infos = _uow.Repository<Distributor>().GetAsQueryable(filter: x =>
                                    //x.CompanyCode == CompanyCode
                                     x.DistributorCode == b.DistributorCode
                                    && x.Status == "1").FirstOrDefault();
                        if (!infos.IsNullOrEmpty())
                        {
                            var entry = _uow.Context.Entry<Distributor>(infos);
                            entry.Property(u => u.ModifiedBy).CurrentValue = "admin";
                            entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                            entry.Property(u => u.Status).CurrentValue = CommonSetting.Status.Inactive;

                            _uow.Save();
                        }//end if
                    }//end foreach
                }//end if
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error", ex);
                return true;
            }
            return false;

        }

        public List<SelectListItem> GetDistributorList(string CompanyCode)
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                infos = _uow.Repository<Distributor>().GetAsQueryable()
                                                .Where(r => r.Status == CommonSetting.Status.Active)
                                                .Select(r => new SelectListItem()
                                                {
                                                    Text = r.DistributorCode + " - " + r.Name,
                                                    Value = r.DistributorCode
                                                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

    }//end class
}//end namespace
