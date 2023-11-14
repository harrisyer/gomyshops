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
    public class TransactionLogViewModels
    {
        [Display(Name = "Transaction Date")]
        public string srcTransactionDate { get; set; }

        [Display(Name = "Merchant TID")]
        public string srcTIDCode { get; set; }

        [Display(Name = "Records Per Page")]
        public int srcRecordsPerPage { get; set; }

        public IEnumerable<SelectListItem> RecordsPerPageDDL { get; set; }

        #region Hidden Inputs To Save Filters Used For Searching
        public string hidsrcTransactionDate { get; set; }

        public string hidsrcTIDCode { get; set; }

        public string hidssrcRecordsPerPage { get; set; }

        public string hidReportSortBy { get; set; }

        public string hidReportSortOrder { get; set; }
        #endregion

    }//end class

    public class TransactionLogListViewModels : ActionsListViewModels
    {
        public long     TransactionLogID { get; set; }
        public string   sTransactionDateTime { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string   IPAddress { get; set; }
        public string   TIDCode { get; set; }
        public string   InvoiceNo { get; set; }
        public string   TransactionType { get; set; }
        public string   TransactionOriginURL  { get; set; }
    }//end class

    public class TransactionLogDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionLogDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long TransactionLogID { get; set; }

        [Display(Name = "Transaction Date Time")]
        public string sTransactionDateTime { get; set; }

        public DateTime TransactionDateTime { get; set; }
        
        [Display(Name = "HTTP Remote Address")]
        public string IPAddress { get; set; }
        
        [Display(Name = "Customer Payment Page")]
        public string TIDName { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNo { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }
        
        [Display(Name = "HTTP Referer")]
        public string TransactionOriginURL { get; set; }

        public string sRequestRaw { get; set; }
        //public PaymentEntryInitRequestModels RequestRaw { get; set; }
    }//end class
}//end namespace

