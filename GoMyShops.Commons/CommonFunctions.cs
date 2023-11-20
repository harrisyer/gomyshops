using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Http;
//using System.Web.Mvc;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GoMyShops.Commons
{
    public static class CommonFunctions
    {
        public static T GetValue<T>(this object obj)
        {
            if (typeof(DBNull) != obj.GetType())
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            return default(T);
        }

        public static T GetValue<T>(this object obj, object defaultValue)
        {
            if (typeof(DBNull) != obj.GetType())
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            return (T)defaultValue;
        }

       
        public static bool EqualDate(DateTime dt1, DateTime dt2)
        {
            if (dt1.CompareTo(dt2) == 0)
            {
                return true;
            }
            else { return false; }
        }//end function

        public static string stringParse(this string value)
        {
            if (value == null)
                return "";
            return value;
        }

        public static int intParse(this object value)
        {
            if (value == null)
                return 0;
            int i;
            string v;
            v = Convert.ToString(value).Trim();
            if (v.Length == 0) { return 0; }
            i = (int)Math.Round(decimal.Parse(v), 0);
            return i;
        }
        public static long longParse(this object value)
        {
            if (value == null)
                return 0;
            long i;
            string v;
            v = Convert.ToString(value).Trim();
            if (v.Length == 0) { return 0; }
            i = (long)Math.Round(decimal.Parse(v), 0);
            return i;
        }
        public static double doubleParse(this object value)
        {
            if (value == null)
                return 0;
            double i;
            string v;
            v = Convert.ToString(value).Trim();
            if (v.Length == 0) { return 0; }
            i = double.Parse(v);
            return i;
        }
        public static decimal decimalParse(this object value)
        {
            if (value == null)
                return 0;
            decimal i;
            string v;
            try
            {
                v = Convert.ToString(value).Trim();
                if (v.Length == 0) { return 0; }
                i = decimal.Parse(v);
                return i;
            }
            catch (Exception )
            {
                return 0;
            }
            finally { }
        }

        public static bool isEvenNumber(this object value)
        {
            int a = intParse(value);
            if (a % 2 == 0)
                return true;
            return false;
        }

        // Function to Check for AlphaNumeric.
        public static bool IsAlphaNumeric(this string strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }
        // Function to test for Positive Integers. 
        public static bool IsNaturalNumber(this string strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
            objNaturalPattern.IsMatch(strNumber);
        }
        // Function to test for Positive Integers with zero inclusive
        public static bool IsWholeNumber(this string strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }
        // Function to Test for Integers both Positive & Negative
        public static bool IsInteger(this string strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }
        // Function to Test for Positive Number both Integer & Real
        public static bool IsPositiveNumber(this string strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        public static bool IsNumber(this string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }
        // Function To test for Alphabets.
        public static bool IsAlpha(this string strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        public static string Left(this string param, int length)
        {
            string result = "";
            if (param.Length < length)
            {
                result = param;
            }
            else
            {
                result = param.Substring(0, length);
            }//end if-else            
            return result;
        }
        public static string Right(this string param, int length)
        {
            string result = "";
            if (param.Length < length)
            {
                result = param;
            }
            else
            {
                result = param.Substring(param.Length - length, length);
            }//end if-else  
            return result;
        }
        public static string Mid(this string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        public static string Mid(this string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }        

        /// <summary>
        /// Equal to VB.net IIF function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cond"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static T Iif<T>(bool cond, T left, T right)
        {
            return cond ? left : right;
        }

        /// <summary>
        /// Check collection is null and no records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            if (!enumerable.Any())
            {
                return true;
            }
            else
            {
                if (enumerable.FirstOrDefault().ToString() == "")
                {
                    return true;
                }
            }

            return false;
        }

        public static DateTime IsNullTimeThenNew<T>(this T? enumerable)
        {
            if (enumerable == null)
            {
                return DateTime.Now;
            }
            return Convert.ToDateTime (enumerable);
        }

        public static bool IsNullOrEmpty<T>(this T enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }            
            return false;
        }

        public static bool IsNullOrEmptyString(this string? enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            if (enumerable==string.Empty)
            {
                return true;
            }

            return false;
        }


        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static string IsNullThenEmpty(this string? obj)
        {
            if (obj == null)
            { return ""; }
            return obj;
        }

        public static string IsNullThenZero(this string obj)
        {
            if (obj == null)
            { return "0"; }
            return obj;
        }

        public static T IsNullThenDefault<T>(this T obj)
        {
            if (!IsNullOrEmpty<T>(obj))
            {
                return (T)obj;
            }
            return obj;
            
        }

        public static T IsNullThenNew<T>(this T obj)
        {
            if (!IsNullOrEmpty<T>(obj))
            {
                return obj;
            }
            var a = Activator.CreateInstance(typeof(T));
            if (a == null)
            {
                return obj;
            }
            else 
            {
                return (T)a;
            }
           
           
        }

        public static T IsNullThenNew<T>(this T obj, IHttpContextAccessor httpContextAccessor)
        {
            if (obj == null)
            {
                //object v = Activator.CreateInstance(typeof(T));
                //return (T)v;
                return (T)Activator.CreateInstance(typeof(T), httpContextAccessor);
            }
            else
            {
                return obj;
            }//end if-else
        }

        public static List<T> IsNullThenNew<T>(this IEnumerable<T> t, IHttpContextAccessor httpContextAccessor)
        {
            if (!IsNullOrEmpty<T>(t))
            {
                return t.ToList();
            }
            else
            {
                Type genericListType = typeof(List<>);
                Type listType = genericListType.MakeGenericType(t.GetType());
                object listInstance = Activator.CreateInstance(listType, httpContextAccessor);
                return (List<T>)listInstance;
            }//end if-else
        }

        public static IEnumerable<T> IsNullThenNewIEnum<T>(this IEnumerable<T> t)
        {
            if (!IsNullOrEmpty<T>(t))
            {
                return t.ToList();
            }
            else
            {
                Type genericListType = typeof(IEnumerable<>);
                Type listType = genericListType.MakeGenericType(t.GetType());
                object listInstance = Activator.CreateInstance(listType);
                return (IEnumerable<T>)listInstance;
            }//end if-else
        }

        public static bool IsAnEnumerable<T>(this T t)
        {
            if (t == null)
            {
                return false;
            }//end if
            return (t is IEnumerable<T>);
        }

        //public static SelectList NewSelectList(this List<SelectListItem> sliList)
        //{
        //    return new SelectList(sliList, "Value", "Text");
        //}

        //public static SelectList NewSelectListWithEmptyRow(this List<SelectListItem> sliList)
        //{
        //    sliList.Insert(0, new SelectListItem { Text = "", Value = "" });
        //    return new SelectList(sliList, "Value", "Text");
        //}
      
        //public static List<SelectListItem> EmptySelectListItems()
        //{
        //    return new List<SelectListItem>();
        //}

        //public static SelectList EmptySelectList()
        //{
        //    return new SelectList(new List<SelectListItem>(), "Value", "Text");
        //}

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T GetObject<T>(Dictionary<string, string> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                if (type.GetProperty(kv.Key) != null)
                    type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }

        public static T GetObject<T>(T obj, Dictionary<string, string> dict)
        {
            Type type = typeof(T);
            //var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                PropertyInfo pi = type.GetProperty(kv.Key);
                if (pi != null)
                {
                    //var propType = pi.PropertyType.Name.ToLower();
                    object propValue = new object();
                    switch (Type.GetTypeCode(pi.PropertyType))
                    {
                        //case TypeCode.Byte:
                        //case TypeCode.SByte:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                            var obj1 = kv.Value.intParse();
                            type.GetProperty(kv.Key).SetValue(obj, obj1);
                            break;
                        case TypeCode.Boolean:
                            if (kv.Value.ToUpper()=="TRUE")
                            {
                                type.GetProperty(kv.Key).SetValue(obj, true);
                            }
                            else
                            {
                                type.GetProperty(kv.Key).SetValue(obj, false);
                            }
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Single:
                            var obj2 = kv.Value.decimalParse();
                            type.GetProperty(kv.Key).SetValue(obj, obj2);
                            break;
                        case TypeCode.DateTime:
                            DateTime dateTime;
                            bool isSuccess = DateTime.TryParseExact(kv.Value,CommonSetting.DateFormatDDMYYHNNSSTT,null,System.Globalization.DateTimeStyles.None,out dateTime);
                            if (!isSuccess)
                            {
                                isSuccess = DateTime.TryParseExact(kv.Value, CommonSetting.DateFormatDDMYYHNNSSTT, null, System.Globalization.DateTimeStyles.None, out dateTime);
                                if (!isSuccess)
                                {
                                    dateTime = DateTime.ParseExact(kv.Value, CommonSetting.StandardDateTimeFormat, null);
                                }                                    
                            }//end if
                            type.GetProperty(kv.Key).SetValue(obj, dateTime);
                            break;
                        default:
                            if (pi.PropertyType == typeof(DateTime?))
                            {
                                if (kv.Value != null)
                                {
                                    DateTime dateTime1;
                                    DateTime? dateTime2=null;
                                    bool isSuccess1 = DateTime.TryParseExact(kv.Value, CommonSetting.DateFormatDDMYYHNNSSTT, null, System.Globalization.DateTimeStyles.None, out dateTime1);
                                    if (!isSuccess1)
                                    {
                                        isSuccess = DateTime.TryParseExact(kv.Value, CommonSetting.DateFormatDDMYYHNNSSTT, null, System.Globalization.DateTimeStyles.None, out dateTime);
                                        if (!isSuccess)
                                        {
                                            dateTime1 = DateTime.ParseExact(kv.Value, CommonSetting.StandardDateTimeFormat, null);
                                        }

                             
                                    }//end if
                                    dateTime2 = dateTime1;
                                    //if (dateTime1!=null)
                                    //{
                                    //    dateTime2 = dateTime1;
                                    //}
                                    type.GetProperty(kv.Key).SetValue(obj, dateTime2);
                                }
                                else
                                {
                                    type.GetProperty(kv.Key).SetValue(obj, null);
                                }
                            }
                            else
                            {
                                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
                            }
                            
                            break;
                    }//end switch
                    
                }
                    
            }
            return obj;
        }

        public static string GetGeneratedNo()
        {
            var currentDate = DateTime.Now;
            string number = string.Format("{0}{1}{2}", "", currentDate.ToString("yyMMddHHmmssff"), RandomInteger(1, 999999).ToString("000000"));
            return number;
        }

        public static string GenerateRandomString()
        {
            string randomTextUpper = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
            string randomTextLower = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
            string CharsL = "abcdefghijklmnopqrstuvwxyz";
            string CharsU = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
               
                    byte[] randomUnsignedInteger32Bytes = new byte[4];
                    rng.GetBytes(randomUnsignedInteger32Bytes);
                    int randomInt32 = BitConverter.ToInt32(randomUnsignedInteger32Bytes, 0);
                    randomInt32 = randomInt32 >= 0 ? randomInt32 : -randomInt32;

                    int i = 0;
                    string returnValue = "";
                    foreach (char c in randomInt32.ToString())
                    {
                        var d = Convert.ToString(c).intParse();


                        if (i % 3 == 0)
                        {
                            returnValue = returnValue + randomTextLower.Substring(d, 1);
                            randomTextLower = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
                        }
                        else if (i % 3 == 1)
                        {
                            returnValue = returnValue + randomTextUpper.Substring(d, 1);
                            randomTextUpper = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
                        }
                        else
                        {
                            var intS = RandomInteger(2, 9);
                            if (intS % 3 == 0)
                            {
                                returnValue = returnValue + "@";

                            }
                            else if (intS % 3 == 1)
                            {
                                returnValue = returnValue + "$";

                            }
                            else
                            {
                                returnValue = returnValue + "#";

                            }//end if-else
                        }//end if-else
                        i = i + 1;

                    }//end foreach

                    returnValue = returnValue + RandomInteger(10, 99);
                //add lower upper
                var stringCharsL = CharsL.Substring(RandomInteger(1, 25), 1);
                var stringCharsU = CharsU.Substring(RandomInteger(1, 25), 1);

                var intR = RandomInteger(2, 9);
                returnValue = returnValue.Substring(intR, returnValue.Length - intR) + stringCharsL + returnValue.Substring(0, intR);

                var intR2 = RandomInteger(2, 9);
                returnValue = returnValue.Substring(intR2, returnValue.Length - intR2) + stringCharsU + returnValue.Substring(0, intR2);

                var intR3 = RandomInteger(2, 9);
                returnValue = returnValue.Substring(intR3, returnValue.Length - intR3) + returnValue.Substring(0, intR3);


                return returnValue;
            }//end using
    
        }

        public static string GetGeneratedRandomNo()
        {
            // Random rnd = new Random();
            //var currentDate = DateTime.Now;
            //string number = string.Format("{0}{1}{2}", "", currentDate.ToString("yyMMddHHmmssff"), rnd.Next(1, 999999).ToString("000000"));
            string randomText = System.Guid.NewGuid().ToString().Replace("-", string.Empty);
            //Random rnd1 = new Random();

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4];//4 for int32
                rng.GetBytes(randomNumber);
                int value = BitConverter.ToInt32(randomNumber, 0);
                value = value >= 0 ? value : -value;
                return value.ToString() + randomText.Substring(2, RandomInteger(6, 12)).ToString();
            }



            //return randomText;
        }


        public static bool IsDictionaryEmptyValues(this Dictionary<string, string> dict)
        {
            bool istrue = false;

            istrue = dict.Select(u => u.Value != "").Count() > 0 ? false : true;
            return istrue;
        }

        public static IList<T> Clone<T>(IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }


        public static T Clone<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        //public static T Clone<T>(T source)
        //{
        //    if (!typeof(T).IsSerializable)
        //    {
        //        throw new ArgumentException("The type must be serializable.", "source");
        //    }

        //    // Don't serialize a null object, simply return the default for that object
        //    if (Object.ReferenceEquals(source, null))
        //    {
        //        return default(T);
        //    }

        //    IFormatter formatter = new BinaryFormatter();
        //    Stream stream = new MemoryStream();
        //    using (stream)
        //    {
        //        formatter.Serialize(stream, source);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return (T)formatter.Deserialize(stream);
        //    }
        //}

        public static void AddDictionary(Dictionary<string, string> dict, string Key, string Value)
        {
            Dictionary<string, string> a = new Dictionary<string, string>();

            if (dict.ContainsKey(Key))
            {
                return;
            }
            else
            {
                dict.Add(Key, Value);
            }

        }

        // Return a random integer between a min and max value.
        public static int RandomInteger(int min, int max)
        {
            RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] four_bytes = new byte[4];
                Rand.GetBytes(four_bytes);

                // Convert that into an uint.
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int)(min + (max - min) *
                (scale / (double)uint.MaxValue));
        }
              
        //For Datetime Function
        public static DateTime ParseStandardDateFormat(string sDateTime, bool isDateTime)
        {
            if (String.IsNullOrEmpty(sDateTime))
            {
                return getDefaultDate();
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

        public static DateTime getDefaultDate()
        {
            DateTime dt = new DateTime(CommonSetting.DefaultDateYear, CommonSetting.DefaultDateMonth, CommonSetting.DefaultDateDay);
            return dt;
        }


    }//end class

   

    public static class CustomExpression //<T> : Expression<T> where T : class
    {
        public static IEnumerable<T> ToEnumerable<T>(this T input)
        {
            yield return input;
        }
        public static Expression<Func<T, bool>> CreatePropertyAccessor<T>(string target, string value)
        {
            var param = Expression.Parameter(typeof(T));
            MemberExpression fieldExp = Expression.PropertyOrField(param, target);

            MemberExpression member = Expression.Property(param, target);
            ConstantExpression value1 = Expression.Constant(value);
            BinaryExpression assignExp = Expression.Equal(member, value1);

            Expression<Func<T, bool>> where = Expression.Lambda<Func<T, bool>>(assignExp, param);
            return where;
        }

        public static Expression<Func<T, bool>> AndAlso<T>(Expression<Func<T, bool>> e1, Expression<Func<T, bool>> e2)
        {
            var lambda1 = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(
            new SwapVisitor(e1.Parameters[0], e2.Parameters[0]).Visit(e1.Body),
            e2.Body), e2.Parameters);
            return lambda1;
        }

        public static IQueryable<T> IQueryable<T>(this IQueryable<T> source, string PropertyName, string TagName, string order)
        {
            var param1 = System.Linq.Expressions.Expression.Parameter(typeof(T), TagName);
            System.Linq.Expressions.Expression parent = param1;
            parent = System.Linq.Expressions.Expression.Property(parent, PropertyName);
            Expression conversion = Expression.Convert(parent, typeof(object));
            //var prop = Expression.Property(param1, PropertyName);

            ParameterExpression param = Expression.Parameter(typeof(T), TagName);
            Expression<Func<T, object>> sortExpression1 = Expression.Lambda<Func<T, object>>(
                                                                            Expression.Convert(
                                                                                Expression.Property(param, PropertyName),
                                                                                typeof(object)),
                                                                            param);

            return IQueryableNullable<T>(source, sortExpression1, order);

        }

        #region IQueryable<T>

        public static IQueryable<T> IQueryable<T>(this IQueryable<T> source,System.Linq.Expressions.Expression<Func<T, object>> sortExpression, string order)
        {
            if (order == CommonSetting.Ordering.Decending)
            {
                return source
                       .OrderByDescending(sortExpression);
            }
            else
            {
                return source
                       .OrderBy(sortExpression);
            }//end if-else
        }

        public static IQueryable<T> IQueryableNullable<T>(this IEnumerable<T> source, System.Linq.Expressions.Expression<Func<T, object>> sortExpression, string order)
        {


            if (order == CommonSetting.Ordering.Decending)
            {

                return source.ToList()
                       .OrderByDescending(sortExpression.Compile()).AsQueryable();
            }
            else
            {
                return source.ToList().OrderBy(sortExpression.Compile()).AsQueryable();
                //.OrderBy(sortExpression).ToList();
            }//end if-else
        }
       
        public static Expression<Func<T, object>> LambaExpressionDefault<T>(string PropertyName, string TagName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), TagName);
            Expression<Func<T, object>> sortExpression1 = Expression.Lambda<Func<T, object>>(
                                                                            Expression.Convert(
                                                                                Expression.Property(param, PropertyName),
                                                                                typeof(object)),
                                                                            param);
            return sortExpression1;
        }

        public static Expression<Func<T, object>> LambaExpression<T>(string PropertyName, string TagName)
        {
            var param1 = System.Linq.Expressions.Expression.Parameter(typeof(T), TagName);
            System.Linq.Expressions.Expression parent = param1;
            parent = System.Linq.Expressions.Expression.Property(parent, PropertyName);          
            var sortExpression = System.Linq.Expressions.Expression.Lambda<Func<T, object>>(parent, param1);
            return sortExpression;
        }

        #endregion

        public static System.Type propertyType<T>(string PropertyName, string TagName)
        {
            var param1 = System.Linq.Expressions.Expression.Parameter(typeof(T), TagName);
            var prop = Expression.Property(param1, PropertyName);

            return prop.Type;


        }

        public static bool isNumericType<T>(string PropertyName, string TagName)
        {
            var param1 = System.Linq.Expressions.Expression.Parameter(typeof(T), TagName);
            var prop = Expression.Property(param1, PropertyName);

            return prop.IsNumericType();

           
        }

        public static bool isNullable<T>(string PropertyName, string TagName)
        {
            var param1 = System.Linq.Expressions.Expression.Parameter(typeof(T), TagName);
            var prop = Expression.Property(param1, PropertyName);

            if (prop.Type.IsGenericType && prop.Type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                return true;
            }
            return false;            
      
        }
    }

    public class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public SwapVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
    

    public static class Extensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source, bool condition,
        Expression<Func<TSource, bool>> predicate)
            {
                if (condition)
                    return source.Where(predicate);
                else
                    return source;
            }
        

        public static IQueryable<TResult>LeftJoin<TSource, TInner, TKey, TResult>(this IQueryable<TSource> source,
                                                         IQueryable<TInner> inner, 
                                                         Func<TSource, TKey> pk, 
                                                         Func<TInner, TKey> fk, 
                                                         Func<TSource, TInner, TResult> result)
        {
            IQueryable<TResult> _result;// =  Queryable.Empty<TResult>();
 
            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      select result(s, left);
 
            return _result;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(
       this IEnumerable<TSource> source, bool condition,
       Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate.Compile()).ToList();
            else
                return source;
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> items, Predicate<T> condition, Func<T, T> replaceAction)
        {
            return items.Select(item => condition(item) ? replaceAction(item) : item);
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }

      
    }//end class

    public static class TempDataExtensions
    {
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            if(tempData!=null)
                if(tempData.Keys.Count()>0)
                    if (!tempData.ContainsKey(key) )                     
                        tempData.Add(key, json);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key)
        {
            if (!tempData.ContainsKey(key)) return default(T);

            var value = tempData[key] as string;

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class IsOptionalAttribute : Attribute
    {
        internal IsOptionalAttribute(bool isOptional)
        {
            this.IsOptional = isOptional;
        }
        public bool IsOptional { get; private set; }
    }
}//end namespace