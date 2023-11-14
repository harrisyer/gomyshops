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
    public class ProcessorProfileViewModels : ListViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessorProfileViewModels() : base()
        {
        }

        public ProcessorProfileViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //[StringLength(30)]
        //[Display(Name = "User ID")]
        //public string srcUserID { get; set; }

        [StringLength(30)]
        [Display(Name = "Processor Code")]
        public string srcProcessorCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Processor Name")]
        public string srcProcessorName { get; set; }

        [Display(Name = "Profile Status")]
        public string srcStatus { get; set; }     
        public IEnumerable<SelectListItem> StatusDDL { get; set; }

    }//end class

    public class ProcessorDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessorDetailsViewModels() : base()
        {       
        }

        public ProcessorDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Display(Name = "Processor Code")]
        public string ProcessorCode { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Processor Name")]
        [Descriptions]
        public string ProcessorName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [StringLength(20)]
        [AlphaNumeric]
        [Display(Name = "Post Code")]
        public string Zip { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        public string CountryCodeName { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        [AlphaNumericSpace]
        public string State { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        [AlphaNumericSpace]
        public string City { get; set; }

        [Required]
        [AlphaNumericSpace]
        [StringLength(50)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Required]
        [AlphaNumericSpace]
        [StringLength(50)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required]
        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Office No")]
        public string PhoneNo { get; set; }

        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        
        [StringLength(20)]
        [PositiveInteger]
        [Display(Name = "Fax No")]
        public string FaxNo { get; set; }

        
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Settlement Currency Code")]
        public string SettlementCurrencyCode { get; set; }

       
        [Display(Name = "Settlement Currency Name")]
        public string SettlementCurrencyName { get; set; }


        [Display(Name = "Volumn Restriction Currency Code")]
        public string VolumnRestrictionCurrencyCode { get; set; }

        [Required]
        [Display(Name = "Integration Code")]
        public string IntegrationCode { get; set; }

        [Display(Name = "Integration Name")]
        public string IntegrationName { get; set; }

        [Required]
        [Display(Name = "Maximum Ticket Size")]
        [Range(0, Double.PositiveInfinity)]
        public decimal UnitMax { get; set; }

        [Required]
        [Range(0, Double.PositiveInfinity)]
        [Display(Name = "Daily Volume Limit")]
        public decimal DailyMax { get; set; }

        [Required]
        [Display(Name = "Monthly Volume Limit")]
        [Range(0, Double.PositiveInfinity)]
        public decimal MonthlyMax { get; set; }

        [Required]
        [Range(0, Double.PositiveInfinity)]
        [Display(Name = "Yearly Volume Limit ")]
        public decimal YearlyMax { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        [Display(Name = "Monthly Alert Threshold (%)")]
        public decimal Threshold { get; set; }

        [Required]
        [Display(Name = "Processor Day End Time")]
        public string DayEndTimeInMinute { get; set; }

        public int intDayEndTimeInMinute { get; set; }

        //[Required]
        //[Display(Name = "Transaction Card Type")]
        //public List<string> CardIds { get; set; }

        //[Display(Name = "Transaction Card Type")]
        //public MultiSelectList Cards { get; set; }

        [Display(Name = "Transaction Type")]
        public List<CardTypeList> Cards { get; set; }
        
        [Display(Name = "Transaction Currency")]
        public List<string> CurrencyIds { get; set; }

        [Display(Name = "Transaction Currency")]
        public MultiSelectList Currencys { get; set; }

        [StringLength(2)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }

        [Display(Name = "Only Allow On Us Card Bin")]
        public bool OnlyAllowPriorityBin { get; set; }

        [Display(Name = "Only Allow On Us Card Bin")]
        public string sOnlyAllowPriorityBin { get; set; }

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
        public IEnumerable<SelectListItem> IntegrationCodeDDL { get; set; }
        public IEnumerable<SelectListItem> SettlementCurrencyDDL { get; set; }
        public IEnumerable<SelectListItem> VolumnRestrictionCurrencyDDL { get; set; }

    }//end class

    //public class CurrencyTypeList
    //{
    //    [Display(Name = "Currency Value")]
    //    public string CurrencyValue { get; set; }

    //    [Display(Name = "Currency Name")]
    //    public string CurrencyName { get; set; }

    //    public bool CardStatus { get; set; }
    //}

    public class ProcessorProfileListViewModels : ActionsListViewModels
    {
        public string ProcessorCode { get; set; }

        public string ProcessorName { get; set; }

        public string StatusName { get; set; }

        public string Status { get; set; }


    }//end class


}//end namespace
