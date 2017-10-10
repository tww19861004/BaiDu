using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helper
{
    public class HttpUtil
    {
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        public static string Generateplogintime()
        {
            string chars = "0123456789";

            Random randrom = new Random((int)DateTime.Now.Ticks);

            string str = "";
            for (int i = 0; i < 5; i++)
            {
                str += chars[randrom.Next(chars.Length)];
            }
            return str;
        }
        //bd__cbs__6rxaj2
        public static string GenerateCallBack()
        {
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            Random randrom = new Random((int)DateTime.Now.Ticks);

            string str = "";
            for (int i = 0; i < 6; i++)
            {
                str += chars[randrom.Next(chars.Length)];
            }
            return str;
            //return "bd__cbs__fwnq4r";
        }

        public static string GetHtml(string url, ref CookieContainer cookie)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = null;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.KeepAlive = true;
                request.Accept = "application/json, text/plain, */*";
                request.CookieContainer = cookie;
                request.AllowAutoRedirect = true;
                response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string ss = sr.ReadToEnd();
                cookie.Add(response.Cookies);                
                sr.Close();
                request.Abort();
                response.Close();
                return ss;
            }
            catch (WebException ex)
            {
                return ex.Message + ex.StackTrace; ;
            }
        }

        public static void PostRequest(string url, CookieContainer cookieContainer, string postData)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);//实例化web访问类  
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST"; //数据提交方式为POST  
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = false; // 不用需自动跳转
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.CookieContainer = cookieContainer;
                request.KeepAlive = true;
                //提交请求  
                byte[] postdatabytes = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = postdatabytes.Length;
                Stream stream;
                stream = request.GetRequestStream();
                //设置POST 数据
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();
                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                var cookieCollection = response.Cookies;//拿到bduss 说明登录成功
                //保存返回cookie  
                //取下一次GET跳转地址  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string content = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (request != null) request.Abort();
                if (response != null) response.Close();
            }
        }

        //quote the input dict values
        //note: the return result for first para no '&'
        public static string quoteParas(Dictionary<string, string> paras)
        {
            string quotedParas = "";
            bool isFirst = true;
            string val = "";
            foreach (string para in paras.Keys)
            {
                if (paras.TryGetValue(para, out val))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        quotedParas += para + "=" +  HttpUtility.UrlEncode(val);
                    }
                    else
                    {
                        quotedParas += "&" + para + "=" + HttpUtility.UrlEncode(val);
                    }
                }
                else
                {
                    break;
                }
            }

            return quotedParas;
        }

        /// <summary>
        /// 遍历CookieContainer
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }

        public static string GetJsTimeSeconds()
        {
            return (DateTime.UtcNow - DateTime.Parse("1970-01-01 0:0:0")).TotalMilliseconds.ToString("0");
        }

    }
}
