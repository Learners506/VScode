using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckRebar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridViews();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateTextBoxesAndDataGrids();
            comboBoxHJLB.SelectedIndexChanged += new EventHandler(Combox_SelectedIndexChanged);
            comboBoxSYNX.SelectedIndexChanged += new EventHandler(Combox_SelectedIndexChanged);
            comboBoxHNTQD.SelectedIndexChanged += new EventHandler(Combox_SelectedIndexChanged);


        }


        private void Combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBoxesAndDataGrids();
        }

        private void UpdateTextBoxesAndDataGrids()
        {
            string 环境类别 = comboBoxHJLB.Text;
            
            // 设置最小保护层
            switch (环境类别)
            {
                case "一":
                    textBoxZXBHC.Text = "20";
                    break;
                case "二a":
                    textBoxZXBHC.Text = "25";
                    break;
                case "二b":
                    textBoxZXBHC.Text = "35";
                    break;
                case "三a":
                    textBoxZXBHC.Text = "40";
                    break;
                case "三b":
                    textBoxZXBHC.Text = "50";
                    break;
            }

            string 使用年限 = comboBoxSYNX.Text;
            string 混凝土强度 = comboBoxHNTQD.Text;
            string 最小保护层 = textBoxZXBHC.Text;

            if (使用年限 == "50" && 混凝土强度 == "<=C25")
            {
                textBoxBHCHD.Text = Convert.ToString(Convert.ToDouble(最小保护层) + 5); 
            }
            else if (使用年限 == "50" && 混凝土强度 == ">C25")
            {
                textBoxBHCHD.Text = 最小保护层;
            }
            else if (使用年限 == "100" && 混凝土强度 == "<=C25")
            {
                textBoxBHCHD.Text = Convert.ToString(Convert.ToDouble(最小保护层) * 1.4 + 5); 
            }
            else if (使用年限 == "100" && 混凝土强度 == ">C25")
            {
                textBoxBHCHD.Text = Convert.ToString(Convert.ToDouble(最小保护层) * 1.4 );
            }
            else
            {
                textBoxBHCHD.Text = "0"; // 默认值
            }






        }

        private void InitializeDataGridViews()
        {
            // 设置列标题
            string[] columnHeaders = {  "200", "250", "300", "350", "400", "450", "500", "600", "700", "800", "900", "1000" };
            for (int i = 0; i < columnHeaders.Length; i++)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = columnHeaders[i] });
            }

            // 设置行标题和固定值
            string[] rowHeaders = {  "12", "14", "16", "18", "20", "22", "25", "28", "32" };
            for (int i = 0; i < rowHeaders.Length; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = rowHeaders[i];
                
            }
            dataGridView1.TopLeftHeaderCell.Value = "钢筋直径/梁宽";

            // 设置所有单元格内容居中
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 隐藏没有数据的最后一行
            dataGridView1.AllowUserToAddRows = false;
            // 设置列宽和行高自适应
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView1.RowTemplate.Height = 50;

        }




    }
}
