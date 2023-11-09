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
using Microsoft.Extensions.Logging;

namespace GoMyShops.BAL
{
    public interface ISignUpBAL
    {
        Task<bool> Create(SignUpDetailsViewModels model, ModelStateDictionary modelState);
        List<SignUpListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total);
        SignUpDetailsViewModels getDetail(string id, string id2);
        bool Edit(SignUpDetailsViewModels model, ModelStateDictionary modelState);
        Task<bool> SendSignUpEmail(string SignUpName);
        void IsSignUpUser(SignUpVerifyViewModels model);
        bool AssignSignUpCompanyDetails(MerchantOnboardingDetailsViewModels model);
    }

    public class SignUpBAL : BaseBAL, ISignUpBAL
    {
        #region Definations
        private readonly ILogger<SignUpBAL> _logger;
        IUnitOfWork _uow;
        IServicesBAL _servicesBAL;
        IUsersBAL _userBAL;
        //IEmailFactory _emailFactory;
        IEmailBAL _emailBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public SignUpBAL(IHttpContextAccessor httpContextAccessor, ILogger<SignUpBAL> logger, IUnitOfWorkFactory uowFactory, IServicesBAL servicesBAL, IUsersBAL userBAL, IEmailBAL emailBAL) : base()
        {
            _uow = uowFactory.Create();
            _servicesBAL = servicesBAL;
            _userBAL = userBAL;
            _emailBAL = emailBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions
        public List<SignUpListViewModels> getData(int offset, int limit, string search, string sort, string order, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, ref int total)
        {
            try
            {
                string srcSignUpName = param1;
                string srcCompanyName = param2;
                string srcContactNo = param3;
                string srcEmail = param4;
                string srcCreateDate = param5;
                string srcStatus = param6;

                var StartDate = CommonFunctionsBAL.ParseStandardDateFormat(srcCreateDate, false);

                var list = new List<SignUpListViewModels>();
                var statusRepo = _uow.Repository<StatusSU>();
                var queryableList = (from a in _uow.Repository<SignUp>().GetAsQueryable()
                                     let status = statusRepo.dbSet.FirstOrDefault(p2 => a.Status == p2.Status)
                                     select new SignUpListViewModels
                                     {
                                          CompanyName = a.CompanyName,
                                          CompanyRegistrationNumber = a.CompanyRegistrationNumber,
                                          ContactNo = a.ContactNo,
                                          CreatedTime=a.CreatedTime,
                                          SignUpName=a.SignUpName,
                                          Email=a.Email,
                                          Status = a.Status,
                                          StatusName= status==null? a.Status : status.StatusName
                                     }
                                    )
                    .WhereIf(!srcSignUpName.IsNullOrEmptyString(), tags => tags.SignUpName.Contains(srcSignUpName))
                    .WhereIf(!srcCompanyName.IsNullOrEmptyString(), tags => tags.CompanyName.Contains(srcCompanyName))
                    .WhereIf(!srcContactNo.IsNullOrEmptyString(), tags => tags.ContactNo.Contains(srcContactNo))
                    .WhereIf(!srcEmail.IsNullOrEmptyString(), tags => tags.CompanyRegistrationNumber.Contains(srcEmail))
                    .WhereIf(!srcStatus.IsNullOrEmptyString(), tags => tags.Status == srcStatus)
                    .WhereIf(!srcCreateDate.IsNullOrEmptyString(), tags => tags.CreatedTime >= StartDate)
                    ;

                if (CurrentUser.UserType == CommonSetting.UserType.OnBoarding)
                {
                    queryableList = queryableList.Where(x => x.SignUpName == CurrentUser.Name)
                                                .Where(x => x.Status == CommonSetting.Status.Active ||
                                                x.Status == CommonSetting.Status.Pending);
                }//end if

                if (!sort.IsNullOrEmptyString())
                {
                    switch (sort)
                    {
                        case "sCreatedTime":
                            sort = "CreatedTime";
                            break;
                        default:
                            break;
                    }//end switch   

                    list = CustomExpression.IQueryable<SignUpListViewModels>(queryableList, sort, "tags", order)
                             .Skip((offset / limit) * limit).Take(limit)
                             .ToList();
                }
                else
                {
                    list = queryableList.OrderByDescending(tags => tags.CreatedTime)
                                        .Skip((offset / limit) * limit).Take(limit)
                                        .ToList();

                }//end if-else

                total = queryableList.Count();
                List<SignUpListViewModels> b = (from a in list
                                                        select new SignUpListViewModels
                                                        {
                                                            CompanyName = a.CompanyName,
                                                            CompanyRegistrationNumber = a.CompanyRegistrationNumber,
                                                            ContactNo = a.ContactNo,
                                                           // CreatedTime = a.CreatedTime,
                                                            sCreatedTime = CommonFunctionsBAL.ParseStandardDateFormat(a.CreatedTime, true, false),
                                                            SignUpName = a.SignUpName,
                                                            Email = a.Email,
                                                            StatusName=a.StatusName,
                                                            //Status = a.Status,
                                                            DetailJson = new ActionsListDetails(a.SignUpName, "", "", "", ""),
                                                            EditJson = new ActionsListDetails(a.SignUpName, "", "", "", ""),

                                                        }).ToList();
                return b;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return new List<SignUpListViewModels>();
        }

        public async Task<bool> Create(SignUpDetailsViewModels model, ModelStateDictionary modelState)
        {
            bool isRegister;
            try
            {
                //if (_userBAL.isUser(model.SignUpName))
                //{
                //    modelState.AddModelError("SignUpName", string.Format(CommonSetting.Messages.CodeExistArgs, "Sign Up Name", model.SignUpName));
                //}//end if

                var TGcount = _uow.Repository<SignUp>().Get(x => x.SignUpName == model.SignUpName).Count();

                if (TGcount > 0)
                {
                    modelState.AddModelError("SignUpName", string.Format(CommonSetting.Messages.CodeExistArgs, "Sign Up Name", model.SignUpName));
                }//end if

                var TGcountEmail = _uow.Repository<SignUp>().Get(x => x.Email == model.Email).Count();

                if (TGcountEmail > 0)
                {
                    modelState.AddModelError("Email", string.Format(CommonSetting.Messages.CodeExistArgs, "Email", model.Email));
                }//end if


                var TGcount1 = _uow.Repository<User>().Get(filter: x => x.Username == model.SignUpName).Count();

                if (TGcount1 > 0)
                {
                    modelState.AddModelError("SignUpName", string.Format(CommonSetting.Messages.CodeExistArgs, "Sign Up Name", model.SignUpName));
                }//end if

                if (modelState.IsValid)
                {
                    var context = _httpContextAccessor.HttpContext;
                    var request =_httpContextAccessor.HttpContext.Request;
                    //HttpRequestBase request = HttpContext.Current.Request.RequestContext.HttpContext.Request;
                    var IPAddress = request.Headers["HTTP_X_FORWARDED_FOR"] ==""? "":  request.Host.ToUriComponent();

                    SignUp insertRow = new SignUp();
                    var insertRepo = _uow.Repository<SignUp>();

                    insertRow.SignUpName = model.SignUpName;
                    insertRow.Email = model.Email;
                    insertRow.ContactNo = model.ContactNo;
                    insertRow.CompanyRegistrationNumber = model.CompanyRegistrationNumber;
                    insertRow.CompanyName = model.CompanyName;
                    insertRow.IsVerified = false;
                    insertRow.IPAddress = IPAddress;
                    insertRow.Status = CommonSetting.Status.Pending;
                    insertRow.CreatedBy = model.SignUpName;
                    insertRow.CreatedTime = DateTime.Now;
                    insertRow.ModifiedBy = model.SignUpName;
                    insertRow.ModifiedTime = DateTime.Now;
                    insertRepo.Insert(insertRow);
                    //isError = _uow.Save();
                   // isError = _uow.Save();
                    if (!isError)
                    {
                        //var a = _emailFactory.Create();
                        //isError = await a.SendSignUpEmailAsync(CommonSetting.EmailSendType.SuccessSignUp, model.SignUpName);


                        isRegister = await _userBAL.Register(model.SignUpName, model.Email, model.Password, modelState);
                        if (!isRegister)
                        {
                            //var deleteEntity = _uow.Repository<SignUp>().GetAsQueryable(x => x.SignUpName == model.SignUpName).FirstOrDefault();
                            //if (deleteEntity != null)
                            //{
                            //    _uow.Repository<SignUp>().Delete(deleteEntity);
                            //    isError = _uow.Save();
                            //}//end if
                            _uow.DetachAll();
                            return false;
                        }
                        else
                        {
                            isError = _uow.Save();
                            if (isError)
                            {
                                await _userBAL.UnRegisterAsync(model.SignUpName);
                            }//end if
                            else
                            {
                                //var a = _emailFactory.Create();
                                isError = await _emailBAL.SendSignUpEmailAsync(CommonSetting.EmailSendType.SuccessSignUp, model.SignUpName);
                                if (isError)
                                {
                                    await _userBAL.UnRegisterAsync(model.SignUpName);
                                }//end if
                            }
                        }//end if-else
                    }//end if
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

        public SignUpDetailsViewModels getDetail(string id, string id2)
        {
            SignUpDetailsViewModels model = new SignUpDetailsViewModels();
            try
            {
                var statusRepo = _uow.Repository<StatusSU>();
                model = (from a in _uow.Repository<SignUp>().GetAsQueryable(x => x.SignUpName == id)
                         let status = statusRepo.dbSet.FirstOrDefault(p2 => a.Status == p2.Status)
                         select new SignUpDetailsViewModels()
                         {
                             CompanyName = a.CompanyName,
                             CompanyRegistrationNumber = a.CompanyRegistrationNumber,
                             ContactNo = a.ContactNo,                         
                             SignUpName = a.SignUpName,
                             Email = a.Email,
                             Status = status == null ? a.Status : status.StatusName,
                             CheckBoxStatus = a.Status == "1" ? true : false,
                             CreatedBy = a.CreatedBy,
                             CreatedTime = a.CreatedTime,
                             ModifiedBy = a.ModifiedBy,
                             ModifiedTime = a.ModifiedTime,
                         }).FirstOrDefault();

                model.sCreatedTime = CommonFunctionsBAL.ParseStandardDateFormat(model.CreatedTime, true, true);
                model.sModifiedTime = model.ModifiedTime.HasValue ? CommonFunctionsBAL.ParseStandardDateFormat(model.ModifiedTime.GetValueOrDefault(), true, true) : " - ";

                model.EditJson = JsonConvert.SerializeObject(new ActionsListDetails(model.SignUpName, "", "", "", ""));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return model.IsNullThenNew();
        }

        public bool Edit(SignUpDetailsViewModels model, ModelStateDictionary modelState)
        {
            try
            {
                //var TGcount = _uow.Repository<SignUp>().Get(x => x.SignUpName == model.SignUpName && x.SignUpName != model.SignUpName).Count();

                //if (TGcount > 0)
                //{
                //    modelState.AddModelError("SignUpName", string.Format(CommonSetting.Messages.CodeExistArgs, "SignUpName", model.SignUpName));
                //}//end if

                var TGcountEmail = _uow.Repository<SignUp>().Get(x => x.SignUpName != model.SignUpName && x.Email == model.Email).Count();

                if (TGcountEmail > 0)
                {
                    modelState.AddModelError("Email", string.Format(CommonSetting.Messages.CodeExistArgs, "Email", model.Email));
                }//end if

                if (!modelState.IsValid)
                {
                    return false;
                }//end if


                var infos = _uow.Repository<SignUp>().GetAsQueryable(x =>
                                     x.SignUpName == model.SignUpName
                                    ).FirstOrDefault();
                if (!infos.IsNullOrEmpty())
                {
                    var entry = _uow.Context.Entry(infos);
                  
                    entry.Property(u => u.Email).CurrentValue = model.Email;
                    entry.Property(u => u.ContactNo).CurrentValue = model.ContactNo;
                    entry.Property(u => u.CompanyRegistrationNumber).CurrentValue = model.CompanyRegistrationNumber;
                    entry.Property(u => u.CompanyName).CurrentValue = model.CompanyName;                                
                    entry.Property(u => u.ModifiedBy).CurrentValue = CurrentUser.Name; ;
                    entry.Property(u => u.ModifiedTime).CurrentValue = DateTime.Now;

                    if (CurrentUser.UserType != CommonSetting.UserType.OnBoarding)
                    {
                        entry.Property(u => u.Status).CurrentValue = CommonFunctions.Iif(model.CheckBoxStatus == true, CommonSetting.Status.Active, CommonSetting.Status.Inactive);
                    }//end if

                    _uow.Repository<SignUp>().Update(entry);
                    _uow.Repository<SignUp>().Update(infos);
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

        public async Task<bool> SendSignUpEmail(string SignUpName)
        {
            try
            {
                //var a = _emailFactory.Create();
                isError = await _emailBAL.SendSignUpEmailAsync(CommonSetting.EmailSendType.SuccessSignUp, SignUpName);
            }
            catch (Exception ex)
            {
                isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return isError;
        }

        public void IsSignUpUser(SignUpVerifyViewModels model)
        {
            try
            {
                //var Status = CommonSetting.Status.Success;
                var signUpRow = _uow.Repository<SignUp>().GetAsQueryable(x =>
                            x.SignUpName == model.SignUpName 
                            //x.IsVerified == true
                        ).Select(x => new SignUpVerifyViewModels
                        {
                            IsVerified = x.IsVerified,
                           // IsSuccess = x.Status == Status ? true : false,
                            Status= x.Status
                        }
                        ).FirstOrDefault();

                if (signUpRow != null)
                {
                    if (signUpRow.Status == CommonSetting.Status.Success)
                    {
                        model.IsSuccess = true;
                    }
                    if (signUpRow.Status == CommonSetting.Status.Active)
                    {
                        model.IsActive = true;
                    }
                    if (signUpRow.Status == CommonSetting.Status.Pending)
                    {
                        model.IsPending = true;
                    }
                    model.IsVerified = signUpRow.IsVerified;
                    model.IsSignUpUser = true;
                }//end if

                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            //return true;
        }

        public bool ValidateIpAddress(string SignUpName)
        {
            try
            {

            }
            catch (Exception ex)
            {
                isError = true;
                _logger.LogError("Error", ex);
            }
            finally { }
            return isError;
        }

        public bool AssignSignUpCompanyDetails(MerchantOnboardingDetailsViewModels model)
        {
            try
            {
                if (CurrentUser.UserType==CommonSetting.UserType.OnBoarding)
                {
                   var data= _uow.Repository<SignUp>().GetAsQueryable(x => x.SignUpName == CurrentUser.Name)                  
                    .Select(x=> new 
                    {
                        CompanyName = x.CompanyName,
                        CompanyRegistrationNumber = x.CompanyRegistrationNumber,
                    }).FirstOrDefault();

                    if (data!=null)
                    {
                        model.BusinessEntityName = data.CompanyName;
                        model.IdNo = data.CompanyRegistrationNumber;
                    }//end if

                }//end if
                return false;
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
    }//end class
}//end namespace