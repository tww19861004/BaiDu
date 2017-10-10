using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatiN.Core;
using WatiN.Core.DialogHandlers;

namespace WatiN1
{
    class Program
    {
        static void Test()
        {
            //打开baidu
            using (IE ie = new IE("https://tieba.baidu.com/p/5341993700"))
            {
                ////给id为kw的文本框添加文字hyddd
                //ie.TextField(Find.ById("kw")).TypeText("hyddd");
                ////单击id为su的按钮
                //ie.Button(Find.ById("su")).Click();
                ////判断打开的页面时否包含“hyddd - 博客园”                
                //ie.Link(Find.ByText("hyddd - 博客园")).Click();

                foreach (IHTMLElement element in ie.HtmlDocument.all)
                {
                    if (element.id == "ueditor_replace")
                    {
                        element.setAttribute("innerHTML", "有人么？还有多久结束？下周吧？");
                        break;
                    }
                }
                string str = ie.Div(Find.ById("ueditor_replace")).GetAttributeValue("innerHTML");
                ie.Link(Find.ByTitle("Ctrl+Enter快捷发表")).ClickNoWait();                
            } 
        }

        static void Main(string[] args)
        {
            System.Threading.Thread th = new Thread(new ThreadStart(Test));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();
        }
    }
}
