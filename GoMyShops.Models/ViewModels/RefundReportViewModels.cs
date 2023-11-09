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

namespace GoMyShops.Models.ViewModels
{
    public class RefundReportViewModels
    {
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }
        
        [Display(Name = "MID Code")]
        public string srcMIDCode { get; set; }

        [Display(Name = "Transaction Date From")]
        public string srcTransactionDateFrom { get; set; }

        [Display(Name = "Transaction Date To")]
        public string srcTransactionDateTo { get; set; }

        [Display(Name = "Refund Date From")]
        public string srcRefundDateFrom { get; set; }

        [Display(Name = "Refund Date To")]
        public string srcRefundDateTo { get; set; }

        [Display(Name = "Payment Reference ID")]
        public string srcPaymentReferenceID { get; set; }

        [Display(Name = "Reason")]
        public string srcReason { get; set; }

        [Display(Name = "Card Type")]
        public string srcCardType { get; set; }

        [Display(Name = "Credit Card No.")]
        public string srcCardNumber1 { get; set; }

        public decimal? srcCardNumber2 { get; set; }

        [Display(Name = "Email")]
        public string srcEmail { get; set; }

        [Display(Name = "Refund Status")]
        public string srcRefundStatus { get; set; }
        
        public decimal TotalTransactionAmount { get; set; }
        
        public string sTotalTransactionAmount { get; set; }

        public decimal TotalRefundAmount { get; set; }

        public string sTotalRefundAmount { get; set; }

        public string hidReportSortBy { get; set; }

        public string hidReportSortOrder { get; set; }
        
        public List<RefundReportListViewModels> RefundReportList { get; set; }

        public IEnumerable<SelectListItem> MerchantDDL { get; set; }
        public IEnumerable<SelectListItem> MIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> ReasonDDL { get; set; }
        public IEnumerable<SelectListItem> CardTypeDDL { get; set; }
        public IEnumerable<SelectListItem> RefundStatusDDL { get; set; }
    }//end class


    public class RefundReportListViewModels : ActionsListViewModels
    {
        public int RowNo { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCode { get; set; }
        public string MIDCode { get; set; }
        public string CardType { get; set; }
        public string CardTypeDesc { get; set; }
        public string PaymentReferenceID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string sTransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public string sTransactionAmount { get; set; }
        public DateTime RefundDate { get; set; }
        public string sRefundDate { get; set; }
        public string Email { get; set; }
        public string CardNumber1 { get; set; }
        public string CardNumber2 { get; set; }
        public string CardNumber { get; set; }
        public decimal RefundAmount { get; set; }
        public string sRefundAmount { get; set; }
        public string Reason { get; set; }
        public int RefundStatus { get; set; }
        public string RefundStatusDesc { get; set; }
        public bool IsException { get; set; }
    }//end class
}//end namespace
