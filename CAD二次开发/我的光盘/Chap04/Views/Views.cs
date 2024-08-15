using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using DotNetARX;
using Autodesk.AutoCAD.Geometry;
namespace Views
{
    public class Views
    {
        [CommandMethod("ZoomScaled")]
        public void ZoomScaled()
        {
            Editor ed=Application.DocumentManager.MdiActiveDocument.Editor;
            ed.ZoomScaled(2);//放大视图为2倍
        }
        [CommandMethod("ZoomWindow")]
        public void ZoomWindow()
        {
            Editor ed=Application.DocumentManager.MdiActiveDocument.Editor;
            //提示用户选择定义缩放窗口的两个角点
            PromptPointResult pt1Result=ed.GetPoint("输入第一个角点");
            if (pt1Result.Status != PromptStatus.OK) return;
            PromptPointResult pt2Result=ed.GetPoint("输入第二个角点");
            if (pt2Result.Status != PromptStatus.OK) return;
            //缩放视图为由两个角点构成的区域
            ed.ZoomWindow(pt1Result.Value, pt2Result.Value);
            //下面一行调用Zoom函数，与上一行的代码效果相同
            //ed.Zoom(pt1Result.Value, pt2Result.Value, Point3d.Origin, 1);
        }
        [CommandMethod("ZoomObject")]
        public void ZoomObject()
        {
            Editor ed=Application.DocumentManager.MdiActiveDocument.Editor;
            //提示用户选择对象
            PromptEntityResult result=ed.GetEntity("请选择对象");
            if (result.Status != PromptStatus.OK) return;
            //根据对象的范围显示视图
            ed.ZoomObject(result.ObjectId);
        }
        [CommandMethod("ZoomExt")]
        public void ZoomExt()
        {
            Editor ed=Application.DocumentManager.MdiActiveDocument.Editor;
            ed.ZoomExtents();
            //下面一行调用Zoom函数，与上一行的代码效果相同
            //ed.Zoom(Point3d.Origin, Point3d.Origin, Point3d.Origin, 1);
        }
    }
}
