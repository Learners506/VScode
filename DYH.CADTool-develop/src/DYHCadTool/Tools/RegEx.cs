namespace DYH.Tools;

public static class RegisterEx
{
    public static void AddTrustedPaths()
    {
        var path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + ";";
        var strs = (string)Acaop.GetSystemVariable("TrustedPaths");
        if (!strs.Contains(path))
        {
            strs = path + strs;
            Acaop.SetSystemVariable("TrustedPaths", strs);
        }
    }
    /// <summary>
    /// 注册dll
    /// </summary>
    /// <param name="dllfile">dll文件路径</param>
    /// <returns></returns>
    public static bool RegisteringCAD(string dllfile)
    {
        RegistryKey? user = Registry.CurrentUser.OpenSubKey(HostApplicationServices.Current.UserRegistryProductRootKey + "\\Applications", true) ?? null;
        if (user == null)
        {
            return false;
        }
        RegistryKey keyUserApp = user.CreateSubKey(Path.GetFileNameWithoutExtension(dllfile))!;
        keyUserApp.SetValue("DESCRIPTION", Path.GetFileNameWithoutExtension(dllfile), RegistryValueKind.String);
        keyUserApp.SetValue("LOADCTRLS", 2, RegistryValueKind.DWord);
        keyUserApp.SetValue("LOADER", dllfile, RegistryValueKind.String);
        keyUserApp.SetValue("MANAGED", 1, RegistryValueKind.DWord);

        return true;
    }

    /// <summary>
    /// 注册调用此函数的dll
    /// </summary>
    /// <returns></returns>
    public static bool RegisteringCAD()
    {
        return RegisteringCAD(Assembly.GetCallingAssembly().Location);
    }
    /// <summary>
    /// 注册dll
    /// </summary>
    /// <returns></returns>
    public static void RegisteringAllCAD()
    {
        string fileName = Assembly.GetCallingAssembly().Location;
        string dllFile1 = fileName.Replace("45\\", "40\\");
        string dllFile2 = fileName.Replace("40\\", "45\\");
        var names = GetHardcopyList();
        foreach (var name in names)
        {
            var strs = name.Split('\\');
            if (strs.Length != 4)
                continue;
            if (!double.TryParse(strs[2].Replace("R", ""), out double version) || version < 19)
                continue;
            var dllFile = version >= 20 ? dllFile2 : dllFile1;
            var address = "SOFTWARE\\" + name + "\\Applications";
            RegistryKey? user = Registry.CurrentUser.OpenSubKey(address, true) ?? null;
            if (user is null)
            {
                continue;
            }
            RegistryKey keyUserApp = user.CreateSubKey(Path.GetFileNameWithoutExtension(dllFile))!;
            keyUserApp.SetValue("DESCRIPTION", Path.GetFileNameWithoutExtension(dllFile), RegistryValueKind.String);
            keyUserApp.SetValue("LOADCTRLS", 2, RegistryValueKind.DWord);
            keyUserApp.SetValue("LOADER", dllFile, RegistryValueKind.String);
            keyUserApp.SetValue("MANAGED", 1, RegistryValueKind.DWord);
        }
    }
    public static List<string> GetHardcopyList()
    {
        List<string> list = new List<string>();
        var key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Autodesk\Hardcopy");
        if (key != null)
        {
            string[] subKeyNames = key.GetValueNames();
            foreach (string name in subKeyNames)
            {
                list.Add(name);
            }
        }
        return list;
    }
    /// <summary>
    /// 删除注册的dll
    /// </summary>
    /// <param name="file">dll文件路径</param>
    /// <returns></returns>
    public static bool DeleteRegisteredCAD(string file)
    {
        RegistryKey? user = Registry.CurrentUser.OpenSubKey(HostApplicationServices.Current.UserRegistryProductRootKey + "\\Applications", true) ?? null;
        if (user == null)
        {
            return false;
        }

        if (user.GetSubKeyNames().Contains(Path.GetFileNameWithoutExtension(file)))
        {
            user.DeleteSubKey(Path.GetFileNameWithoutExtension(file));
            return true;
        }

        return false;
    }

    /// 删除调用此函数的dll的注册
    /// <returns></returns>
    public static bool DeleteRegisteredCAD()
    {
        return DeleteRegisteredCAD(Assembly.GetCallingAssembly().Location);
    }
}