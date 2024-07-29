using Newtonsoft.Json;

namespace DYH.Tools;
public static class SerializeTool
{
    /// <summary>
    /// 序列化为Json
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="obj">对象</param>
    /// <returns>字符串</returns>
    /// 
    public static string SerializeToJson<T>(this T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    /// <summary>
    /// 序列化为Json
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="obj">对象</param>
    /// <param name="filename">文件路径名</param>
    /// <param name="jss">格式</param>
    /// <returns>成功返回True</returns>
    public static bool SerializeToJson<T>(this T obj, string filename, JsonSerializerSettings? jss = null)
    {
        try
        {
            var str = JsonConvert.SerializeObject(obj, jss);
            File.WriteAllText(filename, str);
            return true;
        }
        catch (Exception ex)
        {
            Env.Editor.WriteMessage(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="filename">文件路径名</param>
    /// <param name="jss">格式</param>
    /// <returns>类</returns>
    public static T? DeserializeFromJsonFile<T>(this string filename, JsonSerializerSettings? jss = null) where T : class
    {
        try
        {
            var str = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<T>(str, jss);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="jsonString">JSON字符串</param>
    /// <returns>类</returns>
    public static T? DeserializeFromJson<T>(this string jsonString) where T : class
    {
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
    /// <summary>
    /// 序列化为二进制数组
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="obj">对象</param>
    /// <returns>二进制数组</returns>
    public static byte[] SerializeToByteArray<T>(this T obj)
    {
        var str = JsonConvert.SerializeObject(obj);
        return Encoding.UTF8.GetBytes(str);
    }
    /// <summary>
    /// 序列化为二进制数组
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="obj">对象</param>
    /// <param name="filename">文件路径名</param>
    /// <returns>成功返回True</returns>
    public static bool SerializeToByteArray<T>(this T obj, string filename)
    {
        try
        {
            var str = JsonConvert.SerializeObject(obj);
            var byteArray = Encoding.UTF8.GetBytes(str);
            File.WriteAllBytes(filename, byteArray);
            return true;
        }
        catch (Exception ex)
        {
            Env.Editor.WriteMessage(ex.Message);
            return false;
        }
    }
    /// <summary>
    /// 二进制数组反序列化
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="byteArray">二进制数组</param>
    /// <returns>类</returns>
    public static T? DeserializeTo<T>(this byte[] byteArray) where T : class
    {
        try
        {
            var str = Encoding.UTF8.GetString(byteArray);
            return JsonConvert.DeserializeObject<T>(str);
        }
        catch (Exception ex)
        {
            Env.Editor.WriteMessage(ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 二进制数组反序列化
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="filename">二进制数组</param>
    /// <returns>类</returns>
    public static T? DeserializeFromByteArrayFile<T>(this string filename) where T : class
    {
        try
        {
            var byteArray = File.ReadAllBytes(filename);
            var str = Encoding.UTF8.GetString(byteArray);
            return JsonConvert.DeserializeObject<T>(str);
        }
        catch (Exception ex)
        {
            Env.Editor.WriteMessage(ex.Message);
            return null;
        }
    }
    public static void SaveToNamedObjectsDict<T>(this DBTrans tr, string dicName, T obj)
    {
        var nod = tr.NamedObjectsDict;
        var xr = new Xrecord
        {
            Data = new ResultBuffer(new TypedValue(1004, obj.SerializeToByteArray()))
        };
        using (nod.ForWrite())
        {
            if (nod.Contains(dicName))
                nod.Remove(dicName);
            nod.SetAt(dicName, xr);
            tr.Transaction.AddNewlyCreatedDBObject(xr, true);
        }
    }
    public static T? GetFromNamedObjectsDict<T>(this DBTrans tr, string dicName) where T : class
    {
        var nod = tr.NamedObjectsDict;
        if (!nod.Contains(dicName) || tr.GetObject(nod.GetAt(dicName)) is not Xrecord xr)
            return null;
        return xr.Data.AsArray().FirstOrDefault(tv => tv.TypeCode == 1004)
            .Value is byte[] bytes ? bytes.DeserializeTo<T>() : null;
    }

    public static void SaveToXData<T>(this DBObject dbObject, T t, string appName) where T : class
    {
        using (dbObject.ForWrite())
        {
            var strs = JsonConvert.SerializeObject(t).SplitByLength(200);
            XDataList xd = new XDataList { { 1001, appName } };
            xd.AddRange(strs.Select(s => new TypedValue(1000, s)));
            dbObject.XData = xd;
        }
    }

    public static T? GetFromXData<T>(this DBObject dbObject, string appName)where T:class
    {
        if (dbObject.GetXDataForApplication(appName) is not { } rb)
            return null;
        var strs = rb.GetCodeList(1000).Select(e=>e.ToString());
        var str = string.Join("", strs);
        return JsonConvert.DeserializeObject<T>(str);
    }
}
