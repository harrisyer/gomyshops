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
    public class CreditRatioReportViewModels
    {
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }
        
        [Display(Name = "MID Code")]
        public string srcMIDCode { get; set; }

        [Display(Name = "Date From")]
        public string srcDateFrom { get; set; }

        [Display(Name = "Date To")]
        public string srcDateTo { get; set; }

        [Display(Name = "Account Manager")]
        public string srcAccountManager { get; set; }

        [Display(Name = "Nature Of Business")]
        public string srcMCCName { get; set; }

        [Display(Name = "Highlight Count Ratio More Than Or Equal To")]
        public decimal? srcHighlightCountRatio { get; set; }

        [Display(Name = "Highlight Amount Ratio More Than Or Equal To")]
        public decimal? srcHighlightAmountRatio { get; set; }

        [Display(Name = "Show Only Count Ratio More Than Or Equal To")]
        public decimal? srcFilterCountRatio { get; set; }

        [Display(Name = "Show Only Amount Ratio More Than Or Equal To")]
        public decimal? srcFilterAmountRatio { get; set; }

        public string hidReportSortBy { get; set; }
        public string hidReportSortOrder { get; set; }

        public List<CreditRatioReportListViewModels> CreditRatioReportList { get; set; }
        public CreditRatioReportListViewModels CreditRatioReportTotal { get; set; }

        public IEnumerable<SelectListItem> MerchantDDL { get; set; }
        public IEnumerable<SelectListItem> MIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> AccountManagerDDL { get; set; }
        public IEnumerable<SelectListItem> MCCNameDDL { get; set; }
    }//end class

    public class CreditRatioReportListViewModels : ActionsListViewModels
    {
        public int RowNo { get; set; }
        public string MerchantCode { get; set; }
        public string MerchantName { get; set; }
        public string MIDCode { get; set; }
        public string AccountManager { get; set; }
        public string TIDOriginURL { get; set; }
        public string MCCName { get; set; }
        public DateTime ProcessStartDate { get; set; }
        public string sProcessStartDate { get; set; }
        public string PaymentMethod { get; set; }

        public decimal SalesTransactionCount { get; set; }
        public decimal SalesTransactionAmount { get; set; }
        public string sSalesTransactionAmount { get; set; }

        public decimal RefundTransactionCount { get; set; }
        public decimal RefundTransactionAmount { get; set; }
        public string sRefundTransactionAmount { get; set; }

        public decimal TransactionCountRatio { get; set; }
        public string sTransactionCountRatio { get; set; }
        public decimal TransactionAmountRatio { get; set; }
        public string sTransactionAmountRatio { get; set; }

        public bool IsHighlight { get; set; }
        
    }//end class
}//end namespace
