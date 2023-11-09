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
    public class ProcessorMCCViewModels : ListViewModels
    {
       // private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessorMCCViewModels() : base()
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        [StringLength(30)]
        [Display(Name = "Processor Code")]
        public string srcProcessorCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Processor Name")]
        public string srcProcessorName { get; set; }

        [Display(Name = "Profile Status")]
        public string srcStatus { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class ProcessorMCCDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessorMCCDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Required]
        [Display(Name = "Processor Code")]
        public string ProcessorCode { get; set; }

        [Display(Name = "Processor Name")]
        public string ProcessorName { get; set; }

        [Display(Name = "Mcc")]
        public List<string> Mcc { get; set; }

        //[StringLength(2)]
        //[Display(Name = "Status")]
        //public string Status { get; set; }

        //[Display(Name = "Status")]
        //public bool CheckBoxStatus { get; set; }

        //[Display(Name = "Created By")]
        //public string CreatedBy { get; set; }

        //public DateTime CreatedTime { get; set; }

        //[Display(Name = "Created Time")]
        //public string sCreatedTime { get; set; }

        //[Display(Name = "Modified By")]
        //public string ModifiedBy { get; set; }

        //public DateTime? ModifiedTime { get; set; }

        //[Display(Name = "Modified Time")]
        //public string sModifiedTime { get; set; }

        // For TreeView
        [Display(Name = "Search")]
        public string SearchTreeViewValue { get; set; }
        public string SearchTreeViewType { get; set; }
        public string ParentNode { get; set; }
        public string MCCTreeViewJson { get; set; }
        //public List<TreeViewSelectItemModel> nodes { get; set; }
        //

        public List<MccList> ProcessorMccList { get; set; }
        public IEnumerable<SelectListItem> ProcessorDDL { get; set; }
       

    }//end class

    public class MccList
    {
        [Display(Name = "Parent Code")]
        public string MccParentValue { get; set; }

        [Display(Name = "Code")]
        public string MccValue { get; set; }

        [Display(Name = "Mcc Name")]
        public string MccName { get; set; }

        public bool MccStatus { get; set; }
    }

    

    public class ProcessorMCCListViewModels : ActionsListViewModels
    {
        public string ProcessorCode { get; set; }

        public string ProcessorName { get; set; }

        public string StatusName { get; set; }

        public string Status { get; set; }


    }//end class
}//end namespace
