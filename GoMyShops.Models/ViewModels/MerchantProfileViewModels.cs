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
    public class MerchantProfileViewModels: ListViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantProfileViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [StringLength(30)]
        [Display(Name = "User ID")]
        public string srcUserID { get; set; }

        [StringLength(250)]
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }
        
        [Display(Name = "Merchant Name")]
        public string srcMerchantName { get; set; }

        [Display(Name = "Application Status")]
        public string srcAppStatus { get; set; }

        [Display(Name = "Account Manager")]
        public string srcAccountManager { get; set; }

        [Display(Name = "Profile Status")]
        public string srcStatus { get; set; }

        [Display(Name = "Status Modified Date From")]
        public string srcStatusModifiedDateFrom { get; set; }
        [Display(Name = "Status Modified Date To")]
        public string srcStatusModifiedDateTo { get; set; }
        [Display(Name = "Application Status Modified Date From")]
        public string srcAppStatusModifiedDateFrom { get; set; }
        [Display(Name = "Application Status Modified Date To")]
        public string srcAppStatusModifiedDateTo { get; set; }
        
        public string srcDashBoardActive { get; set; }

        public IEnumerable<SelectListItem> AppStatusDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantCodeDDL { get; set; }
        public IEnumerable<SelectListItem> AccountManagerDDL { get; set; }

    }//end class

    public class MerchantProfileDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantProfileDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentStatus { get; set; }

        public bool ApproveRight { get; set; }

        [Display(Name = "Merchant Code")]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(30)]
        [AlphaNumeric]
        [Display(Name = "Merchant User ID")]
        public string merchatUserid { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        //[AlphaNumeric]
        [Display(Name = "Merchant Login Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [AlphaNumeric]
        [Display(Name = "Verify Password")]
        public string vpassword { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Merchant E-Wallet Master Account")]
        public string ewallet { get; set; }


        [Display(Name = "Application Status")]
        public string appStatus { get; set; }
        [Display(Name = "Application Status")]
        public string appStatusName { get; set; }

        [StringLength(500)]
        [Display(Name = "Application Remark")]
        public string appRemark { get; set; }

        [Display(Name = "Profile Status")]
        public string profilestatus { get; set; }

        [Display(Name = "Profile Status")]
        public string profileStatusName { get; set; }

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }



        [Required]
        [StringLength(100)]
        [Display(Name = "Business Entity Name")]
        //[AlphaNumericSpace]
        //[RegularExpression("^[a-zA-Z0-9 -]+$",ErrorMessage = "The Field is A-Z or 0-9 or Space and -.")]
        public string businessEntityName { get; set; }

        [Required]
        [Display(Name = "Business Type")]
        public string BusinessType { get; set; }
        

        [Required]
        [StringLength(15)]
        //[AlphaNumeric]
        [Display(Name = "Business Registration No")]
        public string idno { get; set; }

        
        [Display(Name = "Partner")]
        public string reseller { get; set; }

        [Display(Name = "Partner")]
        public string resellerDisplay { get; set; }

        public DateTime? ApplicationDate { get; set; }

        [Display(Name = "Application Date")]
        public string sApplicationDate { get; set; }

        [Display(Name = "Application Date")]
        public string sApplicationDateDisplay { get; set; }

        public DateTime EstablishedDate { get; set; }

        [Display(Name = "Date of Incorporation")]
        public string sEstablishedDate { get; set; }

        [Display(Name = "Date of Incorporation")]
        public string sEstablishedDateDisplay { get; set; }

        [Required]
        [Display(Name = "Account Manager")]
        public string accManager { get; set; }

        [Display(Name = "Account Manager")]
        public string accManagerName { get; set; }

        [Display(Name = "Trading as/DBA")]
        public string tradingasdba { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string address1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string address2 { get; set; }

        [Required]
        [StringLength(15)]
        [AlphaNumeric]
        [Display(Name = "Postal Code")]
        public string postcode { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        [AlphaNumericSpace]
        public string State { get; set; }

        [Display(Name = "City")]
        [StringLength(100)]
        [AlphaNumericSpace]
        public string City { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Display(Name = "Country")]
        public string countryName { get; set; }

        [Required]
        [Display(Name = "Company URL")]
        [StringLength(100)]
        //[RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?", ErrorMessage = "Invalid URL.")]
        [RegularExpression(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", ErrorMessage = "Invalid URL.")]
        public string companyurl { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }

        [StringLength(30)]
        [Display(Name = "Title Others")]
        [AlphaSpace]
        public string titleothers { get; set; }

        [Required]
        [StringLength(30)]
        [Descriptions]
        [Display(Name = "Name")]
        public string personnel { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Mobile No")]
        [PositiveInteger]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Office No")]
        public string phone { get; set; }

        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Fax No")]
        public string fax { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Primary Email")]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Finance Email")]
        public string financeEmail { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Risk Email")]
        public string riskEmail { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Technical Email")]
        public string technicalEmail { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Customer Service Email")]
        public string customerServiceEmail { get; set; }

        [Display(Name = "Merchant")]
        public bool contactMerchant { get; set; }

        [Display(Name = "Partner")]
        public bool contactReseller { get; set; }

        //[Required]
        [Display(Name = "Authorization Fee Discount Profile")]
        public string discountgroup { get; set; }

        [Display(Name = "Default Settlement Profile --- Payment")]
        public bool SETTLEMENTGROUP { get; set; }

        [Required]
        [Display(Name = "Card Number Whitelist")]
        public bool cardnowhitelist { get; set; }

        [Required]
        [Display(Name = "Monthly Fee")]
        public decimal FeeMonthly { get; set; }

        [Required]
        [Display(Name = "Annual Fee")]
        public decimal FeeYearly { get; set; }

        [Required]
        [Display(Name = "High Risk Transaction Fee")]
        public decimal FeeHighRisk { get; set; }

        [Required]
        [Display(Name = "Settlement Fee")]
        public decimal FeeSettle { get; set; }

        [Required]
        [Display(Name = "Void Transaction Fee")]
        public decimal FeeVoid { get; set; }

        [Display(Name = "Refund Transaction Fee")]
        public decimal FeeRefund { get; set; }

        [Required]
        [Display(Name = "Declined Sale Transaction Fee")]
        public decimal FeeDecline { get; set; }

        [Required]
        [Display(Name = "Retrieval Transaction Fee")]
        public decimal FeeRetrieval { get; set; }

        [Required]
        [Display(Name = "Chargeback Transaction Fee")]
        public decimal FeeChargeBack { get; set; }

        [Required]
        [Display(Name = "Setup Fee (Note Only)")]
        public decimal FeeSetup { get; set; }

        [Required]
        [Display(Name = "Handling Fee (Note Only)")]
        public decimal FeeWire { get; set; }

        //Bank Information
      
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

        //End Bank Information

        [Display(Name = "White List")]
        public bool WhiteList { get; set; }

        public string MerchantListJson { get; set; }
        public List<MerchantMIDListViewModels> MerchantList { get; set; }

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

        public IEnumerable<SelectListItem> AccountManagerDDL { get; set; }
        public IEnumerable<SelectListItem> TitleDDL { get; set; }
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> PartnerDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantStatusDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> BusinessTypeDDL { get; set; }
        public IEnumerable<SelectListItem> BankDDL { get; set; }
    }

    public class MerchantProfileBankDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantProfileBankDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;        
        }

        [Display(Name = "Merchant Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Merchant Name")]
        public string BusinessEntityName { get; set; }

        //Bank Information

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
        public string BankCode { get; set; }

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

        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> BankDDL { get; set; }
    }

    public class MerchantProfileMid
    {
        public string Code;
        public string RowNo;  
    }

    public class MerchantProfileListViewModels : ActionsListViewModels
    {
        public string CustomerCode { get; set; }

        public string merchatUserid { get; set; }
    
        public string appStatus { get; set; }

        public string appStatusName { get; set; }

        public string CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }

        public string sApplicationDate { get; set; }
        public DateTime? ApplicationDate { get; set; }

        public string AccountManagerCode { get; set; }
        public string AccountManagerName { get; set; }

        public string Status { get; set; }

        public string businessEntityName { get; set; }

        public string CreateCheckBy1 { get; set; }
        public string CreateCheckBy2 { get; set; }
        public string EditCheckBy1 { get; set; }
        public string EditCheckBy2 { get; set; }

        public DateTime? StatusModifiedDate { get; set; }
        public DateTime? AppStatusModifiedDate { get; set; }
    }//end class
}//end namespace
