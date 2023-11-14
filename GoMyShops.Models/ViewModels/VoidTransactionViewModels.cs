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
    public class VoidTransactionViewModels
    {
        #region Filtering & General Model

        public long ID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Merchant")]
        public string MerchantCode { get; set; }

        public string sMerchantCode { get; set; }

        [Display(Name = "MID")]
        public string MIDCode { get; set; }

        [StringLength(30)]
        [Display(Name = "Terminal ID (TID)")]
        public string TIDCode { get; set; }
        
        public DateTime? TransactionDate { get; set; }

        [Display(Name = "Transaction Date")]
        public string sTransactionDateTo { get; set; }

        public DateTime? TransactionDateTo { get; set; }

        [Display(Name = "Transaction Date From")]
        public string sTransactionDateFrom { get; set; }

        public DateTime? TransactionDateFrom { get; set; }

        //[Display(Name = "TID Status")]
        //public string TIDStatus { get; set; }

        //[Display(Name = "Transaction Type")]
        //public string TransactionType { get; set; }
        
        //public int TransactionTypeID { get; set; }

        [Display(Name = "Transaction Status")]
        public string TransactionStatus { get; set; }

        public int TransactionStatusID { get; set; }

        //[Display(Name = "Transaction Currency")]
        //public string CurrencyCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Payment Reference ID")]
        public string PaymentReferenceID { get; set; }

        [StringLength(50)]
        [Display(Name = "Authorization Code")]
        public string AuthorizationCode { get; set; }

        //[Display(Name = "Processor Transaction ID")]
        //public string ProcessorTransactionID { get; set; }

        //[StringLength(250)]
        //[Display(Name = "Billing First Name")]
        //public string BillingFirstName { get; set; }

        //[StringLength(250)]
        //[Display(Name = "Billing Last Name")]
        //public string BillingLastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Card Number")]
        public string CardNumber1 { get; set; }

        public string CardNumber2 { get; set; }

        //[StringLength(50)]
        //[Display(Name = "Card Type")]
        //public string CardType { get; set; }

        //[Display(Name = "Amount")]
        //public decimal? Amount { get; set; }

        [StringLength(50)]
        [Display(Name = "Invoice No.")]
        public string InvoiceNo { get; set; }

        //[Display(Name = "Report Sort By")]
        //public string ReportSortBy { get; set; }

        //public string sReportSortBy { get; set; }

        //public string ReportSortOrder { get; set; }

        //public string sReportSortOrder { get; set; }

        [Range(1,50)]
        [Display(Name = "Records Per Page")]
        public int RecordsPerPage { get; set; }

        public DateTime? SettlementDate { get; set; }
        public string Email { get; set; }
        public string ResponseCode { get; set; }
        public string CardNumber { get; set; }
        public string filteredTransactionDateFrom { get; set; }
        public string filteredTransactionDateTo { get; set; }
        public string RefundStatus { get; set; }

        //public IEnumerable<SelectListItem> ReportTypeDDL { get; set; }
        //public IEnumerable<SelectListItem> MIDCodeDDL { get; set; }
        //public IEnumerable<SelectListItem> TIDCodeDDL { get; set; }
        //public IEnumerable<SelectListItem> TIDStatusDDL { get; set; }
        //public IEnumerable<SelectListItem> TransactionTypeDDL { get; set; }
        //public IEnumerable<SelectListItem> TransactionStatusDDL { get; set; }
        //public IEnumerable<SelectListItem> CurrencyCodeDDL { get; set; }
        //public IEnumerable<SelectListItem> CardTypeDDL { get; set; }
        //public IEnumerable<SelectListItem> InvoiceNoDDL { get; set; }
        //public IEnumerable<SelectListItem> ReportSortByDDL { get; set; }
        //public IEnumerable<SelectListItem> ReportSortOrderDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantTypeDDL { get; set; }
        public IEnumerable<SelectListItem> RefundStatusDDL { get; set; }
        public List<DailyTransactionReportSummaryCurrencyViewModels> SummaryCurrencyViewModels { get; set; }
        #endregion

        #region Hidden Inputs To Save Filters Used For Searching
        public string hidMerchantCode { get; set; }
        
        public string hidMIDCode { get; set; }

        public string hidTIDCode { get; set; }
        
        public string hidsTransactionDateTo { get; set; }

        public string hidsTransactionDateFrom { get; set; }

        public string hidTIDStatus { get; set; }

        public string hidTransactionType { get; set; }

        public string hidTransactionStatus { get; set; }

        public string hidCurrencyCode { get; set; }

        public string hidPaymentReferenceID { get; set; }

        public string hidProcessorTransactionID { get; set; }

        public string hidBillingFirstName { get; set; }

        public string hidBillingLastName { get; set; }

        public string hidCardNumber1 { get; set; }

        public string hidCardNumber2 { get; set; }

        public string hidCardType { get; set; }

        public decimal? hidAmount { get; set; }

        public string hidInvoiceNo { get; set; }

        public string hidReportSortBy { get; set; }

        public string hidReportSortOrder { get; set; }

        public int hidRecordsPerPage { get; set; }
        #endregion
    }//end class

    public class VoidTransactionDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VoidTransactionDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region Transaction Information

        public long TransactionRecordID { get; set; }
        [Display(Name = "Payment Reference ID")]
        public string PaymentReferenceID { get; set; }
        [Display(Name = "Transaction DateTime")]
        public string TransactionDateTime { get; set; }
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        [Display(Name = "Settlement Date")]
        public string SettlementDateTime { get; set; }
        [Display(Name = "Referral Payment Reference ID")]
        public string ReferralOrderReference { get; set; }
        [Display(Name = "Transaction Status")]
        public string TransactionStatus { get; set; }
        [Display(Name = "Transaction Amount")]
        public string Amount { get; set; }
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }
        [Display(Name = "Purchase Amount")]
        public string PurchaseAmount { get; set; }
        [Display(Name = "Void/ Refund Reason")]
        public string Reason { get; set; }
        [Display(Name = "Shipping/ Freight Amount")]
        public string ShippingAmount { get; set; }
        [Display(Name = "Authorization Code")]
        public string AuthorizationCode { get; set; }
        [Display(Name = "Tax Amount")]
        public string TaxAmount { get; set; }
        [Display(Name = "Authorization Reference ID")]
        public string AuthorizationReferenceID { get; set; }//Data Source Missing
        [Display(Name = "Duty Amount")]
        public string DutyAmount { get; set; }
        [Display(Name = "Response Code")]
        public string ResponseCode { get; set; }
        [Display(Name = "Order Detail")]
        public string OrderDetail { get; set; }
        [Display(Name = "Batch ID")]
        public string BatchID { get; set; }
        [Display(Name = "Comment 1")]
        public string Comment1 { get; set; }
        [Display(Name = "Comment 2")]
        public string Comment2 { get; set; }
        [Display(Name = "Processor Transaction ID")]
        public string ProcessorTransactionID { get; set; } //OrderID
        [Display(Name = "Settlement Status")]
        public string SettlementStatus { get; set; }
        [Display(Name = "ARN")]
        public string ARN { get; set; }
        [Display(Name = "Recurring")]
        public string Recurring { get; set; }
        [Display(Name = "AVS Response")]
        public string AvsResponse { get; set; }
        [Display(Name = "CVV2 Response")]
        public string Cvv2Response { get; set; }
        [Display(Name = "Recurring Frequency")]
        public int RecurringFrequency { get; set; }
        [Display(Name = "Recurring End")]
        public string RecurringEnd { get; set; }
        [Display(Name = "Email Receipt Send")]
        public string EmailReceiptSent { get; set; } //bit but int in DB
        [Display(Name = "Processor Message")]
        public string ProcessorMessage { get; set; }
        [Display(Name = "Error Code")]
        public string ErrorCode { get; set; }
        [Display(Name = "Error Message")]
        public string ErrorMessage { get; set; }
        [Display(Name = "Billing Descriptor")]
        public string BillingDescriptor { get; set; }
        [Display(Name = "Transaction Origination")]
        public string TransactionOriginURL { get; set; }
        #endregion

        #region Merchant Information
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Terminal ID (TID)")]
        public string TIDCode { get; set; }
        [Display(Name = "Processor")]
        public string ProcessorName { get; set; }
        [Display(Name = "Processor Code")]
        public string ProcessorCode { get; set; }
        [Display(Name = "MID")]
        public string MIDCode { get; set; }
        [Display(Name = "MCC Code")]
        public string MCCCode { get; set; }
        #endregion

        #region Card Holder Information
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        [Display(Name = "Card Holder IP")]
        public string CardHolderIp { get; set; }
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }
        [Display(Name = "Card Type")]
        public string CardType { get; set; }
        [Display(Name = "Card Expiry Date")]
        public string CardExpiryDate { get; set; }
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
        [Display(Name = "Zip")]
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
        [Display(Name = "Zip")]
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


        [Display(Name = "Country Match")]
        public string CountryMatch { get; set; }

        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Display(Name = "High Risk Country")]
        public string HighRiskCountry { get; set; }

        [Display(Name = "Distance")]
        public int? Distance { get; set; }

        [Display(Name = "IP Region")]
        public string IPRegion { get; set; }

        [Display(Name = "IP City")]
        public string IPCity { get; set; }

        [Display(Name = "IP Longitude")]
        public string IPLongitude { get; set; }

        [Display(Name = "IP Latitude")]
        public string IPLatitude { get; set; }

        [Display(Name = "IP ISP")]
        public string IPISP { get; set; }

        [Display(Name = "IP Organization")]
        public string IPOrganization { get; set; }


        [Display(Name = "Anonymous Proxy")]
        public string AnonymousProxy { get; set; }

        [Display(Name = "Proxy Score")]
        public decimal? ProxyScore { get; set; }

        [Display(Name = "Is Trans Proxy")]
        public string IsTransProxy { get; set; }

        [Display(Name = "Free Mail")]
        public string FreeMail { get; set; }

        [Display(Name = "Carder Email")]
        public string CarderEmail { get; set; }

        [Display(Name = "High Risk Username")]
        public string HighRiskUsername { get; set; }

        [Display(Name = "High Risk Password")]
        public string HighRiskPassword { get; set; }

        [Display(Name = "BIN Name Match")]
        public string BINNameMatch { get; set; }

        [Display(Name = "BIN Phone Match")]
        public string BINPhoneMatch { get; set; }

        [Display(Name = "BIN Name")]
        public string BINName { get; set; }

        [Display(Name = "BIN Phone")]
        public string BINPhone { get; set; }


        [Display(Name = "Customer Phone In Billing Location")]
        public string CustomerPhoneInBillingLocation { get; set; }

        [Display(Name = "Ship Forward")]
        public string ShipForward { get; set; }

        [Display(Name = "City Postal Match")]
        public string CityPostalMatch { get; set; }

        [Display(Name = "Ship City Postal Match")]
        public string ShipCityPostalMatch { get; set; }

        [Display(Name = "Score")]
        public decimal? FraudScore { get; set; }

        [Display(Name = "Risk Score")]
        public decimal? RiskScore { get; set; }

        [Display(Name = "Explanation")]
        public string RiskExplanation { get; set; }

        [Display(Name = "Error")]
        public string RiskError { get; set; }

        #endregion
        #region
        public long? RelatedTransactionRecordID { get; set; }

        [Display(Name = "Payment Reference ID")]
        public string RelatedTransactionPaymentReferenceID { get; set; }

        [Display(Name = "Transaction Date Time")]
        public DateTime? RelatedTransactionTransactionDateTime { get; set; }

        [Display(Name = "Transaction Type")]
        public string RelatedTransactionTransactionType { get; set; }

        [Display(Name = "Amount")]
        public string RelatedTransactionAmount { get; set; }

        [Display(Name = "Transaction Status")]
        public string RelatedTransactionTransactionStatus { get; set; }
        #endregion
    }//end class

    public class VoidTransactionListViewModels : ActionsListViewModels
    {
        public long     TransactionRecordID { get; set; }
        public string   MerchantCode { get; set; }
        public string   MIDCode { get; set; }
        public string   TIDCode { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string   sTransactionDateTime { get; set; }
        public string   CurrencyCode { get; set; }
        public string   PaymentReferenceID { get; set; }
        public string   CardNumber { get; set; }
        public string   CardNumber1 { get; set; }
        public string   CardNumber2 { get; set; }
        public string   AuthorizationCode { get; set; }
        public string   CardType { get; set; }
        public string   ExpiryDate { get; set; }
        public decimal  Amount { get; set; }
        public string   InvoiceNo { get; set; }
        public string   Comment { get; set; }
        public long     RowNumber { get; set; }
        public int      TransactionStatus { get; set; }

    }//end class

    //public class DailyTransactionReportSummaryCurrencyViewModels 
    //{
    //    public string CurrencyCode { get; set; }
    //    public List<DailyTransactionReportSummaryViewModels> SummaryViewModel { get; set; }

    //}//end class

    //public class DailyTransactionReportSummaryViewModels
    //{
    //    public string TransactionType { get; set; }
    //    public long PendingCount { get; set; }
    //    public decimal PendingAmount { get; set; }
    //    public string sPendingAmount { get; set; }
    //    public long SuccessfulCount { get; set; }
    //    public decimal SuccessfulAmount { get; set; }
    //    public string sSuccessfulAmount { get; set; }
    //    public long DeclinedCount { get; set; }
    //    public decimal DeclinedAmount { get; set; }
    //    public string sDeclinedAmount { get; set; }
    //    public long CancelledCount { get; set; }
    //    public decimal CancelledAmount { get; set; }
    //    public string sCancelledAmount { get; set; }
    //    public long TotalCount { get; set; }
    //    public decimal TotalAmount { get; set; }
    //    public string sTotalAmount { get; set; }

    //    //For Grouping Purposes
    //    public string CurrencyCode { get; set; }
    //}//end class

}//end namespace

