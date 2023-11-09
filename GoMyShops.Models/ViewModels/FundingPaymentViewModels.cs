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
    public class FundingPaymentViewModels
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

        [Display(Name = "Settlement Date From")]
        public string srcEndDateFrom { get; set; }

        [Display(Name = "Settlement Date To")]
        public string srcStartDateTo { get; set; }

        [Display(Name = "Payment Date From")]
        public string srcPaymentDateFrom { get; set; }

        [Display(Name = "Payment Date To")]
        public string srcPaymentDateTo { get; set; }

        [Display(Name = "Payment Status")]
        public string srcStatus { get; set; }

        [Display(Name = "Currency Code")]
        public string srcCurrencyCode { get; set; }

        public string srcInvoiceNo { get; set; }

        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> MIDDDL { get; set; }
        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
        public IEnumerable<SelectListItem> CurrencyCodeDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class FundingPaymentDetailsViewModels : DetailsViewModels
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FundingPaymentDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string CustomerCode { get; set; }

        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }

        public string ProcessorCode { get; set; }

        [Display(Name = "Processor")]
        public string Processor { get; set; }
        public string MIDCode { get; set; }

        [Display(Name = "MID")]
        public string MID { get; set; }

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
        [Range(0,90000000,ErrorMessage ="Must be Positive value")]
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

        [Display(Name = "Currency")]
        public string Currency { get; set; }

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

        public bool HasUnpaidPreviousFundingLedger { get; set; }

        public bool IsPaymentDate { get; set; }
    }//end class
}//end namespace
