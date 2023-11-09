using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public class LoginSetupModel
    {
        public int SecurityId { get; set; }
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
    }//end class
}//end namespace
