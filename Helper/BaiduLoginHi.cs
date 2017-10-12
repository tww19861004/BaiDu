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
    public class BaiduLoginHi : LoginBaiduBase
    {
        protected override string TPL
        {
            get
            {
                return "wise";
            }
        }

        public override string Login(string verifyCode)
        {
            return base.Login(verifyCode);
        }
    }
}
