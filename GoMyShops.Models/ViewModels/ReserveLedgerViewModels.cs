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
    public class ReserveLedgerViewModels
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

        public string srcStartDate { get; set; }

        public string srcEndDate { get; set; }

        [Display(Name = "Currency Code")]
        public string srcCurrencyCode { get; set; }

        [Display(Name = "Settlement Date From")]
        public string srcEndDateFrom { get; set; }

        public string srcEndDateTo { get; set; }

        public string srcStartDateFrom { get; set; }

        [Display(Name = "Settlement Date To")]
        public string srcStartDateTo { get; set; }

        [Display(Name = "Payment Date From")]
        public string srcPaymentDateFrom { get; set; }

        [Display(Name = "Payment Date To")]
        public string srcPaymentDateTo { get; set; }

        [Display(Name = "Beneficiary Name")]
        public string srcBeneficiaryName { get; set; }

        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> MIDDDL { get; set; }
        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
        public IEnumerable<SelectListItem> CurrencyCodeDDL { get; set; }


    }//end class

    public class ReserveLedgerListViewModels : ActionsListViewModels
    {
        public int FundingCycle { get; set; }
        public string CustomerCode { get; set; }
        public string MerchantName { get; set; }
        public string ProcessorCode { get; set; }
        public string Processor { get; set; }
        public string MIDCode { get; set; }
        public string MID { get; set; }
        public string sStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string sEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string sPaymentDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Currency { get; set; }
        public decimal AmountBalance { get; set; }
        public string  sAmountBalance { get; set; }
        public decimal FundingAmount { get; set; }
        public string  sFundingAmount { get; set; }
        public decimal NetPayableAmount { get; set; }
        public string sNetPayableAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public string  sPaymentAmount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string BeneficiaryName { get; set; }

        #region Reserve Payment
        public ActionsListDetails PVJson { get; set; }
        public ActionsListDetails SSJson { get; set; }
        #endregion

        #region SOA
        public ActionsListDetails SOAJson { get; set; }
        #endregion
    }//end class

    public class ReserveLedgerDetailsViewModels
    {
        #region Header
        public int Id { get; set; }
        [Display(Name = "Ref. Invoice No")]
        public string   RefInvoiceNo { get; set; }
        [Display(Name = "Funding Cycle")]
        public int      FundingCycle { get; set; }
        [Display(Name = "MID")]
        public string   MIDCode { get; set; }
        [Display(Name = "Merchant")]
        public string   CustomerNameCode { get; set; }
        [Display(Name = "Beneficiary Name")]
        public string   BeneficiaryName { get; set; }
        public string   CustomerCode { get; set; }
        [Display(Name = "Currency")]
        public string   CurrencyCode { get; set; }
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
        [Display(Name = "Handling Fee")]
        public string sWireFee { get; set; }
        public decimal WireFee { get; set; }

        public decimal? MonthlyFee { get; set; }
        public decimal? AnnualFee { get; set; }
        public decimal? SetupFee { get; set; }
        #endregion

        [Display(Name = "Net Payable")]
        public string sNetPayableAmount { get; set; }

        #region Tax Rate
        public bool IsShowTax { get; set; }
        public decimal ReserveLedgerTaxRate { get; set; }
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


        public int ReserveLedgerDetailsID { get; set; }
        public List<ReserveLedgerTransaction> ReserveLedgerTransactionList { get; set; }

    }//end class

    public class ReserveLedgerTransaction
    {
        public DateTime TransactionDate { get; set; }
        public string sTransactionDate { get; set; }
        public List<ReserveLedgerTransactionDetails> ReserveLedgerTransactionDetailsList { get; set; }

        //Duplicated Field ForReport Usage
        public string   FlattenedProcessedTransactionType { get; set; }
        public string   FlattenedDisplayProcessedTransactionType { get; set; }
        public decimal  FlattenedTransactionAmount { get; set; }
        public string   FlattenedsTransactionAmount { get; set; }
        public string   FlattenedBaseAmount { get; set; }
        public string   FlattenedDisplayRate { get; set; }
        public int      FlattenedTransactionCount { get; set; }
    }

    public class ReserveLedgerTransactionDetails
    {
        public DateTime TransactionDate { get; set; }
        public string sTransactionDate { get; set; }
        public string DisplayProcessedTransactionType { get; set; }
        public string BaseAmount { get; set; }
        public string DisplayRate { get; set; }
        public int TransactionCount { get; set; }
        public decimal TransactionAmount { get; set; }
        public string sTransactionAmount { get; set; }
        //public int OrderingGroup { get; set; }
    }
}//end namespace
