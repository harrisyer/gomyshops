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
    public class UserViewModels
    {
        public string srcUserID { get; set; }
        public string srcUserName { get; set; }
        public string srcStatus { get; set; }
        public string srcUserGroup { get; set; }
        public string srcUserType { get; set; }

        public IEnumerable<SelectListItem> UserGroupDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class UserDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsInActive { get; set; }

        //[Required]
        [Display(Name = "Company Code")]
        public string CompCode { get; set; }

        //[Required]
        [StringLength(10)]
        [Display(Name = "Distributor")]
        public string DistCode { get; set; }

        [Display(Name = "Customer")]
        public string CustCode { get; set; }

        [Display(Name = "Customer")]
        public string CustName { get; set; }

        // [Required]
        [StringLength(200)]
        [Display(Name = "Branch")]
        public string Branch { get; set; }

        public string BranchCode { get; set; }

        //[Required]
        //[Display(Name = "Location")]
        //public string LocationCode { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        //public IEnumerable<SelectListItem> LocationDDL { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "User ID")]
        [AlphaNumeric]       
        public string UserName { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        [Display(Name = "Password")]
        [Descriptions]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        [AlphaSpace]
        public string FirstName { get; set; }

        //[Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        [AlphaSpace]
        public string LastName { get; set; }

        //for Mobile
        //[Required]
        //[StringLength(20)]
        //[Display(Name = "Mobile Imei")]
        //public string ImeiNo { get; set; }


        [Required]
        //[StringLength(1)]
        [Display(Name = "User Type")]
        public string Type { get; set; }

        [Display(Name = "User Type")]
        public string TypeName { get; set; }

        [Required]
        [Display(Name = "User Group")]
        public string GroupCode { get; set; }

        [Display(Name = "User Group Type")]
        public string GroupType { get; set; }

        [Display(Name = "User Group Type")]
        public string GroupTypeName { get; set; }

        public bool GroupCodeAccess { get; set; }

        public bool GroupCodeSubAccess { get; set; }

        public bool GroupCodeMainUserAccess { get; set; }

        [Display(Name = "User Group")]
        public string GroupName { get; set; }

        //[Required]
        [Display(Name = "Company")]
        public string[] CompanyCodeArrays { get; set; }

        [StringLength(255)]
        [Display(Name = "User Group")]
        public string UserGroup { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string Add1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address 2")]
        public string Add2 { get; set; }

        //[StringLength(255)]
        //[Display(Name = "Address 3")]
        //public string Add3 { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Country")]
        public string CountryCode { get; set; }


        [StringLength(5)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(20)]
        [Display(Name = "Mobile No")]
        [PositiveInteger]
        public string MobileNo { get; set; }

        [StringLength(20)]
        [Display(Name = "Office No")]
        [PositiveInteger]
        public string PhoneNo { get; set; }

        [StringLength(15)]
        [Display(Name = "Post Code")]
        [AlphaNumeric]
        public string PostCode { get; set; }

        //[StringLength(5)]
        //[Display(Name = "State")]
        //public string StateCode { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        [AlphaNumericSpace]
        public string State { get; set; }

        [Display(Name = "City")]
        [StringLength(100)]
        [AlphaNumericSpace]
        public string City { get; set; }

        [StringLength(1)]
        [Display(Name = "Status")]
        public string Status { get; set; }


        [StringLength(255)]
        [Display(Name = "User Type")]
        public string TypeDesc { get; set; }


        [StringLength(200)]
        [Display(Name = "Branch")]
        public string BranchName { get; set; }


        [StringLength(200)]
        [Display(Name = "Distributor")]
        public string DistributorName { get; set; }

        [StringLength(200)]
        [Display(Name = "Company")]
        public string Company { get; set; }

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


        public IEnumerable<SelectListItem> CompanyDDL { get; set; }
        public IEnumerable<SelectListItem> DistributorDDL { get; set; }
        public IEnumerable<SelectListItem> BranchDDL { get; set; }
      
        public IEnumerable<SelectListItem> StateDDL { get; set; }
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        public IEnumerable<SelectListItem> UserTypeDDL { get; set; }
        public IEnumerable<SelectListItem> UserGroupDDL { get; set; }

        public ResetPasswordViewModel resetPasswordViewModel { get; set; }


    }//end class
    public class UserListViewModels : ActionsListViewModels
    {
        public string CompanyCode { get; set; }
        public string DistributorCode { get; set; }
        public string BranchCode { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public string CustCode { get; set; }
        public string GroupType { get; set; }
        public string GroupTypeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string GroupCode { get; set; }
        public string Status { get; set; }
        public string GroupName { get; set; }
        public string TypeName { get; set; }
        public bool EditGroupAndType { get; set; }
    }//end class
}//end namespace
