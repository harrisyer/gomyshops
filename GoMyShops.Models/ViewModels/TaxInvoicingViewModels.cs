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
    public class TaxInvoiceViewModels
    {
        [Display(Name = "Merchant Name")]
        public string srcCustomerCode { get; set; }

        [Display(Name = "MID")]
        public string srcMIDCode { get; set; }

        [Display(Name = "Invoice Number")]
        public string srcInvoiceNo { get; set; }

        [Display(Name = "Year")]
        public string srcGenerateForYear { get; set; }

        [Display(Name = "Month")]
        public string srcGenerateForMonth { get; set; }

        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> MIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> GenerateForYearDDL { get; set; }
        public IEnumerable<SelectListItem> GenerateForMonthDDL { get; set; }
    }//end class

    public class TaxInvoiceListViewModels : ActionsListViewModels
    {
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string MIDCode { get; set; }
        public string InvoiceNo { get; set; }
        public int GenerateForYear { get; set; }
        public int GenerateForMonth { get; set; }
        public string sGenerateForMonth { get; set; }

        public decimal TaxAmount { get; set; }
        public decimal Amount { get; set; }

        public string sTaxAmount { get; set; }
        public string sAmount { get; set; }

        public string PaymentStatus { get; set; }

        public ActionsListDetails FLJson { get; set; }
        public ActionsListDetails SOAJson { get; set; }
    }

    public class TaxInvoiceDetailViewModels 
    {
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string MIDCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZIPCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MobileNo { get; set; }
        public string OfficeNo { get; set; }
        public string Fax { get; set; }
        
        public string InvoiceNo { get; set; }
        public string SettlementPeriod { get; set; }
        
        public List<TaxInvoiceItem> TaxInvoiceItemList { get; set; }
        public List<TaxSummary> TaxSummaryList { get; set; }

        public decimal Amount { get; set; }
        public decimal TotalExcludingTax { get; set; }
        public decimal TaxAmount { get; set; }

        public string sAmount { get; set; }
        public string sTotalExcludingTax { get; set; }
        public string sTaxAmount { get; set; }

        public string AmountInEnglishWords { get; set; }
    }

    public class TaxInvoiceItem
    {
        public long RowNo { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public string SOANo { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmountExclTax { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmountInclTax { get; set; }

        public string sAmount { get; set; }
        public string sTotalAmountExclTax { get; set; }
        public string sTaxRate { get; set; }
        public string sTaxAmount { get; set; }
        public string sTotalAmountInclTax { get; set; }
    }

    public class TaxSummary
    {
        public string Desc { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
    }
}//end namespace
