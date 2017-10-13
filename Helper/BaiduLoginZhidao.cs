using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class BaiduLoginZhidao : LoginBaiduBase
    {
        public BaiduLoginZhidao(string userName, string password) : base(userName, password) { }
        protected override string u
        {
            get
            {
                return "https://zhidao.baidu.com/search?ct=17&pn=0&tn=ikaslist&rn=10&word=2&fr=wwwt";
            }
        }

        protected override string staticpage
        {
            get
            {
                return "https://zhidao.baidu.com/static/common/https-html/v3Jump.html";
            }
        }

        protected override string TPL
        {
            get
            {
                return "ik";
            }
        }

        protected override string charset
        {
            get
            {
                return "GBK";
            }
        }
    }
}
