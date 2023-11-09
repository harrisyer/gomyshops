using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Models;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models.ViewModels
{
    public class BranchViewModels
    {
        public string srcDistributorCode { get; set; }
        public string srcBranchCode { get; set; }
        public string srcBranchName { get; set; }
        public string srcStatus { get; set; }
        public string srcType { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> CustomerTypeDDL { get; set; }
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> StateDDL { get; set; }
        public IEnumerable<SelectListItem> DistributorDDL { get; set; }
        //public IEnumerable<SelectListItem> StatusDDL
        //{
        //    get
        //    {
        //        List<SelectListItem> statuslist = new List<SelectListItem>();
        //        statuslist.Add(new SelectListItem() { Text = "Active", Value = "1" });
        //        statuslist.Add(new SelectListItem() { Text = "Deactivated", Value = "0" });
        //        //statuslist.Add(new SelectListItem() { Text = "Pending", Value = "3" });
        //        return statuslist;
        //    }
        //}

        //public IEnumerable<SelectListItem> CustomerTypeDDL
        //{
        //    get
        //    {
        //        List<SelectListItem> typelist = new List<SelectListItem>();
        //        typelist.Add(new SelectListItem() { Text = "Normal", Value = "1" });
        //        typelist.Add(new SelectListItem() { Text = "VIP", Value = "0" });
        //        return typelist;
        //    }
        //}

    }//end class

    public class BranchDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BranchDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Company")]
        public string CompCode { get; set; }


        [Required]
        [Display(Name = "Distributor Code")]
        public string DistCode { get; set; }

        [Display(Name = "Distributor Name")]
        public string DistName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Display(Name = "GST Reg ID")]
        public string GSTRegID { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Address 1")]
        public string Add1 { get; set; }

        [StringLength(255)]
        [Display(Name = "Address 2")]
        public string Add2 { get; set; }

        [StringLength(255)]
        [Display(Name = "Address 3")]
        public string Add3 { get; set; }

        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "State")]
        public string StateName { get; set; }

        [Display(Name = "State")]
        public string StateCode { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(15)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Office No")]
        public string PhoneNo { get; set; }

        [StringLength(20)]
        [Display(Name = "Fax No")]
        public string FaxNo { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        //[StringLength(20)]
        //[Display(Name = "RefCode")]
        //public string RefCode { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }

        public IEnumerable<SelectListItem> CompanyDDL { get; set; }
        public IEnumerable<SelectListItem> DistributorDDL { get; set; }
        public IEnumerable<SelectListItem> StateDDL { get; set; }
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }

    }

    public class BranchListViewModels : ActionsListViewModels
    {
        public int CompanyCode { get; set; }
        public string DistributorCode { get; set; }
        public string BranchCode { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }//end class

}//end namespace