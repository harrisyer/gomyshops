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

    public class CommentChatViewModels
    {
        [StringLength(20)]
        [Display(Name = "Title")]
        public string srcCommentTitle { get; set; }

        [StringLength(100)]
        [Display(Name = "Comment")]
        public string srcComment { get; set; }

        [StringLength(20)]
        [Display(Name = "Comment Type")]
        public string srcCommentType { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }

        public IEnumerable<SelectListItem> CommentTypeDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
    }//end class

    public class CommentChatDisplayViewModels
    {
        public List<CommentChatDisplayDetailsViewModels> ccvm { get; set; }
    }

    public class CommentChatDisplayAnnouncementViewModels
    {
        public List<CommentChatAnnouncementViewModels> ccavmList { get; set; }
    }

    public class CommentChatTitleViewModels
    {
        [Required]
        [Descriptions]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Title")]
        public string Title { get; set; }
    
    }

    public class CommentChatAnnouncementViewModels
    {
        [Required]
        [Descriptions]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Descriptions]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Comment { get; set; }

        [Display(Name = "Created Time")]
        public DateTime CreatedTime { get; set; }
        
        //public string sCreatedTime { get; set; }

    }

    public class CommentChatDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentChatDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CommentId { get; set; }

        public int ParentId { get; set; }

        public int childCount { get; set; }

        public string collapseId { get; set; }

        //public string Username { get; set; }

        
        [Display(Name = "Post")]
        public string Title { get; set; }

        //[Required]
        //[Descriptions]
        //[StringLength(2000)]
        //public string CommentMessage { get; set; }

     
        public string Comment { get; set; }

     
        public string Type { get; set; }

        //public List<CommentChatDetailsViewModels> InnerCCVM { get; set; }

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

    }

    public class CommentChatDisplayDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentChatDisplayDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CommentId { get; set; }

        public int ParentId { get; set; }

        public int childCount { get; set; }

        public string collapseId { get; set; }

        public string Username { get; set; }


        [Display(Name = "Post")]
        public string Title { get; set; }

        [Required]
        [Descriptions]
        [StringLength(2000)]
        public string CommentMessage { get; set; }


        public string Comment { get; set; }


        public string Type { get; set; }

        public List<CommentChatDisplayDetailsViewModels> InnerCCVM { get; set; }

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

    }

    public class CommentChatListViewModels : ActionsListViewModels
    {
        public int Id { get; set; }
        //public string CreatedBy { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public System.DateTime CreateDate { get; set; }
        public string sCreateDate { get; set; }
        public string CommentType { get; set; }
        public string CommentTypeName { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
    }//end class
}//end namespace
