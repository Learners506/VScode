
//namespace Project
//{
//    public class demo2
//    {
//        [CommandMethod("FJJG")]
//        public void FJJG()
//        {
//            // 建事务
//            using var tr = new DBTrans();
//            // 设置选择集,promptselectionoptions对象用于配置选择操作开始时将显示给用户的提示，也可以用于指定提示关键词
//            var pso1 = new PromptSelectionOptions()
//            {
//                MessageForAdding = "\n请框选要修改的钢筋"
//            }; // 设置选择集的提示项目
//            // 设置筛选集
//            var sf = new SelectionFilter(new TypedValue[]
//            {
//                new TypedValue(0,"LWPOLYLINE"),
//               // new TypedValue(90,2),
//            }); // 设置选择集筛选条件
//            var r1 = Env.Editor.GetSelection(pso1, sf);
//            // 如果选择集里面有东西的话,getselection是选择窗体内的实体
//            if (r1.Status == PromptStatus.OK)
//            {
//                var set1 = r1.Value;
//                foreach(var id in set1.GetObjectIds())
//                {
//                    var pl = (Polyline)tr.GetObject(id, OpenMode.ForWrite);
//                    var lineweight = pl.GetStartWidthAt(0);// 获取线宽
//                    var startpoint = pl.StartPoint.Z20(); // 获取起点
//                    var endpoint = pl.EndPoint.Z20(); // 获取终点
//                    var angle = startpoint.GetAngle(endpoint); // Ifox自己写的获取两个点角度的函数
//                    // 声明一个czAngle用于计算与线垂直的角度
//                    var czAngle = angle + (angle >= Math.PI * 0.75 && angle < Math.PI * 1.75 ? Math.PI * 0.5 : -Math.PI * 0.5);
//                    // 计算与参照点特定角度和距离的点
//                    var aPoint = startpoint.Polar(czAngle, 200);
//                    var ePoint = endpoint.Polar(czAngle, 200);
//                    // 将新增的两个点坐标加入到p1线内
//                    pl.AddVertexAt(0, aPoint.Point2d(), 0, lineweight, lineweight);
//                    pl.AddVertexAt(pl.NumberOfVertices,ePoint.Point2d(),0,lineweight,lineweight);

//                }
//            }
//        }
//    }
//}
