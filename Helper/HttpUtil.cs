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

        /// <summary>
        /// 遍历CookieContainer
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        private static List<Cookie> GetAllCookies(CookieContainer cc)
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

        public static string GetCookieValue(CookieContainer cc,string cookieName)
        {
            string cookieValue = string.Empty;
            var list = GetAllCookies(cc);
            if (list!=null && list.Count>0)
            {
                var finded = list.Find(r => r.Name == cookieName);
                if (finded != null)
                    cookieValue = finded.Value;
            }
            return cookieValue;
        }
    }
}
