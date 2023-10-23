using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public class AppUser: ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
        : base(principal)
        {
        }

        public string Name
        {
            get
            {
                if (this.FindFirst(ClaimTypes.Name)!=null)
                {
                   return this.FindFirst(ClaimTypes.Name).Value;
                }
                return "";
            }
        }

        public bool MultiLogin
        {
            get
            {
                var a = this.FindFirst("MultiLogin");
                if (a != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool MultiLoginFlag
        {
            get
            {
                var a = this.FindFirst("MultiLoginFlag");
                if (a != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string AuditKey
        {
            get
            {
                var a = this.FindFirst("AuditKey");
                if (a != null)
                {
                    return this.FindFirst("AuditKey").Value;
                }
                else
                {
                    return "0";
                }

            }
        }

        public string UserType
        {
            get
            {
                var a = this.FindFirst("UserType");
                if (a != null)
                {
                    return this.FindFirst("UserType").Value;
                }
                else
                {
                    return "0";
                }

            }
        }


    }//end class
}//end namespace
