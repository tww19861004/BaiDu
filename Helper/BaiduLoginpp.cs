using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class BaiduLoginpp : LoginBaiduBase    
    {
        protected override string TPL
        {
            get
            {
                return "pp";
            }
        }

        public BaiduLoginpp(string userName, string password) : base(userName, password) { }
    }
}
