using Helper;
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

        private LoginBaiduBase loginBaidu = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loginBaidu = new LoginBaiduBase("382233701@qq.com", "Tww19861004#");
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
            loginBaidu.Login(this.textBox1.Text.Trim());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Stream s = loginBaidu.GetValidImage();
            pictureBox1.Image = Image.FromStream(s);
            pictureBox1.Refresh();
        }
    }
}
