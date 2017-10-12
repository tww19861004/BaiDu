using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Specialized;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;

namespace Helper
{
    public class BaiduloginWap : LoginBaiduBase
    {
        protected override string loginPostURL
        {
            get
            {
                return "https://wappass.baidu.com/wp/api/login?tt="+ HttpUtil.GetTimeStamp();
            }
        }

        protected override string TPL
        {
            get
            {
                return "wimn";
            }
        }

        protected override string Login(string verifyCode)
        {
            if (IsNeedVerifyCode)
            {
                if (!CheckValCode(verifyCode))
                {
                    return "验证码输入错误";
                }
            }
            string res = string.Empty;
            var pemToXml = RSAHelper.PemToXml(pubksy);
            var password = RSAHelper.RSAEncrypt(pemToXml, this.password);
            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>()
               {
                    {"staticpage", "https://passport.baidu.com/static/passpc-account/html/v3Jump.html"},
                    //{"staticpage","http://app.baidu.com/sfile/v3Jump.html"},
                    {"charset", "UTF-8"},
                    {"token", token},
                    {"tpl", TPL},
                    {"subpro","" },
                    {"apiver", "v3"},
                    {"tt", HttpUtil.GetTimeStamp()},
                    {"codestring", verifyStr},
                    {"safeflg", "0"},
                    {"u", "https://tieba.baidu.com/index.html#"},
                    {"isPhone", "false"},
                    {"detect", "1"},
                    {"quick_user", "0"},
                    {"gid", gid},
                    {"logintype", "dialogLogin"},
                    {"logLoginType", "pc_loginDialog"},
                    {"idc","" },
                    {"loginmerge", "true"},
                    {"splogin","rate" },
                    {"username", userName},
                    {"password", password},
                    {"verifycode", valcode},
                    {"mem_pass", "on"},
                    {"rsakey", rsakey},
                    {"crypttype", "12"},
                    {"ppui_logintime", HttpUtil.Generateplogintime()},
                    {"countrycode", ""},
                    //{"fp_uid", "960338120bd__cbs__"+HttpUtil.GenerateCallBack()+"8b87eb0671ea3df2d"},
                    //{"fp_info", "960338120bd__cbs__"+HttpUtil.GenerateCallBack()+"8b87eb0671ea3df2d002~~~nxnn3QqCEgie0x2_Unn6yqz090gtoCdtRC_qqz090gtoCd0RC_unmCsVnmCsJnnsEnm3pOq0xwEgi~JIJH4zEp4gVkJAVkFnERaUirBniP4~LPJoEWJ9HdXAFiBzieJAt~4Au~6IskJI2eJnTWcutK4gtOBgugJGJW6ItK8UsianJW6ItKanTw7AE-czFW4mTpXobP4AEYF3JAXAEdJI2M4AwQJzbNJnBRJzHq6UswJAhQ4gJPXnhiJzBHJAwQ6A~W9zukXIJiYgTHJAVk8zieBnE-4zuWcAVw6grNanTLJgiec3tKazhNJEs3FiJHJIBiaRHH4UFiazVw4mLrJn6NBziiBgE-vUnsBqC0Rae0v__DUnvSUnvNUnsPq0KUEgi~JIJH4zEp4gVkJAVkFnERaUirBniP4~LPJoEWJDT9XnhRXdBwBzEn4nuxXmT9XnhRXdBwBzEn4nuxXmT86IFHBzEp4nii4UFu7nERBIFw6zTicusPaUFw6zTi9zukXIJiYgTHJAVkFIwi6dEk6AbWJDTY4dbk6AbWJGFP6dENJAVkFzh-4AukIA0Rnvr3asEYmvaimkP9ZPOov3MEUsAYhIlrZVvKsPFKg1C3kC8nAXMtlhgYdySekQyus4nkJjvvvDXur5lZwCCb~s~aCkfVFUnsKUnsAUnszUnsrnn2LqC7zCNYkl_CqzBAVO4zhd4C__nUnsmnwLYXeksnnvdUnvbnweUkGbjUnvRUnvYUnveUnvpquc93etp2TtpYl8p2x8paktx2ktY__"},
                    //{"dv", "MDExAAoALQALAxIAHgAAAF00AAwCACKKzc3Nzdoidjd5PmwtYD9gMGMzbF1tMm0bfgxlA3o5VjJXDAIAIoqenp6eib3pqOah87L_oP-v_KzzwvKt8oThk_qc5abJrcgHAgAEkZGRkQwCACKKb29vb3nFkdCe2YvKh9iH14TUi7qK1Yr8meuC5J3esdWwBwIABJGRkZEHAgAEkZGRkQ0CACaRkZnW96PirOu5-LXqteW25rmIuOe4zqvZsNav7IPngsGpyKbBpAkCADWws87Pu7u7u7u8RkdELSxLS29vYDR1O3wubyJ9InIhcS4fL3AvWTxOJ0E4exRwFVY-XzFWMwYCACiRkZGRkZGRkZGRlNfX18NSUlJXAwMDAAAAAAVRUVFTi4uLjtra2tgAEwIAJpGzs7Pbr9ur2OLN4pb_mviZt9W03bnM4oHug6zFq8-q0vyU4I3hFwIACpOTsrK37ILZgO8WAgAisMSvn7GCuom4gbiOv4a2j7eCtoO1hLGBtoW9j7yKvIS1jQQCAAaTk5GQpZMVAgAIkZGQzjjFVycBAgAGkZOTg47uBQIABJGRkZ0QAgABkQcCAASRkZGRDQIABZGRku3tDQIAHpGRkhAJXRxSFUcGSxRLG0gYR3ZGGUY2VyRXIE89WQcCAASRkZGRDQIAHpGRmZuC1pfZnsyNwJ_AkMOTzP3Nks293K_cq8S20g0CACaRkZmcvemo5qHzsv-g_6_8rPPC8q3yhOGT-pzlpsmtyIvjguyL7gkCADWws87Pu7u7u7uwbWxvBgdgYERESx9eEFcFRAlWCVkKWgU0BFsEchdlDGoTUD9bPn0VdBp9GAcCAASRkZGRCQIAJ4qIU1I_Pz8_PzDW1oLDjcqY2ZTLlMSXx5ipmcaZ74r4kfeOzaLGowwCACKKnp6enozpvfyy9afmq_Sr-6j4p5am-abQtceuyLHynfmcDAIAIopvb29vfCp-P3E2ZCVoN2g4aztkVWU6ZRN2BG0LcjFeOl8HAgAEkZGRkQkCACOGhcDBDAwMDAwTubntrOKl97b7pPur-Kj3xvap9oXwkv-W4g"},
                    {"callback", "parent.bd__pcbs__"+HttpUtil.GenerateCallBack()},
               });
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            //httpClient.DefaultRequestHeaders.Clear();            
            //httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
            //httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
            HttpResponseMessage response = httpClient.PostAsync(new Uri(loginPostURL), httpContent).Result;
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
                        res = "系统错误,请您稍后再试";
                        break;
                    case "2":
                        res = "您输入的帐号不存在";
                        break;
                    case "4":
                        res = "您输入的帐号或密码有误 field : password";
                        break;
                    case "6":
                        res = "您输入的验证码有误 field : verifyCode";
                        break;
                    case "7":
                        res = "密码错误";
                        break;
                    case "257":
                        res = "需要填入验证码";
                        break;
                    default:
                        res = "errno:" + errno;
                        break;
                }
            }
            //httpClient.Dispose();
            return res;
        }
    }
}
