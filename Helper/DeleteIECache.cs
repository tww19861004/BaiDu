﻿using System;
using System.Diagnostics;
using System.Threading;

public class DeleteIECache

    //删除IE临时文件
    public static void DeleteTemporaryInternetFiles(IntPtr hander)

    //删除IE Cookies
    public static void DeleteAllIECookies(IntPtr hander)

    //删除IE 历史记录
    public static void DeleteHistory(IntPtr hander)

    //删除IE 表单数据
    public static void DeleteFormData(IntPtr hander)

    //删除IE 所有密码 
    public static void DeletePasswords(IntPtr hander)

    //删除IE 所有数据 
    public static void DeleteAll(IntPtr hander)

    public enum ShowWindowCommands : int

        SW_HIDE = 0,

    