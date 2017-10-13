using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Specialized;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;

namespace Helper
{
    public class BaiduLoginWise : LoginBaiduBase
    {
        //http://app.baidu.com/
        //百度移动开发平台
        protected override string TPL
        {
            get
            {
                return "appsearch";
            }
        }

        protected override string staticpage
        {
            get
            {
                return "http://app.baidu.com/sfile/v3Jump.html";
            }
        }

        protected override string UserAgent
        {
            get { return "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36"; }
        }

        public BaiduLoginWise(string userName, string password) : base(userName, password) { }
    }
}
