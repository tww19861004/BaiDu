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

namespace Diagnostics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("IExplore.exe", "http://baidu.com");
            ///从注册表中读取默认浏览器可执行文件路径  
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();

            //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！  
            //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"  
            System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8), "http://tieba.baidu.com/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process ie = new System.Diagnostics.Process();
            ie.StartInfo.FileName = "IEXPLORE.EXE";
            ie.StartInfo.Arguments = @"http://tieba.baidu.com/";
            ie.Start();
        }
    }
}
