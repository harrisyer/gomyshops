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
    public class PartnerConfigurationViewModels
    {
        [StringLength(250)]
        [Display(Name = "Partner Name")]
        public string srcPartnerCode { get; set; }
        public IEnumerable<SelectListItem> PartnerCodeDDL { get; set; }

    }//end class

    public class PartnerConfigurationDetailsViewModels : DetailsViewModels
    {
        //public string CurrentStatus { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartnerConfigurationDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
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
        [Display(Name = "Partner Wire Fee")]
        public decimal PartnerWireFee { get; set; }

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
    }

    public class PartnerConfigurationListViewModels : ActionsListViewModels
    {
        public string PartnerCode { get; set; }

        public string PartnerName { get; set; }

        public string PartnerType { get; set; }

        public string ContactName { get; set; }

        public string AccManager { get; set; }

        public string Status { get; set; }
    }

}//end namespace
