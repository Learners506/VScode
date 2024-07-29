//using Autodesk.AutoCAD.Windows;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project
//{
//    public class WPFCAD
//    {
//        // 创建一个面板
//        private static PaletteSet pset;
//        [CommandMethod(nameof(wpfl))]
//        public void wpfl()
//        {
            
//            // 判断一下面板是不是存在，不存在的话就创建一下
//            if(pset is null)
//            {
//                // 为空的话就创建一个菜单面板，并往面板中添加一个按钮
//                pset = new PaletteSet("菜单");
//                pset.Add("aaa", new Button() { Text = "按钮" });
//                pset.Visible = true;
//            }

//        }
//    }
//}
