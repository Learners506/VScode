//using Project.Properties;
//using Project.WPF;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project
//{
//    public static class Demo8
//    {
//        private static Settings ThisOptions => Settings.Default;
//        [CommandMethod(nameof(CS9))]
//        public static void CS9()
//        {
//            ThisOptions.Reload();
//            var w1 = new TextSettings();
//            var dr = w1.ShowDialog();
//            // 如果点击了取消或者×就返回
//            if (dr != true) return;
//            // 如果点击了确定就可以拿到设置的参数来进行下面的操作
//            var r1 = Env.Editor.GetPoint("\n请选择文字的位置");
//            if (r1.Status != PromptStatus.OK)
//                return;
//            // 利用WPF传入的参数来进行设置文字
//            var dbtext = new DBText()
//            {
//                Position = r1.Value.Ucs2Wcs(),
//                TextString = ThisOptions.TextString,
//                Height = ThisOptions.TextHeight,
//                ColorIndex = ThisOptions.ColorIndex,
//            };
//            using var tr = new DBTrans();
//            tr.CurrentSpace.AddEntity(dbtext);
//        }
//    }
//}
