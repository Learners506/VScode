using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.ProjectItems
{
    public class ReadFies
    {
        [CommandMethod(nameof(FtoE))]
        public void FtoE()
        {
            // 让用户选择一个文件
            var openfiledialog = new System.Windows.Forms.OpenFileDialog();
            openfiledialog.Filter = "文本文件(*.txt)|*.txt|CSV文件(*.csv)|*.csv";
            openfiledialog.Title = "选择输入文件";
            if (openfiledialog.ShowDialog() != DialogResult.OK) return;

            string inputPath = openfiledialog.FileName;
            var columns = new List<List<string>>();

            // 读取文件内容
            using (var reader = new StreamReader(inputPath, Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split('\t'); // 假设分隔符为tab
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (i >= columns.Count)
                        {
                            columns.Add(new List<string>());
                        }
                        columns[i].Add(values[i]);
                    }
                }
            }

            // 获取用户设置的行间距
            var prompt = new PromptDoubleOptions("\n请输入行间距,默认设置为字高1.5倍");
            var rowSpacingResult = Env.Editor.GetDouble(prompt);
            if (rowSpacingResult.Status != PromptStatus.OK) return;

            double rowSpacing = rowSpacingResult.Value;

            // 新建事务
            //using var tr = new DBTrans();

            // 逐列处理
            var colcount = columns.Count;
            int currentcol = 1;
            foreach (var column in columns)
            {

                // 获取用户点击的位置
                var pointOptions = new PromptPointOptions($"\n请选择用于插入这{currentcol}/{colcount}列的位置");
                currentcol++;
                var p = Env.Editor.GetPoint(pointOptions);
                if (p.Status != PromptStatus.OK) return;
                var startPoint = p.Value;
                using var tr = new DBTrans();

                for (int rowIndex = 0; rowIndex < column.Count; rowIndex++)
                {
                    var text = column[rowIndex];
                    var position = new Point3d(startPoint.X, startPoint.Y - rowIndex * rowSpacing, startPoint.Z);

                    // 创建文字对象
                    var dbText = new DBText
                    {
                        Position = position,
                        TextString = text,
                        Height = rowSpacing * 2 / 3 // 可以根据需要调整
                    };

                    // 添加到模型空间
                    tr.CurrentSpace.AddEntity(dbText);
                }

            } 
        }
    }
}
