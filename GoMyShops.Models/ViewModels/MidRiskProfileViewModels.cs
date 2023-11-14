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
    public class MidRiskProfileViewModels
    {

        [StringLength(250)]
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }

        [Display(Name = "Merchant Name")]
        public string srcMerchantName { get; set; }

        [StringLength(100)]
        [Display(Name = "MID")]
        public string srcMID { get; set; }

        [Display(Name = "Application Status")]
        public string srcApplicationStatus { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }     
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> ApplicationStatusDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantCodeDDL { get; set; }

    }//end class

    public class MidRiskProfileDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MidRiskProfileDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentStatus { get; set; }
        public bool ApproveRight { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Merchant")]
        public string CustomerCode { get; set; }


        [StringLength(30)]
        [Display(Name = "Merchant Name")]
        public string CustomerName { get; set; }
        
        [StringLength(30)]
        [Display(Name = "Processor")]
        public string processor { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "MID")]
        public string mid { get; set; }      

        //[Display(Name = "MID Risk")]
        //public string MidRiskNo { get; set; }

        [StringLength(250)]
        [Display(Name = "MID Descriptor")]
        public string MIDDESCRIPTOR { get; set; }

        [Display(Name = "MCC")]
        public string mcc { get; set; }

        [Display(Name = "MCC")]
        public string mccCodeName { get; set; }

        [Display(Name = "Currency")]
        public string currency { get; set; }

        [Display(Name = "Currency")]
        public string currencyCodeName { get; set; }

        public List<CardTypeList> cardTypeList { get; set; }

        [Display(Name = "Shipping Goods")]
        public string IsShippingGoods { get; set; }
        
        [Display(Name = "Email Receipt")]
        public string IsEmailReceipt { get; set; }

        //[Display(Name = "Email Receipt to Merchant")]
        //public string IsEmailReceiptMerchant { get; set; }

        

        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email Receipt Sender")]
        public string EmailReceiptSender { get; set; }

        [Required]
        [Display(Name = "Email Receipt to Merchant")]
        public string IsEmailReceiptMerchant { get; set; }

        //[Display(Name = "Email Receipt to Merchant")]
        //public string emailReceiptReceipient { get; set; }

        [Display(Name = "Merchant Emails")]
        
        public List<string> EmailIds { get; set; }

        [Display(Name = "Merchant Emails")]
        public MultiSelectList Emails { get; set; }

        //[Required]
        //[Display(Name = "Refund Option")]
        //public string refundOption { get; set; }

        [Required]
        [Range(0, 10000000000000, ErrorMessage = "This Field is 0 to 10000000000000")]
        [Display(Name = "Maximum Ticket Size")]
        public decimal UnitMax { get; set; }

        [Required]
        [Range(0, 10000000000000, ErrorMessage = "This Field is 0 to 10000000000000")]
        [Display(Name = "Daily Volume Limit")]
        public decimal DailyMax { get; set; }

        [Required]
        [Range(0, 10000000000000, ErrorMessage = "This Field is 0 to 10000000000000")]
        [Display(Name = "Monthly Volume Limit")]
        public decimal MonthlyMax { get; set; }

        [Required]
        [Range(0, 10000000000000, ErrorMessage = "This Field is 0 to 10000000000000")]
        [Display(Name = "Yearly Volume Limit")]
        public decimal YearlyMax { get; set; }

        public string CurrentDailyValumeTitle { get; set; }
        public decimal CurrentDailyValume { get; set; }
        public string CurrentMonthlyValumeTitle { get; set; }
        public decimal CurrentMonthlyValume { get; set; }
        public string CurrentYearlyValumeTitle { get; set; }
        public decimal CurrentYearlyValume { get; set; }


        [Required]        
        [Display(Name = "Processor Reserve (%)")]
        public decimal Reserve { get; set; }

        [Required]        
        [Display(Name = "Internal Reserve (%) ")]
        public decimal InternalReserve { get; set; }

        [Display(Name = "Total Reserve (%)")]
        public decimal TotalReserve { get; set; }

        [Required]      
        [PositiveInteger]  
        [Display(Name = "Reserve Month(s)")]
        public int ReserveMonth { get; set; }

        [Display(Name = "Recipient Connection Code")]
        public string RecipientConnectionCode { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        [Display(Name = "Refund Threshold(%)")]
        public decimal RefundThreshold { get; set; }

        [Required]
        [Display(Name = "Refund Limit (%) ")]
        public decimal RefundLimit { get; set; }
        
        [Display(Name = "Current Refund (%) ")]
        public decimal CurrentRefund { get; set; }

        [Required]     
        [Display(Name = "Max Number of Transaction")]
        [PositiveInteger]
        public int HourlyDeclineCount { get; set; }

        [Required]       
        [Display(Name = "Max Number of Transaction Velocity (899)")]
        [PositiveInteger]
        public int HourlyDecline899Count { get; set; }

        public List<MIDRiskThresholdLimitViewModels> TLVMDecline { get; set; }
        public List<MIDRiskThresholdLimitViewModels> TLVMRetrieval { get; set; }
        public List<MIDRiskThresholdLimitViewModels> TLVMChargeBack { get; set; }
        public List<MIDRiskThresholdLimitViewModels> TLVMFraud { get; set; }

        [Display(Name = "Reject If Card Holder IP Does Not Match Billing Country")]
        public bool IPCountryChecking { get; set; }

        [Display(Name = "Card Velocity Check")]
        public bool CardVelocity { get; set; }

        //[PositiveInteger]
        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyCardNoDay { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyCardNoWeek { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyCardNoMonth { get; set; }

        [Display(Name = "Email Velocity Check")]
        public bool EmailVelocity { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyEmailDay { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyEmailWeek { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyEmailMonth { get; set; }

        [Display(Name = "Phone Velocity Check")]
        public bool PhoneVelocity { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyPhoneDay { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyPhoneWeek { get; set; }

        [Range(0, 100, ErrorMessage = "This Field is between 1 to 99")]
        public int FrequencyPhoneMonth { get; set; }

        [Display(Name = "Card Issuing Country Restriction")]
        public bool CardIssuingCountry { get; set; }

        [Display(Name = "Accepted Countries")]
        //public string CountryList { get; set; }

        public List<SelectListItem> CountryList { get; set; }

        public string CountryBlackListJson { get; set; }
        [Display(Name = "Blacklisted Countries")]
        //
        public List<SelectListItem> CountryBlackList { get; set; }


        public List<MIDRiskScoreViewModels> MIDRiskScoreList { get; set; }
        //[Display(Name = "Extended Scrubbing Rules")]
        //public bool ThirdPartyScrub { get; set; }

        //public string ThirdPartyScrubCaptureOnly { get; set; }

        //public string AnonymousProxy { get; set; }
        //public string IsTransProxy { get; set; }
        //public string CarderEmail { get; set; }
        //public string BinMatch { get; set; }
        //public string FreeMail { get; set; }
        //public string HighRiskCountry { get; set; }
        //public string CityPostalMatch { get; set; }
        //public string ShipCityPostalMatch { get; set; }

        //public int Distance { get; set; }

        //[Range(0, 10000000, ErrorMessage = "This Field is Positive value")]
        //public decimal ProxyScore { get; set; }

        //[Range(0, 10000000, ErrorMessage = "This Field is Positive value")]
        //public decimal FraudScore { get; set; }

        [Display(Name = "Enable Fraud Scrubbing Rule")]
        public bool IsRiskScore { get; set; } // Fauld Wall

        [Display(Name = "Scrub and Capture")]
        public bool ScrubOnly { get; set; } // Fauld Wall      

        public string ScoreType { get; set; }        

        [Display(Name = "Accept Score")]
        [Range(0, 999, ErrorMessage = "This Field is 0 to 999")]
        public int ScoreValue { get; set; }

        [Display(Name = "Application Status")]
        public string appStatus { get; set; }
        [Display(Name = "Application Status")]
        public string appStatusName { get; set; }

        //[Required]
        [Display(Name = "Profile Status")]
        public string profileStatus { get; set; }

        [Display(Name = "Profile Status")]
        public string profileStatusName { get; set; }

        [Display(Name = "Active")]
        public bool CheckBoxStatus { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [Display(Name = "Created Time")]
        public string sCreatedTime { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "Modified Time")]
        public string sModifiedTime { get; set; }

        [Display(Name = "Create Check By 1")]
        public string CreateCheckBy1 { get; set; }

        public DateTime? CreateCheckBy1Time { get; set; }

        [Display(Name = "Create Check By 1 Time")]
        public string sCreateCheckBy1Time { get; set; }

        [Display(Name = "Create Check By 2")]
        public string CreateCheckBy2 { get; set; }

        public DateTime? CreateCheckBy2Time { get; set; }

        [Display(Name = "Create Check By 2 Time")]
        public string sCreateCheckBy2Time { get; set; }

        [Display(Name = "Edit Check By 1")]
        public string EditCheckBy1 { get; set; }

        public DateTime? EditCheckBy1Time { get; set; }

        [Display(Name = "Edit Check By 1 Time")]
        public string sEditCheckBy1Time { get; set; }

        [Display(Name = "Edit Check By 2")]
        public string EditCheckBy2 { get; set; }

        public DateTime? EditCheckBy2Time { get; set; }

        [Display(Name = "Edit Check By 2 Time")]
        public string sEditCheckBy2Time { get; set; }

        [StringLength(500)]
        [Descriptions]
        [Display(Name = "Remark")]
        public string ApproveRemark { get; set; }

        [Display(Name = "Previous Remark")]
        public string ApproveRemarkReadOnly { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> CustomerMidDDL { get; set; }
        public IEnumerable<SelectListItem> ProfileStatusDDL { get; set; }
        public IEnumerable<SelectListItem> ScoreTypeDDL { get; set; }
        public IEnumerable<SelectListItem> RecipientConnectionCodeDDL { get; set; }
    }//end class

    public class MIDRiskScoreViewModels
    {
        public int ScoreId { get; set; }

        public bool IsSelect { get; set; }

        public int ScoreType { get; set; } //><=

        public string ScoreName { get; set; }

        public int StartScore { get; set; }

        public int EndScore { get; set; }


    }

    public class MIDRiskThresholdLimitViewModels
    {
      
        [StringLength(2)]
        public string LimitType { get; set; }
       
        [StringLength(5)]
        public string CardType { get; set; }

        [StringLength(100)]
        public string CardName { get; set; }

        [Range(0, 10000000, ErrorMessage = "Positive value")]
        public decimal Threshold { get; set; }

        [Range(0, 10000000, ErrorMessage = "Positive value")]
        public decimal Limit { get; set; }
    }//end class

    public class MidRiskProfileListViewModels : ActionsListViewModels
    {
        public string customer { get; set; }

        public string customerCode { get; set; }

        public string customerName { get; set; }

        public string BusinessEntityName { get; set; } 

        public string processor { get; set; }
      
        public string mid { get; set; }
       
        public string mccCode { get; set; }
       
        public string currencyCodeName { get; set; }

        public string appStatus { get; set; }

        public string appStatusName { get; set; }

        public string CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public string Status { get; set; }

        public string CreateCheckBy1 { get; set; }
        public string CreateCheckBy2 { get; set; }
        public string EditCheckBy1 { get; set; }
        public string EditCheckBy2 { get; set; }

    }//end class

}//end namespace
