using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.ProjectItems
{
    public class SelectSimlarText
    {
        [CommandMethod(nameof(xzwzddd))]
        public void xzwzddd()
        {
            using var tr = new DBTrans();
            var r2 = Env.Editor.GetEntity("\n选择文字");//提示用户选择文字
            var ent2 = tr.GetObject<Entity>(r2.ObjectId, OpenMode.ForWrite);
            // 获取选中文字
            string searchText = string.Empty;
            if (ent2 is DBText)
            {
                searchText = (ent2 as DBText).TextString;
            }
            else if (ent2 is MText)
            {
                searchText = (ent2 as MText).Text;
            }
            else
            {
                return;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                Env.Editor.WriteMessage("\n无法获取选中的文字内容.");
                return;
            }
            Env.Editor.WriteMessage($"\n选中的文字内容为: {searchText}");

            // 待查找文字
            List<ObjectId> foundTextIds = new List<ObjectId>();

            //var sf = new SelectionFilter(new TypedValue[]
            //{
            //    new TypedValue((int)DxfCode.Start, "TEXT"),
            //    //new TypedValue((int)DxfCode.Start, "MTEXT")
            //});
            var r3 = Env.Editor.SelectAll();
            if (r3.Status == PromptStatus.OK)
            {
                var set1 =r3.Value;
                foreach (ObjectId id in set1)
                {
                    DBText dbText = tr.GetObject(id, OpenMode.ForRead) as DBText;
                    //Env.Editor.WriteMessage($"\n{dbText.TextString}");
                    if (dbText != null && dbText.TextString.Contains(searchText))
                    {
                        foundTextIds.Add(id);
                    }
                    else
                    {
                        MText mText = tr.GetObject(id, OpenMode.ForRead) as MText;
                        //Env.Editor.WriteMessage($"\n{mText.Text}");
                        if (mText != null && mText.Text.Contains(searchText))
                        {
                            foundTextIds.Add(id);
                        }
                    }
              
                
                }
            }
            // 高亮显示找到的文字对象
            foreach (ObjectId id in foundTextIds)
            {
                Entity ent = tr.GetObject(id, OpenMode.ForWrite) as Entity;
                if (ent != null)
                {
                    ent.Highlight();
                }
            }

            Env.Editor.WriteMessage($"\n找到 {foundTextIds.Count} 个匹配的文字对象.");



        }

    }
}
