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
    public class InactiveMerchantReportViewModels
    {
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }

        [Display(Name = "MID Code")]
        public string srcMIDCode { get; set; }

        [Display(Name = "No Transaction From")]
        public string srcNoTxnFrom { get; set; }
        [Display(Name = "No Transaction To")]
        public string srcNoTxnTo { get; set; }

        [Display(Name = "Minimum Transaction Volume")]
        public decimal srcMinimumTransactionVolume { get; set; }
        [Display(Name = "Minimum Transaction Volume From")]
        public string srcMinimumTransactionVolumeFrom { get; set; }
        [Display(Name = "Minimum Transaction Volume To")]
        public string srcMinimumTransactionVolumeTo { get; set; }

        [Display(Name = "Not Live After Creation From")]
        public string srcNotLiveAfterCreationFrom { get; set; }
        [Display(Name = "Not Live After Creation To")]
        public string srcNotLiveAfterCreationTo { get; set; }
        
        [Display(Name = "Report Mode")]
        public string srcReportMode { get; set; }

        [Display(Name = "Check For Live But No Transactions")]
        public bool srcCheckNoTxn { get; set; }
        [Display(Name = "Check For Minimum Transaction Volume After Live")]
        public bool srcCheckMinimumTransactionVolume { get; set; }
        [Display(Name = "Check For Not Live After Creation")]
        public bool srcCheckNotLiveAfterCreation { get; set; }
        
        public string Title { get; set; }
        public string hidReportSortBy { get; set; }
        public string hidReportSortOrder { get; set; }

        public IEnumerable<SelectListItem> MerchantDDL { get; set; }
        public IEnumerable<SelectListItem> MIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> ReportModeDDL { get; set; }
    }//end class

    public class InactiveMerchantReportListViewModels : ActionsListViewModels
    {
        public string MerchantCode { get; set; }
        public string MerchantName { get; set; }
        public string MIDCode { get; set; }
        public string MCCName { get; set; }
        public DateTime AccountActivationDate { get; set; }
        public string sAccountActivationDate { get; set; }
        public DateTime? LastTransactionDate { get; set; } 
        public string sLastTransactionDate { get; set; }
        public string Reason { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public string sApprovedDate { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public string sProcessStartDate { get; set; }
    }

    public class InactiveMID
    {
        public string MIDCode { get; set; }
        public string Reason { get; set; }
    }

    public class InactiveMerchant
    {
        public string MerchantCode { get; set; }
        public string Reason { get; set; }
    }
}//end namespace
