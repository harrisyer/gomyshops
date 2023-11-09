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
    public class LoginSetupViewModels
    {
        [StringLength(60)]
        [Display(Name = "Security Name")]
        public string srcSecurityName { get; set; }

        [Display(Name = "Required Password Length")]
        public int srcRequiredLength { get; set; }

        [Display(Name = "Lockout Enabled")]
        public bool srcUserLockoutEnabledByDefault { get; set; }

        [Display(Name = "Password Expiry Enabled")]
        public int srcRequireChangePasswordInPeriod { get; set; }
    }//end class

    public class LoginSetupDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginSetupDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //[Required]
        //public int SecurityID { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Security Name")]
        public string SecurityName { get; set; }

        [Required]
        [Display(Name = "Allow Only Alphanumeric User Names")]
        public bool AllowOnlyAlphanumericUserNames { get; set; }

        [Required]
        [Display(Name = "Require Unique Email")]
        public bool RequireUniqueEmail { get; set; }

        [Required]
        [PositiveInteger]
        [Display(Name = "Required Password Length")]
        public int RequiredLength { get; set; }

        [Required]
        [Display(Name = "Require Special Character")]
        public bool RequireNonLetterOrDigit { get; set; }

        [Required]
        [Display(Name = "Require Number")]
        public bool RequireDigit { get; set; }

        [Required]
        [Display(Name = "Require Lower Case")]
        public bool RequireLowerCase { get; set; }

        [Required]
        [Display(Name = "Require Upper Case")]
        public bool RequireUpperCase { get; set; }

        [Required]
        [Display(Name = "Lockout Enabled")]
        public bool UserLockoutEnabledByDefault { get; set; }

        [Required]
        [PositiveInteger]
        [Range(1, 99, ErrorMessage = "Must be Positive value")]
        [Display(Name = "Failed Attempts Before Lockout")]
        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        [Required]
        [PositiveInteger]
        [Range(1, 90000000, ErrorMessage = "Must be Positive value")]
        [Display(Name = "Account Lockout Time (Minutes)")]
        public int DefaultAccountLockoutTimeSpan { get; set; }

        [Required]
        [Display(Name = "Require Approved")]
        public bool RequireApproved { get; set; }

        [Required]
        [Display(Name = "Password Expiry Enabled")]
        public bool RequireChangePasswordInPeriod { get; set; }

        [Required]
        [PositiveInteger]
        [Display(Name = "Days Until Password Expiry")]
        public int ChangePasswordInPeriodTimeSpan { get; set; }

        [Required]
        [Display(Name = "Require First Time Change Password")]
        public bool RequireFirstTimeChangePassword { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }
     
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }//end class

    public class LoginSetupListViewModels : ActionsListViewModels
    {
        public string   SecurityName { get; set; }
        public string   RequiredLength { get; set; }
        public string   UserLockoutEnabledByDefault { get; set; }
        public string   RequireChangePasswordInPeriod { get; set; }
    }//end class
}//end namespace

