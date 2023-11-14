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
using System.Web;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models.ViewModels
{
    public class MerchantTidProfileViewModels:ListBAL
    {
       // private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantTidProfileViewModels() : base()
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        [StringLength(30)]
        [Display(Name = "User ID")]
        public string srcUserID { get; set; }

        [StringLength(30)]
        [Display(Name = "Merchant Name")]
        public string srcMerchantCode { get; set; }

        [StringLength(30)]
        [Display(Name = "TID Code")]
        public string srcTID { get; set; }

        [StringLength(100)]
        [Display(Name = "TID Name")]
        public string srcTIDName { get; set; }

        [Display(Name = "Applcation Status")]
        public string srcApplicationStatus { get; set; }

        [Display(Name = "Status")]
        public string srcStatus { get; set; }
 
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> ApplicationStatusDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantCodeDDL { get; set; }
    }//end class

    public class MerchantTidProfileDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MerchantTidProfileDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
            ServerIPList = new List<MerchantTidServerIPDetailsViewModels>();
            URLList = new List<MerchantTidURLDetailsViewModels>();
            MIDList = new List<MerchantMIDListViewModels>();
        }
       

        public string CurrentStatus { get; set; }
        public bool ApproveRight { get; set; }

        public string CallerMenuName { get; set; }

        [StringLength(30)]
        [Display(Name = "Merchant")]
        public string CustomerCode { get; set; }

       
        [StringLength(30)]
        [Display(Name = "Merchant Name")]
        public string CustomerName { get; set; }

        [StringLength(30)]
        [Display(Name = "TID Code")]
        public string CustomerTIDCode { get; set; }

        [Required]
        [StringLength(100)]
        [Descriptions]
        [Display(Name = "Merchant Site Name")]
        public string SiteName { get; set; }
        
        [Display(Name = "Merchant Site Descriptor")]
        [StringLength(250)]
        [Descriptions]
        public string SiteDescriptor { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Display(Name = "Merchant Terminal ID (TID)")]
        //public string MERCHANTCODE { get; set; }

     
        [StringLength(250)]
        [Required]
        //[RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?", ErrorMessage = "Invalid URL.")]
        [Display(Name = "Return URL")]
        public string ReturnURL { get; set; }

        [StringLength(250)]
        [Required]
        [Display(Name = "Back End Return URL")]
        public string BackEndReturnURL { get; set; }

        [Display(Name = "Generic Response Code")]
        public bool responseFlag { get; set; }

        [Required]
        [Display(Name = "Display Shipping Information")]
        public bool shippingInfo { get; set; }

        [Required]
        [Display(Name = "Display Receipt")]
        public bool showReceipt { get; set; }

        [Required]
        [Display(Name = "Allow Link Payment")]
        public bool LinkPayment { get; set; }


        public bool mid { get; set; }

        [Display(Name = "New Company Logo")]
        public string logo { get; set; }

        //[FileExtensions(Extensions = "png,jpeg,jpg,gif,bmp", ErrorMessage = "Please upload valid format")]
        public IFormFile UploadedFile { get; set; }

        [Display(Name = "Header Note")]
        [StringLength(2000)]
        public string HeaderNote { get; set; }

        [Display(Name = "Content Note")]
        [StringLength(2000)]
        public string ContentNote { get; set; }

        [Display(Name = "Themes")]
        public string ThemeCode { get; set; }

        //[Display(Name = "Screen Background Color")]
        //public string backgroundcolor { get; set; }

        //[Display(Name = "Header Font Type")]
        //public string headerfonttype { get; set; }

        //[Display(Name = "Header Font Size")]
        //public string headerfontsize { get; set; }

        //[Display(Name = "Header Font Color")]
        //public string headerfontcolor { get; set; }


        //[Display(Name = "Header Note")]
        //public string headernote { get; set; }

        //[Display(Name = "Content Note")]
        //public string contentnote { get; set; }

        //[Display(Name = "Content Font Type")]
        //public string contentfonttype { get; set; }

        //[Display(Name = "Content Font Size")]
        //public string contentfontsize { get; set; }

        //[Display(Name = "Content Font Color")]
        //public string contentfontcolor { get; set; }

        public int RemoveLineNumber { get; set; }

        public bool IsMaxServerIP { get; set; }
        public bool IsMaxURL { get; set; }

        [Display(Name = "Application Status")]
        public string appStatus { get; set; }
        [Display(Name = "Application Status")]
        public string appStatusName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Secret Key")]
        public string SecretKey { get; set; }

        //[Required]
        [Display(Name = "Profile Status")]
        public string profileStatus { get; set; }

        [Display(Name = "Profile Status")]
        public string profileStatusName { get; set; }

        [Display(Name = "Active")]
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

        [Display(Name = "Application Modified By")]
        public string ModifiedApplicationStatusBy { get; set; }

        [Display(Name = "Application Modified Time")]
        public DateTime? ModifiedApplicationStatusTime { get; set; }

        [Display(Name = "Application Modified Time")]
        public string sModifiedApplicationStatusTime { get; set; }

        [Display(Name = "Status Modified By")]
        public string ModifiedStatusBy { get; set; }

        [Display(Name = "Status Modified Time")]
        public DateTime? ModifiedStatusTime { get; set; }

        [Display(Name = "Status Modified Time")]
        public string sModifiedStatusTime { get; set; }

        [Display(Name = "Create Check By 1")]
        public string CreateCheckBy1 { get; set; }

        public DateTime? CreateCheckBy1Time { get; set; }

        [Display(Name = "Create Check By 1 Time")]
        public string sCreateCheckBy1Time { get; set; }

        [Display(Name = "Create Check By 2")]
        public string CreateCheckBy2 { get; set; }

        public DateTime? CreateCheckBy2Time { get; set; }

        [Display(Name = "Create Check By 2 Time")]
        public string sCreateCheckBy2Time { get; set; }

        [Display(Name = "Edit Check By 1")]
        public string EditCheckBy1 { get; set; }

        public DateTime? EditCheckBy1Time { get; set; }

        [Display(Name = "Edit Check By 1 Time")]
        public string sEditCheckBy1Time { get; set; }

        [Display(Name = "Edit Check By 2")]
        public string EditCheckBy2 { get; set; }

        public DateTime? EditCheckBy2Time { get; set; }

        [Display(Name = "Edit Check By 2 Time")]
        public string sEditCheckBy2Time { get; set; }

        [StringLength(500)]
        [Descriptions]
        [Display(Name = "Remark")]
        public string ApproveRemark { get; set; }

        [Display(Name = "Previous Remark")]
        public string ApproveRemarkReadOnly { get; set; }

        public List<MerchantMIDListViewModels> MIDList { get; set; }
        public List<MerchantTidServerIPDetailsViewModels> ServerIPList { get; set; }
        public List<MerchantTidURLDetailsViewModels> URLList { get; set; }

        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> CustomerDDL { get; set; }
        public IEnumerable<SelectListItem> ProfileStatusDDL { get; set; }
        public IEnumerable<SelectListItem> MerchantStatusDDL { get; set; }
        public IEnumerable<SelectListItem> ThemesCodeDDL { get; set; }
    }

    public class MerchantMIDListViewModels
    {
        public bool Selected { get; set; }
        public string MIDCode { get; set; }
        public string MIDDescription { get; set; }
        public string Processor { get; set; }
        public string MCC { get; set; }
        public string MCCDescription { get; set; }
        public string Currency { get; set; }
        public string CardType { get; set; }

    }//end class

    public class MerchantTidServerIPDetailsViewModels
    {
        public int LineNumber { get; set; }

        [StringLength(200)]
        
        public string ServerIP { get; set; }

    }//end class

    public class MerchantTidURLDetailsViewModels
    {
        public int LineNumber { get; set; }

        [StringLength(200)]
        //[RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?", ErrorMessage = "Invalid URL.")]
        public string URL { get; set; }

    }//end class

    public class MerchantTidProfileListViewModels : ActionsListViewModels
    {
        public string customer { get; set; }

        public string customerCode { get; set; }

        public string customerName { get; set; }

        public string TIDCode { get; set; }
        public string TIDName { get; set; }
        public string SiteName { get; set; }

        public string appStatus { get; set; }

        public string appStatusName { get; set; }

        public string CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public string Status { get; set; }

        public string CreateCheckBy1 { get; set; }
        public string CreateCheckBy2 { get; set; }
        public string EditCheckBy1 { get; set; }
        public string EditCheckBy2 { get; set; }


    }

}//end namespace
