namespace DYH.Tools;

/// <summary>
/// 获取最佳包围矩形
/// </summary>
public static class BestBox
{
    /// <summary>
    /// 获取单行文字最佳包围点
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static List<Point3d> GetBestBoxPoints(this DBText text)
    {
        var angle = text.Rotation % (Math.PI * 0.5);
        if (Math.Abs(angle - Math.PI * 0.25) < 1e-6)
        {
            var textnew = text.GetTransformedCopy(Matrix3d.Rotation(1e-6, Vector3d.ZAxis, text.Position)) as DBText;
            return textnew!.GetBestBoxPoints();
        }
        var ratio = Math.Abs(Math.Tan(angle));
        var box = text.GetBoundingBoxEx()!.Value;
        var w = box.MaxX - box.MinX;
        var h = box.MaxY - box.MinY;
        var a = (w * ratio - h) / (ratio * ratio - 1);
        var b = (w - ratio * h) / (1 - ratio * ratio);
        if (a < 0 || b < 0)
        {
            var textnew = text.GetTransformedCopy(Matrix3d.Rotation(1e-6, Vector3d.ZAxis, text.Position)) as DBText;
            return textnew!.GetBestBoxPoints();
        }
        var pt1 = new Point3d(box.MaxX - b, box.MinY, 0);
        var pt2 = new Point3d(box.MaxX, box.MaxY - a, 0);
        var pt3 = new Point3d(box.MinX + b, box.MaxY, 0);
        var pt4 = new Point3d(box.MinX, box.MinY + a, 0);
        var fx = Convert.ToInt32(Math.Floor(text.Rotation / (Math.PI * 0.5))) % 4;
        return fx switch
        {
            1 => new() { pt2, pt3, pt4, pt1 },
            2 => new() { pt3, pt4, pt1, pt2 },
            3 => new() { pt4, pt1, pt2, pt3 },
            _ => new() { pt1, pt2, pt3, pt4 }
        };
    }
    /// <summary>
    /// 获取多行文字最佳包围点
    /// </summary>
    /// <param name="mtext">多行文字</param>
    /// <returns>含有4个坐标的表</returns>
    public static List<Point3d> GetBestBoxPoints(this MText mtext)
    {
        var width = mtext.ActualWidth;
        var height = mtext.ActualHeight;
        var location = mtext.Location;
        Point3d? corner1 = null;
        switch (mtext.Attachment)
        {
            case AttachmentPoint.TopLeft:
                corner1 = location + new Vector3d(0, -height, 0);
                break;
            case AttachmentPoint.TopCenter:
                corner1 = location + new Vector3d(-width * 0.5, -height, 0);
                break;
            case AttachmentPoint.TopRight:
                corner1 = location + new Vector3d(-width, -height, 0);
                break;
            case AttachmentPoint.MiddleLeft:
                corner1 = location + new Vector3d(0, -height * 0.5, 0);
                break;
            case AttachmentPoint.MiddleCenter:
                corner1 = location + new Vector3d(-width * 0.5, -height * 0.5, 0);
                break;
            case AttachmentPoint.MiddleRight:
                corner1 = location + new Vector3d(-width, -height * 0.5, 0);
                break;
            case AttachmentPoint.BottomLeft:
                corner1 = location;
                break;
            case AttachmentPoint.BottomCenter:
                corner1 = location + new Vector3d(-width * 0.5, 0, 0);
                break;
            case AttachmentPoint.BottomRight:
                corner1 = location + new Vector3d(-width, 0, 0);
                break;
        }

        if (corner1 is null)
            throw new("\nMText获取box错误");

        var corner2 = corner1.Value + new Vector3d(width, height, 0);
        var mt3d = Matrix3d.Rotation(mtext.Rotation, Vector3d.ZAxis, location);
        return corner1.Value.GetRecPoint3ds(corner2).Select(pt => pt.TransformBy(mt3d)).ToList();
    }
    public static void Move(this DBText text,int index,Point3d pt)
    {
        var pts = text.GetBestBoxPoints();
        Point3d target = index switch
        {
            1 => pts[3],
            2 => pts[2].GetMidPointTo(pts[3]),
            3 => pts[2],
            4 => pts[0].GetMidPointTo(pts[3]),
            5 => pts[0].GetMidPointTo(pts[2]),
            6 => pts[1].GetMidPointTo(pts[2]),
            7 => pts[0],
            8 => pts[0].GetMidPointTo(pts[1]),
            9 => pts[1],
            _ => pts[0],
        };
        text.Move(target, pt);
    }
}
