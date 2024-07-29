using System.Windows.Input;
using Autodesk.AutoCAD.Windows;

namespace DYH.Tools;

public static class StringTool
{
    /// <summary>
    /// 根据长度切分字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="length">长度</param>
    /// <returns></returns>
    public static IEnumerable<string> SplitByLength(this string str, int length)
    {
        //List<string> result = new List<string>();

        for (var i = 0; i < str.Length; i += length)
        {
            yield return i + length > str.Length ? str.Substring(i) : str.Substring(i, length);
        }

        //return result;
    }

    static char tempChar = Convert.ToChar(10000);

    /// <summary>
    /// 通过字符串分解字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="knife"></param>
    /// <returns></returns>
    public static string[] SplitByStr(this string str, string knife)
    {
        while (str.Contains(tempChar))
        {
            tempChar = Convert.ToChar(RandomEx.NextInt(10000, 20000));
        }

        return str.Replace(knife, tempChar.ToString()).Split(tempChar);
    }

    /// <summary>
    /// 对象ToString后prompt(不带引号) 20220720
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="newLine">换行</param>
    /// <returns>对象本身</returns>
    public static T Prompt<T>(this T obj, bool newLine = true)
    {
        var str = obj is null ? "null" : $"{obj}";
        Acaop.DocumentManager.MdiActiveDocument.Editor.WriteMessage(newLine ? $"\n{str}" : str);
        System.Windows.Forms.Application.DoEvents();
        return obj;
    }

    public static T ShowMessageBox<T>(this T obj)
    {
        MessageBox.Show(obj is null ? "null" : $"{obj}");
        return obj;
    }

    /// <summary>
    /// 获取长度，一个汉字算2个字符
    /// </summary>
    /// <param name="aOrgStr"></param>
    /// <returns>个数</returns>
    public static int GetAnsiLength(this String aOrgStr)
    {
        int intLen = aOrgStr.Length;
        int i;
        char[] chars = aOrgStr.ToCharArray();
        for (i = 0; i < chars.Length; i++)
        {
            if (Convert.ToInt32(chars[i]) > 255)
            {
                intLen++;
            }
        }

        return intLen;
    }

    /// <summary>
    /// 判断图层名是否是合法
    /// </summary>
    /// <param name="layerName"></param>
    /// <returns></returns>
    public static bool IsLegalLayerName(this string layerName)
    {
        if (layerName.Trim() == "")
            return false;
        return !(layerName == "" || layerName.Contains("<") || layerName.Contains(">") || layerName.Contains("\"") ||
                 layerName.Contains(":") || layerName.Contains(";") || layerName.Contains("*") ||
                 layerName.Contains("|") || layerName.Contains(",") || layerName.Contains("=") ||
                 layerName.Contains("'") || layerName.Contains("?"));
    }
}

public static class MathTool
{
    /// <summary>
    /// 求最大公约数（辗转相除法）
    /// </summary>
    /// <param name="a">整数</param>
    /// <param name="b">整数</param>
    /// <returns>最大公约数</returns>
    public static int MaxCommonDivisor(this int a, int b)
    {
        while (b != 0)
        {
            (a, b) = (b, a % b);
        }

        return a;
    }
    ///// <summary>
    ///// 叉乘
    ///// </summary>
    ///// <param name="a"></param>
    ///// <param name="b"></param>
    ///// <returns></returns>
    //public static double Cross2d(this Vector3d a, Vector3d b)
    //{
    //    return a.X * b.Y - b.X * a.Y;
    //}

    /// <summary>
    /// 叉积,求法向量
    /// </summary>
    /// <param name="a">向量a</param>
    /// <param name="b">向量b</param>
    /// <returns>右手坐标系系法向量</returns>
    public static Vector3d CrossNormal(Vector3d a, Vector3d b)
    {
        //叉乘:依次用手指盖住每列,交叉相乘再相减,注意主副顺序
        //(a.X  a.Y  a.Z)
        //(b.X  b.Y  b.Z)
        return new Vector3d(a.Y * b.Z - b.Y * a.Z, //主-副(左上到右下是主,左下到右上是副)
            a.Z * b.X - b.Z * a.X, //副-主
            a.X * b.Y - b.X * a.Y); //主-副
    }
}

public static class Win32Api
{
    /// <summary>
    /// 设置活动窗口
    /// </summary>
    /// <param name="hwnd">窗口句柄</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern IntPtr SetFocus(IntPtr hwnd);

    /// <summary>
    /// 判断文件是否被占用
    /// </summary>
    /// <param name="filePath">"路径"</param>
    /// <returns>true被占用</returns>
    public static bool IsFileOccupied(string filePath)
    {
        try
        {
            using var fs = File.OpenWrite(filePath);
            fs.Close();
        }
        catch (Exception)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 模拟键盘按键
    /// </summary>
    /// <param name="key">键值</param>
    /// <param name="bScan"></param>
    /// <param name="dwFlags"></param>
    /// <param name="dwExtraInfo"></param>
    public static void KeyBoardSendKey(Keys key, byte bScan = 0, uint dwFlags = 0, uint dwExtraInfo = 0)
    {
        keybd_event(key, bScan, dwFlags, dwExtraInfo);
        keybd_event(key, bScan, 2, dwExtraInfo);
    }

    [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
    private static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    public static bool ChangeFileNameByDos(string oldFileName, string newFileName)
    {
        var dosName = Path.GetTempFileName() + ".bat";
        if (!File.Exists(oldFileName) || File.Exists(dosName) || File.Exists(newFileName))
            return false;

        var dosStr = $"rename \"{oldFileName}\" \"{Path.GetFileName(newFileName)}\"";
        File.WriteAllText(dosName, dosStr, Encoding.Default);
        ProcessStartInfo psi = new()
        {
            FileName = dosName,
            WindowStyle = ProcessWindowStyle.Hidden,
        };
        using var dosPro = Process.Start(psi);
        dosPro?.WaitForExit();
        //File.Delete(dosName);
        return true;
    }
}

public static class TypeClass
{
    public static void MapperFrom<T>(this T obj1, T obj2)
    {
        var props = typeof(T).GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanRead)
                continue;
            prop.SetValue(obj1, prop.GetValue(obj2, null), null);
        }
    }
}
