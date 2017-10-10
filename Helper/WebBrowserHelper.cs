using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper
{
    public class WebBrowserHelper
    {
        //根据Name获取元素
        public HtmlElement GetElement_Name(WebBrowser wb, string Name)
        {
            HtmlElement e = wb.Document.All[Name];
            return e;
        }

        //根据Id获取元素
        public HtmlElement GetElement_Id(WebBrowser wb, string id)
        {
            HtmlElement e = wb.Document.GetElementById(id);
            return e;
        }

        //根据Index获取元素
        public HtmlElement GetElement_Index(WebBrowser wb, int index)
        {
            HtmlElement e = wb.Document.All[index];
            return e;
        }

        //获取form表单名name,返回表单
        public HtmlElement GetElement_Form(WebBrowser wb, string form_name)
        {
            HtmlElement e = wb.Document.Forms[form_name];
            return e;
        }


        //设置元素value属性的值
        public void Write_value(HtmlElement e, string value)
        {
            e.SetAttribute("value", value);
        }

        //执行元素的方法，如：click，submit(需Form表单名)等
        public void Btn_click(HtmlElement e, string s)
        {

            e.InvokeMember(s);
        }
    }
}
