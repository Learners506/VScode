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
                DataRow selectedRow = excelData.Rows[comboBox1.SelectedIndex + 1]; // �趨Ϊѡ�����ݵ���һ��
                double valuex1 = Math.Abs(Convert.ToDouble(selectedRow[4]));
                double valuex2 = Math.Abs(Convert.ToDouble(selectedRow[5]));
                textBoxFx.Text = Math.Max(valuex1,valuex2).ToString();

                //textBox1.Text = selectedRow[2].ToString(); // ���� textBox1 ��ʾ����������
                //textBox2.Text = selectedRow[3].ToString(); // ���� textBox2 ��ʾ����������
                // ����Լ�����Ӹ���� TextBox ����
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                openFileDialog.Title = "ѡ��һ��Excel�ļ�";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    excelData = ReadExcelFile(filePath);

                    if (excelData != null)
                    {
                        comboBox1.Items.Clear();
                        for (int i = 1; i < excelData.Rows.Count; i++) // ����������
                        {
                            DataRow row = excelData.Rows[i];
                            string item = $"{row[0]} {row[1]}"; // ƴ�ӵ�һ�к͵ڶ���
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
                    return result.Tables[0]; // ���ص�һ��������
                }
            }
        }



    }
}
