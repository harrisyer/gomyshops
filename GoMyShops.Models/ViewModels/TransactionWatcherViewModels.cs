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
    public class TransactionWatcherViewModels
    {
        #region Filtering
        [Display(Name = "Merchant Name")]
        public List<string> srcMerchantCodes { get; set; }
        
        [Display(Name = "Response Code")]
        public List<string> srcResponseCodes { get; set; }

        [Display(Name = "Transaction Type")]
        public List<string> srcTransactionTypes { get; set; }

        [Display(Name = "Currency")]
        public List<string> srcCurrencyCodes { get; set; }
        #endregion

        #region Highlight Criteria
        [Display(Name = "Card Frequency")]
        [PositiveInteger]
        public string srcCardFrequencyCount { get; set; }

        [Display(Name = "Transaction Amount")]
        public string srcTransactionAmount { get; set; }
        #endregion

        #region Other Options
        [Display(Name = "Watcher Refresh Frequency")]
        [PositiveInteger]
        public string srcWatcherRefreshSecond { get; set; }

        [Display(Name = "Watcher No. Of Records Shown")]
        public string srcWatcherRecordCount { get; set; }

        [Display(Name = "Watcher Records Time From")]
        public string srcWatcherRecordTimeFromMinutes { get; set; }
        #endregion

        #region Dropdown Lists
        public MultiSelectList MerchantCodeDDL { get; set; }
        public MultiSelectList ResponseCodeDDL { get; set; }
        public MultiSelectList TransactionTypeDDL { get; set; }
        public MultiSelectList CurrencyCodeDDL { get; set; }
        public List<SelectListItem> WatcherRefreshSecondDDL { get; set; }
        public List<SelectListItem> WatcherRecordCountDDL { get; set; }
        public List<SelectListItem> WatcherRecordTimeFromMinutesDDL { get; set; }
        public List<SelectListItem> CardFrequencyCountDDL { get; set; }
        #endregion

        #region Data & DataLists
        public int TotalTransactionCount { get; set; }
        public int TotalSuccessTransactionCount { get; set; }
        public int TotalDeclinedTransactionCount { get; set; }
        public int TotalPendingTransactionCount { get; set; }
        public int TotalCanceledTransactionCount { get; set; }
        public string sLatestTransactionDate { get; set; }


        public List<TransactionWatcherListViewModels> TransactionList { get; set; }
        public List<TransactionWatcherListViewModels> CriticalAmountExceedTransactionList { get; set; }
        public List<CardGroupViewModels> CriticalCardFrequencyTransactionList { get; set; }

        public DateTime StartTime { get; set; }
        #endregion
    }//end class

    public class TransactionWatcherListViewModels : ActionsListViewModels
    {
        public DateTime TransactionTime { get; set; }
        public string sTransactionTime { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCode { get; set; }
        public string TIDName { get; set; }
        public string CardNumber { get; set; }
        public int TransactionType { get; set; }
        public string TransactionTypeDesc { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public string sAmount { get; set; }
        public string ResponseCode { get; set; }
        public int TransactionStatus { get; set; }
        public string TransactionStatusDesc { get; set; }
        public string CriticalReason { get; set; }
        public string OrderId { get; set; }
    }//end class

    public class CardGroupViewModels
    {
        public string CardNumber { get; set; }
        public int Frequency { get; set; }
        public List<TransactionWatcherListViewModels> TransactionList { get; set; }
    }
}//end namespace
