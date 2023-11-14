using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Data;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using Microsoft.Extensions.Logging;
namespace GoMyShops.Commons
{
    public class Utilites
    {
        public static string GetRandomNumber()
        {
            //string GeneratedRND = "";

            int size = 5;
            char[] chars = new char[36];
            string a;
            //a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);

            foreach(byte b in data)
            {
                result.Append(chars[b % (chars.Length-1)]);
            }
            return result.ToString();
        }

        public static string GetRandomNumber2()
        {
            int maxSize = 6;
            //int minSize = 5;
            char[] chars = new char[36];
            string a;
            //a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();

        }

        public static string GetGeneratedOrderNo2()
        {
            var currentDate = DateTime.Now;
            string RndNumber = GetRandomNumber();
            string dateString = currentDate.ToString("yyMMddHHmmss");

            int second1 = dateString[10].intParse();
            int second2 = dateString[11].intParse();

            char[] set1 = {'n','p','q','r','s','t','u','v','w','x','y','z'};
            char[] set2 = { 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            char[] set3 = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm' };
            char[] set4 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M' };

            char monthChar = new char(); 
            int currentMonth = currentDate.Month - 1;

            //check odd or even
            if (second1 % 2 != 0)
            {
                if (second2 % 2 != 0)
                    monthChar = set1[currentMonth];
                else
                    monthChar = set2[currentMonth];
            }
            else
            { 
                if (second2 % 2 != 0)
                    monthChar = set3[currentMonth];
                else
                    monthChar = set4[currentMonth];
            }

            char[] orderchar = new char[16];

            orderchar[0] = dateString[0]; //y1
            orderchar[1] = dateString[1]; //y2
            orderchar[2] = dateString[8]; //m1
            orderchar[3] = RndNumber[0]; //r1
            orderchar[4] = monthChar; //Month
            orderchar[5] = dateString[7]; //H2
            orderchar[6] = RndNumber[1]; //r2
            orderchar[7] = dateString[10]; //s1
            orderchar[8] = RndNumber[2];//r3
            orderchar[9] = dateString[4]; //d1
            orderchar[10] = dateString[9]; //m2
            orderchar[11] = dateString[6]; //H1
            orderchar[12] = RndNumber[3]; //r4
            orderchar[13] = dateString[5]; //d2
            orderchar[14] = dateString[11]; //s2
            orderchar[15] = RndNumber[4]; //r5

            string constructedString = "";
            foreach (var a in orderchar)
            {
                constructedString += a;
            }

            return constructedString;
        }

        //public static string GetGeneratedOrderNo(string prefix)
        //{
        //    prefix = prefix.ToUpper();

        //    string RndNumber = GetRandomNumber();
        //    var currentDate = DateTime.Now;
        //    string constructedString = string.Format("{0}{1}{2}", prefix, currentDate.ToString("yyMMddHHmmss") , RndNumber);

        //    return constructedString;
        //}

        public static void WriteLog(Microsoft.Extensions.Logging.ILogger log, string type, string method, string source, Exception ex = null)
        {
            if(type.ToLower() == "info")
            {
                log.LogInformation("##|{method}|{source}", method, source);          
            }
            else if(type.ToLower() == "debug")
            {
                log.LogDebug("##|{method}|{source}", method, source);
            }
            else if(type.ToLower() == "error")
            {
                string exMsg = "";
                if(ex != null)
                {
                    exMsg = String.Format("exception caught: {0} | {1} | {2} | {3}", ex.Message,
                                                Newtonsoft.Json.JsonConvert.SerializeObject(ex.InnerException),
                                                Newtonsoft.Json.JsonConvert.SerializeObject(ex.StackTrace),
                                                Newtonsoft.Json.JsonConvert.SerializeObject(ex.Source));
                }
                else
                {
                    exMsg = source;
                }

                log.LogError("##|{0}|{1}", method, exMsg);
            }
            else if(type.ToLower() == "fatal")
            {
                log.LogCritical("##|{0}|{1}", method, source);
            }
        }

        //TODO Harris Core-temp-off
        //public static string GetClientIPAddress(HttpRequest req)
        //{
        //    string originIpAddress = "";
        //    string reqHeader = req.Headers["x-forwarded-for"];

        //    if(reqHeader == null)
        //    {
        //        originIpAddress = req.UserHostAddress;
        //    }
        //    else
        //    {
        //        originIpAddress = reqHeader;
        //    }
        //    return originIpAddress;
        //}

        //TODO Harris Core-temp-off
        //public static string GetClientOriginURL(HttpRequest req)
        //{
        //    string originURL = "";
        //    var ori = req.Headers.GetValues("Referer");
        //    if(ori != null)
        //    {
        //        originURL = ori[0].ToString();
        //    }
        //    return originURL;
        //}

        public static string GetClientOriginHost(string url)
        {
            Uri uri = new Uri(url);
            return string.Format("{0}://{1}", uri.Scheme, uri.Authority);
            //string originURL = "";
            //var ori = req.Headers.GetValues("Referer");
            //if (ori != null)
            //{
            //    originURL = ori[0].ToString();
            //}
            //return originURL;
        }

        public static bool ValidateOriginURL(string originURL, string allowedURL)
        {
            bool valid = false;
            if(!string.IsNullOrEmpty(originURL) && !string.IsNullOrEmpty(allowedURL))
            {
                if(originURL.Contains(allowedURL))
                {
                    valid = true;
                }
            }
            return valid;
        }

        public static string MaskCreditCard(string creditCard)
        {
            string maskedCard = "";
            if(creditCard.Length == 15|| creditCard.Length == 16)
            {
                maskedCard = creditCard.Substring(0, 6) + "XXXXXX" + creditCard.Substring(creditCard.Length - 4, 4);
            }
            else
            {
                maskedCard = "XXXXXXXXXXXXXXXXX";
            }
            return maskedCard;
        }
        
        public static string MaskSecurityCode()
        {
            return "XXX";
        }

        public static string GenerateSignature(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public static string GenerateSignature2(string value)
        {
            Byte[] ob = System.Text.Encoding.UTF8.GetBytes(value);
            //var hash = new System.Security.Cryptography.SHA256();

            Byte[] hb;

            using (HMACSHA256 hash = new HMACSHA256())
            {
                hb = hash.ComputeHash(ob);
            }//end using
        
            string a = "";
            a =  System.Convert.ToBase64String(hb);
            a = BitConverter.ToString(hb);

            return a;
        }

        public static String GetHash(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        //TODO Harris Core-temp-off
        //public static double GetTimeOutSessionInMinutes()
        //{
        //    return Double.Parse(Utilites.GetAppSetting(CommonSetting.PaymentTimeoutMinutes));
        //}

        //public static string GenerateSignaturetest(string SecretKey)
        //{
        //    string a = "TESTING";

        //    var data = Encoding.UTF8.GetBytes(a);
        //    // key
        //    var key = Encoding.UTF8.GetBytes(SecretKey);

        //    // Create HMAC-MD5 Algorithm;
        //    var hmac = new HMACMD5(key);

        //    // Compute hash.
        //    var hashBytes = hmac.ComputeHash(data);

        //    // Convert to HEX string.
        //    return System.BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        //}

        public static string processFPXSettlement()
        {
            string csvPath = @"E:\Settlement\FPX\FPX_Transaction_Report_01-Aug-2017-30-Aug-2017.csv";
            string csvData = File.ReadAllText(csvPath);

            DataTable dt = new DataTable();

            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("Date & Time", typeof(DateTime));
            dt.Columns.Add("Txn Model", typeof(string));
            dt.Columns.Add("FPX Transaction ID", typeof(string));
            dt.Columns.Add("Exchange Order No.", typeof(string));
            dt.Columns.Add("Seller Order No.", typeof(string));
            dt.Columns.Add("Buyer Bank ID", typeof(string));
            dt.Columns.Add("Buyer Name", typeof(string));
            dt.Columns.Add("Transaction Currency", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Debit Auth Code", typeof(string));
            dt.Columns.Add("Debit Auth No.", typeof(string));

            dt.Columns.Add("Credit Auth Code", typeof(string));
            dt.Columns.Add("Credit Auth No.", typeof(string));
            dt.Columns.Add("FPX Status", typeof(string));
            dt.Columns.Add("Settlement Date", typeof(DateTime));

            int count = 0;
            foreach (string row in csvData.Split('\n'))
            {
                if(count > 0)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        //dt.Rows.Add();
                        //int i = 0;

                        //Execute a loop over the columns.
                        string convertedRowText = row.Replace("\"", "");
                        convertedRowText = convertedRowText.Replace("=","");

                        string[] columnData = convertedRowText.Split(',');
                        dt.Rows.Add(columnData[0].intParse(), columnData[1], columnData[2], columnData[3], columnData[4], columnData[5], columnData[6], columnData[7], columnData[8], columnData[9],
                            columnData[10], columnData[11], columnData[12], columnData[13], columnData[14], columnData[15]);

                        //foreach (string cell in row.Split(','))
                        //{
                        //    dt.Rows[dt.Rows.Count - 1][i] = cell;
                        //    i++;
                        //}
                    }
                }
                count++;
            }

            return "";
        }
        public static string getFilePath()
        {
            string path = @"C:\Users\terrance\Desktop\Settlement File\FPX\FPX_Transaction_Report_01-Aug-2017-30-Aug-2017.csv";
            return path;
        }

        //TODO Harris Core-temp-off
        //public static string GetAppSetting(string appSettingKey)
        //{
        //    return ConfigurationManager.AppSettings[appSettingKey];
        //}

        //Integration
        public static string PostWebRequest(string endpoint, string postData, Int16 timeoutInSec = 0)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            wc.Headers.Add("Content-Transfer-Encoding", "binary");

            string result = wc.UploadString(endpoint, postData);
            result = result.Replace("+", "%2B");
            return result;
        }

        public static string ConvertFormPostFormat(string data)
        {
            string FormPostData = "";

            try
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        if (!string.IsNullOrEmpty(FormPostData))
                        {
                            FormPostData += "&";
                        }

                        if (string.IsNullOrEmpty(item.Value))
                        {
                            FormPostData += string.Format("{0}=", item.Key);
                        }
                        else
                        {
                            //FormPostData += string.Format("{0}={1}", item.Key, item.Value);
                            FormPostData += string.Format("{0}={1}", item.Key, HttpUtility.HtmlEncode(item.Value));
                        }
                    }
                }
            }
            catch (Exception)
            {
                //action to logging error message
                throw;
            }
            return FormPostData;
        }

        public static decimal ConvertIntToDecimal(Int64 amount, int decimalPoint)
        {
            decimal result = 0;
            int iDecimalPoint = 0;

            if (decimalPoint == 0)
            {
                iDecimalPoint = 1;
            }
            else
            {
                iDecimalPoint = Convert.ToInt32("1" + iDecimalPoint.ToString("D" + decimalPoint));
            }
            result = (amount / iDecimalPoint);
            return result;
        }

        public static Int64 ConvertDecimalToInt(decimal amount, int decimalPoint)
        {
            Int64 result = 0;
            int iDecimalPoint = 0;

            if (decimalPoint == 0)
            {
                iDecimalPoint = 1;
            }
            else
            {
                iDecimalPoint = Convert.ToInt32("1" + iDecimalPoint.ToString("D" + decimalPoint.ToString()));
            }
            //result = Convert.ToInt64((amount / iDecimalPoint).ToString("f" + decimalPoint.ToString()));
            result = Convert.ToInt64(amount * iDecimalPoint);

            return result;
        }

        public static string ConvertNameValueCollectionToJSON(NameValueCollection nvc)
        {
            Dictionary<string, object> resultDictionary = new Dictionary<string, object>();

            foreach (string key in nvc.Keys)
            {
                string[] values = nvc.GetValues(key);

                if (values.Length == 1)
                {
                    resultDictionary.Add(key, values[0]);
                }
                else
                {
                    resultDictionary.Add(key, values);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(resultDictionary);
        }

        public static string GenerateOrderDescription(string prefix)
        {
            var currentDate = DateTime.Now;
            string RndNumber = GetRandomNumber();
            string dateString = currentDate.ToString("yyMMddHHmmss");

            string concatString = prefix + dateString + RndNumber;

            return concatString;
        }
    }
}
