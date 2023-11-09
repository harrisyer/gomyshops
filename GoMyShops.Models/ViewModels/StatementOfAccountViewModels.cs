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
    public class StatementOfAccountViewModels
    {
        [StringLength(30)]
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }

        [StringLength(100)]
        [Display(Name = "MID")]
        public string srcMIDCode { get; set; }

        [StringLength(100)]
        [Display(Name = "SOA Number")]
        public string srcSOANo { get; set; }

        [StringLength(2)]
        [Display(Name = "Ledger Type")]
        public string srcLedgerType { get; set; }

        [Display(Name = "Start Date From")]
        public string srcStartDateFrom { get; set; }

        [Display(Name = "Start Date To")]
        public string srcStartDateTo { get; set; }

        [Display(Name = "End Date From")]
        public string srcEndDateFrom { get; set; }

        [Display(Name = "End Date To")]
        public string srcEndDateTo { get; set; }

        public string srcInvoiceNo { get; set; }

        public IEnumerable<SelectListItem> MerchantCodeDDL { get; set; }
        public IEnumerable<SelectListItem> MIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> LedgerTypeDDL { get; set; }
    }//end class

    public class StatementOfAccountListViewModels : ActionsListViewModels
    {
        public string SOANo { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCode { get; set; }
        public string MIDCode { get; set; }
        public string LedgerType { get; set; }
        public string LedgerTypeDisplay { get; set; }
        public int FundingCycle { get; set; }
        public string CurrencyCode { get; set; }
        public string sStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string sEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public string sTotalTaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string sTotalAmount { get; set; }

        //Linking
        public ActionsListDetails FLJson { get; set; }
    }//end class

    public class StatementOfAccountDetailsViewModels
    {
        #region Header
        public int Id { get; set; }
        [Display(Name = "SOA Number")]
        public string SOANo { get; set; }
        [Display(Name = "Ledger Reference Number")]
        public string LedgerReferenceNo { get; set; }
        [Display(Name = "Ledger Type")]
        public string LedgerTypeDisplay { get; set; }
        public string LedgerType { get; set; }
        [Display(Name = "MID")]
        public string MIDCode { get; set; }
        [Display(Name = "Merchant")]
        public string CustomerNameCode { get; set; }
        //[Display(Name = "Currency")]
        //public string CurrencyCode { get; set; }
        [Display(Name = "Funding Cycle")]
        public int FundingCycle { get; set; }
        [Display(Name = "Settlement Period")]
        public string sSettlementPeriod { get; set; }
        public DateTime SettlementPeriodTo { get; set; }
        public DateTime SettlementPeriodFrom { get; set; }
        public DateTime GeneratedTime { get; set; }
        [Display(Name = "Generated Time")]
        public string sGeneratedTime { get; set; }
        #endregion

        [Display(Name = "Setup Fee")]
        public string sSetupFee { get; set; }
        [Display(Name = "Monthly Fee")]
        public string sMonthlyFee { get; set; }
        [Display(Name = "Annual Fee")]
        public string sAnnualFee { get; set; }

        public decimal? MonthlyFee { get; set; }
        public decimal? AnnualFee { get; set; }
        public decimal? SetupFee { get; set; }

        public decimal TotalAmount { get; set; }
        [Display(Name = "Total Charged Amount")]
        public string sTotalAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public string sTotalTaxAmount { get; set; }
        
        [Display(Name = "Net Payable")]
        public string sNetPayableAmount { get; set; }

        #region Tax Rate
        public bool IsShowTax { get; set; }
        public decimal FundingLedgerTaxRate { get; set; }
        public List<Tax> TaxList { get; set; }
        #endregion
        
        public List<StatementOfAccountTransaction> StatementOfAccountTransactionList { get; set; }
    }//end class

    public class StatementOfAccountTransaction
    {
        public DateTime TransactionDate { get; set; }
        public string sTransactionDate { get; set; }
        public List<StatementOfAccountTransactionDetails> StatementOfAccountTransactionDetailsList { get; set; }

        //Duplicated Field ForReport Usage
        public int      FlattenedFundingLedgerTransactionGroup { get; set; }
        public DateTime FlattenedTransactionDate { get; set; }
        public string   FlattenedsTransactionDate { get; set; }
        public string   FlattenedProcessedTransactionType { get; set; }
        public string   FlattenedDisplayProcessedTransactionType { get; set; }
        public int      FlattenedTransactionCount { get; set; }
        public string   FlattenedBaseAmount { get; set; }
        public string   FlattenedFlatFeeRate { get; set; }
        public string   FlattenedPercentageFeeRate { get; set; }
        public string   FlattenedDisplayRate { get; set; }
        public decimal  FlattenedTransactionAmount { get; set; }
        public string   FlattenedsTransactionAmount { get; set; }
        public decimal  FlattenedTaxRate { get; set; }
    }

    public class StatementOfAccountTransactionDetails
    {
        public int FundingLedgerTransactionGroup { get; set; }
        public DateTime TransactionDate { get; set; }
        public string sTransactionDate { get; set; }
        public string ProcessedTransactionType { get; set; }
        public string DisplayProcessedTransactionType { get; set; }
        public int TransactionCount { get; set; }
        public string BaseAmount { get; set; }
        public string FlatFeeRate { get; set; }
        public string PercentageFeeRate { get; set; }
        public string DisplayRate { get; set; }
        public decimal TransactionAmount { get; set; }
        public string sTransactionAmount { get; set; }
        public decimal TaxRate { get; set; }
    }
}//end namespace
