using Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginTieBa
{
    public partial class Form1 : Form
    {
        WebBrowserHelper webBrowserHelper;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri("https://passport.baidu.com/v2/?login&tpl=mn&u=http%3A%2F%2Fwww.baidu.com%2F");
            webBrowserHelper = new WebBrowserHelper();
            this.textBox1.Text = "https://tieba.baidu.com/p/5334318422";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.webBrowser1.ScriptErrorsSuppressed = true;
            if (e.Url.AbsoluteUri.Contains("login"))
            {
                HtmlElement userName = webBrowserHelper.GetElement_Name(this.webBrowser1, "userName");
                HtmlElement password = webBrowserHelper.GetElement_Id(this.webBrowser1, "TANGRAM__PSP_3__password");
                HtmlElement verifyCode = webBrowserHelper.GetElement_Id(this.webBrowser1, "TANGRAM__PSP_3__verifyCode");
                HtmlElement login = webBrowserHelper.GetElement_Id(this.webBrowser1, "TANGRAM__PSP_3__submit");                
                if (userName != null)
                    webBrowserHelper.Write_value(userName, "382233701@qq.com");
                if (password != null)
                    webBrowserHelper.Write_value(password, "Tww19861004#");
                if(login!=null)
                    login.InvokeMember("click");

            }
            #region 贴吧            
            if (e.Url.AbsoluteUri.Contains("tieba"))
            {
                HtmlElement ueditor_replace = webBrowserHelper.GetElement_Name(this.webBrowser1, "ueditor_replace");
                if (ueditor_replace != null)
                {
                    ueditor_replace.SetAttribute("innerHTML", "桃夭长老竟然也是了，哎");
                    HtmlElementCollection link = this.webBrowser1.Document.GetElementsByTagName("a");
                    for (int ii = 0; ii < link.Count; ii++)
                    {
                        if (!string.IsNullOrEmpty(link[ii].GetAttribute("title"))
                            && link[ii].GetAttribute("title") == "Ctrl+Enter快捷发表")
                        {
                            link[ii].InvokeMember("click");
                        }
                    }                    
                }
                #region 关注贴吧和取消关注贴吧
                HtmlElement j_head_focus_btn = webBrowser1.Document.All["j_head_focus_btn"];
                if (j_head_focus_btn != null)
                {
                    //关注 和 取消关注
                    //j_head_focus_btn.InvokeMember("click");
                }
                #endregion
                #region 自动签到
                foreach (HtmlElement item1 in webBrowser1.Document.All)
                {
                    //if (item1 != null && item1.OuterHtml != null && item1.OuterHtml.Contains("title=\"签到\""))
                    //{
                    //    item1.InvokeMember("click");
                    //    break;
                    //}
                }
                #endregion
            }
            #endregion
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            //防止弹窗；
            e.Cancel = true;
            string url = this.webBrowser1.StatusText;
            this.webBrowser1.Url = new Uri(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri(this.textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri("http://tieba.baidu.com/i/i/my_reply");
        }
    }
}
