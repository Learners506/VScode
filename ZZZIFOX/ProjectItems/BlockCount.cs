using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.ProjectItems
{
    public class BlockCount
    {
        [CommandMethod(nameof(TQTK))]
        public void TQTK() 
        {
            double textHeight = 2.5;
            var ppop = new PromptDoubleOptions("\n请输入文字高度，默认为2.5");
            ppop.AllowNone = true;
            var res = Env.Editor.GetDouble(ppop);
            if (res.Status == PromptStatus.OK)
            {
                textHeight = res.Value;
            }
            else if (res.Status == PromptStatus.Cancel)
            {
                // 用户按下了Esc键，停止操作
                return;
            }
            else
            {
                textHeight = 2.5;
            }
            using var tr = new DBTrans();
            // 创建文字样式表
            
            string newTextStyle = "zzz_text";
            if (!tr.TextStyleTable.Has(newTextStyle))
            {
                tr.TextStyleTable.Add(newTextStyle,ttr => 
                {
                    ttr.FileName = "tssdeng.shx";
                    ttr.BigFontFileName = "hztxt.shx";
                    ttr.XScale = 0.7;
                });
            }

            //创建存储块名称和数量的字典
            Dictionary<string, int> blockCount = new Dictionary<string, int>();
            // 判断是否结束的标志
            bool continueSelecting = true;
            List<ObjectId> highlightedEntities = new List<ObjectId>();
            while (continueSelecting) 
            {
                var blockref = Env.Editor.GetEntity("\n请选择块，（Esc结束选择）");
                if (blockref.Status != PromptStatus.OK) 
                {
                    continueSelecting = false;
                    continue;
                }
                var blockName = tr.GetObject<BlockReference>(blockref.ObjectId).Name;
                
                if (blockCount.ContainsKey(blockName))
                {
                    Env.Editor.WriteMessage($"\n块{blockName}已经选择过了");
                }
                else 
                {
                    foreach (var id in highlightedEntities)
                    {
                        var ent = (Entity)tr.GetObject(id, OpenMode.ForRead);
                        ent.UpgradeOpen();
                        ent.Unhighlight();
                    }
                    highlightedEntities.Clear();
                    // 统计块数量
                    TypedValue[] filterlist = new TypedValue[]
                    {
                        new TypedValue((int)DxfCode.Start, "INSERT"),
                        new TypedValue((int)DxfCode.BlockName, blockName)
                    };
                    SelectionFilter filter = new SelectionFilter(filterlist);
                    var psr = Env.Editor.SelectAll(filter);
                    if(psr.Status == PromptStatus.OK)
                    {
                        blockCount[blockName] = psr.Value.Count;
                        Env.Editor.WriteMessage($"\n块{blockName}的数量为：{psr.Value.Count}个");
                        // 高亮选择的块
                        foreach(var id in psr.Value.GetObjectIds())
                        {
                            var ent = (Entity)tr.GetObject(id, OpenMode.ForRead);
                            ent.UpgradeOpen();
                            ent.Highlight();
                            highlightedEntities.Add(id);
                        }

                    }
                }
            
            }

            // 插入表格
            var ppr = Env.Editor.GetPoint("\n请选择插入表格的位置");
            //string result = "";
            //foreach (var item in blockCount)
            //{
            //    result+=item.Key + "的个数为：" + item.Value.ToString() + "\n";
            //}
            //var mtext = new MText
            //{
            //    Contents = result,
            //    Location = ppr.Value,
            //    TextHeight = textHeight,
            //};
            //tr.CurrentSpace.AddEntity(mtext);



            Table table = new Table
            {
                TableStyle = tr.Database.Tablestyle,
                Position = ppr.Value
            };

            //table.TableStyle = tr.Database.Tablestyle;
            table.SetSize(blockCount.Count + 1, 2);
            //table.SetColumnWidth(50);
            //table.Position = ppr.Value;

            //table.SetTextHeight(textHeight, 0);
            //table.SetTextHeight(textHeight, 0);
            table.Cells[0, 0].TextString = "块名称";
            table.Cells[0, 1].TextString = "块数量";
            int row = 1;
            foreach (var item in blockCount)
            {
                table.Cells[row, 0].TextString = item.Key;
                table.Cells[row, 1].TextString = item.Value.ToString();
                row++;
            }
            // 自适应列宽
            for (int col = 0; col < table.Columns.Count; col++)
            {
                double maxWidth = 0;
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    var cell = table.Cells[r, col];
                    cell.TextHeight = textHeight;
                    double cellWidth = cell.GetTextString(new FormatOption()).Length * textHeight * 2; // 0.6 是一个经验值，可以根据需要调整
                    if (cellWidth > maxWidth)
                    {
                        maxWidth = cellWidth;
                    }
                    // 设置单元格居中对齐
                    cell.Alignment = CellAlignment.MiddleCenter;
                }
                table.Columns[col].Width = maxWidth;
            }

            tr.CurrentSpace.AddEntity(table);

            foreach (var id in highlightedEntities)
            {
                var ent = (Entity)tr.GetObject(id, OpenMode.ForRead);
                ent.UpgradeOpen();
                ent.Unhighlight();
            }
        }
    }
}
