using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public class UserAccessLevelsModels
    {
        public List<int> CompanyCode { get; set; }
        public string DistributorCode { get; set; }
        public string BranchCode { get; set; }
        public List<string> CustomerCodeAccess { get; set; }
        public List<string> SupplierCodeAccess { get; set; }
    }//end class
}//end namespace
