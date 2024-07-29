namespace DYH.Tools;

public static class PointTool
{
    public static double Distance2dTo(this Point3d pt1, Point5d pt2)
    {
        return new Vector2d(pt2.X - pt1.X, pt2.Y - pt1.Y).Length;
    }
    public static double Distance2dTo(this Point2d pt1, Point5d pt2)
    {
        return new Vector2d(pt2.X - pt1.X, pt2.Y - pt1.Y).Length;
    }
    public static List<Point2d> GetRecPoint2ds(this Point3d corner1, Point5d corner2)
    {
        var XMin = Math.Min(corner1.X, corner2.X);
        var XMax = Math.Max(corner1.X, corner2.X);
        var YMin = Math.Min(corner1.Y, corner2.Y);
        var YMax = Math.Max(corner1.Y, corner2.Y);
        var pt1 = new Point2d(XMin, YMin);
        var pt2 = new Point2d(XMax, YMin);
        var pt3 = new Point2d(XMax, YMax);
        var pt4 = new Point2d(XMin, YMax);
        return new List<Point2d> { pt1, pt2, pt3, pt4 };
    }
    public static List<Point3d> GetRecPoint3ds(this Point3d corner1, Point5d corner2, double z = 0)
    {
        var XMin = Math.Min(corner1.X, corner2.X);
        var XMax = Math.Max(corner1.X, corner2.X);
        var YMin = Math.Min(corner1.Y, corner2.Y);
        var YMax = Math.Max(corner1.Y, corner2.Y);
        var pt1 = new Point3d(XMin, YMin, z);
        var pt2 = new Point3d(XMax, YMin, z);
        var pt3 = new Point3d(XMax, YMax, z);
        var pt4 = new Point3d(XMin, YMax, z);
        return new List<Point3d> { pt1, pt2, pt3, pt4 };
    }
    public static List<Point2d> GetRecPoint2ds(this Point2d corner1, Point5d corner2)
    {
        var XMin = Math.Min(corner1.X, corner2.X);
        var XMax = Math.Max(corner1.X, corner2.X);
        var YMin = Math.Min(corner1.Y, corner2.Y);
        var YMax = Math.Max(corner1.Y, corner2.Y);
        var pt1 = new Point2d(XMin, YMin);
        var pt2 = new Point2d(XMax, YMin);
        var pt3 = new Point2d(XMax, YMax);
        var pt4 = new Point2d(XMin, YMax);
        return new List<Point2d> { pt1, pt2, pt3, pt4 };
    }
    public static List<Point3d> GetRecPoint3ds(this Point2d corner1, Point5d corner2, double z = 0)
    {
        var XMin = Math.Min(corner1.X, corner2.X);
        var XMax = Math.Max(corner1.X, corner2.X);
        var YMin = Math.Min(corner1.Y, corner2.Y);
        var YMax = Math.Max(corner1.Y, corner2.Y);
        var pt1 = new Point3d(XMin, YMin, z);
        var pt2 = new Point3d(XMax, YMin, z);
        var pt3 = new Point3d(XMax, YMax, z);
        var pt4 = new Point3d(XMin, YMax, z);
        return new List<Point3d> { pt1, pt2, pt3, pt4 };
    }
    public static Rect GetRect(this Point3d point, double offsetDist = 10)
    {
        var pt = point.Point2d();
        var v2d = new Vector2d(offsetDist, offsetDist);
        return new Rect(pt - v2d, pt + v2d);
    }
}