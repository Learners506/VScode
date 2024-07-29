//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project
//{
//    public class demo5
//    {
//        private static double textHeight = 300;
//        [CommandMethod("BHMJ")]
//        public void BHMJ()
//        {
//            // 创建一个图层用于放置边界线和面积
//            using (var tr = new DBTrans())
//            {
//                if (!tr.LayerTable.Has("面积")) tr.LayerTable.Add("面积", 1);
//                // 如果不存在“面积”图层的话，就创建这个图层，并将颜色设置为1（红色）
//            }
           


//            while (true)
//            {
//                // 设置循环设置闭合面积
//                var ppo1 = new PromptPointOptions("\n请选择闭合区域内部或[设置文字高度[D]]") { AllowNone = true };
//                // 添加关键字设置
//                ppo1.Keywords.Add("D");
//                // 提示用户获取坐标
//                var r1 = Env.Editor.GetPoint(ppo1);
//                // 分别判断是进行点取还是关键字的输入
//                if (r1.Status == PromptStatus.OK)
//                {
//                    // 拿到用户点取的坐标
//                    var pt1 = r1.Value.Wcs2Ucs();
//                    // 获取坐标点对应的边界线
//                    var dbc = Env.Editor.TraceBoundary(pt1, false);
//                    if (dbc.Count == 1)// 如果成功检测到边界线，会得到1条曲线，判断数量是否为1
//                    {
//                        // 判断dbc中对象是否为闭合曲线
//                        if (dbc[0] is Curve cur && cur.Closed)
//                        {
//                            // 对曲线进行设置
//                            cur.Layer = "面积";
//                            var area = cur.Area / 1e6;
//                            var pt = Env.Editor.GetPoint("\n请选择要插入的点的位置").Value.Wcs2Ucs();
//                            var text1 = new DBText()
//                            {
//                                TextString = "面积" + area.ToString("0.00") + "m2",
//                                Height = textHeight,
//                                Position = pt,
//                                VerticalMode = TextVerticalMode.TextVerticalMid,
//                                HorizontalMode = TextHorizontalMode.TextCenter,
//                                AlignmentPoint = pt,
//                                WidthFactor = 0.7,
//                                Layer = "面积",
//                            };
//                            // 建事务，添加边界线和文字到图纸中
//                            using (var tr = new DBTrans())
//                            {
//                                tr.CurrentSpace.AddEntity(cur, text1);
//                            }

//                        }
//                    }
//                }
//                else if (r1.Status == PromptStatus.Keyword)
//                {
//                    switch (r1.StringResult)
//                    {
//                        case "D":
//                            var r2 = Env.Editor.GetDouble("\n输入文字高度<" + textHeight + ">");
//                            if (r2.Status == PromptStatus.OK && r2.Value > 0) textHeight = r2.Value;
//                            break;
//                    }
//                }
//                else return;

//            }
           



             

//        }




//    }
//}
