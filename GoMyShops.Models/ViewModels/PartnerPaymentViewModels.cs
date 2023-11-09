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
    public class PartnerPaymentViewModels
    {
        [StringLength(30)]
        [Display(Name = "Partner Name")]
        public string srcPartnerCode { get; set; }

        [Display(Name = "Payment Status")]
        public string srcStatus { get; set; }

        [Display(Name = "Settlement Date From")]
        public string srcEndDateFrom { get; set; }

        [Display(Name = "Settlement Date To")]
        public string srcStartDateTo { get; set; }

        //public string srcEndDateFrom { get; set; }

        //public string srcEndDateTo { get; set; }

        [Display(Name = "Payment Date From")]
        public string srcPaymentDateFrom { get; set; }

        [Display(Name = "Payment Date To")]
        public string srcPaymentDateTo { get; set; }

        //[Display(Name = "Beneficiary Name")]
        //public string srcBeneficiaryName { get; set; }

        public IEnumerable<SelectListItem> PartnerDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class PartnerPaymentDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartnerPaymentDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string PartnerCode { get; set; }

        [Display(Name = "Partner Name")]
        public string PartnerName { get; set; }

        [Display(Name = "FundingCycle")]
        public int FundingCycle { get; set; }

        [Display(Name = "Settlement Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Settlement End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Settlement Start Date")]
        public string sStartDate { get; set; }

        [Display(Name = "Settlement End Date")]
        public string sEndDate { get; set; }

        [Display(Name = "Payment Date")]
        public string sPaymentDate { get; set; }

        [Display(Name = "Amount Balance")]
        public decimal AmountBalance { get; set; }

        [Display(Name = "Funding Amount")]
        public decimal AmountCurrent { get; set; }

        [Display(Name = "Total Funding Amount")]
        public decimal AmountTotal { get; set; }

        [Required]
        [Range(0, 90000000, ErrorMessage = "Must be Positive value")]
        [Display(Name = "Payment Amount")]
        public decimal AmountPaid { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Display(Name = "Ledger Note")]
        [StringLength(500)]
        public string LedgerNote { get; set; }

        [Display(Name = "Attachment")]
        public string Attachment { get; set; }

        [Display(Name = "Paid Date")]
        public DateTime? PaidDate { get; set; }

        [Display(Name = "Paid Date")]
        public string sPaidDate { get; set; }

        [Display(Name = "Paid By")]
        public string PaidBy { get; set; }

        [Display(Name = "Beneficiary Name")]
        public string BeneficiaryName { get; set; }

        [Display(Name = "Bank Account No.")]
        public string BankAccountNo { get; set; }

        public bool HasUnpaidPreviousPartnerLedger { get; set; }

        public bool IsPaymentDate { get; set; }
    }//end class
}//end namespace
