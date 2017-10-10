using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{

    public class ErrInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string no { get; set; }
    }

    public class Loginrecord
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> phone { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string rememberedUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string codeString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cookie { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string usernametype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string spLogin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string disable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Loginrecord loginrecord { get; set; }
    }

    public class ResultToken
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrInfo errInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }

}
