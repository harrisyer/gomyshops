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
    public class ProcessingVolumeReportViewModels
    {
        [Display(Name = "Processor")]
        public List<string> srcProcessorCodes { get; set; }

        [Display(Name = "Account Manager")]
        public string srcAccountManagerCode { get; set; }

        [Display(Name = "Settlement Date From")]
        public string srcSettlementDateFrom { get; set; }

        [Display(Name = "Settlement Date To")]
        public string srcSettlementDateTo { get; set; }

        [Display(Name = "Currency")]
        public string srcCurrencyCode { get; set; }

        public string hidReportSortBy { get; set; }
        public string hidReportSortOrder { get; set; }
        
        public IEnumerable<SelectListItem> AccountManagerCodeDDL { get; set; }
        public MultiSelectList ProcessorCodeDDL { get; set; }
        public IEnumerable<SelectListItem> CurrencyCodeDDL { get; set; }

    }//end class

    public class ProcessingVolumeReportListViewModels : ActionsListViewModels
    {
        public int RowNumber { get; set; }
        public string ProcessorCode { get; set; }
        public string ProcessorName { get; set; }
        public string AccountManagerCode { get; set; }
        public string AccountManagerName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal SettledSaleAmount { get; set; }
        public string sSettledSaleAmount { get; set; }
        public decimal SettledSaleAmountMYR { get; set; }
        public string sSettledSaleAmountMYR { get; set; }
        public ActionsListDetails SSJson { get; set; }
    }
}//end namespace
