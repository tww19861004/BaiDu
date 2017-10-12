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
        protected override string TPL
        {
            get
            {
                return "wise";
            }
        }

        protected override string staticpage
        {
            get
            {
                return "http://app.baidu.com/sfile/v3Jump.html";
            }
        }

        public BaiduLoginWise(string userName, string password) : base(userName, password) { }
    }
}
