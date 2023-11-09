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
    public class ProcessorCardBinRuleViewModels
    {
        //[StringLength(30)]
        //[Display(Name = "User ID")]
        //public string srcUserID { get; set; }

        [StringLength(30)]
        [Display(Name = "Processor Code")]
        public string srcProcessorCode { get; set; }

        [StringLength(250)]
        [Display(Name = "Processor Name")]
        public string srcProcessorName { get; set; }

        [StringLength(6)]
        [Display(Name = "First Six Digit")]
        public string srcFirstSix { get; set; }

        [Display(Name = "Last Four Digit")]
        [StringLength(4)]
        public string srcLastFour { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class ProcessorCardBinRuleDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessorCardBinRuleDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Display(Name = "Card Bin Code")]
        public string CardBinCode { get; set; }

        [Display(Name = "Processor")]
        public string ProcessorCode { get; set; }

        [Display(Name = "Processor")]
        public string ProcessorName { get; set; }

        [Required]
        [Display(Name = "First Six Digit")]
        [StringLength(6, MinimumLength = 6)]
        [PositiveInteger]
        public string FirstSix { get; set; }

        [Display(Name = "Last Four Digit")]
        [StringLength(4)]
        [PositiveInteger]
        public string LastFour { get; set; }

        [Required]
        [Display(Name = "Card Bin Type")]
        public string CardBinType { get; set; }

        [Display(Name = "Card Bin Type")]
        public string CardBinName { get; set; }

        [StringLength(2)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [Display(Name = "Created Time")]
        public string sCreatedTime { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "Modified Time")]
        public string sModifiedTime { get; set; }

        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
        public IEnumerable<SelectListItem> CardBinTypeDDL { get; set; }

    }//end class

    public class ProcessorCardBinRuleListViewModels : ActionsListViewModels
    {
        public string CardBinCode { get; set; }
        public string ProcessorCode { get; set; }
        public string ProcessorName { get; set; }      
        public string FirstSix { get; set; }
        public string LastFour { get; set; }
        public string CardBinType { get; set; }
        public string Status { get; set; }
    }//end class
}//end namespace
