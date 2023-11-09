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

namespace GoMyShops.Models.ViewModels
{
    public class AppCtrViewModels
    {

    }

    public class AppCtrDetailViewModels
    {
        public int AppCtrlID { get; set; }

        [Display(Name = "Control Name")]
        public string AppCtrName { get; set; }

        [StringLength(1)]
        public string AppCtrType { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        [Display(Name = "User ID")]
        public string UserCode { get; set; }

        [Display(Name = "User Group")]
        public string GroupCode { get; set; }

        public bool Status { get; set; }
    }//end class
}//end namespace
