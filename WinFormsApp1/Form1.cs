using ExcelDataReader;
using System.IO;
using System.Data;
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
                textBoxFx.Text = Math.Max(valuex1,valuex2).ToString();

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



    }
}
