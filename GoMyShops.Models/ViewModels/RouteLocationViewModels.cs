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
    public class RouteLocationViewModels
    {
        [StringLength(20)]
        [Display(Name = "Route Code")]
        public string srcRouteCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Route Location Code")]
        public string srcRouteLocationCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Route Location Name")]
        public string srcRouteLocationName { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> RouteCodeDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class RouteLocationDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RouteLocationDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Required]
        //[StringLength(20)]
        //[AlphaNumeric]
        [Display(Name = "Route Code")]
        public string RouteCode { get; set; }

        //[Required]
        //[StringLength(100)]
        //[Descriptions]
        [Display(Name = "Route Name")]
        public string RouteName { get; set; }

        [Required]
        [StringLength(20)]
        [AlphaNumeric]
        [Display(Name = "R.Location Code")]
        public string RouteLocationCode { get; set; }

        [Required]
        [StringLength(100)]
        [Descriptions]
        [Display(Name = "R.Location Name")]
        public string RouteLocationName { get; set; }

        [StringLength(500)]
        [Descriptions]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

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

        public IEnumerable<SelectListItem> RouteCodeDDL { get; set; }
    }//end class

    public class RouteLocationListViewModels : ActionsListViewModels
    {        
        public string RouteName { get; set; }
        public string RouteCode { get; set; }
        public string RouteLocationCode { get; set; }
        public string RouteLocationName { get; set; }
        public string Status { get; set; }
    }//end class

    public class RouteLocationDrapListMainViewModels
    {
        public string RouteCode { get; set; }
        public string RouteLocationCode { get; set; }

        [Display(Name = "Route")]
        public string RouteCopyCode { get; set; }

        [Display(Name = "RouteLocation")]
        public string RouteLocationCopyCode { get; set; }
        public IEnumerable<SelectListItem> RouteCopyCodeDDL { get; set; }
        public IEnumerable<SelectListItem> RouteLocationCopyCodeDDL { get; set; }

        public List<RouteLocationDrapListDetailsViewModels> MainList { get; set; }
        public List<RouteLocationDrapListDetailsViewModels> SelectList { get; set; }

        public RouteLocationDrapListMainViewModels()
        {
            MainList = new List<RouteLocationDrapListDetailsViewModels>();
            SelectList = new List<RouteLocationDrapListDetailsViewModels>();
        }
    }//end class

    public class RouteLocationDrapListDetailsViewModels 
    {
        public string BranchCode { get; set; }
        public string LocationCode { get; set; }
        public bool IsStation { get; set; }
        public string itemClass { get; set; }
    }//end class

}//end namespace
