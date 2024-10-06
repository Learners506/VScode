using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

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

        [CommandMethod(nameof(ETC))]
        public void ETC()
        {
            //选择Excel路径
            var fileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls"
            };
            if (fileDialog.ShowDialog() != true) return;
            var excelPath = fileDialog.FileName;
            Env.Editor.WriteMessage(excelPath + "\n");

            // 读取文件
            using (var workbook = new XLWorkbook(excelPath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();

                var r1 = Env.Editor.GetDouble(new PromptDoubleOptions("\n请设置字体大小："));
                var r2 = Env.Editor.GetDouble(new PromptDoubleOptions("\n请设置行高："));
                var r3 = Env.Editor.GetDouble(new PromptDoubleOptions("\n请设置列宽："));
                var fontsize = r1.Value;
                var rowheight = r2.Value;
                var columnwidth = r3.Value;

                var p1 = Env.Editor.GetPoint("\n请设置插入点：");
                var startPoint = p1.Value.Ucs2Wcs();

                using var tr = new DBTrans();
                for (int i = 1; i <= range.RowCount(); i++)
                {
                    for (int j = 1; j <= range.ColumnCount(); j++)
                    {
                        var cellvalue = range.Cell(i, j).Value.ToString();
                        // 创建文本
                        DBText text = new DBText();
                        text.Position = startPoint + new Vector3d((j - 1) * columnwidth, -(i - 1) * rowheight, 0);
                        text.Height = fontsize;
                        text.TextString = cellvalue;
                        tr.CurrentSpace.AppendEntity(text);
                    }
                }
            }
        }
    }
}
