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
    public class RecipientViewModels
    {
        [StringLength(100)]
        [Display(Name = "CompanyCode")]
        public string srcCompanyCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Recipient Group Code")]
        public string srcRecipientGroupCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Recipient Code")]
        public string srcRecipientCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Name")]
        public string srcName { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> CompanyDDL { get; set; }
        public IEnumerable<SelectListItem> RecipientGroupDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class RecipientDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipientDetailsViewModels() : base()
        {

        }

        public RecipientDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [StringLength(20)]
        [Display(Name = "Recipient Code")]
        public string RecipientCode { get; set; }

        //[Required]
        //[StringLength(20)]        
        //[Display(Name = "Recipient Group Code")]
        //public string RecipientGroupCode { get; set; }

        //[Display(Name = "Recipient Group")]
        //public string RecipientGroupName { get; set; }

        //[Display(Name = "Company Code")]
        //public string CompanyCode { get; set; }

        //[Display(Name = "Company Name")]
        //public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        [AlphaSpace]
        public string FirstName { get; set; }
      
        [StringLength(50)]
        [Display(Name = "Last Name")]
        [AlphaSpace]
        public string LastName { get; set; }

        //[StringLength(255)]
        //[Descriptions]
        //[Display(Name = "Decription")]
        //public string RecipientDecription { get; set; }

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

        [StringLength(1)]
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

        //public IEnumerable<SelectListItem> CompanyDDL { get; set; }
        //public IEnumerable<SelectListItem> RecipientGroupDDL { get; set; }

    }//end class
    public class RecipientListViewModels : ActionsListViewModels
    {  
        public string RecipientCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }//end class

}//end namespace