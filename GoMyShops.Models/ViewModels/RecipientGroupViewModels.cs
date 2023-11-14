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
    public class RecipientGroupViewModels
    {
        [StringLength(100)]
        [Display(Name = "CompanyCode")]
        public string srcCompanyCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Recipient Group Code")]
        public string srcRecipientGroupCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Recipient Group Name")]
        public string srcRecipientGroupName { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> CompanyDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class RecipientGroupDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipientGroupDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Required]
        [StringLength(20)]
        [AlphaNumeric]
        [Display(Name = "Recipient Group Code")]
        public string RecipientGroupCode { get; set; }

        [Required]
        [StringLength(100)]
        [Descriptions]
        [Display(Name = "Recipient Group Name")]
        public string RecipientGroupName { get; set; }

        [Required]
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
 
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

        public IEnumerable<SelectListItem> CompanyCodeDDL { get; set; }
  
    }//end class
    public class RecipientGroupListViewModels : ActionsListViewModels
    {
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string RecipientGroupCode { get; set; }
        public string RecipientGroupName { get; set; }
        public string Status { get; set; }
    }//end class

}//end namespace
