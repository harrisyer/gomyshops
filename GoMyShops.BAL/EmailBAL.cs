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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SimpleInjector;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Microsoft.AspNetCore.Http.Extensions;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security.DataProtection;
namespace GoMyShops.BAL
{
    public interface IEmailFactory
    {
       // IEmailBAL Create();

    }

    public class EmailFactory : IEmailFactory
    {
        #region Definations
        private readonly ILogger<EmailFactory> _logger;
        IUnitOfWork? _uow;
        IServicesBAL _servicesBAL;
        //IUsersBAL _userBAL;
        ILoginBAL _loginBAL;
        //private UrlHelper? _urlHelper;
        IEmailSender _emailSender;
        IUnitOfWorkFactory _uowFactory;
        UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        IWebHostEnvironment _hostingEnvironment;
        #endregion

        public EmailFactory(SimpleInjector.Container container, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<EmailFactory> logger, IUnitOfWorkFactory uowFactory, IWebHostEnvironment hostingEnvironment, IEmailSender emailSender, IServicesBAL servicesBAL, ILoginBAL loginBAL)
        {
            _uowFactory = uowFactory;
            _servicesBAL = servicesBAL;
            //var a = Lifestyle.Singleton.CreateProducer<ILoginBAL, LoginBAL>(container);
            //var b = a.GetInstance();
            //var userBAL = Lifestyle.Singleton.CreateProducer<IUsersBAL, UsersBAL>(container);

            //_userBAL= userBAL.GetInstance();
            // _userBAL = userBAL;
            _loginBAL = loginBAL;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _logger = logger;
        }
        //public IEmailBAL Create()
        //{
        //    return new EmailBAL(_httpContextAccessor, _emailSender, _hostingEnvironment, _uowFactory, _servicesBAL, _userBAL, _loginBAL);
        //}

    }


    public interface IEmailBAL
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
        Task<bool> SendUserEmailAsync(string EmailType, string UserName, string Password = "");
        Task<bool> SendSignUpEmailAsync(string EmailType, string UserName);
        Task<bool> SendOnboardingEmailAsync(string EmailType, string UserName);
        Task<bool> SendRecipientEmailAsync(string EmailType, string RecipientConnectionCode, string UserNameLock, string UserName = "admin");
        Task<bool> SendSaleTransactionEmailAsync(string EmailType, string UserName, string CustomerName,
                                string CustomerAddress, string CustomerPhoneNo, string Product, string TransactionAmount,
                                string TransactionDateTime, string OrderNo, string TransactionID, string PaymentType,
                                string TransactionStatus);
        Task<bool> SendPaymentNotificationUserEmailAsync(string EmailType, string UserName, string EmailAddress,
                                string Product, string TransactionAmount,
                                string TransactionDateTime, string OrderNo, string TransactionID, string PaymentType,
                                string TransactionStatus, string DeclinedReason);
        Task<bool> SendVelocityAlertEmailAsync(string EmailType, string UserName, string EndUserEmailAddress, string EndUserName,
                                string Product, string TransactionAmount,
                                string TransactionDateTime, string OrderNo, string TransactionID, string PaymentType,
                                string TransactionStatus, string EndUserPhone, string VelocityError);
        Task<bool> SendMerchantPayoutAlertEmailAsync(string EmailType, string UserName, string MerchantID, string PayoutDateDuration, string StartDate,
                                string EndDate, string TotalPayout, string TotalSalesAmont, string TotalMdr,
                                string TotalRefund, string TotalDispute, string PaymentTransferMethod
                                );
        Task<bool> SendChargebackTransactionEmailAsync(string EmailType, string UserName, string CustomerName,
                                string TransactionAmount,
                                string TransactionDateTime, string TransactionID);
        Task<bool> SendChargebackUpdateEmailAsync(string EmailType, string UserName, string CustomerName,
                                string TransactionAmount,
                                string TransactionDateTime, string TransactionID);
        Task<bool> SendRefundApprovalEmailAsync(string EmailType, string UserName,string CustomerName,
                                string TransactionAmount,
                                string TransactionDateTime, string TransactionID);
        Task<bool> SendVelocityEmailAsync_AdminAsync(string EmailType, string RecipientConnectionCode, string VelocityError, string ValueList, string MidCode, string ProcessorCode, DateTime ProcessDate, string MidName, string ProcessorName);

        Task<bool> SendLinkPaymentAsync(string EmailType, string DestinationEmail, string MerchantName, string CurrencyCode, string Amount, string OrderDetail, string PaymentUrl);
    }

    public class EmailBAL : BaseBAL, IEmailBAL
    {
        #region Definations
        private readonly ILogger<EmailBAL> _logger;
        IUnitOfWork _uow;
        IServicesBAL _servicesBAL;
        //IUsersBAL _userBAL;
        ILoginBAL _loginBAL;
        private Microsoft.AspNetCore.Mvc.IUrlHelper _urlHelper;
        IUrlHelperFactory _urlHelperFactory;
        IEmailSender _emailSender;
        UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor      
        public EmailBAL(IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, ILogger<EmailBAL> logger, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IWebHostEnvironment hostingEnvironment, IUnitOfWorkFactory uowFactory, IServicesBAL servicesBAL, ILoginBAL loginBAL):base()
        {
            _uow = uowFactory.Create();
            _servicesBAL = servicesBAL;
            //_userBAL = userBAL;
            _loginBAL = loginBAL;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _userManager = userManager;
            _logger = logger;

            if (hostingEnvironment.ApplicationName != null)
            {
                if (httpContextAccessor.HttpContext != null)
                {
                    var actionContext = new ActionContext(httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor());

                    _urlHelper = _urlHelperFactory.GetUrlHelper(actionContext);
                }//end if
            }
            else
            {
                //is windows app
            }
        }

        #endregion
        #region Public Functions
        public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                await _emailSender.SendEmailAsync(email, subject, htmlMessage);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
          
        }
        public async Task<bool> SendUserEmailAsync(string EmailType,string UserName,string Password="")
        {
            try
            {    
                string intUsergroup = GetUserGroup(UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = _userManager;//.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                string CallbackUrl = "";
                if (EmailType == CommonSetting.EmailSendType.SuccessRegister)
                {
                    string code = await manager.GeneratePasswordResetTokenAsync(user);
                    code = System.Web.HttpUtility.UrlEncode(code);
                    CallbackUrl = _urlHelper.Action("LoginResetPassword", "Account", new { userName = UserName, code = code }, protocol: "https");
                }//end if

                if (EmailType == CommonSetting.EmailSendType.SuccessTempPassword ||
                    EmailType == CommonSetting.EmailSendType.SuccessCreatePassword)
                {
                    //string code = await manager.GeneratePasswordResetTokenAsync(user.Id);
                    //code = System.Web.HttpUtility.UrlEncode(code);
                    CallbackUrl = _urlHelper.Action("Login", "Account", new { userName = UserName }, protocol: "https");
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, CallbackUrl, ref Subject, ref Body);

                if (EmailType == CommonSetting.EmailSendType.SuccessTempPassword ||
                    EmailType == CommonSetting.EmailSendType.SuccessCreatePassword)
                {
                    //string password = _uow.Repository<User>().GetAsQueryable()
                    //                   .Where(x => x.Username == UserName).Select(x => x.Password).FirstOrDefault();
                    Body = Body.Replace("@PASSWORD@", Password).stringParse();
                }//end if

                //im.Body = Body;
                //im.Subject = Subject;

                //await manager.EmailService.SendAsync(im);
                // await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);

                return false;
           
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendSignUpEmailAsync(string EmailType, string UserName)
        {
            try
            {
                //string intUsergroup = _userBAL.GetUserGroup(UserName);
                //LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(_userBAL.GetSecurityID(intUsergroup)).FirstOrDefault();
                LoginSetupModel lsm = new LoginSetupModel();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                string CallbackUrl = "";
                if (EmailType == CommonSetting.EmailSendType.SuccessSignUp)
                {
                    string code = await manager.GenerateEmailConfirmationTokenAsync(user);
                    code = System.Web.HttpUtility.UrlEncode(code);
                    CallbackUrl = _urlHelper.Action("SignUpVerify", "Account", new { userName = UserName, code = code }, protocol: "https");
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, CallbackUrl, ref Subject, ref Body);

               // im.Body = Body;
               // im.Subject = Subject;

                // await manager.EmailService.SendAsync(im);
                // await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendOnboardingEmailAsync(string EmailType, string UserName)
        {
            try
            {
                LoginSetupModel lsm = new LoginSetupModel();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if


                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);

               // im.Body = Body;
                //im.Subject = Subject;

                //await manager.EmailService.SendAsync(im);
                 await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                return false;

            }
            catch
            {
                return false;
            }
            finally { }
            //return true;
        }

        public List<EmailList> GetRecipient(string RecipientConnectionCode)
        {
            List<EmailList> data = (from a in _uow.Repository<RecipientConnectionDetail>().GetAsQueryable(x => x.RecipientConnectionCode == RecipientConnectionCode
                            && x.RecipientConnectionType == CommonSetting.RecipientConnectionType.UserGroup)
                        join users in _uow.Repository<User>().GetAsQueryable(x=>x.Status==CommonSetting.Status.Active) on new { GroupCode = a.RecipientCode } equals new { users.GroupCode } //into guser
                                                                                                                                                     //from userLOJ in guser.DefaultIfEmpty()
                        //join aspuser in _uow.Repository<AspNetUser>().GetAsQueryable() on new { UserName = users.Username } equals new { aspuser.UserName }
                        select new EmailList
                        {
                            Email = users.Email,
                            Name = users.Username,
                        }
                           ).Union
                           (
                               from a in _uow.Repository<RecipientConnectionDetail>().GetAsQueryable(x => x.RecipientConnectionCode == RecipientConnectionCode
                                        && x.RecipientConnectionType == CommonSetting.RecipientConnectionType.User)
                               join users in _uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active) on new { Username = a.RecipientCode } equals new { users.Username } //into guser
                                                                                                                                                          //from userLOJ in guser.DefaultIfEmpty()
                              //  join aspuser in _uow.Repository<AspNetUser>().GetAsQueryable() on new { UserName = users.Username } equals new { aspuser.UserName }
                               select new EmailList
                               {
                                   Email = users.Email,
                                   Name = users.Username,
                               }
                           ).Union
                           (
                               from a in _uow.Repository<RecipientConnectionDetail>().GetAsQueryable(x => x.RecipientConnectionCode == RecipientConnectionCode
                                        && x.RecipientConnectionType == CommonSetting.RecipientConnectionType.Recipient)
                               join recipient in _uow.Repository<Recipient>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active) on new { RecipientCode = a.RecipientCode } equals new { recipient.RecipientCode } //into guser
                                select new EmailList
                               {
                                   Email = recipient.Email,
                                   Name = recipient.RecipientLastName + " " + recipient.RecipientLastName,
                               }
                           ).ToList();


            if (data != null)
            {
                return data;
            }
            return new List<EmailList>();
        }


        public async Task<bool> SendRecipientEmailAsync(string EmailType, string RecipientConnectionCode,string UserNameLock, string UserName="admin")
        {
            try
            {
                List<EmailList> data = GetRecipient(RecipientConnectionCode);

                //var auditCode = CurrentUser.AuditKey;

                string intUsergroup = GetUserGroup(UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                //var user = await manager.FindByNameAsync(UserName);
              

                string CallbackUrl = "";
                //if (EmailType == CommonSetting.EmailSendType.UserAccountLock)
                //{
                //}//end if

                string Subject = "";
                string Body = "";
                foreach (var item in data)
                {
                    //IdentityMessage im = new IdentityMessage();
                   // im.Destination = item.Email;
                    Subject = "";
                    Body = "";
                    MappingData(EmailType, item.Name, CallbackUrl, ref Subject, ref Body);
                    MappingRecipientData(EmailType, item.Name, UserNameLock, ref Subject, ref Body);

                    //im.Body = Body;
                    // im.Subject = Subject;

                    await _emailSender.SendEmailAsync(item.Email, Subject, Body);
                    //await manager.EmailService.SendAsync(im);
                }//end foreach

                return false;

            }
            catch 
            {
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendSaleTransactionEmailAsync(string EmailType, string UserName,string CustomerName,
                                string CustomerAddress, string CustomerPhoneNo, string Product, string TransactionAmount,
                                string TransactionDateTime, string OrderNo, string TransactionID, string PaymentType, 
                                string TransactionStatus)
        {
            try
            {
               
                string intUsergroup = GetUserGroup(UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);
                MappingTransactionData( EmailType,  UserName,  CustomerName,"",
                                 CustomerAddress,  CustomerPhoneNo,  Product,  TransactionAmount,
                                 TransactionDateTime,  OrderNo,  TransactionID,  PaymentType,
                                 TransactionStatus, "","", ref  Subject, ref  Body);

                //im.Body = Body;
                // im.Subject = Subject;
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                //await manager.EmailService.SendAsync(im);

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendPaymentNotificationUserEmailAsync(string EmailType, string UserName, string EmailAddress,
                                string Product, string TransactionAmount,
                                string TransactionDateTime, string OrderNo, string TransactionID, string PaymentType,
                                string TransactionStatus, string DeclinedReason)
        {
            try
            {

                //string intUsergroup = _userBAL.GetUserGroup(CurrentUser.Name);
                LoginSetupModel lsm = new LoginSetupModel(); //_loginBAL.GetSecuritySetupDetails(_userBAL.GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                //var user = await manager.FindByNameAsync(UserName);
                //if (user == null)
                //{
                //    return false;
                //}
                //if (user.Email.IsNullOrEmptyString())
                //{
                //    return false;
                //}//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = EmailAddress;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);
                MappingTransactionData(EmailType, UserName, "",
                                 "", "","", Product, TransactionAmount,
                                 TransactionDateTime, OrderNo, TransactionID, PaymentType,
                                 TransactionStatus, "", DeclinedReason, ref Subject, ref Body);

                //im.Body = Body;
                //im.Subject = Subject;
                await _emailSender.SendEmailAsync(EmailAddress, Subject, Body);
                //await manager.EmailService.SendAsync(im);

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendVelocityAlertEmailAsync(string EmailType, string UserName, string EndUserEmailAddress, string EndUserName,
                                string Product, string TransactionAmount,
                                string TransactionDateTime, string OrderNo, string TransactionID, string PaymentType,
                                string TransactionStatus, string EndUserPhone, string VelocityError)
        {
            try
            {

                //string intUsergroup = _userBAL.GetUserGroup(CurrentUser.Name);
                LoginSetupModel lsm = new LoginSetupModel(); //_loginBAL.GetSecuritySetupDetails(_userBAL.GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email; 

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);
                MappingTransactionData(EmailType, UserName, EndUserName,
                                 EndUserEmailAddress, "", EndUserPhone, Product, TransactionAmount,
                                 TransactionDateTime, OrderNo, TransactionID, PaymentType,
                                 TransactionStatus, VelocityError,"", ref Subject, ref Body);

                //im.Body = Body;
                //im.Subject = Subject;
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                //await manager.EmailService.SendAsync(im);

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendMerchantPayoutAlertEmailAsync(string EmailType, string UserName,string MerchantID, string PayoutDateDuration, string StartDate,
                                string EndDate, string TotalPayout,string TotalSalesAmont, string TotalMdr,
                                string TotalRefund, string TotalDispute, string PaymentTransferMethod
                                )
        {
            try
            {

                string intUsergroup = GetUserGroup(CurrentUser.Name);
                LoginSetupModel lsm = new LoginSetupModel(); //_loginBAL.GetSecuritySetupDetails(_userBAL.GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);
                MappingSubjectMerchantPayoutData(MerchantID, PayoutDateDuration, ref Subject, ref Body);
                MappingMerchantPayoutBodyData(StartDate,
                                 EndDate, TotalPayout, TotalSalesAmont,
                                 TotalMdr, TotalRefund, TotalDispute, PaymentTransferMethod,
                                 ref Subject, ref Body);

                // im.Body = Body;
                //im.Subject = Subject;

                // manager.EmailService = new GoMyShops.Models.EmailServiceSettlement();
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                //await manager.EmailService.SendAsync(im);

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendChargebackTransactionEmailAsync(string EmailType, string UserName, string CustomerName,
                                string TransactionAmount,
                                string TransactionDateTime, string TransactionID)
        {
            try
            {

                string intUsergroup = GetUserGroup(UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);
                MappingTransactionData(EmailType, UserName, CustomerName, "",
                                 "", "", "", TransactionAmount,
                                 TransactionDateTime, "", TransactionID, "",
                                 "","","", ref Subject, ref Body);

                // im.Body = Body;
                // im.Subject = Subject;

                //manager.EmailService = new GoMyShops.Models.EmailServiceDispute();
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                //await manager.EmailService.SendAsync(im);

                //send Partner
                //IdentityMessage imPartner = new IdentityMessage();
                //imPartner.Destination = user.Email;
                Subject = "";
                Body = "";

                string PartnerName = _uow.Repository<Customer>().GetAsQueryable(x => x.CustomerName == UserName)
                                   .Select(x => x.PartnerCode).FirstOrDefault();
                LoginSetupModel lsmPartner = new LoginSetupModel(); //_loginBAL.GetSecuritySetupDetails(_userBAL.GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> managerPartner;
                managerPartner = _userManager;// _userBAL.GetUserManager(lsm);

                //var user = await manager.FindByNameAsync(UserName);
                //if (user == null)
                //{
                //    return false;
                //}
                //if (user.Email.IsNullOrEmptyString())
                //{
                //    return false;
                //}//end if


                MappingData(CommonSetting.EmailSendType.ChargebackTransactionPartner, UserName, "", ref Subject, ref Body);
                MappingTransactionData(CommonSetting.EmailSendType.ChargebackTransactionPartner, UserName, CustomerName, "",
                                 "", "", "", TransactionAmount,
                                 TransactionDateTime, "", TransactionID, "",
                                 "","","", ref Subject, ref Body);

                //imPartner.Body = Body;
                //imPartner.Subject = Subject;
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                //managerPartner.EmailService = new GoMyShops.Models.EmailServiceDispute();
                //await managerPartner.EmailService.SendAsync(imPartner);
                //

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendChargebackUpdateEmailAsync(string EmailType, string UserName, string CustomerName,
                                string TransactionAmount,
                                string TransactionDateTime, string TransactionID)
        {
            try
            {

                string intUsergroup = GetUserGroup(UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);
                MappingTransactionData(EmailType, UserName, CustomerName, "",
                                 "", "", "", TransactionAmount,
                                 TransactionDateTime, "", TransactionID, "",
                                 "","","", ref Subject, ref Body);

                //im.Body = Body;
                //im.Subject = Subject;
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                // manager.EmailService = new GoMyShops.Models.EmailServiceDispute();
                // await manager.EmailService.SendAsync(im);

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendRefundApprovalEmailAsync(string EmailType, string UserName, string CustomerName,
                                string TransactionAmount,
                                string TransactionDateTime, string TransactionID)
        {
            try
            {

                string intUsergroup = GetUserGroup(UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                var user = await manager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return false;
                }
                if (user.Email.IsNullOrEmptyString())
                {
                    return false;
                }//end if

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = user.Email;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, UserName, "", ref Subject, ref Body);
                MappingTransactionData(EmailType, UserName, "", "",
                                 "", "", "", TransactionAmount,
                                 TransactionDateTime, "", TransactionID, "",
                                 "","","", ref Subject, ref Body);

                //im.Body = Body;
                //im.Subject = Subject;
                await _emailSender.SendEmailAsync(user.Email, Subject, Body);
                //manager.EmailService = new GoMyShops.Models.EmailServiceRefund();
                //await manager.EmailService.SendAsync(im);

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public async Task<bool> SendVelocityEmailAsync_AdminAsync(string EmailType, string RecipientConnectionCode, string VelocityError, string ValueList, string MidCode, string ProcessorCode, DateTime ProcessDate, string MidName, string ProcessorName)
        {
            try
            {
                string Subject = "";
                string Body = "";

                var emailMaster = _uow.Repository<EmailMaster>().GetAsQueryable().FirstOrDefault();
                var email = _uow.Repository<Email>().GetAsQueryable(x => x.EmailType == EmailType).FirstOrDefault();

                if(emailMaster == null || email == null)
                {
                    Utilites.WriteLog(_logger, "info", "SendVelocityEmailAsync_Admin", "Email Template not found; EmailType " + EmailType);
                    return false;
                }

                List<EmailList> data = new List<EmailList>();

                data = GetRecipient(RecipientConnectionCode);

                if (data.Count > 0)
                {
                    Subject = email.EmailSubject.Replace("@TITLE@", emailMaster.EmailTitle).stringParse();
                    Body = email.EmailBody.Replace("@TITLE@", emailMaster.EmailTitle).stringParse();

                    Body = Body.Replace("@COMPANYURL@", emailMaster.CompanyURL).stringParse();
                    Body = Body.Replace("@COMPANYWEB@", emailMaster.CompanyWebSite).stringParse();

                    if (EmailType == "VAM")
                    {
                        Subject = Subject.Replace("@MidCode@", MidCode + " - " + MidName).stringParse();
                        Body = Body.Replace("@MidCode@", MidCode + " - " + MidName).stringParse();
                        Body = Body.Replace("@VelocityError@", VelocityError).stringParse();
                        Body = Body.Replace("@ProcessDate@", ProcessDate.ToString("dd-MM-yyyy")).stringParse();
                    }
                    else if (EmailType == "VAP")
                    {
                        Subject = Subject.Replace("@Processor@", ProcessorCode + " - " + ProcessorName).stringParse();
                        Body = Body.Replace("@Processor@", ProcessorCode + " - " + ProcessorName).stringParse();
                        Body = Body.Replace("@VelocityError@", VelocityError).stringParse();
                        Body = Body.Replace("@ProcessDate@", ProcessDate.ToString("dd-MM-yyyy")).stringParse();
                    }

                    LoginSetupModel lsm = new LoginSetupModel();
                    UserManager < ApplicationUser> manager;
                    manager = _userManager;// _userBAL.GetUserManager(lsm);

                    foreach (var item in data)
                    {
                        //IdentityMessage im = new IdentityMessage();
                        // im.Destination = item.Email;

                        //im.Body = Body;
                        //im.Subject = Subject;
                        await _emailSender.SendEmailAsync(item.Email, Subject, Body);
                        //await _emailSender.SendEmailAsync(user.Email, Subject, Body);

                        Utilites.WriteLog(_logger, "info", "SendVelocityEmailAsync_Admin", "Email Sent to " + item.Email);
                    }
                }
                else
                {
                    Utilites.WriteLog(_logger, "info", "SendVelocityEmailAsync_Admin", "No Recipient Found;  RecipientConnectionCode: " + RecipientConnectionCode);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            return true;
        }

        public async Task<bool> SendLinkPaymentAsync(string EmailType, string DestinationEmail, string MerchantName, string CurrencyCode, string Amount, string OrderDetail, string PaymentUrl)
        {
            try
            {
                LoginSetupModel lsm = new LoginSetupModel(); //_loginBAL.GetSecuritySetupDetails(_userBAL.GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager < ApplicationUser> manager;
                manager = _userManager;// _userBAL.GetUserManager(lsm);

                //IdentityMessage im = new IdentityMessage();
                //im.Destination = DestinationEmail;

                string Subject = "";
                string Body = "";
                MappingData(EmailType, "", "", ref Subject, ref Body);
                MappingLinkPaymentNotificationBodyData(MerchantName, CurrencyCode, Amount, OrderDetail, PaymentUrl, ref Subject, ref Body);

                //im.Body = Body;
                //im.Subject = Subject;
                await _emailSender.SendEmailAsync(DestinationEmail, Subject, Body);
                //await manager.EmailService.SendAsync(im);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
        }
        #endregion
        #region Private Function
        private string GetUserGroup(string UserName)
        {
            string infos = "";
            IEnumerable<User> us = null;
            try
            {             
                us = _uow.Repository<User>().GetAsQueryable(x => x.Username == UserName);
                if (!us.IsNullOrEmpty())
                {
                    infos = us.FirstOrDefault().GroupCode;
                }//end if
              
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                throw;
            }
            finally { }
            return infos;
        }
        private int GetSecurityID(string UserGroup)
        {
            int infos = 0;
            UserGroup us = null;
            try
            {
                    us = _uow.Repository<UserGroup>().GetAsQueryable(x => x.GroupCode == UserGroup).FirstOrDefault();
                    if (us != null)
                    {
                        infos = us.SecurityId;
                    }//end if
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos;
        }
        private void MappingData(string EmailType,string UserName,string CallbackUrl, ref string Subject,ref string Body)
        {
            try
            {
                var emailMaster = _uow.Repository<EmailMaster>().GetAsQueryable().FirstOrDefault();
                var email = _uow.Repository<Email>().GetAsQueryable(x => x.EmailType == EmailType).FirstOrDefault();
                if (email != null)
                {
                    Subject = email.EmailSubject.Replace("@TITLE@", emailMaster.EmailTitle).stringParse();
                    Body = email.EmailBody.Replace("@TITLE@", emailMaster.EmailTitle).stringParse();
                    Body = Body.Replace("@USERNAME@", UserName).stringParse();
                    Body = Body.Replace("@EXPIREHOUR@", emailMaster.TokenExpireHour.ToString()).stringParse();
                    Body = Body.Replace("@URL@", "<a href='" + CallbackUrl + "'>Click here to login</a>").stringParse();
                    Body = Body.Replace("@COMPANYURL@", "<a href='" + emailMaster.CompanyURL + "'>here</a>").stringParse();
                    Body = Body.Replace("@COMPANYWEB@", emailMaster.CompanyWebSite).stringParse();
                }//end if


                

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return ;
            }
            finally { }
            
        }
        private void MappingRecipientData(string EmailType,string UserNameTitle,string UserNameLock, ref string Subject, ref string Body)
        {
            try
            {
                ////var emailMaster = _uow.Repository<EmailMaster>().GetAsQueryable().FirstOrDefault();
                //var email = _uow.Repository<Email>().GetAsQueryable(x => x.EmailType == EmailType).FirstOrDefault();
                //if (email != null)
                //{
                    //Subject = email.EmailSubject.Replace("@TITLE@", emailMaster.EmailTitle).stringParse();                
                    Body = Body.Replace("@USERNAMELOCK@", UserNameLock).stringParse();

                    if (!_httpContextAccessor.HttpContext.Session.GetString("AuditKey").ToString().IsNullOrEmptyString())
                    {
                        Body = Body.Replace("@AUDITCODE@", _httpContextAccessor.HttpContext.Session.GetString("AuditKey").ToString()).stringParse();
                    }//end if

                //}//end if
            }
            catch (Exception)
            {
                return;
            }
            finally { }

        }
        private void MappingTransactionData(string EmailType, string UserName, string CustomerName,string CustomerEmail,
                                string CustomerAddress, string CustomerPhoneNo, string Product, string TransactionAmount,
                                string TransactionDateTime, string OrderNo, string TransactionID, string PaymentType,
                                string TransactionStatus, string VelocityError, string DeclinedReason, ref string Subject, ref string Body)
        {
            try
            {
                //var email = _uow.Repository<Email>().GetAsQueryable(x => x.EmailType == EmailType).FirstOrDefault();
                //if (email != null)
                //{
                Subject = Subject.Replace("@TransactionID@", TransactionID).stringParse();
                Subject = Subject.Replace("@OrderNo@", OrderNo).stringParse();
                Subject = Subject.Replace("@MerchantName@", UserName).stringParse();
                Body = Body.Replace("@CustomerName@", CustomerName).stringParse();
                Body = Body.Replace("@CustomerAddress@", CustomerAddress).stringParse();
                Body = Body.Replace("@CustomerEmail@", CustomerEmail).stringParse();
                Body = Body.Replace("@CustomerPhoneNo@", CustomerPhoneNo).stringParse();
                Body = Body.Replace("@Product@", Product).stringParse();                   
                Body = Body.Replace("@TransactionAmount@", TransactionAmount).stringParse();
                Body = Body.Replace("@TransactionDateTime@", TransactionDateTime).stringParse();
                Body = Body.Replace("@OrderNo@", OrderNo).stringParse();
                Body = Body.Replace("@TransactionID@", TransactionID).stringParse();
                Body = Body.Replace("@PaymentType@", PaymentType).stringParse();
                Body = Body.Replace("@VelocityError@", VelocityError).stringParse();
                Body = Body.Replace("@DeclinedReason@", DeclinedReason).stringParse();

                if (TransactionStatus.ToUpper() == "SUCCESS")
                {
                    Body = Body.Replace("@TransactionStatus@", "<span style='color: #00ff00;'>" + TransactionStatus + "</span>").stringParse();
                }
                else
                {
                    Body = Body.Replace("@TransactionStatus@", "<span style='color: #ff0000;'>" + TransactionStatus + "</span>").stringParse();
                }//end if-else
                    
                //}//end if 
              
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return;
            }
            finally { }

        }
        private void MappingSubjectMerchantPayoutData(string MerchantID,string PayoutDateDuration, ref string Subject, ref string Body)
        {
            try
            {
         
                Subject = Subject.Replace("@PayoutDateDuration@", PayoutDateDuration).stringParse();
                Subject = Subject.Replace("@MerchantID@", MerchantID).stringParse();         

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return;
            }
            finally { }

        }
        private void MappingMerchantPayoutBodyData(string StartDate,
                                string EndDate, string TotalPayout, string TotalSalesAmont, string TotalMdr,
                                string TotalRefund, string TotalDispute, string PaymentTransferMethod, ref string Subject, ref string Body)
        {
            try
            {
                Body = Body.Replace("@StartDate@", StartDate).stringParse();
                Body = Body.Replace("@EndDate@", EndDate).stringParse();
                Body = Body.Replace("@TotalPayout@", TotalPayout).stringParse();
                Body = Body.Replace("@TotalSalesAmount@", TotalSalesAmont).stringParse();
                Body = Body.Replace("@TotalMDR@", TotalMdr).stringParse();
                Body = Body.Replace("@TotalRefund@", TotalRefund).stringParse();
                Body = Body.Replace("@TotalDispute@", TotalDispute).stringParse();
                Body = Body.Replace("@PaymentTransferMethod@", PaymentTransferMethod).stringParse();
    
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return;
            }
            finally { }

        }
        private void MappingLinkPaymentNotificationBodyData(string MerchantName, string CurrencyCode, string Amount, string OrderDetail, string PaymentUrl, ref string Subject, ref string Body)
        {
            try
            {
                Subject = Subject.Replace("@MerchantName@", MerchantName).stringParse();
                Body = Body.Replace("@MerchantName@", MerchantName).stringParse();
                Body = Body.Replace("@PaymentUrl@", PaymentUrl).stringParse();
                Body = Body.Replace("@Currecy@", CurrencyCode).stringParse();
                Body = Body.Replace("@Amount@", Amount).stringParse();
                Body = Body.Replace("@OrderDetail@", OrderDetail).stringParse();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return;
            }
        }
        #endregion

    }//end class

    public class EmailList
    {
        public string Email { get; set; }
        public string Name { get; set; }    
    }//end class
}//end namespace
