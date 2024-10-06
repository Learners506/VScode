using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using ZZZIFOX.Settings;
using ZZZIFOX.WPFUI;

namespace ZZZIFOX.ProjectItems
{
    public class DLLproject
    {
        // 拾取线长赋予文字内容长度值********************************
        [CommandMethod("CS1")]
        public void CS1()

        {
            //1.新建事务
            using var tr = new DBTrans();
            //2.选择直线对象
            var l1 = Env.Editor.GetEntity("\n请选择一条线");
            if (l1.Status == PromptStatus.OK)
            {
                // 如果选取到了对象，则获取一下对象实体
                var ent1 = tr.GetObject<Entity>(l1.ObjectId); //getobject要传入的是object，其他值例如openmode默认是forread
                // 判断拿到的实体是不是直线,如果是直线则执行以下操作
                if (ent1 is Curve cur)
                {
                    // ifox重写的获取曲线长度的方法
                    var length = cur.GetLength() / 1000;
                    // 获取需要赋值的文字对象
                    var t1 = Env.Editor.GetEntity("\n请选择要赋值的文字对象");
                    if (t1.Status == PromptStatus.OK)
                    {
                        // 如果选取到了对象，则获取一下对象实体，获取的对象实体需要改为可写模式，默认是可读，因为要对其进行修改
                        var ent2 = tr.GetObject<Entity>(t1.ObjectId, OpenMode.ForWrite);
                        if (ent2 is DBText text)
                        {
                            // 如果选取的对象是文字的话
                            text.TextString = "长度为：" + length.ToString("0.00") + "m";
                        }
                    }
                }
            }
        }

        // 负筋加钩，识别钢筋多段线，给两端加上钩子********************************
        [CommandMethod("CS2")]
        public void CS2()
        {
            // 建事务
            using var tr = new DBTrans();
            // 设置选择集,promptselectionoptions对象用于配置选择操作开始时将显示给用户的提示，也可以用于指定提示关键词
            var pso1 = new PromptSelectionOptions()
            {
                MessageForAdding = "\n请框选要修改的钢筋"
            }; // 设置选择集的提示项目
            // 设置筛选集
            var sf = new SelectionFilter(new TypedValue[]
            {
                new TypedValue(0,"LWPOLYLINE"),
               // new TypedValue(90,2),
            }); // 设置选择集筛选条件
            var r1 = Env.Editor.GetSelection(pso1, sf);
            // 如果选择集里面有东西的话,getselection是选择窗体内的实体
            if (r1.Status == PromptStatus.OK)
            {
                var set1 = r1.Value;
                foreach (var id in set1.GetObjectIds())
                {
                    var pl = (Polyline)tr.GetObject(id, OpenMode.ForWrite);
                    var lineweight = pl.GetStartWidthAt(0);// 获取线宽
                    var startpoint = pl.StartPoint.Z20(); // 获取起点
                    var endpoint = pl.EndPoint.Z20(); // 获取终点
                    var angle = startpoint.GetAngle(endpoint); // Ifox自己写的获取两个点角度的函数
                    // 声明一个czAngle用于计算与线垂直的角度
                    var czAngle = angle + (angle >= Math.PI * 0.75 && angle < Math.PI * 1.75 ? Math.PI * 0.5 : -Math.PI * 0.5);
                    // 计算与参照点特定角度和距离的点
                    var aPoint = startpoint.Polar(czAngle, 200);
                    var ePoint = endpoint.Polar(czAngle, 200);
                    // 将新增的两个点坐标加入到p1线内
                    pl.AddVertexAt(0, aPoint.Point2d(), 0, lineweight, lineweight);
                    pl.AddVertexAt(pl.NumberOfVertices, ePoint.Point2d(), 0, lineweight, lineweight);
                }
            }
        }

        // 统计所有曲线的总长之和********************************
        [CommandMethod("CS3")]
        public void CS3()
        {
            var tr = new DBTrans();
            // 与用户交互，提示用户选择对象
            var pso = new PromptSelectionOptions()
            {
                MessageForAdding = "\n请选择所有要统计长度的曲线"
            };
            var r0 = Env.Editor.GetSelection(pso);
            // 如果用户正确选择的话
            if (r0.Status == PromptStatus.OK)
            {
                var entids = r0.Value.GetObjectIds();
                List<Entity> entities = new List<Entity>();
                foreach (var entid in entids)
                {
                    var ent = (Entity)tr.GetObject(entid);
                    entities.Add(ent);
                }
                // 筛选出是曲线的对象
                var curlist = from ent in entities where ent is Curve select (Curve)ent;
                double length = 0;
                foreach (var cur in curlist)
                {
                    var l = cur.GetLength();
                    length += l;
                }
                length /= 1000;
                var str = "总长为" + length.ToString("F2") + "m";// 将长度保留两位小数，然后加上前后缀，生成字符串
                Env.Editor.WriteMessage("\n" + str);
            }
        }

        // 计算闭合曲线的面积********************************
        [CommandMethod("CS4")]
        public void CS4()
        {
            double textHeight = 300;

            // 创建一个图层用于放置边界线和面积
            using (var tr = new DBTrans())
            {
                if (!tr.LayerTable.Has("面积")) tr.LayerTable.Add("面积", 1);
                // 如果不存在“面积”图层的话，就创建这个图层，并将颜色设置为1（红色）
            }
            while (true)
            {
                // 设置循环设置闭合面积
                // 获取点提示
                var ppo1 = new PromptPointOptions("\n请选择闭合区域内部或[设置文字高度[D]]") { AllowNone = true };
                // 添加关键字设置
                ppo1.Keywords.Add("D");
                // 提示用户获取坐标
                var r1 = Env.Editor.GetPoint(ppo1);
                // 分别判断是进行点取还是关键字的输入
                if (r1.Status == PromptStatus.OK)
                {
                    // 如果状态是OK，说明用户点取了坐标
                    // 拿到用户点取的坐标
                    var pt1 = r1.Value.Wcs2Ucs();
                    // 获取坐标点对应的边界线
                    var dbc = Env.Editor.TraceBoundary(pt1, false);
                    if (dbc.Count == 1)// 如果成功检测到边界线，会得到1条曲线，判断数量是否为1
                    {
                        // 判断dbc中对象是否为闭合曲线
                        if (dbc[0] is Curve cur && cur.Closed)
                        {
                            // 对曲线进行设置
                            cur.Layer = "面积";
                            var area = cur.Area / 1e6;
                            var pt = Env.Editor.GetPoint("\n请选择要插入的点的位置").Value.Wcs2Ucs();
                            var text1 = new DBText()
                            {
                                TextString = "面积" + area.ToString("0.00") + "m2",
                                Height = textHeight,
                                Position = pt,
                                VerticalMode = TextVerticalMode.TextVerticalMid,
                                HorizontalMode = TextHorizontalMode.TextCenter,
                                AlignmentPoint = pt,
                                WidthFactor = 0.7,
                                Layer = "面积",
                            };
                            // 建事务，添加边界线和文字到图纸中
                            using (var tr = new DBTrans())
                            {
                                tr.CurrentSpace.AddEntity(cur, text1);
                            }

                        }
                    }
                }
                else if (r1.Status == PromptStatus.Keyword)
                {
                    // 如果用户是输入关键字
                    switch (r1.StringResult)
                    {
                        case "D":
                            var r2 = Env.Editor.GetDouble("\n输入文字高度<" + textHeight + ">");
                            if (r2.Status == PromptStatus.OK && r2.Value > 0) textHeight = r2.Value;
                            break;
                    }
                }
                else return;
            }
        }

        // 替换图纸中不存在的文字样式
        [CommandMethod("CS5")]
        public void CS5()
        {
            // 新建事务
            using (var tr = new DBTrans())
            {
                // 获取当前host，host用来做什么
                var host = HostApplicationServices.Current;
                // 获取当前CAD文件中所有文字样式表记录的集合
                var itst = tr.TextStyleTable.GetRecords();
                // 遍历每一个文字样式
                foreach (var tstr in itst)
                {
                    // 判断文字样式没有使用windows字体
                    if (tstr.Font.TypeFace == "")
                    {
                        //将文字样式改为可写模式
                        using (tstr.ForWrite())
                        {
                            try
                            {
                                //利用host支持文件搜索路径查找本地是否存在该文字样式使用的大字体，如果找不到就会报错进入catch
                                host.FindFile(tstr.BigFontFileName, Env.Database, FindFileHint.CompiledShapeFile);
                            }
                            catch
                            {
                                // 如果找不到的话就将它替换成任意一个CAD自带的大字体，这边使用GBHZFS
                                Env.Editor.WriteMessage("\n文字样式" + tstr.Name + "未找到大字体--<" + tstr.BigFontFileName + "替换为GBHZFS");
                                tstr.BigFontFileName = "GBHZFS";
                            }
                            // shx字体同理
                            try
                            {
                                //利用host支持文件搜索路径查找本地是否存在该文字样式使用的大字体，如果找不到就会报错进入catch
                                host.FindFile(tstr.FileName, Env.Database, FindFileHint.CompiledShapeFile);
                            }
                            catch
                            {
                                // 如果找不到的话就将它替换成任意一个CAD自带的大字体，这边使用GBHZFS
                                Env.Editor.WriteMessage("\n文字样式" + tstr.Name + "未找到大字体--<" + tstr.FileName + "替换为GBHZFS");
                                tstr.FileName = "SIMPLEX";
                            }
                        }
                    }
                }
                // 修改完进行regen刷新一下
                Env.Editor.Regen();
            }
        }

        // CAD嵌入停靠或者浮动的PaletteSet侧边栏面板********************************
        // 侧边栏的类型就是一个PaletteSet类型的变量
        private static PaletteSet pset;
        [CommandMethod(nameof(CS6))]
        public static void CS6()
        {
            // pset初始化
            if (pset is null)
            {
                pset = new PaletteSet("菜单");
                var args = new PsetArgs();
                args.Dic.Add("直线", "line");
                args.Dic.Add("圆", "Circle");
                args.Dic.Add("删除", "erase");
                var wpf = new TestPanelSet();
                wpf.DataContext = args;
                
                //wpf进行转换
                var host = new ElementHost()
                {
                    AutoSize = true,
                    Dock = DockStyle.Fill,
                    Child = wpf,
                };
                pset.Add("abc", host);
                pset.Visible = true;
                pset.Dock = DockSides.Left;
                //pset.Size = new Size(100, 500);
                return;
            }
            // 如果是存在的情况下的话
            pset.Visible = !pset.Visible;
        }



    }
    public class PsetArgs
    {
        public PsetArgs() { }
        //构造函数
        public Dictionary<string, string> Dic { get; set; } = new Dictionary<string, string>();
        //新建一个字典用于存储命令的名称和命令
    }
}
