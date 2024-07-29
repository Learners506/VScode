namespace DYH.Tools;
public static class RXClassEx
{
    #region 静态RXClass预生成，避免速度慢
    public static readonly RXClass Entity = RXObject.GetClass(typeof(Entity));

    public static readonly RXClass Curve = RXObject.GetClass(typeof(Curve));
    public static readonly RXClass Line = RXObject.GetClass(typeof(Line));
    public static readonly RXClass PolyLine = RXObject.GetClass(typeof(Polyline));
    public static readonly RXClass Arc = RXObject.GetClass(typeof(Arc));
    public static readonly RXClass Circle = RXObject.GetClass(typeof(Circle));
    public static readonly RXClass Ellipse = RXObject.GetClass(typeof(Ellipse));
    public static readonly RXClass Spline = RXObject.GetClass(typeof(Spline));
    public static readonly RXClass MText = RXObject.GetClass(typeof(MText));
    public static readonly RXClass DBText = RXObject.GetClass(typeof(DBText));
    public static readonly RXClass MLeader = RXObject.GetClass(typeof(MLeader));
    public static readonly RXClass BlockReference = RXObject.GetClass(typeof(BlockReference));
    public static readonly RXClass AttributeReference = RXObject.GetClass(typeof(AttributeReference));
    public static readonly RXClass Table = RXObject.GetClass(typeof(Table));
    
    public static readonly RXClass BlockTableRecord = RXObject.GetClass(typeof(BlockTableRecord));
    #endregion

    /// <summary>
    /// 判断ObjectId所属的对象是否是RXClass类型
    /// </summary>
    /// <param name="objectId">数据库对象Id</param>
    /// <param name="tClass">RXClass类型</param>
    /// <returns>是则返回true，否则返回false</returns>
    public static bool IsDerivedFrom(this ObjectId objectId, RXClass tClass)
    {
        using var oClass = objectId.ObjectClass;
        return  oClass.IsDerivedFrom(tClass);
    }
}