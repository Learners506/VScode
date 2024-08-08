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
            var hdl = Convert.ToDouble(textBoxFx.Text);
            var khxs = khl / hdl;
            textBoxHYXS.Text = khxs.ToString();
            textBoxHDL.Text = hdl.ToString();
            textBoxKHL.Text = khl.ToString();
            string result2 = (khxs >= 1.3) ? "����" : "������";
            textBoxHR.Text = result2;
            if (result2 == "������")
            {
                textBoxHR.BackColor = Color.Red;
            }

            double zkqfl;
            double zqfl;
            var jcg = Convert.ToDouble(textBoxJG.Text);
            var jcc = Convert.ToDouble(textBoxJC.Text);
            var spl = Convert.ToDouble(textBoxFx.Text);
            var wj = Convert.ToDouble(textBoxM.Text);
            //���㸲
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
            string result3 = (kqfxs >= 1.6) ? "����" : "������";
            textBoxQR.Text = result3;
            if (result3 == "������")
            {
                textBoxQR.BackColor = Color.Red;
            }
        }
        // �������
        private void button3_Click(object sender, EventArgs e)
        {
            excelData.Columns.Add("���λ�������",typeof(double));
            excelData.Columns.Add("�ܰ���", typeof(double));
            excelData.Columns.Add("�����ȶ�ϵ��", typeof(double));
            excelData.Columns.Add("�Ƿ����㿹���ȶ�����", typeof(string));
            excelData.Columns.Add("�ܿ�����", typeof(double));
            excelData.Columns.Add("�ܻ�����", typeof(double));
            excelData.Columns.Add("�������ȶ�ϵ��", typeof(double));
            excelData.Columns.Add("�Ƿ����㿹�����ȶ�����", typeof(string));
            excelData.Columns.Add("�ܿ��㸲��", typeof(double));
            excelData.Columns.Add("���㸲��", typeof(double));
            excelData.Columns.Add("���㸲�ȶ�ϵ��", typeof(double));
            excelData.Columns.Add("�Ƿ����㿹�㸲�ȶ�����", typeof(string));

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
                //���㸲
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


                string result1 = (zbl > 0 && kbxs < 1.6) ? "������" : "����";
                string result2 = (khxs >= 1.3) ? "����" : "������";
                string result3 = (kqfxs >= 1.6) ? "����" : "������";

                // ��ֵ
                selectedRow["���λ�������"] = Weight;
                selectedRow["�ܰ���"] = zbl;
                selectedRow["�����ȶ�ϵ��"] = kbxs;
                selectedRow["�Ƿ����㿹���ȶ�����"] = result1;

                selectedRow["�ܿ�����"] = khl;
                selectedRow["�ܻ�����"] = hdl;
                selectedRow["�������ȶ�ϵ��"] = khxs;
                selectedRow["�Ƿ����㿹�����ȶ�����"] = result2;

                selectedRow["�ܿ��㸲��"] = zkqfl;
                selectedRow["���㸲��"] = zqfl;
                selectedRow["���㸲�ȶ�ϵ��"] = kqfxs;
                selectedRow["�Ƿ����㿹�㸲�ȶ�����"] = result3;
            }
            string savepath = "";
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "��ѡ�񱣴�·��";
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savepath = saveFileDialog.FileName;
                }
                ExportDataTableToExcel(excelData, savepath);
                MessageBox.Show("�����ɹ�");
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
