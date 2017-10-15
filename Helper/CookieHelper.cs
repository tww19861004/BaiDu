using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class CookieHelper
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref System.UInt32 pcchCookieData, int dwFlags, IntPtr lpReserved);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int InternetSetCookieEx(string lpszURL, string lpszCookieName, string lpszCookieData, int dwFlags, IntPtr dwReserved);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        internal static extern Int32 GetLastError();

        /**/
        /// <summary>
        /// 通过COM来获取Cookie数据。
        /// </summary>
        /// <param name="url">当前网址。</param>
        /// <param name="cookieName">CookieName.</param>
        /// <param name="cookieData">用于保存Cookie Data的<see cref="StringBuilder"/>实例。</param>
        /// <param name="size">Cookie大小。</param>
        /// <returns>如果成功则返回<c>true</c>,否则返回<c>false</c>。</returns>
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookie(
          string url, string cookieName,
          StringBuilder cookieData, ref int size);

        public static string GetCookies(string url)
        {
            uint datasize = 256;
            StringBuilder cookieData = new StringBuilder((int)datasize);
            if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x2000, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;


                cookieData = new StringBuilder((int)datasize);
                if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x00002000, IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }

        /**/
        /// <summary>
        /// 获取当前<see cref="Uri"/>的<see cref="CookieContainer"/>实例。
        /// </summary>
        /// <param name="uri">当前<see cref="Uri"/>地址。</param>
        /// <returns>当前<see cref="Uri"/>的<see cref="CookieContainer"/>实例。</returns>
        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;

            // 定义Cookie数据的大小。
            int datasize = 256;
            StringBuilder cookieData = new StringBuilder(datasize);

            if (!InternetGetCookie(uri.ToString(), null, cookieData,
              ref datasize))
            {
                if (datasize < 0)
                    return null;

                // 确信有足够大的空间来容纳Cookie数据。
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookie(uri.ToString(), null, cookieData,
                  ref datasize))
                    return null;
            }


            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }

    }
}
