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
    public class IndexViewModel : ListBAL
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexViewModel(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Harris add URL Links
        public string ApplicationStatusApprovedURL { get; set; }
        public string ApplicationStatusPendingOnboardingURL { get; set; }
        public string ApplicationStatusPendingURL { get; set; }
        public string ApplicationStatusDeclinedURL { get; set; }      
        public string StatusActiveURL { get; set; }
        public string StatusInActiveURL { get; set; }
        public string StatusSuspendedURL { get; set; }
        public string StatusTerminatedURL { get; set; }

        //Jie Long URL Links
        public string MerchantWithTransactionCountURL { get; set; }
        public string MerchantWithoutTransactionCountURL { get; set; }
        public string CreditRatioReportURL { get; set; }
        public string DisputeRatioReportURL { get; set; }

        public string ActionsMenuTotalPaymentToday { get; set; }

        public string ActionsMenuTotalPaymentTodayValue { get; set; }

        public string ActionsMenuSuccessfulTransactionsToday { get; set; }

        public string ActionsMenuSuccessfulTransactionsTodayValue { get; set; }

        public string ActionsMenuTotalPaymentCollectedThisMonth { get; set; }

        public string ActionsMenuTotalPaymentCollectedThisMonthValue { get; set; }

        public string ActionsMenuPreviousSettlement { get; set; }

        public string ActionsMenuPreviousSettlementValue { get; set; }

        public string ActionsMenuNextSettlement { get; set; }

        public string ActionsMenuNextSettlementValue { get; set; }

        public string SalesAreaChartJson { get; set; }
        
        //public string ActionTypeTitle { get; set; }

        public string SalesAreaChartTitle { get; set; }

        public string SalesAreaChartTimeTitle { get; set; }

        public string SalesPieChartTitle { get; set; }

        public string SalesPieChartTimeTitle { get; set; }

        public string SalesPieChartJson { get; set; }       

        public string TicketSizeTitle { get; set; }

        public string TicketSizeTimeTitle { get; set; }

        public string TicketSize { get; set; }

        public string TicketSizeCount { get; set; }


        public string AnnoucementType { get; set; }

        #region Admin Dashboard
        public int ActionsMenuActiveMerchantCount { get; set; }
        public int ActionsMenuDeactivatedMerchantCount { get; set; }
        public int ActionsMenuSuspendedMerchantCount { get; set; }
        public int ActionsMenuTerminatedMerchantCount { get; set; }

        public int ActionsMenuApplicationStatusPendingOnboardingMerchantCount { get; set; }
        public int ActionsMenuApplicationStatusPendingMerchantCount { get; set; }
        public int ActionsMenuApplicationStatusApprovedMerchantCount { get; set; }
        public int ActionsMenuApplicationStatusDeclinedMerchantCount { get; set; }

        public string sActionsMenuRefundRatio { get; set; }
        public string sActionsMenuChargebackRatio { get; set; }

        public int ActionsMenuMerchantWithTransactionCount { get; set; }
        public int ActionsMenuMerchantWithoutTransactionCount { get; set; }

        public string AdminSalesBarChartJson { get; set; }
        public string AdminSalesBarChartTitle { get; set; }
        public string AdminSalesBarChartLabelList { get; set; }

        public string AdminSalesBarChartNumberJson { get; set; }
        public string AdminSalesBarChartNumberTitle { get; set; }
        public string AdminSalesBarChartNumberLabelList { get; set; }

        public string AdminSalesAreaChartJson { get; set; }
        public string AdminSalesAreaChartTitle { get; set; }
        public string sAdminSalesAreaChartTotalSaleAmountToday { get; set; }
        public string sAdminSalesAreaChartTotalSaleAmountTotalToday { get; set; }
        public int sAdminSalesAreaChartTotalSaleCountToday { get; set; }
        public string sAdminSalesAreaChartTotalSaleAmountThisMonth { get; set; }
        public int sAdminSalesAreaChartTotalSaleCountThisMonth { get; set; }
        public string sAdminSalesAreaChartTotalSaleAmountThisYear { get; set; }
        public int sAdminSalesAreaChartTotalSaleCountThisYear { get; set; }

        #endregion

        public string Today { get; set; }
        public string ThisMonth { get; set; }
        public string ThisYear { get; set; }

    }//end class

    public class YearlyChartViewModel
    {
        public int Month { get; set; }
        public string sMonth { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public string sAmount { get; set; }
    }
   
}//end namespace
