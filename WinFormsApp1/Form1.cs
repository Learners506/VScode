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
                textBoxFx.Text = Math.Max(valuex1, valuex2).ToString();
                textBoxFy.Text = selectedRow[6].ToString();
                textBoxM.Text = selectedRow[7].ToString();

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

        // ȱʡ����
        private void button2_Click(object sender, EventArgs e)
        {
            textBoxMC.Text = "0.5";
            textBoxJG.Text = "0.25";
            textBoxJK.Text = "0.628";
            textBoxJC.Text = "0.8";
            textBoxLZJ.Text = "3.1";
            textBoxZJJ.Text = "2.2";
        }
        //��ʼ����
        private void button4_Click(object sender, EventArgs e)
        {
            var height = Convert.ToDouble(textBoxJG.Text);
            var width = Convert.ToDouble(textBoxJK.Text);
            var longth = Convert.ToDouble(textBoxJC.Text);
            var Weight = 24 * height * width * longth;
            textBoxWeight.Text = Weight.ToString();

            // �ܰ���
            var zbl = Convert.ToDouble(textBoxFy.Text);
            var kbxs = Weight / zbl;
            textBoxZBL.Text = zbl.ToString();
            textBoxKBXS.Text = kbxs.ToString();
            string result1 = (zbl > 0 && kbxs < 1.6) ? "������" : "����";
            textBoxBR.Text = result1;
            if (result1 == "������")
            {
                textBoxBR.BackColor = Color.Red;
            }
            
            //����
            var mcxs = Convert.ToDouble(textBoxMC.Text);
            var khl = (Weight - zbl) * mcxs;






        }
    }
}
