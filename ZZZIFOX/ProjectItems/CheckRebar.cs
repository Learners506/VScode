using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using ZZZIFOX.WPFUI;

namespace ZZZIFOX.ProjectItems
{
    public class CheckRebar
    {
        private static PaletteSet pset;
        [CommandMethod(nameof(REB))]
        public static void REB()
        {
            // pset初始化
            if (pset is null)
            {
                pset = new PaletteSet("梁钢筋构造");
                var wpf = new Rebar();
                //wpf进行转换
                var host = new ElementHost()
                {
                    AutoSize = true,
                    Dock = DockStyle.Fill,
                    Child = wpf,
                };
                pset.Add("abc", host);
                pset.Visible = true;
                //pset.Dock = DockSides.Bottom;
                //pset.Size = new Size(100, 500);
                return;
            }
            // 如果是存在的情况下的话
            pset.Visible = !pset.Visible;
        }
    }
}
