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
        public BaiduLoginmn(string userName, string password) : base(userName, password) { }
    }
}
