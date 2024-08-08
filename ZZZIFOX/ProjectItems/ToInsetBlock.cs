using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.ProjectItems
{
    public static class ToInsetBlock
    {
        [CommandMethod(nameof(trz))]
        public static void trz() 
        {
            using var tr = new DBTrans();
            var blockref = Env.Editor.GetEntity("\n请选择要插入的图块类型");
            if (blockref.Status != PromptStatus.OK)
            {
                return;
            }
            var blockName = tr.GetObject<BlockReference>(blockref.ObjectId).Name;
            
            var xzs = new PromptSelectionOptions()
            {
                MessageForAdding = "\n请选择要添加桩的范围"
            };
            var fil = new SelectionFilter(new TypedValue[]
            {
                new TypedValue((int)DxfCode.Start,"LWPOLYLINE")
            });
            var r1 = Env.Editor.GetSelection(xzs, fil);
            if (r1.Status != PromptStatus.OK) return;
            var selectionset = r1.Value;
            List<Point3d> polylines = new List<Point3d>();
            foreach (var polyid in selectionset.GetObjectIds())
            {
                var obj = (Polyline)tr.GetObject(polyid);
                if (obj.Closed)
                {
                    var objpt = obj.getcenterpoint();
                    polylines.Add(objpt);
                }
            }
            polylines.Sort((c1, c2) =>
            {
                int compareX = c1.X.CompareTo(c2.X);
                if (compareX == 0)
                {
                    return c1.Y.CompareTo(c2.Y);
                }
                return compareX;
            });
            double maxDistance = 6.0;
            for (int i = 0; i < polylines.Count - 1; i++)
            {
                var currentpoint = polylines[i];
                var nextpoint = polylines[i + 1];
                double distance = currentpoint.DistanceTo(nextpoint);
                if (distance > maxDistance)
                {
                    var midPoint = new Point3d(
                        (currentpoint.X + nextpoint.X) / 2,
                        (currentpoint.Y + nextpoint.Y) / 2,
                        (currentpoint.Z + nextpoint.Z) / 2);

                    var rstring = Env.Editor.GetString("\n请输入旋转角度，1为向上，2为向左，3为向下，4为向右");
                    double rotate = 0;
                    if (rstring.Status == PromptStatus.OK)
                    {
                        switch (rstring.StringResult)
                        {
                            case "1":
                                rotate = 0;
                                break;
                            case "2":
                                rotate = Math.PI/2;
                                break;
                            case "3":
                                rotate = Math.PI;
                                break;
                            case "4":
                                rotate = Math.PI * 3 / 2;
                                break;
                            default:
                                break;
                        }
                    }
                    tr.CurrentSpace.InsertBlock(midPoint, blockName,rotation:rotate);
                }
            }







        }

        public static Point3d getcenterpoint(this Polyline polyline)
        {
            // 计算多段线顶点的平均值作为中心点
            int numVertices = polyline.NumberOfVertices;
            if (numVertices == 0)
            {
                return Point3d.Origin; // 如果多段线没有顶点，返回原点
            }

            double sumX = 0.0;
            double sumY = 0.0;
            double sumZ = 0.0;

            for (int i = 0; i < numVertices; i++)
            {
                Point3d vertex = polyline.GetPoint3dAt(i);
                sumX += vertex.X;
                sumY += vertex.Y;
                sumZ += vertex.Z;
            }

            double centerX = sumX / numVertices;
            double centerY = sumY / numVertices;
            double centerZ = sumZ / numVertices;

            return new Point3d(centerX, centerY, centerZ);
        }
    }
}
