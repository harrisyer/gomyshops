using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// System.Web.Mvc;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace GoMyShops.BAL
{
    public interface ILoginBAL
    {
       // bool SetAuditHeader(string user);
       // bool SetAuditHeaderTimeOut();
        List<SelectListItem> GetSecuritySetupList();
        List<LoginSetupModel> GetSecuritySetupDetails(int SecurityID);
        bool SetAuditHeader(string user,int type=0);
        bool SetAuditHeaderTimeOut();
    }

    public class LoginBAL : ILoginBAL
    {
        private readonly ILogger<LoginBAL> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        IUnitOfWork _uow1;
        public LoginBAL(IUnitOfWorkFactory uowFactory, IHttpContextAccessor httpContextAccessor, ILogger<LoginBAL> logger)
        {
            _uow = uowFactory.Create();
            _uow1= uowFactory.Create();
            _uowFactory = uowFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }


        public List<SelectListItem> GetSecuritySetupList()
        {
            List<SelectListItem> infos=null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                   infos = _uow.Repository<LoginSU>().GetAsQueryable()
                                                       .AsNoTracking()
                                                       .Select(r => new SelectListItem()
                                                        {
                                                            Text =r.SecurityId.ToString() + " - " + r.SecurityName,
                                                            Value = r.SecurityId.ToString()
                                                        }).ToList();
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<LoginSetupModel> GetSecuritySetupDetails(int SecurityID)
        {
            List<LoginSetupModel> infos = null;// new List<LoginSetupModel>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<LoginSU>().GetAsQueryable(filter: x => x.SecurityId == SecurityID)
                                                        .Select(r => new LoginSetupModel()
                                                        {
                                                            SecurityId = r.SecurityId,
                                                            SecurityName = r.SecurityName,
                                                            AllowOnlyAlphanumericUserNames = r.AllowOnlyAlphanumericUserNames,
                                                            RequireUniqueEmail = r.RequireUniqueEmail,
                                                            RequiredLength = r.RequiredLength,
                                                            RequireNonLetterOrDigit = r.RequireNonLetterOrDigit,
                                                            RequireDigit = r.RequireDigit,
                                                            RequireLowercase = r.RequireLowercase,
                                                            RequireUppercase = r.RequireUppercase,
                                                            UserLockoutEnabledByDefault = r.UserLockoutEnabledByDefault,
                                                            DefaultAccountLockoutTimeSpan = r.DefaultAccountLockoutTimeSpan,
                                                            MaxFailedAccessAttemptsBeforeLockout = r.MaxFailedAccessAttemptsBeforeLockout,
                                                            RequireApproved = r.RequireApproved,
                                                            RequireChangePasswordInPeriod = r.RequireChangePasswordInPeriod,
                                                            ChangePasswordInPeriodTimeSpan = r.ChangePasswordInPeriodTimeSpan,
                                                            RequireFirstTimeChangePassword = r.RequireFirstTimeChangePassword
                                                        }).ToList();
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public bool SetAuditHeader(string user,int type=0)
        {
            //UnitOfWork uow = new UnitOfWork();
            try
            {
                var StartTime = DateTime.Now;
                var EndTime = BAL.CommonFunctionsBAL.getDefaultDate();
                if (type==1)
                {
                    EndTime = DateTime.Now;
                }//end if

                HttpRequest request = _httpContextAccessor.HttpContext.Request;
                var userAgent = request.Headers["User-Agent"];
                ClientBrowser _browser = new ClientBrowser(userAgent);

             
                 AuditMaster audit = new AuditMaster()
                {

                    IPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),   // request.Headers["HTTP_X_FORWARDED_FOR"] == "" ? "" : request.Host.ToUriComponent(),
                    StartTime = StartTime,
                    BrowserID = _browser.Version,
                    BrowserType = _browser.Name,
                    EndTime = EndTime,
                    Type = type,
                    //URLAccessed = request.RawUrl,
                    // AreaAccessed="",
                    //UserName = (request.IsAuthenticated) ? User.Identity.Name : "Anonymous",
                    UserName = user
                };
                //using (var uow1 = this._uowFactory.Create())
                //{

                    _uow1.Repository<AuditMaster>().Insert(audit);
                    _uow1.Save();
                _httpContextAccessor.HttpContext.Session.SetString("AuditKey", audit.AuditID.ToString());
                    //context0.SaveChanges();
                    //using (var trans = context0.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))//System.Data.IsolationLevel.Serializable
                    //{

                    //}//end using trans
                //}//end 

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return false;
        }

        public bool SetAuditHeaderTimeOut()
        {
            //UnitOfWork uow = new UnitOfWork();
            int AuditKey = 0;
            try
            {
                HttpRequest request = _httpContextAccessor.HttpContext.Request;

                if (_httpContextAccessor.HttpContext.Session.GetString("AuditKey") != null)
                {
                    AuditKey = CommonFunctions.intParse(CommonFunctions.Iif(_httpContextAccessor.HttpContext.Session.GetString("AuditKey") == "", "", _httpContextAccessor.HttpContext.Session.GetString("AuditKey")).ToString());
                    DateTime EndDateTime = BAL.CommonFunctionsBAL.getDefaultDate();
                    EndDateTime = EndDateTime.AddMinutes(1);

                    var query = (from audit in _uow1.Repository<AuditMaster>().Get(filter: x => x.AuditID == AuditKey && x.EndTime <= EndDateTime)
                                 select audit
                                ).ToList();
                    query.ForEach(a => {
                        a.EndTime = DateTime.Now;
                    });
                    _uow1.Save();
                }

                
                  

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return false;
        }

        public void Dispose()
        {

        }
    }//end class
}//end namespace
