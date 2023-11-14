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
    public class LocationViewModels
    {
        [StringLength(20)]
        [Display(Name = "Branch")]
        public string srcBranchCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Location Code")]
        public string srcLocationCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Location Name")]
        public string srcLocationName { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> BranchDDL { get; set; }
    }//end class

    public class LocationDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocationDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Required]
        [StringLength(20)]
        [AlphaNumeric]
        [Display(Name = "Branch")]
        public string BranchCode { get; set; }

        [Display(Name = "Branch")]
        public string BranchName { get; set; }

        [Required]
        [StringLength(20)]
        [AlphaNumeric]
        //[RegularExpression("[^a-zA-Z0-9]", ErrorMessage = "The Field is A-Z or 0-9.")]
        [Display(Name = "Location Code")]
        public string LocationCode { get; set; }

        [Required]
        [StringLength(100)]
        [Descriptions]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [Display(Name = "Commission Code")]
        public string CommissionCode { get; set; }

        [Display(Name = "Commission Name")]
        public string CommissionName { get; set; }

        [Display(Name = "Is Station")]
        public bool IsStation { get; set; }

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


        public IEnumerable<SelectListItem> BranchDDL { get; set; }
        public IEnumerable<SelectListItem> CommissionDDL { get; set; }
        //public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class LocationListViewModels : ActionsListViewModels
    {
        public string BranchCode { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string IsStation { get; set; }
        public string Status { get; set; }
    }//end class

}//end namespace