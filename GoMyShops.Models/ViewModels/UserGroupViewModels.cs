using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Models;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models.ViewModels
{
    public class UserGroupViewModels
    {

        public string? srcGroupCode { get; set; } = string.Empty;
        public string? srcCompanyCode { get; set; } = string.Empty; 
        public string? srcDistributorCode { get; set; } = string.Empty;
        public string? srcBranchCode { get; set; } = string.Empty;
        public string? srcStatus { get; set; } = string.Empty;
        public string? srcGroupType { get; set; } = string.Empty;

        public IEnumerable<SelectListItem>? GroupTypeDDL { get; set; }
        

        public IEnumerable<SelectListItem>? StatusDDL { get; set; }

    }//end class

    public class UserGroupDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public UserGroupDetailsViewModels() : base()
        {
     
        }

        public UserGroupDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsInActive{get; set;}

        [Display(Name = "Group Code")]
        public string? GroupCode { get; set; } = string.Empty;

        [Required]
        [AlphaNumericSpace]
        [StringLength(20)]
        public string? GroupName { get; set; }= string.Empty;

        //[Required]
        [Display(Name = "Company")]
        public string? CompanyCode { get; set; } = string.Empty;

        [Display(Name = "Company")]
        public string? Company { get; set; }=string.Empty;

        [Required]
        [StringLength(255)]
        [Descriptions]
        [Display(Name = "Description")]
        public string? Description { get; set; } = string.Empty;

        //[Required]
        [StringLength(1)]
        [Display(Name = "Group Type")]
        public string? GroupType { get; set; } = string.Empty;

        [Display(Name = "Group Type")]
        public string? GroupTypeName { get; set; } = string.Empty;

        [StringLength(1)]
        [Display(Name = "Status")]
        public string? Status { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }

        //[Required]
        [Display(Name = "Security Type")]
        public int SecurityId { get; set; }

        [Display(Name = "Security Type")]
        public string? SecurityName { get; set; } = string.Empty;

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; } = string.Empty;

        public DateTime? CreatedTime { get; set; } = DateTime.Now;

        [Display(Name = "Created Time")]
        public string? sCreatedTime { get; set; }

        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "Modified Time")]
        public string? sModifiedTime { get; set; }

        public List<SelectListItem>? CompanyDDL { get; set; }

        public List<AppCtrDetailViewModels>? AppCtrDetailList { get; set; }
        public IEnumerable<SelectListItem>? GroupTypeDDL { get; set; }
        public IEnumerable<SelectListItem>? SecurityNameDDL { get; set; }
    }//end class

    public class UserGroupListViewModels : ActionsListViewModels
    {
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public string? GroupType { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedTimestamp { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime? ModifiedTimestamp { get; set; }
        public string? Status { get; set; }
        public int SecurityId { get; set; }
    }//end class


}//end namespace
