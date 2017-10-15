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
            //HttpClientHandler handler = new HttpClientHandler();
            //handler.UseCookies = true;
            //var uri = new Uri("http://www.baidu.com");

            ////handler.CookieContainer.SetCookies(uri, "aas=test");
            //HttpClient client = new HttpClient(handler);

            //var result = client.GetStringAsync(uri);
            //Console.WriteLine(result.Result);
            //var getCookies = handler.CookieContainer.GetCookies(uri);
            //Console.WriteLine("获取到的cookie数量：" + getCookies.Count);
            //Console.WriteLine("获取到的cookie：");
            //for (int i = 0; i < getCookies.Count; i++)
            //{
            //    Console.WriteLine(getCookies[i].Name + ":" + getCookies[i].Value);
            //}
            //Console.WriteLine("=".PadRight(50, '='));
            //Console.WriteLine(handler.CookieContainer.PerDomainCapacity);

            //string str1 = CookieHelper.GetCookies("http://www.baidu.com");

            //var contailer = CookieHelper.GetUriCookieContainer(new Uri("http://www.baidu.com"));

            //string TEST1 = HttpUtil.GetCookieValue(contailer, "BAIDUID");
            LoginBaiduBase loginBaidu = new LoginBaiduBase("382233701@qq.com","Tww19861004#");
            string res = loginBaidu.Login("");
            string test = loginBaidu.GetCookieValue("BAIDUID");
            string test1 = loginBaidu.GetCookieValue("FP_UID");
            Console.Write(string.Format("BAIDUID:{0}\r\nFP_UID:{1}\r\nres:{2}", test, test1, res));
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
