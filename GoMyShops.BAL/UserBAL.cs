using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL = GoMyShops.Data;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using System.Security.Claims;
using System.Threading.Tasks;
//using Microsoft.Owin.Extensions;
using System.Security.Principal;
using System.Threading;
using Newtonsoft.Json;
//using System.Data.Entity;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace GoMyShops.BAL
{
    public interface IUsersBAL
    {
        int GetSecurityID(string UserGroup);
        UserManager<ApplicationUser> GetUserManager(LoginSetupModel lsm);
        string GetUserGroup(string UserName);
        string GetUserGroupType(string UserName);
        ////bool isUserAsync(string UserName);
        ////void SetUserLastLoginDateAsync();
        Task<DateTime> GetUserLastLoginDateAsync();
        Task AddToPreviousPasswordsAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password);
        Task<IdentityResult> ResetPasswordAsync(UserManager<ApplicationUser> manager, string userId, string token, string newPassword);
        UserManager<ApplicationUser> GetManagerByName(string UserName);
        Task<LoginRedirectModel> Login(LoginModel model, ModelStateDictionary modelState);
        Task<LoginRedirectModel> LoginSignUp(LoginModel model, ModelStateDictionary modelState);
        Task<LoginRedirectModel> LoginPhrase(LoginImageViewModels model, ModelStateDictionary modelState);
        Task<bool> IsLockedOutAsync(string UserName);
        Task<bool> IsLockedOutSignUpUserAsync(string UserName);
        Task<bool> Register(string UserName, string Email, string Password, ModelStateDictionary modelState);
        Task<bool> UnRegisterAsync(string UserName);
        Task<LoginRedirectModel> ResetPassword(ResetPasswordViewModel model, ModelStateDictionary modelState);
        Task<LoginRedirectModel> ForgotResetPassword(ResetPasswordViewModel model, ModelStateDictionary modelState);
        Task<LoginRedirectModel> ForgotSignUpResetPassword(ResetPasswordViewModel model, ModelStateDictionary modelState);
        Task<LoginRedirectModel> ResetPasswordAdmin(ResetPasswordViewModel model, ModelStateDictionary modelState);
        Task<bool> SendUserEmailAsync(string EmailType, string UserName, string Password = "");
        ////void IdentitySignoutAsync();
        bool HasBeenVerified();
        bool SignUpVerified(string SignUpName);
        Task<bool> EmailVerified(SignUpVerifyViewModels model);
        Task<CommonSetting.SignInStatus> VerifyTwoFactorAsync(VerifyCodeViewModel model);
        Task<bool> VerifyTwoFactorWithoutChangedAsync(VerifyCodeViewModel model);
        ////CommonSetting.SignInStatus SignInOrTwoFactorAsync(string UserName, string UserType,bool isPersistent = false);
        Task<string> GenerateTwoFactorTokenAsync(string Provider, string userName);

        Task<CommonSetting.SignInStatus> SignInOrTwoFactorAsync(string UserName, string UserType, bool isPersistent = false);
        Task IdentitySigninAsync(LoginModel model, string providerKey = "", bool isPersistent = false);
        Task<LoginRedirectModel> ResetEmail(ResetEmailViewModel model, ModelStateDictionary modelState);
        Task<string> GetEmailAsync(string UserName, ModelStateDictionary modelState);
        Task<bool> SetEmail(string UserName, string email, ModelStateDictionary modelState);
        Task<LoginRedirectModel> ForgotPassword(ForgotPasswordViewModel model, ModelStateDictionary modelState);
        Task<LoginRedirectModel> ForgotSignUpPassword(ForgotPasswordViewModel model, ModelStateDictionary modelState);
        Task<string> GetResetPasswordCodeAsync(string UserName, ModelStateDictionary modelState);
        //string GetResetPasswordCodeAsync(string UserName, ModelStateDictionary modelState);
        Task<bool> Create(UserDetailsViewModels model, ModelStateDictionary modelState);
        List<UserListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total);
        Task<UserDetailsViewModels> getDetailAsync(string id, string id2, string id3, string id4);
        Task<bool> Edit(UserDetailsViewModels model, ModelStateDictionary modelState);
        bool EditPhrase(LoginImageViewModels model, ModelStateDictionary modelState);
        bool IsPhrased(string UserName);
        void getPhraseDetail(LoginImageViewModels model);
        //bool getPhraseSetup(string UserName);
        bool Deactived(List<UserListViewModels> model);
        Task<List<UserAccessLevelsModels>> GetUserAccessLevelsAsync(string UserName, string BranchCode);
        void PopulateUserAccessLevelList(List<TreeViewItemModel> tvtmList, string UserName);
        Task<bool> SetUserAccessLevelAsync(TreeViewSelectItemsModel tvsims, ModelStateDictionary modelState);
    }
    public class UsersBAL : BaseBAL,IUsersBAL
    {
        #region Definations
        private readonly ILogger<UsersBAL> _logger;
        private readonly int PASSWORD_HISTORY_LIMIT = 4;
        private const string DefaultSecurityStampClaimType = "AspNet.Identity.SecurityStamp";
        IUnitOfWorkFactory _uowFactory;
        ILoginBAL _loginBAL;
        IServicesBAL _servicesBAL;
        IUnitOfWork _uow;
        UserManager<ApplicationUser> _userManager;
        ////private UrlHelper _urlHelper;
        ISmsBAL _smsBAL;
        //IEmailFactory _emailFactory;
        IEmailSender _emailSender;
        IEmailBAL _emailBAL;
      
        private readonly IHttpContextAccessor _httpContextAccessor;
        IWebHostEnvironment _hostingEnvironment;
        public IConfiguration _configuration { get; private set; }
        // private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly LinkGenerator _linkGenerator;
        #endregion
        #region Constructor
        public UsersBAL (LinkGenerator linkGenerator,IHttpContextAccessor httpContextAccessor, ILogger<UsersBAL> logger, IWebHostEnvironment hostingEnvironment, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, UserManager<ApplicationUser> userManager, IUnitOfWorkFactory uowFactory,ILoginBAL loginBAL, IServicesBAL servicesBAL, ISmsBAL smsBAL, IEmailSender emailSender, IEmailBAL emailBAL) :base()
        {
            _uowFactory = uowFactory;
            _userManager = userManager;
            //_userManager.PasswordHasher = new PasswordHasher(ApplicationUser);
            _uow = uowFactory.Create();
            _loginBAL = loginBAL;
            _servicesBAL = servicesBAL;
            _smsBAL = smsBAL;
            _emailBAL = emailBAL;
            _emailSender = emailSender;           
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;
            _configuration = configuration;
            _linkGenerator = linkGenerator;
            _logger = logger;
            var context = _httpContextAccessor.HttpContext;
            //if (_hostingEnvironment.ContentRootPath != null)
            //{
            //    //Todo Harris (Test) Modify Core
            //    if (httpContextAccessor.HttpContext != null)
            //    {
            //        var actionContext = new ActionContext(httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor());

            //        _urlHelper = new UrlHelper(actionContext);
            //    }//end if
            //}
            //else
            //{
            //    //is windows app
            //}
    
        }
        #endregion
        #region Public Functions
        public string GetUserGroupType(string UserName)
        {
            string infos = "";
            //IEnumerable<User> us = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                   // us = uow.Repository<User>().GetAsQueryable(x => x.Username == UserName);

                    var ug = (from a in _uow.Repository<User>().GetAsQueryable(x => x.Username == UserName)
                              join userGroup in _uow.Repository<UserGroup>().GetAsQueryable() on new { a.GroupCode } equals new { userGroup.GroupCode } into guserGroup
                              from userGroupLOJ in guserGroup.DefaultIfEmpty()
                              select userGroupLOJ.GroupType
                              ).FirstOrDefault();

                    if (!ug.IsNullOrEmpty() && ug!=null)
                    {
                        infos = ug;
                    }//end if
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                throw;
            }
            finally { }
            return infos;
        }
        public string GetUserGroup(string UserName)
        {
            string infos = "";
            IEnumerable<User> us = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    us = _uow.Repository<User>().GetAsQueryable(x => x.Username == UserName);
                    if (!us.IsNullOrEmpty())
                    {
                        infos = us.FirstOrDefault().GroupCode;
                    }//end if
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                throw;
            }
            finally { }
            return infos;
        }       

        public int GetSecurityID(string UserGroup)
        {
            int infos = 0;
            UserGroup us=null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    us = _uow.Repository<UserGroup>().GetAsQueryable(x => x.GroupCode == UserGroup).FirstOrDefault();
                    if (us!=null)
                    {
                        infos = us.SecurityId;
                    }//end if
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos;
        }

        public async Task<DateTime> GetUserLastLoginDateAsync()
        {
            try
            {
                //_userManager =  HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                var user = await _userManager.FindByNameAsync(CurrentUser.Name); //_userManager.FindByName(CurrentUser.Name);
                if (user == null)
                {
                    return CommonFunctionsBAL.getDefaultDate();
                }
                else
                {
                    return user.LastLoginDate;

                }//end if-else
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                
            }
            finally {

            }
            return CommonFunctionsBAL.getDefaultDate();
        }

        public async Task SetUserLastLoginDateAsync()
        {
            try
            {
                // _userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                var user = await _userManager.FindByNameAsync(CurrentUser.Name); //_userManager.FindByName(CurrentUser.Name);

                //var user = _userManager.FindByName(CurrentUser.Name);
                if (user != null)
                {
                    //Update LastLoginDate
                    user.LastLoginDate = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                }//end if
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally
            {
            }
           
        }

        public UserManager<ApplicationUser> GetUserManager( LoginSetupModel lsm)
        {
           // ApplicationUser user;
           // UserManager<ApplicationUser> manager;
            //user = new ApplicationUser { UserName = uvm.UserName, Email = uvm.Email };

            //harris test
            //manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
           // manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            // Configure validation logic for usernames

            if (lsm.IsNullOrEmpty())
            {
                ////_userManager.UserValidator = new UserValidator<ApplicationUser>(_userManager)
                //// {
                ////     AllowOnlyAlphanumericUserNames = false,
                ////     RequireUniqueEmail = false
                //// };

                //// // Configure validation logic for passwords
                //// _userManager.PasswordValidator = new PasswordValidator
                //// {
                ////     RequiredLength = 12,
                ////     RequireNonLetterOrDigit = true,
                ////     RequireDigit = true,
                ////     RequireLowercase = true,
                ////     RequireUppercase = true

                ////     //RequiredLength = 3,
                ////     //RequireNonLetterOrDigit = false,
                ////     //RequireDigit = false,
                ////     //RequireLowercase = false,
                ////     //RequireUppercase = false,

                //// };

                //// // Configure user lockout defaults
                //// manager.UserLockoutEnabledByDefault = true;
                //// manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //// manager.MaxFailedAccessAttemptsBeforeLockout = 3;

                //_userManager. = new EmailService();
                //manager.SmsService = new SmsService();

                //var provider = new DpapiDataProtectionProvider("MT");
                //manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                //provider.Create("EmailConfirmation"))
                //{
                //    TokenLifespan = TimeSpan.FromDays(1)
                //}
                //;


                return _userManager;
            }//end if



            ////manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            ////{
            ////    AllowOnlyAlphanumericUserNames = lsm.AllowOnlyAlphanumericUserNames,
            ////    RequireUniqueEmail = lsm.RequireUniqueEmail
            ////};


            ////manager.PasswordValidator = new PasswordValidator
            ////{
            ////    RequiredLength = lsm.RequiredLength,
            ////    RequireNonLetterOrDigit = lsm.RequireNonLetterOrDigit,
            ////    RequireDigit = lsm.RequireDigit,
            ////    RequireLowercase = lsm.RequireLowercase,
            ////    RequireUppercase = lsm.RequireUppercase,
            ////};



            ////// Configure user lockout defaults         
            ////manager.UserLockoutEnabledByDefault = lsm.UserLockoutEnabledByDefault;
            ////manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(lsm.DefaultAccountLockoutTimeSpan);
            ////manager.MaxFailedAccessAttemptsBeforeLockout = lsm.MaxFailedAccessAttemptsBeforeLockout;
          
            ////// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            ////// You can write your own provider and plug it in here.
            ////manager.RegisterTwoFactorProvider(CommonSetting.TwoFactorProvider, new PhoneNumberTokenProvider<ApplicationUser>
            ////{
            ////    MessageFormat = "Your security code is {0}"
            ////});
            ////manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            ////{
            ////    Subject = "Security Code",
            ////    BodyFormat = "Your security code is {0}"
            ////});

            ////manager.EmailService = new EmailService();
            ////manager.SmsService = new SmsService();
            ////var provider1 = new DpapiDataProtectionProvider("MT");
            ////manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
            ////provider1.Create("EmailConfirmation"));


            return _userManager;
            //UserManager = HttpContext.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
        }

        public Task AddToPreviousPasswordsAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            user.PreviousUserPasswords.Add(new PreviousPassword() { UserId = user.Id, PasswordHash = password });
            return manager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(UserManager<ApplicationUser> manager, string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId); //_userManager.FindByName(CurrentUser.Name);
            if (await IsPreviousPassword(manager,userId, newPassword))
            {
                var error = new IdentityError();
                error.Description = "Cannot reuse previous password.";
                return await Task.FromResult(IdentityResult.Failed(error));
            }
            var result = await manager.ResetPasswordAsync(user, token, newPassword);//IdentityResult.Success;//
            if (result.Succeeded)
            {
                //var store = Store as ApplicationUserStore;
                PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>(
                                               new OptionsWrapper<PasswordHasherOptions>(
                                                   new PasswordHasherOptions()
                                                   {
                                                       CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
                                                   })
                                           );
                //var user =  manager.FindById(userId);
                if (user!=null)
                {
                    user.LastPasswordChangedDate = DateTime.Now;
                    var result1= await manager.UpdateAsync(user);
                    if (result1.Succeeded)
                    {
                        await AddToPreviousPasswordsAsync(manager, user, ph.HashPassword(user,newPassword));
                    }
                    else
                    {
                        var error = new IdentityError();
                        error.Description = CommonSetting.PleaseContactAdmin;
                        return await Task.FromResult(IdentityResult.Failed(error));
                    }//end if
                }//end if
                
                

            }
            return result;
        }

        private async Task<IdentityResult> ResetPasswordAsync(UserManager<ApplicationUser> manager, string userId, string token, string newPassword,bool resetFirstLoginDate)
        {
            var user = await _userManager.FindByIdAsync(userId); //_userManager.FindByName(CurrentUser.Name);

            if (await IsPreviousPassword(manager, userId, newPassword))
            {
                var error = new IdentityError();
                error.Description = "Cannot reuse previous password.";
                return await Task.FromResult(IdentityResult.Failed(error));
            }
            var result = await manager.ResetPasswordAsync(user, token, newPassword);//IdentityResult.Success;//
            if (result.Succeeded)
            {
                //var store = Store as ApplicationUserStore;
                PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>(
                                                             new OptionsWrapper<PasswordHasherOptions>(
                                                                 new PasswordHasherOptions()
                                                                 {
                                                                     CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
                                                                 })
                                                         );
                if (user != null)
                {
                    if (resetFirstLoginDate)
                    {
                        user.LastPasswordChangedDate = user.CreateDate; //DateTime.Now;
                    }
                    else
                    {
                        user.LastPasswordChangedDate = DateTime.Now;
                    }//end if-else
                    
                    var result1 = await manager.UpdateAsync(user);
                    if (result1.Succeeded)
                    {
                        await AddToPreviousPasswordsAsync(manager, await manager.FindByIdAsync(userId), ph.HashPassword(user,newPassword));
                    }
                    else
                    {
                        var error = new IdentityError();
                        error.Description = CommonSetting.PleaseContactAdmin;
                        return await Task.FromResult(IdentityResult.Failed(error));
                    }//end if
                }//end if



            }
            return result;
        }

        public UserManager<ApplicationUser> GetManagerByName(string UserName)
        {
            string intUsergroup = GetUserGroup(UserName);
            int securityKey = GetSecurityID(intUsergroup);
            LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();

            UserManager<ApplicationUser> manager;
            manager = GetUserManager(lsm);
            return manager;
        }

        public async Task<LoginRedirectModel> LoginPhrase(LoginImageViewModels model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);
            LoginImageViewModels model1 = new LoginImageViewModels(); 
            string UserType = "";
            try
            {
               

                string intUsergroup = "";
                UserType = GetUserGroupType(model.UserName);

                //Add for Different Login URL
                List<string> AccessUserTypeList = _configuration["AppSettings:LoginUserType"].Split(Convert.ToChar("|")).ToList();
                if (!AccessUserTypeList.Contains(UserType))
                {
                    model.IsUser = false;
                    return lrm;
                }//end if
                //


                if (UserType == "C")
                {
                    intUsergroup = _servicesBAL.GetCustomerGroupCode();
                }
                else
                {
                    intUsergroup = GetUserGroup(model.UserName);
                }//end if-else
                
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);
                //var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                var user = await manager.FindByNameAsync(model.UserName);
                //var user = await manager.FindAsync(model.UserName, model.Password);


                model.IsUser = false;
                if (user != null)
                {
                    model.IsUser = true;

                }
                else
                {
                    ////Temp off
                    ////modelState.AddModelError("", "Invalid credentials.");
                    //modelState.AddModelError("UserName", "The user name is incorrect.");
                    //modelState.AddModelError("Password", "The password is incorrect.");
                    //isError = true;

                    // HttpContext.Response.Redirect("~/Account/Login", true);
                }//end if-else




            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return lrm;

        }

        public async Task<LoginRedirectModel> Login(LoginModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);

            try
            {
                string intUsergroup = "";
                model.UserType = GetUserGroupType(model.UserName);

                if (model.UserType == "C")
                {
                    intUsergroup = _servicesBAL.GetCustomerGroupCode();
                }
                else
                {
                    intUsergroup = GetUserGroup(model.UserName);
                }//end if-else

                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                ApplicationUser user = await manager.FindByNameAsync(model.UserName);
                bool isUserPassword = await manager.CheckPasswordAsync(user, model.Password);
                if (!isUserPassword)
                {
                    user = null;
                }//end if

                //Add for Different Login URL
                List<string> AccessUserTypeList = _configuration["AppSettings:LoginUserType"].Split(Convert.ToChar("|")).ToList();
                if (!AccessUserTypeList.Contains(model.UserType))
                {
                    modelState.AddModelError("UserName", "The user name is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");

                    if (user != null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName,1);
                        await manager.AccessFailedAsync(user);
                    }
                    else
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName,1);                
                    }//end if                   

                    lrm.isError = true;
                    return lrm;
                }//end if
                //

                //check lsm is null
                if (lsm==null)
                {
                    modelState.AddModelError("UserName", "The user name is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");

                    if (user != null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                        await manager.AccessFailedAsync(user);
                    }
                    else
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                    }//end if                   

                    lrm.isError = true;
                    return lrm;
                }//end if
              

              
                //var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                //var user = await manager.FindByNameAsync(model.UserName);
                //var user = await manager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    //check user is active
                    if (!user.IsActive)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName,1);
                        await manager.AccessFailedAsync(user);

                        modelState.AddModelError("", "Invalid credentials. Please try again.");
                        lrm.Message = "Invalid credentials. Please try again.";
                        lrm.isError = false;
                        return lrm;
                    }//end if



                    /* this is the interesting part for you */
                    // Check FirstTime Login mode.
                    if (lsm.RequireFirstTimeChangePassword)
                    {
                        if (user.LastPasswordChangedDate.ToString(CommonSetting.DateFormatYYYYYMMDDHHNNSS) == user.CreateDate.ToString(CommonSetting.DateFormatYYYYYMMDDHHNNSS)) //if true, that means user never changed their password before
                        {
                            string Code =await manager.GeneratePasswordResetTokenAsync(user);
                            lrm = new LoginRedirectModel(_httpContextAccessor,"LoginResetPassword", Code, user.NormalizedUserName, true);
                            lrm.isError = false;
                            return lrm;
                       }//end if
                    }//end if

                    // Check if Change Password period is require.
                    if (lsm.RequireChangePasswordInPeriod)
                    {
                        var a = user.LastPasswordChangedDate.Date;

                        if (user.LastPasswordChangedDate.Date.AddDays(lsm.ChangePasswordInPeriodTimeSpan) < DateTime.Today)
                        {
                            string Code =await manager.GeneratePasswordResetTokenAsync(user);
                            lrm = new LoginRedirectModel(_httpContextAccessor,"LoginResetPassword", Code, user.NormalizedUserName, true);
                            lrm.isError = false;
                            return lrm;
                        }//end if
                    }//end if

                    ApplicationUser validCredentials = await manager.FindByNameAsync(model.UserName);
                    //var validCredentials = await manager.FindAsync(model.UserName, model.Password);
                    bool isUserPasswordvalidCredentials = await manager.CheckPasswordAsync(user, model.Password);
                    if (!isUserPasswordvalidCredentials)
                    {
                        validCredentials = null;
                    }//end if

                    // When a user is lockedout, this check is done to ensure that even if the credentials are valid
                    // the user can not login until the lockout duration has passed
                    if (await manager.IsLockedOutAsync(user))
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName,1);                 

                        modelState.AddModelError("", string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan.ToString()));
                    }
                    // if user is subject to lockouts and the credentials are invalid
                    // record the failure and check if user is lockedout and display message, otherwise,
                    // display the number of attempts remaining before lockout
                    else if (await manager.GetLockoutEnabledAsync(user) && validCredentials == null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName,1);
                       // await manager.AccessFailedAsync(user.Id);

                        // Record the failure which also may cause the user to be locked out
                        await manager.AccessFailedAsync(user);

                        string message;

                        if (await manager.IsLockedOutAsync(user))
                        {
                            message = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan);
                            modelState.AddModelError("", message);
                            lrm.isError = false;
                            lrm.Message = message;
                            lrm.Type = 1;
                            return lrm;
                        }
                        else
                        {
                            int accessFailedCount = await manager.GetAccessFailedCountAsync(user);

                            int attemptsLeft = lsm.MaxFailedAccessAttemptsBeforeLockout - accessFailedCount;
                            message = string.Format(
                                "Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);
                            modelState.AddModelError("", message);
                            lrm.isError = false;
                            lrm.Message = message;
                            return lrm;
                        }// end if-else

                       
                    }
                    else if (validCredentials == null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName,1);
                        await manager.AccessFailedAsync(user);

                        modelState.AddModelError("", "Invalid credentials. Please try again.");
                        lrm.isError = false;
                        return lrm;
                    }
                    else
                    {
                        // await  SignInAsync(user, model.RememberMe);
                        lrm.isError = false;
                        // When token is verified correctly, clear the access failed count used for lockout


                        //change SecurityStamp
                        if (intUsergroup == CommonSetting.GroupCode.Merchant || intUsergroup == CommonSetting.GroupCode.Partner)
                        {
                            await manager.UpdateSecurityStampAsync(user);
                        }//end if

                        await manager.ResetAccessFailedCountAsync(user);

                        ////Update LastLoginDate
                        //user.LastLoginDate= DateTime.Now;
                        //await manager.UpdateAsync(user);



                        //return RedirectToLocal(returnUrl);
                    }//end if-else



                }
                else
                {
                    //Temp off
                    //modelState.AddModelError("", "Invalid credentials.");

                    //log audit for unaccess person.
                    _loginBAL.SetAuditHeader(model.UserName, 1);

                    //check only for user not password
                    var usernameonly = await manager.FindByNameAsync(model.UserName);
                    if (usernameonly != null)
                    {   
                        // if user is subject to lockouts and the credentials are invalid
                        // record the failure and check if user is lockedout and display message, otherwise,
                        // display the number of attempts remaining before lockout
                        if (lsm.UserLockoutEnabledByDefault)
                        {
                            string message;
                            // Record the failure which also may cause the user to be locked out
                            await manager.AccessFailedAsync(usernameonly);

                            if (await IsLockedOutAsync(usernameonly,manager))
                            {
                                var RecipientConnectionCode = _uow.Repository<RecipientConnection>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active && x.ConnectionType == CommonSetting.ConnectionType.Lockout)
                                                            .OrderBy(x => x.CreatedTime).Select(x=>x.RecipientConnectionCode).FirstOrDefault();
                                if (!RecipientConnectionCode.IsNullOrEmptyString())
                                {
                                    //var a = _emailFactory.Create();
                                    isError = await _emailBAL.SendRecipientEmailAsync(CommonSetting.EmailSendType.UserAccountLock, RecipientConnectionCode,model.UserName);

                                }//end if

                                message = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan);
                               
                                lrm.Type = 1;
                            }
                            else
                            {
                                int accessFailedCount = await manager.GetAccessFailedCountAsync(usernameonly);

                                int attemptsLeft = lsm.MaxFailedAccessAttemptsBeforeLockout - accessFailedCount;
                                message = string.Format(
                                    "Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);

                                ////Reset Count 
                                //if (accessFailedCount== lsm.MaxFailedAccessAttemptsBeforeLockout)
                                //{
                                //    await manager.ResetAccessFailedCountAsync(usernameonly.Id);
                                //    var success2 = await manager.SetLockoutEndDateAsync(usernameonly.Id, DateTimeOffset.UtcNow.AddMinutes(lsm.DefaultAccountLockoutTimeSpan));

                                //}

                            }// end if-else


                            lrm.Message = message;
                            modelState.AddModelError("", message);
                            lrm.isError = false;
                            return lrm;
                        }

                    }//end if

                   

                  


                           

                    modelState.AddModelError("UserName", "The user name is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");
                    lrm.isError = true;
                  

                    // HttpContext.Response.Redirect("~/Account/Login", true);
                }//end if-else




            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return lrm;

        }

        public async Task<LoginRedirectModel> LoginSignUp(LoginModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);

            try
            {
                //string intUsergroup = "";
                //model.UserType = GetUserGroupType(model.UserName);

                //if (model.UserType == "C")
                //{
                //    intUsergroup = _servicesBAL.GetCustomerGroupCode();
                //}
                //else
                //{
                //    intUsergroup = GetUserGroup(model.UserName);
                //}//end if-else

                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(CommonSetting.SecurityID.Merchant).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                //var user = await manager.FindAsync(model.UserName, model.Password);
                ApplicationUser user = await manager.FindByNameAsync(model.UserName);
                bool isUserPassword = await manager.CheckPasswordAsync(user, model.Password);
                if (!isUserPassword)
                {
                    user = null;
                }//end if

                //Add for Different Login URL
                List<string> AccessUserTypeList = _configuration["AppSettings:LoginUserType"].Split(Convert.ToChar("|")).ToList();
                if (!AccessUserTypeList.Contains(model.UserType))
                {
                    modelState.AddModelError("UserName", "The user name is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");

                    if (user != null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                        await manager.AccessFailedAsync(user);
                    }
                    else
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                    }//end if                   

                    lrm.isError = true;
                    return lrm;
                }//end if
                //

                //check lsm is null
                if (lsm == null)
                {
                    modelState.AddModelError("UserName", "The user name is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");

                    if (user != null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                        await manager.AccessFailedAsync(user);
                    }
                    else
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                    }//end if                   

                    lrm.isError = true;
                    return lrm;
                }//end if


                if (user != null)
                {
                    ////check user is active
                    //if (!user.IsActive)
                    //{
                    //    //log audit for unaccess person.
                    //    _loginBAL.SetAuditHeader(model.UserName, 1);
                    //    await manager.AccessFailedAsync(user.Id);

                    //    modelState.AddModelError("", "Invalid credentials. Please try again.");
                    //    lrm.isError = false;
                    //    return lrm;
                    //}//end if



                    /* this is the interesting part for you */
                    //// Check FirstTime Login mode.
                    //if (lsm.RequireFirstTimeChangePassword)
                    //{
                    //    if (user.LastPasswordChangedDate.ToString(CommonSetting.DateFormatYYYYYMMDDHHNNSS) == user.CreateDate.ToString(CommonSetting.DateFormatYYYYYMMDDHHNNSS)) //if true, that means user never changed their password before
                    //    {
                    //        string Code = manager.GeneratePasswordResetToken(user.Id);
                    //        lrm = new LoginRedirectModel("LoginResetPassword", Code, user.UserName, true);
                    //        lrm.isError = false;
                    //        return lrm;
                    //    }//end if
                    //}//end if

                    //// Check if Change Password period is require.
                    //if (lsm.RequireChangePasswordInPeriod)
                    //{
                    //    var a = user.LastPasswordChangedDate.Date;

                    //    if (user.LastPasswordChangedDate.Date.AddDays(lsm.ChangePasswordInPeriodTimeSpan) < DateTime.Today)
                    //    {
                    //        string Code = manager.GeneratePasswordResetToken(user.Id);
                    //        lrm = new LoginRedirectModel("LoginResetPassword", Code, user.UserName, true);
                    //        lrm.isError = false;
                    //        return lrm;
                    //    }//end if
                    //}//end if
                  
                    //var validCredentials = await manager.FindAsync(model.UserName, model.Password);
                    ApplicationUser validCredentials = await manager.FindByNameAsync(model.UserName);
                    //var validCredentials = await manager.FindAsync(model.UserName, model.Password);
                    bool isUserPasswordvalidCredentials = await manager.CheckPasswordAsync(user, model.Password);
                    if (!isUserPasswordvalidCredentials)
                    {
                        validCredentials = null;
                    }//end if

                    // When a user is lockedout, this check is done to ensure that even if the credentials are valid
                    // the user can not login until the lockout duration has passed
                    if (await manager.IsLockedOutAsync(user))
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);

                        modelState.AddModelError("", string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan.ToString()));
                    }
                    // if user is subject to lockouts and the credentials are invalid
                    // record the failure and check if user is lockedout and display message, otherwise,
                    // display the number of attempts remaining before lockout
                    else if (await manager.GetLockoutEnabledAsync(user) && validCredentials == null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                        // await manager.AccessFailedAsync(user.Id);

                        // Record the failure which also may cause the user to be locked out
                        await manager.AccessFailedAsync(user);

                        string message;

                        if (await manager.IsLockedOutAsync(user))
                        {
                            message = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan);
                            modelState.AddModelError("", message);
                            lrm.isError = false;
                            lrm.Message = message;
                            lrm.Type = 1;
                            return lrm;
                        }
                        else
                        {
                            int accessFailedCount = await manager.GetAccessFailedCountAsync(user);

                            int attemptsLeft = lsm.MaxFailedAccessAttemptsBeforeLockout - accessFailedCount;
                            message = string.Format(
                                "Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);
                            modelState.AddModelError("", message);
                            lrm.isError = false;
                            lrm.Message = message;
                            return lrm;
                        }// end if-else


                    }
                    else if (validCredentials == null)
                    {
                        //log audit for unaccess person.
                        _loginBAL.SetAuditHeader(model.UserName, 1);
                        await manager.AccessFailedAsync(user);

                        modelState.AddModelError("", "Invalid credentials. Please try again.");
                        lrm.isError = false;
                        return lrm;
                    }
                    else
                    {
                        //// await  SignInAsync(user, model.RememberMe);
                        //lrm.isError = false;
                        //// When token is verified correctly, clear the access failed count used for lockout


                        ////change SecurityStamp
                        //if (intUsergroup == CommonSetting.GroupCode.Merchant || intUsergroup == CommonSetting.GroupCode.Partner)
                        //{
                        //    await manager.UpdateSecurityStampAsync(user.Id);
                        //}//end if

                        //await manager.ResetAccessFailedCountAsync(user.Id);

                    
                    }//end if-else



                }
                else
                {
                    //Temp off
                    //modelState.AddModelError("", "Invalid credentials.");

                    //log audit for unaccess person.
                    _loginBAL.SetAuditHeader(model.UserName, 1);

                    //check only for user not password
                    var usernameonly = await manager.FindByNameAsync(model.UserName);
                    if (usernameonly != null)
                    {
                        // if user is subject to lockouts and the credentials are invalid
                        // record the failure and check if user is lockedout and display message, otherwise,
                        // display the number of attempts remaining before lockout
                        if (lsm.UserLockoutEnabledByDefault)
                        {
                            string message;
                            // Record the failure which also may cause the user to be locked out
                            await manager.AccessFailedAsync(usernameonly);

                            if (await IsLockedOutAsync(usernameonly, manager))
                            {
                                var RecipientConnectionCode = _uow.Repository<RecipientConnection>().GetAsQueryable()
                                                            .OrderBy(x => x.CreatedTime).Select(x => x.RecipientConnectionCode).FirstOrDefault();
                                if (!RecipientConnectionCode.IsNullOrEmptyString())
                                {
                                    //var a = _emailFactory.Create();
                                    isError = await _emailBAL.SendRecipientEmailAsync(CommonSetting.EmailSendType.UserAccountLock, RecipientConnectionCode, model.UserName);

                                }//end if

                                message = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan);

                                lrm.Type = 1;
                            }
                            else
                            {
                                int accessFailedCount = await manager.GetAccessFailedCountAsync(usernameonly);

                                int attemptsLeft = lsm.MaxFailedAccessAttemptsBeforeLockout - accessFailedCount;
                                message = string.Format(
                                    "Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);

                                ////Reset Count 
                                //if (accessFailedCount== lsm.MaxFailedAccessAttemptsBeforeLockout)
                                //{
                                //    await manager.ResetAccessFailedCountAsync(usernameonly.Id);
                                //    var success2 = await manager.SetLockoutEndDateAsync(usernameonly.Id, DateTimeOffset.UtcNow.AddMinutes(lsm.DefaultAccountLockoutTimeSpan));

                                //}

                            }// end if-else


                            lrm.Message = message;
                            modelState.AddModelError("", message);
                            lrm.isError = false;
                            return lrm;
                        }

                    }//end if








                    modelState.AddModelError("UserName", "The user name is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");
                    lrm.isError = true;


                    // HttpContext.Response.Redirect("~/Account/Login", true);
                }//end if-else




            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return lrm;

        }

        public async Task<bool> IsLockedOutAsync(string UserName)
        {
            try {

                string intUsergroup = "";
                var UserType = GetUserGroupType(UserName);

                if (UserType == "C")
                {
                    intUsergroup = _servicesBAL.GetCustomerGroupCode();
                }
                else
                {
                    intUsergroup = GetUserGroup(UserName);
                }//end if-else

                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                var usernameonly = await manager.FindByNameAsync(UserName);
                if (usernameonly != null)
                {
                    var lockoutTime = await manager.GetLockoutEndDateAsync(usernameonly);
                    return lockoutTime >= DateTimeOffset.UtcNow;
                }//end if
              
            }
            catch (Exception ex)
            {
                var a = ex;
                //lrm.isError = true;
                //_logger.LogError("Error", ex);
            }
            finally { }
            return false;

          
        }

        public async Task<bool> IsLockedOutAsync(ApplicationUser user,UserManager<ApplicationUser> aum)
        {
            var lockoutTime = await aum.GetLockoutEndDateAsync(user);
            return lockoutTime >= DateTimeOffset.UtcNow;
        }

        public async Task<bool> IsLockedOutSignUpUserAsync(string UserName)
        {
            try
            {

                //string intUsergroup = "";
                //var UserType = GetUserGroupType(UserName);

                //if (UserType == "C")
                //{
                //    intUsergroup = _servicesBAL.GetCustomerGroupCode();
                //}
                //else
                //{
                //    intUsergroup = GetUserGroup(UserName);
                //}//end if-else

                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(CommonSetting.SecurityID.Merchant).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                var usernameonly = await manager.FindByNameAsync(UserName);
                if (usernameonly != null)
                {
                    var lockoutTime = await manager.GetLockoutEndDateAsync(usernameonly);
                    return lockoutTime >= DateTimeOffset.UtcNow;
                }//end if

            }
            catch (Exception)
            {
                //lrm.isError = true;
                //_logger.LogError("Error", ex);
            }
            finally { }
            return false;


        }

        public async Task<string> GenerateTwoFactorTokenAsync(string Provider,string userName)
        {
            try
            {
                string intUsergroup = GetUserGroup(userName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                var user = await manager.FindByNameAsync(userName);

                if (user != null)
                {
                    return await manager.GenerateTwoFactorTokenAsync(user, Provider);
                }//end if


            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return "";
        }

        public  string GetVerifiedUserIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;


            ////var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            ////var user = result.Principal;


            ////if (result != null && user.Identity != null
            ////    && !String.IsNullOrEmpty(user.Identity.GetUserId()))
            ////{
            ////    return user.Identity. result.Identity.GetUserId();
            ////}
            ////return null;
        }

        public  bool HasBeenVerified()
        {
            return  GetVerifiedUserIdAsync() != null;
        }

        public bool SignUpVerified(string SignUpName)
        {
            try
            {
                var isUser = _uow.Repository<User>().GetAsQueryable(x =>
                             x.Username == SignUpName
                            ).Count() > 0;

                if (isUser)
                {
                    return true;
                }
                else
                {
                    var isSignUpUser = _uow.Repository<SignUp>().GetAsQueryable(x =>
                              x.SignUpName == SignUpName &&
                              x.IsVerified == false
                            ).Count() > 0;

                    if (isSignUpUser)
                    {
                        return false;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return true;
        }

        

        public async Task<bool> EmailVerified(SignUpVerifyViewModels model)
        {
            LoginSetupModel ssvm = new LoginSetupModel(); // _loginBAL.GetSecuritySetupDetails(CommonSetting.SecurityID.Merchant).FirstOrDefault();
            UserManager<ApplicationUser> manager;
            manager = GetUserManager(ssvm);

            try
            {
                var user = await manager.FindByNameAsync(model.SignUpName);
                var code= System.Web.HttpUtility.UrlDecode(model.Code);

                if (user != null)
                {
                    var result = await manager.ConfirmEmailAsync(user, code);
                    if (result.Succeeded)
                    {
                        var infos = _uow.Repository<SignUp>().GetAsQueryable(x =>
                                    x.SignUpName == model.SignUpName
                                   ).FirstOrDefault();
                        if (!infos.IsNullOrEmpty())
                        {
                            var entry = _uow.Context.Entry(infos);
                            entry.Property(u => u.IsVerified).CurrentValue = true;
                            entry.Property(u => u.Status).CurrentValue = CommonSetting.Status.Active;
                            entry.Property(u => u.ModifiedBy).CurrentValue = model.SignUpName;
                            entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                            _uow.Repository<SignUp>().Update(entry);
                            _uow.Repository<SignUp>().Update(infos);                                 
                            isError = _uow.Save();
                        }//end if

                        return true;
                    }//end if
                }//end if
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return false;
        }

        public async Task<bool> VerifyTwoFactorWithoutChangedAsync(VerifyCodeViewModel model)
        {
            isTrue = false;
            try
            {
                string intUsergroup = GetUserGroup(model.UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                var user = await manager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    isTrue=false;
                }
                //if (await manager.IsLockedOutAsync(user.Id))
                //{
                //    model.ErrorMessage = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan.ToString());
                //    return SignInStatus.LockedOut;
                //}
                if (await manager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, model.Code))
                {
                    isTrue = true;
                }
                // If the token is incorrect, record the failure which also may cause the user to be locked out
                await manager.AccessFailedAsync(user);               

            }
            catch (Exception ex)
            {
                isTrue = false;
                _logger.LogError("Error", ex);
            }
            finally { }
            return isTrue;
        }

        public async Task<CommonSetting.SignInStatus> VerifyTwoFactorAsync(VerifyCodeViewModel model)
        {
            try
            {
                string intUsergroup = GetUserGroup(model.UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                var user = await manager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    return CommonSetting.SignInStatus.Failure;
                }

                if (await manager.IsLockedOutAsync(user))
                {
                    model.ErrorMessage = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", lsm.DefaultAccountLockoutTimeSpan.ToString());
                    return CommonSetting.SignInStatus.LockedOut;
                }
                if (await manager.VerifyTwoFactorTokenAsync(user,TokenOptions.DefaultPhoneProvider, model.Code))
                {
                    // When token is verified correctly, clear the access failed count used for lockout
                    await manager.ResetAccessFailedCountAsync(user);
                    await _signInManager.SignInAsync (user, false, IdentityConstants.ApplicationScheme);
                    var aa = _userManager.Users.FirstOrDefault();
                    //change SecurityStamp
                    //if (intUsergroup == CommonSetting.GroupCode.Administrator || intUsergroup == CommonSetting.GroupCode.AccountManager)
                    //{
                    //    await manager.UpdateSecurityStampAsync(user);
                     
                    //}//end if

                    //TODO Harris testing sign in here
                   

                    return CommonSetting.SignInStatus.Success;
                }
                // If the token is incorrect, record the failure which also may cause the user to be locked out
                await manager.AccessFailedAsync(user);
                return CommonSetting.SignInStatus.Failure;
    
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return CommonSetting.SignInStatus.Failure; 

        }

        /// <summary>
        /// No used
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserType"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public async Task<CommonSetting.SignInStatus> SignInOrTwoFactorAsync(
        string UserName, string UserType,
        bool isPersistent=false)
        {
            string intUsergroup = GetUserGroup(UserName);
            LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
            UserManager<ApplicationUser> manager;
            manager = GetUserManager(lsm);
          
          
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, UserName));
            claims.Add(new Claim(ClaimTypes.Name, UserName));
            //claims.Add(new Claim("UserType", UserType));
            var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. Required when setting the 
                // ExpireTimeSpan option of CookieAuthenticationOptions 
                // set with AddCookie. Also required when setting 
                // ExpiresUtc.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
            IdentityConstants.ApplicationScheme,
            new ClaimsPrincipal(identity),
            authProperties);

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(identity));

            //await _signInManager.SignInAsync(identity, authProperties);
            return CommonSetting.SignInStatus.RequiresVerification;
         
        }


        public async Task IdentitySigninAsync(LoginModel model, string providerKey = "", bool isPersistent = false)
        {
            var claims = new List<Claim>();

            if (model.UserType.IsNullOrEmptyString() )
            {
                model.UserType = GetUserGroupType(UserName);
            }//end if

            //claims = GetClaims(UserName, UserType);
            // var user =await _userManager.FindByNameAsync(UserName);

            //if (user == null)
            //{
            //    claims.Add(new Claim(DefaultSecurityStampClaimType, ""));
            //}
            //else
            //{
            //    var securityStamp =await _userManager.GetSecurityStampAsync(user);
            //    claims.Add(new Claim(DefaultSecurityStampClaimType, securityStamp));
            //}//end if-else

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ////This uses OWIN authentication

            ////AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            //await IdentitySignoutAsync();



            ////var a =_httpContextAccessor.HttpContext.Current.GetOwinContext().Authentication;
            ////HttpContext.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>()

            //var authProperties = new AuthenticationProperties
            //{
            //    AllowRefresh = true,
            //    IsPersistent = false,// Whether the authentication session is persisted across multiple requests. 
            //    ExpiresUtc = DateTime.UtcNow.AddDays(1),
            //};
           
            await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent, true);

           // await _signInManager.SignInAsync(user, authProperties, CookieAuthenticationDefaults.AuthenticationScheme);

           // await _httpContextAccessor.HttpContext.SignInAsync(
           //CookieAuthenticationDefaults.AuthenticationScheme,
           //new ClaimsPrincipal(identity),
           //authProperties);

           // _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(identity);


            ////keep for wheather user already select image and Phase.
            //if (IsPhrased(UserName))
            //{
            //    _httpContextAccessor.HttpContext.Session.SetString(CommonSetting.SessionId.LoginPhase, "1");           
            //}
            //else
            //{
            //    _httpContextAccessor.HttpContext.Session.SetString(CommonSetting.SessionId.LoginPhase, "2");        
            //}//end if-else


        }

        public async Task IdentitySignoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<LoginRedirectModel> ResetPassword(ResetPasswordViewModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);

            try
            {
                string intUsergroup = GetUserGroup(model.Email);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

               
                ApplicationUser user = await manager.FindByNameAsync(model.Email);
                bool isUserPassword = await manager.CheckPasswordAsync(user, model.Password);
                if (!isUserPassword)
                {
                    user = null;
                }//end if
                //var user = await manager.FindAsync(model.Email,model.CurrentPassword);

                if (user == null)
                {

                    //model.EmailCode = System.Web.HttpUtility.UrlDecode(model.EmailCode);

                    //lrm = new LoginRedirectModel("Login", model.EmailCode, user.UserName, true);
                    modelState.AddModelError("CurrentPassword", "The password is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");
                    modelState.AddModelError("ConfirmPassword", "The password is incorrect.");
                    lrm.isError = true;
                    return lrm;

      
                }
                //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

             
                var result = await ResetPasswordAsync(manager, user.Id, model.EmailCode, model.Password);
                if (result.Succeeded)
                {
                    await SendUserEmailAsync(CommonSetting.EmailSendType.PasswordChange, model.Email);
                    //if (!_isError)
                    //{
                    _smsBAL.SendUserProfile(model.Email, CommonSetting.SmsMessage.TypePasswordChange);
                    //}//end if
                    
                    await _signInManager.SignOutAsync();
                    lrm = new LoginRedirectModel(_httpContextAccessor,"Login", model.EmailCode, user.NormalizedUserName, true);
                    lrm.isError = false;
                    return lrm;
                    //return RedirectToAction("ResetPasswordConfirmation", "Account");
                    //HttpContext.Response.Redirect("~/Account/Login", true);
                }//end if

                if(result.Errors != null & result.Errors.Any())
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    TranslateAndAddPasswordPolicyError(errors, lsm, modelState,"");
                }
            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return lrm;

        }

        public async Task<LoginRedirectModel> ForgotResetPassword(ResetPasswordViewModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);

            try
            {
                string intUsergroup = GetUserGroup(model.Email);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);

                //modelState.AddModelError("Password", "The password is incorrect.");
                var user = await manager.FindByNameAsync(model.Email);
                //var user = await manager.FindAsync(model.Email,model.CurrentPassword);

                if (user == null)
                {

                    //model.EmailCode = System.Web.HttpUtility.UrlDecode(model.EmailCode);

                    //lrm = new LoginRedirectModel("Login", model.EmailCode, user.UserName, true);
                    //modelState.AddModelError("CurrentPassword", "The password is incorrect.");
                    modelState.AddModelError("Password", "The password is incorrect.");
                    modelState.AddModelError("ConfirmPassword", "The password is incorrect.");
                    lrm.isError = true;
                    return lrm;


                }
                //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);


                var result = await ResetPasswordAsync(manager, user.Id, model.EmailCode, model.Password);
                if (result.Succeeded)
                {
                    await SendUserEmailAsync(CommonSetting.EmailSendType.PasswordChange, model.Email);
                    //if (!_isError)
                    //{
                    //TODO harris _smsBAL.SendUserProfile
                    //_smsBAL.SendUserProfile(model.Email, CommonSetting.SmsMessage.TypePasswordChange);
                    //}//end if
                    //AuthenticationManager.SignOut();

                    await _signInManager.SignOutAsync();

                    lrm = new LoginRedirectModel(_httpContextAccessor,"Login", model.EmailCode, user.NormalizedUserName, true);
                    lrm.isError = false;
                    return lrm;
                    //return RedirectToAction("ResetPasswordConfirmation", "Account");
                    //HttpContext.Response.Redirect("~/Account/Login", true);
                }//end if

                if (result.Errors != null & result.Errors.Any())
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    TranslateAndAddPasswordPolicyError(errors, lsm, modelState, "");
                }

            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return lrm;

        }

        public async Task<LoginRedirectModel> ForgotSignUpResetPassword(ResetPasswordViewModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);

            try
            {
                string intUsergroup = GetUserGroup(model.Email);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(CommonSetting.SecurityID.Merchant).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                manager = GetUserManager(lsm);
             
                var user = await manager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    modelState.AddModelError("Password", "The password is incorrect.");
                    modelState.AddModelError("ConfirmPassword", "The password is incorrect.");
                    lrm.isError = true;
                    return lrm;

                }        

                var result = await ResetPasswordAsync(manager, user.Id, model.EmailCode, model.Password);
                if (result.Succeeded)
                {
                    await SendUserEmailAsync(CommonSetting.EmailSendType.PasswordChange, model.Email);
               
                    //_smsBAL.SendUserProfile(model.Email, CommonSetting.SmsMessage.TypePasswordChange);
                   
                    await _signInManager.SignOutAsync();
                    lrm = new LoginRedirectModel(_httpContextAccessor,"Login", model.EmailCode, user.NormalizedUserName, true);
                    lrm.isError = false;
                    return lrm;
                    //return RedirectToAction("ResetPasswordConfirmation", "Account");
                    //HttpContext.Response.Redirect("~/Account/Login", true);
                }//end if

                if (result.Errors != null & result.Errors.Any())
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    TranslateAndAddPasswordPolicyError(errors, lsm, modelState, "");
                }

            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return lrm;

        }

     

        public async Task<LoginRedirectModel> ResetPasswordAdmin(ResetPasswordViewModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);

            try
            {
                string intUsergroup = GetUserGroup(model.Email);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
              //  UserManager<ApplicationUser> manager;
              //  manager = GetUserManager(lsm);


                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    //string Code = manager.GeneratePasswordResetToken(user.Id);



                    lrm = new LoginRedirectModel(_httpContextAccessor,"Login", model.EmailCode, user.NormalizedUserName, true);
                    lrm.isError = true;
                    return lrm;

                    // Don't reveal that the user does not exist
                    //return RedirectToAction("ResetPasswordConfirmation", "Account");
                    //HttpContext.Response.Redirect("~/Account/Login", true);
                }
                //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

                //string Code = manager.GeneratePasswordResetToken(user.Id);
                //model.Code = model.Code.Replace(" ", "+");
                //if (model.Code.IsNullOrEmptyString())
                //{
                //    model.Code = manager.GeneratePasswordResetToken(user.Id);
                //}//end if
                var result = await ResetPasswordAsync(_userManager, user.Id, model.EmailCode, model.Password,true);
                if (result.Succeeded)
                {  
                    //var infos = _uow.Repository<User>().GetAsQueryable()
                    //          .Where(x => x.Username == model.Email)
                    //         .FirstOrDefault();
                    //if (!infos.IsNullOrEmpty())
                    //{
                    //    var entry = _uow.Context.Entry<User>(infos);                  
                    //    entry.Property(u => u.Password).CurrentValue = model.Password;

                    //    _uow.Save();
                    //}//end if
                    isError = await SendUserEmailAsync(CommonSetting.EmailSendType.SuccessTempPassword, model.Email,model.Password);

                    lrm = new LoginRedirectModel(_httpContextAccessor,"Login", model.EmailCode, user.NormalizedUserName, false);
                    lrm.isSuccess = true;
                    lrm.isError = false;
                    return lrm;
                    //return RedirectToAction("ResetPasswordConfirmation", "Account");
                    //HttpContext.Response.Redirect("~/Account/Login", true);
                }//end if

                if (result.Errors != null & result.Errors.Any())
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    TranslateAndAddPasswordPolicyError(errors, lsm, modelState, "");
                }
                
            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return lrm;

        }

        public async Task<List<UserAccessLevelsModels>> GetUserAccessLevelsAsync(string UserName, string BranchCode)
        {
            List<UserAccessLevelsModels> infos = null;
            try
            {
                infos =await _uow.Repository<UserAccessLevel>().GetAsQueryable()
                                                .Where(r => r.BranchCode == BranchCode && r.Username== UserName)
                                                .Select(r => new UserAccessLevelsModels
                                                {
                                                     BranchCode=r.BranchCode,
                                                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);

        }

        public async Task<LoginRedirectModel> ForgotPassword(ForgotPasswordViewModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);
            try
            {
                if (modelState.IsValid)
                {
                    string intUsergroup = GetUserGroup(model.UserName);
                    LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                    UserManager<ApplicationUser> manager;
                    manager = GetUserManager(lsm);


                    //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                    var user = await manager.FindByNameAsync(model.UserName);
                    if (user == null)// || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                    {
                        //modelState.AddModelError("", "The user does not exist.");
                        lrm = new LoginRedirectModel(_httpContextAccessor,"Login","", model.UserName, true);
                        lrm.isError = true;
                        lrm.returnString= "Resend email fail, the user does not exist.";
                        return lrm;
                    
                    }

                    if (user.NormalizedEmail.IsNullOrEmptyString())
                    {
                        //modelState.AddModelError("", "The user email does not exist.");
                        lrm = new LoginRedirectModel(_httpContextAccessor,"Login", "", model.UserName, true);
                        lrm.isError = true;
                        lrm.returnString = "Resend email fail, the user email does not exist.";
                        return lrm;
                    }//end if

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await manager.GeneratePasswordResetTokenAsync(user);
                    code = System.Web.HttpUtility.UrlEncode(code);
                    var callbackUrl = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext,"ForgotResetPassword", "Account", new { userName = model.UserName, code = code });

                    var msg= "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>";
                 
                    await _emailBAL.SendEmailAsync(user.NormalizedEmail, "Reset Password link", msg);
                   
                    lrm.isSuccess = true;

                    // SendEmail(user.NormalizedEmail, callbackUrl, "ResetPassword", "Please reset your password by clicking here");
                    // return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return lrm;
        }

        public async Task<LoginRedirectModel> ForgotSignUpPassword(ForgotPasswordViewModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);
            try
            {
                if (modelState.IsValid)
                {
                    string intUsergroup = GetUserGroup(model.UserName);
                    LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(CommonSetting.SecurityID.Merchant).FirstOrDefault();
                    UserManager<ApplicationUser> manager;
                    manager = GetUserManager(lsm);

                    var user = await manager.FindByNameAsync(model.UserName);
                    if (user == null)
                    {                 
                        lrm = new LoginRedirectModel(_httpContextAccessor,"Login", "", model.UserName, true);
                        lrm.isError = true;
                        lrm.returnString = "Resend email fail, the user does not exist.";
                        return lrm;
                    }

                    if (user.NormalizedEmail.IsNullOrEmptyString())
                    {
                        //modelState.AddModelError("", "The user email does not exist.");
                        lrm = new LoginRedirectModel(_httpContextAccessor, "Login", "", model.UserName, true);
                        lrm.isError = true;
                        lrm.returnString = "Resend email fail, the user email does not exist.";
                        return lrm;
                    }//end if

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await manager.GeneratePasswordResetTokenAsync(user);
                    code = System.Web.HttpUtility.UrlEncode(code);
                    //_userManager.EmailService.SendAsync(user.Id,)
                    //var callbackUrl = _urlHelper.Action("ForgotResetPassword", "Account", new { userName = model.UserName, code = code }, protocol: HttpContext.Current.Request.Url.Scheme);
                    var callbackUrl = _linkGenerator.GetPathByAction("ForgotResetPassword", "Account", new { userName = model.UserName, code = code });
                    var msg = "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>";
                    //IdentityMessage im = new IdentityMessage();
                    //im.Destination = user.NormalizedEmail;
                    //im.Body = msg;
                    //im.Subject = "Reset Password link";

                    await _emailSender.SendEmailAsync(user.NormalizedEmail, "Reset Password link", msg);
                 
                    lrm.isSuccess = true;

                    // SendEmail(user.NormalizedEmail, callbackUrl, "ResetPassword", "Please reset your password by clicking here");
                    // return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return lrm;
        }


        public async Task<string> GetEmailAsync(string UserName,ModelStateDictionary modelState)
        {
           
            try
            {
                    //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                    var user =await  _userManager.FindByNameAsync(UserName);
                    if (user == null)
                    {
                        modelState.AddModelError("", string.Format("UserNotFoundArgs", UserName));
                        return "";
                    }//end if

                    return user.NormalizedEmail;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return "";
        }

        public async Task<bool> SetEmail(string UserName,string email, ModelStateDictionary modelState)
        {

            try
            {
                // _userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                var user = await _userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    modelState.AddModelError("", string.Format("UserNotFoundArgs", UserName));
                    return false;
                }//end if

                if (user.NormalizedEmail == email)
                {
                    return true;
                }//end if


                var result=await _userManager.SetEmailAsync(user, email);
                if (result.Succeeded)
                {
                    return true;
                }//end if
                AddErrors(result, modelState);
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return false;
        }

        public async Task<bool> isUserAsync(string UserName)
        {

            try
            {
                //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                var user = await _userManager.FindByNameAsync(UserName);
                //var user = _userManager.FindByName(UserName);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }//end if

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return false;
        }

        ////private async Task<bool> SetStatusAsync(string UserName, bool isActive, ModelStateDictionary modelState)
        ////{

        ////    try
        ////    {
        ////        //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
        ////        var user = await _userManager.FindByNameAsync(UserName);
        ////        //var user = _userManager.FindByName(UserName);
        ////        if (user == null)
        ////        {
        ////            modelState.AddModelError("", string.Format("UserNotFoundArgs", UserName));
        ////            return false;
        ////        }//end if


        ////        user.IsActive = isActive;

        ////        var result =await  _userManager.UpdateAsync(user);
        ////        if (result.Succeeded)
        ////        {
        ////            return true;
        ////        }//end if
        ////        AddErrors(result, modelState);
        ////        return false;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        _logger.LogError("Error", ex);
        ////    }
        ////    finally { }

        ////    // If we got this far, something failed, redisplay form
        ////    return false;
        ////}

        private async Task<bool> SetStatusAsync(string UserName, bool isActive, ModelStateDictionary modelState)
        {

            try
            {
                //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                var user =await _userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    modelState.AddModelError("", string.Format("UserNotFoundArgs", UserName));
                    return false;
                }//end if


                user.IsActive = isActive;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }//end if
                AddErrors(result, modelState);
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return false;
        }

        private async Task<string> GetPasswordAsync(string UserName)
        {
            //no used
            try
            {
                //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                var user = await _userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return "";
                }//end if

                return user.PasswordHash;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return "";
        }

        public async Task<string> GetResetPasswordCodeAsync(string UserName, ModelStateDictionary modelState)
        {

            try
            {
                string intUsergroup = GetUserGroup(UserName);
                LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
                UserManager<ApplicationUser> manager;
                //manager = GetUserManager(lsm);
                //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                var user = await _userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return "";
                }//end if

                string code =await _userManager.GeneratePasswordResetTokenAsync(user);

                return code;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return "";
        }

        ////public async Task<string> GetResetPasswordCodeAsync(string UserName, ModelStateDictionary modelState)
        ////{

        ////    try
        ////    {
        ////        string intUsergroup = GetUserGroup(UserName);
        ////        LoginSetupModel lsm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(intUsergroup)).FirstOrDefault();
        ////        UserManager<ApplicationUser> manager;
        ////        manager = GetUserManager(lsm);
        ////        //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
        ////        var user = await manager.FindByNameAsync(UserName);
        ////        if (user == null)
        ////        {
        ////            return "";
        ////        }//end if

        ////        string code = await manager.GeneratePasswordResetTokenAsync(user);

        ////        return code;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        _logger.LogError("Error", ex);
        ////    }
        ////    finally { }

        ////    // If we got this far, something failed, redisplay form
        ////    return "";
        ////}

        public async Task<LoginRedirectModel> ResetEmail(ResetEmailViewModel model, ModelStateDictionary modelState)
        {
            var lrm = new LoginRedirectModel(_httpContextAccessor);
            try
            {
                if (modelState.IsValid)
                {
                    //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user == null )
                    {
                        modelState.AddModelError("", string.Format("UserNotFoundArgs", model.UserName));
                        return lrm;
                    }//end if

                    //string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                    //var user = await _userManager.FindByNameAsync(model.UserName);
                    var result = await _userManager.SetEmailAsync(user, model.Email);
                    if (result.Succeeded)
                    {
                        lrm.isSuccess = true;
                    }//end if
                    AddErrors(result, modelState);


                }
            }
            catch (Exception ex)
            {
                lrm.isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }

            // If we got this far, something failed, redisplay form
            return lrm;
        }

        public void Dispose()
        {

        }
        #endregion
        #region Public User Form
        public List<UserListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total)
        {

            var sessionclass = new SearchCriteria();
            bool EditAble = true;
            try
            {
                sessionclass.srcUserID = param1;
                sessionclass.srcUserName = param2;
                sessionclass.srcUserGroup = param3;
                sessionclass.srcStatus = param4;

                

                var List = new List<UserListViewModels>();
                //int GroupCode = CommonFunctions.intParse(sessionclass.srcUserGroup);

                var iparamRepo = _uow.Repository<Param>();

                var query = (from user in _uow.Repository<User>().GetAsQueryable()
                             join usergroup in _uow.Repository<UserGroup>().GetAsQueryable() on user.GroupCode equals usergroup.GroupCode into ug
                             from usergroupLOJ in ug.DefaultIfEmpty()
                             let iparam = iparamRepo.dbSet.FirstOrDefault(p2 => user.Type == p2.ParamKey && p2.ParamCode == CommonSetting.ParamCodes.UserType)
                             select new UserListViewModels
                             {
                                 CompanyCode = user.CompanyCode,
                                 DistributorCode = user.DistributorCode,
                                 BranchCode = user.BranchCode,
                                 UserID = user.Username,
                                 GroupType= usergroupLOJ.GroupType,
                                 //LocationCode=user.LocationCode,
                                 CustCode = user.CustomerCode,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 GroupCode = user.GroupCode,
                                 Type = user.Type,
                                 GroupName = usergroupLOJ.GroupName,
                                 TypeName = iparam == null ? "" : iparam.ParamDesc,
                                 Status = user.Status
                             });
                var queryableList = query
                    .WhereIf(!sessionclass.srcUserID.IsNullOrEmptyString(), tags => tags.UserID.Contains(sessionclass.srcUserID))
                    .WhereIf(!sessionclass.srcUserName.IsNullOrEmptyString(), tags => tags.FirstName.Contains(sessionclass.srcUserName) || tags.LastName.Contains(sessionclass.srcUserName))
                    .WhereIf(!sessionclass.srcUserGroup.IsNullOrEmptyString(), tags => tags.GroupCode == sessionclass.srcUserGroup)
                    .WhereIf(!sessionclass.srcStatus.IsNullOrEmptyString(), tags => tags.Status == sessionclass.srcStatus)
                    ;

                //check root admin
                if (CurrentUser.Name!="root" )
                {
                    queryableList = queryableList.Where(tags => tags.UserID != "root");
                }//endif

                
                //check Customer
                if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                {
                    EditAble = false;
                    mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
                    if (mCustomerCode.IsNullOrEmptyString())
                    {
                        mCustomerCode = _servicesBAL.GetCustomerCodeFromUserEntity();
                        queryableList = queryableList.Where(tags => tags.GroupCode != CommonSetting.GroupCode.Merchant);
                    }//end if
                    queryableList = queryableList.Where(tags => tags.CustCode == mCustomerCode);
                }//end if

                //check Partner
                if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                {
                    EditAble = false;
                    mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
                    if (mCustomerCode.IsNullOrEmptyString())
                    {
                        mCustomerCode = _servicesBAL.GetPartnerCodeFromUserEntity();
                        queryableList = queryableList.Where(tags => tags.GroupCode != CommonSetting.GroupCode.Partner);
                    }//end if
                    queryableList = queryableList.Where(tags => tags.CustCode == mCustomerCode);
                    
                }//end if

                if (!sort.IsNullOrEmptyString())
                {
                    List = CustomExpression.IQueryable<UserListViewModels>(queryableList, sort, "tags", order)
                         .Skip((offset / limit) * limit).Take(limit)
                         .ToList();
                }
                else
                {
                    List = queryableList
                                      .OrderBy(tags => tags.UserID)
                                      .Skip((offset / limit) * limit).Take(limit)
                                      .ToList();
                }//end if-else

                total = queryableList
                        .Count();

                List<UserListViewModels> b = (from a in List
                                              select new UserListViewModels
                                              {
                                                  IdKey = a.CompanyCode + a.DistributorCode,
                                                  DistributorCode = a.DistributorCode,
                                                  TypeName = a.TypeName,
                                                  GroupName = a.GroupName,
                                                  GroupCode = a.GroupCode,
                                                  UserID = a.UserID,
                                                  LocationCode = a.LocationCode,
                                                  CustCode = a.CustCode,
                                                  GroupTypeName = CommonFunctionsBAL.GroupTypeName(a.GroupType),
                                                  Username = a.FirstName + " " + a.LastName,
                                                  Type = a.Type,
                                                  BranchCode = a.BranchCode,
                                                  Status = a.Status,
                                                  EditGroupAndType = EditAble,
                                                  DetailJson = new ActionsListDetails(a.UserID, a.LocationCode, "", "", ""),
                                                  EditJson = new ActionsListDetails(a.UserID, a.LocationCode, "", "", ""),
                                                  //DetailJson = new ActionsListDetails(a.UserID, a.DistributorCode, a.BranchCode, "", ""),
                                                  //EditJson = new ActionsListDetails(a.UserID, a.DistributorCode, a.BranchCode, "", ""),

                                              }).ToList();

                return b;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }

            return new List<UserListViewModels>();

        }
        public async Task<bool> Create(UserDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                var TGcount = _uow.Repository<User>().Get(filter: x => x.Username ==  model.UserName).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("UserName", "User existed");
                }//end if

                var TGcount1 = _uow.Repository<SignUp>().Get(x => x.SignUpName == model.UserName).Count();

                if (TGcount1 > 0)
                {
                    modelState.AddModelError("UserName", "User existed");
                }//end if

                if (!CommonFunctionsBAL.isValidMalaysiaMobileNo(model.CountryCode, model.MobileNo))
                {
                    modelState.AddModelError("MobileNo", "Mobile no not a valid Malaysia mobile no.");
                }//end if

                if (modelState.IsValid)
                {
                    var Password = CommonFunctions.GenerateRandomString();
                    model.Password = Password;
                    mCustomerCode = model.CustCode;

                    if (CurrentUser.UserType == CommonSetting.UserType.Customer)
                    {
                        mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
                    }//end if
                    if (CurrentUser.UserType == CommonSetting.UserType.Partner)
                    {
                        mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
                    }//end if

                    //if (model.CustCode == "")
                    //{
                    //    mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
                    //}
                    //else
                    //{
                    //    mCustomerCode = model.CustCode;
                    //}//end if-else


                    User insertRow = new User();
                    var insertRepo = _uow.Repository<User>();

                    //insertRow.CompanyCode = "1";
                    //insertRow.DistributorCode = model.DistCode;
                    insertRow.BranchCode = "HQ";
                    insertRow.CustomerCode = mCustomerCode.stringParse();
                    insertRow.Username = model.UserName;
                    insertRow.FirstName = model.FirstName;
                    insertRow.LastName = model.LastName.stringParse();
                    insertRow.Address1 = model.Add1;
                    insertRow.Address2 = model.Add2;
                    //insertRow.Password = model.Password;
                    insertRow.Country = model.CountryCode;
                    insertRow.State = model.State.stringParse();
                    insertRow.City = model.City.stringParse();
                    insertRow.PostCode = model.PostCode;
                    insertRow.PhoneNo = model.PhoneNo;
                    insertRow.MobileNo = model.MobileNo;
                    //insertRow.Email = model.Email;
                    insertRow.Type = model.Type;
                    insertRow.GroupCode = model.GroupCode;
                    insertRow.Status = "1";
                    //insertRow.CustomerCode = model.mCustomerCode.stringParse();
                    insertRow.Phrase = "";
                    insertRow.ImageCode = "";
                    insertRow.CreatedBy = CurrentUser.Name;//HttpContext.Current.User.Identity.Name;
                    insertRow.CreatedTime = DateTime.Now;

                    insertRepo.Insert(insertRow);


                    bool isRegister = await Register(model, modelState);

                    if (!isRegister)
                    {
                        _uow.DetachAll();
                        return false;
                    }
                    else
                    {
                        isError = _uow.Save();
                        if (isError)
                        {
                            await UnRegisterAsync(model.UserName);
                        }//end if
                        else
                        {
                            //var a = _emailFactory.Create();
                            isError = await SendUserEmailAsync(CommonSetting.EmailSendType.SuccessCreatePassword, model.UserName, Password);
                        }
                    }//end if-else

             
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
        public async Task<UserDetailsViewModels> getDetailAsync(string id, string id2, string id3, string id4)
        {
            UserDetailsViewModels model = null;
            bool EditAble = true;
            try
            {
                var statusRepo = _uow.Repository<StatusSU>();
                var countryRepo = _uow.Repository<Country>();
                var stateRepo = _uow.Repository<State>();
                //var hhtUserRepo = _uow.Repository<HHTUser>();
                var iparamRepo = _uow.Repository<Param>();

                model = (from B in _uow.Repository<User>().GetAsQueryable()
                                       .Where(x => x.Username == id  )
                         //join location in _uow.Repository<Location>().GetAsQueryable() on B.LocationCode equals location.LocationCode into glocation
                         //from locationLOJ in glocation.DefaultIfEmpty()
                         join branch in _uow.Repository<Branch>().GetAsQueryable() on
                                        new { B.BranchCode } equals new { branch.BranchCode } into branchs
                         from branchLOJ in branchs.DefaultIfEmpty()
                         join company in _uow.Repository<Company>().GetAsQueryable() on branchLOJ.CompanyCode equals company.CompanyCode into C
                         from companyLOJ in C.DefaultIfEmpty()
                         join usergroup in _uow.Repository<UserGroup>().GetAsQueryable() on
                                        new {  B.GroupCode } equals new {  usergroup.GroupCode } into ug
                         from usergroupLOJ in ug.DefaultIfEmpty()
                         join distributor in _uow.Repository<Distributor>().GetAsQueryable() on
                                        new { branchLOJ.CompanyCode, branchLOJ.DistributorCode } equals new { distributor.CompanyCode, distributor.DistributorCode } into dist
                         from distributorLOJ in dist.DefaultIfEmpty()
                         
                         let status = statusRepo.dbSet.FirstOrDefault(p2 => B.Status == p2.Status)
                         let country = countryRepo.dbSet.FirstOrDefault(p2 => B.Country == p2.CountryCode)
                         //let state = stateRepo.dbSet.FirstOrDefault(p2 => B.Country == p2.CountryCode && B.State == p2.StateCode)
                         //////let hhtUser = hhtUserRepo.dbSet.FirstOrDefault(p2 => B.Username == p2.UserName)
                         let iparam= iparamRepo.dbSet.FirstOrDefault(p2 => B.Type == p2.ParamKey && p2.ParamCode==CommonSetting.ParamCodes.Designation)
                         select new UserDetailsViewModels(_httpContextAccessor)
                         {
                             CompCode = B.CompanyCode,
                             Company = B.CompanyCode + " - " + companyLOJ.Name,
                             GroupCode = B.GroupCode,
                             GroupName =  usergroupLOJ.GroupName,
                             UserName = B.Username,
                             DistCode = B.DistributorCode,
                             DistributorName= B.DistributorCode + " - " + distributorLOJ.Name,
                             BranchCode = B.BranchCode,
                             BranchName = B.BranchCode + " - " + branchLOJ.Name,
                             CustCode=B.CustomerCode,
                             GroupType= usergroupLOJ.GroupType,
                             //LocationCode =B.LocationCode,
                             //LocationName = B.LocationCode + " - " + locationLOJ.LocationName,
                             FirstName = B.FirstName,
                             LastName = B.LastName,
                             Add1 = B.Address1,
                             Add2 = B.Address2,                           
                             CountryCode = B.Country,
                             Country = country == null ? B.Country : country.CountryCode + " - " +  country.Name,
                             State = B.State,
                             City = B.City,
                             //State = state == null ? B.State : state.StateCode + " - " + state.Name,
                             PostCode = B.PostCode,
                             PhoneNo = B.PhoneNo,
                             MobileNo = B.MobileNo,
                             //Email = B.Email,
                             Password="123",
                             TypeName= iparam == null ? B.Type : iparam.ParamDesc,//iparam.ParamKey + " - " + iparam.ParamDesc,  
                             Type = B.Type,
                             Status = status == null ? B.Status : status.StatusName,
                             CheckBoxStatus = B.Status == "1" ? true : false,
                             CreatedBy = B.CreatedBy,
                             CreatedTime = B.CreatedTime,
                             ModifiedBy = B.ModifiedBy,
                             ModifiedTime = B.ModifiedTime,
                             //ImeiNo = hhtUser == null ? "" : hhtUser.ImeiNo,

                         }).FirstOrDefault();

                if (!model.IsNullOrEmpty())
                {
                    //check is Merchant or Partner group or not.
                    if (model.GroupCode==CommonSetting.GroupCode.Merchant ||
                        model.GroupCode == CommonSetting.GroupCode.Partner || model.GroupCode==CommonSetting.GroupCode.OnBoarding)
                    {
                        model.GroupCodeAccess = true;
                        model.GroupCodeMainUserAccess = true;
                    }//end if
                     //

                    //check is Merchant or Partner sub-group or not.
                    if (!model.CustCode.IsNullOrEmptyString())
                    {
                        model.GroupCodeAccess = true;
                        model.GroupCodeSubAccess = true;                        
                        model.GroupTypeName = CommonFunctionsBAL.GroupTypeName(model.GroupType);
                    }//end if
                    //



                    //add to check IsInActiveable user
                    if (model.UserName == "admin" || model.UserName == "root" )
                    {
                        model.IsInActive = true;
                    }//end if

                    //

                    model.sCreatedTime = CommonFunctionsBAL.ParseStandardDateFormat(model.CreatedTime, true, true);
                    model.sModifiedTime = model.ModifiedTime.HasValue ? CommonFunctionsBAL.ParseStandardDateFormat(model.ModifiedTime.GetValueOrDefault(), true, true) : " - ";


                    model.EditJson = JsonConvert.SerializeObject(new ActionsListDetails(model.UserName,"", "", "", ""));
                    //model.EditJson = JsonConvert.SerializeObject(new ActionsListDetails(model.UserName, model.DistCode, model.BranchCode, "", ""));
                    ModelStateDictionary msd = new ModelStateDictionary();
                    model.Email =await GetEmailAsync(model.UserName,msd);
                    //model.Password = GetPassword(model.UserName);
                }//end if
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return model.IsNullThenNew(_httpContextAccessor);
        }
        public async Task<bool> Edit(UserDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                if (!CommonFunctionsBAL.isValidMalaysiaMobileNo(model.CountryCode, model.MobileNo))
                {
                    modelState.AddModelError("MobileNo", "Mobile no not a valid Malaysia mobile no.");
                }//end if

               

                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                var infos = _uow.Repository<User>().GetAsQueryable()
                                .Where(x => x.Username == model.UserName )
                                //.Where(x => x.CompanyCode == "1" && x.Username == model.UserName &&
                                //            x.DistributorCode == model.DistCode && x.BranchCode == model.BranchCode)
                                .FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    if (infos.GroupCode == CommonSetting.GroupCode.AccountManager && model.GroupCode != CommonSetting.GroupCode.AccountManager)
                    {
                        var infoAccountManager = _uow.Repository<AccountManager>().GetAsQueryable(x =>
                                        x.AccountManagerUserCode == model.UserName
                                       ).FirstOrDefault();
                        if (!infoAccountManager.IsNullOrEmpty())
                        {
                            modelState.AddModelError("GroupCode", "Some Account Manager still under this user.");
                        }//end if

                        if (!modelState.IsValid)
                        {
                            return false;
                        }//end if
                    }//end if
                    


                    var entry = _uow.Context.Entry<User>(infos);
                    //entry.Property(u => u.BranchCode).CurrentValue = model.BranchCode;                   
                    //entry.Property(u => u.Username).CurrentValue = model.Username;
                    entry.Property(u => u.FirstName).CurrentValue = model.FirstName;
                    entry.Property(u => u.LastName).CurrentValue = model.LastName.stringParse();
                    entry.Property(u => u.Address1).CurrentValue = model.Add1;
                    entry.Property(u => u.Address2).CurrentValue = model.Add2;                    
                    entry.Property(u => u.Country).CurrentValue = model.CountryCode;
                    entry.Property(u => u.State).CurrentValue = model.State.stringParse();
                    entry.Property(u => u.City).CurrentValue = model.City.stringParse();
                    entry.Property(u => u.PostCode).CurrentValue = model.PostCode;
                    entry.Property(u => u.PhoneNo).CurrentValue = model.PhoneNo;
                    entry.Property(u => u.MobileNo).CurrentValue = model.MobileNo;
                    entry.Property(u => u.Type).CurrentValue = model.Type;
                    entry.Property(u => u.GroupCode).CurrentValue = model.GroupCode;
                    //insertRow.Phrase = "";
                    //insertRow.ImageCode = "";                  
                    entry.Property(u => u.ModifiedBy).CurrentValue =CurrentUser.Name; ;
                    entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                    entry.Property(u => u.Status).CurrentValue = CommonFunctions.Iif(model.CheckBoxStatus == true, CommonSetting.Status.Active, CommonSetting.Status.Inactive);
                    
                }//end if

                //var infosHHTUser = _uow.Repository<HHTUser>().GetAsQueryable()
                //                .Where(x => x.UserName == model.UserName)
                //                //.Where(x => x.CompanyCode == "1" && x.Username == model.UserName &&
                //                //            x.DistributorCode == model.DistCode && x.BranchCode == model.BranchCode)
                //                .FirstOrDefault();
                //if (!infosHHTUser.IsNullOrEmpty())
                //{
                //    var entryHHTUser = _uow.Context.Entry<HHTUser>(infosHHTUser);
                //    //entryHHTUser.Property(u => u.ImeiNo).CurrentValue = model.ImeiNo;
                //    entryHHTUser.Property(u => u.Status).CurrentValue = Convert.ToInt32( CommonFunctions.Iif(model.CheckBoxStatus == true, CommonSetting.Status.Active, CommonSetting.Status.Inactive));
                //    entryHHTUser.Property(u => u.ModifiedBy).CurrentValue = HttpContext.Current.User.Identity.Name; ;
                //    entryHHTUser.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                //}//end if

                isError = _uow.Save();

                //Send Email 
                if (!isError)
                {               
                    isError = await SendUserEmailAsync(CommonSetting.EmailSendType.ProfileUpdate, model.UserName);
                    _smsBAL.SendUserProfile(model.UserName, CommonSetting.SmsMessage.TypeProfileUpdate);
                }//end if


                await SetEmail(model.UserName,model.Email, modelState);
                 var isSuccess= await SetStatusAsync(model.UserName, model.CheckBoxStatus, modelState);
                 if (!isSuccess)
                 {
                    return true;
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
        public bool EditPhrase(LoginImageViewModels model, ModelStateDictionary modelState)
        {
            isError = false;
            try
            {
                var infos = _uow.Repository<User>().GetAsQueryable()
                               .Where(x => x.Username == CurrentUser.Name)
                               .FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    var entry = _uow.Context.Entry<User>(infos);            
                    entry.Property(u => u.ImageCode).CurrentValue = model.ImageCode.stringParse();
                    entry.Property(u => u.Phrase).CurrentValue = model.Phrase.stringParse();
                    entry.Property(u => u.ModifiedBy).CurrentValue =CurrentUser.Name;
                    entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                    isError = _uow.Save();

                    //Set LoginPhase is Phased
                    if (!isError)
                    {
                        _httpContextAccessor.HttpContext.Session.SetString(CommonSetting.SessionId.LoginPhase, "1");
                        //httpContext.Session.(CommonSetting.SessionId.LoginPhase) != null
                        //model.HttpContext.Current.Session[CommonSetting.SessionId.LoginPhase] = "1";
                    }//end if

                }//end if
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return true;
            }
            finally { }
            return isError;
           
        }

        public bool IsPhrased(string UserName)
        {
            isError = false;
            try
            {
                var infos = _uow.Repository<User>().GetAsQueryable()
                               .Where(x => x.Username == UserName)
                               .FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {

                    if (infos.ImageCode.stringParse()=="" || infos.Phrase.stringParse() == "")
                    {
                        return false;
                    }//end if

                    return true;
                }//end if          

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            return false;

        }

        public void getPhraseDetail(LoginImageViewModels model)
        {
            LoginImageViewModels model1 = new LoginImageViewModels();
            string userName = model.UserName;
            try
            {
                if (CurrentUser != null)
                {
                    if (CurrentUser.Name !=null)
                    {
                        if (CurrentUser.Name != "")
                        {
                            userName = CurrentUser.Name;
                        }//end if                     
                    }//end if
                }//end if
               

                model1 = (from B in _uow.Repository<User>().GetAsQueryable()
                                       .Where(x => x.Username == userName)                
                         select new LoginImageViewModels
                         {
                              ImageCode=B.ImageCode,
                               Phrase=B.Phrase,
                         }).FirstOrDefault();
                if (model1 != null)
                {
                    model.ImageCode = model1.ImageCode.stringParse();
                    model.Phrase = model1.Phrase.stringParse();
                }//end if

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            //return model.IsNullThenNew();
        }
        //public bool getPhraseSetup(string UserName)
        //{
        //    try
        //    {
        //       var model1 =  _uow.Repository<User>().GetAsQueryable()
        //                    .Where(x => x.Username == UserName)
        //                    .Where(x => x.ImageCode != "")
        //                    .Where(x => x.Phrase != "").Count();
                       
        //        if (model1 >0)
        //        {
        //            return true;
        //        }//end if

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error", ex);
        //    }
        //    finally { }
        //    return false;
        //}
        public bool Deactived(List<UserListViewModels> model)
        {
            try
            {
                if (!model.IsNullOrEmpty())
                {
                    foreach (var b in model)
                    {
                        var infos = _uow.Repository<User>().GetAsQueryable()
                                    .Where(x => x.CompanyCode == "1" && x.Username == b.UserID &&
                                            x.DistributorCode == b.DistributorCode && x.BranchCode == b.BranchCode
                                            && x.Status == CommonSetting.Status.Active)
                                    .FirstOrDefault();
                        if (!infos.IsNullOrEmpty())
                        {
                            var entry = _uow.Context.Entry<User>(infos);
                            entry.Property(u => u.ModifiedBy).CurrentValue = CurrentUser.Name;
                            entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                            entry.Property(u => u.Status).CurrentValue = CommonSetting.Status.Inactive;

                           

                            ModelStateDictionary modelState = new ModelStateDictionary();
                            var isSuccess =  SetStatusAsync(b.UserID, false, modelState);
                            //if (!isSuccess)
                            //{
                            //    return true;
                            //}//end if

                        }//end if

                        //var infosHHTUser = _uow.Repository<HHTUser>().GetAsQueryable()
                        //    .Where(x => x.UserName == b.UserID)
                        //    .FirstOrDefault();
                        //if (!infosHHTUser.IsNullOrEmpty())
                        //{
                        //    var entryHHTUser = _uow.Context.Entry<HHTUser>(infosHHTUser);
                        //    entryHHTUser.Property(u => u.Status).CurrentValue = Convert.ToInt32(CommonSetting.Status.Inactive);
                        //    entryHHTUser.Property(u => u.ModifiedBy).CurrentValue = HttpContext.Current.User.Identity.Name; ;
                        //    entryHHTUser.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;
                        //}//end if

                        isError = _uow.Save();


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
        public void PopulateUserAccessLevelList(List<TreeViewItemModel> tvtmList, string UserName)
        {
            int NodeIdCount =  1;
            int distributorCount = 0;
            int branchCount = 0;
            try
            {
                

                var companyModel = _uow.Repository<Company>().GetAsQueryable().ToList();
                var ual = _uow.Repository<UserAccessLevel>().GetAsQueryable(x => x.Username == UserName);
                foreach (Company c in companyModel)
                {
                    TreeViewItemModel tvimCompany = new TreeViewItemModel();
                    tvimCompany.text = c.CompanyCode + " - " + c.Name; 
                    tvimCompany.nodeId = c.CompanyCode;
                    tvimCompany.nodeCode = c.CompanyCode;
                    tvimCompany.parentId = "0";
                    tvimCompany.type = "C";
                    tvimCompany.selectable = true;

                    if (ual.Where(x => x.CompanyCode == c.CompanyCode).Count() >= 1)
                    {
                        tvimCompany.checkedByDefault = true;
                    }
                    else
                    {
                        tvimCompany.checkedByDefault = false;
                    }//end if-else
                    

                    tvimCompany.color = "#000000";
                    tvimCompany.backColor = "#9589FF";
                    tvimCompany.icon = "glyphicon";

                    var distributorModel = _uow.Repository<Distributor>().GetAsQueryable()
                                                                         .Where(x=>x.CompanyCode==c.CompanyCode)
                                                                         .ToList();
                    distributorCount = distributorModel.Count();
                    foreach (Distributor d in distributorModel)
                    {
                        if (tvimCompany.nodes == null)
                        {
                            tvimCompany.nodes = new List<TreeViewItemModel>();
                        }//end if

                        

                        TreeViewItemModel tvimDistributor = new TreeViewItemModel();
                        //if (tvimDistributor.state == null)
                        //{
                        //    tvimDistributor.state = new List<string>();
                        //}//end if
                        tvimDistributor.text = d.DistributorCode + " - " + d.Name; 
                        tvimDistributor.nodeId = d.DistributorCode;
                        tvimDistributor.nodeCode = d.DistributorCode;
                        tvimDistributor.parentId = tvimCompany.nodeId;
                        tvimDistributor.type = "D";
                        tvimDistributor.selectable = true;

                        if (ual.Where(x => x.DistributorCode == d.DistributorCode).Count() >= 1)
                        {
                            tvimDistributor.checkedByDefault = true;
                        }
                        else
                        {
                            tvimDistributor.checkedByDefault = false;
                        }//end if-else

                     
                        //tvimDistributor.state.check = true;
                        tvimDistributor.color = "#000000";
                        tvimDistributor.backColor = "#ADA4FF";
                        tvimDistributor.icon = "glyphicon";

                        var branchModel = _uow.Repository<Branch>().GetAsQueryable()
                                                                         .Where(x => x.CompanyCode == c.CompanyCode
                                                                                  && x.DistributorCode == d.DistributorCode)
                                                                         .ToList();
                        branchCount = branchModel.Count();
                        foreach (Branch b in branchModel)
                        {
                            if (tvimDistributor.nodes == null)
                            {
                                tvimDistributor.nodes = new List<TreeViewItemModel>();
                            }//end if

                            TreeViewItemModel tvimBranch = new TreeViewItemModel();
                            tvimBranch.text = b.BranchCode + " - " + b.Name  ;
                            tvimBranch.nodeId = b.BranchCode;
                            tvimBranch.nodeCode= b.BranchCode;
                            tvimBranch.parentId = tvimDistributor.nodeId;
                            tvimBranch.type = "B";
                            tvimBranch.selectable = true;

                            if (ual.Where(x => x.BranchCode == b.BranchCode).Count() >= 1)
                            {
                                tvimBranch.checkedByDefault = true;
                            }
                            else
                            {
                                tvimBranch.checkedByDefault = false;
                            }//end if-else

                            //tvimBranch.selectedBackColor = "#FFBDBD";
                            tvimBranch.color = "#000000";
                            tvimBranch.backColor = "#CCC6FF";
                            tvimBranch.icon = "glyphicon glyphicon-circle-arrow-right";

                            //tvimBranch.tags = new string[] { (fileCount + dirCount).ToString() };
                            tvimDistributor.nodes.Add(tvimBranch);

                            NodeIdCount++;
                        }//end foreach

                        tvimDistributor.tags = new string[] { (branchCount).ToString() };
                        tvimCompany.nodes.Add(tvimDistributor);
                    }//end foreach


                    tvimCompany.tags = new string[] { (distributorCount).ToString() };
                    tvtmList.Add(tvimCompany);

                }//end foreach
    
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
            finally { }



        }
        public async Task<bool> SendUserEmailAsync(string EmailType, string UserName,string Password="")
        {
            try
            {
                // a = _emailFactory.Create();
                isError = await _emailBAL.SendUserEmailAsync(EmailType, UserName,Password);


                return isError;

            }
            catch (Exception )
            {
                return false;
            }
            finally { }
            
        }
        public async Task<bool> SetUserAccessLevelAsync(TreeViewSelectItemsModel tvsims, ModelStateDictionary modelState)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return false;
                }//end if

                //using (var uow = this._uowFactory.Create())
                //{
                    var ual = _uow.Repository<UserAccessLevel>().GetAsQueryable(x => x.Username == tvsims.userName);
                    _uow.Repository<UserAccessLevel>().DeleteAll(ual);
                    var insertRepo = _uow.Repository<UserAccessLevel>();
                    foreach (TreeViewSelectItemModel tvsim in tvsims.nodes)
                    {
                        UserAccessLevel insertRow = new UserAccessLevel();
                        switch (tvsim.nodeType)
                        {
                            case "C":
                                insertRow.CompanyCode = tvsim.nodeCode;
                                insertRow.DistributorCode = "";
                                insertRow.BranchCode = "";
                                insertRow.Username = tvsims.userName;
                                insertRow.CreatedBy= CurrentUser.Name;
                                insertRow.CreatedTime = DateTime.Now;
                                insertRepo.Insert(insertRow);
                                break;
                            case "D":
                                insertRow.CompanyCode = "";
                                insertRow.DistributorCode = tvsim.nodeCode;
                                insertRow.BranchCode = "";
                                insertRow.Username = tvsims.userName;
                                insertRow.CreatedBy = CurrentUser.Name;
                                insertRow.CreatedTime = DateTime.Now;
                                insertRepo.Insert(insertRow);
                                break;
                            case "B":
                                insertRow.CompanyCode = "";
                                insertRow.DistributorCode = "";
                                insertRow.BranchCode = tvsim.nodeCode;
                                insertRow.Username = tvsims.userName;
                                insertRow.CreatedBy = CurrentUser.Name;
                                insertRow.CreatedTime = DateTime.Now;
                                insertRepo.Insert(insertRow);
                                break;
                            default:
                                break;
                        }//end switch   


                        
                    }//end foreach
                    isError = await _uow.SaveAsync();
                //}//end using

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

        public async Task<bool> Register(string UserName, string Email,string Password, ModelStateDictionary modelState)
        {
          
            try
            {
                if (modelState.IsValid)
                {
                    LoginSetupModel ssvm = _loginBAL.GetSecuritySetupDetails(CommonSetting.SecurityID.Merchant).FirstOrDefault();
                    ApplicationUser user;
                    user = new ApplicationUser { UserName = UserName, Email = Email };
                    UserManager<ApplicationUser> manager;
                    //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                    //manager = _userManager;
                    manager = GetUserManager(ssvm);

                    //skip password checking
                    //manager.PasswordValidator = new PasswordValidator
                    //{
                    //    RequiredLength = 12,
                    //    RequireNonLetterOrDigit = true,
                    //    RequireDigit = true,
                    //    RequireLowercase = true,
                    //    RequireUppercase = true,

                    //};
                    //remember to change loginsetupmodel so error mapping is accurate
                    //ssvm.RequiredLength = 12;


                    var result = await manager.CreateAsync(user, Password);
                    if (result.Succeeded)
                    {
                        //store password's history
                        //await AddToPreviousPasswordsAsync(manager, user, user.PasswordHash);
                        return true;
                    }

                    if (result.Errors != null & result.Errors.Any())
                    {
                        var errors = new List<string>();
                        errors = result.Errors.Select(x => x.Description).ToList();
                        TranslateAndAddPasswordPolicyError(errors, ssvm, modelState, "");
                    }
                    return false;
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

        public async Task<bool> UnRegisterAsync(string UserName)
        {
            try
            {               
                   // UserManager<ApplicationUser> manager;
                    //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                    //manager = _userManager;

                    var user =await  _userManager.FindByNameAsync(UserName);
                    if (user == null)
                    {
                        var result =await _userManager.DeleteAsync(user);
                    }//end if
                return true;
              
            }
            catch (Exception)
            {
                return false;
            }
            finally { }
         
        }

        #endregion
        #region Private Functions
        //private void MappingData1(string EmailType, string UserName, string CallbackUrl, ref string Subject, ref string Body)
        //{
        //    try
        //    {
        //        var emailMaster = _uow.Repository<EmailMaster>().GetAsQueryable().FirstOrDefault();
        //        var email = _uow.Repository<Email>().GetAsQueryable(x => x.EmailType == EmailType).FirstOrDefault();
        //        if (email != null)
        //        {
        //            Subject = email.EmailSubject.Replace("@TITLE@", emailMaster.EmailTitle).stringParse();
        //            Body = email.EmailBody.Replace("@TITLE@", emailMaster.EmailTitle).stringParse();
        //            Body = Body.Replace("@USERNAME@", UserName).stringParse();
        //            Body = Body.Replace("@EXPIREHOUR@", emailMaster.TokenExpireHour.ToString()).stringParse();
        //            Body = Body.Replace("@URL@", "<a href='" + CallbackUrl + "'>URL</a>").stringParse();
        //            Body = Body.Replace("@COMPANYURL@", "<a href='" + emailMaster.CompanyURL + "'>here</a>").stringParse();
        //            Body = Body.Replace("@COMPANYWEB@", emailMaster.CompanyWebSite).stringParse();
        //        }//end if

        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }
        //    finally { }

        //}

        private async Task<bool> IsPreviousPassword(UserManager<ApplicationUser> manager, string userId, string newPassword)
        {
            var user = await manager.FindByIdAsync(userId);
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>(
                                               new OptionsWrapper<PasswordHasherOptions>(
                                                   new PasswordHasherOptions()
                                                   {
                                                       CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3,
                                                        IterationCount =100000
                                                   })
                                           );

            // Convert the stored Base64 password to bytes
            //byte[] decodedHashedPassword = Convert.FromBase64String(newPassword);

            if (user.PreviousUserPasswords.OrderByDescending(x => x.CreateDate).
            Select(x => x.PasswordHash).Take(PASSWORD_HISTORY_LIMIT)
            .Where(x => ph.VerifyHashedPassword(user,x, newPassword) != PasswordVerificationResult.Failed).
            Any())
            {
                return true;
            }
            return false;
        }

        private async Task<bool> Register(UserDetailsViewModels uvm, ModelStateDictionary modelState)
        {
            try
            {
                if (modelState.IsValid)
                {
                    LoginSetupModel ssvm = _loginBAL.GetSecuritySetupDetails(GetSecurityID(uvm.GroupCode)).FirstOrDefault();
                    ApplicationUser user;
                    user = new ApplicationUser { UserName = uvm.UserName, NormalizedUserName= uvm.UserName, NormalizedEmail= uvm.Email, Email = uvm.Email };
                    UserManager<ApplicationUser> manager;
                    //_userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                    //manager = _userManager;
                    manager = GetUserManager(ssvm);

                    //skip password checking
                    ////manager.PasswordValidator = new PasswordValidator
                    ////{
                    ////    RequiredLength = 12,
                    ////    RequireNonLetterOrDigit = true,
                    ////    RequireDigit = true,
                    ////    RequireLowercase = true,
                    ////    RequireUppercase = true,

                    ////};

                    var result = await manager.CreateAsync(user, uvm.Password);
                    if (result.Succeeded)
                    {
                        //store password's history
                        await AddToPreviousPasswordsAsync(manager, user, user.PasswordHash);
                        return true;
                    }
                    AddErrors(result, modelState);
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally { }
            return false;
        }

        private void AddErrors(IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("", error.Description);
            }
        }

        //Harris Add to test Owin Authetication
        private List<Claim> GetClaims(string UserName, string UserType)
        {
            var claims = new List<Claim>();          
            claims.Add(new Claim(ClaimTypes.NameIdentifier, UserName));
            claims.Add(new Claim(ClaimTypes.Name, UserName));

            claims.Add(new Claim("user", ""));
            claims.Add(new Claim("role", "Member"));    
            
            // _httpContextAccessor.HttpContext.Session.SetString(CommonSetting.SessionId.LoginPhase, "1");
            claims.Add(new Claim("AuditKey", _httpContextAccessor.HttpContext.Session.GetString("AuditKey").ToString()));
            claims.Add(new Claim("UserType", UserType));

           // var signinManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            //claims.Add(new Claim(AspNet.Identity.SecurityStamp., await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture<string>()));
           

            var UserCategory = "1";
            if (UserType == "W" || UserType == "A")
            {
                UserCategory = "1";
            }
            else
            {
                if (UserType == "P")
                {
                    string Partner = (from a in _uow.Repository<Partner>().GetAsQueryable(x => x.PartnerName == UserName)
                                 select a.PartnerCode
                              ).FirstOrDefault();
                    if (Partner.stringParse() == "")
                    {
                        UserCategory = "1";
                    }
                    else
                    {
                        UserCategory = "0";
                    }//end if-else
                }//end if

                if (UserType == "C")
                {
                    string Customer = (from a in _uow.Repository<Customer>().GetAsQueryable(x => x.CustomerName == UserName)
                                 select a.CustomerCode
                                  ).FirstOrDefault();
                   
                    if (Customer.stringParse() == "")
                    {
                        UserCategory = "1";
                    }
                    else
                    {
                        UserCategory = "0";
                    }//end if-else
                }//end if
            }//end if-else
            claims.Add(new Claim("UserCategory", UserCategory));


            //var roles = new[] { "Admin", "User"};
            //var groups = new[] { "Admin", "User" };

            //foreach (var item in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, item));
            //}
            //foreach (var item in groups)
            //{
            //    claims.Add(new Claim(ClaimTypes.GroupClaimType, item));
            //}
            return claims;
        }

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get { return HttpContext.Current.GetOwinContext().Authentication; }
        //}

        private void TranslateAndAddPasswordPolicyError(IEnumerable<string> errors, LoginSetupModel lsm, ModelStateDictionary modelState, string modelErrorKey)
        {
            string errorstring = "<ul style=\"padding-left:40px\">";
            foreach(var error in errors)
            {
                foreach (var splitError in error.Split('.'))
                {
                    if (!String.IsNullOrEmpty(splitError))
                    {
                        errorstring += "<li style=\"list-style:disc; \">" + MapPasswordPolicyError(splitError, lsm) + "<li/>";
                    }
                }
            }
            errorstring += "<ul/>";

            modelState.AddModelError(modelErrorKey, errorstring);
        }

        private string MapPasswordPolicyError(string error, LoginSetupModel lsm)
        {
            string translatedError = String.Empty;

            if(error.Contains(CommonSetting.originalPasswordValidatorMessage.RequiredLength_1) 
                && error.Contains(CommonSetting.originalPasswordValidatorMessage.RequiredLength_2))
            {
                translatedError = String.Format(CommonSetting.translatedPasswordValidatorMessage.RequiredLength, lsm.RequiredLength);
            }
            else if (error.Contains(CommonSetting.originalPasswordValidatorMessage.RequireNonLetterOrDigit))
            {
                translatedError = CommonSetting.translatedPasswordValidatorMessage.RequireNonLetterOrDigit;
            }
            else if (error.Contains(CommonSetting.originalPasswordValidatorMessage.RequireDigit))
            {
                translatedError = CommonSetting.translatedPasswordValidatorMessage.RequireDigit;
            }
            else if (error.Contains(CommonSetting.originalPasswordValidatorMessage.RequireUppercase))
            {
                translatedError = CommonSetting.translatedPasswordValidatorMessage.RequireUppercase;
            }
            else if (error.Contains(CommonSetting.originalPasswordValidatorMessage.RequireLowercase))
            {
                translatedError = CommonSetting.translatedPasswordValidatorMessage.RequireLowercase;
            }
            else
            {
                translatedError = error;
            }

            return translatedError;
        }
        #endregion
   }//end class
}//end namespace