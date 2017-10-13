using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = true;
            var uri = new Uri("http://www.baidu.com");

            //handler.CookieContainer.SetCookies(uri, "aas=test");
            HttpClient client = new HttpClient(handler);

            var result = client.GetStringAsync(uri);
            Console.WriteLine(result.Result);
            var getCookies = handler.CookieContainer.GetCookies(uri);
            Console.WriteLine("获取到的cookie数量：" + getCookies.Count);
            Console.WriteLine("获取到的cookie：");
            for (int i = 0; i < getCookies.Count; i++)
            {
                Console.WriteLine(getCookies[i].Name + ":" + getCookies[i].Value);
            }
            Console.WriteLine("=".PadRight(50, '='));
            Console.WriteLine(handler.CookieContainer.PerDomainCapacity);


            Console.ReadKey();
        }
    }
}
