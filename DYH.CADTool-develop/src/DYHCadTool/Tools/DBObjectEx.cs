namespace DYH.Tools;

public static class DBObjectEx
{
    public static DBObject GetObject(this ObjectId objectId) => objectId.GetObject(OpenMode.ForRead);

    /// <summary>
    /// 更新实体动画(在提交事务前显示)
    /// </summary>
    /// <param name="ent">实体</param>
    public static void UpdateScreenEx(this Entity ent)
    {
        Env.Editor.Redraw(ent);
    }

    public static void UpdateScreenEx(params Entity[] entities)
    {
        foreach (var ent in entities)
        {
            ent.Draw(); //图元刷新
            ent.RecordGraphicsModified(true); //图块刷新
            if (ent is Dimension dim) //标注刷新
                dim.RecomputeDimensionBlock(true);
        }

        Acaop.UpdateScreen();
        System.Windows.Forms.Application.DoEvents();
    }

    public static List<Point3d> IntersectWith(this Entity ent1, Entity ent2,
        Intersect intersectType = Intersect.OnBothOperands, Plane? plane = null)
    {
        plane ??= new(Point3d.Origin, Vector3d.ZAxis);
        using Point3dCollection point3dCollection = new();
        ent1.IntersectWith(ent2, intersectType, plane, point3dCollection, IntPtr.Zero, IntPtr.Zero);
        return point3dCollection.Cast<Point3d>().ToList();
    }

    public static bool MoveStretchPoint2d(this Entity ent, Point3d from, Point3d to, double tol = 0.1)
    {
        var p3dc = new Point3dCollection();
        ent.GetStretchPoints(p3dc);
        for (int i = 0; i < p3dc.Count; i++)
        {
            var pt = p3dc[i];
            if (pt.Distance2dTo(from) < tol)
            {
                ent.MoveStretchPointsAt([i], pt.GetVectorTo(to).Z20());
                return true;
            }
        }

        return false;
    }

    public static bool MoveStretchPoint2d(this Entity ent, Point3d from, Vector3d v, double tol = 0.1)
    {
        var p3dc = new Point3dCollection();

        ent.GetStretchPoints(p3dc);
        for (int i = 0; i < p3dc.Count; i++)
        {
            var pt = p3dc[i];
            if (pt.Z20().DistanceTo(from.Z20()) < tol)
            {
                ent.MoveStretchPointsAt([i], v);
                return true;
            }
        }

        return false;
    }

    public static bool MoveStretchPoint(this Entity ent, Point3d from, Point3d to, Tolerance? tol = null)
    {
        var p3dc = new Point3dCollection();
        ent.GetStretchPoints(p3dc);
        for (int i = 0; i < p3dc.Count; i++)
        {
            if (p3dc[i].IsEqualTo(from, tol ?? new Tolerance(0, 0)))
            {
                ent.MoveStretchPointsAt([i], from.GetVectorTo(to));
                return true;
            }
        }

        return false;
    }
}

public static class EntityTool
{
    public static bool IsOnLockedLayer(this Entity ent, Transaction? tr = null)
    {
        if (ent.IsNewObject)
            return false;
        tr ??= DBTrans.GetTopTransaction(ent.Database);
        return ((LayerTableRecord)tr.GetObject(ent.LayerId)).IsLocked;
    }

    /// <summary>
    /// 获取矩阵转换复制后的对象列表
    /// 一个对象转换后可能有多个对象
    /// </summary>
    /// <param name="ent">实体</param>
    /// <param name="mt">矩阵</param>
    /// <returns>矩阵转换复制后的对象列表</returns>
    public static List<Entity> GetTransformedCopyList(this Entity ent, Matrix3d mt)
    {
        var result = new List<Entity>();
        try
        {
            var entNew = ent.GetTransformedCopy(mt);
            if (entNew is Dimension dim)
            {
                dim.RecomputeDimensionBlock(true);
            }

            result.Add(entNew);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("eExplodeBeforeTramsform"))
            {
                using var dboc = new DBObjectCollection();
                ent.Explode(dboc);
                result.AddRange(dboc.Cast<Entity>());
            }
            else
            {
                throw;
            }
        }

        return result;
    }
}

public static class BoundingBoxEx
{
    /// <summary>
    /// 包围盒
    /// </summary>
    /// <param name="ent"></param>
    /// <returns></returns>
    public static Extents3d BoundingBox(this Entity ent)
    {
        try
        {
            if (ent is BlockReference brf) return brf.GeometryExtentsBestFit();
            else return ent.GeometricExtents;
        }
        catch
        {
            return new Extents3d();
        }
    }

    /// <summary>
    /// 获取包围盒中点
    /// </summary>
    /// <param name="ent">实体</param>
    /// <returns>中点坐标</returns>
    public static Point3d GetBoxMidPoint(this Entity ent)
    {
        return ent.BoundingBox().MidPoint();
    }

    /// <summary>
    /// 通过包围盒生成矩形多段线
    /// </summary>
    /// <param name="extents3D">包围盒</param>
    /// <param name="lineWidth">线宽</param>
    /// <returns>多段线</returns>
    public static Polyline ToRectangle2D(this Extents3d extents3D, double lineWidth = 0)
    {
        return extents3D.GetRecPoint2ds().ToPolyline(lineWidth, true);
    }

    public static double Area(this Extents3d extents3D)
    {
        return Math.Abs(extents3D.MaxPoint.X - extents3D.MinPoint.X) *
               Math.Abs(extents3D.MaxPoint.Y - extents3D.MinPoint.Y);
    }

    public static Point3d MidPoint(this Extents3d extents3D)
    {
        return new Point3d((extents3D.MaxPoint.X + extents3D.MinPoint.X) * 0.5,
            (extents3D.MaxPoint.Y + extents3D.MinPoint.Y) * 0.5, (extents3D.MaxPoint.Z + extents3D.MinPoint.Z) * 0.5);
    }

    /// <summary>
    /// 获取矩形4个3d角点(左下起，正方向)
    /// </summary>
    /// <param name="extents3d">包围盒</param>
    /// <param name="z">z轴坐标</param>
    /// <returns>点表</returns>
    public static List<Point3d> GetRecPoint3ds(this Extents3d extents3d, double z = 0)
    {
        var xMin = extents3d.MinPoint.X;
        var xMax = extents3d.MaxPoint.X;
        var yMin = extents3d.MinPoint.Y;
        var yMax = extents3d.MaxPoint.Y;
        var pt1 = new Point3d(xMin, yMin, z);
        var pt2 = new Point3d(xMax, yMin, z);
        var pt3 = new Point3d(xMax, yMax, z);
        var pt4 = new Point3d(xMin, yMax, z);
        return [pt1, pt2, pt3, pt4];
    }

    public static List<Point3d> GetRecPoint3ds(this Extents2d extents2d, double z = 0)
    {
        var XMin = extents2d.MinPoint.X;
        var XMax = extents2d.MaxPoint.X;
        var YMin = extents2d.MinPoint.Y;
        var YMax = extents2d.MaxPoint.Y;
        var pt1 = new Point3d(XMin, YMin, z);
        var pt2 = new Point3d(XMax, YMin, z);
        var pt3 = new Point3d(XMax, YMax, z);
        var pt4 = new Point3d(XMin, YMax, z);
        return [pt1, pt2, pt3, pt4];
    }

    public static List<Point2d> GetRecPoint2ds(this Extents3d extents3d)
    {
        var XMin = extents3d.MinPoint.X;
        var XMax = extents3d.MaxPoint.X;
        var YMin = extents3d.MinPoint.Y;
        var YMax = extents3d.MaxPoint.Y;
        var pt1 = new Point2d(XMin, YMin);
        var pt2 = new Point2d(XMax, YMin);
        var pt3 = new Point2d(XMax, YMax);
        var pt4 = new Point2d(XMin, YMax);
        return [pt1, pt2, pt3, pt4];
    }

    public static List<Point2d> GetRecPoint2ds(this Extents2d extents2d)
    {
        var XMin = extents2d.MinPoint.X;
        var XMax = extents2d.MaxPoint.X;
        var YMin = extents2d.MinPoint.Y;
        var YMax = extents2d.MaxPoint.Y;
        var pt1 = new Point2d(XMin, YMin);
        var pt2 = new Point2d(XMax, YMin);
        var pt3 = new Point2d(XMax, YMax);
        var pt4 = new Point2d(XMin, YMax);
        return [pt1, pt2, pt3, pt4 ];
    }
}

public static class CurveTool
{
    /// <summary>
    /// 点表生成PL线
    /// </summary>
    /// <param name="pointList">点表</param>
    /// <param name="plineWidth">线宽</param>
    /// <param name="closed">是否闭合</param>
    /// <returns>Polyline</returns>
    public static Polyline ToPolyline(this IEnumerable<Point2d> pointList, double plineWidth = 0, bool closed = false)
    {
        var pl = new Polyline();
        var enumerable = pointList.ToList();
        for (int i = 0; i < enumerable.Count(); i++)
        {
            pl.AddVertexAt(i, enumerable.ElementAt(i), 0, plineWidth, plineWidth);
        }

        pl.Closed = closed;
        return pl;
    }

    /// <summary>
    /// 点表生成PL线
    /// </summary>
    /// <param name="pointList">点表</param>
    /// <param name="plineWidth">线宽</param>
    /// <param name="closed">是否闭合</param>
    /// <returns>Polyline</returns>
    public static Polyline ToPolyline(this IEnumerable<Point3d> pointList, double plineWidth = 0, bool closed = false)
    {
        var pl = new Polyline();
        var enumerable = pointList.ToList();
        for (int i = 0; i < enumerable.Count(); i++)
        {
            pl.AddVertexAt(i, enumerable.ElementAt(i).Point2d(), 0, plineWidth, plineWidth);
        }

        pl.Closed = closed;
        return pl;
    }

    /// <summary>
    /// 偏移
    /// </summary>
    /// <param name="cur">曲线</param>
    /// <param name="referencePoint">参考点</param>
    /// <param name="offsetDist">偏移距离</param>
    /// <returns>偏移后的曲线表</returns>
    public static List<Curve> Offset(this Curve cur, Point5d referencePoint, double offsetDist)
    {
        var closestPt = cur.GetClosestPointTo(referencePoint, false);
        var v1 = cur.GetFirstDerivative(closestPt);
        var v2 = closestPt.GetVectorTo(referencePoint);
        if (v1.Cross2d(v2) >= 0) offsetDist = -offsetDist;
        try
        {
            var dboc = cur.GetOffsetCurves(offsetDist);
            var curList = new List<Curve>();
            foreach (var dbo in dboc)
            {
                if (dbo is Curve cur1) curList.Add(cur1);
            }

            return curList;
        }
        catch
        {
            return new List<Curve>();
        }
    }
}

public static class BlockTool
{
    /// <summary>
    /// 设置块动态值
    /// </summary>
    /// <param name="blockReference"></param>
    /// <param name="propertyNameValues"></param>
    /// <returns></returns>
    public static bool SetBlockProperty(this BlockReference blockReference,
        Dictionary<string, object> propertyNameValues)
    {
        if (!blockReference.IsDynamicBlock)
            return false;
        foreach (DynamicBlockReferenceProperty item in blockReference.DynamicBlockReferencePropertyCollection)
        {
            if (propertyNameValues.TryGetValue(item.PropertyName, out var value))
            {
                item.Value = item.PropertyTypeCode switch
                {
                    1 => Convert.ToDouble(value),
                    2 => Convert.ToInt32(value),
                    3 => Convert.ToInt16(value),
                    4 => Convert.ToInt16(value),
                    5 => Convert.ToString(value),
                    13 => Convert.ToInt64(value),
                    _ => value,
                };
            }
        }

        return true;
    }

    /// <summary>
    /// 设置块属性值
    /// </summary>
    /// <param name="blockReference"></param>
    /// <param name="attributeNameValues"></param>
    /// <param name="trans"></param>
    public static void SetBlockAttribute(this BlockReference blockReference,
        Dictionary<string, string> attributeNameValues, Transaction? trans = null)
    {
        if (blockReference.IsNewObject || blockReference.AttributeCollection.Count == 0)
            return;
        trans ??= DBTrans.GetTopTransaction(blockReference.Database);
        foreach (ObjectId item in blockReference.AttributeCollection)
        {
            AttributeReference attRef = trans.GetObject<AttributeReference>(item)!;
            if (attributeNameValues.ContainsKey(attRef.Tag))
            {
                attRef.UpgradeOpen();
                attRef.TextString = attributeNameValues[attRef.Tag];
                attRef.DowngradeOpen();
            }
        }
    }

    /// <summary>
    /// 设置块属性值
    /// </summary>
    /// <param name="blockReference"></param>
    /// <param name="Name"></param>
    /// <param name="Value"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static bool SetBlockAttribute(this BlockReference blockReference, string Name, string Value,
        DBTrans? trans = null)
    {
        try
        {
            if (blockReference.AttributeCollection.Count > 0)
            {
                trans ??= DBTrans.Top;
                foreach (ObjectId item in blockReference.AttributeCollection)
                {
                    AttributeReference attRef = trans.GetObject<AttributeReference>(item)!;
                    if (Name == attRef.Tag)
                    {
                        attRef.UpgradeOpen();
                        attRef.TextString = Value;
                        attRef.DowngradeOpen();
                    }
                }

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 获取块属性值
    /// </summary>
    /// <param name="blockReference"></param>
    /// <param name="valueName"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static string? GetBlockAttribute(this BlockReference blockReference, string valueName, DBTrans? trans = null)
    {
        trans ??= DBTrans.Top;
        foreach (ObjectId item in blockReference.AttributeCollection)
        {
            using AttributeReference attRef = trans.GetObject<AttributeReference>(item)!;
            if (attRef.Tag == valueName)
                return attRef.TextString;
        }

        return null;
    }

    /// <summary>
    /// 添加带属性的块
    /// </summary>
    /// <param name="space"></param>
    /// <param name="pt"></param>
    /// <param name="btrid"></param>
    /// <param name="scale"></param>
    /// <param name="rotation"></param>
    /// <param name="atts"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static BlockReference AddAttBlockReference(this BlockTableRecord space, Point3d pt, ObjectId btrid,
        Scale3d scale = default, double rotation = default, Dictionary<string, string>? atts = default,
        Transaction? trans = null)
    {
        var db = space.Database;
        trans ??= db.TransactionManager.TopTransaction;
        var brf = new BlockReference(pt, btrid) { ScaleFactors = scale, Rotation = rotation };
        space.UpgradeOpen();
        space.AppendEntity(brf);
        trans.AddNewlyCreatedDBObject(brf, true);
        space.DowngradeOpen();
        if (trans.GetObject(btrid, OpenMode.ForRead) is not BlockTableRecord btr)
            throw new("id出错");
        if (atts != null && btr.HasAttributeDefinitions)
        {
            foreach (var item in btr)
            {
                var obj = trans.GetObject(item, OpenMode.ForRead);
                if (obj is AttributeDefinition { Constant: false } attdef)
                {
                    using AttributeReference attref = new();
                    attdef.UpgradeOpen();
                    attref.SetAttributeFromBlock(attdef, brf.BlockTransform);
                    attref.Position = attdef.Position.TransformBy(brf.BlockTransform);
                    attref.AdjustAlignment(db);
                    if (atts.TryGetValue(attdef.Tag, value: out var att))
                    {
                        attref.TextString = att;
                    }

                    brf.AttributeCollection.AppendAttribute(attref);
                    trans.AddNewlyCreatedDBObject(attref, true);
                }
            }
        }

        return brf;
    }
}

public static class ExplodeTool
{
    private static readonly List<Entity> explodeList = new();

    private static void ExplodeEnd(this Entity ent)
    {
        var dbc = new DBObjectCollection();
        if (!ent.Visible) return;
        if (ent is DBText dbt && dbt.TextString.Trim() == "") return;
        try
        {
            ent.Explode(dbc);
            if (dbc.Count > 0)
            {
                foreach (var obj in dbc)
                {
                    if (obj is Entity ent1)
                    {
                        if (ent is DBText)
                            continue;
                        ent1.ExplodeEnd();
                    }
                }
            }
            else
            {
                explodeList.Add(ent);
            }
        }
        catch
        {
            explodeList.Add(ent);
        }
    }

    /// <summary>
    /// 获取炸到底图元列表
    /// </summary>
    /// <param name="ent"></param>
    /// <returns></returns>
    public static List<Entity> GetExplodeEndEntities(this Entity ent)
    {
        //explodeList = new List<Entity>();
        explodeList.Clear();
        ent.ExplodeEnd();
        return explodeList;
    }

    public static List<Entity> Explode(this Entity ent)
    {
        using var dboc = new DBObjectCollection();
        var entList = new List<Entity>();
        try
        {
            ent.Explode(dboc);
        }
        catch
        {
            // ignored
        }

        if (dboc.Count == 0)
            return [ent.CloneEx()];
        foreach (var item in dboc)
        {
            if (item is Entity ent2)
            {
                entList.Add(ent2);
            }
        }

        return entList;
    }
}

public static class JigExTransientTool
{
    public static void Add(this JigExTransient jet, params Entity[] ents)
    {
        foreach (var ent in ents)
        {
            jet.Add(ent);
        }
    }

    public static void Add(this JigExTransient jet, TransientDrawingMode tdm, params Entity[] ents)
    {
        foreach (var ent in ents)
        {
            jet.Add(ent, tdm);
        }
    }
}
/// <summary>
/// 多重引线工具
/// </summary>
public static class MLeaderTool
{
    /// <summary>
    /// 设置最后的坐标
    /// </summary>
    /// <param name="ml"></param>
    /// <param name="pt"></param>
    /// <param name="index"></param>
    public static void SetLastVertex(this MLeader ml, Point3d pt, int index = 0)
    {
        var targetPoint = ml.GetFirstVertex(0);
        var newPt = new Point3d(pt.X, pt.Y, targetPoint.Z);
        ml.SetLastVertex(index, newPt);
        ml.SetDogleg(index, newPt.X >= targetPoint.X ? Vector3d.XAxis : -Vector3d.XAxis);
    }
}