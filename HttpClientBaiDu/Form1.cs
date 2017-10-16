using Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpClientBaiDu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private BaiduLoginWise loginBaidu = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loginBaidu = new BaiduLoginWise("382233701@qq.com", "Tww19861004#");
                if (loginBaidu.IsNeedVerifyCode)
                {
                    Stream s = loginBaidu.GetValidImage();
                    pictureBox1.Image = Image.FromStream(s);
                    pictureBox1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.button1.Enabled = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string res = loginBaidu.Login(this.textBox1.Text.Trim());
            string test = loginBaidu.GetCookieValue("BAIDUID");
            string test1 = loginBaidu.GetCookieValue("FP_UID");
            this.label1.Text = string.Format("BAIDUID:{0}\r\nFP_UID:{1}\r\nres:{2}", test,test1,res);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Stream s = loginBaidu.GetValidImage();
            pictureBox1.Image = Image.FromStream(s);
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //生成百度id
            //从注册表中读取默认浏览器可执行文件路径  
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();

            //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！  
            //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"  
            System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8), "https://www.baidu.com/");

            string cookie = CookieHelper.GetCookies("https://www.baidu.com/");
        }
    }
}
