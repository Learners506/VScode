namespace DYH.Tools;

public static class DBDictionaryTool
{
    public static ObjectId GetOrCreateZrpybTableStyle(this DBTrans tr)
    {
        string dicName = "自然排烟表";
        var dict = tr.TableStyleDict;
        if (!dict.Contains(dicName))
        {
            TableStyle ts = new TableStyle()
            {
                IsTitleSuppressed = true,
                IsHeaderSuppressed = true,
            };
            using (dict.ForWrite())
            {
                dict.SetAt(dicName, ts);
                tr.Transaction.AddNewlyCreatedDBObject(ts, true);
            }
        }

        return dict.GetAt(dicName);
    }
    /// <summary>
    /// 设置软指针id
    /// </summary>
    /// <param name="dic">字典</param>
    /// <param name="key">key</param>
    /// <param name="ids">id列表</param>
    /// <returns>字典项的id</returns>
    public static ObjectId SetSoftPointer(this DBDictionary dic, string key, IEnumerable<ObjectId> ids)
    {
        var xr = new Xrecord()
        {
            Data = new ResultBuffer(ids.Select(id => new TypedValue(330, id)).ToArray()),
        };
        var id = dic.SetAt<Xrecord>(key, xr);
        return id;
    }
    /// <summary>
    /// 获取软指针id
    /// </summary>
    /// <param name="dic">字典</param>
    /// <param name="key">key</param>
    /// <returns>id列表</returns>
    public static List<ObjectId> GetSoftPointer(this DBDictionary dic, string key)
    {
        if (dic.GetAt<Xrecord>(key) is not { } xr)
            return [];
        return xr.Where(e=>e.TypeCode==330)
            .Select(e=>e.Value)
            .OfType<ObjectId>()
            .ToList();
    }
}