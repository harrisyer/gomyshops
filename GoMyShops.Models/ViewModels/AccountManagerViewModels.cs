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
    public class AccountManagerViewModels
    {
        [StringLength(30)]
        [Display(Name = "Account Manager Code")]
        public string srcAccountManagerCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Account Manager Name")]
        public string srcAccountManagerName { get; set; }

        [StringLength(30)]
        [Display(Name = "User ID")]
        public string srcAccountManagerUserCode { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }      

    }//end class

    public class AccountManagerDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountManagerDetailsViewModels() : base()
        {

        }

        public AccountManagerDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Account Manager Code")]
        public string AccountManagerCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Account Manager Name")]
        [Descriptions]
        public string AccountManagerName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "User ID")]
        public string AccountManagerUserCode { get; set; }

        [StringLength(2)]
        [Display(Name = "Status")]
        public string Status { get; set; }

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

        public IEnumerable<SelectListItem> UserCodeDDL { get; set; }

    }//end class

    public class AccountManagerListViewModels : ActionsListViewModels
    {
        public string AccountManagerCode { get; set; }
        public string AccountManagerName { get; set; }
        public string AccountManagerUserCode { get; set; }     
        public string Status { get; set; }
    }//end class

}//end namespace
