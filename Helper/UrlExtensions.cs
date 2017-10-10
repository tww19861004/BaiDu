using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helper
{
    public static class UrlExtensions
    {        
        public static string ToQueryString(this NameValueCollection nvc)
        {
            return string.Join("&", nvc.GetUrlList());
        }

        public static IEnumerable<string> GetUrlList(this NameValueCollection nvc)
        {
            foreach (var k in nvc.AllKeys)
            {
                var values = nvc.GetValues(k);
                if (values == null) { yield return k; continue; }
                for (int i = 0; i < values.Length; i++)
                {
                    yield return
                    // 'gracefully' handle formatting
                    // when there's 1 or more values
                    string.Format(
                        values.Length > 1
                            // pick your array format: k[i]=v or k[]=v, etc
                            ? "{0}[]={1}"
                            : "{0}={1}"
                        , k, HttpUtility.UrlEncode(values[i]), i);
                }
            }
        }

    }
}
