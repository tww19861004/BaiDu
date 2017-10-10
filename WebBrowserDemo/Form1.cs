using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WebBrowserDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri("https://passport.baidu.com/v2/?login&tpl=mn&u=http%3A%2F%2Fwww.baidu.com%2F");
        }        

        private void button1_Click(object sender, EventArgs e)
        {            
            string cookies = this.webBrowser1.Document.Cookie;
            string url = string.Format("https://tieba.baidu.com/f?ie=utf-8&kw={0}&fr=search", HttpUtility.UrlEncode(this.textBox1.Text));
            this.webBrowser1.Url = new Uri("http://tieba.baidu.com/p/5140864708");
            string str = webBrowser1.DocumentText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {                
            }
        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.webBrowser1.ScriptErrorsSuppressed = true;
            if (e.Url.AbsoluteUri.Contains("tieba"))
            {
                //在这里进行你的处理
                HtmlElement userName = webBrowser1.Document.All["ueditor_replace"];//获取百度搜索的文本框  <br>
                HtmlElement password = webBrowser1.Document.All["TANGRAM__PSP_3__password"];
                if (userName != null)
                {
                    userName.SetAttribute("value", "今天哎我真的好幸运啊，我得到英语四级了");
                }                                
            }
        }
    }
}
