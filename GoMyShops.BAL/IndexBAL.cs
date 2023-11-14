using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Data.Entity;
using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace GoMyShops.BAL
{
    public interface IIndexBAL 
    {
        //void getData(IndexViewModel model);
        //string GetTransactionList(IndexViewModel model, string ActionType, string ActionPeriod);
        //string GetDonutChart(IndexViewModel model, string ActionType, string ActionPeriod);
        //string GetTicketSize(IndexViewModel model, string ActionType, string ActionPeriod);

        //string GetAdminTransactionList(IndexViewModel model);
    }

    public class IndexBAL : BaseBAL, IIndexBAL
    {
        #region Definations
        private readonly ILogger<IndexBAL> _logger;
        IUnitOfWork _uow;
        IServicesBAL _servicesBAL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public IndexBAL(IHttpContextAccessor httpContextAccessor, IUnitOfWorkFactory uowFactory, IServicesBAL servicesBAL, ILogger<IndexBAL> logger)  : base()
        {
            _uow = uowFactory.Create();
            _servicesBAL = servicesBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;           
        }
        #endregion
        #region Constants
        const string APPLICATION_STATUS_PENDING = "2";
        const string APPLICATION_STATUS_APPROVED = "1";
        const string APPLICATION_STATUS_DECLINE = "0";

        const string STATUS_ACTIVE = "1";
        const string STATUS_INACTIVE = "0";
        const string STATUS_SUSPENDED = "7";
        const string STATUS_TERMINATED = "8";

        #endregion

        //public void getData(IndexViewModel model)
        //{
        //    //var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

        //    model.IsNullThenNew();

           

        //    model.SalesAreaChartTitle =" Transactions for today " +  DateTime.Now.ToString("yyyy MMMMMMMMMMMMM dd");

        //    //check Customer
        //    if (CurrentUser.UserType == CommonSetting.UserType.Customer)
        //    {
        //        mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
        //        TotalPaymentToday(model);
        //        GetSettlement(model);
        //    }//end if

        //    //check Partner
        //    if (CurrentUser.UserType == CommonSetting.UserType.Partner)
        //    {
        //        mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
        //        TotalPaymentTodayPartner(model);
        //        GetSettlementPartner( model);
        //    }//end if

        //    //check Admin
        //    if (CurrentUser.UserType == CommonSetting.UserType.Admin)
        //    {
        //        GetAdminMerchantInfo(model);
        //       // GetAdminTransactionList(model);
        //    }//end if


        //    //model.TargetDonutBarJson = this.GetTargetSalesDonutBarData();

        //    //this.GetDailyCreditSalesList(model, DateTime.Now.Month);

        //    model.Today = DateTime.Now.ToString(CommonSetting.StandardDateFormat);
        //    model.ThisMonth=DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture) + " " + DateTime.Now.Year.ToString();
        //    model.ThisYear = DateTime.Now.Year.ToString();


        //}
        //public string GetTransactionList(IndexViewModel model,string ActionType, string ActionPeriod)
        //{
        //    List<AreaChartSalesViewModels> salesList = new List<AreaChartSalesViewModels>();
        //    string dailyAreaChartSales = string.Empty;
        //    string ActionTypeTitle = string.Empty;
        //    try
        //    {
        //        ////check Customer
        //        //if (CurrentUser.UserType == CommonSetting.UserType.Customer)
        //        //{
        //        //    mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();            
        //        //}//end if

        //        ////check Partner
        //        //if (CurrentUser.UserType == CommonSetting.UserType.Partner)
        //        //{
        //        //    mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
        //        //}//end if


        //        switch (ActionType)
        //        {
        //            case "2":
        //                ActionTypeTitle = "Transactions Chart";
        //                break;           
        //            default:
        //                ActionTypeTitle = "Sales Chart";
        //                break;
        //        }//end switch   

        //        model.SalesAreaChartTimeTitle = ActionTypeTitle;

        //        DateTime first = DateTime.Now;
        //        DateTime last = DateTime.Now;
        //        switch (ActionPeriod)
        //        {
        //            case "2":
        //                first = DateTime.Today.AddDays(-1);
        //                last = first.AddDays(7);
        //                model.SalesAreaChartTitle = "for this week";
        //                break;
        //            case "3":
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //                last = DateTime.Today;
        //                model.SalesAreaChartTitle = "for this month";
        //                break;
        //            case "4":                        
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
        //                last = first.AddMonths(1).AddDays(-1);
        //                model.SalesAreaChartTitle ="for last month";
        //                break;
        //            default:                       
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        //                last = first;
        //                model.SalesAreaChartTitle = "for " + last.ToString("yyyy MMMMMMMMMMMMM dd");
        //                break;
        //        }//end switch   
      
        //        DateTime dateSTmte = first.MinusTimeEndSkipTime();
        //        DateTime dateETat = BAL.CommonFunctionsBAL.AddTimeEndSkipTime(last);

        //        IQueryable<AreaChartInitViewModels> data = null;

        //        //check Customer
        //        if (CurrentUser.UserType == CommonSetting.UserType.Customer)
        //        {
        //            mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
        //            data = (from customerMid in _uow.Repository<CustomerMID>().GetAsQueryable(x => x.CustomerCode == mCustomerCode)
        //                    join trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => (x.Status == TRANSACTIONSTATUS_SUCCESS ||
        //                         x.Status == TRANSACTIONSTATUS_DECLINED || x.Status == TRANSACTIONSTATUS_CANCEL)
        //                         && x.TransactionType == TRANSACTIONTYPE_INT_SALES) on new { customerMid.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                                                                                                                                                             //join customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode) on new { customerMid.CustomerCode } equals new { customer.CustomerCode } //into gcustomerMid
        //                    select new AreaChartInitViewModels
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,
        //                        TransactionStatus = trv.Status,
        //                        Amount = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });
        //        }//end if

        //        //check Partner
        //        if (CurrentUser.UserType == CommonSetting.UserType.Partner)
        //        {
        //            mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
        //            data = (from customerMid in _uow.Repository<CustomerMID>().GetAsQueryable()
        //                    join trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => (x.Status == TRANSACTIONSTATUS_SUCCESS ||
        //                         x.Status == TRANSACTIONSTATUS_DECLINED || x.Status == TRANSACTIONSTATUS_CANCEL) && x.TransactionType == TRANSACTIONTYPE_INT_SALES) 
        //                    on new { customerMid.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                    join customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode) on new { customerMid.CustomerCode } equals new { customer.CustomerCode } //into gcustomerMid
        //                    select new AreaChartInitViewModels
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,
        //                        TransactionStatus = trv.Status,
        //                        Amount = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });
        //        }//end if

        //        if (data != null)
        //        {
        //            //Get TransactionStatus
        //            if (ActionPeriod == "1")
        //            {
        //                var hourShellList = new List<AreaChartHourViewModels>();

        //                for (int i = 0; i < 24; i++)
        //                {
        //                    hourShellList.Add(new AreaChartHourViewModels { ykeysTransactionSuccess = 0, ykeysTransactionFail = 0, xkey = i });
        //                }

        //                switch (ActionType)
        //                {
        //                    case "2":

        //                        var query1 = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                       .Where(tags => tags.PayTime >= dateSTmte)
        //                       .Where(tags => tags.PayTime <= dateETat)
        //                       .GroupBy(x => x.PayTime.Hour)
        //                       .Select(x => new
        //                       {
        //                           Success = x.Count(y => y.TransactionStatus == TRANSACTIONSTATUS_SUCCESS),
        //                           Fail = x.Count(y => y.TransactionStatus == TRANSACTIONSTATUS_DECLINED ||
        //                                               y.TransactionStatus == TRANSACTIONSTATUS_CANCEL),
        //                           Hour = x.Key

        //                       }).ToList();
                                
        //                        foreach (var hourData in query1)
        //                        {
        //                            foreach (var shellData in hourShellList)
        //                            {
        //                                if (hourData.Hour == shellData.xkey)
        //                                {
        //                                    shellData.ykeysTransactionSuccess = CommonFunctions.doubleParse(hourData.Success);
        //                                    shellData.ykeysTransactionFail = CommonFunctions.doubleParse(hourData.Fail);
        //                                }
        //                            }
        //                        }

        //                        dailyAreaChartSales = JsonConvert.SerializeObject(hourShellList);
        //                        model.SalesAreaChartJson = dailyAreaChartSales;
        //                        break;
        //                    default:
        //                        var query3 = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                        .Where(tags => tags.PayTime >= dateSTmte)
        //                        .Where(tags => tags.PayTime <= dateETat)
        //                        .GroupBy(x => x.PayTime.Hour)
        //                        .Select(x => new
        //                        {
        //                            Success = x.Where(y => y.TransactionStatus == TRANSACTIONSTATUS_SUCCESS).Select(y => y.Amount).DefaultIfEmpty(0).FirstOrDefault(),
        //                            Fail = x.Where(y => y.TransactionStatus == TRANSACTIONSTATUS_DECLINED ||
        //                                                y.TransactionStatus == TRANSACTIONSTATUS_CANCEL).Select(y => y.Amount).DefaultIfEmpty(0).FirstOrDefault(),
        //                            Hour = x.Key

        //                        }).ToList();

        //                        foreach (var hourData in query3)
        //                        {
        //                            foreach (var shellData in hourShellList)
        //                            {
        //                                if (hourData.Hour == shellData.xkey)
        //                                {
        //                                    shellData.ykeysTransactionSuccess = Convert.ToDouble(hourData.Success);
        //                                    shellData.ykeysTransactionFail = Convert.ToDouble(hourData.Fail);
        //                                }
        //                            }
        //                        }

        //                        dailyAreaChartSales = JsonConvert.SerializeObject(hourShellList);
        //                        model.SalesAreaChartJson = dailyAreaChartSales;
        //                        break;
        //                }//end switch
        //            }
        //            else
        //            {
        //                var dateShellList = new List<AreaChartSalesViewModels>();
        //                var dateIndex = first.Date;

        //                while (dateIndex.Date <= last.Date)
        //                {
        //                    dateShellList.Add(new AreaChartSalesViewModels
        //                    {
        //                        ykeysTransactionSuccess = 0,
        //                        ykeysTransactionFail = 0,
        //                        xkeyDate = dateIndex,
        //                        xkey = dateIndex.ToString(),
        //                        xkeyYMD = dateIndex.ToString("yyyy-MM-dd"),
        //                    });
        //                    dateIndex = dateIndex.AddDays(1);
        //                }

        //                switch (ActionType)
        //                {
        //                    case "2":
        //                        var query1 = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                       .Where(tags => tags.PayTime >= dateSTmte)
        //                       .Where(tags => tags.PayTime <= dateETat)
        //                       .GroupBy(x => System.Data.Entity.DbFunctions.TruncateTime(x.PayTime))
        //                       .Select(x => new
        //                       {
        //                           Success = x.Count(y => y.TransactionStatus == TRANSACTIONSTATUS_SUCCESS),
        //                           Fail = x.Count(y => y.TransactionStatus == TRANSACTIONSTATUS_DECLINED ||
        //                                               y.TransactionStatus == TRANSACTIONSTATUS_CANCEL),
        //                           Day = x.Key

        //                       }).ToList();

        //                        foreach (var dateData in query1)
        //                        {
        //                            foreach (var shellData in dateShellList)
        //                            {
        //                                if (dateData.Day == shellData.xkeyDate)
        //                                {
        //                                    shellData.ykeysTransactionSuccess = CommonFunctions.doubleParse(dateData.Success);
        //                                    shellData.ykeysTransactionFail = CommonFunctions.doubleParse(dateData.Fail);
        //                                }
        //                            }
        //                        }
                                
        //                        dailyAreaChartSales = JsonConvert.SerializeObject(dateShellList);
        //                        model.SalesAreaChartJson = dailyAreaChartSales;
        //                        break;
        //                    default:
        //                        var query3 = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                       .Where(tags => tags.PayTime >= dateSTmte)
        //                       .Where(tags => tags.PayTime <= dateETat)
        //                       .GroupBy(x => System.Data.Entity.DbFunctions.TruncateTime(x.PayTime))
        //                       .Select(x => new
        //                       {
        //                           Success = x.Where(y => y.TransactionStatus == TRANSACTIONSTATUS_SUCCESS).Select(y => y.Amount).DefaultIfEmpty(0).FirstOrDefault(),
        //                           Fail = x.Where(y => y.TransactionStatus == TRANSACTIONSTATUS_DECLINED ||
        //                                               y.TransactionStatus == TRANSACTIONSTATUS_CANCEL).Select(y => y.Amount).DefaultIfEmpty(0).FirstOrDefault(),
        //                           Day = x.Key

        //                       }).ToList();
                                
        //                        foreach (var dateData in query3)
        //                        {
        //                            foreach (var shellData in dateShellList)
        //                            {
        //                                if (dateData.Day == shellData.xkeyDate)
        //                                {
        //                                    shellData.ykeysTransactionSuccess = Convert.ToDouble(dateData.Success);
        //                                    shellData.ykeysTransactionFail = Convert.ToDouble(dateData.Fail);
        //                                }
        //                            }
        //                        }
                                
        //                        dailyAreaChartSales = JsonConvert.SerializeObject(dateShellList);
        //                        model.SalesAreaChartJson = dailyAreaChartSales;
        //                        break;
        //                }//end switch   


        //            }//end if-else

        //        }//end if




        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }
        //    return dailyAreaChartSales;

        //}

        //public string GetAdminTransactionList(IndexViewModel model)
        //{
        //    List<AreaChartSalesViewModels> salesList = new List<AreaChartSalesViewModels>();
        //    string monthlyAreaChartSales = string.Empty;
        //    try
        //    {
        //        #region Area Chart Data
        //        model.SalesAreaChartTimeTitle = DateTime.Now.Year.ToString();

        //        DateTime first = new DateTime(DateTime.Now.Year, 1, 1) ;
        //        DateTime last = DateTime.Now;

        //        DateTime dateSTmte = first.MinusTimeEndSkipTime();
        //        DateTime dateETat = BAL.CommonFunctionsBAL.AddTimeEndSkipTime(last);
        //        string TransactionStatusSuccess = ((int)enumValue.TransactionStatus.SUCCESS).ToString();

        //        var chartData = (from trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => x.Status == TransactionStatusSuccess
        //                && x.TransactionTime >= dateSTmte
        //                && x.TransactionTime <= dateETat
        //                && x.TransactionType == TRANSACTIONTYPE_INT_SALES)
        //                group trv by trv.TransactionTime.Month into grpTrv
        //                select new YearlyChartViewModel
        //                {
        //                    Month = grpTrv.Key,
        //                    Count = grpTrv.Count(), //Dont need to show this in chart
        //                    Amount = grpTrv.Sum(y => y.Amount)
        //                }).ToList();

        //        //Populate All Empty Months, Assign Month Name

        //        List<YearlySalesChartViewModel> yscvmList = new List<YearlySalesChartViewModel>();
        //        if (chartData != null)
        //        {                    
        //            for (int i = 1; i <= 12; i++)
        //            {
        //                //harris modify
        //                YearlySalesChartViewModel yscvm = new YearlySalesChartViewModel();

        //                var row = chartData.Where(x => x.Month == i).FirstOrDefault();
        //                if (row != null)
        //                {
        //                    yscvm.xkeyYMD= DateTime.Now.Year + "-" + i.ToString().PadLeft(2, Convert.ToChar("0"));
        //                    yscvm.ykeySales = row.Amount;
        //                    yscvm.ykeyNumber = row.Count;
        //                }
        //                else
        //                {
        //                    yscvm.xkeyYMD = DateTime.Now.Year + "-" + i.ToString().PadLeft(2, Convert.ToChar("0"));
        //                    yscvm.ykeySales = 0M;
        //                    yscvm.ykeyNumber = 0;
        //                }//end if-else 

        //                yscvmList.Add(yscvm);
        //            }//end for
        //        }
        //        //yscvmList = yscvmList.OrderBy(x => x.Month).ToList();

        //        monthlyAreaChartSales = JsonConvert.SerializeObject(yscvmList);
        //        model.AdminSalesAreaChartJson = monthlyAreaChartSales;
        //        #endregion

        //        #region Data Beside Area / Bar Chart
        //        var currentMonth = DateTime.Now.Month;

        //        model.sAdminSalesAreaChartTotalSaleAmountThisYear = chartData.Sum(x => x.Amount).toStandardDecimalFormat(false);
        //        model.sAdminSalesAreaChartTotalSaleCountThisYear = chartData.Sum(x => x.Count);

        //        model.sAdminSalesAreaChartTotalSaleAmountThisMonth = chartData.Any(x => x.Month == currentMonth) ?
        //            chartData.First(x => x.Month == currentMonth).Amount.toStandardDecimalFormat(false) : 0.toStandardDecimalFormat(false);
        //        model.sAdminSalesAreaChartTotalSaleCountThisMonth = chartData.Any(x => x.Month == currentMonth) ?
        //            chartData.First(x => x.Month == currentMonth).Count : 0;

        //        //Testing Data 29-06-2018
        //        DateTime dateToday = DateTime.Now.Date ;// DateTime.Now.AddMonths(-2).AddDays(13).Date; //DateTime.Now.Date;

        //        var todayDataList = (from trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => x.Status == TransactionStatusSuccess
        //                         && x.TransactionType == TRANSACTIONTYPE_INT_SALES
        //                         && DbFunctions.TruncateTime(x.TransactionTime) == dateToday)
        //                         group trv by 1 into grpTrv
        //                         select new
        //                         {
        //                             Count = grpTrv.Count(),
        //                             Amount = grpTrv.Sum(y => y.Amount)
        //                         }).ToList();

        //        if (todayDataList == null || !todayDataList.Any())
        //        {
        //            model.sAdminSalesAreaChartTotalSaleAmountToday = 0.toStandardDecimalFormat(false);
        //            model.sAdminSalesAreaChartTotalSaleCountToday = 0;
        //        }
        //        else
        //        {
        //            model.sAdminSalesAreaChartTotalSaleAmountToday = todayDataList.First().Amount.toStandardDecimalFormat(false);
        //            model.sAdminSalesAreaChartTotalSaleCountToday = todayDataList.First().Count;
        //        }
        //        #endregion

        //        #region Bar Chart Data
        //        var iparamRepo = _uow.Repository<Param>();
        //        var todayBarChartDataList = (from trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x =>
        //                                     DbFunctions.TruncateTime(x.TransactionTime) == dateToday
        //                                     && x.TransactionType == TRANSACTIONTYPE_INT_SALES)
        //                                     group trv by trv.CardType into grpTrv
        //                                     let iparam = iparamRepo.dbSet.FirstOrDefault(p2 => grpTrv.Key == p2.ParamKey && p2.ParamCode == CommonSetting.ParamCodes.CreditCardType)
        //                                     select new
        //                                     {
        //                                         CardType = iparam == null ? grpTrv.Key : iparam.ParamDesc,
        //                                         PendingCount = grpTrv.Count(x => x.Status == TRANSACTIONSTATUS_PENDING),
        //                                         PendingAmount = grpTrv.Any(x => x.Status == TRANSACTIONSTATUS_PENDING) 
        //                                            ? grpTrv.Where(x => x.Status == TRANSACTIONSTATUS_PENDING).Sum(x => x.Amount) : 0,
        //                                         SuccessCount = grpTrv.Count(x => x.Status == TRANSACTIONSTATUS_SUCCESS),
        //                                         SuccessAmount = grpTrv.Any(x => x.Status == TRANSACTIONSTATUS_SUCCESS)
        //                                            ? grpTrv.Where(x => x.Status == TRANSACTIONSTATUS_SUCCESS).Sum(x => x.Amount) : 0,
        //                                         DeclinedCount = grpTrv.Count(x => x.Status == TRANSACTIONSTATUS_DECLINED),
        //                                         DeclinedAmount = grpTrv.Any(x => x.Status == TRANSACTIONSTATUS_DECLINED)
        //                                            ? grpTrv.Where(x => x.Status == TRANSACTIONSTATUS_DECLINED).Sum(x => x.Amount) : 0,
        //                                         CanceledCount = grpTrv.Count(x => x.Status == TRANSACTIONSTATUS_CANCEL),
        //                                         CanceledAmount = grpTrv.Any(x => x.Status == TRANSACTIONSTATUS_CANCEL)
        //                                            ? grpTrv.Where(x => x.Status == TRANSACTIONSTATUS_CANCEL).Sum(x => x.Amount) : 0
        //                                     }).OrderBy(x => x.CardType).ToList();

        //            var dataTransactionVolume = ((todayBarChartDataList
        //                                       .Where(x => x.CardType == CommonSetting.PaymentType.FPX)
        //                                       .Select(x => new
        //                                       {
        //                                           CardType = CommonSetting.PaymentType.FPX,
        //                                           x.PendingAmount,
        //                                           x.SuccessAmount,
        //                                           x.DeclinedAmount,
        //                                           x.CanceledAmount,
        //                                       }
        //                                       )).Union(todayBarChartDataList
        //                                       .Where(x => x.CardType != CommonSetting.PaymentType.FPX)
        //                                       .GroupBy(x=>x.CardType=="1")
        //                                       .Select(g => new
        //                                       {
        //                                           CardType = "Credit Card",
        //                                           PendingAmount = g.Sum(p => p.PendingAmount) ,
        //                                           SuccessAmount = g.Sum(p => p.SuccessAmount) ,
        //                                           DeclinedAmount = g.Sum(p => p.DeclinedAmount) ,
        //                                           CanceledAmount = g.Sum(p => p.CanceledAmount) ,
        //                                           //PendingAmount= g.Sum(p => p.PendingAmount) + 1000M,
        //                                           //SuccessAmount = g.Sum(p => p.SuccessAmount) + 1000M,
        //                                           //DeclinedAmount = g.Sum(p => p.DeclinedAmount) + 600M,
        //                                           //CanceledAmount = g.Sum(p => p.CanceledAmount) +500M,
        //                                       }
        //                                       ))).ToList();

        //            if (!dataTransactionVolume.Any() || !dataTransactionVolume.Any(x => x.CardType == CommonSetting.PaymentType.FPX))
        //            {
        //                dataTransactionVolume.Add(new {
        //                    CardType = CommonSetting.PaymentType.FPX,
        //                    PendingAmount = 0M,
        //                    SuccessAmount = 0M,
        //                    DeclinedAmount = 0M,
        //                    CanceledAmount = 0M
        //                });
        //            }
        //            if (!dataTransactionVolume.Any() || !dataTransactionVolume.Any(x => x.CardType == "Credit Card"))
        //            {
        //                dataTransactionVolume.Add(new
        //                {
        //                    CardType = "Credit Card",
        //                    PendingAmount = 0M,
        //                    SuccessAmount = 0M,
        //                    DeclinedAmount = 0M,
        //                    CanceledAmount = 0M
        //                });
        //            }
        //            model.sAdminSalesAreaChartTotalSaleAmountTotalToday = todayBarChartDataList
        //                                                                   .Select(x => new
        //                                                                   {
        //                                                                       Amount = x.PendingAmount +
        //                                                                       x.SuccessAmount +
        //                                                                       x.DeclinedAmount +
        //                                                                       x.CanceledAmount
        //                                                                   }
        //                                                                   ).Sum(x => x.Amount).toStandardDecimalFormat(false);

        //            model.AdminSalesBarChartJson = JsonConvert.SerializeObject(dataTransactionVolume);

        //            var dataTransactionNumber= ((todayBarChartDataList
        //                                      .Where(x => x.CardType == CommonSetting.PaymentType.FPX)
        //                                      .Select(x => new
        //                                      {
        //                                          CardType = CommonSetting.PaymentType.FPX,
        //                                          x.PendingCount,
        //                                          x.SuccessCount,
        //                                          x.DeclinedCount,
        //                                          x.CanceledCount,
        //                                      }
        //                                      )).Union(todayBarChartDataList
        //                                      .Where(x => x.CardType != CommonSetting.PaymentType.FPX)
        //                                      .GroupBy(x => x.CardType == "1")
        //                                      .Select(g => new
        //                                      {
        //                                          CardType = "Credit Card",
        //                                          PendingCount = g.Sum(p => p.PendingCount),
        //                                          SuccessCount = g.Sum(p => p.SuccessCount),
        //                                          DeclinedCount = g.Sum(p => p.DeclinedCount),
        //                                          CanceledCount = g.Sum(p => p.CanceledCount),
        //                                      }
        //                                      ))).ToList();

        //            if (!dataTransactionNumber.Any() || !dataTransactionNumber.Any(x => x.CardType == CommonSetting.PaymentType.FPX))
        //            {
        //                dataTransactionNumber.Add(new
        //                {
        //                    CardType = CommonSetting.PaymentType.FPX,
        //                    PendingCount = 0,
        //                    SuccessCount = 0,
        //                    DeclinedCount = 0,
        //                    CanceledCount = 0
        //                });
        //            }
        //            if (!dataTransactionNumber.Any() || !dataTransactionNumber.Any(x => x.CardType == "Credit Card"))
        //            {
        //                dataTransactionNumber.Add(new
        //                {
        //                    CardType = "Credit Card",
        //                    PendingCount = 0,
        //                    SuccessCount = 0,
        //                    DeclinedCount = 0,
        //                    CanceledCount = 0
        //                });
        //            }

        //            model.AdminSalesBarChartNumberJson = JsonConvert.SerializeObject(dataTransactionNumber);
                
        //        model.AdminSalesBarChartLabelList = String.Join(",",todayBarChartDataList.Select(x => x.CardType).OrderBy(x => x));
        //        #endregion
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }
        //    return monthlyAreaChartSales;

        //}
        //public string GetDonutChart(IndexViewModel model, string ActionType, string ActionPeriod)
        //{
        //    //List<AreaChartSalesViewModels> salesList = new List<AreaChartSalesViewModels>();
        //    string DonutChartJson = string.Empty;
        //    string ActionTypeTitle = string.Empty;
        //    try
        //    {
        //        switch (ActionType)
        //        {
        //            case "4":
        //                ActionTypeTitle = "Device Browser";
        //                break;
        //            case "3":
        //                ActionTypeTitle = "Web Browser";
        //                break;
        //            case "2":
        //                ActionTypeTitle = "Transactions";
        //                break;
        //            default:
        //                ActionTypeTitle = "Sales";
        //                break;
        //        }//end switch   

        //        model.SalesPieChartTimeTitle = ActionTypeTitle;

        //        DateTime first = DateTime.Now;
        //        DateTime last = DateTime.Now;
        //        switch (ActionPeriod)
        //        {
        //            case "2":
        //                first = DateTime.Today.AddDays(-1);
        //                last = first.AddDays(7);
        //                model.SalesPieChartTitle = "for this week";
        //                break;
        //            case "3":
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //                last = DateTime.Today;
        //                model.SalesPieChartTitle = "for this month";
        //                break;
        //            case "4":
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
        //                last = first.AddMonths(1).AddDays(-1);
        //                model.SalesPieChartTitle = "for last month";
        //                break;
        //            default:
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        //                last = first;
        //                model.SalesPieChartTitle ="for " + last.ToString("yyyy MMMMMMMMMMMMM dd");
        //                break;
        //        }//end switch   

        //        DateTime dateSTmte = first.MinusTimeEndSkipTime();
        //        DateTime dateETat = BAL.CommonFunctionsBAL.AddTimeEndSkipTime(last);
        //        string TransactionStatusSuccess = ((int)enumValue.TransactionStatus.SUCCESS).ToString();
              
        //        IQueryable<DonutChartInitViewModels> data = null;

        //        //check Customer
        //        if (CurrentUser.UserType == CommonSetting.UserType.Customer)
        //        {
        //            mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
        //            data = (from customerMid in _uow.Repository<CustomerMID>().GetAsQueryable(x => x.CustomerCode == mCustomerCode)
        //                    join trv in _uow.Repository<TransactionRecordVelocity>()
        //                    .GetAsQueryable(x => x.Status == TransactionStatusSuccess && x.TransactionType == TRANSACTIONTYPE_INT_SALES)
        //                    on new { customerMid.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                    //join customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode) on new { customerMid.CustomerCode } equals new { customer.CustomerCode } //into gcustomerMid
        //                    select new DonutChartInitViewModels
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,
        //                        BrowserID =trv.BrowserID,
        //                        CardType=trv.CardType,
        //                        DeviceType=trv.DeviceType,
        //                        Amount = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });
        //        }//end if

        //        //check Partner
        //        if (CurrentUser.UserType == CommonSetting.UserType.Partner)
        //        {
        //            mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
        //            data = (from customerMid in _uow.Repository<CustomerMID>().GetAsQueryable()
        //                    join trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => x.Status == TransactionStatusSuccess && x.TransactionType == TRANSACTIONTYPE_INT_SALES) 
        //                    on new { customerMid.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                    join customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode) on new { customerMid.CustomerCode } equals new { customer.CustomerCode } //into gcustomerMid
        //                    select new DonutChartInitViewModels
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,
        //                        BrowserID = trv.BrowserID,
        //                        CardType = trv.CardType,
        //                        DeviceType = trv.DeviceType,
        //                        Amount = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });
        //        }//end if         

        //        if (data!=null)
        //        {
        //            List<DonutChartViewModels> bList = new List<DonutChartViewModels>();
        //            var CardList = _servicesBAL.GetiParamWithoutCodeList(CommonSetting.ParamCodes.CreditCardType).ToList();
        //            switch (ActionType)
        //            {
        //                case "4":
        //                    var queryMobileBrowser = data.Where(x => x.DeviceType == "2")
        //                    .Where(tags => tags.PayTime >= dateSTmte)
        //                    .Where(tags => tags.PayTime <= dateETat)
        //                    .GroupBy(x => x.BrowserID)
        //                    .Select(x => new
        //                    {
        //                        BrowserID = x.Key,
        //                        Count = x.Count(),

        //                    }).ToList();
                            
        //                    if (queryMobileBrowser == null || !queryMobileBrowser.Any())
        //                    {
        //                        DonutChartViewModels b1 = new DonutChartViewModels();
        //                        b1.label = "No Data";
        //                        b1.value = 1;
        //                        bList.Add(b1);
        //                    }
        //                    else
        //                    {
        //                        foreach (var item in queryMobileBrowser)
        //                        {
        //                            DonutChartViewModels b1 = new DonutChartViewModels();
        //                            b1.label = item.BrowserID;
        //                            b1.value = item.Count;
        //                            bList.Add(b1);
        //                        }//foreach
        //                    }
        //                    DonutChartJson = JsonConvert.SerializeObject(bList);
        //                    model.SalesPieChartJson = DonutChartJson;
        //                    break;
        //                case "3":
        //                    var queryWebBrowser = data.Where(x => x.DeviceType == "1")
        //                    .Where(tags => tags.PayTime >= dateSTmte)
        //                    .Where(tags => tags.PayTime <= dateETat)
        //                    .GroupBy(x => x.BrowserID)
        //                    .Select(x => new
        //                    {
        //                        BrowserID = x.Key,
        //                        Count = x.Count(),

        //                    }).ToList();

        //                    if (queryWebBrowser == null || !queryWebBrowser.Any())
        //                    {
        //                        DonutChartViewModels b1 = new DonutChartViewModels();
        //                        b1.label = "No Data";
        //                        b1.value = 1;
        //                        bList.Add(b1);
        //                    }
        //                    else
        //                    {
        //                        foreach (var item in queryWebBrowser)
        //                        {
        //                            DonutChartViewModels b1 = new DonutChartViewModels();
        //                            b1.label = item.BrowserID;
        //                            b1.value = item.Count;
        //                            bList.Add(b1);
        //                        }//foreach
        //                    }

        //                    DonutChartJson = JsonConvert.SerializeObject(bList);
        //                    model.SalesPieChartJson = DonutChartJson;
        //                    break;
        //                case "2":
        //                    var query1 = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                    .Where(tags => tags.PayTime >= dateSTmte)
        //                    .Where(tags => tags.PayTime <= dateETat)
        //                    .GroupBy(x => x.CardType)
        //                    .Select(x => new
        //                    {
        //                        CardType = x.Key,
        //                    //DeviceType = x.DeviceType,
        //                    Count = x.Count(),

        //                    }).ToList();

        //                    if (query1 == null || !query1.Any())
        //                    {
        //                        DonutChartViewModels b1 = new DonutChartViewModels();
        //                        b1.label = "No Data";
        //                        b1.value = 1;
        //                        bList.Add(b1);
        //                    }
        //                    else
        //                    {
        //                        foreach (var item in query1)
        //                        {
        //                            DonutChartViewModels b1 = new DonutChartViewModels();
        //                            b1.label = CardList.Where(x => x.Value == item.CardType).Select(x => x.Text).DefaultIfEmpty("").FirstOrDefault();
        //                            b1.value = item.Count;
        //                            bList.Add(b1);
        //                        }//foreach
        //                    }
        //                    DonutChartJson = JsonConvert.SerializeObject(bList);
        //                    model.SalesPieChartJson = DonutChartJson;
        //                    break;
        //                default:
        //                    var query3 = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                   .Where(tags => tags.PayTime >= dateSTmte)
        //                   .Where(tags => tags.PayTime <= dateETat)
        //                   .GroupBy(x => x.CardType)
        //                   .Select(x => new
        //                   {
        //                       CardType = x.Key,
        //                       //DeviceType = x.DeviceType,
        //                       Sum = x.Sum(y => y.Amount),

        //                   }).ToList();


        //                    if (query3 == null || !query3.Any())
        //                    {
        //                        DonutChartViewModels b1 = new DonutChartViewModels();
        //                        b1.label = "No Data";
        //                        b1.value = 1;
        //                        bList.Add(b1);
        //                    }
        //                    else
        //                    {
        //                        foreach (var item in query3)
        //                        {
        //                            DonutChartViewModels b1 = new DonutChartViewModels();
        //                            b1.label = CardList.Where(x => x.Value == item.CardType).Select(x => x.Text).DefaultIfEmpty("").FirstOrDefault();
        //                            b1.value = item.Sum.doubleParse();
        //                            bList.Add(b1);
        //                        }//foreach
        //                    }
        //                    DonutChartJson = JsonConvert.SerializeObject(bList);
        //                    model.SalesPieChartJson = DonutChartJson;
        //                    break;
        //            }//end switch   
        //        }//end if
               
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }
        //    return DonutChartJson;

        //}
        //public string GetTicketSize(IndexViewModel model, string ActionType, string ActionPeriod)
        //{
        //    string DonutChartJson = string.Empty;
        //    string ActionTypeTitle = "Avg Transaction Size";
        //    model.TicketSizeTimeTitle = ActionTypeTitle;
        //    try
        //    {
        //        //switch (ActionType)
        //        //{
        //        //    case "4":
        //        //        ActionTypeTitle = "Device Browser";
        //        //        break;
        //        //    case "3":
        //        //        ActionTypeTitle = "Web Browser";
        //        //        break;
        //        //    case "2":
        //        //        ActionTypeTitle = "Transactions";
        //        //        break;
        //        //    default:
        //        //        ActionTypeTitle = "Sales";
        //        //        break;
        //        //}//end switch   


        //        DateTime first = DateTime.Now;
        //        DateTime last = DateTime.Now;
        //        switch (ActionPeriod)
        //        {
        //            case "2":
        //                first = DateTime.Today.AddDays(-1);
        //                last = first.AddDays(7);
        //                model.TicketSizeTitle = "for this week";
        //                break;
        //            case "3":
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //                last = DateTime.Today;
        //                model.TicketSizeTitle = "for this month";
        //                break;
        //            case "4":
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
        //                last = first.AddMonths(1).AddDays(-1);
        //                model.TicketSizeTitle = "for last month";
        //                break;
        //            default:
        //                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        //                last = first;
        //                model.TicketSizeTitle = "for " + last.ToString("yyyy MMMMMMMMMMMMM dd");
        //                break;
        //        }//end switch   

        //        DateTime dateSTmte = first.MinusTimeEndSkipTime();
        //        DateTime dateETat = BAL.CommonFunctionsBAL.AddTimeEndSkipTime(last);
        //        string TransactionStatusSuccess = ((int)enumValue.TransactionStatus.SUCCESS).ToString();

        //        IQueryable<DonutChartInitViewModels> data = null;

        //        //check Customer
        //        if (CurrentUser.UserType == CommonSetting.UserType.Customer)
        //        {
        //            mCustomerCode = _servicesBAL.GetCustomerCodeFromUser();
        //            data = (from customerMid in _uow.Repository<CustomerMID>().GetAsQueryable(x => x.CustomerCode == mCustomerCode)
        //                    join trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => x.Status == TransactionStatusSuccess && x.TransactionType == TRANSACTIONTYPE_INT_SALES) 
        //                    on new { customerMid.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                    //join customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode) on new { customerMid.CustomerCode } equals new { customer.CustomerCode } //into gcustomerMid
        //                    select new DonutChartInitViewModels
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,                             
        //                        Amount = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });
        //        }//end if

        //        //check Partner
        //        if (CurrentUser.UserType == CommonSetting.UserType.Partner)
        //        {
        //            mCustomerCode = _servicesBAL.GetPartnerCodeFromUser();
        //            data = (from customerMid in _uow.Repository<CustomerMID>().GetAsQueryable()
        //                    join trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => x.Status == TransactionStatusSuccess && x.TransactionType == TRANSACTIONTYPE_INT_SALES) 
        //                    on new { customerMid.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                    join customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode) on new { customerMid.CustomerCode } equals new { customer.CustomerCode } //into gcustomerMid
        //                    select new DonutChartInitViewModels
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,                             
        //                        Amount = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });
        //        }//end if     

        //        if (data != null)
        //        {
        //            var query3 = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //              .Where(tags => tags.PayTime >= dateSTmte)
        //              .Where(tags => tags.PayTime <= dateETat)
        //              .GroupBy(i => 1)
        //              .Select(g => new {
        //                  Count = g.Count(),
        //                  Average = g.Average(i => i.Amount)
        //              }).FirstOrDefault();
        //            if (query3 == null)
        //            {
        //                model.TicketSize = "No transactions within selected period.";
        //            }
        //            else
        //            {
        //                model.TicketSize = query3.Average.decimalParse().ToString("N") + " per " + query3.Count.ToString() + " Transactions.";

        //            }//end if
        //        }//end if

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }
        //    return DonutChartJson;

        //}
        #region Private Functions
        //private void TotalPaymentToday(IndexViewModel model)
        //{
        //    DateTime dateSTmte = BAL.CommonFunctionsBAL.MinusTimeEnd(DateTime.Today.Date);
        //    DateTime dateETat = BAL.CommonFunctionsBAL.AddTimeEnd(DateTime.Today.Date);
        //    var firstDayOfMonth = new DateTime(DateTime.Today.Date.Year, DateTime.Today.Date.Month, 1);
        //    DateTime dateFDOMTmte = BAL.CommonFunctionsBAL.MinusTimeEnd(firstDayOfMonth);
        //    try
        //    {
        //        string TransactionStatusSuccess = ((int)enumValue.TransactionStatus.SUCCESS).ToString();
        //        var data = (from customerMid in _uow.Repository<CustomerMID>().GetAsQueryable()
        //                    join trv in _uow.Repository<TransactionRecordVelocity>().GetAsQueryable(x => x.Status == TransactionStatusSuccess && x.TransactionType == TRANSACTIONTYPE_INT_SALES) 
        //                    on new { customerMid.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                    //from trvLOJ in gtrv.DefaultIfEmpty()
        //                    select new
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,
        //                        Payment = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });

        //        //Get Total Payment
        //        var TotalPayment = data.Where(tags => tags.CustomerCode == mCustomerCode)
        //                    .Where(tags => tags.PayTime >= dateSTmte)
        //                    .Where(tags => tags.PayTime <= dateETat)
        //                    .Select(x=>x.Payment)
        //                    .DefaultIfEmpty(0).Sum();
             
        //        model.ActionsMenuTotalPaymentTodayValue = TotalPayment.decimalParse().ToString("N");

        //        //Get Successful Transactions
        //        var TotalCount = data.Where(tags => tags.CustomerCode == mCustomerCode)
        //                    .Where(tags => tags.PayTime >= dateSTmte)
        //                    .Where(tags => tags.PayTime <= dateETat).Count();
        //        model.ActionsMenuSuccessfulTransactionsTodayValue = TotalCount.intParse().ToString("G");

        //        //Get Total Payment Month
        //        var TotalPaymentMonth = data.Where(tags => tags.CustomerCode == mCustomerCode)
        //                    .Where(tags => tags.PayTime >= dateFDOMTmte)
        //                    .Where(tags => tags.PayTime <= dateETat)
        //                    .Select(x => x.Payment)
        //                    .DefaultIfEmpty(0).Sum();
        //        model.ActionsMenuTotalPaymentCollectedThisMonthValue = TotalPaymentMonth.decimalParse().ToString("N");

      


        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }
        
        //}
        //private void TotalPaymentTodayPartner(IndexViewModel model)
        //{
        //    DateTime dateSTmte = BAL.CommonFunctionsBAL.MinusTimeEnd(DateTime.Today.Date);
        //    DateTime dateETat = BAL.CommonFunctionsBAL.AddTimeEnd(DateTime.Today.Date);
        //    var firstDayOfMonth = new DateTime(DateTime.Today.Date.Year, DateTime.Today.Date.Month, 1);
        //    DateTime dateFDOMTmte = BAL.CommonFunctionsBAL.MinusTimeEnd(firstDayOfMonth);
        //    try
        //    {
        //        string TransactionStatusSuccess = ((int)enumValue.TransactionStatus.SUCCESS).ToString();
        //        var data = (from customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode)
        //                    join customerMid in _uow.Repository<CustomerMID>().GetAsQueryable() on new { customer.CustomerCode } equals new { customerMid.CustomerCode } into gcustomerMid
        //                    from customerMidLOJ in gcustomerMid.DefaultIfEmpty()
        //                    join trv in _uow.Repository<TransactionRecordVelocity>()
        //                    .GetAsQueryable(x => x.Status == TransactionStatusSuccess && x.TransactionType == TRANSACTIONTYPE_INT_SALES) on new { customerMidLOJ.MIDCode } equals new { trv.MIDCode } //into gtrv
        //                    //from trvLOJ in gtrv.DefaultIfEmpty()
        //                    select new
        //                    {
        //                        Payment = trv.Amount,
        //                        PayTime = trv.TransactionTime,
        //                    });                         
         
        //       //Get Total Payment
        //        var TotalPayment = data //.Where(tags => tags.CustomerCode == mCustomerCode)
        //                    .Where(tags => tags.PayTime >= dateSTmte)
        //                    .Where(tags => tags.PayTime <= dateETat)
        //                    .Select(x => x.Payment)
        //                    .DefaultIfEmpty(0).Sum();
        //        model.ActionsMenuTotalPaymentTodayValue = TotalPayment.decimalParse().ToString("N");

        //        //Get Successful Transactions
        //        var TotalCount = data //.Where(tags => tags.CustomerCode == mCustomerCode)
        //                    .Where(tags => tags.PayTime >= dateSTmte)
        //                    .Where(tags => tags.PayTime <= dateETat).Count();
        //        model.ActionsMenuSuccessfulTransactionsTodayValue = TotalCount.intParse().ToString("G");

        //        //Get Successful Transactions
        //        var TotalPaymentMonth = data //.Where(tags => tags.CustomerCode == mCustomerCode)
        //                    .Where(tags => tags.PayTime >= dateFDOMTmte)
        //                    .Where(tags => tags.PayTime <= dateETat).Select(x => x.Payment)
        //                    .DefaultIfEmpty(0).Sum();
        //        model.ActionsMenuTotalPaymentCollectedThisMonthValue = TotalPaymentMonth.decimalParse().ToString("N");

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }

        //}
        //private void GetSettlement(IndexViewModel model)
        //{
        //     try
        //    {
        //       var data = (from a in _uow.Repository<FundingLedger>().GetAsQueryable(x => x.IsLedgerHidden == false)
        //                    join customerMid in _uow.Repository<CustomerMID>().GetAsQueryable() on new { a.MIDCode } equals new { customerMid.MIDCode } into gcustomerMid
        //                    from customerMidLOJ in gcustomerMid.DefaultIfEmpty()
        //                    select new
        //                    {
        //                        CustomerCode = customerMidLOJ.CustomerCode,
        //                        MIDCode=a.MIDCode,
        //                        Payment = a.AmountCurrent + a.AmountBalance,
        //                        FundingID = a.FundingID,
        //                    });

        //        //Get Previous Settlement
        //        var PreviousSettlement = data.Where(tags => tags.CustomerCode == mCustomerCode)
        //                                     .GroupBy(x => x.MIDCode)
        //                                     .SelectMany(x => x.OrderByDescending(y => y.FundingID).Skip(1).Take(1))
        //                                     .AsNoTracking().Select(x => x.Payment);//.Sum();//.Sum(x => x.Payment);

        //        model.ActionsMenuPreviousSettlementValue=0M.ToString("G");
        //        if (PreviousSettlement.ToList().Count()>0)
        //        {
        //            model.ActionsMenuPreviousSettlementValue = PreviousSettlement.Sum().decimalParse().ToString("G");
        //        }//end if   

        //        //Get Last Settlement
        //        var LastSettlement = data.Where(tags => tags.CustomerCode == mCustomerCode)

        //                                     .GroupBy(x => x.MIDCode)
        //                                     .SelectMany(x => x.OrderByDescending(y => y.FundingID).Take(1))
        //                                     .AsNoTracking().Select(x => x.Payment);//.Sum();//.Sum(x => x.Payment);
        //        model.ActionsMenuNextSettlementValue = 0M.ToString("G");
        //        if (LastSettlement.ToList().Count() > 0)
        //        {
        //            model.ActionsMenuNextSettlementValue = LastSettlement.Sum().decimalParse().ToString("G");
        //        }//end if
               
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }

        //}
        //private void GetSettlementPartner(IndexViewModel model)
        //{
        //    try
        //    {
        //        var data = (from a in _uow.Repository<FundingLedger>().GetAsQueryable(x => x.IsLedgerHidden == false)
        //                    join customerMid in _uow.Repository<CustomerMID>().GetAsQueryable() on new { a.MIDCode } equals new { customerMid.MIDCode } //into gcustomerMid
        //                    //from customerMidLOJ in gcustomerMid.DefaultIfEmpty()
        //                    join customer in _uow.Repository<Customer>().GetAsQueryable(x => x.PartnerCode == mCustomerCode) on new { customerMid.CustomerCode } equals new { customer.CustomerCode } //into gcustomerMid
        //                    //from customerMidLOJ in gcustomerMid.DefaultIfEmpty()
        //                    select new
        //                    {
        //                        CustomerCode = customerMid.CustomerCode,
        //                        MIDCode = a.MIDCode,
        //                        Payment = a.AmountCurrent + a.AmountBalance,
        //                        FundingID = a.FundingID,
        //                    });

        //        //Get Previous Settlement
        //        var PreviousSettlement = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                                     .GroupBy(x => new { x.CustomerCode, x.MIDCode })
        //                                     .SelectMany(x => x.OrderByDescending(y => y.FundingID).Skip(1).Take(1))
        //                                     .AsNoTracking()
        //                                     .Select(x => x.Payment);//.Sum();
        //        model.ActionsMenuPreviousSettlementValue = 0M.ToString("G");
        //        if (PreviousSettlement.ToList().Count() > 0)
        //        {
        //            model.ActionsMenuPreviousSettlementValue = PreviousSettlement.Sum().decimalParse().ToString("G");
        //        }//end if 

        //        //Get Last Settlement
        //        var LastSettlement = data//.Where(tags => tags.CustomerCode == mCustomerCode)
        //                                     .GroupBy(x => new { x.CustomerCode, x.MIDCode })
        //                                     .SelectMany(x => x.OrderByDescending(y => y.FundingID).Take(1))
        //                                     .AsNoTracking()
        //                                     .Select(x => x.Payment);//.Sum();
        //        model.ActionsMenuNextSettlementValue = 0M.ToString("G");
        //        if (LastSettlement.ToList().Count() > 0)
        //        {
        //            model.ActionsMenuNextSettlementValue = LastSettlement.Sum().decimalParse().ToString("G");
        //        }//end if
                

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //    }
        //    finally { }

        //}
        //private void GetAdminMerchantInfo(IndexViewModel model)
        //{
        //    var dateToday = CommonFunctionsBAL.ParseStandardDateFormat(DateTime.Now.Date, false, false);
        //    var dateThirtyDaysBefore = CommonFunctionsBAL.ParseStandardDateFormat(DateTime.Now.Date.AddDays(-30), false, false);

        //    //Harris set URL Link
        //    if (HttpRuntime.AppDomainAppId != null)
        //    {
        //        var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //        model.ApplicationStatusApprovedURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = CommonSetting.ApplicationStatus.Approve,
        //            status = "",
        //            srcDashBoardActive = "",
        //        });
        //        model.ApplicationStatusPendingOnboardingURL = urlHelper.Action("ListDashboard", "Onboarding", new
        //        {
        //            srcCompanyName = "",
        //            srcCreateDate = "",
        //            srcOnBoardingType = "",
        //            srcApplicationStatus = "",
        //            srcIsGetAllPending = "true"
        //        });
        //        model.ApplicationStatusPendingURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = CommonSetting.ApplicationStatus.Pending,
        //            status = "",
        //            srcDashBoardActive = "",
        //        });
        //        model.ApplicationStatusDeclinedURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = CommonSetting.ApplicationStatus.Decline,
        //            status = "",
        //            srcDashBoardActive = "",
        //        });
        //        model.StatusActiveURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = "",
        //            status = CommonSetting.Status.Active,
        //            srcDashBoardActive = "",
        //        });
        //        model.StatusInActiveURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = "",
        //            status = CommonSetting.Status.Inactive,
        //            srcDashBoardActive = "",
        //        });
        //        model.StatusSuspendedURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = "",
        //            status = CommonSetting.Status.Suspended,
        //            srcDashBoardActive = "",
        //        });
        //        model.StatusTerminatedURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = "",
        //            status = CommonSetting.Status.Terminated,
        //            srcDashBoardActive = "",
        //        });

        //        model.MerchantWithTransactionCountURL = urlHelper.Action("ListDashboard", "MerchantProfile", new
        //        {
        //            applicationstatus = "",
        //            status = "",
        //            srcDashBoardActive = "Active",
        //        });

        //        model.MerchantWithoutTransactionCountURL = urlHelper.Action("ListDashboard", "InactiveMerchantReport", new
        //        {
                    
        //        });

        //        model.CreditRatioReportURL = urlHelper.Action("ListTransaction", "CreditRatioReport", new CreditRatioReportViewModels
        //        {
        //            srcDateFrom = dateThirtyDaysBefore,
        //            srcDateTo = dateToday
        //        });
        //        model.DisputeRatioReportURL = urlHelper.Action("ListTransaction", "DisputeRatioReport", new DisputeRatioReportViewModels
        //        {
        //            srcDateFrom = dateThirtyDaysBefore,
        //            srcDateTo = dateToday,
        //            srcTransactionType = ((int)enumValue.TransactionType.CHARGEBACK).ToString()
        //        });
        //    }
            
        //    var customerApplicationStatusData = (from a in _uow.Repository<Customer>().GetAsQueryable()
        //                                         group a by a.ApplicationStatus into grpApplicationStatus
        //                                         select new
        //                                         {
        //                                             ApplicationStatus = grpApplicationStatus.Key,
        //                                             MerchantCount = grpApplicationStatus.Count()
        //                                         }).ToList();

           
        //    //model.ActionsMenuApplicationStatusPendingMerchantCount
        //    //    = customerApplicationStatusData.Any(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Pending)
        //    //      ? customerApplicationStatusData.First(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Pending).MerchantCount : 0;
        //    //model.ActionsMenuApplicationStatusApprovedMerchantCount
        //    //   = customerApplicationStatusData.Any(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Approve)
        //    //     ? customerApplicationStatusData.First(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Approve).MerchantCount : 0;
        //    //model.ActionsMenuApplicationStatusDeclinedMerchantCount
        //    //    = customerApplicationStatusData.Any(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Decline)
        //    //      ? customerApplicationStatusData.First(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Decline).MerchantCount : 0;
            
        //    model.ActionsMenuApplicationStatusPendingMerchantCount
        //       = (customerApplicationStatusData.Where(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Pending)
        //         .Select(x => x.MerchantCount).FirstOrDefault()).intParse();

        //    model.ActionsMenuApplicationStatusApprovedMerchantCount
        //       = (customerApplicationStatusData.Where(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Approve)
        //         .Select(x => x.MerchantCount).FirstOrDefault()).intParse();

        //    model.ActionsMenuApplicationStatusDeclinedMerchantCount
        //      = (customerApplicationStatusData.Where(x => x.ApplicationStatus == CommonSetting.ApplicationStatus.Decline)
        //        .Select(x => x.MerchantCount).FirstOrDefault()).intParse();

        //    var customerStatusData = (from a in _uow.Repository<Customer>().GetAsQueryable()
        //                              group a by a.Status into grpStatus
        //                              select new
        //                              {
        //                                  Status = grpStatus.Key,
        //                                  MerchantCount = grpStatus.Count()
        //                              }).ToList();

        //    model.ActionsMenuApplicationStatusPendingOnboardingMerchantCount = _uow.Repository<CustomerOnboarding>().GetAsQueryable(x => x.ApplicationStatus != CommonSetting.ApplicationStatus.Approve
        //                                      && x.ApplicationStatus != CommonSetting.ApplicationStatus.Decline)
        //                                      .Count();
            
        //    //Harris Get ActionsMenuActiveMerchantCount
        //    //DateTime first = DateTime.Now;
        //    //DateTime last = DateTime.Now;

        //    //first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //    //DateTime dateSTmte = first.MinusTimeEndSkipTime();
        //    //DateTime dateETat = BAL.CommonFunctionsBAL.AddTimeEndSkipTime(last);

        //    //model.ActionsMenuActiveMerchantCount = _uow.Repository<Customer>().GetAsQueryable()
        //    //                         .Where(x => x.Status == CommonSetting.Status.Active)
        //    //                         .Where(x => x.CreatedTime >= dateSTmte)
        //    //                         .Where(x => x.CreatedTime <= dateETat).Count();

        //    model.ActionsMenuActiveMerchantCount
        //      = (customerStatusData.Where(x => x.Status == CommonSetting.Status.Active)
        //        .Select(x => x.MerchantCount).FirstOrDefault()).intParse();

        //    model.ActionsMenuDeactivatedMerchantCount
        //      = (customerStatusData.Where(x => x.Status == CommonSetting.Status.Inactive)
        //        .Select(x => x.MerchantCount).FirstOrDefault()).intParse();

        //    model.ActionsMenuSuspendedMerchantCount
        //    = (customerStatusData.Where(x => x.Status == CommonSetting.Status.Suspended)
        //      .Select(x => x.MerchantCount).FirstOrDefault()).intParse();

        //    model.ActionsMenuTerminatedMerchantCount
        //   = (customerStatusData.Where(x => x.Status == CommonSetting.Status.Terminated)
        //     .Select(x => x.MerchantCount).FirstOrDefault()).intParse();


        //    //var tranasctionTypeCountData = (from a in _uow.Repository<TransactionRecord>().GetAsQueryable()
        //    //                                where a.TransactionStatus == TRANSACTIONSTATUS_INT_SUCCESS
        //    //                                group a by a.TransactionType into grpTransactionType
        //    //                                select new
        //    //                                {
        //    //                                    TransactionType = grpTransactionType.Key,
        //    //                                    TransactionCount = grpTransactionType.Count()
        //    //                                }).ToList();

        //    //decimal SalesCount = tranasctionTypeCountData.Any(x => x.TransactionType == (int)enumValue.TransactionType.SALES)
        //    //    ? tranasctionTypeCountData.First(x => x.TransactionType == (int)enumValue.TransactionType.SALES).TransactionCount : 0;

        //    //decimal RefundCount = tranasctionTypeCountData.Any(x => x.TransactionType == (int)enumValue.TransactionType.CREDITREFUND)
        //    //    ? tranasctionTypeCountData.First(x => x.TransactionType == (int)enumValue.TransactionType.CREDITREFUND).TransactionCount : 0;

        //    //decimal ChargebackCount = tranasctionTypeCountData.Any(x => x.TransactionType == (int)enumValue.TransactionType.CHARGEBACK)
        //    //    ? tranasctionTypeCountData.First(x => x.TransactionType == (int)enumValue.TransactionType.CHARGEBACK).TransactionCount : 0;

        //    int totals = 0;

        //    model.sActionsMenuRefundRatio = _creditRatioReportBAL.getData(0, 0, "", "", "", "", "", "", "", dateThirtyDaysBefore, dateToday, "", "", "", "", ref totals).CreditRatioReportTotal.sTransactionCountRatio;
        //    model.sActionsMenuChargebackRatio = _disputeRatioReportBAL.getData(0, 0, "", "", "", "", "", "", "", dateThirtyDaysBefore, dateToday, "", "", "", "", "5", ref totals).DisputeRatioReportTotal.sTransactionCountRatio;

        //    //model.sActionsMenuRefundRatio = (RefundCount / SalesCount).toStandardDecimalFormat(false);
        //    //model.sActionsMenuChargebackRatio = (ChargebackCount / SalesCount).toStandardDecimalFormat(false);

        //    var activeInactiveMerchant = _inactiveMerchantReportBAL.GetActiveInactiveMerchantForAdminDashBoard();

        //    model.ActionsMenuMerchantWithTransactionCount = 
        //        activeInactiveMerchant.Item1.Count 
        //        - activeInactiveMerchant.Item2.Count
        //        - activeInactiveMerchant.Item3.Count; //Active Merchant Count
        //    model.ActionsMenuMerchantWithoutTransactionCount = activeInactiveMerchant.Item3.Count; //Inactive Merchant Count
        //}
        #endregion
    }//end class
}//end namespace
