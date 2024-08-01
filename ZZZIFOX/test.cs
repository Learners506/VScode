using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZIFOX.DotNetARX;

namespace ZZZIFOX
{
    public class test
    {
        [CommandMethod(nameof(TQWZ))]
        public void TQWZ()
        {
            Env.Editor.WriteMessage("快捷命令TQWZ");
            // 新建事务
            using var tr = new DBTrans();
            MessageBox.Show("请选择要输出文字");
            // 设置选择集选项
            var xzs = new PromptSelectionOptions()
            {
                MessageForAdding = "\n请选择要输出的文字"
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
                double zheight = texts.First().Height;
                // 定义容差
                MessageBox.Show("请输入设置容差（默认为字高）" + "\n此处字高为：" + zheight.ToString("0.00"));
                double defaultTolerance = zheight;
                double tolerance;
                var pr = Env.Editor.GetString($"请输入容差值，默认为{defaultTolerance}");
                if (pr.Status == PromptStatus.OK)
                {
                    if (!double.TryParse(pr.StringResult, out tolerance))
                    {
                        Env.Editor.WriteMessage("容差已设置为默认值");
                        tolerance = defaultTolerance;
                    }
                    MessageBox.Show("容差设置为：" + tolerance.ToString());
                }
                else
                {
                    return;
                }
                // 根据容差进行分组排序
                string str = "";//用于接收结果
                var grouptextbyrow = texts.GroupBy(t => Math.Round(t.Position.Y / tolerance)).OrderByDescending(g => g.Key);
                foreach (var group in grouptextbyrow)
                {
                    var sorttextbyx = group.OrderBy(t => t.Position.X);
                    foreach (var t in sorttextbyx)
                    {
                        var ts = t.TextString;
                        Env.Editor.WriteMessage(ts + " ");
                        str += ts;
                        str += " ";
                    }
                    str += "\n";
                    Env.Editor.WriteMessage("\n");
                }

                var savefiledialog = new System.Windows.Forms.SaveFileDialog();
                savefiledialog.Filter = "文本文件(*.txt)|*.txt";
                savefiledialog.Title = "保存输出结果";
                if (savefiledialog.ShowDialog() == DialogResult.OK)
                {
                    string outputpath = savefiledialog.FileName;
                    using (StreamWriter writer = new StreamWriter(outputpath, true))
                    {
                        writer.Write(str);
                    }
                    Env.Editor.WriteMessage("文件保存成功\nwrite by zzz");
                    MessageBox.Show("结果已经保存至文件" + outputpath);
                }

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

        [CommandMethod(nameof(CS999))]
        public void CS999()
        {
            string str1 = "999";
            string str2 = "aaa";
            Env.Editor.WriteMessage(str1.IsNumeric().ToString());
            Env.Editor.WriteMessage(str2.IsNumeric().ToString());
        }

    }
}
