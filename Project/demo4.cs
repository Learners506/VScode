//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project
//{
//    public class demo4
//    {
//        [CommandMethod("TJXC")]
//        public void TJXC()
//        {
//            var tr = new DBTrans();
//            // 与用户交互，提示用户选择对象
//            var pso = new PromptSelectionOptions() { 
//            MessageForAdding= "\n请选择所有要统计长度的曲线"
//            };
//            var r0 = Env.Editor.GetSelection(pso);
//            // 如果用户正确选择的话
//            if(r0.Status== PromptStatus.OK)
//            {
//                var entids = r0.Value.GetObjectIds();
//                List<Entity> entities = new List<Entity>();
//                foreach (var entid in entids)
//                {
//                    var ent = (Entity)tr.GetObject(entid);
//                    entities.Add(ent);
//                }
//                // 筛选出是曲线的对象
//                var curlist = from ent in entities where ent is Curve select (Curve)ent;
//                double length=0;
//                foreach (var cur in curlist)
//                {
//                    var l = cur.GetLength();
//                    length += l;
//                }
//                length /= 1000;
//                var str = "总长为" + length.ToString("F2") + "m";// 将长度保留两位小数，然后加上前后缀，生成字符串
//                Env.Editor.WriteMessage("\n" + str);
//            }

//        }
//    }
//}
