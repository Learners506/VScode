namespace DYH.Tools;

public interface IScanSort
{
    /// <summary>
    /// 中心
    /// </summary>
    public Point3d Center { get; }
    /// <summary>
    /// 宽
    /// </summary>
    public double Width { get; }
    /// <summary>
    /// 高
    /// </summary>
    public double Height { get; }
}
public static class ScanSortTool
{
    /// <summary>
    /// 扫掠排序
    /// </summary>
    /// <param name="source">数据</param>
    /// <param name="scanSortType">排序方式</param>
    /// <param name="tolerance">容差</param>
    /// <typeparam name="T">类型</typeparam>
    /// <returns>排序后的列表</returns>
    public static List<List<T>> ScanSort<T>(this IEnumerable<T> source, ScanSortType scanSortType, double tolerance = 0) where T : IScanSort
    {
        List<List<T>> result = new List<List<T>>();
        tol = tolerance;
        if (scanSortType.HasFlag(ScanSortType.VerticalFirst))
        {
            var orderList = scanSortType.HasFlag(ScanSortType.UpFirst)
                ? source.OrderByDescending(e => e.Center.Y + e.Height * 0.5).ToList()
                : source.OrderBy(e => e.Center.Y - e.Height * 0.5).ToList();
            while (orderList.Any())
            {
                var item1 = orderList[0];
                var subList1 = new List<T>() { item1 };
                orderList.RemoveAt(0);
                while (orderList.Any())
                {
                    var item2 = orderList[0];
                    if (IsHorizontalCrash(item1, item2))
                    {
                        subList1.Add(item2);
                        orderList.RemoveAt(0);
                    }
                    else break;
                }

                var subOrderList = scanSortType.HasFlag(ScanSortType.LeftFirst)
                    ? subList1.OrderBy(e => e.Center.X).ToList()
                    : subList1.OrderByDescending(e => e.Center.X).ToList();
                result.Add(subOrderList);
            }
        }
        else
        {
            var orderList = scanSortType.HasFlag(ScanSortType.LeftFirst)
                ? source.OrderBy(e => e.Center.X - e.Width * 0.5).ToList()
                : source.OrderByDescending(e => e.Center.X + e.Width * 0.5).ToList();
            while (orderList.Any())
            {
                var item1 = orderList[0];
                var subList1 = new List<T>() { item1 };
                orderList.RemoveAt(0);
                while (orderList.Any())
                {
                    var item2 = orderList[0];
                    if (IsVerticalCrash(item1, item2))
                    {
                        subList1.Add(item2);
                        orderList.RemoveAt(0);
                    }
                    else break;
                }

                var subOrderList = scanSortType.HasFlag(ScanSortType.UpFirst)
                    ? subList1.OrderByDescending(e => e.Center.Y).ToList()
                    : subList1.OrderBy(e => e.Center.Y).ToList();
                result.Add(subOrderList);
            }
        }
        return result;
    }
    private static double tol = 0;

    private static bool IsHorizontalCrash(IScanSort a, IScanSort b)
    {
        return Math.Abs(a.Center.Y - b.Center.Y) <= (a.Height + b.Height) * 0.5 + tol;
    }

    private static bool IsVerticalCrash(IScanSort a, IScanSort b)
    {
        return Math.Abs(a.Center.X - b.Center.X) <= (a.Width + b.Width) * 0.5 + tol;
    }
}

[Flags]
public enum ScanSortType
{
    VerticalFirst = 0b001, // 1位表示垂直优先还是水平优先
    UpFirst = 0b010, // 2位表示上优先还是下优先
    LeftFirst = 0b100, // 3位表示左优先还是右优先
    UpDownLeftRight = 0b111, // 上下左右
    DownUpLeftRight = 0b101, // 下上左右
    UpDownRightLeft = 0b011, // 上下右左
    DownUpRightLeft = 0b001, // 下上右左
    LeftRightUpDown = 0b110, // 左右上下
    LeftRightDownUp = 0b100, // 左右下上
    RightLeftUpDown = 0b010, // 右左上下
    RightLeftDownUp = 0b000, // 右左下上
}