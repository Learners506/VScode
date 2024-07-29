//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project
//{
//    public static class demo10
//    {
//        [CommandMethod(nameof(CS9))]
//        public static void CS9()
//        {
//            using var tr = new DBTrans();
//            // 获取桌面文件
//            var filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Drawing1.dwg";
//            // 获取桌面文件中名为123的块
//            var btrId = tr.BlockTable.GetBlockFrom(filename, "123", false);
//            var r1 = Env.Editor.GetPoint("\n请选择点");
//            if (r1.Status != PromptStatus.OK) return;
//            tr.CurrentSpace.InsertBlock(r1.Value.Ucs2Wcs(), btrId);
//        }
//    }
//}
