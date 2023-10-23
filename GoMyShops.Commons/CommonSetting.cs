using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoMyShops.Commons
{
    public class CommonSetting
    {
        public const string StandardDateFormat = "dd-MM-yyyy";
        public const string StandardDateTimeFormat = "dd-MM-yyyy HH:mm";
        public const string StandardDetailDateFormat = "dd MMM yyyy";
        public const string StandardDetailDateTimeFormat = "dd MMM yyyy HH:mm";

        public const string DateFormatFromDatePicker = "dd/M/yyyy";
        /// <summary>
        /// dd/MM/yy
        /// </summary>
        public const string DateFormatCalendar = "dd/MM/yy";        
        /// <summary>
        /// dd/MM/yyyy
        /// </summary>
        public const string DateFormatDDMMYY = "dd/MM/yyyy";
        public const string DateFormatDDMMMYY = "dd-MMM-yyyy";
        public const string DefaultDate = "1900-01-01";
        public const int DefaultDateYear = 1900;
        public const int DefaultDateMonth = 1;
        public const int DefaultDateDay = 1;
        public const string DateFormatYYMMDD = "yyy/MMM/dd";
        public const string DateFormatDDMMYYHHNNSS = "dd/MM/yyyy HH:mm:ss";
        public const string DateFormatDDMMYYHHNN = "dd/MM/yyyy HH:mm";
        public const string DateFormatDDMYYHNNSSTT = "dd/M/yyyy h:mm:ss tt";
        public const string DateFormatDDMMMYYHHNN = "dd MMM yyyy HH:mm";
        public const string DateFormatYYMMDDHHNNSS = "yyy/MMM/dd hh:mm:ss";
        public const string DateFormatDatePickerSales = "dd/MM/yyyy";
        public const string DateFormatMMMDDYYYYHHNNTT = "MMM dd, yyyy hhmm tt";
        public const string DateFormatMMMDDYYYY = "MMM dd, yyyy";
        public const string DateFormatYYYYMMDDHHMMTT_WITHDASH = "yyyy-MM-dd hh:mm tt";
        public const string DropDownListDashes = "-";

        public const string CustomNegativeSignWithTwoDecimalPlaces = "{0:#,##0.00;(#,##0.00);#,##0.00}";


        public struct Messages
        {
            public const string CodeExistArgs = "{0} {1} existed!";
            public const string RequiredArgs = "{0} {1} is required field!";
            public const string RequiredArgsAnd = "{0} is required field and {1}!";
        }

        public struct Ordering
        {
            public const string Accending = "asc";
            public const string Decending = "desc";            
        }
      
    }//end class
}//end namespace