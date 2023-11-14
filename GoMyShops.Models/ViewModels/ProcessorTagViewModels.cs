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
    public class ProcessorTagViewModels
    {
        [StringLength(20)]
        [Display(Name = "Processor Code")]
        public string srcProcessorCode { get; set; }

        [StringLength(30)]
        [Display(Name = "MID Tag Name")]
        public string srcProcessorTagName { get; set; }

        [StringLength(30)]
        [Display(Name = "MID Tag Code")]
        public string srcProcessorTagCode { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }
   
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> ProcessorCodeDDL { get; set; }
    }//end class

    public class ProcessorTagDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessorTagDetailsViewModels() : base()
        {     
        }

        public ProcessorTagDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Required]      
        [Display(Name = "Processor")]
        public string ProcessorCode { get; set; }

        [Display(Name = "Processor")]
        public string ProcessorCodeName { get; set; }

        [Display(Name = "MID Tag Code")]
        public string ProcessorTagCode { get; set; }

        [Required]
        [StringLength(30)]
        [AlphaNumericSpace]
        [Display(Name = "MID Tag Name")]
        public string ProcessorTagName { get; set; }
        
        [StringLength(2)]
        [Display(Name = "Active")]
        public string Status { get; set; }

        [Display(Name = "Active")]
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

        public IEnumerable<SelectListItem> ProcessorCodeDDL { get; set; }

    }//end class

    public class ProcessorTagListViewModels : ActionsListViewModels
    {
        public string ProcessorCode { get; set; }


        public string ProcessorCodeName { get; set; }
        public string ProcessorTagCode { get; set; }
        public string ProcessorTagName { get; set; }
        public string Status { get; set; }
    }//end class
}//end namespace

