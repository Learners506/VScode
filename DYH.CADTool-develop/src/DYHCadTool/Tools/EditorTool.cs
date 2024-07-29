namespace DYH.Tools;
public static class EditorTool
{
    /// <summary>
    /// 添加关键字
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="po">PromptOptions</param>
    /// <param name="keyword">关键字</param>
    /// <returns>返回对应类型的PromptOptions</returns>
    public static T AddKeyword<T>(this T po, params string[] keyword) where T : PromptOptions
    {
        for (int i = 0; i < keyword.Length; i++)
        {
            po.Keywords.Add(keyword[i]);
        }
        po.AppendKeywordsToMessage = false;
        return po;
    }
    /// <summary>
    /// 添加关键字对，字母，选项，字母，选项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="po"></param>
    /// <param name="keywords"></param>
    /// <returns></returns>
    public static T AddKeywordPair<T>(this T po, params string[] keywords) where T : PromptOptions
    {
        int num = keywords.Length / 2 * 2;
        for (int i = 0; i < num; i += 2)
        {
            var key = keywords[i];
            if (key != " ")
                po.Keywords.Add(key, key, keywords[i + 1] + "(" + key + ")");
            else
                po.Keywords.Add(key, key, keywords[i + 1]);
        }
        return po;
    }
    public static T SetKeywordPair<T>(this T po, params string[] keywords) where T : PromptOptions
    {
        po.Keywords.Clear();
        return po.AddKeywordPair(keywords);
    }
    /// <summary>
    /// 添加关键字
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="po">PromptOptions</param>
    /// <param name="keyword">关键字</param>
    /// <returns>返回对应类型的PromptOptions</returns>
    public static T SetKeyword<T>(this T po, params string[] keyword) where T : PromptOptions
    {
        po.Keywords.Clear();
        for (int i = 0; i < keyword.Length; i++)
        {
            po.Keywords.Add(keyword[i]);
        }
        po.AppendKeywordsToMessage = false;
        return po;
    }
    public static PromptSelectionOptions AddKeyword(this PromptSelectionOptions pso, params string[] keyword)
    {
        for (int i = 0; i < keyword.Length; i++)
        {
            pso.Keywords.Add(keyword[i]);
        }
        return pso;
    }
    /// <summary>
    /// 获取点
    /// </summary>
    /// <param name="ed">命令行</param>
    /// <param name="ppomessage">提示</param>
    /// <returns>PromptPointResult</returns>
    public static PromptPointResult GetPointEx(this Editor ed, string ppomessage = "\n选择点")
    {
        //声明获取点提示选项ppo1
        var ppo = new PromptPointOptions(ppomessage)
        {
            AllowNone = true,
        };
        return ed.GetPoint(ppo);
    }
    /// <summary>
    /// 获取点（有基点）
    /// </summary>
    /// <param name="ed">命令行</param>
    /// <param name="ppomessage">提示信息</param>
    /// <param name="basePoint">基点</param>
    /// <returns>PromptPointResult</returns>
    public static PromptPointResult GetPointEx(this Editor ed, Point3d basePoint, string ppomessage = "\n选择点")
    {
        //声明获取点提示选项ppo1
        var ppo = new PromptPointOptions(ppomessage)
        {
            AllowNone = true,
            BasePoint = basePoint,
            UseBasePoint = true
        };
        return ed.GetPoint(ppo);
    }
    public static Point3d? PickPoint(this Editor ed, string ppomessage = "\n选择点")
    {
        var r1 = ed.GetPointEx(ppomessage);
        return r1.Status == PromptStatus.OK ? r1.Value : null;
    }
    public static Point3d? PickPoint(this Editor ed, Point3d basePoint, string ppomessage = "\n选择点")
    {
        var r1 = ed.GetPointEx(basePoint, ppomessage);
        return r1.Status == PromptStatus.OK ? r1.Value : null;
    }
    public static PromptDoubleResult GetDoubleEx(this Editor ed, string pdoMessage, Action<PromptDoubleOptions>? act = null)
    {
        PromptDoubleOptions pdo = new(pdoMessage)
        {
            AllowNone = true,
        };
        act?.Invoke(pdo);
        return ed.GetDouble(pdo);
    }

    /// <summary>
    /// 获取选择集返回值对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="psr"></param>
    /// <param name="trans"></param>
    /// <param name="openMode"></param>
    /// <param name="openErased"></param>
    /// <param name="forceOpenOnLockedLayer"></param>
    /// <returns></returns>
    public static List<T> GetEntities<T>(this PromptSelectionResult psr, Transaction trans, OpenMode openMode = OpenMode.ForRead, bool openErased = false, bool forceOpenOnLockedLayer = false)
    {
        if (psr.Status != PromptStatus.OK)
            return new();
        return psr.Value.GetObjectIds().Select(id => trans.GetObject(id, openMode, openErased, forceOpenOnLockedLayer)).OfType<T>().ToList();
    }
    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="ed"></param>
    /// <param name="message"></param>
    /// <param name="allowObjectOnLockedLayer"></param>
    /// <returns></returns>
    public static PromptEntityResult GetEntityEx(this Editor ed, string message = "\n选择对象", bool allowObjectOnLockedLayer = false)
    {
        var peo = new PromptEntityOptions(message)
        {
            AllowNone = true,
            AllowObjectOnLockedLayer = allowObjectOnLockedLayer,
            AppendKeywordsToMessage = false
        };
        return ed.GetEntity(peo);
    }
    public static DBObject? PickEntity(this Editor ed, string message = "\n选择对象", bool allowObjectOnLockedLayer = false)
    {
        var r1 = ed.GetEntityEx(message, allowObjectOnLockedLayer);
        if (r1.Status != PromptStatus.OK)
            return null;
        var tr = DBTrans.GetTopTransaction(ed.Document.Database);
        return tr.GetObject(r1.ObjectId);
    }

    public static IEnumerable<T> SelectEntities<T>(this Editor ed, string? str = null, SelectionFilter? selectionFilter = null)
    {
        var tr = DBTrans.GetTop(ed.Document.Database);
        PromptSelectionOptions pso = new() { RejectObjectsOnLockedLayers = true };
        if (str != null)
            pso.MessageForAdding = str;
        var r1 = Env.Editor.GetSelection(pso, selectionFilter);
        if (r1.Status != PromptStatus.OK)
            return new List<T>();
        return r1.Value.GetObjectIds().Select(id => tr.GetObject(id)).OfType<T>();
    }
    public static void DrawEntities(this JigEx jigEx, params Entity[] ents)
    {
        jigEx.DatabaseEntityDraw(wd =>
        {
            ents.ForEach(e => wd.Geometry.Draw(e));
        });
    }
    //public static PromptSelectionResult GetSelection(string msg = "\n选择对象",SelectionFilter? selectionFilter = null)
    //{

    //}
}
