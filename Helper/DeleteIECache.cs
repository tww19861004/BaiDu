using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

public class DeleteIECache{    [DllImport("shell32.dll")]    public extern static IntPtr ShellExecute(IntPtr hwnd,                                             string lpOperation,                                             string lpFile,                                             string lpParameters,                                             string lpDirectory,                                             int nShowCmd                                            );

    //删除IE临时文件
    public static void DeleteTemporaryInternetFiles(IntPtr hander)    {        ShellExecute(hander, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8", null, (int)DeleteIECache.ShowWindowCommands.SW_SHOW);    }

    //删除IE Cookies
    public static void DeleteAllIECookies(IntPtr hander)    {        ShellExecute(hander, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 2", null, (int)DeleteIECache.ShowWindowCommands.SW_SHOW);    }

    //删除IE 历史记录
    public static void DeleteHistory(IntPtr hander)    {        ShellExecute(hander, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 1", null, (int)DeleteIECache.ShowWindowCommands.SW_SHOW);    }

    //删除IE 表单数据
    public static void DeleteFormData(IntPtr hander)    {        ShellExecute(hander, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 16", null, (int)DeleteIECache.ShowWindowCommands.SW_SHOW);    }

    //删除IE 所有密码 
    public static void DeletePasswords(IntPtr hander)    {        ShellExecute(hander, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 32", null, (int)DeleteIECache.ShowWindowCommands.SW_SHOW);    }

    //删除IE 所有数据 
    public static void DeleteAll(IntPtr hander)    {        ShellExecute(hander, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess  255", null, (int)DeleteIECache.ShowWindowCommands.SW_SHOW);    }

    public enum ShowWindowCommands : int    {

        SW_HIDE = 0,        SW_SHOWNORMAL = 1,        SW_NORMAL = 1,        SW_SHOWMINIMIZED = 2,        SW_SHOWMAXIMIZED = 3,        SW_MAXIMIZE = 3,        SW_SHOWNOACTIVATE = 4,        SW_SHOW = 5,        SW_MINIMIZE = 6,        SW_SHOWMINNOACTIVE = 7,        SW_SHOWNA = 8,        SW_RESTORE = 9,        SW_SHOWDEFAULT = 10,        SW_MAX = 10    }

    }