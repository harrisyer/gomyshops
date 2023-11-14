using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;

//using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using GoMyShops.Commons;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using Newtonsoft.Json;
//using System.Web.Routing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace GoMyShops.BAL
{
    public static class CommonFunctionsBAL
    {
        public static AppUser?  GetCurrentUser(AppUser CurrentUser,IHttpContextAccessor httpContextAccessor)
        {
            if (CurrentUser == null)
            {
                if (httpContextAccessor != null)
                {
                    if (httpContextAccessor.HttpContext != null)
                    {
                        if (httpContextAccessor.HttpContext.User != null)
                        {
                            return new AppUser(httpContextAccessor.HttpContext.User as ClaimsPrincipal);
                        }
                    }//end if
                }
            }
            
            return CurrentUser;
        }
        //private static readonly ILogger<CommonFunctionsBAL> _logger;
        public static void AssignEmptyProperty<T>(T obj)
        {
            Type type = typeof(T);
            //var newInstance = Activator.CreateInstance(type);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (pi != null)
                {
                    if (Type.GetTypeCode(pi.PropertyType)== TypeCode.String)
                    {
                        string objString = ((string)pi.GetValue(obj)).stringParse();
                        if (objString == "")
                        {
                            objString = "-";
                        }//end if
                        pi.SetValue(obj, objString);
                        //type.GetProperty(kv.Key).SetValue(obj, objString);
                    }//end if
                }//end if
          


            }//end foreach    
            //return obj;
        }

        public static void AssignEmptyProperty<T>(T obj,params string[] excludeFields)
        {
            Type type = typeof(T);
            //var newInstance = Activator.CreateInstance(type);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (pi != null)
                {
                    if (excludeFields.Contains(pi.Name))
                    {
                        continue;
                    }

                    if (Type.GetTypeCode(pi.PropertyType) == TypeCode.String)
                    {
                        string objString = ((string)pi.GetValue(obj)).stringParse();
                        if (objString == "")
                        {
                            objString = "-";
                        }//end if
                        pi.SetValue(obj, objString);
                        //type.GetProperty(kv.Key).SetValue(obj, objString);
                    }//end if
                }//end if



            }//end foreach    
            //return obj;
        }

        public static byte[] GZip(byte[] byteArray, byte[] byteCompressByte)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream lioGZip = new GZipStream(stream,
                    System.IO.Compression.CompressionMode.Compress, true))
                {
                    lioGZip.Write(byteArray, 0, byteArray.Length);
                    lioGZip.Close();
                }
                stream.Position = 0;
                byteCompressByte = new byte[stream.Length];
                stream.Read(byteCompressByte, 0, byteCompressByte.Length);
            }
            return byteCompressByte;
        }

        public static byte[] GUnZip(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (MemoryStream from = new MemoryStream(byteArray))
                {
                    using (GZipStream lioGZip = new GZipStream(from,
                    System.IO.Compression.CompressionMode.Decompress, true))
                    {
                        lioGZip.CopyTo(stream);
                        from.Close();
                        return stream.ToArray();
                    }//end using
                }//end using    
            }//end using      
        }

        //public static string encodeTo64Str(string inJSONStr, string LogName = "")
        //{
        //    log4net.ILog CustomLogger = null;
        //    if (!LogName.IsNullOrEmptyString())
        //    {

        //        //Todo Harris (Test) Modify Core
        //        var repo = log4net.LogManager.CreateRepository(
        //        Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

        //        CustomLogger = log4net.LogManager.GetLogger(repo.Name,LogName);
        //        if (inJSONStr.Length > 0)
        //        {
        //            try
        //            {
        //                //LogDebug("encodeTo64Str", inJSONStr);
        //                CustomLogger.Info("Start Encode");
        //                return Convert.ToBase64String(Encoding.UTF8.GetBytes(inJSONStr), Base64FormattingOptions.None);
        //                //CustomLogger.Info("End Encode");
        //            }
        //            catch (FormatException ex)
        //            {
        //                CustomLogger.Error("encodeTo64Str", ex);
        //                throw new Exception(">>>>>>>>>>>>>>>>>>>>" + ex.Message);
        //            }

        //        }
        //    }
        //    else
        //    {
        //        if (inJSONStr.Length > 0)
        //        {
        //            try
        //            {                
        //                return Convert.ToBase64String(Encoding.UTF8.GetBytes(inJSONStr), Base64FormattingOptions.None);                  
        //            }
        //            catch (FormatException ex)
        //            {
        //                Logger.Error("encodeTo64Str", ex);
        //                throw new Exception(">>>>>>>>>>>>>>>>>>>>" + ex.Message);
        //            }

        //        }
        //    }//end if-else
        //    return ""; 
        //}

        //public static bool ResizeLogo(IFormFile fileStream,string fileName,double scaleFactor=0)
        //{
        //    //bool isError = false;

        //    try
        //    {
        //        if (fileStream != null && fileStream.Length > 0)
        //        {
        //            var allowedFormats = new[]
        //            {
        //            ImageFormat.Jpeg,
        //            ImageFormat.Png,
        //            ImageFormat.Gif,
        //            ImageFormat.Bmp
        //            };


        //            try
        //            {
        //                var imgCheck = Image.FromStream(fileStream.OpenReadStream());
        //            }
        //            catch 
        //            {
        //                return true;
        //            }
        //            finally { }
                    


        //            using (var img = Image.FromStream(fileStream.OpenReadStream()))
        //            {
        //                if (allowedFormats.Contains(img.RawFormat))
        //                {
        //                    using (var srcImage = Image.FromStream(fileStream.OpenReadStream()))
        //                    {

        //                        //var newWidth = (int)(srcImage.Width * scaleFactor);
        //                        //var newHeight = (int)(srcImage.Height * scaleFactor);

        //                        if (System.IO.File.Exists(fileName))
        //                            System.IO.File.Delete(fileName);

        //                        using (var newImage = new Bitmap(120, 60))
        //                        using (var graphics = Graphics.FromImage(newImage))
        //                        {
        //                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 120, 60));
        //                            newImage.Save(fileName);
        //                        }
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    return true;
        //                }//end if-else
        //            }//end using



        //        }//end if
        //        return false;

                
        //    }
        //    catch 
        //    {          
        //        return true;
        //    }
        //    finally { }


           
        //}

        public static bool isValidMalaysiaMobileNo(string Country,string Mobile)
        {
            if (Mobile.stringParse() == "" || Mobile.stringParse().Length < 2)
            {
                return false;
            }

            if (Country == CommonSetting.CountryCodeMalaysia && Mobile.Substring(0, 2) != "60" && Mobile.Length != 11)
            {
                return false;
            }//end if
            return true;
        }

        public async static Task<string> RenderPartialViewToString(IWebHostEnvironment env, ICompositeViewEngine _viewEngine, ControllerContext controllerContext, string viewName, object model, bool CheckValid = true)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controllerContext.ActionDescriptor.ActionName;
            }

            using (var writer = new StringWriter())
            {
               // ViewEngineResult viewResult =
               //     _viewEngine.FindView(controllerContext, viewName, false);
                ViewEngineResult viewResult = _viewEngine.GetView(env.WebRootPath, viewName, false);

                var factory = controllerContext.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
                var tempData = factory.GetTempData(controllerContext.HttpContext);

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                ViewContext viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View,
                    viewDictionary,
                    tempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        //public static string RenderRazorViewToString(ICompositeViewEngine _viewEngine,ControllerContext controllerContext, string viewName, object model,bool CheckValid=true)
        //{
        //    controllerContext.Controller.ViewData.Model = model;
        //    var b = new ViewDataDictionary(controllerContext.Controller.ViewData);
        //    var a = new ViewDataDictionary(model);
        //    var viewData = new ViewDataDictionary();

        //    if (CheckValid)
        //    {
        //        if (!b.ModelState.IsValid)
        //        {
        //            viewData = b;
        //        }
        //        else
        //        {
        //            viewData = a;
        //        }//end if-else
        //    }
        //    else
        //    {
        //        viewData = a;
        //    }//end if-else


        //    //a.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        var ViewResult = _viewEngine.FindView(controllerContext, viewName,false);

        //        var ViewContext = new ViewContext(
        //                controllerContext, ViewResult.View, viewData, controllerContext.Controller.TempData,
        //                sw);
                
                
        //        ViewResult.View.(ViewContext, sw);
        //        ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}

        /// <summary>
        /// var view = this.View(“ViewName”);
        ///   var html = view.ToHtml();
        /// </summary>
        /// <param name="result"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string ToHtml(this ViewResult result, HttpContext httpContext)
        {
            var feature = httpContext.Features.Get<IRoutingFeature>();
            var routeData = feature.RouteData;
            var viewName = result.ViewName ?? routeData.Values["action"] as string;
            var actionContext = new ActionContext(httpContext, routeData, new ControllerActionDescriptor());
            var options = httpContext.RequestServices.GetRequiredService<IOptions<MvcViewOptions>>();
            var htmlHelperOptions = options.Value.HtmlHelperOptions;
            var viewEngineResult = result.ViewEngine?.FindView(actionContext, viewName, true) ?? options.Value.ViewEngines.Select(x => x.FindView(actionContext, viewName, true)).FirstOrDefault(x => x != null);
            var view = viewEngineResult.View;
            var builder = new StringBuilder();

            using (var output = new StringWriter(builder))
            {
                var viewContext = new ViewContext(actionContext, view, result.ViewData, result.TempData, output, htmlHelperOptions);

                view
                    .RenderAsync(viewContext)
                    .GetAwaiter()
                    .GetResult();
            }

            return builder.ToString();
        }


        public static bool YesNo(string data)
        {
            return data == CommonSetting.YesNo.Yes ? true : false;
        }

        public static string YesNoReverse(bool data)
        {
            return data ==true ? CommonSetting.YesNo.Yes : CommonSetting.YesNo.No;
        }

        public static DateTime getDefaultDate()
        {
            DateTime dt = new DateTime(CommonSetting.DefaultDateYear, CommonSetting.DefaultDateMonth, CommonSetting.DefaultDateDay);
            return dt;
        }

        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
        public static DateTime GetLastDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime lastDayInWeek = dayInWeek.Date;
            while (lastDayInWeek.DayOfWeek != firstDay)
                lastDayInWeek = lastDayInWeek.AddDays(1);

            return lastDayInWeek;
        }

        public static DateTime isDateElseDefault(string date, string format)
        {
            //string tempDate0 = "";
            string tempDate = "";
            if (date.IsNullOrEmpty())
            {
                return getDefaultDate();
            }
            if (date.Trim() == string.Empty)
            {
                return getDefaultDate();
            }
            else
            {
                // DateTime dt;

                switch (format)
                {
                    case CommonSetting.DateFormatDDMMYY:
                        //if (date.Length != 10)
                        //{
                        //    return getDefaultDate();
                        //}
                        //  30/12/2000


                        tempDate = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                        return new DateTime(Convert.ToInt32(date.Substring(6, 4)), Convert.ToInt32(date.Substring(3, 2)), Convert.ToInt32(date.Substring(0, 2)));
                    case CommonSetting.DateFormatFromDatePicker:
                        DateTime dt = DateTime.ParseExact(date, format, null);
                        tempDate = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                        return dt;


                    default:
                        return getDefaultDate();

                }//end switch   


            }//end if-else

        }

        public static DateTime isDateElseDefault(string date, string inFormat, string outFormat,string time="")
        {
            //string tempDate0 = "";
            string tempDate = "";
            if (date.IsNullOrEmpty())
            {
                return getDefaultDate();
            }
            if (date.Trim() == string.Empty)
            {
                return getDefaultDate();
            }
            else
            {
                // DateTime dt;

                switch (inFormat)
                {
                    case CommonSetting.DateFormatDDMMYY:
                        //if (date.Length != 10)
                        //{
                        //    return getDefaultDate();
                        //}
                        //  30/12/2000


                        //tempDate = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                        DateTime dtDate= new DateTime(Convert.ToInt32(date.Substring(6, 4)), Convert.ToInt32(date.Substring(3, 2)), Convert.ToInt32(date.Substring(0, 2)));
                        //if (outFormat != "")
                        //{
                        //    tempDate = Convert.ToInt32(date.Substring(6, 4)) + "-" + Convert.ToInt32(date.Substring(3, 2)) + "-" + Convert.ToInt32(date.Substring(0, 2));
                        //    dtDate = DateTime.Parse(dtDate.ToString(CommonSetting.DateFormatYYMMDD) + " " + time);
                        //    return dtDate;
                        //}
                        dtDate = DateTime.Parse(dtDate.ToString(CommonSetting.DateFormatYYMMDD) + " " + time);
                        return dtDate;
                              
                    //case CommonSetting.DateFormatDatePickerSales:
                    //    DateTime dt3 = DateTime.ParseExact(date, CommonSetting.DateFormatDatePickerSales, null);
                    //    return dt3;
                    case CommonSetting.DateFormatDDMMYYHHNN:
                        DateTime dt2 = DateTime.ParseExact(date, CommonSetting.DateFormatDDMMYYHHNN, null);
                        return dt2;

                    case CommonSetting.DateFormatFromDatePicker:
                        DateTime dt = DateTime.ParseExact(date, inFormat, null);
                        tempDate = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                        return dt;
                        

                    default:
                        DateTime dt1;
                        var isdate = DateTime.TryParse(date, out dt1);
                        if (isdate)
                        {
                            //switch (outFormat)
                            //{
                            //    case CommonSetting.DateFormatYYMMDD:
                            //        return DateTime.Parse(dt1.ToString(outFormat));
                            //    default:

                            //        break;
                            //}//end switch   
                            return DateTime.Parse(dt1.ToString(outFormat));
                             
                        }
                        return getDefaultDate();

                }//end switch   


            }//end if-else

        }

        public static string isTimeElseEmpty(string date, string format)
        {
           // string tempDate = "";
            if (date.IsNullOrEmpty())
            {
                return "";
            }
            if (date.Trim() == string.Empty)
            {
                return "";
            }
            else
            {
                // DateTime dt;

                switch (format)
                {
                    case CommonSetting.DateFormatDDMMYY:
                        return "";

                    default:
                        DateTime dt;
                        var isdate = DateTime.TryParse(date, out dt);
                        if (isdate)
                        {
                            return dt.ToString(format);
                        }

                        return "";

                }//end switch   


            }//end if-else

        }

        public static string isDateElseEmpty(string date, string format)
        {
            string tempDate = "";
            if (date.IsNullOrEmpty())
            {
                return "";
            }
            if (date.Trim() == string.Empty)
            {
                return "";
            }
            else
            {
                // DateTime dt;

                switch (format)
                {
                    case CommonSetting.DateFormatDDMMYY:
                        tempDate = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                        return tempDate;


                    default:
                        DateTime dt;
                        var isdate = DateTime.TryParse(date,out dt);
                        if (isdate)
                        {
                            return dt.ToString(format);
                        }

                        return "";

                }//end switch   


            }//end if-else

        }

        public static string isDateElseEmpty(DateTime date, string format)
        {
            //string tempDate = "";
            if (date == null)
            {
                return "";
            }
            else
            {
                if (date == getDefaultDate())
                {
                    return "";
                }

                switch (format)
                {
                    case CommonSetting.DateFormatDDMMYY:
                        return date.Day.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + date.Month.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + date.Year.ToString();
                    case CommonSetting.DateFormatDDMMMYY:
                        return date.ToString(CommonSetting.DateFormatDDMMMYY);
                        //return date.Day.ToString().PadLeft(2, Convert.ToChar("0")) + " " + date.ToString("MMMMMMMMMMMMM") + " " + date.Year.ToString();
                    case CommonSetting.DateFormatDDMMYYHHNNSS:
                        return date.Day.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + date.Month.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + date.Year.ToString()
                               + " " + date.Hour.ToString().PadLeft(2, Convert.ToChar("0")) + ":" + date.Minute.ToString().PadLeft(2, Convert.ToChar("0")) + ":" +
                               date.Second.ToString().PadLeft(2, Convert.ToChar("0"));
                    default:                       
                        return date.ToString(format);
                        //return "";
                }//end switch   
            }//end if-else

        }

        public static string isDateElseEmpty(DateTime? date, string format)
        {
            //string tempDate = "";
            if (date == null)
            {
                return "";
            }
            else
            {
                DateTime d = date.Value;
                switch (format)
                {
                    case CommonSetting.DateFormatDDMMMYY:
                        return date.Value.ToString(CommonSetting.DateFormatDDMMMYY);
                    case CommonSetting.DateFormatDDMMMYYHHNN:
                        return date.Value.ToString(CommonSetting.DateFormatDDMMMYYHHNN);
                    case CommonSetting.DateFormatCalendar:
                        return date.Value.ToString(CommonSetting.DateFormatCalendar);
                    case CommonSetting.DateFormatDDMMYY:
                        return date.Value.Day.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + date.Value.Month.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + date.Value.Year.ToString();
                    case CommonSetting.DateFormatDDMMYYHHNNSS:
                        return d.Day.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + d.Month.ToString().PadLeft(2, Convert.ToChar("0")) + "-" + d.Year.ToString()
                               //+ d.Hour.ToString().PadLeft(2, Convert.ToChar("0")) + ":" + d.Minute.ToString().PadLeft(2, Convert.ToChar("0")) + ":"
                               + " " + d.Hour.ToString().PadLeft(2, Convert.ToChar("0")) + ":" + d.Minute.ToString().PadLeft(2, Convert.ToChar("0")) + ":"
                               + d.Minute.ToString().PadLeft(2, Convert.ToChar("0"));
                    case CommonSetting.DateFormatMMMDDYYYYHHNNTT:
                        return date.Value.ToString(CommonSetting.DateFormatMMMDDYYYYHHNNTT);
                    case CommonSetting.DateFormatMMMDDYYYY:
                        return date.Value.ToString(CommonSetting.DateFormatMMMDDYYYY);
                    default:
                        return "";
                }//end switch   
            }//end if-else

        }

        public static string isDateElseEmpty(string date,string inFormat, string outFormat)
        {
            string tempDate = "";
            if (date.IsNullOrEmpty())
            {
                return "";
            }
            if (date.Trim() == string.Empty)
            {
                return "";
            }
            else
            {
                // DateTime dt;

                switch (inFormat)
                {
                    case CommonSetting.DateFormatDDMMYY:
                        tempDate = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                        return tempDate;


                    default:
                        DateTime dt;
                        var isdate = DateTime.TryParse(date, out dt);
                        if (isdate)
                        {
                            return dt.ToString(outFormat);
                        }
                        return "";

                }//end switch   


            }//end if-else

        }

        public static void isDateLowerThenEqual(ref DateTime st, ref  DateTime et)
        {
            if (st > et)
            {
                et = st;
            }

        }

        public static void isDateLowerThenEqualPlusTimeEnd(ref DateTime st, ref  DateTime et)
        {
            if (st > et)
            {
                et = st.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

        }

        /// <summary>
        /// Returns default date If sDateTime parameter is empty
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <param name="isDateTime"></param>
        /// <returns></returns>
        public static DateTime ParseStandardDateFormat(string sDateTime, bool isDateTime)
        {
            if(String.IsNullOrEmpty(sDateTime))
            {
                return CommonFunctionsBAL.getDefaultDate();
            }

            return DateTime.ParseExact(sDateTime, isDateTime ? CommonSetting.StandardDateTimeFormat : CommonSetting.StandardDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static string ParseStandardDateFormat(DateTime dateTime, bool isDateTime, bool isDetail)
        {
            if (isDetail)
            {
                return dateTime.ToString(isDateTime ? CommonSetting.StandardDetailDateTimeFormat : CommonSetting.StandardDetailDateFormat);
            }
            else
            {
                return dateTime.ToString(isDateTime ? CommonSetting.StandardDateTimeFormat : CommonSetting.StandardDateFormat);
            }
        }

        public static DateTime AddTimeEnd(DateTime dt)
        {
            return dt.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime MinusTimeEnd(DateTime dt)
        {
            return dt.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime AddTimeEndSkipTime(this DateTime dt)
        {
            return dt.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        public static DateTime MinusTimeEndSkipTime(this DateTime dt)
        {
            return dt.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
        }


        public static AlertList Success( string message, bool autoDisappear, bool dismissable = false)
        {
            return AddAlert( AlertStyles.Success, autoDisappear, message, dismissable);
        }

        private static AlertList AddAlert( string alertStyle, bool autoDisappear, string message, bool dismissable)
        {
            //var alerts = TempData.ContainsKey(Alert.TempDataKey)
            //    ? (List<Alert>)TempData[Alert.TempDataKey]
            //    : new List<Alert>();
            var alerts = new AlertList();
            alerts.alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                AutoDisappear = autoDisappear
            });


           


            return alerts;
        }

        public static string IifYesNo(bool isYes)
        {
            return CommonFunctions.Iif(isYes, "YES", "NO");
        }

        public static bool Luhn(string digits)
        {
            return digits.All(char.IsDigit) && digits.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }

        public static bool Mod10Check(string creditCardNumber)
        {
            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            //// 1.	Starting with the check digit double the value of every other digit 
            //// 2.	If doubling of a number results in a two digits number, add up
            ///   the digits to get a single digit number. This will results in eight single digit numbers                    
            //// 3. Get the sum of the digits
            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);


            //// If the final sum is divisible by 10, then the credit card number
            //   is valid. If it is not divisible by 10, the number is invalid.            
            return sumOfDigits % 10 == 0;
        }

        public static string GroupTypeName(string GroupCode)
        {

            switch (GroupCode)
            {
                case "A":
                    return "Account Manager";
                case "C":
                    return "Merchant";
                case "P":
                    return "Partner";
                default:
                    return "Admin";
            }//end switch   
        }

    }//end class

    public class MatchExpression
    {
        public List<Regex> Regexes { get; set; }

        public Action<System.Text.RegularExpressions.Match, object> Action { get; set; }
    }

    public class ClientBrowser
    {
        private static Dictionary<string, string> _versionMap = new Dictionary<string, string>{
            {"/8","1.0" },
            { "/1","1.2"},
            { "/3","1.3"},
            { "/412","2.0"},
            { "/416","2.0.2"},
            { "/417","2.0.3"},
            { "/419","2.0.4"},
            {"?","/" }
        };

        public ClientBrowser(string userAgent)
        {
            foreach (var matchItem in _matchs)
            {
                foreach (var regexItem in matchItem.Regexes)
                {
                    if (regexItem.IsMatch(userAgent))
                    {
                        var match = regexItem.Match(userAgent);

                        matchItem.Action(match, this);

                        this.Major = new Regex(@"\d*").Match(this.Version).Value;

                        return;
                    }
                }
            }
        }

        public string Major { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }


        private static void NameVersionAction(Match match, Object obj)
        {
            ClientBrowser current = obj as ClientBrowser;

            current.Name = new Regex(@"^[a-zA-Z]+", RegexOptions.IgnoreCase).Match(match.Value).Value;
            if (match.Value.Length > current.Name.Length)
            {
                current.Version = match.Value.Substring(current.Name.Length + 1);
            }
        }

        private static List<MatchExpression> _matchs = new List<MatchExpression> {
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(opera\smini)\/([\w\.-]+)",RegexOptions.IgnoreCase),// Opera Mini
                    new Regex(@"(opera\s[mobiletab]+).+version\/([\w\.-]+)",RegexOptions.IgnoreCase),// Opera Mobi/Tablet
                    new Regex(@"(opera).+version\/([\w\.]+)",RegexOptions.IgnoreCase),// Opera > 9.80
                    new Regex(@"(opera)[\/\s]+([\w\.]+)",RegexOptions.IgnoreCase)// Opera < 9.80
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(opios)[\/\s]+([\w\.]+)",RegexOptions.IgnoreCase)// Opera mini on iphone >= 8.0
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Opera Mini";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"\s(opr)\/([\w\.]+)",RegexOptions.IgnoreCase)// Opera Webkit
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Opera";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(kindle)\/([\w\.]+)",RegexOptions.IgnoreCase),// Kindle
                    new Regex(@"(lunascape|maxthon|netfront|jasmine|blazer)[\/\s]?([\w\.]+)*",RegexOptions.IgnoreCase),// Lunascape/Maxthon/Netfront/Jasmine/Blazer
                    
                    new Regex(@"(avant\s|iemobile|slim|baidu)(?:browser)?[\/\s]?([\w\.]*)",RegexOptions.IgnoreCase), // Avant/IEMobile/SlimBrowser/Baidu
                    new Regex(@"(?:ms|\()(ie)\s([\w\.]+)",RegexOptions.IgnoreCase),// Internet Explorer
                    
                    new Regex(@"(rekonq)\/([\w\.]+)*",RegexOptions.IgnoreCase),// Rekonq
                    new Regex(@"(chromium|flock|rockmelt|midori|epiphany|silk|skyfire|ovibrowser|bolt|iron|vivaldi|iridium|phantomjs)\/([\w\.-]+)",RegexOptions.IgnoreCase), // Chromium/Flock/RockMelt/Midori/Epiphany/Silk/Skyfire/Bolt/Iron/Iridium/PhantomJS
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(trident).+rv[:\s]([\w\.]+).+like\sgecko",RegexOptions.IgnoreCase)// IE11
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    current.Name = "IE";
                    current.Version = "11";
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(edge)\/((\d+)?[\w\.]+)",RegexOptions.IgnoreCase),// Microsoft Edge
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(yabrowser)\/([\w\.]+)",RegexOptions.IgnoreCase)// Yandex
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Yandex";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(comodo_dragon)\/([\w\.]+)",RegexOptions.IgnoreCase)// Comodo Dragon
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = nameAndVersion[0].Replace('_',' ');
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(micromessenger)\/([\w\.]+)",RegexOptions.IgnoreCase)// WeChat
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "WeChat";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"xiaomi\/miuibrowser\/([\w\.]+)",RegexOptions.IgnoreCase)// MIUI Browser
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "MIUI Browser";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"\swv\).+(chrome)\/([\w\.]+)",RegexOptions.IgnoreCase)// Chrome WebView
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = new Regex("(.+)").Replace(nameAndVersion[0],"$1 WebView");
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"android.+samsungbrowser\/([\w\.]+)",RegexOptions.IgnoreCase),
                    new Regex(@"android.+version\/([\w\.]+)\s+(?:mobile\s?safari|safari)*",RegexOptions.IgnoreCase)// Android Browser
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Android Browser";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(chrome|omniweb|arora|[tizenoka]{5}\s?browser)\/v?([\w\.]+)",RegexOptions.IgnoreCase),// Chrome/OmniWeb/Arora/Tizen/Nokia
                    new Regex(@"(qqbrowser)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase)// QQBrowser
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(uc\s?browser)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),
                    new Regex(@"ucweb.+(ucbrowser)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),
                    new Regex(@"juc.+(ucweb)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),// UCBrowser
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Android Browser";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(dolfin)\/([\w\.]+)",RegexOptions.IgnoreCase)// Dolphin
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Dolphin";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"((?:android.+)crmo|crios)\/([\w\.]+)",RegexOptions.IgnoreCase)// Chrome for Android/iOS
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Chrome";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@";fbav\/([\w\.]+);",RegexOptions.IgnoreCase)// Facebook App for iOS
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Facebook";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"fxios\/([\w\.-]+)",RegexOptions.IgnoreCase)// Firefox for iOS
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Firefox";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"version\/([\w\.]+).+?mobile\/\w+\s(safari)",RegexOptions.IgnoreCase)// Mobile Safari
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Mobile Safari";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"version\/([\w\.]+).+?(mobile\s?safari|safari)",RegexOptions.IgnoreCase)// Safari & Safari Mobile
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = nameAndVersion[1];
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"webkit.+?(mobile\s?safari|safari)(\/[\w\.]+)",RegexOptions.IgnoreCase)// Safari < 3.0
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = nameAndVersion[0];

                    var version = nameAndVersion[1];

                    current.Version = _versionMap.Keys.Any(m=>m==version)? _versionMap[version]:version;
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(konqueror)\/([\w\.]+)",RegexOptions.IgnoreCase),// Konqueror
                    new Regex(@"(webkit|khtml)\/([\w\.]+)",RegexOptions.IgnoreCase)
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(navigator|netscape)\/([\w\.-]+)",RegexOptions.IgnoreCase)// Netscape
                },
                Action = (Match match,Object obj) =>{
                    ClientBrowser current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Netscape";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(swiftfox)",RegexOptions.IgnoreCase),// Swiftfox
                    new Regex(@"(icedragon|iceweasel|camino|chimera|fennec|maemo\sbrowser|minimo|conkeror)[\/\s]?([\w\.\+]+)",RegexOptions.IgnoreCase),// IceDragon/Iceweasel/Camino/Chimera/Fennec/Maemo/Minimo/Conkeror
                    new Regex(@"(firefox|seamonkey|k-meleon|icecat|iceape|firebird|phoenix)\/([\w\.-]+)",RegexOptions.IgnoreCase),// Firefox/SeaMonkey/K-Meleon/IceCat/IceApe/Firebird/Phoenix
                    new Regex(@"(mozilla)\/([\w\.]+).+rv\:.+gecko\/\d+",RegexOptions.IgnoreCase),// Mozilla
                    new Regex(@"(polaris|lynx|dillo|icab|doris|amaya|w3m|netsurf|sleipnir)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),// Polaris/Lynx/Dillo/iCab/Doris/Amaya/w3m/NetSurf/Sleipnir
                    new Regex(@"(links)\s\(([\w\.]+)",RegexOptions.IgnoreCase),// Links
                    new Regex(@"(gobrowser)\/?([\w\.]+)*",RegexOptions.IgnoreCase),// GoBrowser
                    new Regex(@"(ice\s?browser)\/v?([\w\._]+)",RegexOptions.IgnoreCase),// ICE Browser
                    new Regex(@"(mosaic)[\/\s]([\w\.]+)",RegexOptions.IgnoreCase)// Mosaic
                },
                Action = NameVersionAction
            },
        };
    }

}//end namespace
