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
    public class RecipientConnectionViewModels
    {
        [StringLength(100)]
        [Display(Name = "Recipient Connection Name")]
        public string srcRecipientConnectionName { get; set; }

        //[StringLength(30)]
        [Display(Name = "Connection Type")]
        public string srcConnectionType { get; set; }

        //[StringLength(30)]
        //[Display(Name = "Dashboard Code")]
        //public string srcDashboardCode { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> ConnectionTypeDDL { get; set; }
    }//end class

    public class RecipientConnectionDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipientConnectionDetailsViewModels() : base()
        {

        }

        public RecipientConnectionDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //[StringLength(20)]
        [Display(Name = "Recipient Connection Code")]
        public string RecipientConnectionCode { get; set; }

        [Display(Name = "Recipient Connection List")]
        public string RecipientConnectionCopy { get; set; }

        [Required]
        [Descriptions]
        [StringLength(100)]
        [Display(Name = "Recipient Connection Name")]
        public string RecipientConnectionName { get; set; }

        [Required]
        [StringLength(500)]
        [Descriptions]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Required]
        [Display(Name = "Connection Type")]
        public string ConnectionType { get; set; }

        [Display(Name = "Connection Type")]
        public string ConnectionTypeName { get; set; }

        public string UserGroupTreeViewJson { get; set; }
        public string UserTreeViewJson { get; set; }
        public string RecipientTreeViewJson { get; set; }

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

       
        public IEnumerable<SelectListItem> ConnectionTypeDDL { get; set; }
        public IEnumerable<SelectListItem> RecipientConnectionDDL { get; set; }


    }//end class

    public class RecipientConnectionListViewModels : ActionsListViewModels
    {
        public string RecipientConnectionCode { get; set; }
        public string RecipientConnectionName { get; set; }
        public string ConnectionType { get; set; }       
        public string Status { get; set; }
    }//end class

    public class RecipientConnectionCollectionViewModels 
    {
        public RecipientConnectionDetailsViewModels RecipientConnectionDetails { get; set; }
        public List<RecipientDetailsViewModels> RecipientDetailsList { get; set; }
  
    }//end class

    public class RecipientCodeAndDashboardViewModels
    {
        public string RecipientConnectionCode { get; set; }
        public string RecipientConnectionName { get; set; }
        public string Dashboard { get; set; }

    }//end class

}//end namespace