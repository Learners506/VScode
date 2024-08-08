using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.ProjectItems
{
    public static class ReplaceBlock
    {
        [CommandMethod(nameof(rez))]
        public static void rez()
        { 
            using var tr = new DBTrans();
            var blockref = Env.Editor.GetEntity("\n请选择要替换的块名,该图块为向上");
            if (blockref.Status != PromptStatus.OK)
            {
                return;
            }
            // 获取到块名
            var blockName = tr.GetObject<BlockReference>(blockref.ObjectId).Name;
            Env.Editor.WriteMessage("\n{0}", blockName);

            var blockref2 = Env.Editor.GetEntity("\n请选择要被替换的块名");
            if (blockref2.Status != PromptStatus.OK)
            {
                return;
            }
            // 获取到块名
            var blockName2 = tr.GetObject<BlockReference>(blockref2.ObjectId).Name;
            //var blockName2 = "Drawing3";
            Env.Editor.WriteMessage("\n{0}", blockName2);

            // 获取同名块
            var fil = new SelectionFilter(new TypedValue[]
            {
                new TypedValue((int)DxfCode.Start, "INSERT"),
                new TypedValue((int)DxfCode.BlockName, blockName2)
            });
            var xzs = new PromptSelectionOptions()
            {
                MessageForAdding = "\n请选择要替换的块范围"
            };
            var s1 = Env.Editor.GetSelection(xzs,fil);
            if(s1.Status != PromptStatus.OK) return;
            // 获取到同名块
            var ids = s1.Value.GetObjectIds();

            var rstring = Env.Editor.GetString("\n请输入旋转角度，1为向上，2为向左，3为向下，4为向右");
            double rotate = 0;
            if (rstring.Status == PromptStatus.OK)
            {
                switch (rstring.StringResult)
                {
                    case "1":
                        rotate = 0;
                        break;
                    case "2":
                        rotate = Math.PI / 2;
                        break;
                    case "3":
                        rotate = Math.PI;
                        break;
                    case "4":
                        rotate = Math.PI * 3 / 2;
                        break;
                    default:
                        break;
                }
            }

            foreach(var id in ids)
            {
                var blk = tr.GetObject<BlockReference>(id,OpenMode.ForWrite);
                var pos = blk.Position;
                blk.Erase();
                tr.CurrentSpace.InsertBlock(pos, blockName, rotation: rotate);
                
  
            }


        }
        [CommandMethod(nameof(rez2),CommandFlags.UsePickSet)]
        public static void rez2()
        {
            using var tr = new DBTrans();
            var res = Env.Editor.SelectImplied();
            if (res.Status != PromptStatus.OK)
            {
                return;
            }
            Env.Editor.WriteMessage("\n{0}", res.Value.Count);
            var blockref = Env.Editor.GetEntity("\n请选择要替换的块名,该图块为向上");
            if (blockref.Status != PromptStatus.OK)
            {
                return;
            }
            // 获取到块名
            var blockName = tr.GetObject<BlockReference>(blockref.ObjectId).Name;

            Env.Editor.WriteMessage("\n{0}", blockName);

            
            var ids = res.Value.GetObjectIds();

            var rstring = Env.Editor.GetString("\n请输入旋转角度，1为向上，2为向左，3为向下，4为向右");
            double rotate = 0;
            if (rstring.Status == PromptStatus.OK)
            {
                switch (rstring.StringResult)
                {
                    case "1":
                        rotate = 0;
                        break;
                    case "2":
                        rotate = Math.PI / 2;
                        break;
                    case "3":
                        rotate = Math.PI;
                        break;
                    case "4":
                        rotate = Math.PI * 3 / 2;
                        break;
                    default:
                        break;
                }
            }

            foreach (var id in ids)
            {
                var blk = tr.GetObject<BlockReference>(id, OpenMode.ForWrite);
                var pos = blk.Position;
                blk.Erase();
                tr.CurrentSpace.InsertBlock(pos, blockName, rotation: rotate);


            }


        }
    }
}
