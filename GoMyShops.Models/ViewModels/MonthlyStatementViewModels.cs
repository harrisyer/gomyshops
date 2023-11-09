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
    public class MonthlyStatementViewModels
    {
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }
        
        [Display(Name = "Year")]
        public string srcGenerateForYear { get; set; }

        [Display(Name = "Month")]
        public string srcGenerateForMonth { get; set; }

        public IEnumerable<SelectListItem> MerchantDDL { get; set; }
        public IEnumerable<SelectListItem> GenerateForYearDDL { get; set; }
        public IEnumerable<SelectListItem> GenerateForMonthDDL { get; set; }
    }//end class

    public class MonthlyStatementListViewModels : ActionsListViewModels
    {
        public string MerchantName { get; set; }
        public string MerchantCode { get; set; }
        public decimal PayableAmount { get; set; }
        public string sPayableAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string sPaidAmount { get; set; }
        public int PayableLedgerCount { get; set; }
        public int PaidLedgerCount { get; set; }
        public int GenerateForYear { get; set; }
        public int GenerateForMonth { get; set; }
        public string sGenerateForMonth { get; set; }

        public ActionsListDetails FLRLJson { get; set; }
    }//end class

    public class MonthlyStatementDetailsViewModels
    {
        [Display(Name = "Merchant Code")]
        public string MerchantCode { get; set; }
        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }
        [Display(Name = "Generated For Year")]
        public int GenerateForYear { get; set; }
        public int GenerateForMonth { get; set; }
        [Display(Name = "Generated For Month")]
        public string sGenerateForMonth { get; set; }
        [Display(Name = "Settlement Period")]
        public string sGenerateForPeriod { get; set; }
        [Display(Name = "Statement Generated Time")]
        public string sGeneratedTime { get; set; }
        
        public string sTotalPayableAmount { get; set; }
        public string sTotalPaidAmount { get; set; }

        //List Is Due To MID Grouping In View
        public List<MonthlyStatementMID> MonthlyStatementMIDList { get; set; }

        //For Report Use, Same Function As Above List But Without MID Grouping
        public List<MonthlyStatementLedger> MonthlyStatementLedgerList { get; set; }
    }//end class

    public class MonthlyStatementLedger
    {
        [Display(Name = "MID Code")]
        public string MIDCode { get; set; }
        [Display(Name = "Ledger Type")]
        public string LedgerType { get; set; }
        public int FundingCycle { get; set; }
        [Display(Name = "Ref No.")]
        public string ReferenceNo { get; set; }
        public DateTime StartDate { get; set; }
        [Display(Name = "Start Date")]
        public string sStartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Display(Name = "End Date")]
        public string sEndDate { get; set; }
        public DateTime PaymentDate { get; set; }
        [Display(Name = "Payment Date")]
        public string sPaymentDate { get; set; }
        public decimal PayableAmount { get; set; }
        [Display(Name = "Payable Amount")]
        public string sPayableAmount { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        public decimal? PaidAmount { get; set; }
        [Display(Name = "Paid Amount")]
        public string sPaidAmount { get; set; }
        public DateTime? PaidDate { get; set; }
        [Display(Name = "Paid Date")]
        public string sPaidDate { get; set; }
    }

    public class MonthlyStatementMID
    {
        public string MIDCode { get; set; }
        public List<MonthlyStatementLedger> MonthlyStatementLedgerList { get; set; }
    }
}//end namespace
