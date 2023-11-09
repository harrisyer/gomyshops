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
using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Web.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
//using System.Data.Entity.Infrastructure;

namespace GoMyShops.BAL
{
    public interface IServicesBAL
    {
        //string ConvertAuditEntityName(string Name);
        void ConvertAuditValue(List<AuditDelta> list);
        string ConvertAuditEntityKey(string FieldName,string value);
        string ConvertDbFieldNameToEnglishName(string FieldName);
        string ConvertDbTableNameToEnglishName(string TableName);
        List<AuditValue> ConvertAuditCompositeKeys(List<AuditValue> model);
        List<SelectListItem> GetLoginFilterList();
        List<SelectListItem> GetCountryList();
        List<SelectListItem> GetStatusList();
        List<SelectListItem> GetMerchantStatusList();
        List<SelectListItem> GetStateByCountry(string CountryCode);
        List<SelectListItem> GetiParamList(string paramCode);
        string GetiParamDesc(string paramCode, string paramKey);
        SelectListItem GetiParamByKey(string paramCode, string paramKey);
        List<SelectListItem> GetiParamWithoutCodeList(string paramCode);
        string GetiParamValue(string paramCode, string paramKey);
        List<SelectListItem> GetCustomerTypeList();//hardcode
        List<SelectListItem> GetDriverList();
        List<SelectListItem> GetBrokerList();
        List<SelectListItem> GetAccountManagerUserList(string AccountManagerUserCode);
        List<SelectListItem> GetAccountManagerUserList();
        List<SelectListItem> GetUserList();
        List<SelectListItem> GetUserListByType(string type);
        //List<SelectListItem> GetUserListByStation(string station);
        string GetUpdateDocCode(string doccode, string username, string companyCode, string distributorcode, string branchcode);
        bool GetAppCtrRight(CommonSetting.AppCtrlID AppCtrlID);
        List<ErrorCodeSU> GetError(string type);
        ErrorCodeSU GetErrorByKey(string key);
        List<SelectListItem> GetMccCode();
        List<SelectListItem> GetMccCodeByIndustry(int IndustryCode);
        List<SelectListItem> GetIndustryCode();
        List<SelectListItem> GetMaillingTypeList();
        List<SelectListItem> GetOfficeAreaZoneList();
        List<SelectListItem> GetOfficeSpaceList();
        List<SelectListItem> GetOfficeTypeList();
        List<SelectListItem> GetAnnouncementTypeList();
        List<SelectListItem> GetAnnouncementFrequencyList();
        List<SelectListItem> GetTargetAudienceList();
        List<Recipient> GetRecipientList(bool isRefresh = false);
        List<CardTypeList> GetCreditCardTypeList();
        List<CardTypeList> GetOnboardingCreditCardList();
        List<SelectListItem> GetMdrPayTypeList();
        List<SelectListItem> GetMdrPaymentDayOfWeekList();
        List<SelectListItem> GetMdrPaymentWeekList();
        List<SelectListItem> GetMdrPaymentWeekPayDayList();
        string GetCustomerCodeFromUser();
        string GetCustomerCodeFromUserEntity();
        //List<string> GetTIDCodeFromUser();
        string GetPartnerCodeFromUser();
        string GetPartnerCodeFromUserEntity();
        string GetCustomerGroupCode();
        string GetPartnerGroupCode();
        List<SelectListItem> GetIntegrationCode();
        List<SelectListItem> GetScoreTypeList();
        List<MIDRiskScoreViewModels> GetRiskScoreList();
        List<SelectListItem> GetBankList();

        //Caching
        List<LookupIndustrySU> GetLookupIndustrySUList();
        List<MccCodeSU> GetMccCodeList();

    }

    public class ServicesBAL: BaseBAL, IServicesBAL
    {
        private readonly ILogger<ServicesBAL> _logger;       
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        private IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServicesBAL(IHttpContextAccessor httpContextAccessor, ILogger<ServicesBAL> logger, IUnitOfWorkFactory uowFactory, IMemoryCache memoryCache) :base()
        {
            _cache = memoryCache;
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        //public string ConvertAuditEntityName(string Name)
        //{
        //    switch (Name.ToUpper())
        //    {
        //        case "CUSTOMER":
        //            return "Merchant Profile";           
        //        default:
        //            return Name;
        //    }//end switch   
        //}

        public void ConvertAuditValue(List<AuditDelta> list)
        {
            //string data = "";        
            try
            {
                var iparamList = _uow.Repository<Param>().GetAsQueryable().ToList();
                foreach (var item in list)
                {
                    switch (item.FieldName.ToUpper())
                    {
                        case "MCCCODE":
                            item.ValueAfter = _uow.Repository<MccCodeSU>().GetAsQueryable(x => x.Code == item.ValueAfter)
                                                     .Select(x => x.Name).FirstOrDefault();
                            item.ValueBefore = _uow.Repository<MccCodeSU>().GetAsQueryable(x => x.Code == item.ValueBefore)
                                                     .Select(x => x.Name).FirstOrDefault();
                            break;
                        case "IndustryCode":
                            int valueAfter = item.ValueAfter.intParse();
                            int valueBefore = item.ValueBefore.intParse();
                            item.ValueAfter = _uow.Repository<LookupIndustrySU>().GetAsQueryable(x => x.Id == valueAfter)
                                                     .Select(x => x.Name).FirstOrDefault();
                            item.ValueBefore = _uow.Repository<LookupIndustrySU>().GetAsQueryable(x => x.Id == valueBefore)
                                                     .Select(x => x.Name).FirstOrDefault();
                            break;
                        case "COUNTRYCODE":
                            item.ValueAfter = _uow.Repository<Country>().GetAsQueryable(x => x.CountryCode == item.ValueAfter)
                                                     .Select(x => x.Name).FirstOrDefault();
                            item.ValueBefore = _uow.Repository<Country>().GetAsQueryable(x => x.CountryCode == item.ValueBefore)
                                                     .Select(x => x.Name).FirstOrDefault();
                            break;
                        case "STATUS":
                            item.ValueAfter = _uow.Repository<StatusSU>().GetAsQueryable(x => x.Status == item.ValueAfter)
                                                     .Select(x => x.StatusName).FirstOrDefault();
                            item.ValueBefore = _uow.Repository<StatusSU>().GetAsQueryable(x => x.Status == item.ValueBefore)
                                                     .Select(x => x.StatusName).FirstOrDefault();
                            break;
                        default:
                            if (item.FieldName== "FundingPeriod")
                            {
                                continue;
                            }//end if
                            var ValueAfter = iparamList.Where(r => r.ParamCode == item.FieldName
                                                       && r.ParamKey == item.ValueAfter
                                                       && r.ParamStatus == CommonSetting.Status.Active).Select(x => x.ParamDesc).FirstOrDefault();

                            item.ValueAfter = ValueAfter == null ? item.ValueAfter : ValueAfter;

                            var ValueBefore = iparamList.Where(r => r.ParamCode == item.FieldName
                                                             && r.ParamKey == item.ValueBefore
                                                             && r.ParamStatus == CommonSetting.Status.Active).Select(x => x.ParamDesc).FirstOrDefault();
                            item.ValueBefore = ValueBefore == null ? item.ValueBefore : ValueBefore;
                            break;
                    }//end switch  

                    item.FieldName = ConvertDbFieldNameToEnglishName(item.FieldName);


                }//end foreach 
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
           
        }

        public string ConvertAuditEntityKey(string FieldName, string value)
        {
            string data = "";
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    switch (FieldName.ToUpper())
                    {
                        case "CUSTOMERCODE":
                            data = _uow.Repository<Customer>().GetAsQueryable(x => x.CustomerCode == value)
                                                     .Select(x => x.CustomerName).FirstOrDefault();
                            break;                  
                    }//end switch   

                    
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return data;
        }

        public string ConvertDbFieldNameToEnglishName(string FieldName)
        {
            string[] ActualFieldName = { "AccountManagerCode", "AccountManagerName", "AccountManagerUserCode",
                "Address1", "Address2", "AdjustmentDate", "AllowOnlyAlphanumericUserNames", "Amount",
                "AmountBalance", "AmountCurrent", "AmountPaid", "AppCtrDesc", "AppCtrlID", "AppCtrName",
                "AppCtrType", "ApplicationDate", "ApplicationRemark", "ApplicationStatus", "ApplicationType",
                "ApproveFlag", "AreaAccessed", "Attachment", "AuditActionTypeENUM", "AuditID", "BankAccountName",
                "BankAccountNo", "BankAddress1", "BankAddress2", "BankCountryCode", "BankName", "BankState",
                "BankZip", "Bin", "BlacklistCountry", "BranchCode", "BrowserID", "BrowserType",
                "BusinessEntityName", "CardBinCode", "CardBinType", "CardType", "cardVelocity",
                "ChangePasswordInPeriodTimeSpan", "ChargeBackThreshold", "City", "Code", "CompanyCode",
                "CompanyURL", "CompanyWebSite", "CompositeKeys", "ContactMerchant", "ContactName",
                "ContactPerson", "ContactReseller", "ContentNote", "Country", "CountryCode", "CountryCode2",
                "CreateCheckBy1", "CreateCheckBy1Time", "CreateCheckBy2", "CreateCheckBy2Time", "CreatedBy",
                "CreatedDate", "CreatedTime", "CreateFlag", "Currency", "CurrencyCode", "CurrencyNum",
                "CurrentFundingPeriod", "CurrentFundingPeriodFrom", "CurrentFundingPeriodPaymentDate",
                "CurrentFundingPeriodTo", "CurrentWeekCount", "CustomerCode", "CustomerName",
                "CustomerServiceEmail", "CustomerServicePhone", "CustomerTIDCode", "DailyMax",
                "DataModel", "DateTimeStamp", "Day", "DayEndTimeInMinute", "DeclineCount", "DeclineCount899",
                "Default", "DefaultAccountLockoutTimeSpan", "DeleteFlag", "Description",
                "Designation", "DetailFlag", "DistributorCode", "EditCheckBy1", "EditCheckBy1Time",
                "EditCheckBy2", "EditCheckBy2Time", "EditFlag", "EldestUnpaidPeriodFrom",
                "EldestUnpaidPeriodPaymentDate", "EldestUnpaidPeriodTo", "Email", "EmailBody",
                "EmailCustomerService", "EmailFinance", "EmailFrom", "EmailReceiptMerchantRecipient",
                "EmailReceiptSender", "EmailRisk", "EmailSubject", "EmailTechnical", "EmailTitle",
                "EmailType", "emailVelocity", "EndDate", "EndScore", "EndTime", "EntryCode",
                "EntryDescription", "EntryType", "ErrorCode", "ErrorMessage", "EstablishedDate",
                "ExceptionCount", "FaxNo", "FeeChargeBack", "FeeCode", "FeeDecline", "FeeHighRisk",
                "FeeMonthly", "FeeRefund", "FeeRetrieval", "FeeSettle", "FeeSetup", "FeeType", "FeeVoid",
                "FeeWire", "FeeYearly", "FieldName", "FileName", "FirstName", "FirstSix", "FlatFeeRate",
                "frequencyCardNoDay", "frequencyCardNoMonth", "frequencyCardNoWeek", "frequencyEmailDay",
                "frequencyEmailMonth", "frequencyEmailWeek", "frequencyPhoneDay", "frequencyPhoneMonth",
                "frequencyPhoneWeek", "FundingCycle", "FundingID", "FundingLedgerID", "FundingPeriodType",
                "Gateway", "GatewayAmount", "GateWayFlatRate", "GateWayPercentageRate",
                "GatewayTransactionCount", "GenderCode", "GroupCode", "GroupName", "GroupType",
                "HeaderNote", "HoldingPeriod", "HoldPeriod", "HourlyDecline899Count", "HourlyDeclineCount",
                "IconName", "Id", "IdNo", "ImageCode", "IndustryCode", "InstitutionCode", "IntegrationCode",
                "IntegrationName", "IntegrationType", "InternalReserve", "InternalReservePercentage",
                "IPAddress", "IPCountryChecking", "IsChargeback", "IsCountryRestriction", "IsDefault",
                "IsEmailReceipt", "IsEmailReceiptMerchant", "IsEmailToMerchant", "IsFraud", "IsProcessed",
                "IsRefund", "IsReserve", "IsRiskScore", "IsSelect", "IsSelected", "IsSettlement",
                "IsShippingGoods", "KeyFieldID", "KeyFieldValue", "LastFour", "LastName", "LedgerMonth",
                "LedgerNote", "Limit", "LimitType", "LocationPath", "Logo",
                "MaxFailedAccessAttemptsBeforeLockout", "MCCCode", "MdrChargeMethod", "MdrChargeType",
                "MenuFlag", "Message", "MIDCode", "MIDFundingType", "MIDPaymentType", "MobileNo", "ModelName",
                "ModifiedBy", "ModifiedTime", "ModuleActionType", "ModuleCode", "ModuleDesc", "ModuleID",
                "ModuleName", "ModuleSequence", "ModuleStatus", "Month", "MonthlyMax", "Name",
                "NextFundingPeriod", "NextWeekCount", "OfficeExt", "OfficeNo", "OriginFileName",
                "OriginUrl", "OriginUrl2", "OriginUrl3", "OriginUrl4", "OriginUrl5", "PaidBy",
                "PaidDate", "ParamCode", "ParamValue", "ParentModuleID", "Partner", "PartnerCode",
                "PartnerFlatRate", "PartnerName", "PartnerPercentageRate", "PartnerType", "Password",
                "PayDay", "PaymentDate", "PercentageFeeRate", "Personnel", "PhoneNo", "phoneVelocity",
                "Phrase", "PostCode", "Priority", "Processor", "ProcessorAmount", "ProcessorCode",
                "ProcessorFlatRate", "ProcessorInfo", "ProcessorName", "ProcessorPercentageRate",
                "ProcessorReserve", "ProcessorReservePercentage", "ProcessorTagCode", "ProcessorTagName",
                "ProcessStartDate", "RateRefundRatio", "RateReserve", "ReasonCode", "ReasonDesc",
                "ReconSuccessCount", "ReconTime", "RefundLimit", "RefundThreshold", "RequireApproved",
                "RequireChangePasswordInPeriod", "RequireDigit", "RequiredLength",
                "RequireFirstTimeChangePassword", "RequireLowercase", "RequireNonLetterOrDigit",
                "RequireUniqueEmail", "RequireUppercase", "ReserveAmount", "ReserveID", "ReserveMonth",
                "ResourceKey", "ResponseCode", "ResponseFlag", "ScoreId", "ScoreName", "ScoreType",
                "ScrubOnly", "SecretKey", "SecurityId", "SecurityName", "Sequence", "ServerIP", "ServerIP2",
                "ServerIP3", "ServerIP4", "ServerIP5", "SettleAmount", "SettlementCurrencyCode",
                "SettlementDate", "SettlementFileTransactionCount", "SetupFee", "ShippingInfoFlag",
                "ShowReceiptFlag", "SiteDescriptor", "SortOrder", "StartDate", "StartScore", "StartTime",
                "State", "Status", "StatusDesc", "StatusName", "SwiftNo", "TargetAction", "TargetController",
                "ThemeCode", "Threshold", "TIDName", "TitleCode", "TitleOther", "TokenExpireHour",
                "TotalAdjustmentReserve", "TotalAmount", "TotalFee", "TotalMiscCharge", "TotalMiscRebate",
                "TotalReserve", "TradingAsDba", "TransactionCount", "TransactionDate", "TransactionDesc",
                "TransactionFeeCriteriaID", "TransactionGroupName", "TransactionRecordFeeID",
                "TransactionRecordID", "TransactionStatusID", "TransactionTypeID", "TransitNo", "Type",
                "UnitMax", "URL", "UrlReturn", "UsedMdrType", "UserCode", "UserLockoutEnabledByDefault",
                "Username", "ValueAfter", "ValueBefore", "ValueChanges", "VolumnRestrictionCurrencyCode",
                "WeekStart", "WhiteList", "WireFee", "Year", "YearlyMax", "Zip","StartScore","IsSelected",
                "ActivatedTime","AnnualFee","AnnualFeeLastChargedYear","ApproveRemark","BankCode"
                ,"BaseAmount","BeneficiaryName","BusinessModel","BusinessRegistrationNo","BusinessType"
                ,"Comment","DateFrom","DateTo","Descriptions","DisplayFrequency","DocumentCode","DONo"
                ,"FilePath","FundingPeriod","GenerateForMonth","GenerateForYear","InternalReserveAmount"
                ,"InternalReserveRate","IsAdmin","IsMerchant","IsPartner","MccDescription"
                ,"MIDFundingStatus","MIDReserveStatus","ModifiedApplicationStatusBy"
                ,"ModifiedApplicationStatusTime","ModifiedStatusBy","ModifiedStatusTime"
                ,"MonthlyFee","MonthlyFeeLastChargedMonth","MonthlyFeeLastChargedYear"
                ,"OriginalSalesTransId","ProcessorReserveAmount","ProcessorReserveRate"
                ,"ReasonType","RecipientConnectionCode","Reference","ReserveEnabledDate"
                ,"ReserveLedgerID","ReservePeriod","ReserveStartDate","ResolveCount"
                ,"SettledSalesAmount","TaxCode","TaxInvoiceID","TaxNo","TaxRate","Title"
                ,"TransactionTime","TransactionType","TransId","UserID","WeekCount",//
                "AcceptPaymentCreditCardType","AdditionalDescription","AgreementDate","ApplicationDocumentedRemark","AverageBillingAmount","BillingContactName","CapitalResources",
                "ChargeBackFee","ChargebackVolumn2MonthAgo","ChargebackVolumn3MonthAgo","ChargebackVolumn4MonthAgo"
                ,"ChargebackVolumn5MonthAgo","ChargebackVolumn6MonthAgo","ChargebackVolumnLastMonth","ChargeDate",
                "CheckListCode","CheckListDesc","CompanyName","CompanyRegistrationDate","CompanyRegistrationNumber",
                "CompanyUrl","ContactNo","Count","createddate","CreditCardRate","CurrentAcquirer","CurrentTransactionFee",
                "CustomerServicesContactName","CustomerServicesContactNo","DepositPrepaidByCustomerPercentage",
                "DesiredProcessingCurrencyCode","ExpiryDate","FPXFee","FundingLedgerTransactionGroup",
                "FundingStatus","FundingType","GenerationDate","GSTRegisterNo","HighestBillingAmount","InvoiceNo",
                "IsAcceptPaymentbyCreditCard","IsAcceptTransactionsBeforeReceivedProduct","IsAssociative","IsCharges",
                "IsHaveRetailShop","IsLedgerHidden","IsMarked","IsMobileApp","IsOfferWarranties","IsPartnership",
                "IsSdnBhd","IsShoppingCart","IsSole","IsVerified","LedgerType","LegalAddress1","LegalAddress2",
                "LegalCountryCode","LegalState","LegalZip","LinkPaymentFlag","LinkPaymentTransactionId",
                "MailingAddressType","MaintenanceFee","MIDStatus","MobileAppName","ModifiedAcceptStatusBy",
                "ModifiedAcceptStatusTime","ModifiedDocumentedStatusBy","ModifiedDocumentedStatusTime",
                "ModifiedPendingDocumentStatusBy","ModifiedPendingDocumentStatusTime","ModifiedPendingRateSheetStatusBy",
                "ModifiedPendingRateSheetStatusTime","MonthlyOnlineSales","MonthlyRetailSales","NoChargeback2MonthAgo",
                "NoChargeback3MonthAgo","NoChargeback4MonthAgo","NoChargeback5MonthAgo","NoChargeback6MonthAgo"
                ,"NoChargebackLastMonth","NoOfEmployee","NoRefund2MonthAgo","NoRefund3MonthAgo","NoRefund4MonthAgo",
                "NoRefund5MonthAgo","NoRefund6MonthAgo","NoRefundLastMonth","NoTransactions2MonthAgo",
                "NoTransactions3MonthAgo","NoTransactions4MonthAgo","NoTransactions5MonthAgo","NoTransactions6MonthAgo",
                "NoTransactionsLastMonth","OfficeAreaZoneCode","OfficeSpaceCode","OnboardingCode",
                "OnBoardingType","OnlyAllowPriorityBin","OrderDescription","OrderDetail","OrderId","OutletsCount",
                "PartnerAmount","PartnerLedgerID","PartnerProfileStatus","PartnerWireFee","Principal1Address1"
                ,"Principal1Address2","Principal1CountryCode","Principal1CurrentAddress1","Principal1CurrentAddress2"
                ,"Principal1CurrentCountryCode","Principal1CurrentOfficeType","Principal1CurrentState"
                ,"Principal1CurrentStayYear","Principal1CurrentZip","Principal1Designation","Principal1DOB","Principal1Email"
                ,"Principal1MobileNo","Principal1Name","Principal1NRICNo","Principal1OfficeNo",
                "Principal1OfficeType","Principal1PercentageOwned","Principal1State","Principal1StayYear",
                "Principal1Zip","Principal2Address1","Principal2Address2","Principal2CountryCode",
                "Principal2CurrentAddress1","Principal2CurrentAddress2","Principal2CurrentCountryCode",
                "Principal2CurrentOfficeType","Principal2CurrentState","Principal2CurrentStayYear",
                "Principal2CurrentZip","Principal2Designation","Principal2DOB","Principal2Email",
                "Principal2MobileNo","Principal2Name","Principal2NRICNo","Principal2OfficeNo",
                "Principal2OfficeType","Principal2PercentageOwned","Principal2State","Principal2StayYear",
                "Principal2Zip","ProcessedTransactionType","ProcessingFee","ProductReceivedDayCount"
                ,"ProductReceivedSalesPercentage","ProductType","RefundVolumn2MonthAgo","RefundVolumn3MonthAgo",
                "RefundVolumn4MonthAgo","RefundVolumn5MonthAgo","RefundVolumn6MonthAgo","RefundVolumnLastMonth",
                "RegistrationFee","ResponseDesc","RowNo","SalesVolumn2MonthAgo","SalesVolumn3MonthAgo",
                "SalesVolumn4MonthAgo","SalesVolumn5MonthAgo","SalesVolumn6MonthAgo","SalesVolumnLastMonth"
                ,"SettlementFee","ShoppingCartName","SignUpName","SOAID","SOANo","SourceData",
                "StatementOfAccountID","TargetedCountryCode","TaxInvoiceDetailID","TechnicalContactName"
                ,"TidCode","TokenId","TotalTaxAmount","TradingName","UrlReturnBackEnd","WarrantiesInWeeks","YearOfBusiness" };

            string[] EnglishFieldName = { "Account Manager Code", "Account Manager Name", "Account Manager User Code",
                "Address 1", "Address 2", "Adjustment Date", "Allow Only Alphanumeric User Names", "Amount",
                "Amount Balance", "Amount Current", "Amount Paid", "Application Control", "Application Control ID", "Application Control Name",
                "Application Control Type", "Application Date", "Application Remark", "Application Status", "Application Type",
                "Approve Flag", "Area Accessed", "Attachment", "Audit ActionType", "Audit ID", "Bank Account Name",
                "Bank Account No", "BankAddress 1", "BankAddress 2", "Bank Country Code", "Bank Name", "Bank State",
                "Bank Post Code", "Bin", "Blacklist Country", "Branch Code", "Browser ID", "Browser Type",
                "Business Entity Name", "Card Bin Code", "Card Bin Type", "Card Type", "Card Velocity",
                "Change Password In Period TimeSpan", "ChargeBack Threshold", "City", "Code", "Company Code",
                "Company URL", "Company WebSite", "CompositeKeys", "Contact Merchant", "Contact Name",
                "Contact Person", "Contact Reseller", "Content Note", "Country", "CountryCode", "CountryCode 2",
                "First Create CheckBy", "First Create CheckBy Time", "Second Create CheckBy", "Second CreateCheckBy Time", "Created By",
                "Created Date", "Created Time", "Create Flag", "Currency", "Currency Code", "Currency Number",
                "Current Funding Period", "Current Funding Period From", "Current Funding Period PaymentDate",
                "Current Funding Period To", "Current Week Count", "Customer Code", "Customer Name",
                "Customer Service Email", "Customer Service Phone", "Customer TID Code", "Daily Maximum",
                "Data Model", "Date TimeStamp", "Day", "Day EndTime In Minute", "Decline Count", "Decline Count 899",
                "Default", "Default Account Lockout TimeSpan", "Delete Flag", "Description",
                "Designation", "Detail Flag", "Distributor Code", "First EditCheckBy", "First EditCheckBy Time",
                "Second EditCheckBy", "Second EditCheckBy Time", "Edit Flag", "Eldest Unpaid eriod From",
                "Eldest Unpaid Period Payment Date", "Eldest Unpaid Period To", "Email", "Email Body",
                "Email Customer Service", "Email Finance", "Email From", "Email Receipt Merchant Recipient",
                "Email Receipt Sender", "Email Risk", "Email Subject", "Email Technical", "Email Title",
                "Email Type", "Email Velocity", "End Date", "End Score", "End Time", "Entry Code",
                "Entry Description", "Entry Type", "Error Code", "Error Message", "Established Date",
                "Exception Count", "Fax No", "Fee Charge Back", "Fee Code", "Fee Decline", "Fee HighRisk",
                "Fee Monthly", "Fee Refund", "Fee Retrieval", "Fee Settle", "Fee Setup", "Fee Type", "Fee Void",
                "Fee Wire", "Fee Yearly", "Field Name", "File Name", "First Name", "First Six", "Flat Fee Rate",
                "Frequency CardNo Day", "Frequency CardNo Month", "Frequency CardNo Week", "Frequency Email Day",
                "Frequency Email Month", "Frequency Email Week", "Frequency Phone Day", "Frequency Phone Month",
                "Frequency Phone Week", "Funding Cycle", "Funding ID", "Funding Ledger ID", "Funding Period Type",
                "Gateway", "Gateway Amount", "GateWay Flat Rate", "GateWay Percentage Rate",
                "Gateway Transaction Count", "Gender Code", "Group Code", "Group Name", "Group Type",
                "Header Note", "Holding Period", "Hold Period", "HourlyDecline 899 Count", "Hourly Decline Count",
                "Icon Name", "Id", "Id No", "Image Code", "Industry Code", "Institution Code", "Integration Code",
                "Integration Name", "Integration Type", "Internal Reserve", "Internal Reserve Percentage",
                "IP Address", "IPCountry Checking", "Is Chargeback", "Is Country Restriction", "Is Default",
                "IsEmail Receipt", "IsEmail Receipt Merchant", "IsEmail To Merchant", "Is Fraud", "Is Processed",
                "Is Refund", "Is Reserve", "Is Risk Score", "Is Select", "Is Selected", "Is Settlement",
                "Is Shipping Goods", "KeyField ID", "Key Field Value", "Last Four", "Last Name", "Ledger Month",
                "Ledger Note", "Limit", "Limit Type", "Location Path", "Logo",
                "Max Failed Access Attempts Before Lockout", "MCC Code", "Mdr Charge Method", "Mdr Charge Type",
                "Menu Flag", "Message", "MID Code", "MID Funding Type", "MID Payment Type", "Mobile No", "Model Name",
                "Modified By", "Modified Time", "ModuleAction Type", "Module Code", "Module Desc", "Module ID",
                "Module Name", "Module Sequence", "Module Status", "Month", "Monthly Max", "Name",
                "Next Funding Period", "Next Week Count", "Office Ext", "Office No", "Origin File Name",
                "Origi nUrl", "Origin Url 2", "Origin Url 3", "Origin Url 4", "Origin Url 5", "Paid By",
                "Paid Date", "Param Code", "Param Value", "Parent Module ID", "Partner", "Partner Code",
                "Partner Flat Rate", "Partner Name", "Partner Percentage Rate", "Partner Type", "Password",
                "Pay Day", "Payment Date", "Percentage Fee Rate", "Personnel", "Phone No", "Phone Velocity",
                "Phrase", "Post Code", "Priority", "Processor", "Processor Amount", "Processor Code",
                "Processor Flat Rate", "Processor Info", "Processor Name", "Processor Percentage Rate",
                "Processor Reserve", "Processor Reserve Percentage", "Processor Tag Code", "Processor Tag Name",
                "ProcessStart Date", "Rate Refund Ratio", "Rate Reserve", "Reason Code", "Reason Description",
                "Recon Success Count", "Recon Time", "Refund Limit", "Refund Threshold", "Require Approved",
                "Require Change Password In Period", "Require Digit", "Required Length",
                "Require First Time Change Password", "Require Lower Case", "Require None Letter Or Digit",
                "Require Unique Email", "Require Uppercase", "Reserve Amount", "Reserve ID", "Reserve Month",
                "Resource Key", "Response Code", "Response Flag", "Score Id", "Score Name", "Score Type",
                "Scrub Only", "Secret Key", "Security Id", "Security Name", "Sequence", "Server IP", "Server IP 2",
                "Server IP 3", "Server IP 4", "Server IP 5", "Settle Amount", "Settlement Currency Code",
                "Settlement Date", "Settlement File Transaction Count", "Setup Fee", "Shipping Info Flag",
                "ShowReceiptFlag", "Site Descriptor", "Sort Order", "Start Date", "Start Score", "Start Time",
                "State", "Status", "Status Description", "Status Name", "Swift No", "Target Action", "Target Controller",
                "Theme Code", "Threshold", "TID Name", "Title Code", "Title Other", "Token Expire Hour",
                "Total Adjustment Reserve", "Total Amount", "Total Fee", "Total Misc Charge", "Total Misc Rebate",
                "Total Reserve", "Trading AsDba", "Transaction Count", "Transaction Date", "Transaction Desc",
                "Transaction Fee Criteria ID", "Transaction Group Name", "Transaction Record Fee ID",
                "Transaction Record ID", "Transaction Status ID", "Transaction Type ID", "Transit No", "Type",
                "Unit Max", "URL", "UrlReturn", "Used Mdr Type", "User Code", "User Lockout Enabled By Default",
                "User name", "Value After", "Value Before", "Value Changes", "Volumn Restriction Currency Code",
                "Week Start", "White List", "Wire Fee", "Year", "Yearly Max", "Postcode","Start Score","Is Selected","Activated Time","Annual Fee","Annual Fee Last Charged Year","Approve Remark","Bank Code","Base Amount","Beneficiary Name","Business Model","Business Registration No","Business Type","Comment","Date From","Date To","Descriptions","Display Frequency","Document Code","DO No","File Path","Funding Period","Generate For Month","Generate For Year","Internal Reserve Amount","Internal Reserve Rate","Is Admin","Is Merchant","Is Partner","Mcc Description","MID Funding Status","MID Reserve Status","Modified Application StatusBy","Modified Application Status Time","Modified Status By","Modified Status Time","Monthly Fee","Monthly Fee Last Charged Month","Monthly Fee Last Charged Year","Original Sales Trans Id"
                ,"Processor Reserve Amount","Processor Reserve Rate","Reason Type","Recipient Connection Code",
                "Reference","Reserve Enabled Date","Reserve Ledger ID","Reserve Period"
                ,"Reserve Start Date","Resolve Count","Settled Sales Amount","Tax Code","Tax Invoice ID"
                ,"Tax No","Tax Rate","Title","Transaction Time","Transaction Type","Trans Id","User ID"
                ,"Week Count",
                 "Accept Payment Credit Card Type","Additional Description","Agreement Date","Application Documented Remark","Average Billing Amount","Billing Contact Name","Capital Resources",
                "ChargeBack Fee","Chargebac kVolumn 2Month Ago","Chargeback Volumn 3Month Ago","Chargeback Volumn 4Month Ago"
                ,"Chargeback Volumn 5Month Ago","Chargeback Volumn 6Month Ago","Chargeback Volumn Last Month","Charge Date",
                "CheckList Code","CheckList Description","Company Name","Company Registration Date","Company Registration Number",
                "Company Url","Contact No","Count","Created date","Credit Card Rate","Current Acquirer","Current Transaction Fee",
                "Customer Services Contact Name","Customer Services Contact No","Deposit Prepaid By Customer Percentage",
                "Desired Processing Currency Code","Expiry Date","FPX Fee","Funding Ledger Transaction Group",
                "Funding Status","Funding Type","Generation Date","GST Register No","Highest Billing Amount","Invoice No",
                "Is AcceptPayment by Credit Card","Is Accept Transactions Before Received Product","Is Associative","Is Charges",
                "Is Have Retail Shop","Is Ledger Hidden","Is Marked","Is Mobile App","Is Offer Warranties","Is Partnership",
                "Is Sdn Bhd","Is Shopping Cart","Is Sole","Is Verified","Ledger Type","Legal Address 1","Legal Address 2",
                "Legal Country Code","Legal State","Legal Zip","Link Payment Flag","Link Payment Transaction Id",
                "Mailing Address Type","Maintenance Fee","MID Status","Mobile Application Name","Modified Accept Status By",
                "Modified Accept Status Time","Modified Documented Status By","Modified Documented Status Time",
                "Modified Pending Document Status By","Modified Pending Document Status Time","Modified Pending Rate Sheet Status By",
                "Modified Pending Rate Sheet Status Time","Monthly Online Sales","Monthly Retail Sales","No Chargeback 2Month Ago",
                "No Chargeback 3Month Ago","No Chargeback 4Month Ago","No Chargeback 5Month Ago","No Chargeback 6Month Ago"
                ,"No Chargeback Last Month","No Of Employee","No Refund 2Month Ago","No Refund 3Month Ago","No Refund 4Month Ago",
                "No Refund 5Month Ago","No Refund 6Month Ago","No Refund Last Month","No Transactions 2Month Ago",
                "No Transactions 3Month Ago","No Transactions 4Month Ago","No Transactions 5Month Ago","No Transactions 6Month Ago",
                "No Transactions Last Month","Office Area Zone Code","Office Space Code","Onboarding Code",
                "OnBoarding Type","Only Allow Priority Bin","Order Description","Order Detail","Order Id","Outlets Count",
                "Partner Amount","Partner Ledger ID","Partner Profile Status","Partner Wire Fee","Principal 1 Address 1"
                ,"Principal 1 Address 2","Principal 1 Country Code","Principal 1 Current Address 1","Principal 1 Current Address 2"
                ,"Principal 1 Current Country Code","Principal 1 Current Office Type","Principal 1 Current State"
                ,"Principal 1 Current Stay Year","Principal 1 Current Zip","Principal 1 Designation","Principal 1 DOB","Principal 1 Email"
                ,"Principal 1 Mobile No","Principal 1 Name","Principal 1 NRIC No","Principal 1 Office No",
                "Principal 1 Office Type","Principal 1 Percentage Owned","Principal 1 State","Principal 1 Stay Year",
                "Principal 1 Zip","Principal 2 Address 1","Principal 2 Address 2","Principal 2 Country Code",
                "Principal 2 Current Address 1","Principal 2 Current Address 2","Principal 2 Current Country Code",
                "Principal 2 Current Office Type","Principal 2 Current State","Principal 2 Current Stay Year",
                "Principal 2 Current Zip","Principal 2 Designation","Principal 2 DOB","Principal 2 Email",
                "Principal 2 Mobile No","Principal 2 Name","Principal 2 NRIC No","Principal 2 Office No",
                "Principal 2 Office Type","Principal 2 Percentage Owned","Principal 2 State","Principal 2 Stay Year",
                "Principal 2 Zip","Processed Transaction Type","Processing Fee","Product Received Day Count"
                ,"Product Received Sales Percentage","Product Type","Refund Volumn 2Month Ago","Refund Volumn 3Month Ago",
                "Refund Volumn 4Month Ago","Refund Volumn 5Month Ago","Refund Volumn 6Month Ago","Refund Volumn Last Month",
                "Registration Fee","Response Description","Row No","Sales Volumn 2Month Ago","Sales Volumn 3Month Ago",
                "Sales Volumn 4Month Ago","Sales Volumn 5Month Ago","Sales Volumn 6Month Ago","Sales Volumn Last Month"
                ,"Settlement Fee","Shopping Cart Name","Sign Up Name","SOA ID","SOA No","Source Data",
                "Statement Of Account ID","Targeted Country Code","Tax Invoice Detail ID","Technical Contact Name"
                ,"Tid Code","Token Id","Total Tax Amount","Trading Name","Url Return Back End","Warranties In Weeks","Year Of Business"
            };


            if (ActualFieldName.Contains(FieldName))
            {
                int position = Array.IndexOf(ActualFieldName, FieldName);
                if (position > 0)
                {
                    return EnglishFieldName[position];
                }//end if
            }//end if
            return FieldName;
        }

        public string ConvertDbTableNameToEnglishName(string TableName)
        {
            string[] ActualTableName = {"AccountManager", "AppCtrlSU", "AppCtrlUserProfile",
                "AuditDetail", "AuditMaster", "BankResponseCodeSU", "CBRetrievalLogStatusSU",
                "ChargeBackReasonSU", "Country", "Currency", "Customer", "CustomerCardBinRule",
                "CustomerMID", "CustomerMIDCardType", "CustomerMIDFee", "CustomerMIDFunding",
                "CustomerMIDReserve", "CustomerTID", "CustomerTIDCustomerMID", "Email", "EmailMaster", "ErrorCodeSU",
                "FundingEntry", "FundingLedger", "FundingLedgerAdjustment", "FundingLedgerDetails",
                "FundingTransactionTypeSU", "IntegrationSU", "LoginSU", "LookupIndustrySU", "MccCodeSU",
                "MIDFraudScrubProfile", "MIDFraudScrubRule", "MIDRisk", "MIDRiskScore",
                "MIDRiskThresholdLimit", "ModuleActionSU", "ModuleSU", "Partner", "Processor",
                "ProcessorCardBinRule", "ProcessorCardType", "ProcessorCurrency", "ProcessorMCC",
                "ProcessorPriorityBin", "ProcessorTag", "ReserveLedger", "ReserveTransactionTypeSU",
                "RetrievalReasonSU", "RiskScoreSU", "SettlementFileInfo", "StatusSU", "SysParameterSU",
                "TransactionFeeCriteriaSU", "TransactionRecordFee", "TransactionRiskVolumeLimit",
                "User", "UserGroup","Announcement","Document","Recipient","RecipientConnection"
                ,"RecipientConnectionDetail","RecipientGroup","ProcessorMCC,SignUp,CustomerOnboarding," +
                "CustomerOBRateSheet,CustomerOBOwnerShip,CustomerOBOnlineBusiness,CustomerOBOnlinePaymentHistory," +
                "CustomerOBBank,CustomerOBDocument,CustomerOBCheckList,CustomerOBAcceptCardType"};

            string[] EnglishTableName = {"Account Manager", "AppCtrlSU", "AppCtrlUserProfile",
                "Audit Detail", "Audit Master", "BankResponseCodeSU", "CBRetrievalLogStatusSU",
                "ChargeBackReasonSU", "Country", "Currency", "Merchant Profile", "Merchant Card BIN Profile",
                "Merchant MID Profile", "Merchant MID Card Type", "Merchant MID Fee", "Merchant MID Funding",
                "Merchant MID Reserve", "Merchant TID Profile", "CustomerTID CustomerMID", "Email", "Email Master", "ErrorCodeSU",
                "MID Funding Entry", "Funding Ledger", "Funding Ledger Adjustment", "Funding Ledger Details",
                "FundingTransactionTypeSU", "IntegrationSU", "Login SU", "LookupIndustry SU", "MccCode SU",
                "MID Fraud Scrub Profile", "MID Fraud Scrub Rule", "MID Risk Profile", "MID Risk Score",
                "MIDRiskThresholdLimit", "ModuleActionSU", "ModuleSU", "Partner Profile", "Processor Profile",
                "Processor Card BIN Profile", "Processor Card Type", "Processor Currency", "Processor MCC List",
                "Processor PriorityBin", "Merchant MID Tag", "ReserveLedger", "ReserveTransactionTypeSU",
                "RetrievalReasonSU", "RiskScoreSU", "Settlement File Info", "StatusSU", "SysParameterSU",
                "TransactionFeeCriteriaSU", "TransactionRecordFee", "TransactionRiskVolumeLimit",
                "User", "Users Group","Announcement","Document","Recipient","Recipient Connection"
                ,"Recipient Connection Detail","Recipient Group","Processor MCC","Sign Up","Customer Onboarding","Customer Onboarding RateSheet",
                "Customer Onboarding OwnerShip","Customer Onboarding Online Business",
                "Customer Onboarding Online Payment History","Customer Onboarding Bank","Customer Onboarding Document",
                "Customer Onboarding Check List", "Customer Onboarding Accept Card Type"};


            if (ActualTableName.Contains(TableName))
            {
                int position = Array.IndexOf(ActualTableName, TableName);
                if (position > 0)
                {
                    return EnglishTableName[position];
                }//end if
            }//end if
            return TableName;
        }

        public List<AuditValue> ConvertAuditCompositeKeys(List<AuditValue> model)
        {           
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                   

                    //var iparamList = uow.Repository<Param>().GetAsQueryable().ToList();

                    foreach (var item in model)
                    {
                        switch (item.FieldName.ToUpper())
                        {
                            case "CUSTOMERCODE":
                                item.Value= item.Value + " - " + (_uow.Repository<Customer>().GetAsQueryable(x => x.CustomerCode == item.Value)
                                                         .Select(x => x.CustomerName).FirstOrDefault()).stringParse();
                                break;
                            default:
                                break;
                        }//end switch   

                        item.FieldName = ConvertDbFieldNameToEnglishName(item.FieldName);

                        //item.Value = iparamList.Where(r => r.ParamCode == item.FieldName
                        //                                  && r.ParamKey == item.Value
                        //                                  && r.ParamStatus == CommonSetting.Status.Active).Select(x => x.ParamDesc).FirstOrDefault();
                    }//end foreach


                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return model;
        }


        public List<SelectListItem> GetCountryList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<Country>().GetAsQueryable().OrderBy(r=>r.Sequence)
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.Name,// r.CountryCode + " - " + r.Name,
                                                         Value = r.CountryCode
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

        public List<SelectListItem> GetStateByCountry(string CountryCode)
        {
            List<SelectListItem> infos = null;
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    infos = _uow.Repository<State>().GetAsQueryable(filter: x => x.CountryCode == CountryCode)
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.StateCode + " - " + r.Name,
                                                         Value = r.StateCode
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

        public List<SelectListItem> GetStatusList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<StatusSU>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active ||
                                                                         x.Status == CommonSetting.Status.Inactive)
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text =  r.StatusName,
                                                         Value = r.Status
                                                     }).ToList();
                //}//end using
                //infos.Add(new SelectListItem() { Text = "Active", Value = "1" });
                //infos.Add(new SelectListItem() { Text = "Deactivated", Value = "0" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetMerchantStatusList()
        {
            List<SelectListItem> infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<StatusSU>().GetAsQueryable(x=>x.Status== CommonSetting.Status.Active ||
                                                                         x.Status == CommonSetting.Status.Inactive ||
                                                                         x.Status == CommonSetting.Status.Suspended ||
                                                                         x.Status == CommonSetting.Status.Terminated)
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.StatusName,
                                                         Value = r.Status
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

        public List<SelectListItem> GetDriverList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<User>().GetAsQueryable(x=>x.Status==CommonSetting.Status.Active && x.Type==CommonSetting.Designation.Driver)
                                               
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.Username,
                                                         Value = r.Username
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

        public List<SelectListItem> GetBrokerList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active && x.Type == CommonSetting.Designation.Broker)

                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.Username,
                                                         Value = r.Username
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

        public List<SelectListItem> GetAccountManagerUserList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    List<string> accountManagerUsers =_uow.Repository<AccountManager>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active
                                                       ).Select(x=>x.AccountManagerUserCode).Distinct().ToList();

                    infos = _uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active
                                                                  && x.GroupCode == CommonSetting.GroupCode.AccountManager)
                            .Where(x=> !accountManagerUsers.Contains(x.Username))
                             .Select(r => new SelectListItem()
                             {
                                 Text = r.Username + " - " + r.FirstName + " " + r.LastName ,
                                 Value = r.Username,
                             }).ToList();


                    //infos = uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active
                    //                                              && x.GroupCode == CommonSetting.GroupCode.AccountManager)

                    //                                 .Select(r => new SelectListItem()
                    //                                 {
                    //                                     Text = r.Username,
                    //                                     Value = r.Username
                    //                                 }).ToList();
               // }//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetAccountManagerUserList( string AccountManagerUserCode)
        {
            List<SelectListItem> infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    List<string> accountManagerUsers = _uow.Repository<AccountManager>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active && x.AccountManagerUserCode != AccountManagerUserCode
                                                       ).Select(x => x.AccountManagerUserCode).Distinct().ToList();

                    infos = _uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active
                                                                  && x.GroupCode == CommonSetting.GroupCode.AccountManager)
                            .Where(x => !accountManagerUsers.Contains(x.Username))
                             .Select(r => new SelectListItem()
                             {
                                 Text = r.Username + " - " + r.FirstName + " " + r.LastName,
                                 Value = r.Username,
                             }).ToList();


                    //infos = uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active
                    //                                              && x.GroupCode == CommonSetting.GroupCode.AccountManager)

                    //                                 .Select(r => new SelectListItem()
                    //                                 {
                    //                                     Text = r.Username,
                    //                                     Value = r.Username
                    //                                 }).ToList();
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetUserList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active)

                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.Username,
                                                         Value = r.Username
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

        public List<SelectListItem> GetUserListByType(string type)
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<User>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active && x.Type == type)

                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.Username,
                                                         Value = r.Username
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

        public List<SelectListItem> GetLoginFilterList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Failed Login", Value = "2" });
                typelist.Add(new SelectListItem() { Text = "Login", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "Create or Edit", Value = "0" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetCustomerTypeList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Normal", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "VIP", Value = "0" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetMaillingTypeList()
        {
            List<SelectListItem> infos = null;
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Legal or Registered Address", Value = "0" });
                typelist.Add(new SelectListItem() { Text = "Business Address", Value = "1" });         
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetOfficeAreaZoneList()
        {
            List<SelectListItem> infos = null;
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Residential", Value = "0" });
                typelist.Add(new SelectListItem() { Text = "Industrial", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "Commercial", Value = "2" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetOfficeSpaceList()
        {
            List<SelectListItem> infos = null;
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "0-500", Value = "0" });
                typelist.Add(new SelectListItem() { Text = "501-2000", Value = "1" });
                typelist.Add(new SelectListItem() { Text = ">2000", Value = "2" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetOfficeTypeList()
        {
            List<SelectListItem> infos = null;
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Own", Value = "0" });
                typelist.Add(new SelectListItem() { Text = "Rent", Value = "1" });      
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetAnnouncementTypeList()
        {
            List<SelectListItem> infos = null;
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Internally & Public", Value = "0" });
                typelist.Add(new SelectListItem() { Text = "Internally", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "Public", Value = "2" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetAnnouncementFrequencyList()
        {
            List<SelectListItem> infos = null;
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Always", Value = "0" });
                typelist.Add(new SelectListItem() { Text = "Once", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "None", Value = "2" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetTargetAudienceList()
        {
            List<SelectListItem> infos = null;
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Admin", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "Merchant", Value = "2" });
                typelist.Add(new SelectListItem() { Text = "Partner", Value = "3" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetScoreTypeList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "=", Value = "1" });
                typelist.Add(new SelectListItem() { Text = ">=", Value = "2" });
                typelist.Add(new SelectListItem() { Text = "<=", Value = "3" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }


        public List<CardTypeList> GetCreditCardTypeList()
        {
            List<CardTypeList> infos = new List<CardTypeList>();
            try
            {
                CardTypeList MasterCard = new CardTypeList();
                MasterCard.CardStatus = false;
                MasterCard.CardValue = "M";
                MasterCard.CardName = "MasterCard";
                infos.Add(MasterCard);

                CardTypeList VisaCard = new CardTypeList();
                VisaCard.CardStatus = false;
                VisaCard.CardValue = "V";
                VisaCard.CardName = "Visa";
                infos.Add(VisaCard);

                return infos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<CardTypeList> GetOnboardingCreditCardList()
        {
            List<CardTypeList> infos = new List<CardTypeList>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<Param>().GetAsQueryable()
                                                    .Where(r => r.ParamCode == CommonSetting.ParamCodes.OnBoardingCreditCard && r.ParamStatus == CommonSetting.Status.Active)
                                                    .OrderBy(x => x.SortOrder)
                                                     .Select(r => new CardTypeList()
                                                     {
                                                         CardValue = r.ParamKey,
                                                          CardName=r.ParamDesc
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

        public List<MIDRiskScoreViewModels> GetRiskScoreList()
        {
            List<MIDRiskScoreViewModels> infos = new List<MIDRiskScoreViewModels>();
            try
            {
               // using (var uow = this._uowFactory.Create())
               // {
                    infos = _uow.Repository<RiskScoreSU>().GetAsQueryable(x=>x.Id!=1 && x.Status==CommonSetting.Status.Active)                                                    
                                                    .Select(r => new MIDRiskScoreViewModels()
                                                    {
                                                        IsSelect=r.IsDefault,
                                                        ScoreName=r.ScoreName,
                                                        ScoreId=r.Id,
                                                        StartScore=r.StartScore,
                                                        EndScore=r.EndScore,
                                                        ScoreType=r.ScoreType
                                                    }).ToList();
                //}//end using

        
                return infos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetMdrPayTypeList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {

                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Percentage Fee + Flat Fee", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "Percentage Fee / Flat Fee (which higher)", Value = "2" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetMdrPaymentDayOfWeekList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "1", Value = "7" });
                typelist.Add(new SelectListItem() { Text = "2", Value = "14" });
                typelist.Add(new SelectListItem() { Text = "3", Value = "21" });
                typelist.Add(new SelectListItem() { Text = "4", Value = "28" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetMdrPaymentWeekList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Monday", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "Tuesday", Value = "2" });
                typelist.Add(new SelectListItem() { Text = "Wednesday", Value = "3" });
                typelist.Add(new SelectListItem() { Text = "Thursday ", Value = "4" });
                typelist.Add(new SelectListItem() { Text = "Friday", Value = "5" });
                typelist.Add(new SelectListItem() { Text = "Saturday", Value = "6" });
                typelist.Add(new SelectListItem() { Text = "Sunday", Value = "0" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetMdrPaymentWeekPayDayList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                List<SelectListItem> typelist = new List<SelectListItem>();
                typelist.Add(new SelectListItem() { Text = "Monday", Value = "1" });
                typelist.Add(new SelectListItem() { Text = "Tuesday", Value = "2" });
                typelist.Add(new SelectListItem() { Text = "Wednesday", Value = "3" });
                typelist.Add(new SelectListItem() { Text = "Thursday ", Value = "4" });
                typelist.Add(new SelectListItem() { Text = "Friday", Value = "5" });
                return typelist;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetiParamList(string paramCode)
        {
            List<SelectListItem> infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<Param>().GetAsQueryable()
                                                    .Where(r=>r.ParamCode==paramCode && r.ParamStatus==CommonSetting.Status.Active) 
                                                    .OrderBy(x=>x.SortOrder) 
                                                    .Select(r => new SelectListItem()
                                                     {
                                                         Text =r.ParamValue + " - " +  r.ParamDesc,
                                                         Value = r.ParamKey
                                                     }).ToList();
               // }//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetiParamWithoutCodeList(string paramCode)
        {
            List<SelectListItem> infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<Param>().GetAsQueryable()
                                                    .Where(r => r.ParamCode == paramCode && r.ParamStatus == CommonSetting.Status.Active)
                                                    .OrderBy(x => x.SortOrder)
                                                    .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.ParamDesc,
                                                         Value = r.ParamKey
                                                     }).ToList();
               // }//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public SelectListItem GetiParamByKey(string paramCode, string paramKey)
        {
            SelectListItem infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<Param>().GetAsQueryable()
                                                    .Where(r => r.ParamCode == paramCode && r.ParamKey == paramKey &&  r.ParamStatus == CommonSetting.Status.Active)
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.ParamValue + " - " + r.ParamDesc,
                                                         Value = r.ParamKey
                                                     }).FirstOrDefault();
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public string GetiParamValue(string paramCode, string paramKey)
        {
            string value = string.Empty;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    var infos = _uow.Repository<Param>().GetAsQueryable()
                                                    .Where(r => r.ParamCode == paramCode 
                                                           && r.ParamKey==paramKey
                                                           && r.ParamStatus == CommonSetting.Status.Active)
                                                     .Select(r => r.ParamValue).ToList();
                    if (!infos.IsNullOrEmpty())
                    {
                        value = infos.FirstOrDefault();
                    }//end if
                //}//end using
            }
            catch (Exception ex)
            {
                value = string.Empty;
                _logger.LogError("Error", ex);
            }
            finally { }
            return value;
        }

        public string GetiParamDesc(string paramCode, string paramKey)
        {
            string value = string.Empty;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    var infos = _uow.Repository<Param>().GetAsQueryable()
                                                    .Where(r => r.ParamCode == paramCode
                                                           && r.ParamKey == paramKey
                                                           && r.ParamStatus == CommonSetting.Status.Active)
                                                     .Select(r => r.ParamDesc).ToList();
                    if (!infos.IsNullOrEmpty())
                    {
                        value = infos.FirstOrDefault();
                    }//end if
                //}//end using
            }
            catch (Exception ex)
            {
                value = string.Empty;
                _logger.LogError("Error", ex);
            }
            finally { }
            return value;
        }

        public bool GetAppCtrRight(CommonSetting.AppCtrlID AppCtrlID)
        {
            bool value = false;
            int appCtrlID = (int)AppCtrlID;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    var AppCtrlSURepo = (from a in _uow.Repository<AppCtrlSU>().GetAsQueryable()
                        .Where(r => r.Status == CommonSetting.Status.Active && r.AppCtrlID== appCtrlID)
                             select new AppCtrDetailViewModels
                             {
                                 AppCtrlID = a.AppCtrlID,
                                 AppCtrName = a.AppCtrName,
                                 AppCtrType = a.AppCtrType,
                                 SortOrder = a.SortOrder,
                             }).FirstOrDefault();

                    if (!AppCtrlSURepo.IsNullOrEmpty())
                    {
                        if (AppCtrlSURepo.AppCtrType == "G")
                        {
                            var us = _uow.Repository<User>().GetAsQueryable(x => x.Username == CurrentUser.Name).FirstOrDefault(); 
                            if (!us.IsNullOrEmpty())
                            {
                                var GroupCode = us.GroupCode;
                                var found = _uow.Repository<AppCtrlUserProfile>().GetAsQueryable()
                                           .Where(r => r.GroupCode == GroupCode && r.AppCtrlID == appCtrlID).Count();
                                if (found>0)
                                {
                                    value = true;
                                }//end if             
                            }//end if
                        }//end if
                    }//end if
            
                //}//end using
            }
            catch (Exception ex)
            {
                value = false;
                _logger.LogError("Error", ex);
            }
            finally { }
            return value;
        }


        public string GetUpdateDocCode(string doccode, string username,string companyCode, string distributorcode, string branchcode)
        {

            int currentSeq = 0;
            DocSequenceSU DocSeq = new DocSequenceSU();

            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    DocSeq = _uow.Repository<DocSequenceSU>().GetAsQueryable(filter: x => x.DocCode == doccode && x.CompanyCode == companyCode
                                            && x.DistributorCode == distributorcode && x.BranchCode == branchcode
                                            ).FirstOrDefault();

                    if (DocSeq == null)
                    {
                        throw new Exception(DocSeq.DocCode + " not found in DocType or DocSequence table");
                    }//end if


                    var entry = _uow.Context.Entry<DocSequenceSU>(DocSeq);
                    bool saveFailed;
                    int intTryCount = 0;
                    do
                    {
                        saveFailed = false;

                        try
                        {
                            entry.Property(u => u.Sequence).CurrentValue = CommonFunctions.intParse(entry.Property(u => u.Sequence).CurrentValue) + 1;
                            _uow.Save();
                            currentSeq = entry.Property(u => u.Sequence).CurrentValue;
                        }
                        catch (DbUpdateConcurrencyException )
                        {
                            saveFailed = true;
                            intTryCount++;
                            entry.Reload();
                        }


                    } while (saveFailed && intTryCount < CommonSetting.DbUpdateConcurrencyTry);

                    if (saveFailed)
                    {
                        throw new Exception("Other process is in progress, please try later.");
                    }//end if


                //}//end using

                if (DocSeq != null && currentSeq != 0)
                {
                    string runningnoformat = "";
                    for (int i = 0; i < DocSeq.Length; i++)
                        runningnoformat += 0;
                    string runningcode = DocSeq.Prefix + currentSeq.ToString(runningnoformat);
                    return runningcode;
                }
                else
                {
                    throw new Exception(DocSeq.DocCode + " not found in DocType or DocSequence table");
                }//end if-else

            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                throw;
            }
            finally { }
        }

        public List<ErrorCodeSU> GetError(string type)
        {
            List<ErrorCodeSU> infos = new List<ErrorCodeSU>();
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                         infos = _uow.Repository<ErrorCodeSU>().GetAsQueryable()
                                                        .Where(r => r.ModelName == type)
                                                        .ToList();
                    //    if (!infos.IsNullOrEmpty())
                    //{
                    //    value = infos.FirstOrDefault();
                    //}//end if
                //}//end using
            }
            catch (Exception ex)
            {
                //value = false;
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos;
        }

        public ErrorCodeSU GetErrorByKey(string key)
        {
            ErrorCodeSU infos = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    infos = _uow.Repository<ErrorCodeSU>().GetAsQueryable()
                                .Where(r => r.FieldName == key).FirstOrDefault();
               // }//end using
            }
            catch (Exception ex)
            {
                //value = false;
                _logger.LogError("Error", ex);
            }
            finally { }

            return infos;
        }

        public string GetCustomerGroupCode()
        {
            string infos = "";
            // IEnumerable<User> us = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    var us = _uow.Repository<UserGroup>().GetAsQueryable(x => x.GroupType == "C");
                    if (!us.IsNullOrEmpty())
                    {
                        infos = us.FirstOrDefault().GroupCode;
                    }//end if
               // }//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                throw;
            }
            finally { }
            return infos;
        }

        public string GetCustomerCodeFromUser()
        {
            string CustomerCode = "";
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    var infos = (from a in _uow.Repository<Customer>().GetAsQueryable(x => x.CustomerName == CurrentUser.Name)
                                 select a.CustomerCode 
                                ).FirstOrDefault();

                    if (infos!=null)
                    {
                        CustomerCode= infos;
                    }//end if

                
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return CustomerCode;
        }

        public string GetCustomerCodeFromUserEntity()
        {
            string CustomerCode = "";
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    var infos = (from a in _uow.Repository<User>().GetAsQueryable(x => x.Username == CurrentUser.Name)
                                 select a.CustomerCode
                                ).FirstOrDefault();

                    if (infos != null)
                    {
                        CustomerCode = infos;
                    }//end if


               // }//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return CustomerCode;
        }

        //public List<string> GetTIDCodeFromUser()
        //{
        //    List<string> CustomerTIDCode = new List<string>();
        //    try
        //    {
        //        //using (var uow = _uowFactory.Create())
        //        //{
        //            var infos = (from a in _uow.Repository<Customer>().GetAsQueryable(x => x.CustomerName == CurrentUser.Name)
        //                         join TIDRepo in _uow.Repository<CustomerTID>().GetAsQueryable()
        //                         on a.CustomerCode equals TIDRepo.CustomerCode
        //                         select TIDRepo.CustomerTIDCode
        //                        );

        //            if (infos != null)
        //            {
        //                CustomerTIDCode = infos.ToList();
        //            }//end if
        //        //}//end using
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error", ex);
        //    }
        //    finally { }
        //    return CustomerTIDCode;
        //}

        public string GetPartnerGroupCode()
        {
            string infos = "";
            // IEnumerable<User> us = null;
            try
            {
                //using (var uow = this._uowFactory.Create())
                //{
                    var us = _uow.Repository<UserGroup>().GetAsQueryable(x => x.GroupType == CommonSetting.UserType.Partner);
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

        public string GetPartnerCodeFromUser()
        {
            string PartnerCode = "";
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    var infos = (from a in _uow.Repository<Partner>().GetAsQueryable(x => x.PartnerName == CurrentUser.Name)
                                 select a.PartnerCode
                                ).FirstOrDefault();

                    if (infos != null)
                    {
                        PartnerCode = infos;
                    }//end if


                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return PartnerCode;
        }

        public string GetPartnerCodeFromUserEntity()
        {
            string PartnerCode = "";
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    var infos = (from a in _uow.Repository<User>().GetAsQueryable(x => x.Username == CurrentUser.Name)
                                 select a.CustomerCode
                                ).FirstOrDefault();

                    if (infos != null)
                    {
                        PartnerCode = infos;
                    }//end if


                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return PartnerCode;
        }

        public List<LookupIndustrySU> GetLookupIndustrySUList()
        {
            object cachedObjectData = _cache.Get(CommonSetting.CacheKey.IndustryCode);
            if (cachedObjectData == null)
            {
                var industryModel = _uow.Repository<LookupIndustrySU>().GetAsQueryable(x => x.Status == 1).OrderBy(x => x.IndustryCode).ToList();

                //Todo Harris (Test) Modify Core
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetPriority(CacheItemPriority.High)
                     .SetSlidingExpiration(TimeSpan.FromSeconds(CommonSetting.HttpRuntimeCacheSecond));

                _cache.Set(CommonSetting.CacheKey.IndustryCode, industryModel, cacheEntryOptions);
                return industryModel;
            }
            return (List<LookupIndustrySU>)cachedObjectData;

        }

        public List<MccCodeSU> GetMccCodeList()
        {
            object cachedObjectData = _cache.Get(CommonSetting.CacheKey.MCC);
            if (cachedObjectData == null)
            {
                var mccModel = _uow.Repository<MccCodeSU>().GetAsQueryable()
                              .ToList();

                //Todo Harris (Test) Modify Core
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetPriority(CacheItemPriority.High)
                     .SetSlidingExpiration(TimeSpan.FromSeconds(CommonSetting.HttpRuntimeCacheSecond));

                _cache.Set(CommonSetting.CacheKey.MCC, mccModel,  cacheEntryOptions);
                return mccModel;
            }
            return (List<MccCodeSU>)cachedObjectData;

        }

        public List<SelectListItem> GetMccCode()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    infos = _uow.Repository<MccCodeSU>().GetAsQueryable()

                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text =  r.Name,
                                                         Value = r.Code
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

        public List<SelectListItem> GetMccCodeByIndustry(int IndustryCode)
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    infos = _uow.Repository<MccCodeSU>().GetAsQueryable(x=>x.IndustryCode==IndustryCode)

                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.Code + " - " + r.MccDescription,
                                                         Value = r.Code
                                                     }).ToList();
               // }//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetIndustryCode()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    infos = _uow.Repository<LookupIndustrySU>().GetAsQueryable(x=>x.Status==1)

                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.Name,
                                                         Value = r.IndustryCode.ToString()
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

        public List<Recipient> GetRecipientList(bool isRefresh = false)
        {
            List<Recipient> cachedObjectData = _cache.Get<List<Recipient>>(CommonSetting.CacheKey.Recipient);         
            if (cachedObjectData == null  || isRefresh == true)
            {
                var model = _uow.Repository<Recipient>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active)
                              .ToList();

                //Todo Harris (Test) Modify Core
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetPriority(CacheItemPriority.High)
                     .SetSlidingExpiration(TimeSpan.FromSeconds(CommonSetting.HttpRuntimeCacheSecond));


                _cache.Set(CommonSetting.CacheKey.Recipient, model, cacheEntryOptions);
                return model;
            }
            return (List<Recipient>)cachedObjectData;

        }

        public List<SelectListItem> GetIntegrationCode()
        {
            List<SelectListItem> infos = null;
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    infos = _uow.Repository<IntegrationSU>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active)
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.IntegrationCode,
                                                         Value = r.IntegrationCode
                                                     }).Distinct().ToList();
                //}//end using
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

        public List<SelectListItem> GetBankList()
        {
            List<SelectListItem> infos = null;
            try
            {
                //using (var uow = _uowFactory.Create())
                //{
                    infos = _uow.Repository<Bank>().GetAsQueryable(x => x.Status == CommonSetting.Status.Active)
                                                     .Select(r => new SelectListItem()
                                                     {
                                                         Text = r.BankCode + " - " + r.BankName,
                                                         Value = r.BankCode
                                                     }).Distinct().OrderBy(x => x.Text).ToList();
               // }//end using
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

