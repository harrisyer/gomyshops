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
using System.Net;
using System.IO;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoMyShops.BAL
{
    public interface ISmsBAL
    {
        string GetPhoneCode(string UserName);
        Task<bool> SendSMSAsync(SMSModels model, string UserName);
        bool SendSMS(SMSModels model, string UserName);
        bool SendUserTac(string UserName, string TacNo);
        bool SendUserProfile(string UserName, string SMSType);
        bool SendMerchantFundingAlert(string UserName, string Settlement);
        bool SendSalesTransaction(string UserName, string TransactionFee);
        bool SendChargebackAlert(string UserName, string TransactionID);
        bool SendTransactionStatus(string UserName, string TransactionID, string Status);
    }

    public class SmsBAL : ISmsBAL
    {
        #region Definations
        private readonly ILogger<SmsBAL> _logger;
        IUnitOfWork _uow;
        IServicesBAL _servicesBAL;
        public IConfiguration _configuration { get; private set; }
        #endregion
        #region Constructor
        public SmsBAL(IConfiguration configuration, IUnitOfWorkFactory uowFactory, IServicesBAL servicesBAL, ILogger<SmsBAL> logger)
        {
            _uow = uowFactory.Create();
            _servicesBAL = servicesBAL;
            _configuration = configuration;
            _logger=logger;

    }
    #endregion

    public async Task<bool> SendSMSAsync(SMSModels model, string UserName)
        {
            //var a = new M3Tech.MPServiceSoapClient(M3Tech.MPServiceSoapClient.EndpointConfiguration.IMPServiceSoap12);
            try
            {               
                if (ConfigureSMS(model, UserName) == false)
                {
                    return false;
                }//end if

                //M3Tech.deliverMessageResponse smsResponse =await a.deliverMessageAsync(model.Userkey, model.Password, model.MsgID
                //          , model.TimeStamp, model.ServiceID, model.aMsg, model.Mobile
                //          , model.MCN, model.ChargeCode, model.MsgType);

                //if (smsResponse!=null)
                //{
                //    var data= smsResponse.Body.deliverMessageResult.Split(Convert.ToChar(","));
                //    string code = data[0];
                //    string Message = data[1];
                    
                //    switch (code)
                //    {
                //        case "00":
                //            return true;
                                          
                //        default:
                //            return false;
                            
                //    }//end switch   

                //}//end if
            }
            catch (Exception ex)
            {               
                _logger.LogError("Error", ex);
            }
            finally { }
           // a.Close();
            return false;
        }

        public bool SendSMS(SMSModels model, string UserName)
        {
            string SendSMSVendor = _configuration["AppSettings:SendSMSVendor"];// ConfigurationManager.AppSettings["SendSMSVendor"];
           
            if (SendSMSVendor == "M3")
            {
                //var a = new M3Tech.MPServiceSoapClient();
                try
                {
                    //M3Tech.deliverMessageRequest dm= new M3Tech.deliverMessageRequest();
                    //dm.Body = new M3Tech.deliverMessageRequestBody();

                    //var aMsg = model.aMsg;
                    //var ChargeCode = model.ChargeCode;
                    //var MCN = model.MCN;
                    //var Mobile = model.Mobile;
                    //var MsgID = model.MsgID;
                    //var MsgType = model.MsgType;
                    //var Password = model.Password;
                    //var ServiceID = model.ServiceID;
                    //var TimeStamp = model.TimeStamp; //YYYYMMDDHHMMSS.
                    //var Userkey = model.Userkey;

                    if (ConfigureSMS(model, UserName) == false)
                    {
                        return false;
                    }//end if

                    //For M3 Tech
                    var properties = from p in model.GetType().GetProperties()
                                     where p.GetValue(model, null) != null
                                     select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(model, null).ToString());

                    string queryString = String.Join("&", properties.ToArray());
                    queryString = "https://www.m3tech.my/imp/mtradeozopay_uat/submitsm.asp?" + queryString;
                    //var returnMsg = SendSMSToURL("https://www.m3tech.my/imp/mtradeozopay_uat/submitsm.asp?un=cloudtacsms&pwd=xuzo6EPeMa&dstno=" + model.Mobile + "&msg=" + model.aMsg + "&type=1&sendid=" + model.MsgID);

                    //Logger.Error("queryString=>" +  queryString);

                    var returnMsg = SendSMSToURL(queryString); //M3 using ssl3

                    //Logger.Error("SMS=>" + returnMsg + "  <>  " + queryString);
                    if (returnMsg=="00")
                    {
                        return true;
                    }//end if

                    //string smsResponse = a.deliverMessage(model.Userkey, model.Password, model.MsgID
                    //          , model.TimeStamp, model.ServiceID, model.aMsg, model.Mobile
                    //          , model.MCN, model.ChargeCode, model.MsgType);


                    //if (!smsResponse.IsNullOrEmptyString())
                    //{
                    //    var data = smsResponse.Split(Convert.ToChar(","));
                    //    string code = data[0];
                    //    string Message = data[1];

                    //    switch (code)
                    //    {
                    //        case "00":
                    //            return true;
                    //            break;
                    //        default:
                    //            return false;
                    //            break;
                    //    }//end switch   

                    //}//end if


                }
                catch (Exception ex)
                {
                    _logger.LogError("Error", ex);
                }
                finally { }
            }
            else
            {
                try
                {
                    if (ConfigureSMS(model, UserName) == false)
                    {
                        return false;
                    }//end if

                    //For Mifun

                    var returnMsg = SendSMSToURL("https://www.isms.com.my/isms_send.php?un=cloudtacsms&pwd=xuzo6EPeMa&dstno=" + model.Mobile + "&msg=" + model.aMsg + "&type=1&sendid=" + model.MsgID);
                    //
                    if (returnMsg.Contains("2000"))
                    {
                        return true;
                    }//end if                    

                }
                catch (Exception ex)
                {
                    _logger.LogError("Error", ex);
                }
                finally { }
            }//end if-else

            return false;
        }

        private string SendSMSToURL(string getUri)
        {
            string SentResult = String.Empty;
            //Logger.Error(getUri);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls ;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUri);

            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader responseReader = new StreamReader(response.GetResponseStream());

            String resultmsg = responseReader.ReadToEnd();
            responseReader.Close();

            int StartIndex = 0;

            int LastIndex = resultmsg.Length;

            if (LastIndex > 0)
                SentResult = resultmsg.Substring(StartIndex, LastIndex);

            responseReader.Dispose();

            return SentResult;
        }

        public bool SendUserTac(string UserName, string TacNo)
        {
            SMSModels model = new SMSModels();
            try
            {
                model.aMsg = "Ozopay: Verification Code requested for " + UserName + ". Verification Code:" + TacNo;
                return SendSMS(model, UserName);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public bool SendUserProfile(string UserName,string SMSType)
        {
            SMSModels model = new SMSModels();
            try
            {
                switch (SMSType)
                {
                    case CommonSetting.SmsMessage.TypeProfileUpdate:
                        model.aMsg = "Ozopay: Your profile update is successfully registered in Ozopay. Please check your email or dashboard for more info.";
                        break;
                    case CommonSetting.SmsMessage.TypePasswordChange:
                        model.aMsg = "Ozopay: Your login password is successfully changed.";//"Ozopay: Your password is successfully changed in Ozopay.";
                        break;                 
                    default:
                        break;
                }//end switch   

               return SendSMS(model, UserName);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public bool SendMerchantFundingAlert(string UserName, string Settlement)
        {
            SMSModels model = new SMSModels();
            try
            {
                model.aMsg = "Ozopay: Merchant settlement "+ Settlement + ". Please check your email or dashboard for more info.";

                return SendSMS(model, UserName);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public bool SendSalesTransaction(string UserName, string TransactionFee)
        {
            SMSModels model = new SMSModels();
            try
            {
                model.aMsg = "One successful transaction of " + TransactionFee + " has been made at your store. Please check your email or dashboard for more info.";

                return SendSMS(model, UserName);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public bool SendChargebackAlert(string UserName, string TransactionID)
        {
            SMSModels model = new SMSModels();
            try
            {
                model.aMsg = "A dispute has been raised for [" + TransactionID + "]. Please check your email or dashboard for more info. ";

                return SendSMS(model, UserName);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public bool SendTransactionStatus(string UserName, string TransactionID, string Status)
        {
            SMSModels model = new SMSModels();
            try
            {
                model.aMsg = "Ozopay: There is a status change for ["+ TransactionID + "]. It is now ["+ Status + "] Please check email or dashboard for more info.";

                return SendSMS(model, UserName);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;
        }

        public string GetPhoneCode(string UserName)
        {
            try
            {
                var user = _uow.Repository<User>().GetAsQueryable()
                             .Where(x => x.Username == UserName).Select(x => new { x.MobileNo }).FirstOrDefault();

             
                if (user != null)
                {
                    return "XXX-XXXX" + user.MobileNo.Right(3);             
                }//end if

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return "";
            }
            finally { }
            return "";
        }

        #region Private Function
        private bool ConfigureSMS(SMSModels model,string UserName)
        {
            try
            {
                var user = _uow.Repository<User>().GetAsQueryable()
                           .Where(x => x.Username == UserName).Select(x => new {  x.MobileNo, x.Country}).FirstOrDefault();

                var country = "";
                if (user != null)
                {
                    model.Mobile = user.MobileNo;
                    country = user.Country;
                }//end if

                if (model.Mobile.IsNullOrEmptyString())
                {
                    return false;
                }//end if
                if (!CommonFunctionsBAL.isValidMalaysiaMobileNo(country, model.Mobile) )
                {
                    return false;
                }//end if

                //model.aMsg = model.aMsg;
                //model.ChargeCode = "01";
                model.MCN = "push";
                //model.Mobile = model.Mobile;
                model.MsgID = CommonFunctions.GetGeneratedNo();
                model.MsgType = CommonSetting.SmsMessage.MsgType;
                model.Password = CommonSetting.SmsMessage.Password;
                model.ServiceID = CommonSetting.SmsMessage.ServiceID;
                model.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                model.Userkey = CommonSetting.SmsMessage.Userkey;

            
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return false;
            }
            finally { }
            //return true;

           
        }
        #endregion

    }//end namespace
}//end class
