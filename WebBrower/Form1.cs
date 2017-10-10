using Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrower
{
    public partial class Form1 : Form
    {
        public CookieContainer cookieContainer = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate(new Uri("https://tieba.baidu.com/index.html", UriKind.Absolute));//UriKind=UriKind.Absolute 

            var httpResult = new HttpHelper().GetHtml(
                new HttpItem()
                {
                    URL = "https://tieba.baidu.com/index.html",
                    Method = "GET",
                    Cookie = this.webBrowser1.Document.Cookie,
                    ResultCookieType = ResultCookieType.String
                });
            if (httpResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                webBrowser1.DocumentText = httpResult.Html;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri("https://passport.baidu.com/v2/?login&tpl=mn&u=http%3A%2F%2Fwww.baidu.com%2F");              
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            foreach (HtmlElement he in webBrowser1.Document.All)
            {
                if (he!=null && he.OuterHtml !=null && he.OuterHtml.Contains("userName"))
                {
                    
                }
            }
            HtmlElement userName = webBrowser1.Document.All["TANGRAM__PSP_3__userName"];//获取百度搜索的文本框  <br>
            HtmlElement password = webBrowser1.Document.All["TANGRAM__PSP_3__password"];//获取百度搜索的按钮  <br>
            HtmlElement login = webBrowser1.Document.All["TANGRAM__PSP_3__submit"];//获取百度搜索的按钮  <br>
            if (userName != null)
                userName.SetAttribute("value", "382233701@qq.com");//给百度搜索的文本框赋值  <br>
            if (password != null)
                password.SetAttribute("value", "Tww119861004#");
            if (login != null)
                login.InvokeMember("click");//调用百度搜索按钮的点击事 
        }

        private void 进入贴吧(object sender, EventArgs e)
        {
            var httpResult = new HttpHelper().GetHtml(
                new HttpItem()
                {
                    URL = "https://tieba.baidu.com/index.html",
                    Method = "GET",
                    Cookie = this.webBrowser1.Document.Cookie,
                    ResultCookieType = ResultCookieType.String
                });
            if (httpResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                webBrowser1.DocumentText = httpResult.Html;
            }
        }
    }
}
