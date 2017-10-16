using Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Mothod1
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.baidu.com");
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            int i1 = response.Cookies.Count; //这个是 3
            int i2 = request.CookieContainer.Count;//这个是 2

            //Mothod2
            var uri = new Uri("http://www.baidu.com");
            string str1 = CookieHelper.GetCookies("http://www.baidu.com");
            //var contailer = CookieHelper.GetUriCookieContainer(uri);            
            HttpClientHandler handler = new HttpClientHandler() {};            
            HttpClient client = new HttpClient(handler);                      
            var result = client.GetAsync(uri).Result;

            var lst = result.Headers.GetValues("Set-Cookie").ToList();
            var lst1 = handler.CookieContainer.GetCookies(uri);

            //Mothod3
            var lst3 = CookieHelper.GetUriCookieContainer(uri);

            Console.ReadKey();
        }

        public static void WriteCookiesToDisk(string file, CookieContainer cookieJar)
        {
            using (Stream stream = File.Create(file))
            {
                try
                {
                    Console.Out.Write("Writing cookies to disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, cookieJar);
                    Console.Out.WriteLine("Done.");
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("Problem writing cookies to disk: " + e.GetType());
                }
            }
        }

        public static CookieContainer ReadCookiesFromDisk(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    Console.Out.Write("Reading cookies from disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    Console.Out.WriteLine("Done.");
                    return (CookieContainer)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Problem reading cookies from disk: " + e.GetType());
                return new CookieContainer();
            }
        }
    }
}
