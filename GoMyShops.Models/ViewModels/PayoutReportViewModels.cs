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
    public class PayoutReportViewModels
    {
        [Display(Name = "Beneficiary Name")]
        public string srcBeneficiaryName { get; set; }
        
        [Display(Name = "Receiving Bank")]
        public string srcBankCode { get; set; }

        [Display(Name = "Payment Date From")]
        public string srcPaymentDateFrom { get; set; }

        [Display(Name = "Payment Date To")]
        public string srcPaymentDateTo { get; set; }

        [Display(Name = "Merchant")]
        public string srcMerchantCode { get; set; }

        public string hidReportSortBy { get; set; }

        public string hidReportSortOrder { get; set; }

        public IEnumerable<SelectListItem> BeneficiaryNameDDL { get; set; }
        public IEnumerable<SelectListItem> BankDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantCodeDDL { get; set; }
    }//end class
    
    public class PayoutReportListViewModels : ActionsListViewModels
    {
        public string BeneficiaryName { get; set; }
        public string MerchantCode { get; set; }
        public string MerchantName { get; set; }
        public string DebitAccountNo { get; set; }
        public string BankCode { get; set; }
        public string AccountNo { get; set; }
        public decimal Amount { get; set; }
        public string sAmount { get; set; }
        public string BusinessRegistrationNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public string sPaymentDate { get; set; }
        public string TransactionRefNo { get; set; }
        public ActionsListDetails FLRLJson { get; set; }
    }//end class
}//end namespace
