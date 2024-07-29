#if acad
using Autodesk.AutoCAD.BoundaryRepresentation;

//using Region = Autodesk.AutoCAD.DatabaseServices.Region;

namespace DYH.Tools;

public static class RegionTool
{
    /// <summary>
    /// 面域转曲线
    /// </summary>
    /// <param name="region"></param>
    /// <returns></returns>
    public static IEnumerable<Curve> GetCurves(this Region region)
    {
        using var brep = new Brep(region);


        var loops = brep.Complexes
            .SelectMany(complex => complex.Shells)
            .SelectMany(shell => shell.Faces)
            .SelectMany(face => face.Loops);
        foreach (var loop in loops)
        {
            var curves3d = loop.Edges.Select(edge => ((ExternalCurve3d)edge.Curve).NativeCurve);
            if (1 < curves3d.Count())
            {
                if (curves3d.All(curve3d => curve3d is CircularArc3d || curve3d is LineSegment3d))
                {
                    var pline = (Polyline)Curve.CreateFromGeCurve(new CompositeCurve3d(curves3d.ToOrderedArray()));
                    pline.Closed = true;
                    yield return pline;
                }
                else
                {
                    foreach (var curve3d in curves3d) yield return Curve.CreateFromGeCurve(curve3d);
                }
            }
            else
            {
                yield return Curve.CreateFromGeCurve(curves3d.First());
            }
        }
    }

    //按首尾相连对曲线集合进行排序
    public static Curve3d[] ToOrderedArray(this IEnumerable<Curve3d> source)
    {
        var tol = new Tolerance(0.001, 0.001);
        var list = source.ToList();
        var count = list.Count;
        var array = new Curve3d[count];
        var i = 0;
        array[0] = list[0];
        list.RemoveAt(0);
        int index;
        while (i < count - 1)
        {
            var pt = array[i++].EndPoint;
            if ((index = list.FindIndex(c => c.StartPoint.IsEqualTo(pt, tol))) != -1)
                array[i] = list[index];
            else if ((index = list.FindIndex(c => c.EndPoint.IsEqualTo(pt, tol))) != -1)
                array[i] = list[index].GetReverseParameterCurve();
            else
                throw new ArgumentException("非连续曲线.");
            list.RemoveAt(index);
        }

        return array;
    }
}

#endif