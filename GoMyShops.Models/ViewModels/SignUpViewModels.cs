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
    public class SignUpViewModels
    {
        [StringLength(20)]
        [Display(Name = "SignUp Name")]
        public string srcSignUpName { get; set; }

        [StringLength(100)]
        [Display(Name = "Company Name")]
        public string srcCompanyName { get; set; }

        [StringLength(20)]
        [Display(Name = "Contact No")]
        public string srcContactNo { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string srcEmail { get; set; }

        [Display(Name = "Create Date")]
        public string srcCreateDate { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class SignUpDetailsViewModels:  DetailsViewModels
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public SignUpDetailsViewModels() /*: base()*/
        {
            //_httpContextAccessor = httpContextAccessor;
        }


        [Required]
        [StringLength(20)]
        [Display(Name = "SignUp Name")]
        public string SignUpName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Company Name")]       
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Company Registration No")]
        public string CompanyRegistrationNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Contact No")]
        [PositiveInteger]
        public string ContactNo { get; set; }
        
        [StringLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [StringLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords does not match.")]
        public string ConfirmPassword { get; set; }


        [StringLength(2)]
        [Display(Name = "Status")]
        public string Status { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedTime { get; set; }

        [Display(Name = "Created Time")]
        public string sCreatedTime { get; set; } = string.Empty;

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; } = string.Empty;

        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "Modified Time")]
        public string sModifiedTime { get; set; } = string.Empty;

        public bool IsSignUpSuccess { get; set; } 

        //public string EditJson { get; set; }

    }//end class

    public class SignUpListViewModels : ActionsListViewModels
    {
        public string SignUpName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string sCreatedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
    }//end class

    public class SignUpVerifyViewModels
    {
        public string Message { get; set; }
        public bool IsVerified { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsActive { get; set; }
        public bool IsPending { get; set; }
        public bool IsSignUpUser { get; set; }
        public string SignUpName { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
    }//end class

    public class SignUpLoginViewModels
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }

}//end namespace
