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
    public class CSettlementSummaryReportViewModels
    {
        [Display(Name = "Processor")]
        public string srcProcessorCode { get; set; }
        
        [Display(Name = "Currency")]
        public string srcCurrencyCode { get; set; }

        [Display(Name = "Settlement Date From")]
        public string srcSettlementDateFrom { get; set; }

        [Display(Name = "Settlement Date To")]
        public string srcSettlementDateTo { get; set; }

        public string hidReportSortBy { get; set; }

        public string hidReportSortOrder { get; set; }

        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
        public IEnumerable<SelectListItem> CurrencyCodeDDL { get; set; }

        public string FilterBySubtitle { get; set; }
        public string FilterDateSubtitle { get; set; }

        public List<CSettlementSummaryReportListViewModels> SummaryList { get; set; }
        public List<CSettlementSummaryReportListViewModels> TotalList { get; set; }
    }//end class
    
    public class CSettlementSummaryReportListViewModels : ActionsListViewModels
    {
        public int RowNumber { get; set; }
        public string ProcessorCode { get; set; }
        public string ProcessorName { get; set; }
        public string CurrencyCode { get; set; }

        public decimal? TotalAuthorizedSaleAmount { get; set; }
        public decimal? TotalSettledSaleAmount { get; set; }
        public decimal? TotalDeclinedSaleAmount { get; set; }
        public decimal? TotalMDRAmount { get; set; }
        public decimal? TotalChargebackAmount { get; set; }
        public decimal? TotalRefund { get; set; }
        public decimal? TotalTransactionFee { get; set; }
        public decimal? TotalReserve { get; set; }

        public decimal? TotalRetrievalAmount { get; set; }
        public decimal? TotalChargebackFromRetrievalAmount { get; set; }
        public decimal? TotalRetrievalRepresentmentAmount { get; set; }
        public decimal? TotalChargebackRepresentmentAmount { get; set; }
        public decimal? TotalNetPayable { get; set; }

        public string sTotalAuthorizedSaleAmount { get; set; }
        public string sTotalSettledSaleAmount { get; set; }
        public string sTotalDeclinedSaleAmount { get; set; }
        public string sTotalMDRAmount { get; set; }
        public string sTotalChargebackAmount { get; set; }
        public string sTotalRefund { get; set; }
        public string sTotalTransactionFee { get; set; }
        public string sTotalReserve { get; set; }
        public string sTotalRetrievalAmount { get; set; }
        public string sTotalChargebackFromRetrievalAmount { get; set; }
        public string sTotalRetrievalRepresentmentAmount { get; set; }
        public string sTotalChargebackRepresentmentAmount { get; set; }
        public string sTotalNetPayable { get; set; }

        public decimal? TotalSettledSaleCount { get; set; }
        public decimal? TotalDeclinedSaleCount { get; set; }
        public decimal DeclinedSettledCountRatio { get; set; }
        public string sDeclinedSettledCountRatio { get; set; }
        public decimal DeclinedSettledAmountRatio { get; set; }
        public string sDeclinedSettledAmountRatio { get; set; }
    }//end class

    public class CSettlementSummaryReportDetailViewModels
    {
        public List<CSettlementSummaryReportAmtListViewModels> AmountList = new List<CSettlementSummaryReportAmtListViewModels>();
        public List<CSettlementSummaryReportAmtListViewModels> FeeList = new List<CSettlementSummaryReportAmtListViewModels>();
        public List<CSettlementSummaryReportAmtListViewModels> ReserveList = new List<CSettlementSummaryReportAmtListViewModels>();
        public List<CSettlementSummaryReportAmtListViewModels> MDRList = new List<CSettlementSummaryReportAmtListViewModels>();

        [Display(Name = "Net Payable")]
        public string NetPayable { get; set; }
        public bool IsNoAmountCardTypes { get; set; }
        public bool IsNoCountCardTypes { get; set; }

        public string srcSettlementDateFrom { get; set; }
        public string srcSettlementDateTo { get; set; }
        public string srcCurrencyCode { get; set; }
        public string srcProcessorCode { get; set; }

        public string ProcessorName { get; set; }
        public string sSettlementDateFrom { get; set; }
        public string sSettlementDateTo { get; set; }

        #region Net Payable Calculation
        public decimal TotalSettledSalesAmount { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public decimal TotalRetrievalAmount { get; set; }
        public decimal TotalChargebackAmount { get; set; }
        public decimal TotalRetrievalRepresentmentAmount { get; set; }
        public decimal TotalChargebackRepresentmentAmount { get; set; }
        public decimal TotalTransactionFeesAmount { get; set; }
        public decimal TotalReserveAmount { get; set; }

        [Display(Name = "Total Sales Amount")]
        public string sTotalSettledSalesAmount { get; set; }
        [Display(Name = "Refund Amount")]
        public string sTotalRefundAmount { get; set; }
        [Display(Name = "Retrieval Amount")]
        public string sTotalRetrievalAmount { get; set; }
        [Display(Name = "Chargeback Amount")]
        public string sTotalChargebackAmount { get; set; }
        [Display(Name = "Retrieval Representment Amount")]
        public string sTotalRetrievalRepresentmentAmount { get; set; }
        [Display(Name = "Chargeback Representment Amount")]
        public string sTotalChargebackRepresentmentAmount { get; set; }
        [Display(Name = "Total Transaction Fees Amount")]
        public string sTotalTransactionFeesAmount { get; set; }
        [Display(Name = "Reserve Amount")]
        public string sTotalReserveAmount { get; set; }
        #endregion
    }

    public class CSettlementSummaryReportAmtListViewModels : ActionsListViewModels
    {
        public string TransactionDesc { get; set; }
        public string TranslatedTransactionDesc { get; set; }
        public int TransactionStatus { get; set; }
        public decimal BaseAmount { get; set; }
        public string sTotalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TransactionFeeAmount { get; set; }
        public string sTransactionFeeAmount { get; set; }
        public long TotalTransactionCount { get; set; }
        public decimal TotalMDRAmount { get; set; }

        //public decimal PercentageFeeRate { get; set; }
        //public string sPercentageFeeRate { get; set; }
        //public decimal FlatFeeRate { get; set; }
        //public string sFlatFeeRate { get; set; }

        public List<CardTypeAmount> CardTypeAmountList { get; set; }

        //For Report Usage
        public string CardType { get; set; }
        public decimal Amount { get; set; }
        public string sAmount { get; set; }
        public long TransactionCount { get; set; }

    }//end class
}//end namespace
