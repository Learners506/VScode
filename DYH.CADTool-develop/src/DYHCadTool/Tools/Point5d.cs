namespace DYH.Tools;
public struct Point5d
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    public Point5d(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public static implicit operator Point5d(Point3d point3d)
    {
        return new Point5d(point3d.X, point3d.Y, point3d.Z);
    }
    public static implicit operator Point5d(Point2d point2d)
    {
        return new Point5d(point2d.X, point2d.Y, 0);
    }
    public static implicit operator Point3d(Point5d point5d)
    {
        return new Point3d(point5d.X, point5d.Y, point5d.Z);
    }
    public static implicit operator Point2d(Point5d point5d)
    {
        return new Point2d(point5d.X, point5d.Y);
    }
    public static bool operator ==(Point5d left, Point5d right)
    {
        return (Point2d)left == (Point2d)right;
    }
    public static bool operator !=(Point5d left, Point5d right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Point5d p5d && p5d == this;
    }

    public override int GetHashCode()
    {
        return ((Point2d)this).GetHashCode();
    }

    public override string ToString()
    {
        return ((Point3d)this).ToString();
    }
}
