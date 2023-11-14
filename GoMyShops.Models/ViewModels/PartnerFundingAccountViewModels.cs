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
    public class PartnerFundingAccountViewModels
    {
        [StringLength(250)]
        [Display(Name = "Partner Name")]
        public string srcPartnerCode { get; set; }
        public IEnumerable<SelectListItem> PartnerCodeDDL { get; set; }

    }//end class

    public class PartnerFundingAccountDetailsViewModels : DetailsViewModels
    {
        //public string CurrentStatus { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartnerFundingAccountDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Display(Name = "Partner Code")]
        public string PartnerCode { get; set; }
        
        [Display(Name = "Partnership Program")]
        public string PartnerTypeName { get; set; }

        //[Required]
        [StringLength(50)]
        [AlphaSpace]
        [Display(Name = "Partner Name")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Account Manager")]
        public string AccManager { get; set; }

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

        public IEnumerable<SelectListItem> MdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> MdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> WeekHoldingPeriodDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentDayOfWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekDDL { get; set; }
        public IEnumerable<SelectListItem> NewMdrPaymentWeekPayDayDDL { get; set; }
        public IEnumerable<SelectListItem> NewWeekHoldingPeriodDDL { get; set; }

        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> BankDDL { get; set; }
    }

    public class PartnerFundingAccountListViewModels : ActionsListViewModels
    {
        public string PartnerCode { get; set; }

        public string PartnerName { get; set; }

        public string PartnerType { get; set; }

        public string ContactName { get; set; }

        public string AccManager { get; set; }

        public string Status { get; set; }
    }

}//end namespace
