using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.Owin.Security;
//using Owin;
using GoMyShops.BAL;
using GoMyShops.Commons;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
//using System.Web.Security;
using System.IO;
using System.Security.Cryptography;
using GoMyShops.Models.Helpers;
using GoMyShops.BAL.MVCFilters;
using System.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Text;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;
using Azure.Core;
using GoMyShops.Data.Entity;
using GoMyShops.BAL.WebAPI;

namespace GoMyShops.Controllers
{
    [Authorize]
    public class AccountController : Web.Controllers.BaseController
    {
        #region Definations
        private readonly ILogger<AccountController> _logger;
        IUsersBAL _userBAL;
        ILoginBAL _loginBAL;
        ISmsBAL _smsBAL;
        ISignUpBAL _signUpBAL;
        IAnnouncementBAL _announcementBAL;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenServiceBAL _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;
        public IConfiguration _configuration { get; private set; }
        IWebHostEnvironment _hostingEnvironment;
        #endregion
        #region Constructor
        public AccountController(IHttpContextAccessor httpContextAccessor, ILogger<AccountController> logger, IWebHostEnvironment hostingEnvironment, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAnnouncementBAL announcementBAL, IUsersBAL userBAL, ILoginBAL loginBAL, ISmsBAL smsBAL, ISignUpBAL signUpBAL, IHttpClientFactory httpClientFactory, ITokenServiceBAL tokenService)
        {
            _userManager = userManager;
            _userBAL = userBAL;
            _loginBAL = loginBAL;
            _smsBAL = smsBAL;
            _announcementBAL = announcementBAL;
            _signUpBAL = signUpBAL;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        //TODO Harris Core-temp-off
        //public ApplicationUserManager UserManager {
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
        #endregion

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CspReport( CspReportRequest request)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                Request.Body.Position = 0;
                using (StreamReader inputStream = new StreamReader(Request.Body))
                {
                    string s = inputStream.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        CspReportRequest cspPost = JsonConvert.DeserializeObject<CspReportRequest>(s);

                        if (cspPost != null)
                        {
                            //now you can access properties of cspPost.CspReport
                            sb.Append("document-uri =");
                            sb.Append(cspPost.CspReport.DocumentUri);
                            sb.Append(System.Environment.NewLine);
                            sb.Append("violated-directive =");
                            sb.Append(cspPost.CspReport.ViolatedDirective);
                            sb.Append(System.Environment.NewLine);
                            sb.Append("original-policy =");
                            sb.Append(cspPost.CspReport.OriginalPolicy);
                            sb.Append(System.Environment.NewLine);
                            sb.Append("blocked-uri =");
                            sb.Append(cspPost.CspReport.BlockedUri);
                            sb.Append(System.Environment.NewLine);
                            sb.Append("source-file =");
                            sb.Append(cspPost.CspReport.SourceFile);
                            sb.Append(System.Environment.NewLine);
                            sb.Append("referrer =");
                            sb.Append(cspPost.CspReport.Referrer);
                            sb.Append(System.Environment.NewLine);
                            _logger.LogWarning(sb.ToString());
                            
                        }//end if

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
            finally { }
           

           // Logger.Warn($"CSP Violation: {request.CspReport.DocumentUri}, {request.CspReport.BlockedUri}");
            return Json(true);
            //return Ok();
        }        

        [AllowAnonymous]
        //[OutputCache(CacheProfile = "LayoutCache")]
        public async Task<ActionResult> PopulateAnnouncementList()
        {
            AnnouncementDisplayViewModels model = new AnnouncementDisplayViewModels();
            try
            {
                model=await _announcementBAL.GetAnnouncementListAsync(CommonSetting.AnnouncementType.Public);      
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
            }
            finally { }

            return PartialView(model);
        }



        //
        // GET: /Account/Login
        [AllowAnonymous]
       
        public ActionResult Login(string? returnUrl,string? type)
        {



            if (CurrentUser != null)
            {
                if (!CurrentUser.MultiLogin)
                {
                    if (User != null)
                    {
                        if (User.Identity != null)
                        {


                            if (User.Identity.IsAuthenticated)
                            {
                                return RedirectToAction("Index", "Home",new {type="1" });

                            }//end if
                        }//end if
                    }//end if
                }
            }



            //ViewBag.ReturnUrl = returnUrl;
            LoginModel model = new LoginModel();
            model.Msg = "";
            //var vvv = CurrentUser;
            model.MsgType = type;
         
            if (_configuration["AppSettings:SignUp"] == "1")
            {
                model.IsSignUp = true;
            }


            if (type=="1")
            {
                model.Msg = "An email with instructions to reset password has been sent. Please check your email inbox.";
            }//end if
            if (type == "2")
            {
                model.Msg = "Fail to send the email.";     
            }//end if
            if (type == "3")
            {
                model.Msg = "Password already reset, please login with new Password.";
            }//end if

            if (type == "4")
            {
                model.Msg = "Your Session has timed out due to inactivity.";
            }//end if
            if (type == "5")
            {
                model.Msg = "WARNING: An existing session has been detected using the same login credentials or your credential details have been changed.";
            }//end if
            if (type == "6")
            {
                model.Msg = "Your account has been locked out due to multiple failed login attempts. Please try again later.";
            }//end if
            if (type == "7")
            {
                model.Msg = "An email with instructions to login has been sent. Please check your email inbox.";
            }//end if
            if (type == "8")
            {
                model.Msg = "An email with verification link has been sent. Please check your email inbox.";
            }//end if
            if (type == "9")
            {
                model.Msg = "Please verify your account.";
            }//end if
            if (type == "10")
            {
                model.Msg = "Your email is successfully verified.";
            }//end if
            if (type == "11")
            {
                model.Msg = " Your email verification is invalid. Please log in to get verification Email again.";
            }//end if
            if (type == "12")
            {
                model.Msg = " There is an issue with your login. Please login again. ";
            }//end if
            return View("Login", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [AntiForgeryHandleError]
        //[PreventDuplicateRequest]
        public async Task<ActionResult> Login(LoginModel model, string? returnUrl)
        {
            //Check for SignUp
            SignUpVerifyViewModels svvm = new SignUpVerifyViewModels();
            svvm.SignUpName = model.UserName;
            _signUpBAL.IsSignUpUser(svvm);
            string IsRealUser = "0";

            if (svvm.IsSignUpUser)
            {
                if (!svvm.IsVerified)
                {
                    var isSendEmail = await _signUpBAL.SendSignUpEmail(model.UserName);

                    return RedirectToAction("Login", "Account", new { userName = model.UserName, type = "8" });

                }
                else
                {
                    //Check Lockout
                    if (await _userBAL.IsLockedOutAsync(model.UserName))
                    {
                        return RedirectToAction("Login", "Account", new { userName = model.UserName, type = "6" });
                        //return View("Login", new { userName = model.UserName, type = "6" });
                    }//end if

                    return RedirectToAction("LoginSignUp", "Account", new { userName = model.UserName });

                }//end if

                //ModelState.AddModelError("UserName", "The user name is incorrect.");
                //ModelState.AddModelError("Password", "The password is incorrect.");
                //return View(model);
          
            }
            else
            {
                //Check Lockout
                if (await _userBAL.IsLockedOutAsync(model.UserName))
                {
                    return RedirectToAction("Login", "Account", new { userName = model.UserName, type = "6" });
                    //return View("Login", new { userName = model.UserName, type = "6" });
                }//end if



                HttpContext.Session.Clear();
                HttpContext.Session.SetString("DummySession", "DummySession");
               

                var lrm = new LoginRedirectModel(_httpContextAccessor);
                LoginImageViewModels LoginPhraseModel = new LoginImageViewModels();
                LoginPhraseModel.UserName = model.UserName;
                lrm = await _userBAL.LoginPhrase(LoginPhraseModel, ModelState);



                if (LoginPhraseModel.IsUser)
                {
                    TempData["IsRealUser"] = "1";
                    IsRealUser = "1";
                }
                else
                {
                    TempData["IsRealUser"] = "0";
                    IsRealUser = "0";
                }//end if-else

                return RedirectToAction("LoginPhrase", "Account", new { userName = model.UserName, isRealUser= IsRealUser });
            }//end if-else

            
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginSignUp(string userName)
        {
            SignUpLoginViewModels model = new SignUpLoginViewModels();        
            model.UserName = userName;           
            return View(model);
         
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]      
        public async Task<ActionResult> LoginSignUp(SignUpLoginViewModels model1, string? returnUrl)
        {
            LoginModel model = new LoginModel();
            model.UserType =CommonSetting.UserType.OnBoarding;
            model.UserName = model1.UserName;
            model.Password = model1.Password;

            var msInvalidList = ModelState.Errors();


            if (ModelState.IsValid)       
            {
                HttpContext.Session.Clear();
               // HttpContext.Session.SetString("DummySession", "DummySession");
               //_httpContextAccessor.HttpContext= HttpContext;
               // _httpContextAccessor.HttpContext.Session.SetString("DummySession", "DummySession");

                var lrm = new LoginRedirectModel(_httpContextAccessor);            
                lrm = await _userBAL.LoginSignUp(model, ModelState);

                _isError = lrm.isError;

                if (lrm.IsRedirect)
                {
                    lrm.Code = System.Web.HttpUtility.UrlEncode(lrm.Code);
                    return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName });

                }//end if

                //Check Lockout
                if (lrm.Type == 1)
                {
                    //return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName });
                    return RedirectToAction("Login", "Account", new { userName = lrm.UserName, type = "6" });

                }//end if

                if (ModelState.IsErrors(""))
                {
                    model1.Message = lrm.Message;
                    return View(model1);
                }//end if

                if (_isError)
                {
                    return RedirectToAction("Login", "Account", new { });
                }//end if


                _loginBAL.SetAuditHeader(model.UserName);
                // await _userBAL.IdentitySigninAsync(model);
                await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                HttpContext.Session.SetString(CommonSetting.SessionId.LoginPhase, "1");
                return RedirectToAction("DefaultIndex", "Home", new {});
                //return RedirectToLocal(returnUrl);


            }//end if

            ModelState.AddModelError("UserName", "The user name is incorrect.");
            ModelState.AddModelError("Password", "The password is incorrect.");
            return View(model1);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginPhrase(string userName ,string isRealUser)
        {
            LoginImageViewModels model = new LoginImageViewModels();
            model.Phrase = "";
            model.Code = "123456";
            model.UserName = userName;
            //if (isRealUser)//TempData["IsRealUser"] != null)
            //{
            //    var IsRealUser = TempData["IsRealUser"].ToString();

                if (isRealUser == "1")
                {
                    _userBAL.getPhraseDetail(model);

                    var wwwroot = _hostingEnvironment.WebRootPath;

                    DirectoryInfo directoryInfo = new DirectoryInfo(wwwroot + CommonSetting.LoginImagePath);
                    FileInfo[] Images;

                    Images = directoryInfo.GetFiles();
                    if (Images!=null)
                    {
                        int Count = Images.Count();

                        if (model.ImageCode.stringParse() != "")
                        {
                            model.Src = String.Format(@"{0}{1}/{2}{3}", "~", CommonSetting.LoginImagePath, model.ImageCode + ".png", "");
                        }
                        else
                        {
                            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                            {
                                byte[] randomNumber = new byte[4];//4 for int32
                                rng.GetBytes(randomNumber);
                                int value = BitConverter.ToInt32(randomNumber, 0);
                                value = value >= 0 ? value : -value;
                                int randomSelect = value % Count;
                                model.Src = String.Format(@"{0}{1}/{2}{3}", "~",CommonSetting.LoginImagePath, Images[randomSelect].Name, "");
                            }
                        }//end if-else


                    }//end if

  
                }
                else
                {
                    RandomImage(model);
                }

                return View(model);
            //}
            //else
            //{
            //    RandomImage(model);
            //    return View(model);
            //}//end if-else

            
        }
               
        private void RandomImage(LoginImageViewModels model)
        {

            try
            {
                var wwwroot = _hostingEnvironment.WebRootPath;

                DirectoryInfo directoryInfo = new DirectoryInfo(wwwroot + CommonSetting.LoginImagePath);
                FileInfo[] Images;

                Images = directoryInfo.GetFiles();
                if (Images != null)
                {
                    int Count = Images.Count();

                    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                    {
                        byte[] randomNumber = new byte[4];//4 for int32
                        rng.GetBytes(randomNumber);
                        int value = BitConverter.ToInt32(randomNumber, 0);
                        value = value >= 0 ? value : -value;
                        int randomSelect = value % Count;
                        model.Src = String.Format(@"{0}{1}/{2}{3}", "~", CommonSetting.LoginImagePath, Images[randomSelect].Name, "");
                    }//end using
                }//end if
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
        
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]       
        //[PreventDuplicateRequest]
        public async Task<ActionResult> LoginPhrase(LoginImageViewModels model1 , string? returnUrl)
        {
            // bool isLoginError = true;
            //model1.Code = "123456";
            LoginModel model = new LoginModel();
            model.UserType = CurrentUser.UserType;
            model.UserName = model1.UserName;
            model.Password = model1.Password;
           
            var msInvalidList=ModelState.Errors();


            //if (ModelState.IsValid)
            if (!ModelState.IsErrors("Phrase"))
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("DummySession", "DummySession");
                //Session.Abandon();

                var lrm = new LoginRedirectModel(_httpContextAccessor);
                //LoginImageViewModels LoginPhraseModel = new LoginImageViewModels();
                lrm = await _userBAL.Login(model, ModelState);


                _isError = lrm.isError;

                if (lrm.IsRedirect)
                {
                    lrm.Code = System.Web.HttpUtility.UrlEncode(lrm.Code);
                    return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName });

                }//end if

                //Check Lockout
                if (lrm.Type == 1)
                {
                    //return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName });
                    return RedirectToAction("Login", "Account", new { userName = lrm.UserName, type = "6" });

                }//end if

                if (ModelState.IsErrors("Phrase"))
                {
                    model1.Message = lrm.Message;
                    return View(model1);
                }//end if

                if (_isError)
                {
                    //MessageDanger("The user name or password provided is incorrect.", true, true);               
                    return RedirectToAction("Login", "Account", new { });

                    //return View(model1);
                }//end if

              


                if (model.UserType == CommonSetting.UserType.Admin)
                {
                    // _userBAL.SignInOrTwoFactor(model.UserName,model.UserType);
                    string code = await _userBAL.GenerateTwoFactorTokenAsync(TokenOptions.DefaultPhoneProvider, model.UserName);

                    string SendSMSVendor = _configuration["AppSettings:SendSMSVendor"];
                    //string SendSMSVendor = _configuration.GetSection(.AppSettings["SendSMSVendor"];
                    if (SendSMSVendor.stringParse() == "")
                    {
                        return RedirectToAction("VerifyCode", new { provider = "", code = code, userName = model.UserName });

                    }
                    else
                    {
                        _smsBAL.SendUserTac(model.UserName, code);
                        return RedirectToAction("VerifyCode", new {userName = model.UserName });

                    }//end if-else


                }//end if

                _loginBAL.SetAuditHeader(model.UserName);
                await _userBAL.IdentitySigninAsync(model);

                if (_userBAL.IsPhrased(model.UserName))
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    return RedirectToAction("EditLoginImage", "User");
                }//end if-else
           
            }//end if

            ModelState.AddModelError("UserName", "The user name is incorrect.");
            ModelState.AddModelError("Password", "The password is incorrect.");
            return View(model1);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public ActionResult VerifyCode(string provider,string code,string userName, string returnUrl)
        {          
            var model = new VerifyCodeViewModel();
            model.UserName = userName;
            model.ReturnUrl = returnUrl;
            model.PhoneCode = "An SMS with the Verification Code has been sent to " + _smsBAL.GetPhoneCode(userName) + ".";
            //model.Provider = provider;
            //model.Code = code;

            return View(model);
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]       
        //[PreventDuplicateRequest]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            //SignInStatus status =new SignInStatus();

            //bool HasBeenVerified = false;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //HasBeenVerified =await _userBAL.HasBeenVerified();

            //if (HasBeenVerified)
            //{
            //    ModelState.AddModelError("Code", "The code can not be reused, Please Log in again.");
            //}//end if


            var status = await _userBAL.VerifyTwoFactorAsync(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //TODO Send sms code
            if (status == CommonSetting.SignInStatus.Success)
            {
                var newtokens = await _tokenService.LoginFromWebApp(model.UserName);
               
                if (newtokens.IsError)
                {
                    ModelState.AddModelError("Web Resources", "Unable to connect to Resource's website.");
                    return View(model);
                }
                else
                {
                    Response.Cookies.Append("X-Access-Token", newtokens.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                    Response.Cookies.Append("X-Username", model.UserName, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                    Response.Cookies.Append("X-Refresh-Token", newtokens.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });

                }//end if


                //TODO harris Call WebAPI for login token
                ////var id1 = JsonConvert.SerializeObject(model.UserName);
                ////var content = new StringContent(id1, Encoding.UTF8, "application/json");
                ////content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                ////var request1 = new HttpRequestMessage
                ////   (HttpMethod.Get, $"https://localhost:7097/Account/LoginFromWebApp?userName={model.UserName}");


                ////using (var httpClient = _httpClientFactory.CreateClient())
                ////{
                ////    httpClient.DefaultRequestHeaders.Clear();
                ////    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ////    var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7097/Account/LoginFromWebApp", content);

                ////    if (httpResponseMessage.IsSuccessStatusCode)
                ////    {
                ////        //Set-Cookie

                ////        var values = httpResponseMessage.Headers;
                ////        if (values == null)
                ////        {
                ////            ModelState.AddModelError("Web Resources", "Unable to connect to Resource's website.");
                ////        }
                ////        else {
                ////            Response.Cookies.Append("X-Access-Token", httpResponseMessage.Headers.GetValues("X-Access-Token").FirstOrDefault(""), new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                ////            Response.Cookies.Append("X-Username", httpResponseMessage.Headers.GetValues("X-Username").FirstOrDefault(""), new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                ////            Response.Cookies.Append("X-Refresh-Token", httpResponseMessage.Headers.GetValues("X-Refresh-Token").FirstOrDefault(""), new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });

                ////        }

                ////    }
                ////}//end using

                if (_userBAL.IsPhrased(model.UserName))
                {
                    _loginBAL.SetAuditHeader(model.UserName);
                  // var a= await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, false, false);

                    //await _userBAL.SignInOrTwoFactorAsync(model.UserName,"");
                    return RedirectToLocal(model.ReturnUrl);
                }
                else
                {
                    _loginBAL.SetAuditHeader(model.UserName);
                    //await _userBAL.IdentitySigninAsync(model.UserName, "");
                    return RedirectToAction("EditLoginImage", "User", new { UserName = model.UserName });
                }//end if-else
          
            }
            else if (status == CommonSetting.SignInStatus.LockedOut)
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }
            else
            {
                ModelState.AddModelError("Code", "wrong verification code.");
            }//end if-else
            return View(model);
            //var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(model.ReturnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid code.");
            //        return View(model);
            //}
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GetConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmUser", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmUser
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmUser(string userId, string code)
        {
            if (userId == null || code == null) 
            {
                return View("Error");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync( userId), code);
            if (result.Succeeded)
            {
                return View("ConfirmUser");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPasswordGet()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();
            model.UserName = "";
            model.Email = "";
            return View("ForgotPassword", model);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]       
        //[PreventDuplicateRequest]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check for SignUp
                SignUpVerifyViewModels svvm = new SignUpVerifyViewModels();
                svvm.SignUpName = model.UserName;
                _signUpBAL.IsSignUpUser(svvm);

                if (svvm.IsSignUpUser)
                {
                    if (!svvm.IsVerified)
                    {
                        return RedirectToAction("Login", "Account", new { type = "9" });
                    }//end if
                }//end if

                var lrm = new LoginRedirectModel(_httpContextAccessor);
                lrm = await _userBAL.ForgotPassword(model, ModelState);
                _isError = lrm.isError;

                if (lrm.IsRedirect)
                {
                    if (lrm.isError)
                    {
                        Danger(lrm.returnString, true, true);
                        //return RedirectToAction("Login", "Account");
                        //return View(model);
                    }//end if
                    return RedirectToAction(lrm.RedirectAction, "Account", new { type = "1" });

                }//end if

                if (!ModelState.IsValid)
                {
                    Danger(lrm.returnString, true, true);
                    return RedirectToAction("Login", "Account",new { type = "2" });
                    //return View(model);
                }//end if

                if (_isError)
                {
                    ModelState.AddModelError("", "Resend Email fail.");
                    Danger("Resend Email fail.", true, true);
                    return View(model);
                }//end if

                if (lrm.isSuccess)
                {
                    var a= RedirectToAction("Login", "Account", new { type = "1" });
                   // var b = RedirectToAction("Login", "Account", new { type = "1" });
                    //return RedirectToAction(nameof(Login), new { type = "1" });
                    return a;//RedirectToAction("Login", "Account", new { type = "1" });
                }//end if


                return View();
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        ////
        //// GET: /Account/ResetPassword
        //[AllowAnonymous]
        //public ActionResult ResetPassword(string code)
        //{
        //    if (code == null) 
        //    {
        //        return View("Error");
        //    }
        //    return View();
        //}

        [AllowAnonymous]
        public async Task<ActionResult> MenuResetPassword(string code, string userName)
        {
            ResetPasswordViewModel a = new ResetPasswordViewModel();
            //a.Code = code;
            a.EmailCode = System.Web.HttpUtility.UrlEncode(await _userBAL.GetResetPasswordCodeAsync(User.Identity.Name, ModelState));
            a.Email = User.Identity.Name;
            a.Type = "3";
            return View("ResetPassword", a);
        }

        //[AllowAnonymous]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code, string userName,string partial)
        {
            ResetPasswordViewModel a = new ResetPasswordViewModel();
            a.EmailCode = code;// System.Web.HttpUtility.UrlEncode(code);
            a.Email = userName;
            
            if (a.Email.IsNullOrEmptyString())
            {
                a.Email = User.Identity.Name;
            }
                       
            if (partial == "Y")
            {
                //a.Code = System.Web.HttpUtility.UrlDecode(code);
                return code == null ? PartialView("Error") : PartialView(a);
            }
            
            return code == null ? View("Error") : View(a);
        }

        [AllowAnonymous]
        public ActionResult ForgotResetPassword(string code, string userName, string partial)
        {
            ResetPasswordViewModel a = new ResetPasswordViewModel();
            a.EmailCode = code;// System.Web.HttpUtility.UrlEncode(code);
            a.Email = userName;

            if (a.Email.IsNullOrEmptyString())
            {
                a.Email = User.Identity.Name;
            }

            if (partial == "Y")
            {
                //a.Code = System.Web.HttpUtility.UrlDecode(code);
                return code == null ? PartialView("Error") : PartialView(a);
            }

            return code == null ? View("Error") : View(a);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]     
        public async Task<ActionResult> ForgotResetPassword(ResetPasswordViewModel model)
        {
            model.EmailCode = System.Web.HttpUtility.UrlDecode(model.EmailCode);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var lrm = new LoginRedirectModel(_httpContextAccessor);         
            lrm = await _userBAL.ForgotResetPassword(model, ModelState);
            _isError = lrm.isError;

            if (lrm.IsRedirect)
            {
                //if (!_isError)
                //{
                //    _smsBAL.SendUserProfile(model.Email, CommonSetting.SmsMessage.TypePasswordChange);
                //}//end if

                return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName, type = "3" });

            }//end if

            if (!ModelState.IsValid)
            {
                return View();
            }//end if

            if (_isError)
            {
                Danger("User password reset fail.", true, true);
                return View();
            }//end if

            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginResetPassword(string code, string userName, string partial)
        {
            ResetPasswordViewModel a = new ResetPasswordViewModel();
            a.EmailCode = code;// System.Web.HttpUtility.UrlEncode(code);
            a.Email = userName;

            if (a.Email.IsNullOrEmptyString())
            {
                a.Email = User.Identity.Name;
            }

            if (partial == "Y")
            {
                //a.Code = System.Web.HttpUtility.UrlDecode(code);
                return code == null ? PartialView("Error") : PartialView(a);
            }

            return code == null ? View("Error") : View(a);
        }

        [AllowAnonymous]
        public async Task<ActionResult> SignUpVerify(string code, string userName)
        {
            SignUpVerifyViewModels model = new SignUpVerifyViewModels();
            model.IsVerified = false;
            model.SignUpName = userName;
            model.Code = code;

            bool isVerified = await _userBAL.EmailVerified(model);

            if (isVerified)
            {
                model.IsVerified = true;
                return RedirectToAction("Login", "Account", new { userName = userName, type = "10" });
            }
            else
            {
                return RedirectToAction("Login", "Account", new { userName = userName, type = "11" });
            }//end if-else
            //return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[PreventDuplicateRequest]
        public async Task<ActionResult> LoginResetPassword(ResetPasswordViewModel model)
        {
            model.EmailCode = System.Web.HttpUtility.UrlDecode(model.EmailCode);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var lrm = new LoginRedirectModel(_httpContextAccessor);

            lrm = await _userBAL.ResetPassword(model, ModelState);
            _isError = lrm.isError;

            if (lrm.IsRedirect)
            {
                //if (!_isError)
                //{
                //    _smsBAL.SendUserProfile(model.Email, CommonSetting.SmsMessage.TypePasswordChange);
                //}//end if

                return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName, type="3" });

            }//end if

            if (!ModelState.IsValid)
            {
                MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false, "The password you entered is invalid"), false, true);
                return View(model);
            }//end if

            if (_isError)
            {
                MessageDanger("User password reset fail.", true, true);
                return View();
            }//end if

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[PreventDuplicateRequest]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            model.EmailCode = System.Web.HttpUtility.UrlDecode(model.EmailCode);

            if (model.Type.IsNullOrEmptyString())
            {
                model.Type = "1";
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var lrm = new LoginRedirectModel(_httpContextAccessor);

            lrm = await _userBAL.ResetPassword(model, ModelState);
            _isError = lrm.isError;

            if (lrm.IsRedirect)
            {
                //if (!_isError)
                //{
                //    _smsBAL.SendUserProfile(model.Email, CommonSetting.SmsMessage.TypePasswordChange);
                //}//end if
                //return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName, type = "1" });
                return RedirectToAction(lrm.RedirectAction, "Account", new { userName = lrm.UserName, type = model.Type });

            }//end if

            if (!ModelState.IsValid)
            {
                MessageDanger(ModelStateHelper.Errorsstr(ModelState, true, false, "The password you entered is invalid"), false, true);
                return View(model);
            }//end if

            if (_isError)
            {
                MessageDanger("User password reset fail.", true, true);
                return View();
            }//end if

            return View();
        }

        [AllowAnonymous]
       
        public async Task<ActionResult> ResetEmailGetAsync()
        {
            ResetEmailViewModel a = new ResetEmailViewModel();
            a.UserName = User.Identity.Name;
            a.OldEmail =await _userBAL.GetEmailAsync(a.UserName, ModelState);
            // a.Code = code;
            //a.Email = userName;
            //if (a.Email.IsNullOrEmptyString())
            //{
            //    a.Email = User.Identity.Name;
            //}
            //return code == null ? View("Error") : View(a);
            return View("ResetEmail", a);
        }

        
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[PreventDuplicateRequest]
        public async Task<ActionResult> ResetEmail(ResetEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView();
            }

            var lrm = new LoginRedirectModel(_httpContextAccessor);
            //var model = new ResetEmailViewModel();
            model.UserName = User.Identity.Name;
            //model.Email = newEmail;
            lrm = await _userBAL.ResetEmail(model, ModelState);
            _isError = lrm.isError;

            if (lrm.IsRedirect)
            {
                return RedirectToAction(lrm.RedirectAction, "Account", new { code = lrm.Code, userName = lrm.UserName });

            }//end if

            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }//end if

            if (_isError)
            {
                Danger("Invalid Email.", true, true);
                return PartialView(model);
            }//end if

            if (lrm.isSuccess)
            {
                MessageSuccess(string.Format(CommonSetting.UserResetEmailArgs,User.Identity.Name), true, true);
                var OldEmail = model.Email;
                ModelState.Clear();
                model.Email = "";
                model.ConfirmEmail = "";
                model.OldEmail = OldEmail;
                return PartialView(model);
            }//end if


            return PartialView();
        }


        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Email);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("", "No user found.");
        //            return View();
        //        }
        //        IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("ResetPasswordConfirmation", "Account");
        //        }
        //        else
        //        {
        //            AddErrors(result);
        //            return View();
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            //Todo Harris (Test) Modify Core
            IdentityResult result = await _userManager.RemoveLoginAsync(await _userManager.GetUserAsync(User), providerKey, "Mifun");
            if (result.Succeeded)
            {

                var user = await _userManager.GetUserAsync(User);//  FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _signInManager.SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(await _userManager.GetUserAsync(User), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.GetUserAsync(User);// _userManager.FindByIdAsync(User.Identity.GetUserId());
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelStateEntry state = ModelState["OldPassword"];               
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await _userManager.AddPasswordAsync(await _userManager.GetUserAsync(User), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //////
        ////// POST: /Account/ExternalLogin
        ////[HttpPost]
        ////[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult ExternalLogin(string provider, string returnUrl)
        ////{
        ////    // Request a redirect to the external login provider
        ////    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        ////}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var user = await UserManager.FindAsync(loginInfo.Login);
        //    if (user != null)
        //    {
        //        await SignInAsync(user, isPersistent: false);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then prompt the user to create an account
        //        ViewBag.ReturnUrl = returnUrl;
        //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        ////
        //// POST: /Account/LinkLogin
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        //}

        ////
        //// GET: /Account/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //    }
        //    IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Manage");
        //    }
        //    return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
        //        IdentityResult result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInAsync(user, isPersistent: false);
                        
        //                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //                // Send an email with this link
        //                // string code = await UserManager.GetConfirmationTokenAsync(user.Id);
        //                // var callbackUrl = Url.Action("ConfirmUser", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //                // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");
                        
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            var info = _loginBAL.SetAuditHeaderTimeOut();
            await _signInManager.SignOutAsync();
            //AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public JsonResult SetAuditHeaderTimeOut()
        {
            //Harris add
            var info = _loginBAL.SetAuditHeaderTimeOut();
            //AuthenticationManager.SignOut();
            //FormsAuthentication.SignOut();
            //End Harris add
            return Json(new { success = CommonSetting.SuccessModifyRecords });
        }


        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //[ChildActionOnly]
        //public ActionResult RemoveAccountList()
        //{
        //    var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
        //    ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
        //    return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && _userManager != null)
        //    {
        //        _userManager.Dispose();
        //        _userManager = null;
        //    }
        //    base.Dispose(disposing);
        //}



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        //private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private bool HasPassword()
        {
            var user = _userManager.GetUserId(User); //.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                //Todo Harris (Test) Modify Core
                //return user.PasswordHash != null;
                return true;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { type = "1" });
            }
        }

        //private class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}
        #endregion
    }
}