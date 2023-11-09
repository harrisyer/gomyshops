using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Http;

namespace GoMyShopsv.Models.ViewModels
{
    public class CommissionViewModels
    {
        [StringLength(20)]
        [Display(Name = "Commission Code")]
        public string srcCommissionCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Commission Name")]
        public string srcCommissionName { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class
    public class CommissionDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommissionDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Required]
        [StringLength(20)]
        [AlphaNumeric]
        [Display(Name = "Commission Code")]
        public string CommissionCode { get; set; }

        [Required]
        [StringLength(100)]
        [Descriptions]
        [Display(Name = "Commission Name")]
        public string CommissionName { get; set; }

        [Required]
        [Display(Name = "Commission Type")]
        public string CommissionType { get; set; }

        [Required]
        [Display(Name = "Value Type")]
        public string CommissionValueType { get; set; }

        [StringLength(1)]
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

        public IEnumerable<SelectListItem> CommissionTypeDDL { get; set; }
        public IEnumerable<SelectListItem> CommissionValueTypeDDL { get; set; }

    }//end class
    public class CommissionListViewModels : ActionsListViewModels
    {
        public string CommissionCode { get; set; }
        public string CommissionName { get; set; }
        public string CommissionType { get; set; }
        public string CommissionValueType { get; set; }
        public string Status { get; set; }
    }//end class

    public class CommissionListMainViewModels
    {
        public string CommissionCode { get; set; }

        public int RemoveLineNumber { get; set; }

        public List<CommissionListDetailsViewModels> MainList { get; set; }

        public CommissionListMainViewModels()
        {
            MainList = new List<CommissionListDetailsViewModels>();
        }
    }//end class

    public class CommissionListDetailsViewModels
    {
        public int LineNumber { get; set; }

        public string CommissionValueType { get; set; }

        public string CommissionCode { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = false)]
        public decimal MinValue { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = false)]
        public decimal MaxValue { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = false)]
        public decimal CommissionValue { get; set; }



    }//end class
}//end namespace
