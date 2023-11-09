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
    public class MerchantOnboardingViewModels : ListViewModels
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingViewModels() : base()
        {
           // _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Company Registered Name")]
        public string srcCompanyName { get; set; }

        [Display(Name = "Application Type")]
        public string srcOnBoardingType { get; set; }

        [Display(Name = "Application Status")]
        public string srcApplicationStatus { get; set; }

        [Display(Name = "Create Date")]
        public string srcCreateDate { get; set; }

        public string srcIsGetAllPending { get; set; }

        [Display(Name = "Onboarding Code")]
        public string srcOnboardingCode { get; set; }

        //public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> AppStatusDDL { get; set; }
        public IEnumerable<SelectListItem> OnBoardingTypeDDL { get; set; }
    }//end class

    public class MerchantOnboardingMainDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingMainDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public MerchantOnboardingDetailsViewModels MerchantOnboardingDetailsVM { get; set; }
        public MerchantOnboardingOwnershipDetailsViewModels MerchantOnboardingOwnershipDetailsViewModelsVM { get; set; }
        public MerchantOnboardingOnlineBusinessDetailsViewModels MerchantOnboardingOnlineBusinessDetailsVM { get; set; }
        public MerchantOnboardingBankDetailsViewModels MerchantOnboardingBankDetailsVM { get; set; }
        public MerchantOnboardingPaymentHistoryDetailsViewModels MerchantOnboardingPaymentHistoryDetailsVM { get; set; }
        public MerchantOnboardingMainCheckListDetailsViewModels MerchantOnboardingMainCheckListDetailsVM { get; set; }
        public MerchantOnboardingAccountManagerDetailsViewModels MerchantOnboardingAccountManagerDetailsVM { get; set; }
        public MerchantOnboardingRateSheetDetailsViewModels MerchantOnboardingRateSheetDetailsVM { get; set; }
    }//end class

    public class MerchantOnboardingAccountManagerDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingAccountManagerDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        //[Required]
        [Display(Name = "Account Manager")]
        public string AccountManager { get; set; }

        [Display(Name = "Account Manager")]
        public string AccountManagerName { get; set; }

        public IEnumerable<SelectListItem> AccountManagerDDL { get; set; }

    }//end class

    public class MerchantOnboardingDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool ApproveRight { get; set; }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        [Required]
        [Display(Name = "Application Type")]
        public string OnBoardingType { get; set; }

        [Display(Name = "Application Type")]
        public string OnBoardingTypeName { get; set; }

        //[Required]
        //[Display(Name = "Account Manager")]
        //public string AccountManager { get; set; }

        //[Display(Name = "Account Manager")]
        //public string AccountManagerName { get; set; }

        [StringLength(20)]
        [Display(Name = "User Id")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Company Registration Name")]
        public string BusinessEntityName { get; set; }

        [Required]
        [StringLength(50)]
        //[AlphaNumeric]
        [Display(Name = "Company Registration No")]
        public string IdNo { get; set; }


        [Display(Name = "Company Registration Date")]
        public DateTime CompanyRegistrationDate { get; set; }

        [Display(Name = "Company Registration Date")]
        public string sCompanyRegistrationDate { get; set; }

        [Display(Name = "Company Registration Date")]
        public string sCompanyRegistrationDateDetailFormat { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string LegalAddress1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string LegalAddress2 { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        public string LegalState { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string LegalCity { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string LegalZip { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Country")]
        public string LegalCountryCode { get; set; }

        [Display(Name = "Country")]
        public string LegalCountryName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string Zip { get; set; }

        [StringLength(5)]
        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Business Type")]
        public string BusinessType { get; set; }

        [Display(Name = "Business Type")]
        public string BusinessTypeName { get; set; }

        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Telephone No")]
        public string PhoneNo { get; set; }


        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Fax No")]
        public string FaxNo { get; set; }

        [Display(Name = "No. of employees")]
        [Range(0, 10000000, ErrorMessage = "This Field is between 1 to 9999999")]
        public int NoOfEmployee { get; set; }

        [Display(Name = "Capital Resources")]
        [StringLength(100)]
        public string CapitalResources { get; set; }

        [Display(Name = "Mailling Type")]
        [StringLength(100)]
        public string MaillingType { get; set; }

        [StringLength(100)]
        [Display(Name = "Mailling Type")]
        public string MaillingTypeName { get; set; }

        [Display(Name = "GST Registration No")]
        [StringLength(30)]
        public string GSTRegisterNo { get; set; }

        [Required]
        [Display(Name = "Merchant Category Code")]
        public string MCC { get; set; }

        [Display(Name = "Merchant Category Code")]
        public string MCCCodeName { get; set; }

        [Required]
        [Display(Name = "Industry Code")]
        public string Industry { get; set; }

        [Display(Name = "Industry Code")]
        public string IndustryName { get; set; }

        [Required]
        [StringLength(30)]
        [Descriptions]
        [Display(Name = "Name")]
        public string Personnel { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(30)]
        [Display(Name = "Title Others")]
        [AlphaSpace]
        public string TitleOther { get; set; }

        [StringLength(50)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [PositiveInteger]
        [Display(Name = "Mobile No")]
        [StringLength(30)]
        public string MobileNo { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(2)]
        //public string GenderCode { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Primary Email")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Customer Service Email")]
        public string EmailCustomerService { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Finance Email")]
        public string EmailFinance { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Risk Email")]
        public string EmailRisk { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Technical Email")]
        public string EmailTechnical { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Billing Contact Name")]
        public string BillingContactName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Technical Contact Name")]
        public string TechnicalContactName { get; set; }

        [Display(Name = "Customer Services Contact Name")]
        [StringLength(50)]
        public string CustomerServicesContactName { get; set; }

        [PositiveInteger]
        [Display(Name = "Customer Services Contact No")]
        [StringLength(30)]
        public string CustomerServicesContactNo { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Office area Zoned")]
        public string OfficeAreaZoneCode { get; set; }

        //[Required]
        [Display(Name = "Office area Zoned")]
        public string OfficeAreaZoneName { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Office space (sf)")]
        public string OfficeSpaceCode { get; set; }

        //[Required]
        [Display(Name = "Office space (sf)")]
        public string OfficeSpaceName { get; set; }

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

        [StringLength(2)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        public string StatusName { get; set; }

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }

        [Display(Name = "Application Status")]
        public string AppStatus { get; set; }
        [Display(Name = "Application Status")]
        public string AppStatusName { get; set; }
        
        public string NewAppStatusName { get; set; }

        public DateTime ApplicationDate { get; set; }

        [Display(Name = "Application Date")]
        public string sApplicationDate { get; set; }

        [StringLength(500)]
        [Descriptions]
        [Display(Name = "Approved Document Remark")]
        public string ApplicationDocumentedRemark { get; set; }

        [StringLength(500)]
        [Descriptions]
        [Display(Name = "Approved Rate Sheet Remark")]
        public string ApplicationRemark { get; set; } //Final Approve rate sheet

        public bool IsRateSheet { get; set; }

        [Display(Name = "Modified Documented By")]
        public string ModifiedDocumentedStatusBy { get; set; }

        public DateTime? ModifiedDocumentedStatusTime { get; set; }

        [Display(Name = "Modified Documented Time")]
        public string sModifiedDocumentedStatusTime { get; set; }

        [Display(Name = "Modified Pending Documents By")]
        public string ModifiedPendingDocumentStatusBy { get; set; }

        public DateTime? ModifiedPendingDocumentStatusTime { get; set; }

        [Display(Name = "Modified Pending Documents Time")]
        public string sModifiedPendingDocumentStatusTime { get; set; }

        [Display(Name = "Modified Pending Rate Sheet By")]
        public string ModifiedPendingRateSheetStatusBy { get; set; }

        public DateTime? ModifiedPendingRateSheetStatusTime { get; set; }

        [Display(Name = "Modified Pending Rate Sheet Time")]
        public string sModifiedPendingRateSheetStatusTime { get; set; }

        [Display(Name = "Modified Convert Merchant By")]
        public string ModifiedAcceptStatusBy { get; set; }

        public DateTime? ModifiedAcceptStatusTime { get; set; }

        [Display(Name = "Modified Convert Merchant Time")]
        public string sModifiedAcceptStatusTime { get; set; }

        public IEnumerable<SelectListItem> OnBoardingTypeDDL { get; set; }
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> TitleDDL { get; set; }
        public IEnumerable<SelectListItem> BusinessTypeDDL { get; set; }
        public IEnumerable<SelectListItem> MaillingTypeDDL { get; set; }
        public IEnumerable<SelectListItem> MccCodeDDL { get; set; }
        public IEnumerable<SelectListItem> IndustryCodeDDL { get; set; }
        public IEnumerable<SelectListItem> OfficeAreaZoneCodeDDL { get; set; }
        public IEnumerable<SelectListItem> OfficeSpaceCodeDDL { get; set; }

    }//end class

    public class MerchantOnboardingOwnershipDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingOwnershipDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Name")]
        [Required]
        [AlphaNumericSpace]
        public string Principal1Name { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "NRIC / Passport No")]
        public string Principal1NRICNo { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Office No")]
        public string Principal1OfficeNo { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Designation")]
        [AlphaNumericSpace]
        public string Principal1Designation { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Mobile No")]
        public string Principal1MobileNo { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "This Field is between 0 to 100")]
        [Display(Name = "% Owned")]
        public decimal Principal1PercentageOwned { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime Principal1DOB { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public string sPrincipal1DOB { get; set; }

        [Display(Name = "Date Of Birth")]
        public string sPrincipal1DOBDetailFormat { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Principal1Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string Principal1Address1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string Principal1Address2 { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        public string Principal1State { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string Principal1City { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string Principal1Zip { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Country")]
        public string Principal1CountryCode { get; set; }

        [Display(Name = "Country")]
        public string Principal1CountryName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string Principal1CurrentAddress1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string Principal1CurrentAddress2 { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        public string Principal1CurrentState { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string Principal1CurrentCity { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string Principal1CurrentZip { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Country")]
        public string Principal1CurrentCountryCode { get; set; }

        [Display(Name = "Country")]
        public string Principal1CurrentCountryName { get; set; }

        [Required]
        [Display(Name = "Year(s) There")]
        public int Principal1CurrentStayYear { get; set; }

        [Required]
        [Display(Name = "Year(s) There")]
        public int Principal1StayYear { get; set; }

        [Required]
        [Display(Name = "Office Type")]
        public string Principal1OfficeType { get; set; }

        [Display(Name = "Office Type")]
        public string Principal1OfficeTypeName { get; set; }

        [Required]
        [Display(Name = "Office Type")]
        public string Principal1CurrentOfficeType { get; set; }

        [Display(Name = "Office Type")]
        public string Principal1CurrentOfficeTypeName { get; set; }

        //Principal 2
        [StringLength(100)]
        [Display(Name = "Name")]
        //[Required]
        [AlphaNumericSpace]
        public string Principal2Name { get; set; }

        //[Required]
        [StringLength(30)]
        [Display(Name = "NRIC / Passport No")]
        public string Principal2NRICNo { get; set; }

        //[Required]
        [StringLength(30)]
        [Display(Name = "Office No")]
        public string Principal2OfficeNo { get; set; }

        //[Required]
        [StringLength(50)]
        [Display(Name = "Designation")]
        [AlphaNumericSpace]
        public string Principal2Designation { get; set; }

        //[Required]
        [StringLength(30)]
        [Display(Name = "Mobile No")]
        public string Principal2MobileNo { get; set; }

        //[Required]
        [Range(0, 100, ErrorMessage = "This Field is between 0 to 100")]
        [Display(Name = "% Owned")]
        public decimal Principal2PercentageOwned { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime Principal2DOB { get; set; }

        //[Required]
        [Display(Name = "Date Of Birth")]
        public string sPrincipal2DOB { get; set; }

        //[Required]
        [Display(Name = "Date Of Birth")]
        public string sPrincipal2DOBDetailFormat { get; set; }

        //[Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Principal2Email { get; set; }

        //[Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string Principal2Address1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string Principal2Address2 { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        public string Principal2State { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string Principal2City { get; set; }

        //[Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string Principal2Zip { get; set; }

        //[Required]
        [StringLength(5)]
        [Display(Name = "Country")]
        public string Principal2CountryCode { get; set; }

        [Display(Name = "Country")]
        public string Principal2CountryName { get; set; }

        //[Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string Principal2CurrentAddress1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string Principal2CurrentAddress2 { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        public string Principal2CurrentState { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string Principal2CurrentCity { get; set; }

        //[Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string Principal2CurrentZip { get; set; }

        //[Required]
        [StringLength(5)]
        [Display(Name = "Country")]
        public string Principal2CurrentCountryCode { get; set; }

        [Display(Name = "Country")]
        public string Principal2CurrentCountryName { get; set; }

        //[Required]
        [Display(Name = "Year(s) There")]
        public int Principal2CurrentStayYear { get; set; }

        //[Required]
        [Display(Name = "Year(s) There")]
        public int Principal2StayYear { get; set; }

        //[Required]
        [Display(Name = "Office Type")]
        public string Principal2OfficeType { get; set; }

        [Display(Name = "Office Type")]
        public string Principal2OfficeTypeName { get; set; }

        //[Required]
        [Display(Name = "Office Type")]
        public string Principal2CurrentOfficeType { get; set; }

        [Display(Name = "Office Type")]
        public string Principal2CurrentOfficeTypeName { get; set; }

        public IEnumerable<SelectListItem> Principal1CountryDDL { get; set; }
        //public IEnumerable<SelectListItem> Principal2CountryDDL { get; set; }
        public IEnumerable<SelectListItem> Principal1OfficeTypeDDL { get; set; }

        #region String Display
        public string sPrincipal1PercentageOwned { get; set; }
        public string sPrincipal2PercentageOwned { get; set; }
        #endregion

        public bool IsOwnershipCompleted { get; set; }
    }//end class

    public class MerchantOnboardingOnlineBusinessDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingOnlineBusinessDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        [Display(Name = "Trading Name")]
        [StringLength(100)]
        [Required]
        public string TradingName { get; set; }

        [Display(Name = "Type of products")]
        [Required]
        [StringLength(100)]
        public string ProductType { get; set; }

        [Required]
        [Display(Name = "Targeted Country")]
        public string TargetedCountryCode { get; set; }

        [Display(Name = "Targeted Country")]
        public string TargetedCountryName { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "This Field is between 0 to 1000")]
        [Display(Name = "No. of Years in Business")]
        public int YearOfBusiness { get; set; }

        [Required]
        [Display(Name = "Website URL")]
        [StringLength(100)]
        [RegularExpression(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", ErrorMessage = "Invalid URL.")]
        public string CompanyUrl { get; set; }


        [Display(Name = "Accept Credit Card")]
        public bool IsAcceptPaymentbyCreditCard { get; set; }

        //[Display(Name = "Credit Card Type")]
        //public string AcceptPaymentCreditCardType { get; set; }

        //[Required]
        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Current Transaction Fee (%)")]
        public decimal CurrentTransactionFee { get; set; }

        [Display(Name = "Credit Card Type")]
        public List<CardTypeList> AcceptPaymentCreditCardType { get; set; }

        public List<string> sAcceptPaymentCreditCardTypeList { get; set; }

        [Display(Name = "Current Acquirer")]
        [StringLength(100)]
        public string CurrentAcquirer { get; set; }

        [Display(Name = "Desired Processing Currency")]
        public string DesiredProcessingCurrencyCode { get; set; }

        [Display(Name = "Desired Processing Currency")]
        public string DesiredProcessingCurrencyName { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Estimated Monthly Online Sales")]
        public decimal MonthlyOnlineSales { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Average Billing Amount")]
        public decimal AverageBillingAmount { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Highest Billing Amount")]
        public decimal HighestBillingAmount { get; set; }

        [Display(Name = "Have Physical retail shop")]
        public bool IsHaveRetailShop { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        public decimal MonthlyRetailSales { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Display(Name = "Outlets Count")]
        public int OutletsCount { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Display(Name = "How many days?")]
        public int ProductReceivedDayCount { get; set; }

        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        [Display(Name = "% of sales in this category")]
        public decimal ProductReceivedSalesPercentage { get; set; }

        [Display(Name = "Accept Transactions before customer received product?")]
        public bool IsAcceptTransactionsBeforeReceivedProduct { get; set; }

        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        [Display(Name = "% of deposit")]
        public decimal DepositPrepaidByCustomerPercentage { get; set; }

        [Display(Name = "Offer Warranties")]
        public bool IsOfferWarranties { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Duration of extend service (in weeks)")]
        public decimal WarrantiesInWeeks { get; set; }

        [Display(Name = "Using shopping cart")]
        public bool IsShoppingCart { get; set; }

        [Display(Name = "Shopping Cart Name")]
        [StringLength(100)]
        public string ShoppingCartName { get; set; }

        [Display(Name = "Selling via mobile app")]
        public bool IsMobileApp { get; set; }

        [Display(Name = "OS platform Name")]
        [StringLength(100)]
        public string MobileAppName { get; set; }

        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> DesiredProcessingCurrencyCodeDDL { get; set; }

        #region String Display
        public string sCurrentTransactionFee { get; set; }
        public string sMonthlyOnlineSales { get; set; }
        public string sAverageBillingAmount { get; set; }
        public string sHighestBillingAmount { get; set; }
        public string sMonthlyRetailSales { get; set; }
        public string sProductReceivedSalesPercentage { get; set; }
        public string sDepositPrepaidByCustomerPercentage { get; set; }
        #endregion

        public bool IsOnlineBusinessCompleted { get; set; }
    }//end class

    public class MerchantOnboardingBankDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingBankDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Account No")]
        [Required]
        [PositiveInteger]
        public string BankAccountNo { get; set; }

        [Display(Name = "Account Name")]
        [Required]
        [StringLength(50)]
        [AlphaNumericSpace]
        public string BankAccountName { get; set; }

        //For Display Usage
        [Display(Name = "Bank")]
        public string Bank { get; set; }

        //For Selection Usage
        [Display(Name = "Bank")]
        [StringLength(20)]
        [Required]
        public string BankCode { get; set; }

        [Display(Name = "Bank")]
        public string BankName { get; set; }

        [Display(Name = "Bank Address 1")]
        [Required]
        [StringLength(100)]
        [Descriptions]
        public string BankAddress1 { get; set; }

        [Display(Name = "Bank Address 2")]
        [StringLength(100)]
        [Descriptions]
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
        //[Required]
        public string InstitutionCode { get; set; }

        [Display(Name = "Transit No")]
        [StringLength(10)]
        //[Required]
        public string TransitNo { get; set; }

        [Display(Name = "Bank Swift")]
        [StringLength(20)]
        [AlphaNumeric]
        //[Required]
        public string SwiftNo { get; set; }

        //End Bank Information

        [Display(Name = "Status")]
        public string Status { get; set; }

        //[Display(Name = "Created By")]
        //public string CreatedBy { get; set; }

        //public DateTime CreatedTime { get; set; }

        //[Display(Name = "Created Time")]
        //public string sCreatedTime { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "Modified Time")]
        public string sModifiedTime { get; set; }

        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> BankDDL { get; set; }

        public bool IsBankCompleted { get; set; }
    }//end class

    public class MerchantOnboardingPaymentHistoryDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingPaymentHistoryDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal SalesVolumnLastMonth { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoTransactionsLastMonth { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal ChargebackVolumnLastMonth { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoChargebackLastMonth { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal RefundVolumnLastMonth { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoRefundLastMonth { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal SalesVolumn2MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoTransactions2MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal ChargebackVolumn2MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoChargeback2MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal RefundVolumn2MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoRefund2MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal SalesVolumn3MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoTransactions3MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal ChargebackVolumn3MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoChargeback3MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal RefundVolumn3MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoRefund3MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal SalesVolumn4MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoTransactions4MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal ChargebackVolumn4MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoChargeback4MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal RefundVolumn4MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoRefund4MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal SalesVolumn5MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoTransactions5MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal ChargebackVolumn5MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoChargeback5MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal RefundVolumn5MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoRefund5MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal SalesVolumn6MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoTransactions6MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal ChargebackVolumn6MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoChargeback6MonthAgo { get; set; }

        [Range(0, 100000000, ErrorMessage = "The range is between 0 to 100000000")]
        [Required]
        public decimal RefundVolumn6MonthAgo { get; set; }

        [Range(0, 1000000, ErrorMessage = "The range is between 0 to 1000000")]
        [Required]
        public int NoRefund6MonthAgo { get; set; }


        #region String Display
        public string sSalesVolumnLastMonth { get; set; }
        public string sChargebackVolumnLastMonth { get; set; }
        public string sRefundVolumnLastMonth { get; set; }
        public string sSalesVolumn2MonthAgo { get; set; }
        public string sChargebackVolumn2MonthAgo { get; set; }
        public string sRefundVolumn2MonthAgo { get; set; }
        public string sSalesVolumn3MonthAgo { get; set; }
        public string sChargebackVolumn3MonthAgo { get; set; }
        public string sRefundVolumn3MonthAgo { get; set; }
        public string sSalesVolumn4MonthAgo { get; set; }
        public string sChargebackVolumn4MonthAgo { get; set; }
        public string sRefundVolumn4MonthAgo { get; set; }
        public string sSalesVolumn5MonthAgo { get; set; }
        public string sChargebackVolumn5MonthAgo { get; set; }
        public string sRefundVolumn5MonthAgo { get; set; }
        public string sSalesVolumn6MonthAgo { get; set; }
        public string sChargebackVolumn6MonthAgo { get; set; }
        public string sRefundVolumn6MonthAgo { get; set; }
        #endregion

        public bool IsPaymentHistoryCompleted { get; set; }
    }//end class

    public class MerchantOnboardingMainCheckListDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingMainCheckListDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

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

        public List<MerchantOnboardingCheckListDetailsViewModels> MerchantOnboardingCheckListDetailsVMList { get; set; }

        public DocumentDownloadViewModels ddlVMList { get; set; }

        public bool IsCheckListCompleted { get; set; }
    }

    public class MerchantOnboardingCheckListDetailsViewModels
    {
     

        [Display(Name = "CheckList Code")]
        public string CheckListCode { get; set; }

        [Display(Name = "CheckList Description")]
        public string CheckListDescription { get; set; }

        [Display(Name = "Required")]
        public bool IsRequired { get; set; }

        [Display(Name = "Marking")]
        public bool IsMarked { get; set; }

    }//end class

    public class MerchantOnboardingRateSheetDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantOnboardingRateSheetDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        public DateTime GenerationDate { get; set; }
        [Required]
        [Display(Name = "Generation Date")]
        public string sGenerationDate { get; set; }

        //For Detail Page Usage
        public string sGenerationDateDetailFormat { get; set; }

        public DateTime AgreementDate { get; set; }
        [Required]
        [Display(Name = "Agreement Date")]
        public string sAgreementDate { get; set; }

        [Display(Name = "Settlement Period")]
        public string sSettlementPeriodType { get; set; }

        [Required]
        [Display(Name = "Settlement Period")]
        public int SettlementPeriodType { get; set; }

        //For Detail Page Usage
        public string sAgreementDateDetailFormat { get; set; }

        [Display(Name = "One-time Merchantrade Registration Fee")]
        public decimal RegistrationFee { get; set; }

        [Display(Name = "One-time Credit Card Processing Fee")]
        public decimal ProcessingFee { get; set; }

        [Display(Name = "Merchantrade Yearly Maintenance Fee")]
        public decimal MaintenanceFee { get; set; }

        [Display(Name = "Visa and MasterCard")]
        public decimal CreditCardRate { get; set; }

        [Display(Name = "Financial Process Exchange (FPX)")]
        public decimal FPXFee { get; set; }

        [Display(Name = "Settlement Fee")]
        public decimal SettlementFee { get; set; }

        [Display(Name = "Chargeback Fee")]
        public decimal ChargeBackFee { get; set; }

        public IEnumerable<SelectListItem> SettlementPeriodTypeDDL { get; set; }

        #region String Display
        public string sRegistrationFee { get; set; }
        public string sProcessingFee { get; set; }
        public string sMaintenanceFee { get; set; }
        public string sCreditCardRate { get; set; }
        public string sFPXFee { get; set; }
        public string sSettlementFee { get; set; }
        public string sChargeBackFee { get; set; }
        #endregion

        public bool IsRateSheetCompleted { get; set; }
    }//end class

    public class MerchantOnboardingListViewModels : ActionsListViewModels
    {
        public string OnboardingCode { get; set; }
        public string OnBoardingType { get; set; }
        public string OnBoardingTypeDesc { get; set; }
        public string AccountManagerName { get; set; }
        public string BusinessEntityName { get; set; }
        //public DateTime CreatedTime { get; set; }
        //public string sCreatedTime { get; set; }
        public string ApplicationStatus { get; set; }
        public string ApplicationStatusName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string sApplicationDate { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
    }//end class

    public class MerchantAgreementTemplateViewModels
    {
        [Display(Name = "Application Code")]
        public string OnboardingCode { get; set; }

        [Display(Name = "Generation Date")]
        public DateTime GenerationDate { get; set; }

        [Required]
        [Display(Name = "Generation Date")]
        public string sGenerationDate { get; set; }

        [Display(Name = "Agreement Date")]
        public DateTime AgreementDate { get; set; }

        [Required]
        [Display(Name = "Agreement Date")]
        public string sAgreementDate { get; set; }

        [Display(Name = "Attention Of")]
        public string AttentionOf { get; set; }

        [Display(Name = "FacsimileNo")]
        public string FacsimileNo { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Registration Fee")]
        public decimal RegistrationFee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Processing Fee")]
        public decimal ProcessingFee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Maintenance Fee")]
        public decimal MaintenanceFee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "CreditCard Rate")]
        public decimal CreditCardRate { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "FPX Fee")]
        public decimal FPXFee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "Settlement Fee")]
        public decimal SettlementFee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100000000")]
        [Display(Name = "ChargeBack Fee")]
        public decimal ChargeBackFee { get; set; }
    }

    public class MerchantAgreementTemplate
    {

        public string OnboardingCode { get; set; }


        public DateTime GenerationDate { get; set; }


        public string sGenerationDate { get; set; }


        public DateTime AgreementDate { get; set; }


        public string sAgreementDate { get; set; }


        public string MerchantName { get; set; } //Application Form

        public string CompanyRegNo { get; set; } //Application Form

        public string RegisteredAddress { get; set; } //Application Form

        public string AttentionOf { get; set; }

        public string CorrespondenceAddress { get; set; } //Application Form

        public string TelNo { get; set; }  //Application Form
        public string FacsimileNo { get; set; }
        public string Email { get; set; }  //Application Form
        public string CurrencyCode { get; set; }  //Application Form


        public string RegFee { get; set; }
        public string ProcessingFee { get; set; }
        public string MaintenanceFee { get; set; }
        public string CreditCardRate { get; set; }
        public string FPXFee { get; set; }
        public string SettlementFee { get; set; }
        public string ChargeBackFee { get; set; }

        public string SettlementPeriodType { get; set; }
    }

}//end namespace
