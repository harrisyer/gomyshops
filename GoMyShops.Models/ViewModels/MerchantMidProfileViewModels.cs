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
    public class MerchantMidProfileViewModels:ListBAL
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantMidProfileViewModels() : base()
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        [StringLength(30)]
        [Display(Name = "User ID")]
        public string srcUserID { get; set; }

        [StringLength(30)]
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }

        //[Display(Name = "Merchant Name")]
        //public string customer { get; set; }

        [StringLength(100)]
        [Display(Name = "MID")]
        public string srcMID { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        [Display(Name = "Application Status")]
        public string srcApplicationStatus { get; set; }

        [Display(Name = "Currency")]
        public string srcCurrency { get; set; }

        [Display(Name = "Processor")]
        public string srcProcessor { get; set; }

        [Display(Name = "Category")]
        public string srcIndustry { get; set; }

        [Display(Name = "MCC")]
        public string srcMCC { get; set; }

        public IEnumerable<SelectListItem> CurrencyDDL { get; set; }
        public IEnumerable<SelectListItem> ApplicationStatusDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantCodeDDL { get; set; }
        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
        public IEnumerable<SelectListItem> MccCodeDDL { get; set; }
        public IEnumerable<SelectListItem> IndustryDDL { get; set; }
    }//end class

    public class MerchantMidProfileDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantMidProfileDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CurrentStatus { get; set; }
        
        public string CallerMenuName { get; set; }

        public bool ApproveRight { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public string CustomerCode { get; set; }

        
        [Display(Name = "Merchant")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Processor")]
        public string processor { get; set; }

        [Display(Name = "Processor")]
        public string processorName { get; set; }

       
        [Display(Name = "Tag")]
        public string tag { get; set; }

        //[Required]
        [StringLength(100)]
        [Display(Name = "MID")]
        public string mid { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "MID Descriptor")]
        public string MIDDESCRIPTOR { get; set; }

        [Required]
        [StringLength(3000)]
        [Display(Name = "Processor Information")]
        public string processorInfo { get; set; }

        [Required]
        [Display(Name = "MCC")]
        public string mcc { get; set; }
       
        [Display(Name = "MCC")]
        public string mccCodeName { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string industry { get; set; }
        
        [Display(Name = "Category")]
        public string sIndustry { get; set; }

        //[Required]
        [Display(Name = "Priority")]
        public string priority { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string currency { get; set; }
        
        [Display(Name = "Currency")]
        public string currencyCodeName { get; set; }

        [Required]
        [Display(Name = "MDR Charge Method")]
        public string MdrChargeType { get; set; }
        
        [Display(Name = "MDR Charge Method")]
        public string sMdrChargeType { get; set; }

        [Required]
        [Display(Name = "Processed Card Type")]
        public bool cardtype { get; set; }

        //[Display(Name = "Profile Status")]
        //public string DELETED { get; set; }
        
        public DateTime? ProcessingStartDate { get; set; }

        [Display(Name = "Include Chargeback and Retrieval into active period")]
        public bool IsChargeback { get; set; }

        [Display(Name = "Include Refund into active period")]
        public bool IsRefund { get; set; }

        [Display(Name = "Email voucher to Merchant/Partner")]
        public bool IsEmailToMerchant { get; set; }

        [Display(Name = "Processing Start Date")]
        public string sProcessingStartDate { get; set; }

        public string PaymentType { get; set; }

        [Display(Name = "Funding Period")]
        [PositiveInteger]
        public int FundingPeriod { get; set; }

        [Display(Name = "Holding Period")]
        [PositiveInteger]
        public int HoldingPeriod { get; set; }

        [Display(Name = "# of Week")]
        public string MdrPaymentDayOfWeek { get; set; }

        [Display(Name = "# of Week")]
        public string sMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "Week start on")]
        public string MdrPaymentWeek { get; set; }

        [Display(Name = "Week start on")]
        public string sMdrPaymentWeek { get; set; }

        [Display(Name = "Payday")]       
        public string MdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Payday")]
        public string sMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string WeekHoldingPeriod { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string sWeekHoldingPeriod { get; set; }

        public bool HasPendingFunding { get; set; }

        public string PendingPaymentType { get; set; }

        [Display(Name = "Funding Period")]
        [PositiveInteger]
        public int? PendingFundingPeriod { get; set; }

        [Display(Name = "Holding Period")]
        [PositiveInteger]
        public int? PendingHoldingPeriod { get; set; }

        [Display(Name = "# of Week")]
        public string PendingMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "# of Week")]
        public string sPendingMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "Week start on")]
        public string PendingMdrPaymentWeek { get; set; }

        [Display(Name = "Week start on")]
        public string sPendingMdrPaymentWeek { get; set; }

        [Display(Name = "Payday")]
        public string PendingMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Payday")]
        public string sPendingMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string PendingWeekHoldingPeriod { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string sPendingWeekHoldingPeriod { get; set; }

        public string NewPaymentType { get; set; }

        [Display(Name = "Funding Period")]
        [PositiveInteger]
        public int? NewFundingPeriod { get; set; }

        [Display(Name = "Holding Period")]
        [PositiveInteger]
        public int? NewHoldingPeriod { get; set; }

        [Display(Name = "# of Week")]
        public string NewMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "# of Week")]
        public string sNewMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "Week start on")]
        public string NewMdrPaymentWeek { get; set; }

        [Display(Name = "Week start on")]
        public string sNewMdrPaymentWeek { get; set; }

        [Display(Name = "Payday")]
        public string NewMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Payday")]
        public string sNewMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string NewWeekHoldingPeriod { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string sNewWeekHoldingPeriod { get; set; }

        public bool HasFundingConfigurationChanges { get; set; }

        #region Reserve

        public bool HasReserveConfigurationChanges { get; set; }

        [Display(Name = "Reserve")]
        public bool IsReserve { get; set; }

        [Display(Name = "Processor Reserve (%)")]
        [Range(0,100,ErrorMessage ="The range is between 0 to 100")]
        public decimal ProcessorReservePercentage { get; set; }

        [Display(Name = "Internal Reserve (%)")]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        public decimal InternalReservePercentage { get; set; }

        [Display(Name = "Funding Period (days)")]
        public int ReserveFundingPeriod { get; set; }

        [Display(Name = "Reserve Period (days)")]
        public int ReservePeriod { get; set; }
        
        public DateTime? ReserveStartDate { get; set; }

        [Display(Name = "Reserve Start Date")]
        public string sReserveStartDate { get; set; }

        public bool HasPendingReserve { get; set; }

        [Display(Name = "Reserve")]
        public bool PendingIsReserve { get; set; }

        [Display(Name = "Processor Reserve (%)")]
        public decimal? PendingProcessorReservePercentage { get; set; }

        [Display(Name = "Internal Reserve (%)")]
        public decimal? PendingInternalReservePercentage { get; set; }

        [Display(Name = "Funding Period (days)")]
        public int? PendingReserveFundingPeriod { get; set; }

        [Display(Name = "Reserve Period (days)")]
        public int? PendingReservePeriod { get; set; }
        
        public DateTime? PendingReserveStartDate { get; set; }

        [Display(Name = "Reserve Start Date")]
        public string sPendingReserveStartDate { get; set; }

        [Display(Name = "Reserve")]
        public bool NewIsReserve { get; set; }

        [Display(Name = "Processor Reserve (%)")]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        public decimal? NewProcessorReservePercentage { get; set; }

        [Display(Name = "Internal Reserve (%)")]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        public decimal? NewInternalReservePercentage { get; set; }

        [Display(Name = "Funding Period (days)")]
        [PositiveInteger]
        public int? NewReserveFundingPeriod { get; set; }

        [Display(Name = "Reserve Period (days)")]
        [PositiveInteger]
        public int? NewReservePeriod { get; set; }
        
        public DateTime? NewReserveStartDate { get; set; }

        [Display(Name = "Reserve Start Date")]
        public string sNewReserveStartDate { get; set; }
        #endregion

        [Display(Name = "Application Status")]
        public string appStatus { get; set; }
        [Display(Name = "Application Status")]
        public string appStatusName { get; set; }

        [Required]
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

        [Display(Name = "Application Modified By")]
        public string ModifiedApplicationStatusBy { get; set; }

        [Display(Name = "Application Modified Time")]
        public DateTime? ModifiedApplicationStatusTime { get; set; }

        [Display(Name = "Application Modified Time")]
        public string sModifiedApplicationStatusTime { get; set; }

        [Display(Name = "Status Modified By")]
        public string ModifiedStatusBy { get; set; }

        [Display(Name = "Status Modified Time")]
        public DateTime? ModifiedStatusTime { get; set; }

        [Display(Name = "Status Modified Time")]
        public string sModifiedStatusTime { get; set; }

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

        public List<CardTypeList> cardTypeList { get; set; }
        public List<MidFeeList> MidFeeList { get; set; }
        public List<MidFeeList> MidMdrList { get; set; }

        public IEnumerable<SelectListItem> ProcessorTagDDL { get; set; }
        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
        public IEnumerable<SelectListItem> MccCodeDDL { get; set; }
        public IEnumerable<SelectListItem> IndustryCodeDDL { get; set; }
        public IEnumerable<SelectListItem> PriorityDDL { get; set; }
        public IEnumerable<SelectListItem> CurrencyDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> MdrChargeTypeDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> WeekHoldingPeriodDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> NewWeekHoldingPeriodDDL { get; set; }
        public IEnumerable<SelectListItem> ProfileStatusDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantStatusDDL { get; set; }
    }

    public class MerchantMidProfileFundingDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantMidProfileFundingDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CurrentStatus { get; set; }

        public bool ApproveRight { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public string CustomerCode { get; set; }

        [Required]
        [Display(Name = "Processor")]
        public string processor { get; set; }


        //[Required]
        [StringLength(100)]
        [Display(Name = "MID")]
        public string mid { get; set; }

      
        [StringLength(250)]
        [Display(Name = "MID")]
        public string MidName { get; set; }        

        public DateTime? ProcessingStartDate { get; set; }

        [Display(Name = "Processing Start Date")]
        public string sProcessingStartDate { get; set; }

        [Display(Name = "Include Chargeback and Retrieval into active period")]
        public bool IsChargeback { get; set; }

        [Display(Name = "Include Refund into active period")]
        public bool IsRefund { get; set; }

        [Display(Name = "Email voucher to Merchant/Partner")]
        public bool IsEmailToMerchant { get; set; }

        public bool HasFundingConfigurationChanges { get; set; }

        public string PaymentType { get; set; }

        [Display(Name = "Funding Period")]
        [PositiveInteger]
        public int FundingPeriod { get; set; }

        [Display(Name = "Holding Period")]
        [PositiveInteger]
        public int HoldingPeriod { get; set; }

        [Display(Name = "# of Week")]
        public string MdrPaymentDayOfWeek { get; set; }

        [Display(Name = "# of Week")]
        public string sMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "Week start on")]
        public string MdrPaymentWeek { get; set; }

        [Display(Name = "Week start on")]
        public string sMdrPaymentWeek { get; set; }

        [Display(Name = "Payday")]
        public string MdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Payday")]
        public string sMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string WeekHoldingPeriod { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string sWeekHoldingPeriod { get; set; }

        public bool HasPendingFunding { get; set; }

        public string PendingPaymentType { get; set; }
        
        [Display(Name = "Funding Period")]
        [PositiveInteger]
        public int? PendingFundingPeriod { get; set; }

        [Display(Name = "Holding Period")]
        [PositiveInteger]
        public int? PendingHoldingPeriod { get; set; }

        [Display(Name = "# of Week")]
        public string PendingMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "# of Week")]
        public string sPendingMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "Week start on")]
        public string PendingMdrPaymentWeek { get; set; }

        [Display(Name = "Week start on")]
        public string sPendingMdrPaymentWeek { get; set; }

        [Display(Name = "Payday")]
        public string PendingMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Payday")]
        public string sPendingMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string PendingWeekHoldingPeriod { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string sPendingWeekHoldingPeriod { get; set; }

        public string NewPaymentType { get; set; }

        [Display(Name = "Funding Period")]
        [PositiveInteger]
        public int? NewFundingPeriod { get; set; }

        [Display(Name = "Holding Period")]
        [PositiveInteger]
        public int? NewHoldingPeriod { get; set; }

        [Display(Name = "# of Week")]
        public string NewMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "# of Week")]
        public string sNewMdrPaymentDayOfWeek { get; set; }

        [Display(Name = "Week start on")]
        public string NewMdrPaymentWeek { get; set; }

        [Display(Name = "Week start on")]
        public string sNewMdrPaymentWeek { get; set; }

        [Display(Name = "Payday")]
        public string NewMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Payday")]
        public string sNewMdrPaymentWeekPayDay { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string NewWeekHoldingPeriod { get; set; }

        [Display(Name = "Holding Period (Week)")]
        public string sNewWeekHoldingPeriod { get; set; }

        [Display(Name = "Application Status")]
        public string appStatus { get; set; }
        [Display(Name = "Application Status")]
        public string appStatusName { get; set; }
       
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
        
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> WeekHoldingPeriodDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> NewWeekHoldingPeriodDDL { get; set; }
        public IEnumerable<SelectListItem> ProfileStatusDDL { get; set; }
    }

    public class MerchantMidProfileReserveDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantMidProfileReserveDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CurrentStatus { get; set; }

        public bool ApproveRight { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public string CustomerCode { get; set; }

        [Required]
        [Display(Name = "Processor")]
        public string processor { get; set; }

        public bool HasReserveConfigurationChanges { get; set; }

        //[Required]
        [StringLength(100)]
        [Display(Name = "MID")]
        public string mid { get; set; }


        [StringLength(250)]
        [Display(Name = "MID")]
        public string MidName { get; set; }

        public DateTime? ProcessingStartDate { get; set; }

        [Display(Name = "Processing Start Date")]
        public string sProcessingStartDate { get; set; }

        [Display(Name = "Reserve")]
        public bool IsReserve { get; set; }

        [Display(Name = "Processor Reserve (%)")]
        public decimal ProcessorReservePercentage { get; set; }

        [Display(Name = "Internal Reserve (%)")]
        public decimal InternalReservePercentage { get; set; }

        [Display(Name = "Funding Period (days)")]
        public int ReserveFundingPeriod { get; set; }

        [Display(Name = "Reserve Period (days)")]
        public int ReservePeriod { get; set; }

        public DateTime? ReserveStartDate { get; set; }

        [Display(Name = "Reserve Start Date")]
        public string sReserveStartDate { get; set; }

        public bool HasPendingReserve { get; set; }

        [Display(Name = "Reserve")]
        public bool PendingIsReserve { get; set; }

        [Display(Name = "Processor Reserve (%)")]
        public decimal? PendingProcessorReservePercentage { get; set; }

        [Display(Name = "Internal Reserve (%)")]
        public decimal? PendingInternalReservePercentage { get; set; }

        [Display(Name = "Funding Period (days)")]
        [PositiveInteger]
        public int? PendingReserveFundingPeriod { get; set; }

        [Display(Name = "Reserve Period (days)")]
        [PositiveInteger]
        public int? PendingReservePeriod { get; set; }

        public DateTime? PendingReserveStartDate { get; set; }

        [Display(Name = "Reserve Start Date")]
        public string sPendingReserveStartDate { get; set; }
        
        [Display(Name = "Reserve")]
        public bool NewIsReserve { get; set; }

        [Display(Name = "Processor Reserve (%)")]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        public decimal? NewProcessorReservePercentage { get; set; }

        [Display(Name = "Internal Reserve (%)")]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        public decimal? NewInternalReservePercentage { get; set; }

        [Display(Name = "Funding Period (days)")]
        [PositiveInteger]
        public int? NewReserveFundingPeriod { get; set; }

        [Display(Name = "Reserve Period (days)")]
        [PositiveInteger]
        public int? NewReservePeriod { get; set; }

        public DateTime? NewReserveStartDate { get; set; }

        [Display(Name = "Reserve Start Date")]
        public string sNewReserveStartDate { get; set; }

        [Display(Name = "Application Status")]
        public string appStatus { get; set; }
        [Display(Name = "Application Status")]
        public string appStatusName { get; set; }
        
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
    }

    public class CardTypeList
    {
        [Display(Name = "Card Value")]
        public string CardValue { get; set; }

        [Display(Name = "Card Name")]
        public string CardName { get; set; }

       
        public bool CardStatus { get; set; }
    }

    public class MidFeeList
    {        
        public string FeeCode { get; set; }       
        public string FeeName { get; set; }

        [Range(0, Double.PositiveInfinity)]
        public decimal Gateway { get; set; }

        [Range(0, Double.PositiveInfinity)]
        public decimal Partner { get; set; }

        [Range(0, Double.PositiveInfinity)]
        public decimal Processor { get; set; }

        public decimal TotalFee { get; set; }
    }

    public class MerchantMidProfileListViewModels : ActionsListViewModels
    {
        public string BusinessEntityName { get; set; }

        public string customer { get; set; }

        public string customerCode { get; set; }

        public string customerName { get; set; }

        public string processor { get; set; }

        public string processorName { get; set; }

        public string Industry { get; set; }
        public string mcc { get; set; }
   
        public string mccCodeName { get; set; }

        public int priority { get; set; }

        public string midCode { get; set; }

        public string currencyCode { get; set; }
        public string currency { get; set; }

        public string appStatus { get; set; }

        public string appStatusName { get; set; }

        public string CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public string Status { get; set; }

        public string CreateCheckBy1 { get; set; }
        public string CreateCheckBy2 { get; set; }
        public string EditCheckBy1 { get; set; }
        public string EditCheckBy2 { get; set; }

        public bool onlyAllowOnUsCardBin { get; set; }
        public string sOnlyAllowOnUsCardBin { get; set; }
    }

}//end namespace
