namespace ZZZIFOX
{
    public static class Command
    {


        // 右边朝向
        [CommandMethod(nameof(r1right))]
        public static void r1right()
        {
            using var tr = new DBTrans();
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
                var obj = (Polyline)tr.GetObject(polyid, OpenMode.ForWrite);
                if (obj.Closed)
                {
                    var objpt = obj.getcenterpoint();
                    polylines.Add(objpt);
                    obj.Erase();
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
            // 新建图层
            tr.LayerTable.Add("支架3", it =>
            {
                it.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                it.LineWeight = LineWeight.LineWeight030;
                it.IsPlottable = true;
            });


            for (int i = 0; i < polylines.Count; i++)
            {

                var midPoint = polylines[i];
                var blockid = tr.BlockTable.GetBlockFrom(@"D:\工作记录\20240729补桩\hhhhh.dwg", false);
                var blockRef = new BlockReference(midPoint, blockid);
                blockRef.Layer = "支架3";
                tr.CurrentSpace.AddEntity(blockRef);
            }

        }

        // 上面
        [CommandMethod(nameof(r1up))]
        public static void r1up()
        {
            using var tr = new DBTrans();
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
                var obj = (Polyline)tr.GetObject(polyid, OpenMode.ForWrite);
                if (obj.Closed)
                {
                    var objpt = obj.getcenterpoint();
                    polylines.Add(objpt);
                    obj.Erase();
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
            // 新建图层
            tr.LayerTable.Add("支架3", it =>
            {
                it.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                it.LineWeight = LineWeight.LineWeight030;
                it.IsPlottable = true;
            });


            for (int i = 0; i < polylines.Count; i++)
            {

                var midPoint = polylines[i];
                var blockid = tr.BlockTable.GetBlockFrom(@"D:\工作记录\20240729补桩\hhhhh.dwg", false);
                var blockRef = new BlockReference(midPoint, blockid);
                blockRef.Layer = "支架3";
                blockRef.TransformBy(Matrix3d.Rotation(Math.PI / 2, Vector3d.ZAxis, midPoint));
                tr.CurrentSpace.AddEntity(blockRef);
            }

        }

        // 左面
        [CommandMethod(nameof(r1left))]
        public static void r1left()
        {
            using var tr = new DBTrans();
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
                var obj = (Polyline)tr.GetObject(polyid, OpenMode.ForWrite);
                if (obj.Closed)
                {
                    var objpt = obj.getcenterpoint();
                    polylines.Add(objpt);
                    obj.Erase();
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
            // 新建图层
            tr.LayerTable.Add("支架3", it =>
            {
                it.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                it.LineWeight = LineWeight.LineWeight030;
                it.IsPlottable = true;
            });


            for (int i = 0; i < polylines.Count; i++)
            {

                var midPoint = polylines[i];
                var blockid = tr.BlockTable.GetBlockFrom(@"D:\工作记录\20240729补桩\hhhhh.dwg", false);
                var blockRef = new BlockReference(midPoint, blockid);
                blockRef.Layer = "支架3";
                blockRef.TransformBy(Matrix3d.Rotation(2*Math.PI / 2, Vector3d.ZAxis, midPoint));
                tr.CurrentSpace.AddEntity(blockRef);
            }

        }

        // 下面
        [CommandMethod(nameof(r1down))]
        public static void r1down()
        {
            using var tr = new DBTrans();
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
                var obj = (Polyline)tr.GetObject(polyid, OpenMode.ForWrite);
                if (obj.Closed)
                {
                    var objpt = obj.getcenterpoint();
                    polylines.Add(objpt);
                    obj.Erase();
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
            // 新建图层
            tr.LayerTable.Add("支架3", it =>
            {
                it.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                it.LineWeight = LineWeight.LineWeight030;
                it.IsPlottable = true;
            });


            for (int i = 0; i < polylines.Count; i++)
            {

                var midPoint = polylines[i];
                var blockid = tr.BlockTable.GetBlockFrom(@"D:\工作记录\20240729补桩\hhhhh.dwg", false);
                var blockRef = new BlockReference(midPoint, blockid);
                blockRef.Layer = "支架3";
                blockRef.TransformBy(Matrix3d.Rotation(3 * Math.PI / 2, Vector3d.ZAxis, midPoint));
                tr.CurrentSpace.AddEntity(blockRef);
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
