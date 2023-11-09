using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("LoginSU")]
    public partial class LoginSU
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SecurityId { get; set; }

        [Required]
        [StringLength(60)]
        public string SecurityName { get; set; }

        public bool AllowOnlyAlphanumericUserNames { get; set; }

        public bool RequireUniqueEmail { get; set; }

        public int RequiredLength { get; set; }

        public bool RequireNonLetterOrDigit { get; set; }

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireUppercase { get; set; }

        public bool UserLockoutEnabledByDefault { get; set; }

        public int DefaultAccountLockoutTimeSpan { get; set; }

        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        public bool RequireApproved { get; set; }

        public bool RequireChangePasswordInPeriod { get; set; }

        public int ChangePasswordInPeriodTimeSpan { get; set; }

        public bool RequireFirstTimeChangePassword { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

    }//end class
}//end namespace
