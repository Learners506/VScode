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
    }
}
