using System.Globalization;

namespace DYH.Tools;

public static class XDataEx
{
    public static void ClearXData(this DBObject dBObject, string appName)
    {
        if (dBObject.GetXDataForApplication(appName) is not null)
            dBObject.XData = new XDataList() { { 1001, appName } };
    }

    public static void AddHandles(this XDataList xdl, IEnumerable<Handle> handles)
    {
        handles.ForEach(h => xdl.Add(1005, h));
    }
    public static void AddHandles(this XDataList xd, params Handle[] handles)
    {
        handles.ForEach(h => xd.Add(1005, h));
    }
    public static Handle? GetHandle(this ResultBuffer rb)
    {
        return rb.GetHandleList().FirstOrDefault();
    }
    public static Handle? GetHandleFromXData(this DBObject obj, string appName)
    {
        return obj.GetXDataForApplication(appName)?.GetHandle();
    }
    public static List<Handle> GetHandleList(this ResultBuffer? rb)
    {
        List<Handle> handleList = new();
        if (rb is not null)
        {
            foreach (var tv in rb)
            {
                if (tv.TypeCode == 1005)
                {
                    try
                    {
                        if (long.TryParse(tv.Value.ToString(), NumberStyles.HexNumber, null, out long l))
                        {
                            handleList.Add(new(l));
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }
        return handleList;
    }
    public static List<T> GetCodeList<T>(this ResultBuffer? rb, int typeCode)
    {
        List<T> resultList = new();
        if (rb is null)
            return resultList;
        foreach (var tv in rb)
        {
            if (tv.TypeCode == typeCode && tv.Value is T tValue)
            {
                resultList.Add(tValue);
            }
        }
        return resultList;
    }
    public static List<object> GetCodeList(this ResultBuffer rb, int typeCode)
    {
        return rb.AsArray().Where(tv => tv.TypeCode == typeCode).Select(tv => tv.Value).ToList();
    }
    public static void AddCodeList(this XDataList xd, int typeCode, params object[] values)
    {
        foreach (var t in values)
        {
            xd.Add(typeCode, t);
        }
    }
    public static HashSet<string> GetAppNames(this ResultBuffer? rb)
    {
        var result = new HashSet<string>();
        if (rb is not null)
        {
            foreach (var item in rb)
            {
                if (item.TypeCode == 1001)
                    result.Add(item.Value.ToString());
            }
        }
        return result;
    }

    
}
