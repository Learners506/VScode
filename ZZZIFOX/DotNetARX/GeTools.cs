using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.DotNetARX
{
    // 几何计算类
    public static class GeTools
    {
        /// <summary>
        /// 获取两个点之间的中点
        /// </summary>
        /// <param name="pt1">第一点</param>
        /// <param name="pt2">第二点</param>
        /// <returns>返回两个点之间的中点</returns>
        public static Point3d MidPoint(Point3d pt1, Point3d pt2)
        {
            Point3d midPoint = new Point3d((pt1.X + pt2.X) / 2.0,
                                        (pt1.Y + pt2.Y) / 2.0,
                                        (pt1.Z + pt2.Z) / 2.0);
            return midPoint;
        }


        /// <summary>
        /// 计算从第一点到第二点所确定的矢量与X轴正方向的夹角
        /// </summary>
        /// <param name="pt1">第一点</param>
        /// <param name="pt2">第二点</param>
        /// <returns>返回两点所确定的矢量与X轴正方向的夹角</returns>
        public static double AngleFromXAxis(this Point3d pt1, Point3d pt2)
        {
            //构建一个从第一点到第二点所确定的矢量
            Vector2d vector = new Vector2d(pt1.X - pt2.X, pt1.Y - pt2.Y);
            //返回该矢量和X轴正半轴的角度（弧度）
            return vector.Angle;
        }

    }
}
