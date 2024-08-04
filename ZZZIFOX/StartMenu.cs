using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ZZZIFOX
{
    public static class StartMenu
    {
        [CommandMethod(nameof(AddMenu))]
        public static void AddMenu()
        {
            // 获取菜单按钮
            var menus = Autodesk.AutoCAD.ApplicationServices.Application.MenuGroups.InvokeMethod("item", "Acad").GetProperty("menus");
            // 尝试添加测试菜单
            try
            {
                menus.InvokeMethod("Add", "ZZZFox");
            }
            catch {
                MessageBox.Show("添加失败");
            }
            // 获取添加的测试菜单
            var menu = menus.InvokeMethod("item", "ZZZFox");
            while (Convert.ToInt32(menu.GetProperty("Count")) > 0)
            {
                //清空测试菜单下的所有按钮
                menu.InvokeMethod("item", 0).InvokeMethod("delete");
            }

            // 添加文件操作子菜单栏
            var zcdl = menu.InvokeMethod("AddSubMenu", 0, "文件操作");
            {

                zcdl.InvokeMethod("AddMenuItem", 0, "文字导出txt(EtoF)", "\u0003EtoF ");
                zcdl.InvokeMethod("AddMenuItem", 1, "txt导入为文字(FtoE)", "\u0003FtoE ");

            }
            // 添加分隔符
            menu.InvokeMethod("AddSeparator", 1);

            // 添加块操作图元菜单栏
            var zcd2 = menu.InvokeMethod("AddSubMenu", 2, "圆元操作");
            {

                zcd2.InvokeMethod("AddMenuItem", 0, "统计图块(TQTK)", "\u0003TQTK ");
                zcd2.InvokeMethod("AddMenuItem", 1, "txt导入为文字(FtoE)", "\u0003FtoE ");

            }


            menu.InvokeMethod("AddMenuItem", 3, "测试文件(CS99)", "\u0003CS999 ");

            // 添加菜单到菜单栏
            try
            {
                menu.InvokeMethod("RemoveFromMenuBar");
            }
            catch { }
            menu.InvokeMethod("InsertInMenuBar", "");

        }

        private static object GetProperty(this object obj, string key)
        {
            var comtype = Type.GetTypeFromHandle(Type.GetTypeHandle(obj));
            return comtype.InvokeMember(key, BindingFlags.GetProperty, null, obj, null);
        }
        private static void SetProperty(this object obj, string key, object value)
        {
            var comtype = Type.GetTypeFromHandle(Type.GetTypeHandle(obj));
            comtype.InvokeMember(key, BindingFlags.SetProperty, null, obj, new object[] { value });
        }
        private static object InvokeMethod(this object obj, string method, params object[] objArray)
        {
            var comtype = Type.GetTypeFromHandle(Type.GetTypeHandle(obj));
            return comtype.InvokeMember(method, BindingFlags.InvokeMethod, null, obj, objArray);
        }
    }
}
