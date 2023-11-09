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
    public class FundingEntryViewModels
    {
        [StringLength(30)]
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }
   
        [StringLength(100)]
        [Display(Name = "MID")]
        public string srcMIDCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Processor")]
        public string srcProcessorCode { get; set; }
        
        [Display(Name = "Application Status")]
        public string srcApplicationStatus { get; set; }

        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> MIDDDL { get; set; }        
        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
        public IEnumerable<SelectListItem> ApplicationStatusDDL { get; set; }
    }//end class

    public class FundingEntryDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FundingEntryDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool ApproveRight { get; set; }

        [Display(Name = "Entry Code")]
        public string EntryCode { get; set; }

        [Required]
        [Display(Name = "MID")]
        public string MIDCode { get; set; }

        [Required]
        [Display(Name = "Merchant Name")]
        public string CustomerCode { get; set; }   

        [Display(Name = "Processor")]
        public string ProcessorCode { get; set; }

        [Display(Name = "Merchant Name")]
        public string Customer { get; set; }
        
        public string Processor { get; set; }       
        public string MID { get; set; }

        public bool IsShowEldestUnPaid { get; set; }
        public string sCurrentFundingPeriod { get; set; }
        public string sEldestUnpaidFundingPeriod { get; set; }


        [Required]
        [Display(Name = "Entry Description")]
        [StringLength(500)]
        [AlphaNumericSpace]
        public string EntryDescription { get; set; }

        [Display(Name = "Gateway Amount")]
        public decimal GatewayAmount { get; set; }

        [Display(Name = "Processor Amount")]
        public decimal ProcessorAmount { get; set; }

        [Display(Name = "Gateway Amount")]
        public string sGatewayAmount { get; set; }

        [Display(Name = "Processor Amount")]
        public string sProcessorAmount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Entry Type")]
        public string EntryType { get; set; }

        [Required]
        [Display(Name = "Apply To")]
        public string FundingPeriodType { get; set; }

        [Display(Name = "Payment Date")]
        public string sPaymentDate { get; set; }

        public string sEldestDate { get; set; }

        [StringLength(2)]
        [Display(Name = "Status")]
        public string Status { get; set; }

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

        public string CreateCheckBy1 { get; set; }

        public DateTime? CreateCheckBy1Time { get; set; }

        [Display(Name = "Create Check By 1 Time")]
        public string sCreateCheckBy1Time { get; set; }

        public string CreateCheckBy2 { get; set; }

        public DateTime? CreateCheckBy2Time { get; set; }

        [Display(Name = "Create Check By 2 Time")]
        public string sCreateCheckBy2Time { get; set; }

        public string ApplicationStatus { get; set; }

        [Display(Name = "Application Status")]
        public string ApplicationStatusName { get; set; }

        [Display(Name = "Remark")]
        public string ApproveRemark { get; set; }

        [Display(Name = "Previous Remark")]
        public string ApproveRemarkReadOnly { get; set; }

        public DateTime? CurrentFundingPeriodFrom { get; set; }
        public DateTime? CurrentFundingPeriodTo { get; set; }
        public DateTime? CurrentFundingPeriodPaymentDate { get; set; }
        public DateTime? EldestUnpaidPeriodFrom { get; set; }
        public DateTime? EldestUnpaidPeriodTo { get; set; }
        public DateTime? EldestUnpaidPeriodPaymentDate { get; set; }

        public IEnumerable<SelectListItem> EntryTypeDDL { get; set; }
        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> MIDDDL { get; set; }
        public IEnumerable<SelectListItem> FundingPeriodDDL { get; set; }

        public DateTime? ApproveDate { get; set; }
        [Display(Name = "Approve Date")]
        public string sApproveDate { get; set; }

    }//end class

    public class FundingEntryListViewModels : ActionsListViewModels
    {
        public string EntryCode { get; set; }
        public string CustomerCode { get; set; }
        public string Customer { get; set; }
        public string BusinessEntityName { get; set; }
        public string ProcessorCode { get; set; }
        public string Processor { get; set; }
        public string MIDCode { get; set; }
        public string MID { get; set; }
        public string EntryType { get; set; }
        public decimal Amount { get; set; }
        public string sAmount { get; set; }
        public DateTime CreatedTime { get; set; }
        public string sCreatedTime { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string sApproveDate { get; set; }
        public string ApplicationStatus { get; set; }       
        public string ApplicationStatusName { get; set; }
        public string CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public string CreateCheckBy1 { get; set; }
    }//end class
}//end namespace
