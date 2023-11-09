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
    public class LinkPaymentViewModels
    {
        [MaxLength(50)]
        [Display(Name = "Merchant Name")]
        public string MerchantCode { get; set; }

        [StringLength(30)]
        [Display(Name = "Terminal ID (TID)")]
        public string TIDCode { get; set; }



        [Display(Name = "Link Payment Name")]
        public string LinkPaymentName { get; set; }

        [Display(Name = "Expiry Date")]
        public string sExpiryDate { get; set; }

        [Display(Name = "Created Date")]
        public string sCreatedDate { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Order Detail")]
        public string OrderDetail { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }


        [Display(Name = "Amount")]
        public string sAmount { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(0, 10000000000000, ErrorMessage = "This Field is 0 to 10000000000000")]
        public decimal Amount { get; set; }



        [MaxLength(50)]
        [Required]
        [Display(Name = "Merchant Name")]
        public string C_MerchantCode { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Terminal ID (TID)")]
        public string C_TIDCode { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string C_Currency { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(1, 10000000000000, ErrorMessage = "This Field is 1 to 10000000000000")]
        public decimal C_Amount { get; set; }

        //[Required]
        //[Display(Name = "Amount")]
        //public string C_sAmount { get; set; }

        [Required]
        [Display(Name = "Order Detail")]
        public string C_OrderDetail { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string C_Status { get; set; }

        [Required]
        [Display(Name = "Link Payment Name")]
        public string C_LinkPaymentName { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public string C_sExpiryDate { get; set; }


        public IEnumerable<SelectListItem> MerchantCodeDDL { get; set; }
        public IEnumerable<SelectListItem> TIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> StatusDDL { get; set; }
        public IEnumerable<SelectListItem> CurrencyDDL { get; set; }

        public IEnumerable<SelectListItem> C_MerchantCodeDDL { get; set; }
        public IEnumerable<SelectListItem> C_TIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> C_StatusDDL { get; set; }
        public IEnumerable<SelectListItem> C_CurrencyDDL { get; set; }

        public List<LinkPaymentItemViewModels> LinkPaymentItem { get; set; }

        public string Title { get; set; }

        [Range(1, 50)]
        [Display(Name = "Records Per Page")]
        public int RecordsPerPage { get; set; }
    }

    public class LinkPaymentCreateViewModels
    {
        public IEnumerable<SelectListItem> C_MerchantCodeDDL { get; set; }
        public IEnumerable<SelectListItem> C_TIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> C_StatusDDL { get; set; }
        public IEnumerable<SelectListItem> C_CurrencyDDL { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Merchant Name")]
        public string C_MerchantCode { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Terminal ID (TID)")]
        public string C_TIDCode { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string C_Currency { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(1, 10000000000000, ErrorMessage = "This Field is 1 to 10000000000000")]
        public decimal C_Amount { get; set; }

        [Required]
        [Display(Name = "Order Detail")]
        public string C_OrderDetail { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string C_Status { get; set; }

        [Required]
        [Display(Name = "Link Payment Name")]
        public string C_LinkPaymentName { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public string C_sExpiryDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime C_ExpiryDate { get; set; }

        [Display(Name = "Created Date")]
        public string C_sCreatedDate { get; set; }

        [Display(Name = "Created Date")]
        public DateTime C_CreatedDate { get; set; }
    }

    public class LinkPaymentEditViewModels
    {
        public IEnumerable<SelectListItem> C_MerchantCodeDDL { get; set; }
        public IEnumerable<SelectListItem> C_TIDCodeDDL { get; set; }
        public IEnumerable<SelectListItem> C_StatusDDL { get; set; }
        public IEnumerable<SelectListItem> C_CurrencyDDL { get; set; }


        [Display(Name = "Merchant Name")]
        public string C_MerchantCode { get; set; }


        [Display(Name = "Terminal ID (TID)")]
        public string C_TIDCode { get; set; }


        [Display(Name = "Currency")]
        public string C_Currency { get; set; }


        [Display(Name = "Amount")]
        [Range(1, 10000000000000, ErrorMessage = "This Field is 1 to 10000000000000")]
        public decimal C_Amount { get; set; }


        [Display(Name = "Order Detail")]
        public string C_OrderDetail { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string C_Status { get; set; }

        [Required]
        [Display(Name = "Link Payment Name")]
        public string C_LinkPaymentName { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public string C_sExpiryDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime C_ExpiryDate { get; set; }

        [Display(Name = "Created Date")]
        public string C_sCreatedDate { get; set; }

        [Display(Name = "Created Date")]
        public DateTime C_CreatedDate { get; set; }

        public int Id { get; set; }
    }

    public class LinkPaymentDetailViewModels
    {
        [MaxLength(50)]
        [Display(Name = "Merchant Name")]
        public string D_MerchantCode { get; set; }

        [StringLength(30)]
        [Display(Name = "Terminal ID (TID)")]
        public string D_TIDCode { get; set; }

        [Display(Name = "Currency")]
        public string D_Currency { get; set; }

        [Display(Name = "Amount")]
        [Range(1, 10000000000000, ErrorMessage = "This Field is 1 to 10000000000000")]
        public decimal D_Amount { get; set; }

        [Display(Name = "Order Detail")]
        public string D_OrderDetail { get; set; }

        [Display(Name = "Status")]
        public string D_Status { get; set; }

        [Display(Name = "Link Payment Name")]
        public string D_LinkPaymentName { get; set; }

        [Display(Name = "Expiry Date")]
        public string D_sExpiryDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime D_ExpiryDate { get; set; }

        [Display(Name = "Created Date")]
        public string D_sCreatedDate { get; set; }

        [Display(Name = "Created Date")]
        public DateTime D_CreatedDate { get; set; }

        [Required]
        public int D_LinkPaymentTransactionID { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string C_Email { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int C_Type { set; get; }

        public IEnumerable<SelectListItem> C_TypeDDL { get; set; }
    }

    public class LinkPaymentCreateItemViewModels
    {
        [Required]
        public int D_LinkPaymentTransactionID { get; set; }

        public int D_LinkPaymentItemID { get; set; }

        public string D_TidCode { get; set; }

        [Display(Name = "Email")]
        public string C_Email { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int C_Type { set; get; }

        [Display(Name = "Type")]
        public string sC_Type { set; get; }

        [Required]
        [Display(Name = "Status")]
        public string C_Status { get; set; }

        [Display(Name = "Status")]
        public string sC_Status { get; set; }

        [Display(Name = "URL")]
        public string C_URL { get; set; }

        public IEnumerable<SelectListItem> C_TypeDDL { get; set; }
        public IEnumerable<SelectListItem> C_StatusDDL { get; set; }
    }

    public class LinkPaymentListViewModels : ActionsListViewModels
    {
        public int RowNumber { set; get; }
        public string MerchantName { set; get; }
        public string MerchantCode { set; get; }
        public string TIDCode { set; get; }
        public DateTime ExpiryDate { get; set; }
        public string sExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string sCreatedDate { get; set; }
        public string LinkPaymentName { get; set; }
        public decimal Amount { get; set; }
        public string sAmount { get; set; }
        public string OrderDetail { get; set; }
        public string sStatus { get; set; }
        public int Status { get; set; }
        public string Currency { get; set; }
        public int LinkPaymentId { get; set; }
    }

    public class LinkPaymentItemViewModels : ActionsListViewModels
    {
        public string TokenId { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public string sType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string sCreatedDate { get; set; }
        public string URL { get; set; }
        public int Status { get; set; }
        public string sStatus { get; set; }
        public int LinkPaymentItemId { get; set; }
        public int RowNumber { get; set; }
    }

}
