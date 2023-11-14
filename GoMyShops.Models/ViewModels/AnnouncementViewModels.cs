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
    public class AnnouncementViewModels
    {
        [StringLength(500)]
        [Display(Name = "Title")]
        public string? srcTitle { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Message")]
        public string? srcMessage { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Type")]
        public string? srcType { get; set; } = string.Empty;

        [Display(Name = "Display Frequency")]
        public string? srcDisplayFrequency { get; set; } = string.Empty;

        [Display(Name = "Target Audience")]
        public string? srcTargetAudience { get; set; } = string.Empty;

        [Display(Name = "Start Date")]
        public string? srcStartDate { get; set; }

        [Display(Name = "Status")]
        public string? srcStatus { get; set; } = string.Empty;

        public IEnumerable<SelectListItem>? AnnouncementTypeDDL { get; set; }
        public IEnumerable<SelectListItem>? DisplayFrequencyDDL { get; set; }
        public IEnumerable<SelectListItem>? TargetAudienceDDL { get; set; }
        public IEnumerable<SelectListItem>? StatusDDL { get; set; }
    }//end class

    public class AnnouncementDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnnouncementDetailsViewModels() : base()
        {
     
        }

        public AnnouncementDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Descriptions]
        [StringLength(500)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        //[AllowHtml]        
        [Display(Name = "Content")]
        public string? Message { get; set; }= string.Empty;

        [Display(Name = "Type")]
        public string? Type { get; set; }=string.Empty;

        [Display(Name = "Type")]
        public string? TypeDisplay { get; set; } = string.Empty;

        [Display(Name = "Display Frequency")]
        public string? DisplayFrequency { get; set; } = string.Empty;

        [Display(Name = "Display Frequency")]
        public string? DisplayFrequencyDisplay { get; set; } = string.Empty;

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Merchant")]
        public bool IsMerchant { get; set; }

        [Display(Name = "Partner")]
        public bool IsPartner { get; set; }


        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Start Date")]
        public string sStartDate { get; set; }=DateTime.Now.ToString(CommonSetting.StandardDateFormat);

        [Display(Name = "Start Date")]
        public string? sStartDateDisplay { get; set; }

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [Display(Name = "End Date")]
        public string sEndDate { get; set; } = DateTime.Now.AddDays(1).ToString(CommonSetting.StandardDateFormat);

        [Display(Name = "End Date")]
        public string? sEndDateDisplay { get; set; } = string.Empty;

        public string? CreatedBy { get; set; }=string.Empty;

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [Display(Name = "Created Time")]
        public string? sCreatedTime { get; set; } = string.Empty;

        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "Modified Time")]
        public string? sModifiedTime { get; set; }

        [StringLength(1)]
        [Display(Name = "Status")]
        public string? Status { get; set; }

        [Display(Name = "Status")]
        public bool CheckBoxStatus { get; set; }

        public IEnumerable<SelectListItem>? AnnouncementTypeDDL { get; set; }
        public IEnumerable<SelectListItem>? DisplayFrequencyDDL { get; set; }

    }

    public class AnnouncementIndexMainList
    {
        public List<AnnouncementIndexList>? data;    
    }

    public class AnnouncementIndexList
    {
        public string Code1=string.Empty;
        public int RowNo1;
    }

    public class AnnouncementListViewModels : ActionsListViewModels
    {
        public int Id { get; set; }
        //public string CreatedBy { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public string Message { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string sCreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public string sStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string sEndDate { get; set; }
        public string Type { get; set; }
        public string DisplayFrequency { get; set; }
        public int Priority { get; set; }
        public int RowIndex { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMerchant { get; set; }
        public bool IsPartner { get; set; }
        public string TargetAudience { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }//end class

    public class AnnouncementDisplayViewModels
    {
        public int AnnouncementCount { get; set; }
        public List<AnnouncementDisplayDetailsViewModels> advm { get; set; }
        public List<AnnouncementDisplayDetailsViewModels> advm2 { get; set; }
    }

    public class AnnouncementDisplayDetailsViewModels
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Descriptions]
        [StringLength(500)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        //[AllowHtml]
        // @Html.Raw(theString)
        [Display(Name = "Content")]
        public string Message { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Display Frequency")]
        public string DisplayFrequency { get; set; }

        [Display(Name = "Display Frequency")]
        public string DisplayFrequencyDisplay { get; set; }

        public int Priority { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Merchant")]
        public bool IsMerchant { get; set; }

        [Display(Name = "Partner")]
        public bool IsPartner { get; set; }


        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public string sStartDate { get; set; }

        [Display(Name = "Start Date")]
        public string sStartDateDisplay { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "End Date")]
        public string sEndDate { get; set; }

        public DateTime CreatedTime { get; set; }       

        public DateTime? ModifiedTime { get; set; }

        public string CssClass { get; set; }

    }

    
}//end namespace
