//using Autodesk.AutoCAD.Windows;
//using Project.WPF;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms.Integration;

//namespace Project
//{
//    public static class demo9
//    {

//        // 侧边栏的类型就是一个PaletteSet类型的变量
//        private static PaletteSet pset;
//        [CommandMethod(nameof(CS1))]
//        public static void CS1()
//        {
//            // pset初始化
//            if (pset is null)
//            {
//                pset = new PaletteSet("菜单");
//                var args = new PsetArgs();
//                args.Dic.Add("直线", "line");
//                args.Dic.Add("圆", "Circle");
//                args.Dic.Add("删除", "erase");
//                var wpf = new UserControl1();
//                wpf.DataContext = args;
//                //wpf进行转换
//                var host = new ElementHost()
//                {
//                    AutoSize = true,
//                    Dock = DockStyle.Fill,
//                    Child = wpf,
//                };
//                pset.Add("abc", host);
//                pset.Visible = true;
//                pset.Dock = DockSides.Left;
//                //pset.Size = new Size(100, 500);
//                return;
//            }
//            // 如果是存在的情况下的话
//            pset.Visible = !pset.Visible;
//        }
//    }

//    public class PsetArgs
//    {
//        public PsetArgs() { }
//        //构造函数
//        public Dictionary<string, string> Dic { get; set; } = new Dictionary<string, string>();
//        //新建一个字典用于存储命令的名称和命令
//    }
//}
