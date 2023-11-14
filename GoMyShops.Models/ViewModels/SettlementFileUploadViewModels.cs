using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Models;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models.ViewModels
{
    public class SettlementFileUploadViewModels
    {
        [StringLength(30)]
        [Display(Name = "Bank Name")]
        public string srcBankName { get; set; }

        [Display(Name = "Settlement Date")]
        public DateTime? srcSettlementDate { get; set; }

        public IEnumerable<SelectListItem> BankNameDDL { get; set; }

        public string FileUploadFormat { get; set; }
    }

    public class SettlementFileUploadDetailsViewModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SettlementFileUploadDetailsViewModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Required]
        [StringLength(30)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required]
        [Display(Name = "Settlement Date")]
        public string sSettlementDate { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile FileUpload { get; set; }

        public IEnumerable<SelectListItem> BankNameDDL { get; set; }

        public string ErrorMsg { get; set; }

        public string FileUploadFormat { get; set; }
    }

    public class SettlementFileUploadListViewModels : ActionsListViewModels
    {
        public int                  Id { get; set; }
        public string               BankName { get; set; }
        public DateTime             SettlementDate { get; set; }
        public string               sSettlementDate { get; set; }
        public string               FileName { set; get; }
        public DateTime             CreatedTime { set; get; }
        public string               sCreatedTime { set; get; }
        public DateTime             TransactionTime { set; get; }
        public string               sTransactionTime { set; get; }
        public int                  Status { get; set; }
        public string               sStatus { get; set; }
        public ActionsListDetails   ReconJson { get; set; }
        public ActionsListDetails   ExceptionJson { get; set; }

        public int                  GatewayTransactionCount { get; set; }
        public int                  SettlementFileTransactionCount { get; set; }
        public int                  ReconSuccessCount { get; set; }
        public int                  ExceptionCount { get; set; }
        public int                  ResolveCount { get; set; }
        public ActionsListDetails   TRListJSON { get; set; }
        public ActionsListDetails   SuccessTRListJSON { get; set; }
        public ActionsListDetails   ResolvedTRListJSON { get; set; }
    }

    public class SettlementFileExceptionViewModels
    {
        [Display(Name = "Bank Name")]
        public string   srcExceptionBankName { get; set; }

        [Display(Name = "Settlement Date")]
        public string   srcsExceptionSettlementDate { get; set; }

        [Display(Name = "Transaction Status")]
        public string   srcGatewayTransactionStatus { get; set; }

        [Display(Name = "Settlement Status")]
        public string   srcGatewaySettlementStatus { get; set; }

        public IEnumerable<SelectListItem> TransactionStatusDDL { get; set; }
        public IEnumerable<SelectListItem> SettlementStatusDDL { get; set; }
    }

    public class SettlementFileExceptionListViewModels
    {
        public int          ID { get; set; }
        public string       OrderID { get; set; }
        public string       Message { get; set; }

        public string       IntegrationCode { get; set; }
        public DateTime     SettlementDate { get; set; }
        public string       sSettlementDate { get; set; }

        public int          GatewayTransactionStatus { get; set; }
        public int?         GatewaySettlementStatus { get; set; }
        public int          SettlementFileStatus { get; set; }
        public bool         IsRefundable { get; set; }

        public string       ResolvedBy { get; set; }
        public int          ResolveMethod { get; set; }
        public string       sResolveMethod { get; set; }
        public DateTime?    ResolveTime { get; set; }
        public string       sResolveTime { get; set; }
        public int          ResolveID { get; set; }

        public bool         IsRefund { get; set; }
        public string       RefundType { get; set; }
        public bool         HasProcessorRefund { get; set; }

        public DateTime     CreatedTime { get; set; }
        public string       sCreatedTime { get; set; }

        public ActionsListDetails TRDetailsJSON { get; set; }
        public string       orderingInfo { get; set; }
        public DateTime     TransactionTime { get; set; }
        public string       sTransactionTime { get; set; }
    }

    public class SettlementFileReconViewModels
    {
        [Display(Name = "Bank Name")]
        public string ReconBankName { get; set; }

        [Display(Name = "Settlement Date")]
        public string sReconSettlementDate { get; set; }

        [Display(Name = "File Name")]
        public string FileName { set; get; }

        public DateTime CreatedTime { set; get; }

        [Display(Name = "Upload Time")]
        public string sCreatedTime { set; get; }

        public int Status { get; set; }

        [Display(Name = "Status")]
        public string sStatus { get; set; }
    }
}
