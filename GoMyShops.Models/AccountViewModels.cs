using System.ComponentModel.DataAnnotations;

namespace GoMyShops.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
    }

    public class ResetPasswordViewModel
    {
        //[Required]
        //[EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string? ResetPasswordUserName { get; set; } = string.Empty;

        public string EmailCode { get; set; }

        public string? Type { get; set; } = string.Empty;

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string? CurrentPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords does not match.")]
        public string ConfirmPassword { get; set; }

        
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }
    }

    public class ResetEmailViewModel
    {
        [StringLength(20)]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Old Email")]
        public string? OldEmail { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [Display(Name = "New Email")]
        public string? Email { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [Compare(nameof(Email), ErrorMessage = "New Email don't match.")]
        [Display(Name = "Confirm New Email")]
        public string? ConfirmEmail { get; set; }
    }

    public class VerifyCodeViewModel
    {

        public string Provider { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; }    

        public string ErrorMessage { get; set; } = string.Empty;

        public string PhoneCode { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }
}
