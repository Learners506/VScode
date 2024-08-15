using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
namespace InitAndOpt
{
    public class OptimizeClass
    {
        [CommandMethod("OptCommand")]
        public void OptCommand()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string fileName = "C:\\Hello.dll";// Hello.dll程序集的文件名
            ExtensionLoader.Load(fileName); // 载入Hello.dll程序集
            // 在命令行上显示信息，提示用户Hello.dll程序集已经被载入
            ed.WriteMessage("\n" + fileName + "被载入，请输入Hello进行测试！");
        }
    }
}
