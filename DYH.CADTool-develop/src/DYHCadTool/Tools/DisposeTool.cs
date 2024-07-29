//namespace DYH.Tools;
//public class DBObjectManager : IDisposable
//{
//    #region 构造
//    public static DBObjectManager Run()
//    {
//        return new DBObjectManager();
//    }
//    private DBObjectManager()
//    {
//        Commit();
//        IsDisposed = false;
//    }
//    public bool IsDisposed { get; private set; }
//    protected virtual void Dispose(bool disposing)
//    {
//        if (disposing)
//        {
//            if (!IsDisposed)
//            {
//                Commit();
//                IsDisposed = true;
//            }
//        }
//    }
//    public void Dispose()
//    {
//        Dispose(true);
//        GC.SuppressFinalize(this);
//    }
//    #endregion

//    #region 管理器
//    private static readonly Stack<Stack<DBObject>> stack = new();
//    public void Commit()
//    {
//        while (stack.Any())
//        {
//            var stackSet = stack.Pop();
//            while (stackSet.Any())
//            {
//                stackSet.Pop()?.Dispose();
//            }
//        }
//        Nest();
//    }
//    public void Undo()
//    {
//        if (stack.Any())
//        {
//            var stackSet = stack.Peek();
//            while (stackSet.Any())
//            {
//                var dbo = stackSet.Pop();
//                dbo?.Cancel();
//                //dbo?.Dispose();
//            }
//        }
//        //Nest();
//    }
//    public void Abort()
//    {
//        while (stack.Any())
//        {
//            var stackSet = stack.Pop();
//            while (stackSet.Any())
//            {
//                var dbo = stackSet.Pop();
//                dbo?.Cancel();
//                dbo?.Dispose();
//            }
//        }
//        Nest();
//    }
//    public void Nest()
//    {
//        var stackSet = new Stack<DBObject>();
//        stack.Push(stackSet);
//    }
//    public static T Mark<T>(T dbobject) where T : DBObject
//    {
//        stack.Peek().Push(dbobject);
//        return dbobject;
//    }
//    #endregion
//}

//public static class OpenEx
//{
//    public static DBObject? OpenObject(this ObjectId id, OpenMode openMode = OpenMode.ForRead, bool openErased = false, bool forceOpenOnLockedLayer = false)
//    {
//        return id.Open(openMode, openErased, forceOpenOnLockedLayer) is { } obj ? obj.Mark() : null;
//    }
//    public static T Mark<T>(this T dbobject) where T : DBObject
//    {
//        return DBObjectManager.Mark(dbobject);
//    }
//    public static IEnumerable<T> Mark<T>(this IEnumerable<T> dbos) where T : DBObject
//    {
//        foreach (var dbobject in dbos)
//        {
//            yield return dbobject.Mark();
//        }
//    }
//}