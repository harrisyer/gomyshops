using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoMyShops.BAL;
using GoMyShops.Commons;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.BAL.MVCFilters;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
namespace GoMyShops.Controllers
{
    //[CustomAuthorization]
    public class HomeController : Web.Controllers.BaseController
    {
        #region Definations
        private readonly ILogger<HomeController> _logger;
        IIndexBAL _indexBAL;
        IUsersBAL _userBAL;
        IAnnouncementBAL _announcementBAL;
        public readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public HomeController(IHttpContextAccessor httpContextAccessor,IAnnouncementBAL announcementBAL, IUsersBAL userBAL, IIndexBAL indexBAL, ILogger<HomeController> logger)
        {
           
            _indexBAL = indexBAL;
            _userBAL = userBAL;
            _announcementBAL = announcementBAL;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        
        public ActionResult Index(string type)
        {
            TempData[CommonSetting.TempDataKeys.InitLoadMenu] = "Y";
            var model = new IndexViewModel(_httpContextAccessor);
            //_indexBAL.getData(model);
            model.AnnoucementType = type;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult DefaultIndex(string type)
        {
            TempData[CommonSetting.TempDataKeys.InitLoadMenu] = "Y";
            var model = new IndexViewModel(_httpContextAccessor);
            //_indexBAL.getData(model);
            //model.AnnoucementType = type;
            return View(model);
        }

        public ActionResult AdminDashboard(IndexViewModel model)
        {

            //IndexViewModel model = new IndexViewModel();
            try
            {
                //model = _announcementBAL.GetAnnouncementListwithID(AnnountcementId);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
            }
            finally { }

            return PartialView(model);
        }

        //[AllowAnonymous]
        public ActionResult PopulateAnnouncementList(string type)
        {
            AnnouncementDisplayViewModels model = new AnnouncementDisplayViewModels();
            try
            {
               
                    //model = _announcementBAL.GetAnnouncementList(CommonSetting.AnnouncementType.Internally);
                    //_userBAL.SetUserLastLoginDate();
              

            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error", ex);
            }
            finally { }

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnHeader]
        public ActionResult GetTransactionAreaChart(string ActionType, string ActionPeriod)
        {
            IndexViewModel model = new IndexViewModel(_httpContextAccessor);
            //var data =_indexBAL.GetTransactionList(model, ActionType, ActionPeriod);
            //data = model
            return Json(new { data = model, AreaChart =model.SalesAreaChartTitle,SalesAreaChartTimeTitle = model.SalesAreaChartTimeTitle });
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnHeader]
        public ActionResult GetPieChart(string ActionType, string ActionPeriod)
        {
            IndexViewModel model = new IndexViewModel(_httpContextAccessor);
            //var Donut = _indexBAL.GetDonutChart(model, ActionType, ActionPeriod);
            return Json(new { donut = model, model.SalesPieChartTitle,model.SalesPieChartTimeTitle });
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnHeader]
        public ActionResult GetTicketSize(string ActionType, string ActionPeriod)
        {
            IndexViewModel model = new IndexViewModel(_httpContextAccessor);
            //var TicketSize = _indexBAL.GetTicketSize(model, ActionType, ActionPeriod);
            return Json(new { TicketSizeTitle = model.TicketSizeTitle, TicketSizeTimeTitle = model.TicketSizeTimeTitle, TicketSize = model.TicketSize });
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnHeader]
        public ActionResult GetFirstTimeChart(string ActionType, string ActionPeriod)
        {
            IndexViewModel model = new IndexViewModel(_httpContextAccessor);
            //var data = _indexBAL.GetTransactionList(model, ActionType, ActionPeriod);
            //var Donut = _indexBAL.GetDonutChart(model, ActionType, ActionPeriod);
            //var TicketSize = _indexBAL.GetTicketSize(model, ActionType, ActionPeriod);
            return Json(new { data = model, AreaChart = model.SalesAreaChartTitle, SalesAreaChartTimeTitle = model.SalesAreaChartTimeTitle, donut= "Donut", SalesPieChartTitle = model.SalesPieChartTitle,model.SalesPieChartTimeTitle, TicketSizeTitle=model.TicketSizeTitle,model.TicketSizeTimeTitle,TicketSize=model.TicketSize });
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnHeader]
        public ActionResult GetAdminFirstTimeChart()
        {
            IndexViewModel model = new IndexViewModel(_httpContextAccessor);
            //var data = _indexBAL.GetAdminTransactionList(model);
            
            return Json(new { data = model.AdminSalesBarChartJson, dataNumber = model.AdminSalesBarChartNumberJson,
                model.AdminSalesAreaChartJson,
                model.sAdminSalesAreaChartTotalSaleAmountTotalToday,
                model.sAdminSalesAreaChartTotalSaleAmountToday,
                model.sAdminSalesAreaChartTotalSaleCountToday,
                model.sAdminSalesAreaChartTotalSaleAmountThisMonth,
                model.sAdminSalesAreaChartTotalSaleCountThisMonth,
                model.sAdminSalesAreaChartTotalSaleAmountThisYear,
                model.sAdminSalesAreaChartTotalSaleCountThisYear,                             

            });
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "contact :";

            return View();
        }

        //[AllowAnonymous]
        //public ActionResult CommentChatAnnouncementsList()
        //{
        //    var model = new CommentChatDisplayAnnouncementViewModels();
        //    model= _commentChatBAL.GetCommentDisplayAnnouncementList();
        //    return PartialView(model);
        //}

        //[AllowAnonymous]
        //public  ActionResult CommentChatDisplayList(string commentChatPageNo)
        //{
        //    var model = new CommentChatDisplayViewModels();
        //    model.ccvm = _commentChatBAL.GetCommentDisplayList(commentChatPageNo.intParse());
        //    return PartialView(model);
        //}

      
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SetComment(string commentId,string commentMessage)
        //{
        //    var model = new List<CommentChatDetailsViewModels>();
        //    if (!ModelState.IsValid)
        //    {
        //        return PartialView(model);
        //    }//end if
        //    //var model = new CommentChatDisplayViewModels();
        //    model = _commentChatBAL.SetComment(commentId, commentMessage, ModelState);

        //    if (Request.IsAjaxRequest())
        //    {
        //        return Json(new { Success = true,ListCount=model.Count(), ViewString = CommonFunctionsBAL.RenderRazorViewToString(ControllerContext, ControllerContext.RouteData.Values["action"].ToString(), model) }, JsonRequestBehavior.AllowGet);
        //    }
        //    //return PartialView(model);
        //    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        //}

    }//end class
}//end namespace