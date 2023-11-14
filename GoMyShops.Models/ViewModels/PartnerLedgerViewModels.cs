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
    public class PartnerLedgerViewModels
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

    public class PartnerLedgerListViewModels : ActionsListViewModels
    {
        public int FundingCycle { get; set; }
        public string PartnerCode { get; set; }
        public string PartnerName { get; set; }
        
        public string sStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string sEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string sPaymentDate { get; set; }
        public DateTime? PaymentDate { get; set; }
       
        public decimal AmountBalance { get; set; }
        public string sAmountBalance { get; set; }
        public decimal FundingAmount { get; set; }
        public string sFundingAmount { get; set; }
        public decimal NetPayableAmount { get; set; }
        public string sNetPayableAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public string sPaymentAmount { get; set; }
        //public string Note { get; set; }
        public string Status { get; set; }
        //public string BeneficiaryName { get; set; }

        #region Partner Payment
        public ActionsListDetails PVJson { get; set; }
        public ActionsListDetails SSJson { get; set; }
        #endregion
    }//end class

    public class PartnerLedgerDetailsViewModels
    {
        #region Header
        public int Id { get; set; }
        [Display(Name = "Ref. Invoice No")]
        public string   RefInvoiceNo { get; set; }
        [Display(Name = "Funding Cycle")]
        public int      FundingCycle { get; set; }
        //[Display(Name = "MIDs")]
        //public string   MIDCodes { get; set; }
        public string PartnerCode { get; set; }
        public string PartnerName { get; set; }
        [Display(Name = "Partner")]
        public string   PartnerNameCode { get; set; }
        [Display(Name = "Beneficiary Name")]
        public string   BeneficiaryName { get; set; }
        [Display(Name = "Settlement Period")]
        public string   sSettlementPeriod { get; set; }
        public DateTime SettlementPeriodTo { get; set; }
        public DateTime SettlementPeriodFrom { get; set; }
        //public string   sSettlementPeriodTo { get; set; }
        public string   sSettlementPeriodFrom { get; set; }
        public DateTime PaymentDueDate { get; set; }
        [Display(Name = "Payment Due Date")]
        public string   sPaymentDueDate { get; set; }
        public DateTime GeneratedTime { get; set; }
        [Display(Name = "Generated Time")]
        public string   sGeneratedTime { get; set; }
        public string   VoucherStatus { get; set; }
        [Display(Name = "Voucher Status")]
        public string   sVoucherStatus { get; set; }
        [Display(Name = "Holdback")]
        public int      HoldbackDays { get; set; }
        [Display(Name = "Payment Frequency")]
        public int      PaymentFrequencyInDays { get; set; }
        #endregion

        #region Total Amount
        [Display(Name = "Balance (b/f)")]
        public string sTotalBalance { get; set; }
        //[Display(Name = "Settled Sales Amount")]
        //public string sTotalSettledSalesAmount { get; set; }
        //[Display(Name = "Reserve")]
        //public string sReserve { get; set; }
        //[Display(Name = "Refund")]
        //public string sTotalRefund { get; set; }
        //[Display(Name = "Retrieval")]
        //public string sTotalRetrieval { get; set; }
        //[Display(Name = "Chargeback")]
        //public string sTotalChargeback { get; set; }
        //[Display(Name = "Chargeback From Retrieval")]
        //public string sTotalChargebackFromRetrieval { get; set; }
        //public decimal TotalChargebackFromRetrieval { get; set; }
        //[Display(Name = "Retrieval Representment")]
        //public string sTotalRetrievalRepresentment { get; set; }
        //public decimal TotalRetrievalRepresentment { get; set; }
        //[Display(Name = "Chargeback Representment")]
        //public string sTotalChargebackRepresentment { get; set; }
        //public decimal TotalChargebackRepresentment { get; set; }
        //[Display(Name = "Excessive Chargeback")]
        //public string sTotalExcessiveChargeback { get; set; }
        //public decimal TotalExcessiveChargeback { get; set; }
        //[Display(Name = "Reserve")]
        //public string sTotalReserve { get; set; }
        //public string sTotalAdjustmentReserve { get; set; }
        //[Display(Name = "Miscellaneous Charges")]
        //public string sMiscCharges { get; set; }
        //[Display(Name = "Miscellaneous Rebate")]
        //public string sMiscRebate { get; set; }
        public decimal WireFee { get; set; }
        [Display(Name = "Handling Fee")]
        public string sWireFee { get; set; }
        //[Display(Name = "Setup Fee")]
        //public string sSetupFee { get; set; }
        //[Display(Name = "Monthly Fee")]
        //public string sMonthlyFee { get; set; }
        //[Display(Name = "Annual Fee")]
        //public string sAnnualFee { get; set; }

        //public decimal? MonthlyFee { get; set; }
        //public decimal? AnnualFee { get; set; }
        //public decimal? SetupFee { get; set; }

        //[Display(Name = "Total Adjustment")]
        //public string sTotalAdjustmentAmount { get; set; }
        #endregion

        #region Total Fee
        public int TotalAuthorizedFeeCount { get; set; }
        public int TotalDeclinedFeeCount { get; set; }
        public int TotalSettlementFeeCount { get; set; }
        public int TotalVoidFeeCount { get; set; }
        public int TotalRefundFeeCount { get; set; }
        public int TotalRetrievalFeeCount { get; set; }
        public int TotalChargebackFeeCount { get; set; }
        public int TotalExcessiveRetrievalFeeCount { get; set; }
        public int TotalExcessiveChargebackFeeCount { get; set; }
        public int TotalMDRCount { get; set; }

        [Display(Name = "Authorized")]
        public string sTotalAuthorizedFeeAmount { get; set; }
        [Display(Name = "Declined")]
        public string sTotalDeclinedFeeAmount { get; set; }
        [Display(Name = "Settlement")]
        public string sTotalSettlementFeeAmount { get; set; }
        [Display(Name = "Void")]
        public string sTotalVoidFeeAmount { get; set; }
        [Display(Name = "Refund")]
        public string sTotalRefundFeeAmount { get; set; }
        [Display(Name = "Retrieval")]
        public string sTotalRetrievalFeeAmount { get; set; }
        [Display(Name = "Chargeback")]
        public string sTotalChargebackFeeAmount { get; set; }
        [Display(Name = "Excessive Retrieval")]
        public string sTotalExcessiveRetrievalFeeAmount { get; set; }
        [Display(Name = "Excessive Chargeback")]
        public string sTotalExcessiveChargebackFeeAmount { get; set; }
        public decimal TotalTransactionFeeAmount { get; set; }
        [Display(Name = "Total Transaction Fee")]
        public string sTotalTransactionFeeAmount { get; set; }
        [Display(Name = "MDR")]
        public string sTotalMDR { get; set; }
        #endregion

        [Display(Name = "Net Payable")]
        public string sNetPayableAmount { get; set; }

        #region Tax Rate
        public bool IsShowTax { get; set; }
        //public decimal PartnerLedgerTaxRate { get; set; }
        public List<Tax> TaxList { get; set; }
        #endregion

        #region Bank Information
        [Display(Name = "Beneficiary Bank")]
        public string BankName { get; set; }
        [Display(Name = "Beneficiary Bank Address")]
        public string BankAddress { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNo { get; set; }
        [Display(Name = "Swift Number ID")]
        public string SwiftNo { get; set; }
        #endregion
        
        public int PartnerLedgerDetailsID { get; set; }
        public List<PartnerLedgerMID> PartnerLedgerMIDList { get; set; }
        public List<PartnerLedgerTransaction> PartnerLedgerOwnTransactionList { get; set; }

        public string sOwnNetPayableAmount { get; set; }

        //Report Usage
        public List<FlattenedPartnerLedgerTransaction> FlattenedPartnerLedgerOwnTransactionList { get; set; }
        public List<FlattenedPartnerLedgerTransaction> FlattenedPartnerLedgerTransactionList { get; set; }
    }//end class

    public class PartnerLedgerMID
    {
        public string MIDCode { get; set; }
        public List<PartnerLedgerTransaction> PartnerLedgerTransactionList { get; set; }
        public string sNetPayableAmount { get; set; }
    }

    public class PartnerLedgerTransaction
    {
        public DateTime TransactionDate { get; set; }
        public string   sTransactionDate { get; set; }
        public List<PartnerLedgerTransactionDetails> PartnerLedgerTransactionDetailsList { get; set; }
        
    }

    public class PartnerLedgerTransactionDetails
    {       
        public string   MIDCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string   sTransactionDate { get; set; }
        public string   ProcessedTransactionType { get; set; }
        public string   DisplayProcessedTransactionType { get; set; }
        public int      TransactionCount { get; set; }
        public string   BaseAmount { get; set; }
        public string   FlatFeeRate { get; set; }
        public string   PercentageFeeRate { get; set; }
        public string   DisplayRate { get; set; }
        public decimal  TransactionAmount { get; set; }
        public string   sTransactionAmount { get; set; }
        public int      FundingLedgerTransactionGroup { get; set; }
        public string   AdditionalDescription { get; set; }
        public decimal  TaxRate { get; set; }
    }

    public class FlattenedPartnerLedgerTransaction
    {
        public DateTime FlattenedTransactionDate { get; set; }
        public string FlattenedsTransactionDate { get; set; }
        public string FlattenedMIDCode { get; set; }
        public string FlattenedProcessedTransactionType { get; set; }
        public string FlattenedDisplayProcessedTransactionType { get; set; }
        public string FlattenedBaseAmount { get; set; }
        public string FlattenedDisplayRate { get; set; }
        public int FlattenedTransactionCount { get; set; }
        public string FlattenedFlatFeeRate { get; set; }
        public string FlattenedPercentageFeeRate { get; set; }
        public decimal FlattenedTransactionAmount { get; set; }
        public string FlattenedsTransactionAmount { get; set; }
        public int FlattenedOrderingGroup { get; set; }
        public string FlattenedAdditionalDescription { get; set; }
        public int FlattenedFundingLedgerTransactionGroup { get; set; }
    }
}//end namespace
