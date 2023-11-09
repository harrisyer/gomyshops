using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models.ViewModels
{
    public class AreaChartSalesViewModels
    {
        public string xkey { get; set; }
        public string xkeyYMD { get; set; }
        public double ykeysTransactionSuccess { get; set; }
        public double ykeysTransactionFail { get; set; }
        
        public DateTime? xkeyDate { get; set; }
    }//end class
    public class AreaChartHourViewModels
    {
        public int xkey { get; set; }
        public string xkeyYMD { get; set; }
        public double ykeysTransactionSuccess { get; set; }
        public double ykeysTransactionFail { get; set; }
    }//end class
    public class AreaChartInitViewModels
    {
        public string CustomerCode { get; set; }
        public string TransactionStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayTime { get; set; }
    }//end class
    public class DonutChartInitViewModels
    {
        public string CustomerCode { get; set; }
        public string CardType { get; set; }
        public string BrowserID { get; set; }
        public string DeviceType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayTime { get; set; }
    }//end class
    public class DonutChartViewModels
    {
        public string label { get; set; }
        public double value { get; set; }
    }//end class

    public class YearlySalesChartViewModel
    {      
        public string xkeyYMD { get; set; }       
        public decimal ykeySales { get; set; }
        public int ykeyNumber { get; set; }
    }
}//end namespace
