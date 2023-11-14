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
    public class DocumentUploadViewModels
    {
        [StringLength(100)]
        [Display(Name = "Title")]
        public string srcTitle { get; set; }

        [StringLength(100)]
        [Display(Name = "Message")]
        public string srcMessage { get; set; }

        [StringLength(20)]
        [Display(Name = "Type")]
        public string srcType { get; set; }

        [StringLength(20)]
        [Display(Name = "Type")]
        public string srcFileName { get; set; }

        [Display(Name = "Created By")]
        public string srcCreatedBy { get; set; }

        [Display(Name = "Created Time")]
        public string srcCreatedTime { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }

    }//end class

    public class DocumentUploadDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DocumentUploadDetailsViewModels() : base()
        {      
        }

        public DocumentUploadDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "Document Code")]
        public string DocumentCode { get; set; }

        [Required]
        [Descriptions]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Descriptions]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Message { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Type")]
        public string TypeDisplay { get; set; }

        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [AlphaNumericSpace]
        [StringLength(100)]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Existing File Name")]
        public string FileNameExisting { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }
        [Display(Name = "Created Time")]
        public string sCreatedTime { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "Modified Time")]
        public string sModifiedTime { get; set; }

        [StringLength(1)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }


        public string IdKey { get; set; }
        public MerchantOnboardingMainCheckListDetailsViewModels MerchantOnboardingMainCheckListDetailsVM { get; set; }

    }//end class

    public class DocumentUploadListViewModels : ActionsListViewModels
    {
        public string DocumentCode { get; set; }
        //public string OnBoardingCode { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string sCreateDate { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }       
        public string Type { get; set; }        
        public int Priority { get; set; }
        public int RowIndex { get; set; }
        public string FileName { get; set; }      
        public string Status { get; set; }
        public string url { get; set; }
    }//end class

    public class DocumentDownloadViewModels
    {
        public List<DocumentUploadListViewModels> dulvm { get; set; }
    }


}//end namespace
