using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using System.IO;
using System.Windows.Forms;

namespace ZZZIFOX.ProjectItems
{
    public class ExportFile
    {
        [CommandMethod(nameof(EtoF))]
        public void EtoF()
        {
            // 新建事务
            using var tr = new DBTrans();
            // 设置选择集选项
            var xzs = new PromptSelectionOptions()
            {
                MessageForAdding = "\n请选择要输出的文字"
            };
            // 设置筛选，只选择文字
            var fil = new SelectionFilter(new TypedValue[]
            {
                new TypedValue(0, "TEXT")
            });
            var r1 = Env.Editor.GetSelection(xzs, fil);
            if (r1.Status != PromptStatus.OK) return;

            // 定义文字列表，用于存储文字列表
            var texts = new List<DBText>();
            foreach (var id in r1.Value.GetObjectIds())
            {
                var text = (DBText)tr.GetObject(id);
                texts.Add(text);
            }

            // 获取用户输入的两个点
            var pointOptions = new PromptPointOptions("\n请选择第一个点");
            var p1 = Env.Editor.GetPoint(pointOptions);
            if (p1.Status != PromptStatus.OK) return;
            pointOptions.Message = "\n请选择第二个点";
            var p2 = Env.Editor.GetPoint(pointOptions);
            if (p2.Status != PromptStatus.OK) return;

            // 计算两个点之间的距离作为列间隔
            double columnSpacing = p1.Value.DistanceTo(p2.Value);

            // 按X坐标分列
            var columns = texts
                .GroupBy(t => Math.Floor(t.Position.X / columnSpacing))
                .OrderBy(g => g.Key)
                .Select(g => g.OrderByDescending(t => t.Position.Y).ToList()) // 按Y坐标从上到下排序
                .ToList();

            // 获取最大行数
            int maxRows = columns.Max(col => col.Count);

            // 显示保存文件对话框
            var savefiledialog = new System.Windows.Forms.SaveFileDialog();
            savefiledialog.Filter = "文本文件(*.txt)|*.txt";
            savefiledialog.Title = "保存输出结果";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                string outputpath = savefiledialog.FileName;
                using (StreamWriter writer = new StreamWriter(outputpath,true,Encoding.Default))
                {
                    for (int i = 0; i < maxRows; i++)
                    {
                        var row = new List<string>();
                        foreach (var column in columns)
                        {
                            if (i < column.Count)
                            {
                                row.Add(column[i].TextString);
                            }
                            else
                            {
                                row.Add(""); // 保持列对齐
                            }
                        }
                        writer.WriteLine(string.Join("\t", row));
                    }
                }
                MessageBox.Show("结果已经保存至文件 " + outputpath);
                Process.Start(new ProcessStartInfo(outputpath) { UseShellExecute = true });
            }
        }

        [CommandMethod(nameof(DQWZ))]
        public void DQWZ()
        {
            // 新建事务
            using var tr = new DBTrans();
            // 设置选择集选项
            var xzs = new PromptSelectionOptions()
            {
                MessageForAdding = "\n请选择要对齐的文字"
            };
            // 设置筛选，只选择文字
            var fil = new SelectionFilter(new TypedValue[]
            {
                new TypedValue(0,"TEXT")
            });
            var r1 = Env.Editor.GetSelection(xzs, fil);
            if (r1.Status == PromptStatus.OK)
            {
                // 定义文字列表，用于存储文字列表
                var texts = new List<DBText>();

                foreach (var id in r1.Value.GetObjectIds())
                {
                    var text = (DBText)tr.GetObject(id);
                    texts.Add(text);
                }
                double poy = texts.First().Position.Y;
                foreach (var id in r1.Value.GetObjectIds())
                {
                    var t2 = (DBText)tr.GetObject(id, OpenMode.ForWrite);
                    var position = t2.Position;
                    t2.Position = new Point3d(position.X, poy, position.Z);
                }
            }
        }


        // 导出某个图层的文字内容
        [CommandMethod(nameof(dctcwz))]
        public void dctcwz()
        {
            using var tr = new DBTrans();
            // 提示选中某个图层的对象
            var r1 = Env.Editor.GetEntity("\n拾取对象图层");
            if (r1.Status != PromptStatus.OK) return;
            // 拿到图层的名字
            var layername = tr.GetObject<Entity>(r1.ObjectId).Layer;
            // 根据图层的名字建立筛选规则
            var sf = new SelectionFilter(new TypedValue[]
            {
                new TypedValue(0,"TEXT,MTEXT"),
                new TypedValue(8,layername)
            });
            // 按照筛选规则选中图中所有符合对象的内容
            var r2 = Env.Editor.SelectAll(sf);
            if (r2.Status != PromptStatus.OK) return;
            var str = "";
            foreach (var id in r2.Value.GetObjectIds())
            {
                // 拿到所有单行文字的内容
                var dbt = tr.GetObject<DBText>(id);
                if (dbt != null)
                {
                    str += dbt.TextString + "单行文字" + "\n";
                }
                else // 拿到所有多行文字的内容
                {
                    var mtxt = tr.GetObject<MText>(id);
                    if (mtxt != null)
                    {
                        str += mtxt.Text + "多行文字" + "\n";
                    }
                }
            }
            //写到桌面的文件中
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + layername + ".txt", str);
        }

    }
}
