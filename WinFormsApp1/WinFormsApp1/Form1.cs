using ExcelDataReader;
using System.IO;
using System.Data;
using ClosedXML.Excel;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private DataTable excelData;
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && excelData != null)
            {
                DataRow selectedRow = excelData.Rows[comboBox1.SelectedIndex + 1]; // 设定为选择数据的那一行
                double valuex1 = Math.Abs(Convert.ToDouble(selectedRow[4]));
                double valuex2 = Math.Abs(Convert.ToDouble(selectedRow[5]));
                textBoxFx.Text = Math.Max(valuex1, valuex2).ToString();
                textBoxFy.Text = selectedRow[6].ToString();
                textBoxM.Text = selectedRow[7].ToString();

                //textBox1.Text = selectedRow[2].ToString(); // 假设 textBox1 显示第三列数据
                //textBox2.Text = selectedRow[3].ToString(); // 假设 textBox2 显示第四列数据
                // 你可以继续添加更多的 TextBox 更新
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                openFileDialog.Title = "选择一个Excel文件";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    excelData = ReadExcelFile(filePath);

                    if (excelData != null)
                    {
                        comboBox1.Items.Clear();
                        for (int i = 1; i < excelData.Rows.Count; i++) // 跳过标题行
                        {
                            DataRow row = excelData.Rows[i];
                            string item = $"{row[0]} {row[1]}"; // 拼接第一列和第二列
                            comboBox1.Items.Add(item);
                        }
                    }
                }
            }
        }
        private DataTable ReadExcelFile(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    return result.Tables[0]; // 返回第一个工作表
                }
            }
        }

        // 缺省参数
        private void button2_Click(object sender, EventArgs e)
        {
            textBoxMC.Text = "0.5";
            textBoxJG.Text = "0.25";
            textBoxJK.Text = "0.628";
            textBoxJC.Text = "0.8";
            textBoxLZJ.Text = "3.1";
            textBoxZJJ.Text = "2.2";
        }
        //开始验算
        private void button4_Click(object sender, EventArgs e)
        {
            var height = Convert.ToDouble(textBoxJG.Text);
            var width = Convert.ToDouble(textBoxJK.Text);
            var longth = Convert.ToDouble(textBoxJC.Text);
            var Weight = 24 * height * width * longth;
            textBoxWeight.Text = Weight.ToString();

            // 总拔力
            var zbl = Convert.ToDouble(textBoxFy.Text);
            var kbxs = Weight / zbl;
            textBoxZBL.Text = zbl.ToString();
            textBoxKBXS.Text = kbxs.ToString();
            string result1 = (zbl > 0 && kbxs < 1.6) ? "不满足" : "满足";
            textBoxBR.Text = result1;
            if (result1 == "不满足")
            {
                textBoxBR.BackColor = Color.Red;
            }

            //滑移
            var mcxs = Convert.ToDouble(textBoxMC.Text);
            var khl = (Weight - zbl) * mcxs;
            var hdl = Convert.ToDouble(textBoxFx.Text);
            var khxs = khl / hdl;
            textBoxHYXS.Text = khxs.ToString();
            textBoxHDL.Text = hdl.ToString();
            textBoxKHL.Text = khl.ToString();
            string result2 = (khxs >= 1.3) ? "满足" : "不满足";
            textBoxHR.Text = result2;
            if (result2 == "不满足")
            {
                textBoxHR.BackColor = Color.Red;
            }

            double zkqfl;
            double zqfl;
            var jcg = Convert.ToDouble(textBoxJG.Text);
            var jcc = Convert.ToDouble(textBoxJC.Text);
            var spl = Convert.ToDouble(textBoxFx.Text);
            var wj = Convert.ToDouble(textBoxM.Text);
            //抗倾覆
            if (zbl >= 0)
            {
                zkqfl = Weight * jcc / 2;
                zqfl = Math.Abs(zbl) * jcc / 2 + Math.Abs(spl) * jcg + Math.Abs(wj);
            }
            else
            {
                zkqfl = Weight * jcc / 2 + jcc / 2 * Math.Abs(zbl);
                zqfl = Math.Abs(spl) * jcg + Math.Abs(wj);
            }
            var kqfxs = zkqfl / zqfl;

            textBoxKQF.Text = zkqfl.ToString();
            textBoxQFL.Text = zqfl.ToString();
            textBoxQFXS.Text = kqfxs.ToString();
            string result3 = (kqfxs >= 1.6) ? "满足" : "不满足";
            textBoxQR.Text = result3;
            if (result3 == "不满足")
            {
                textBoxQR.BackColor = Color.Red;
            }
        }
        // 导出结果
        private void button3_Click(object sender, EventArgs e)
        {
            excelData.Columns.Add("条形基础自重",typeof(double));
            excelData.Columns.Add("总拔力", typeof(double));
            excelData.Columns.Add("抗拔稳定系数", typeof(double));
            excelData.Columns.Add("是否满足抗拔稳定验算", typeof(string));
            excelData.Columns.Add("总抗滑力", typeof(double));
            excelData.Columns.Add("总滑动力", typeof(double));
            excelData.Columns.Add("抗滑移稳定系数", typeof(double));
            excelData.Columns.Add("是否满足抗滑移稳定验算", typeof(string));
            excelData.Columns.Add("总抗倾覆力", typeof(double));
            excelData.Columns.Add("总倾覆力", typeof(double));
            excelData.Columns.Add("抗倾覆稳定系数", typeof(double));
            excelData.Columns.Add("是否满足抗倾覆稳定验算", typeof(string));

            for (int i = 1; i < excelData.Rows.Count; i++)
            {
                DataRow selectedRow = excelData.Rows[i];
                double valuex3 = Math.Abs(Convert.ToDouble(selectedRow[4]));
                double valuex4 = Math.Abs(Convert.ToDouble(selectedRow[5]));
                

                var spl = Convert.ToDouble(Math.Max(valuex3, valuex4).ToString());
                var zbl = Convert.ToDouble(selectedRow[6].ToString());
                var wj = Convert.ToDouble(selectedRow[7].ToString());

                var height = Convert.ToDouble(textBoxJG.Text);
                var width = Convert.ToDouble(textBoxJK.Text);
                var longth = Convert.ToDouble(textBoxJC.Text);
                var Weight = 24 * height * width * longth;
                
                var kbxs = Weight / zbl;
                var mcxs = Convert.ToDouble(textBoxMC.Text);
                var khl = (Weight - zbl) * mcxs;
                var hdl = Convert.ToDouble(textBoxFx.Text);
                var khxs = khl / hdl;
                double zkqfl;
                double zqfl;
                var jcg = Convert.ToDouble(textBoxJG.Text);
                var jcc = Convert.ToDouble(textBoxJC.Text);
                //抗倾覆
                if (zbl >= 0)
                {
                    zkqfl = Weight * jcc / 2;
                    zqfl = Math.Abs(zbl) * jcc / 2 + Math.Abs(spl) * jcg + Math.Abs(wj);
                }
                else
                {
                    zkqfl = Weight * jcc / 2 + jcc / 2 * Math.Abs(zbl);
                    zqfl = Math.Abs(spl) * jcg + Math.Abs(wj);
                }
                var kqfxs = zkqfl / zqfl;


                string result1 = (zbl > 0 && kbxs < 1.6) ? "不满足" : "满足";
                string result2 = (khxs >= 1.3) ? "满足" : "不满足";
                string result3 = (kqfxs >= 1.6) ? "满足" : "不满足";

                // 赋值
                selectedRow["条形基础自重"] = Weight;
                selectedRow["总拔力"] = zbl;
                selectedRow["抗拔稳定系数"] = kbxs;
                selectedRow["是否满足抗拔稳定验算"] = result1;

                selectedRow["总抗滑力"] = khl;
                selectedRow["总滑动力"] = hdl;
                selectedRow["抗滑移稳定系数"] = khxs;
                selectedRow["是否满足抗滑移稳定验算"] = result2;

                selectedRow["总抗倾覆力"] = zkqfl;
                selectedRow["总倾覆力"] = zqfl;
                selectedRow["抗倾覆稳定系数"] = kqfxs;
                selectedRow["是否满足抗倾覆稳定验算"] = result3;
            }
            string savepath = "";
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "请选择保存路径";
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savepath = saveFileDialog.FileName;
                }
                ExportDataTableToExcel(excelData, savepath);
                MessageBox.Show("导出成功");
            }
        }


        static void ExportDataTableToExcel(DataTable dataTable, string exportPath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable);
                workbook.SaveAs(exportPath);
            }
        }
    }
}
