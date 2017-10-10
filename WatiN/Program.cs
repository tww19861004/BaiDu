using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace WatiN
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IE ie = new IE("https://tieba.baidu.com/p/5336577403"))
            {
                // Maximize the IE window in order to view test
                ie.ShowWindow(NativeMethods.WindowShowStyle.Maximize);

                // search for txtNickName and type "gsus" in it
                ie.TextField(Find.ById("txtNickName")).TypeText("gsus");

                // fire the click event of the button
                ie.Button(Find.ById("btnCheck")).Click();                                
            }
        }
    }
}
