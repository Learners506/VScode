
//namespace Project
//{
//    public class demo1
//    {
//        [CommandMethod("ZZ")]
//        public void ZZ()
//        {
//            MessageBox.Show("helloa");
//        }

//        [CommandMethod("SQXC")]
//        public void SQXC()

//        {
//            //1.新建事务
//            using var tr = new DBTrans();
//            //2.选择直线对象
//            var l1 = Env.Editor.GetEntity("\n请选择一条线");
//            if(l1.Status == PromptStatus.OK)
//            {
//                // 如果选取到了对象，则获取一下对象实体
//                var ent1 = tr.GetObject<Entity>(l1.ObjectId); //getobject要传入的是object，其他值例如openmode默认是forread
//                // 判断拿到的实体是不是直线,如果是直线则执行以下操作
//                if(ent1 is Curve cur)
//                {
//                    // ifox重写的获取曲线长度的方法
//                    var length = cur.GetLength() / 1000;
//                    // 获取需要赋值的文字对象
//                    var t1 = Env.Editor.GetEntity("\n请选择要赋值的文字对象");
//                    if(t1.Status == PromptStatus.OK)
//                    {
//                        // 如果选取到了对象，则获取一下对象实体，获取的对象实体需要改为可写模式，默认是可读，因为要对其进行修改
//                        var ent2 = tr.GetObject<Entity>(t1.ObjectId,OpenMode.ForWrite);
//                        if(ent2 is DBText text)
//                        {
//                            // 如果选取的对象是文字的话
//                            text.TextString = "长度为：" + length.ToString("0.00") + "m";
//                        }

//                    }



//                }
//            }
           

            

//        }
//    }
//}
