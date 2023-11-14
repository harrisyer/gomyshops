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
    public class PartnerViewModels
    {
        [StringLength(250)]
        [Display(Name = "Partner Name")]
        public string srcPartnerCode { get; set; }
        
        [Display(Name = "Company Name")]
        public string srcPartnerName { get; set; }

        //[Display(Name = "Application Status")]
        //public string srcAppStatus { get; set; }

        [Display(Name = "Profile Status")]
        public string srcStatus { get; set; }
        //public IEnumerable<SelectListItem> AppStatusDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> PartnerCodeDDL { get; set; }

    }//end class

    public class PartnerDetailsViewModels : DetailsViewModels
    {
        //public string CurrentStatus { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartnerDetailsViewModels() : base()
        {    
        }

        public PartnerDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Display(Name = "Partner Code")]
        public string PartnerCode { get; set; }

        [Required]
        [StringLength(30)]
        [AlphaNumeric]
        [Display(Name = "Partner User ID")]
        public string PartnerName { get; set; }
       
        [Display(Name = "Partnership Program")]
        public string PartnerType { get; set; }

        [Display(Name = "Partnership Program")]
        public string PartnerTypeName { get; set; }

        [Required]
        [Display(Name = "Account Manager")]
        public string AccManager { get; set; }
        
        [Required]
        [StringLength(100)]
        [Descriptions]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [StringLength(100)]
        [Descriptions]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        [AlphaNumericSpace]
        public string State { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        [AlphaNumericSpace]
        public string City { get; set; }

        [Required]
        [StringLength(15)]
        [AlphaNumeric]
        [Display(Name = "Post Code")]
        public string Zip { get; set; }

        [StringLength(5)]
        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        public string CountryCodeName { get; set; }

        //[Required]
        [StringLength(50)]
        [AlphaSpace]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "Company URL")]
        [StringLength(100)]
        //[RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?", ErrorMessage = "Invalid URL.")]
        [RegularExpression(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", ErrorMessage = "Invalid URL.")]
        public string Companyurl { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Primary Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Finance Email")]
        public string FinanceEmail { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Risk Email")]
        public string RiskEmail { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Technical Email")]
        public string TechnicalEmail { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Customer Service Email")]
        public string CustomerServiceEmail { get; set; }

        
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Office No")]
        [PositiveInteger]
        public string PhoneNo { get; set; }

        [StringLength(30)]
        [Display(Name = "Fax No")]
        [PositiveInteger]
        public string FaxNo { get; set; }

        [StringLength(2)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [StringLength(2)]
        public string OriginalStatus { get; set; }

        [StringLength(2)]
        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Display(Name = "Status")]
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

        #region Bank Information
        [StringLength(50)]
        [Display(Name = "Account No")]
        public string BankAccountNo { get; set; }

        [Display(Name = "Account Name")]
        [StringLength(50)]
        public string BankAccountName { get; set; }

        //For Display Usage
        [Display(Name = "Bank")]
        public string Bank { get; set; }

        //For Selection Usage
        [Display(Name = "Bank")]
        [StringLength(20)]
        public string BankCode { get; set; }

        [Display(Name = "Bank Address 1")]
        [StringLength(250)]
        public string BankAddress1 { get; set; }

        [Display(Name = "Bank Address 2")]
        [StringLength(250)]
        public string BankAddress2 { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "State")]
        [AlphaNumericSpace]
        public string BankState { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "City")]
        [AlphaNumericSpace]
        public string BankCity { get; set; }

        [Required]
        [StringLength(15)]
        [AlphaNumeric]
        [Display(Name = "Post Code")]
        public string BankZip { get; set; }

        [StringLength(5)]
        [Required]
        [Display(Name = "Country")]
        public string BankCountryCode { get; set; }

        [Display(Name = "Country")]
        public string BankCountryCodeName { get; set; }

        [Display(Name = "Institution Code")]
        [StringLength(10)]
        public string InstitutionCode { get; set; }

        [Display(Name = "Transit No")]
        [StringLength(10)]
        public string TransitNo { get; set; }

        [Display(Name = "Swift No")]
        [StringLength(10)]
        public string SwiftNo { get; set; }
        #endregion

        #region Partner Funding Configuration
        public DateTime? ProcessingStartDate { get; set; }

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
        #endregion

        [Required]
        [Display(Name = "Partner Handling Fee")]
        public decimal PartnerWireFee { get; set; }

        [Display(Name = "Transfer Partner Fee")]
        public bool IsTransferPartnerFeeToGateWay { get; set; }

        public string MIDCodeList { get; set; }
        public string PendingMIDCodeList { get; set; }

        public IEnumerable<SelectListItem> AccountManagerDDL { get; set; }
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> PartnerTypeDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantStatusDDL { get; set; }
        public IEnumerable<SelectListItem> BankDDL { get; set; }

        public IEnumerable<SelectListItem> MdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> WeekHoldingPeriodDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> NewWeekHoldingPeriodDDL { get; set; }
    }

    public class PartnerListViewModels : ActionsListViewModels
    {
        public string PartnerCode { get; set; }

        public string PartnerName { get; set; }

        public string PartnerType { get; set; }

        public string ContactName { get; set; }

        public string AccManager { get; set; }

        public string Status { get; set; }
    }

}//end namespace
