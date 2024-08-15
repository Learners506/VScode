using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using DotNetARX;
namespace NetSelection
{
    public class NetSelection
    {
        [CommandMethod("TestGetSelect")]
        public static void TestGetSelect()
        {
            Document doc=Application.DocumentManager.MdiActiveDocument;
            Database db=doc.Database;
            Editor ed=doc.Editor;
            //生成三个同心圆并添加到当前模型空间
            Circle cir1=new Circle(Point3d.Origin, Vector3d.ZAxis, 10);
            Circle cir2=new Circle(Point3d.Origin, Vector3d.ZAxis, 20);
            Circle cir3=new Circle(Point3d.Origin, Vector3d.ZAxis, 30);
            db.AddToModelSpace(new Circle[] { cir1, cir2, cir3 });
            //提示用户选择对象
            PromptSelectionResult psr = ed.GetSelection();
            if (psr.Status != PromptStatus.OK) return;//如果未选择，则返回
            //获取选择集
            SelectionSet ss=psr.Value;
            //信息提示框，给出选择集中包含实体个数的提示
            Application.ShowAlertDialog("选择集中实体的数量：" + ss.Count.ToString());
        }

        [CommandMethod("MergeSelection")]
        public static void MergeSelection()
        {
            Document doc=Application.DocumentManager.MdiActiveDocument;
            Editor ed=doc.Editor;
            //第一次选择
            PromptSelectionResult ss1=ed.GetSelection();
            if (ss1.Status != PromptStatus.OK) return; //若选择不成功，返回
            Application.ShowAlertDialog("第一个选择集中实体的数量：" + ss1.Value.Count.ToString());
            //第二次选择
            PromptSelectionResult ss2=ed.GetSelection();
            if (ss2.Status != PromptStatus.OK) return;
            Application.ShowAlertDialog("第二个选择集中实体的数量：" + ss2.Value.Count.ToString());
            //第二个选择集的ObjectId加入到第一个选择集中
            var ss3=ss1.Value.GetObjectIds().Union(ss2.Value.GetObjectIds());
            Application.ShowAlertDialog("合并后选择集中实体的数量：" + ss3.Count().ToString());
        }

        [CommandMethod("DelFromSelection")]
        public static void DelFromSelection()
        {
            Document doc=Application.DocumentManager.MdiActiveDocument;
            Editor ed=doc.Editor;
            //第一次选择
            PromptSelectionResult ss1=ed.GetSelection();
            if (ss1.Status != PromptStatus.OK) return; //若选择不成功，返回
            Application.ShowAlertDialog("第一个选择集中实体的数量：" + ss1.Value.Count.ToString());
            //第二次选择
            PromptSelectionResult ss2=ed.GetSelection();
            if (ss2.Status != PromptStatus.OK) return;
            Application.ShowAlertDialog("第二个选择集中实体的数量：" + ss2.Value.Count.ToString());
            //若第二次选择的实体位于第一个选择集中，则删除该实体的ObjectId
            var ss3=ss1.Value.GetObjectIds().Except(ss2.Value.GetObjectIds());
            Application.ShowAlertDialog("删除第二个选择集后第一个选择集中实体的数量：" + ss3.Count().ToString());
        }

        [CommandMethod("TestPickFirst", CommandFlags.UsePickSet)]
        public static void TestPickFirst()
        {
            Document doc=Application.DocumentManager.MdiActiveDocument;
            Editor ed=doc.Editor;
            //获取当前已选择的实体
            PromptSelectionResult psr=ed.SelectImplied();
            //在命令发出前已有实体被选中
            if (psr.Status == PromptStatus.OK)
            {
                SelectionSet ss1=psr.Value; //获取选择集
                //显示当前已选择的实体个数
                Application.ShowAlertDialog("PickFirst示例：当前已选择的实体个数：" + ss1.Count.ToString());
                //清空当前选择集
                ed.SetImpliedSelection(new ObjectId[0]);
                psr = ed.GetSelection();//提示用户进行新的选择
                if (psr.Status == PromptStatus.OK)
                {
                    //设置当前已选择的实体
                    ed.SetImpliedSelection(psr.Value.GetObjectIds());
                    SelectionSet ss2=psr.Value;
                    Application.ShowAlertDialog("PickFirst示例：当前已选择的实体个数：" + ss2.Count.ToString());
                }
            }
            else
            {
                Application.ShowAlertDialog("PickFirst示例：当前已选择的实体个数：0");
            }
        }

        [CommandMethod("TestPolygonSelect")]
        public static void TestPolygonSelect()
        {
            Document doc=Application.DocumentManager.MdiActiveDocument;
            Editor ed=doc.Editor;
            //声明一个Point3d类列表对象，用于存储多段线的顶点
            Point3dList pts=new Point3dList();
            //提示用户选择多段线
            PromptEntityResult per=ed.GetEntity("请选择多段线");
            if (per.Status != PromptStatus.OK) return;//选择错误，返回
            using (Transaction trans=doc.TransactionManager.StartTransaction())
            {
                //转换为Polyline对象
                Polyline pline=trans.GetObject(per.ObjectId, OpenMode.ForRead) as Polyline;
                if (pline != null)
                {
                    //遍历所选多段线的顶点并添加到Point3d类列表
                    for (int i = 0; i < pline.NumberOfVertices; i++)
                    {
                        Point3d point=pline.GetPoint3dAt(i);
                        pts.Add(point);
                    }
                    //窗口选择，仅选择完全位于多边形区域中的对象
                    PromptSelectionResult psr=ed.SelectWindowPolygon(pts);
                    if (psr.Status == PromptStatus.OK)
                    {
                        Application.ShowAlertDialog("选择集中实体的数量：" + psr.Value.Count.ToString());
                    }
                }
                trans.Commit();
            }
        }

        [CommandMethod("TestSelectException")]
        public static void TestSelectException()
        {
            Document doc=Application.DocumentManager.MdiActiveDocument;
            Editor ed=doc.Editor;
            Point3d pt1=Point3d.Origin;
            Point3d pt2=new Point3d(100, 100, 0);
            //交叉窗口选择，选择由pt1和pt2组成的矩形窗口包围的或相交的对象
            PromptSelectionResult psr=ed.SelectCrossingWindow(pt1, pt2);
            if (psr.Status == PromptStatus.OK)
            {
                Application.ShowAlertDialog("选择集中实体的数量：" + psr.Value.Count.ToString());
            }
        }

        [CommandMethod("TestFilter")]
        public static void TestFilter()
        {
            Document doc=Application.DocumentManager.MdiActiveDocument;
            Editor ed=doc.Editor;
            //创建一个自定义的TypedValue列表对象，用于构建过滤器列表
            TypedValueList values=new TypedValueList();
            //选择图层1上的直线对象
            values.Add(DxfCode.LayerName, "图层1");
            values.Add(typeof(Line));
            //构建过滤器列表，注意这里使用自定义类型转换
            SelectionFilter filter=new SelectionFilter(values);
            //选择图形中所有满足过滤器的对象，即位于图层1上的直线
            PromptSelectionResult psr=ed.SelectAll(filter);
            if (psr.Status == PromptStatus.OK)
            {
                Application.ShowAlertDialog("选择集中实体的数量：" + psr.Value.Count.ToString());
            }
        }
    }
}
