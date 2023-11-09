using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoMyShops.Commons
{
    public class CommonSetting
    {
        public const string StandardDateFormat = "dd-MM-yyyy";
        public const string StandardDateTimeFormat = "dd-MM-yyyy HH:mm";
        public const string StandardDetailDateFormat = "dd MMM yyyy";
        public const string StandardDetailDateTimeFormat = "dd MMM yyyy HH:mm";

        public const string DateFormatFromDatePicker = "dd/M/yyyy";
        /// <summary>
        /// dd/MM/yy
        /// </summary>
        public const string DateFormatCalendar = "dd/MM/yy";
        /// <summary>
        /// dd/MM/yyyy
        /// </summary>
        public const string DateFormatDDMMYY = "dd/MM/yyyy";
        public const string DateFormatDDMMMYY = "dd-MMM-yyyy";
        public const string DefaultDate = "1900-01-01";
        public const int DefaultDateYear = 1900;
        public const int DefaultDateMonth = 1;
        public const int DefaultDateDay = 1;
        public const string DateFormatYYMMDD = "yyy/MMM/dd";
        public const string DateFormatDDMMYYHHNNSS = "dd/MM/yyyy HH:mm:ss";
        public const string DateFormatDDMMYYHHNN = "dd/MM/yyyy HH:mm";
        public const string DateFormatDDMYYHNNSSTT = "dd/M/yyyy h:mm:ss tt";
        public const string DateFormatDDMMMYYHHNN = "dd MMM yyyy HH:mm";
        public const string DateFormatYYMMDDHHNNSS = "yyy/MMM/dd hh:mm:ss";
        public const string DateFormatDatePickerSales = "dd/MM/yyyy";
        public const string DateFormatMMMDDYYYYHHNNTT = "MMM dd, yyyy hhmm tt";
        public const string DateFormatMMMDDYYYY = "MMM dd, yyyy";
        public const string DateFormatYYYYMMDDHHMMTT_WITHDASH = "yyyy-MM-dd hh:mm tt";
        public const string DropDownListDashes = "-";

        public const string DateFormatYYYYYMMDDHHNNSS = "yyyy/MMM/dd HH:mm:ss";

        public const string TicketDateFormat = "dd MMM yyyy HH:mm";
        public const string DateFormatYYYY_MM_DD_HHNNSS = "yyyy-MM-dd HH:mm:ss";

        public const string FillOutTheBlank = "Please fill out this field.";
        public const string ValidationSumarryHeader = "Please clean the following errors and try again";

        public const string Success = "Successful";
        public const string SuccessModifyRecords = "Successful modify records!";
        public const string SuccessCreateRecords = "Successful Create records!";
        public const string SuccessUploadFile = "File uploaded successfully!";
        public const string SuccessModifyRecordsArgs = "Successful modify {0}!";
        public const string SuccessModifyRecordsUserAccessLAyerArgs = "Successful modify User Access Layer {0} !";
        public const string TrySelectOneRecordsUserAccessLAyerArgs = "Try to select at least one records {0} !";
        public const string SuccessCreateRecordsArgs = "Successful Created {0}!";
        public const string SuccessResetPassword = "Successful Reset Password";
        public const string SuccessRefundTransaction = "Successful Refund Transaction!";
        public const string SuccessCreateRetrieval = "Successful Create Retrieval Transaction!";
        public const string SuccessUpdateRetrieval = "Successful Update Retrieval Transaction!";
        public const string SuccessCreateChargeback = "Successful Create Chargeback Transaction!";
        public const string SuccessUpdateChargeback = "Successful Update Chargeback Transaction!";
        public const string SecurityNameNotFoundArgs = "SecurityName {0} Not found.";
        public const string UserNotFoundArgs = "User {0} Not found.";
        public const string UserResetEmailArgs = "User {0} reset Email successfully.";
        public const string UserResendEmailArgs = "User {0} resend Email successfully.";
        public const string PleaseContactAdmin = "Errors, please contact admin!";
        public const int DbUpdateConcurrencyTry = 5;
        public const string RequestTypePOST = "POST";
        public const string PaymentTimeoutMinutes = "PaymentTimeoutMinutes";
        public const string ExceptionOrdering = "ExceptionOrdering";

        public const string SettlementProcess = "SettlementProcess";
        public const string MigTransForSettlement = "MigTransForSettlement";
        public const string ReconStarted = "Recon process has already started for {0} {1}";
        public const string MigrationProcessOngoing = "Migration process in ongoing for {0} {1}";
        public const string ReconProcess = "ReconProcess";
        public const string ServiceUploadFileProcess = "ServiceUploadFileProcess";
        public const string SuccessRecon = "Recon process has successfully started for {0} {1}";
        public const string FundingEntryAutoDeclineRemark = "Funding Entry Application Automatically Declined By System Due To Funding Cycle Settled.";

        //public const decimal BusTicketDiscountRate = 0.75M;
        public const decimal GST = 0.06M;

        public const string CountryCodeMalaysia = "MY";
        public const string LogoPath = @"c://OzoPayLogo/";
        public const string LoginImagePath = @"/Images/VerifyImages";
        public const string TwoFactorProvider = "PhoneCode";

        public const string DocumentStorageRoot = @"c://Ozopay Documents/somefiles/";
        public const string DocumentRoot = @"c://Ozopay Documents/Documents/";
        public const string OnboardingDocumentStorageRoot = @"c://Ozopay Documents/Onboardingsomefiles/";
        public const string OnboardingDocumentRoot = @"c://Ozopay Documents/OnboardingDocuments/";
        //Caching Setting
        public const int HttpRuntimeCacheSecond = 600;

        public const string UploadDocumentPassword = "Password@111";


        public enum SignInStatus
        {
            //
            // Summary:
            //     Sign in was successful
            Success = 0,
            //
            // Summary:
            //     User is locked out
            LockedOut = 1,
            //
            // Summary:
            //     Sign in requires addition verification (i.e. two factor)
            RequiresVerification = 2,
            //
            // Summary:
            //     Sign in failed
            Failure = 3
        }

        public struct SmsMessage
        {
            public const string Userkey = "ozopayuat";
            public const string Password = "o20paymt18";
            public const string ServiceID = "ozopayuat";
            public const string MsgType = "01";
            public const string ChargeCode = "000";

            public const string TypeProfileUpdate = "PU";
            public const string TypePasswordChange = "PC";
            public const string TypeUserTac = "UT";
        }

        public const string CustomNegativeSignWithTwoDecimalPlaces = "{0:#,##0.00;(#,##0.00);#,##0.00}";

        public struct YesNo
        {
            public const string Yes = "1";
            public const string No = "2";
        }

        public struct Messages
        {
            public const string CodeExistArgs = "{0} {1} existed!";
            public const string RequiredArgs = "{0} {1} is required field!";
            public const string RequiredArgsAnd = "{0} is required field and {1}!";
        }

        public struct SessionId
        {
            public const string ListPageNumberSessionID = "ListPageNumberSessionID";
            public const string AuditKey = "AuditKey";
            public const string LoginPhase = "LoginPhase";
        }

        public struct Ordering
        {
            public const string Accending = "asc";
            public const string Decending = "desc";

        }

        public struct ApplicationStatus
        {
            public const string Decline = "0";
            public const string Approve = "1";
            public const string Pending = "2";
        }

        public struct OnboardingApplicationStatus
        {
            public const string Decline = "0";
            public const string Approved = "1";
            public const string Pending = "2";
            public const string PendingDocuments = "3";
            public const string Documented = "4";
            public const string ApprovedApplication = "5";
            public const string ApprovedRateSheet = "6";
        }

        public struct UserGroup
        {
            public const string CheckMaster = "2";
            public const string Admin = "1";
        }

        public struct GroupCode
        {
            public const string Administrator = "UGR0000001";
            public const string Merchant = "UGR0000003";
            public const string Partner = "UGR0000004";
            public const string AccountManager = "UGR0000005";
            public const string OnBoarding = "UGR0000006";
        }

        public struct SecurityID
        {
            public const int Merchant = 2;
            public const int Admin = 1;
        }

        public struct EmailType
        {
            public const string Primary = "1";
            public const string Finance = "2";
            public const string Risk = "3";
            public const string Technical = "4";
            public const string CustomerService = "5";
        }

        public struct EmailSendType
        {
            public const string SuccessRegister = "SR";
            public const string SuccessSignUp = "SU";
            public const string ProfileUpdate = "PU";
            public const string PasswordChange = "PC";
            public const string MerchantActivate = "MA";
            public const string MerchantUpdate = "MU";
            public const string SaleTransactionApprove = "STA";
            public const string SaleTransactionUpdate = "STU";
            public const string PaymentNotificationUser = "PNU";
            public const string VelocityAlert = "VCA";
            public const string MerchantPayoutAlert = "MPA";
            public const string ChargebackTransactionAlert = "CTA";
            public const string ChargebackTransactionPartner = "CTP";
            public const string ChargebackUpdateAlert = "CUA";
            public const string RefundApprovalAlert = "RAA";
            public const string RefundApprovalUser = "RAU";
            public const string SuccessTempPassword = "STP";
            public const string SuccessCreatePassword = "SCP";
            public const string UnsuccessfulTransaction = "VCB";
            public const string UserAccountLock = "UAL";
            public const string OnboardingApllicationAccepted = "OAA";
            public const string OnboardingApllicationDecline = "OAD";
            public const string OnboardingApllicationRateSheet = "OAR";
        }

        public struct RiskScoreType
        {
            public const int Equal = 1;
            public const int GreaterEqual = 2;
            public const int LessEqual = 3;
            public const int BetweenInclude = 4;
        }

        public struct CacheKey
        {
            public const string IndustryCode = "IndustryCode";
            public const string MCC = "MCC";
            public const string Recipient = "Recipient";
        }

        public struct Status
        {
            public const string Active = "1";
            public const string Inactive = "0";
            public const string Success = "2";
            public const string Pending = "3";
            public const string Cancelled = "4";
            public const string Deleted = "9";
            public const string Fail = "6";
            public const string Suspended = "7";
            public const string Terminated = "8";
        }

        public struct ParamCodes
        {
            public const string UserType = "UserType";
            public const string CommentType = "CommentType";
            public const string UserGroupModuleAccessType = "UGMType";
            public const string BusLayoutType = "BusLayoutType";
            public const string BusCategory = "BusCategory";
            public const string TransactionType = "TransactionType";
            public const string CommissionType = "CommissionType";
            public const string CommissionValueType = "CommissionVType";
            public const string ReportTicketType = "TicketType";
            public const string Designation = "Designation";
            public const string CreditCardType = "CreditCardType";
            public const string Title = "Title";
            public const string AppStatus = "AppStatus";
            public const string Priority = "Priority";
            public const string Currency = "Currency";
            public const string FeeType = "FeeType";
            public const string MdrType = "MdrType";
            public const string ProfileStatus = "ProfileStatus";
            public const string BitYesNo = "BitYesNo";
            public const string EmailType = "EmailType";
            public const string Ordering = "Ordering";
            public const string TxnStatus = "TxnStatus";
            public const string LimitType = "LimitType";
            public const string Partnership = "Partnership";
            public const string EntryType = "EntryType";
            public const string FundingPeriod = "FundingPeriod";
            public const string CardBinType = "CardBinType";
            public const string RefundReason = "RefundReason";
            public const string ThemesType = "ThemesType";
            public const string BusinessType = "BusinessType";
            public const string ConnectionType = "ConnectionType";
            public const string OnBoardingType = "OnBoardingType";
            public const string PaymentWebEndpoint = "PAYMENTWEBENDPOINT";
            public const string OnBoardingCreditCard = "OnBoardingCC";
            public const string OnBoardingApplicationStatus = "OBAppStatus";
            public const string OnBoardingSettlementPeriod = "SettlePeriod";
        }

        public struct AnnouncementType
        {
            public const string InternallyAndPublic = "0";
            public const string Internally = "1";
            public const string Public = "2";
            //public const string PublicOnly = "PO";
        }

        public struct UserType
        {
            public const string Customer = "C";
            public const string Admin = "W";
            public const string Partner = "P";
            public const string AccountManager = "A";
            public const string OnBoarding = "O";
        }

        public struct MIDFeeType
        {
            public const string Fee = "F";
            public const string MDR = "M";
        }

        public struct MIDPaymentType
        {
            public const string Tx = "1";
            public const string Week = "2";
        }

        public struct Designation
        {
            public const string Driver = "D";
            public const string Broker = "B";
            public const string User = "A";
        }

        public struct CommissionType
        {
            public const string Driver = "D";
            public const string Broker = "B";
            public const string Parcel = "P";
        }

        public struct CommissionValueType
        {
            public const string Value = "V";
            public const string Percentage = "P";
        }

        public struct BusCategory
        {
            public const string Express = "E";
            public const string Stage = "S";
        }

        public struct Calendar
        {    //month,agendaWeek,agendaDay,listMonth
            public const string Month = "month";
            public const string Week = "agendaWeek";
            public const string Day = "agendaDay";
            public const string listMonth = "listMonth";
        }

        public struct DocCodes
        {
            public const string UserGroup = "UGR";
            public const string Customer = "CUS";
            public const string MID = "MID";
            public const string TID = "TID";
            public const string TAG = "TAG";
            public const string MIDRisk = "MRK";
            public const string AccountManager = "AMC";
            public const string Partner = "PNR";
            public const string FundingEntry = "FEC";
            public const string CardBinCode = "CBC";
            public const string RecepientConnectionTransaction = "RCT";
            public const string Recipient = "REC";
            public const string RecipientConnection = "RTC";
            public const string Processor = "PCS";
            public const string ProcessorCardBinCode = "PBC";
            public const string Document = "DOC";
            public const string MerchantOnBoarding = "MOB";
            public const string MerchantOnBoardingDocument = "OBC";

        }

        //public struct TicketType
        //{
        //    public const string Departure = "1";
        //    public const string Return = "2";
        //}

        //public struct TicketAdultType
        //{
        //    public const string Adult = "1";
        //    public const string Child = "2";
        //}

        //public struct TicketActionType
        //{
        //    public const string Purchase = "1";
        //    public const string Manual = "2";
        //    public const string BackDate = "3";
        //    public const string Reservation = "4";
        //}
        public struct RecipientConnectionType
        {
            public const int UserGroup = 1;
            public const int User = 2;
            public const int Recipient = 3;
        }

        public struct ConnectionType
        {
            public const string MIDRisk = "1";
            public const string Lockout = "2";
        }

        public struct ResponseType
        {
            public const string MerchantRequest = "MerchantRequest";
            public const string PaymentInitRequest = "PaymentInitRequest";
            public const string ProcessCCPaymentRequest = "ProcessCCPaymentRequest";
            public const string ProcessFPXPaymentRequest = "ProcessFPXPaymentRequest";
        }

        public struct GeneralError
        {
            public const string ErrorCode = "999";
            public const string ErrorDesc = "General Error. Please contact our Administrator";
        }

        public struct CreditCardType
        {
            public const string Visa = "V";
            public const string Master = "M";
            //public const string FPX = "FPX";
            public const string MasterCardElectron = "ME";
            public const string VisaElectron = "VE";
            public const string Amex = "A";
            public const string FPXB2B = "FPXB2B";

        }

        public struct ResponseValidationType
        {
            public const string HotCard = "HotCard";
            public const string TerminalID = "CustomerPaymentPageText";
            public const string CardType = "CardTypeText";
            public const string CardNo = "CardNo";
            public const string CardExpireMonth = "CardExpireMonth";
            public const string CardExpireYear = "CardExpireYear";
            public const string IsSameAsBilling = "IsSameAsBilling";
            public const string CardBin = "CardBin";
            public const string MID = "MID";
            public const string BankID = "BankID";
            public const string CardHolderName = "CardHolderName";
            public const string SecurityCode = "SecurityCode";
            public const string ShipAddress = "ShipAddress";
            public const string ShipFirstName = "ShipFirstName";
            public const string ShipLastName = "ShipLastName";
            public const string ShipCity = "ShipCity";
            public const string ShipState = "ShipState";
            public const string ShipZip = "ShipZip";
            public const string ShipCountry = "ShipCountry";
            public const string SessionID = "SessionID";
            public const string DuplicateOrderDescription = "DuplicateOrderDescription";
            public const string InvalidTransactionStatus = "InvalidTransactionStatus";
            public const string TransactionProcessed = "TransactionProcessed";
            public const string TransactionNotfound = "TransactionNotfound";
            public const string PurchaseAmount = "PurchaseAmount";

            public const string EmailVelocity = "EmailVelocity";
            public const string PhoneVelocity = "PhoneVelocity";
            public const string CreditCardVelocity = "CreditCardVelocity";

            #region Custom Validation Errors
            public const string ValidateOriginURL = "ValidateOriginURL";
            public const string ValidateIPAddress = "ValidateIPAddress";
            public const string ValidateHasProcessorRouting = "ValidateHasProcessorRouting";
            public const string ValidateCardExpiryDate = "ValidateCardExpiryDate";
            public const string ValidateSignatureMatch = "ValidateSignatureMatch";
            public const string ValidateSessionTimeout = "ValidateSessionTimeout";
            public const string ValidateCardTypeSelected = "ValidateCardTypeSelected";
            public const string ValidateMIDCurrency = "ValidateMIDCurrency";

            public const string ValidateHotCard = "ValidateHotCard";
            public const string ValidateCardBin = "ValidateCardBin";

            public const string ValidateProcessorHotCard = "ValidateProcessorHotCard";
            public const string ValidateProcessorCardBin = "ValidateProcessorCardBin";
            public const string ValidateGatewayHotCard = "ValidateGatewayHotCard";
            public const string ValidateGatewayCardBin = "ValidateGatewayCardBin";
            public const string ValidateProcessorMCC = "ValidateProcessorMCC";

            public const string ValidateProcessorMaxAmountPerTicket = "ValidateProcessorMaxAmountPerTicket";
            public const string ValidateProcessorDailyValueLimit = "ValidateProcessorDailyValueLimit";
            public const string ValidateProcessorMonthlyValueLimit = "ValidateProcessorMonthlyValueLimit";
            public const string ValidateProcessorYearlyValueLimit = "ValidateProcessorYearlyValueLimit";

            public const string ValidateMIDMaxAmountPerTicket = "ValidateMIDMaxAmountPerTicket";
            public const string ValidateMIDDailyValueLimit = "ValidateMIDDailyValueLimit";
            public const string ValidateMIDMonthlyValueLimit = "ValidateMIDMonthlyValueLimit";
            public const string ValidateMIDYearlyValueLimit = "ValidateMIDYearlyValueLimit";

            public const string ValidateDailyEmailCountLimit = "ValidateDailyEmailCountLimit";
            public const string ValidateMonthlyEmailCountLimit = "ValidateMonthlyEmailCountLimit";
            public const string ValidateWeeklyEmailCountLimit = "ValidateWeeklyEmailCountLimit";

            public const string ValidateDailyPhoneCountLimit = "ValidateDailyPhoneCountLimit";
            public const string ValidateMonthlyPhoneCountLimit = "ValidateMonthlyPhoneCountLimit";
            public const string ValidateWeeklyPhoneCountLimit = "ValidateWeeklyPhoneCountLimit";

            public const string ValidateDailyCreditCardCountLimit = "ValidateDailyCreditCardCountLimit";
            public const string ValidateMonthlyCreditCardCountLimit = "ValidateMonthlyCreditCardCountLimit";
            public const string ValidateWeeklyCreditCardCountLimit = "ValidateWeeklyCreditCardCountLimit";

            public const string ValidateRiskProfileSetup = "ValidateRiskProfileSetup";
            public const string ValidateFWScoring = "ValidateFWScoring";

            public const string CancelledPayment = "CancelledPayment";

            public const string InvalidPaymentMethod = "InvalidPaymentMethod";

            //LinkPaymentError
            public const string LinkPayment_TerminalID = "LinkPayment_CustomerPaymentPageText";
            public const string LinkPayment_Expired = "LinkPayment_Expired";
            public const string LinkPayment_Processed = "LinkPayment_Processed";

            public const string LinkPayment_PaymentEntry = "LinkPayment_PaymentEntry";

            public const string GeneralError = "GeneralError";
            #endregion
        }

        public struct HotCardType
        {
            public const string Gateway = "1";
            public const string Merchant = "1";
        }

        public struct ResponseStatusCode
        {
            public const string Success = "00000";
            public const string Fail = "00001";

        }

        public struct ActionsType
        {
            public const string List = "List";
            public const string Edit = "Edit";
            public const string Create = "Create";
            public const string Details = "Details";
            public const string Deactived = "Deactived";
        }

        public struct TempDataKeys
        {
            public const string ListPageId = "ListPageId";
            //public const string ListDeactiveURL = "ListDeactiveURL";
            public const string IsPassbackError = "IsPassbackError";
            public const string PassbackErrorMsg = "PassbackErrorMsg";
            public const string InitLoadMenu = "InitLoadMenu";
            public const string UserType = "UserType";
        }

        public struct menuPageSessionId
        {
            public const string Audit = "audit";
            public const string Branch = "branch";
            public const string Distributor = "distributor";
            public const string UserGroup = "usergroup";
            public const string User = "user";
        }

        public struct PaymentType
        {
            public const string FPX = "FPX";
            public const string CreditCard = "CreditCard";
        }
        public struct PaymentCardType
        {
            public const string VISA = "VISA";
            public const string MASTER = "MASTER";
            public const string AMEX = "AMEX";
        }

        public struct IntegrationType
        {
            public const string SALES = "SALES";
            public const string REQUERY = "REQUERY";
            public const string REFUND = "REFUND";
            public const string PULLBANKLIST = "PULLBANKLIST";
            public const string REVERSE = "REVERSE";
        }

        public struct IntegrationCode
        {
            public const string FPX = "FPX";
            public const string ALLIANCEBANK = "ALCB";
            public const string FINEXUS = "FINEXUS";
            public const string FPXB2B = "FPXB2B";
            public const string DUMMY = "DUMMY";
        }

        public struct ChargebackRetrievalFilter
        {
            public const string CAN_BE_RETRIEVAL = "CAN_BE_RETRIEVAL";
            public const string SUCCESS_RETRIEVAL = "SUCCESS_RETRIEVAL";

            public const string FROM_RETRIEVAL = "FROM_RETRIEVAL";
            public const string CAN_BE_CHARGEBACK = "CAN_BE_CHARGEBACK";
            public const string SUCCESS_CHARGEBACK = "SUCCESS_CHARGEBACK";

            public const string SUCCESS_RETRIEVAL_REPRESENTMENT = "SUCCESS_RETRIEVAL_REPRESENTMENT";
            public const string FAILED_RETRIEVAL_REPRESENTMENT = "FAILED_RETRIEVAL_REPRESENTMENT";

            public const string SUCCESS_CHARGEBACK_REPRESENTMENT = "SUCCESS_CHARGEBACK_REPRESENTMENT";
            public const string FAILED_CHARGEBACK_REPRESENTMENT = "FAILED_CHARGEBACK_REPRESENTMENT";
        }

        public struct FundingLedgerStatus
        {
            public const string PROCESSING = "Processing";
            public const string SETTLED = "Settled";
        }

        public enum AppCtrlID
        {
            ApproveCreateMerchantProfile = 1,
            ApproveEditMerchantProfile = 2,
        }

        public enum AuditActionType
        {
            Create = 1,
            Update = 2,
            Delete = 3
        }

        public struct AuditMasterType
        {
            public const int FailedAccess = 1;
        }

        public enum FeeCode
        {
            Monthly = 1,
            RetrievalTransaction = 10,
            ExcessiveRetrieval = 11,
            ChargebackTransaction = 12,
            ExcessiveChargeback = 13,
            Setup = 14,
            Handling = 15,
            Annual = 2,
            HighRiskTransaction = 3,
            Authorize = 5,
            Settlement = 6,
            VoidTransaction = 7,
            RefundTransaction = 8,
            DeclinedSaleTransaction = 9
        }

        public enum MdrChargeMethod
        {
            Percentage_Plus_Flat = 1,
            Percentage_Flat_WhichHigher = 2

        }

        public enum MdrFeeType
        {
            Percentage = 1,
            Flat = 2
        }

        public enum PaymentEntryType
        {
            PaymentPage = 1,
            Snippet = 2,
            API = 3,
            MobileApp = 4,
            EmailLinkPayment = 5
        }

        public struct MerchantResponseCode
        {
            public const string SUCCESS = "000";
            public const string FAILED = "001";
            public const string CANCELLED = "002";
            public const string PENDING = "003";
            public const string VELOCITYFAILED = "899";
        }

        public enum PaymentPageSource
        {
            PaymentOption = 1,
            CreditCard = 2,
            FPX = 3
        }

        public struct VelocityType
        {
            public const string Velocity899 = "Vel899";
            public const string VelocityDecline = "VelDecline";
        }

        public struct originalPasswordValidatorMessage
        {
            public const string RequiredLength_1 = "Passwords must be at least";
            public const string RequiredLength_2 = "characters";
            public const string RequireNonLetterOrDigit = "Passwords must have at least one non letter or digit character";
            public const string RequireDigit = "Passwords must have at least one digit ('0'-'9')";
            public const string RequireUppercase = "Passwords must have at least one uppercase ('A'-'Z')";
            public const string RequireLowercase = "Passwords must have at least one lowercase ('a'-'z')";
        }

        public struct translatedPasswordValidatorMessage
        {
            public const string RequiredLength = "Password must contain {0} characters";
            public const string RequireNonLetterOrDigit = "Password must contain special character";
            public const string RequireDigit = "Password must contain number";
            public const string RequireUppercase = "Password must contain uppercase";
            public const string RequireLowercase = "Password must contain lowercase";
        }

        public struct SysParameterSUParamCode
        {
            public const string PAYOUTRPTDEBITACCNO = "PAYOUTRPTDEBITACCNO";
            public const string PAYOUTRPTTXNREFNO = "PAYOUTRPTTXNREFNO";
        }

        public struct TransactionWatcherCriticalReason
        {
            public const string TRANSACTIONAMOUNT = "TRANSACTIONAMOUNT";
            public const string CARDFREQUENCY = "CARDFREQUENCY";
        }

        public struct ReportSource
        {
            public const string DAILYTRANSACTIONREPORT = "DailyTransactionReport";
            public const string RECON_GATEWAYTXN = "Recon_GatewayTxn";
            public const string RECON_RECONSUCCESSTXN = "Recon_ReconSuccessTxn";
            public const string RECON_RECONRESOLVEDTXN = "Recon_ReconResolvedTxn";
        }


    }//end class
}//end namespace