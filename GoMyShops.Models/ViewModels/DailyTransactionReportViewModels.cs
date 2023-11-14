using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Models;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models.ViewModels
{
    public class DailyTransactionReportViewModels
    {
        #region Filtering & General Model

        public long ID { get; set; }

        [Display(Name = "Merchant Name")]
        public string MerchantCode { get; set; }

        [Display(Name = "MID")]
        public string MIDCode { get; set; }

        [Display(Name = "TID")]
        public string TIDCode { get; set; }
        
        public DateTime? TransactionDate { get; set; }

        [Display(Name = "Transaction Date To")]
        public string sTransactionDateTo { get; set; }

        public DateTime? TransactionDateTo { get; set; }

        [Display(Name = "Transaction Date From")]
        public string sTransactionDateFrom { get; set; }

        public DateTime? TransactionDateFrom { get; set; }

        [Display(Name = "Settlement Date To")]
        public string sSettlementDateTo { get; set; }

        public DateTime? SettlementDateTo { get; set; }

        [Display(Name = "Settlement Date From")]
        public string sSettlementDateFrom { get; set; }

        public DateTime? SettlementDateFrom { get; set; }

        [Display(Name = "TID Status")]
        public string TIDStatus { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }
        
        public int TransactionTypeID { get; set; }

        [Display(Name = "Transaction Status")]
        public string TransactionStatus { get; set; }

        public int TransactionStatusID { get; set; }

        [Display(Name = "Transaction Currency")]
        public string CurrencyCode { get; set; }

        [AlphaNumeric]
        [Display(Name = "Payment Reference ID")]
        public string PaymentReferenceID { get; set; }

        [AlphaNumeric]
        [Display(Name = "Processor Transaction ID")]
        public string ProcessorTransactionID { get; set; }

        [AlphaSpace]
        [Display(Name = "Billing First Name")]
        public string BillingFirstName { get; set; }

        [AlphaSpace]       
        [Display(Name = "Billing Last Name")]
        public string BillingLastName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber1 { get; set; }

        public string CardNumber2 { get; set; }

        [StringLength(50)]
        [Display(Name = "Card Type")]
        public string CardType { get; set; }

        public string CardTypeDesc { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [AlphaNumericSpace]
        [Display(Name = "Invoice No.")]
        public string InvoiceNo { get; set; }

        [Display(Name = "Processor")]
        public string ProcessorCode { get; set; }

        public long RowNumber { get; set; }

        public string IntegrationCode { get; set; }

        public int? SettlementStatus { get; set; }

        public int? SuccessSettlementFileInfoID { get; set; }
        public int? ResolvedSettlementFileInfoID { get; set; }

        public DateTime? SettlementDate { get; set; }
        public string ResponseCode { get; set; }
        public string CardNumber { get; set; }

        public string ReportSource { get; set; }
        public string Title { get; set; }
            
        public bool IsException { get; set; }

        public IEnumerable<SelectListItem> ReportTypeDDL { get; set; }
        public IEnumerable<SelectListItem> MIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> TIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> TIDStatusDDL { get; set; }
        public IEnumerable<SelectListItem> TransactionTypeDDL { get; set; }
        public IEnumerable<SelectListItem> TransactionStatusDDL { get; set; }
        public IEnumerable<SelectListItem> CurrencyCodeDDL { get; set; }
        public IEnumerable<SelectListItem> CardTypeDDL { get; set; }
        public IEnumerable<SelectListItem> InvoiceNoDDL { get; set; }
        public IEnumerable<SelectListItem> ReportSortByDDL { get; set; }
        public IEnumerable<SelectListItem> ReportSortOrderDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantTypeDDL { get; set; }
        public IEnumerable<SelectListItem> ProcessorCodeDDL { get; set; }

        public List<DailyTransactionReportSummaryCurrencyViewModels> SummaryCurrencyViewModels { get; set; }
        #endregion
        
        public string hidReportSortBy { get; set; }

        public string hidReportSortOrder { get; set; }
    }//end class

    public class DailyTransactionReportDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DailyTransactionReportDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region Transaction Information

        public long     TransactionRecordID { get; set; }
        [Display(Name = "Payment Reference ID")]
        public string   PaymentReferenceID { get; set; }
        [Display(Name = "Transaction DateTime")]
        public string   TransactionDateTime { get; set; }
        [Display(Name = "Invoice No")]
        public string   InvoiceNo { get; set; }
        [Display(Name = "Settlement Date")]
        public string   SettlementDateTime { get; set; }
        [Display(Name = "Referral Payment Reference ID")]
        public string   ReferralOrderReference { get; set; }
        [Display(Name = "Transaction Status")]
        public string   TransactionStatus { get; set; }
        [Display(Name = "Transaction Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Transaction Amount")]
        public string sAmount { get; set; }
        [Display(Name = "Transaction Type")]
        public string   TransactionType { get; set; }
        [Display(Name = "Purchase Amount")]
        public string sPurchaseAmount { get; set; }
        [Display(Name = "Purchase Amount")]
        public decimal PurchaseAmount { get; set; }
        [Display(Name = "Void/ Refund Reason")]
        public string   Reason { get; set; }
        //[Display(Name = "Shipping/ Freight Amount")]
        //public string   ShippingAmount{ get; set; }
        [Display(Name = "Authorization Code")]
        public string   AuthorizationCode { get; set; }
        //[Display(Name = "Tax Amount")]
        //public string  TaxAmount { get; set; }
        [Display(Name = "Authorization Reference ID")]
        public string   AuthorizationReferenceID { get; set; }//Data Source Missing
        //[Display(Name = "Duty Amount")]
        //public string   DutyAmount  { get; set; }
        [Display(Name = "Response Code")]
        public string   ResponseCode { get; set; }
        [Display(Name = "Order Detail")]
        public string   OrderDetail { get; set; }
        [Display(Name = "Order Description")]
        public string   OrderDescription { get; set; }
        [Display(Name = "Batch ID")]
        public string   BatchID { get; set; }
        [Display(Name = "Comment 1")]
        public string   Comment1 { get; set; }
        [Display(Name = "Comment 2")]
        public string   Comment2 { get; set; }
        [Display(Name = "Processor Transaction ID")]
        public string   ProcessorTransactionID { get; set; } //OrderID
        [Display(Name = "Settlement Status")]
        public string   SettlementStatus { get; set; }
        [Display(Name = "ARN")]
        public string   ARN { get; set; }
        [Display(Name = "Recurring")]
        public string   Recurring { get; set; }
        //[Display(Name = "AVS Response")]
        //public string   AvsResponse { get; set; }
        [Display(Name = "CVV2 Response")]
        public string   Cvv2Response { get; set; }
        [Display(Name = "Recurring Frequency")]
        public int      RecurringFrequency { get; set; }
        [Display(Name = "Recurring End")]
        public string   RecurringEnd { get; set; }
        [Display(Name = "Email Receipt Send")]
        public string   EmailReceiptSent { get; set; } //bit but int in DB
        [Display(Name = "Processor Message")]
        public string   ProcessorMessage { get; set; }
        [Display(Name = "Error Code")]
        public string   ErrorCode { get; set; }
        [Display(Name = "Error Message")]
        public string   ErrorMessage { get; set; }
        [Display(Name = "Billing Descriptor")]
        public string   BillingDescriptor { get; set; }
        [Display(Name = "Transaction Origination")]
        public string   TransactionOriginURL { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Entry Type")]
        public string EntryType { get; set; }
        #endregion

        #region Merchant Information
        [Display(Name = "Merchant Code")]
        public string   CustomerCode { get; set; }
        //[Display(Name = "Merchant Name")]
        //public string   CustomerName { get; set; }
        [Display(Name = "Merchant Name")]
        public string   BusinessEntityName { get; set; }
        [Display(Name = "Merchant Terminal ID (TID)")]
        public string   TIDCode { get; set; }
        [Display(Name = "Processor")]
        public string   ProcessorName { get; set; }
        [Display(Name = "Processor Code")]
        public string   ProcessorCode { get; set; }
        [Display(Name = "MID")]
        public string   MIDCode { get; set; } 
        [Display(Name = "MCC Code")]
        public string   MCCCode { get; set; }
        #endregion

        #region Card Holder Information
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        public string CardNumber1 { get; set; }
        public string CardNumber2 { get; set; }
        [Display(Name = "Card Holder IP")]
        public string CardHolderIp { get; set; }
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }
        [Display(Name = "Card Type")]
        public string CardType { get; set; }
        [Display(Name = "Card Type")]
        public string CardTypeDesc { get; set; }
        [Display(Name = "Card Expiry Date")]
        public string CardExpiryDate { get; set; }

        public int CardExpiryYear { get; set; }
        public int CardExpiryMonth { get; set; }
        #endregion

        #region Billing Information
        [Display(Name = "First Name")]
        public string BillingFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string BillingLastName { get; set; }
        [Display(Name = "Address")]
        public string BillingAddress { get; set; }
        [Display(Name = "City")]
        public string BillingCity { get; set; }
        [Display(Name = "Company")]
        public string BillingCompany { get; set; }//Data Source Missing
        [Display(Name = "State")]
        public string BillingState { get; set; }
        [Display(Name = "Phone")]
        public string BillingPhone { get; set; }
        [Display(Name = "Post Code")]
        public string BillingZIP { get; set; }
        [Display(Name = "Email")]
        public string BillingEmail { get; set; }
        [Display(Name = "Country")]
        public string BillingCountry { get; set; }
        [Display(Name = "DOB")]
        public string BillingDOB { get; set; }//Data Source Missing
        #endregion

        #region Shipping Information
        [Display(Name = "First Name")]
        public string ShippingFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string ShippingLastName { get; set; }
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }
        [Display(Name = "City")]
        public string ShippingCity { get; set; }
        [Display(Name = "State")]
        public string ShippingState { get; set; }
        [Display(Name = "Post Code")]
        public string ShippingZIP { get; set; }
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }
        #endregion

        #region MID Fraud Scrub Rule
        
        public int? FrequencyCardNoDay { get; set; }
              
        public int? FrequencyCardNoWeek { get; set; }
               
        public int? FrequencyCardNoMonth { get; set; }
              
        public int? FrequencyEmailDay { get; set; }
              
        public int? FrequencyEmailWeek { get; set; }
              
        public int? FrequencyEmailMonth { get; set; }
              
        public int? FrequencyPhoneDay { get; set; }
               
        public int? FrequencyPhoneWeek { get; set; }
               
        public int? FrequencyPhoneMonth { get; set; }

        [Display(Name = "Risk Score")]
        public string RiskScore { get; set; }

        [Display(Name = "Fraud Scrubbing Enabled")]
        public string sIsRiskScore { get; set; }

        [Display(Name = "Scrub And Capture Enabled")]
        public string sScrubAndCapture { get; set; }        
       
        [Display(Name = "Decline Rule Description")]
        public string DeclineRuleDescription { get; set; }
        #endregion

        public string CurrencyCode { get; set; }
        public List<RelatedTransaction> RelatedTransactionList { get; set; }
    }//end class

    public class DailyTransactionReportListViewModels : ActionsListViewModels
    {
        public long ID { get; set; }
        public string MerchantCode { get; set; }
        public string MIDCode { get; set; }
        public string TIDCode { get; set; }
        public string TransactionDate { get; set; }
        public string TIDStatus { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
        public string CurrencyCode { get; set; }
        public string PaymentReferenceID { get; set; }
        public string ProcessorTransactionID { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardTypeDesc { get; set; }
        public decimal Amount { get; set; }
        public string sAmount { get; set; }
        public string InvoiceNo { get; set; }
        public string SettlementDate { get; set; }
        public string Email { get; set; }
        public string ResponseCode { get; set; }
        public long RowNumber { get; set; }
    }//end class

    public class DailyTransactionReportSummaryCurrencyViewModels 
    {
        public string CurrencyCode { get; set; }
        public List<DailyTransactionReportSummaryViewModels> SummaryViewModel { get; set; }

    }//end class

    public class DailyTransactionReportSummaryViewModels
    {
        public string TransactionType { get; set; }
        public long PendingCount { get; set; }
        public decimal PendingAmount { get; set; }
        public string sPendingAmount { get; set; }
        public long SuccessfulCount { get; set; }
        public decimal SuccessfulAmount { get; set; }
        public string sSuccessfulAmount { get; set; }
        public long DeclinedCount { get; set; }
        public decimal DeclinedAmount { get; set; }
        public string sDeclinedAmount { get; set; }
        public long CancelledCount { get; set; }
        public decimal CancelledAmount { get; set; }
        public string sCancelledAmount { get; set; }
        public long TotalCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string sTotalAmount { get; set; }

        //For Grouping Purposes
        public string CurrencyCode { get; set; }
    }//end class
}//end namespace

