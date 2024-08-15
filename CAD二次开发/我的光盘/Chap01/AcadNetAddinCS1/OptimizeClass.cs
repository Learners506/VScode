#region Namespaces
using System.Diagnostics;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Application;
#endregion

namespace AcadNetAddinCS1
{
    public class OptimizeClass
    {
        [CommandMethod("CmdGroup1", "OptCommand", null, CommandFlags.Modal, null, "AcadNetAddinCS1", "OptCommand")]
        public void OptCommand_Method()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;            
            try
            {
                string fileName = "C:\\Hello.dll";// Hello.dll程序集的文件名
                ExtensionLoader.Load(fileName); // 载入Hello.dll程序集
                // 在命令行上显示信息，提示用户Hello.dll程序集已经被载入
                ed.WriteMessage("\n" + fileName + "被载入，请输入Hello进行测试！");
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                ed.WriteMessage(ex.ToString());
            }
        }
    }
}
