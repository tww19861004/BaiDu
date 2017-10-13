using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class BaiduLoginmn : LoginBaiduBase
    {
        protected override string TPL
        {
            get
            {
                return "mn";
            }
        }

        protected override string staticpage
        {
            get
            {
                return "https://www.baidu.com/cache/user/html/v3Jump.html";
            }
        }

        protected override string u
        {
            get
            {
                return "https://www.baidu.com/";
            }
        }

        public BaiduLoginmn(string userName, string password) : base(userName, password) { }
    }
}
