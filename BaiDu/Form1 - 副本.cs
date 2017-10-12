using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Helper;
using Entity;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace BaiDu
{
    public partial class Form1 : Form
    {
        private Regex _regex = new Regex(@"\{.*\}", RegexOptions.IgnoreCase);
        Regex _loginErrNoRegex = new Regex("^err_no=.*", RegexOptions.IgnoreCase);
        public Form1()
        {
            InitializeComponent();
        }
        HttpClient httpClient = null;
        public CookieContainer cookieContainer = null;
        private string token = string.Empty;
        private string verifyStr = string.Empty;
        private string valcode = string.Empty;
        private string pubksy = string.Empty;
        private string rsakey = string.Empty;
        private string gid = string.Empty;
        private string dv = string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            //init
            cookieContainer = new CookieContainer();
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36");

            Image img = GetValidImage();
            pictureBox1.Image = img;
            pictureBox1.Refresh();
            gid = Guid.NewGuid().ToString().Substring(1);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var ret = string.IsNullOrWhiteSpace(txtVerifyCode.Text.Trim());
            label4.Text = ret ? "请输入验证码！" : string.Empty;
            if (ret) return;
            if (!CheckValCode(txtVerifyCode.Text.Trim()))
            {
                MessageBox.Show("验证码不通过");
                Image img = GetValidImage();
                pictureBox1.Image = img;
                pictureBox1.Refresh();
                txtVerifyCode.Text = "";
                return;
            }
            try
            {
                if (!GetToken())
                {
                    MessageBox.Show("GetToken Failed");
                    return;
                }
                if (!GetPubksyAndRsakey())
                {
                    MessageBox.Show("GetPubksyAndRsakey Failed");
                    return;
                }
                Login(txtName.Text.Trim(), txtPassword.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Login(string username, string pwd)
        {
            IsLoginNeedVerifyCode();
            var pemToXml = RSAHelper.PemToXml(pubksy);
            var password = RSAHelper.RSAEncrypt(pemToXml, pwd);
            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>()
               {
                    {"apiver", "v3"},
                    {"staticpage", "https://passport.baidu.com/static/passpc-account/html/v3Jump.html"},
                    {"charset", "UTF-8"},
                    {"token", token},
                    {"tpl", "tb"},
                    {"tt", HttpUtil.GetTimeStamp()},
                    {"safeflg", "0"},
                    {"u", "https://passport.baidu.com/"},
                    {"isPhone", "false"},
                    {"detect", "1"},
                    {"quick_user", "0"},
                    {"gid", gid},
                    {"logintype", "dialogLogin"},
                    {"logLoginType", "pc_loginDialog"},
                    {"loginmerge", "true"},
                    {"splogin","rate" },
                    {"username", username},
                    {"password", password},
                    {"verifycode", valcode},
                    {"codestring", verifyStr},
                    {"mem_pass", "on"},
                    {"rsakey", rsakey},
                    {"crypttype", "12"},
                    {"ppui_logintime", HttpUtil.Generateplogintime()},
                    //{"countrycode", ""},
                    //{"fp_uid", "960338120bd__cbs__"+HttpUtil.GenerateCallBack()+"8b87eb0671ea3df2d"},
                    //{"fp_info", "960338120bd__cbs__"+HttpUtil.GenerateCallBack()+"8b87eb0671ea3df2d002~~~nxnn3QqCEgie0x2_Unn6yqz090gtoCdtRC_qqz090gtoCd0RC_unmCsVnmCsJnnsEnm3pOq0xwEgi~JIJH4zEp4gVkJAVkFnERaUirBniP4~LPJoEWJ9HdXAFiBzieJAt~4Au~6IskJI2eJnTWcutK4gtOBgugJGJW6ItK8UsianJW6ItKanTw7AE-czFW4mTpXobP4AEYF3JAXAEdJI2M4AwQJzbNJnBRJzHq6UswJAhQ4gJPXnhiJzBHJAwQ6A~W9zukXIJiYgTHJAVk8zieBnE-4zuWcAVw6grNanTLJgiec3tKazhNJEs3FiJHJIBiaRHH4UFiazVw4mLrJn6NBziiBgE-vUnsBqC0Rae0v__DUnvSUnvNUnsPq0KUEgi~JIJH4zEp4gVkJAVkFnERaUirBniP4~LPJoEWJDT9XnhRXdBwBzEn4nuxXmT9XnhRXdBwBzEn4nuxXmT86IFHBzEp4nii4UFu7nERBIFw6zTicusPaUFw6zTi9zukXIJiYgTHJAVkFIwi6dEk6AbWJDTY4dbk6AbWJGFP6dENJAVkFzh-4AukIA0Rnvr3asEYmvaimkP9ZPOov3MEUsAYhIlrZVvKsPFKg1C3kC8nAXMtlhgYdySekQyus4nkJjvvvDXur5lZwCCb~s~aCkfVFUnsKUnsAUnszUnsrnn2LqC7zCNYkl_CqzBAVO4zhd4C__nUnsmnwLYXeksnnvdUnvbnweUkGbjUnvRUnvYUnveUnvpquc93etp2TtpYl8p2x8paktx2ktY__"},
                    //{"dv", "MDExAAoALQALAxIAHgAAAF00AAwCACKKzc3Nzdoidjd5PmwtYD9gMGMzbF1tMm0bfgxlA3o5VjJXDAIAIoqenp6eib3pqOah87L_oP-v_KzzwvKt8oThk_qc5abJrcgHAgAEkZGRkQwCACKKb29vb3nFkdCe2YvKh9iH14TUi7qK1Yr8meuC5J3esdWwBwIABJGRkZEHAgAEkZGRkQ0CACaRkZnW96PirOu5-LXqteW25rmIuOe4zqvZsNav7IPngsGpyKbBpAkCADWws87Pu7u7u7u8RkdELSxLS29vYDR1O3wubyJ9InIhcS4fL3AvWTxOJ0E4exRwFVY-XzFWMwYCACiRkZGRkZGRkZGRlNfX18NSUlJXAwMDAAAAAAVRUVFTi4uLjtra2tgAEwIAJpGzs7Pbr9ur2OLN4pb_mviZt9W03bnM4oHug6zFq8-q0vyU4I3hFwIACpOTsrK37ILZgO8WAgAisMSvn7GCuom4gbiOv4a2j7eCtoO1hLGBtoW9j7yKvIS1jQQCAAaTk5GQpZMVAgAIkZGQzjjFVycBAgAGkZOTg47uBQIABJGRkZ0QAgABkQcCAASRkZGRDQIABZGRku3tDQIAHpGRkhAJXRxSFUcGSxRLG0gYR3ZGGUY2VyRXIE89WQcCAASRkZGRDQIAHpGRmZuC1pfZnsyNwJ_AkMOTzP3Nks293K_cq8S20g0CACaRkZmcvemo5qHzsv-g_6_8rPPC8q3yhOGT-pzlpsmtyIvjguyL7gkCADWws87Pu7u7u7uwbWxvBgdgYERESx9eEFcFRAlWCVkKWgU0BFsEchdlDGoTUD9bPn0VdBp9GAcCAASRkZGRCQIAJ4qIU1I_Pz8_PzDW1oLDjcqY2ZTLlMSXx5ipmcaZ74r4kfeOzaLGowwCACKKnp6enozpvfyy9afmq_Sr-6j4p5am-abQtceuyLHynfmcDAIAIopvb29vfCp-P3E2ZCVoN2g4aztkVWU6ZRN2BG0LcjFeOl8HAgAEkZGRkQkCACOGhcDBDAwMDAwTubntrOKl97b7pPur-Kj3xvap9oXwkv-W4g"},
                    {"callback", "parent.bd__pcbs__"+HttpUtil.GenerateCallBack()},
               });
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            httpContent.Headers.Add("KeepAlive", "true");

            HttpResponseMessage response = httpClient.PostAsync(new Uri("https://passport.baidu.com/v2/api/?login"), httpContent).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                //var cookies = HttpHelper.GetAllCookies(cookieContainer);
                //return HttpHelper.GetAllCookies(cookieContainer).FirstOrDefault(c => c.Name.Equals("BDUSS")) != null;                
                int i1 = result.IndexOf("err_no=");
                int i2 = result.IndexOf("&callback=");
                string errno = result.Substring(i1, i2 - i1).Replace("err_no=", "");
                switch (errno)
                {
                    case "-1":
                        this.label4.Text = "系统错误,请您稍后再试";
                        break;
                    case "2":
                        this.label4.Text = "您输入的帐号不存在";
                        break;
                    case "4":
                        this.label4.Text = "您输入的帐号或密码有误 field : password";
                        break;
                    case "6":
                        this.label4.Text = "您输入的验证码有误 field : verifyCode";
                        break;
                    case "7":
                        this.label4.Text = "密码错误";
                        break;
                    case "257":
                        this.label4.Text = "需要填入验证码";
                        break;
                    default:
                        this.label4.Text = "errno:" + errno;
                        break;
                }
            }
            //httpClient.Dispose();
        }

        //判断登陆是否需要验证码
        private bool IsLoginNeedVerifyCode()
        {
            bool res = false;
            var nvc = new NameValueCollection
                {
                    {"token", token},
                    {"tpl", "tb"},
                    {"apiver", "v3"},
                    {"tt", HttpUtil.GetTimeStamp()},
                    {"sub_source", "leadsetpwd"},
                    {"username", "382233701@qq.com"},                    
                    {"callback", "bd__cbs__"+HttpUtil.GenerateCallBack()},
                };
            HttpResponseMessage response = httpClient.GetAsync(new Uri("https://passport.baidu.com/v2/api/?logincheck&" + nvc.ToQueryString())).Result;
            response.EnsureSuccessStatusCode();
            String Result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {

            }
            return res;
        }

        private bool GetToken()
        {
            bool res = false;
            var nvc = new NameValueCollection
                {
                    {"tpl", "tb"},
                    {"apiver", "v3"},
                    {"tt", HttpUtil.GetTimeStamp()},
                    {"class", "login"},
                    {"gid", gid},
                    {"logintype", "dialogLogin"},
                    {"callback", "bd__cbs__"+HttpUtil.GenerateCallBack()},
                };
            HttpResponseMessage response = httpClient.GetAsync(new Uri("https://passport.baidu.com/v2/api/?getapi&" + nvc.ToQueryString())).Result;
            response.EnsureSuccessStatusCode();
            String Result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                if (_regex.IsMatch(Result))
                {
                    //不是纯json需要match一下
                    dynamic result = JsonConvert.DeserializeObject<JObject>(_regex.Match(Result).Value);
                    var no = result.errInfo.no.Value;
                    if (no.Equals("0"))
                    {
                        token = result.data.token.Value;
                        res = true;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("result.errInfo.no:{0}", no));
                    }
                }
            }
            return res;
        }

        private bool GetPubksyAndRsakey()
        {
            bool res = false;
            var nvc = new NameValueCollection
            {
                {"token", token},
                {"tpl", "tb"},
                {"apiver", "v3"},
                {"tt", HttpUtil.GetTimeStamp()},
                {"gid", gid},
                {"callback", "bd__cbs__"+HttpUtil.GenerateCallBack()},
            };
            HttpResponseMessage response = httpClient.GetAsync(new Uri("https://passport.baidu.com/v2/getpublickey?" + nvc.ToQueryString())).Result;
            response.EnsureSuccessStatusCode();
            String Result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                if (_regex.IsMatch(Result))
                {
                    //不是纯json需要match一下
                    dynamic obj = JsonConvert.DeserializeObject<JObject>(_regex.Match(Result).Value);
                    var no = obj.errno.Value;
                    if (no.Equals("0"))
                    {
                        pubksy = obj.pubkey.Value;
                        rsakey = obj.key.Value;
                        res = true;
                    }
                }
            }
            return res;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Image img = GetValidImage();
            pictureBox1.Image = img;
            pictureBox1.Refresh();

            LoginBaiduUtility loginBaidu = new LoginBaiduUtility("382233701@qq.com","Tww19861004#");

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private async Task<string> GetHtmlStringAsync(string url)
        {
            var httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;//error
            }
        }

        private async Task<Stream> GetHtmlStreamAsync(string url1)
        {
            var httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.GetAsync(url1);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else
            {
                return null;//error
            }
        }

        private async Task<byte[]> GetHtmlBytesAsync(string url1)
        {
            var httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.GetAsync(url1);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                return null;//error
            }
        }

        private Image GetValidImage()
        {
            var nvc = new NameValueCollection
            {
                {"token", token},
                {"tpl", "mn"},
                {"apiver", "v3"},
                {"tt", HttpUtil.GetTimeStamp()},
                {"fr", "login"},
                //{"vcodetype", "7ea7JrZpMpKLyZu112UWT4NIYUCa8eOetNIn0rP8P6FMti58cJ8BIl1lnRqX9fdeYp84BbSMmr+yLUGUd/Do8yDrI/YV0uaa400z"},
                {"callback", "bd__cbs__"+HttpUtil.GenerateCallBack()},
            };
            HttpResponseMessage response = httpClient.GetAsync(new Uri("https://passport.baidu.com/v2/?reggetcodestr&" + nvc.ToQueryString())).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                if (_regex.IsMatch(result))
                {
                    //不是纯json需要match一下
                    dynamic obj = JsonConvert.DeserializeObject<JObject>(_regex.Match(result).Value);
                    var no = obj.errInfo.no.Value;
                    if (no.Equals("0"))
                    {
                        verifyStr = obj.data.verifyStr.Value;
                        response = httpClient.GetAsync(new Uri("https://passport.baidu.com/cgi-bin/genimage?" + obj.data.verifyStr.Value)).Result;
                        response.EnsureSuccessStatusCode();
                        if (response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            return Image.FromStream(response.Content.ReadAsStreamAsync().Result);
                        }
                        return null;
                    }
                }
                return null;
            }
            return null;
        }

        private bool CheckValCode(string code)
        {
            var nvc = new NameValueCollection
            {
                {"token", token},
                {"tpl", "mn"},
                {"apiver", "v3"},
                { "tt", HttpUtil.GetTimeStamp()},
                { "verifycode",code},
                { "codestring",verifyStr},
                {"callback", "bd__cbs__"+HttpUtil.GenerateCallBack()},
            };
            HttpResponseMessage response = httpClient.GetAsync(new Uri("https://passport.baidu.com/v2/?checkvcode&" + nvc.ToQueryString())).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                if (_regex.IsMatch(result))
                {
                    //不是纯json需要match一下
                    dynamic obj = JsonConvert.DeserializeObject<JObject>(_regex.Match(result).Value);
                    var no = obj.errInfo.no.Value;
                    if (no.Equals("0"))
                    {
                        valcode = code;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
