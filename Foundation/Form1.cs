using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using MathNet.Numerics.RootFinding;
using MathNet.Numerics;
using System.IO;

namespace Foundation
{
    public partial class Form1 : Form
    {
        private Color filledBackgroundColor = Color.White;
        public string outresult;
        public int computetime =1;



        public Form1()
        {
            InitializeComponent();
            // 设置初始状态
            radioSingle.Checked = true;
            textBoxTCH.Enabled = false;
            textBoxCZ2.Enabled = false;
            textBoxDZ2.Enabled = false;
            textBoxKB2.Enabled = false;
            
            // 页面三初始化

            textBoxSXXS.Enabled = false;
            textBoxHSJMJ.Enabled = false;
            textBoxLLYXXS.Enabled = false;
            textBoxYLYXXS.Enabled = false;
            textBoxZSQDSPCZL.Enabled = false;

            textBoxWYYXZ.Enabled = false;
            textBoxSPCZL.Enabled = false;

            textBoxSXL3.Enabled = false;

            textBoxDXSWSD3.Enabled = false;
            textBoxZDEC.Enabled = false;
            textBoxZDFT.Enabled = false;
            textBoxES.Enabled = false;


            radioButtonYSJJ.Checked = true;



            radioSingle.CheckedChanged += RadioButton_CheckedChanged;
            radioMulty.CheckedChanged += RadioButton_CheckedChanged;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            // 注册 Form1_Load 事件处理程序
            this.Load += new EventHandler(Form1_Load);
            ApplyTextBoxColorChange(new List<System.Windows.Forms.TextBox> { textBoxCLCSD3, textBoxZWJ,textBoxZNJ,textBoxBHC,textBoxPJL,textBoxZDEC,textBoxZDFT,textBoxES,textBoxSXL3,textBoxKLBLXS,textBoxSXXS,textBoxLLYXXS,textBoxYLYXXS,textBoxWYYXZ});

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxES.Text = "200000";

            // 设置列数
            dataGridView1.ColumnCount = 6;
            dataGridView2.ColumnCount = 4;

            // 设置不允许用户添加行数据
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;

            // 禁止调整行和列的调整
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.AllowUserToResizeColumns = false;


            // 设置自动换行
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // 隐藏最左边的行头
            dataGridView1.RowHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            // 设置列适应 DataGridView 大小
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 设置行适应 DataGridView 大小
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // 添加列标题
            dataGridView1.Columns[0].Name = "参数";
            dataGridView1.Columns[1].Name = "坚硬、硬质黏土、粉质黏土、密实粉土";
            dataGridView1.Columns[2].Name = "可塑黏土、粉质黏土、中密粉土";
            dataGridView1.Columns[3].Name = "软塑黏土、粉质黏土、稍密粉土";
            dataGridView1.Columns[4].Name = "粗砂、中砂";
            dataGridView1.Columns[5].Name = "细沙、粉砂";

            dataGridView2.Columns[0].Name = "土的名称";
            dataGridView2.Columns[1].Name = "黏性土";
            dataGridView2.Columns[2].Name = "粉质黏土、粉土";
            dataGridView2.Columns[3].Name = "砂土";

            // 设置所有单元格内容居中对齐
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // 设置列标题居中对齐
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // 设置列标题居中对齐
            }




            // 添加行数据
            string[] row1 = new string[] { "重度（kN/m3）", "17", "16", "15","17","15" };
            string[] row2 = new string[] { "等代内摩擦角（°）", "35", "30", "15" ,"35","30"};

            string[] row3 = new string[] { "侧压力系数", "0.72", "0.6", "0.38"};

            dataGridView1.Rows.Add(row1);
            dataGridView1.Rows.Add(row2);
            dataGridView2.Rows.Add(row3);
            dataGridView3.AllowUserToAddRows = false; // 隐藏未提交的行
            // 添加土层参数列表
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView3.RowHeadersVisible = true;

            // 设置表格居中对齐
            // 设置单元格内容居中对齐
            dataGridView3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 设置列标题居中对齐
            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 设置行头居中对齐
            dataGridView3.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 绑定 CellValueChanged 事件
            dataGridView3.CellValueChanged += new DataGridViewCellEventHandler(dataGridView3_CellValueChanged);
            dataGridView3.CurrentCellDirtyStateChanged += new EventHandler(dataGridView3_CurrentCellDirtyStateChanged);


        }
        private void dataGridView3_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView3.IsCurrentCellDirty)
            {
                dataGridView3.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 检查是否是第六列的数据发生变化
            if (e.ColumnIndex == dataGridView3.Columns["Column6"].Index)
            {
                UpdateAccumulatedColumn();
            }
        }

        private void UpdateAccumulatedColumn()
        {
            // 更新累加列的值
            double accumulatedValue = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (double.TryParse(row.Cells["Column6"].Value?.ToString(), out double cellValue))
                {
                    accumulatedValue += cellValue;
                }
                row.Cells["Column7"].Value = accumulatedValue;
            }
        }


        // 页面一按钮
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSingle.Checked)
            {
                textBoxTCH.Enabled = false;
                textBoxCZ2.Enabled = false;
                textBoxDZ2.Enabled = false;
                textBoxKB2.Enabled = false;
            }
            else if (radioMulty.Checked)
            {
                textBoxTCH.Enabled = true;
                textBoxCZ2.Enabled = true;
                textBoxDZ2.Enabled = true;
                textBoxKB2.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        // 取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 确定按钮，开始计算
        private void button1_Click(object sender, EventArgs e)
        {
            // 检查各文本框是否输入正确

            if (radioSingle.Checked)
            {
                if (!AreTextBoxesFilled(textBoxZJ, textBoxYPJ, textBoxZC, textBoxYPJU, textBoxSYPJZJ, textBoxCZ1, textBoxKB1, textBoxDZ1)) // 根据实际情况传入所有相关的文本框
                {
                    return;
                }
            }
            else if (radioMulty.Checked)
            {
                if (!AreTextBoxesFilled(textBoxZJ, textBoxYPJ, textBoxZC, textBoxYPJU, textBoxSYPJZJ, textBoxCZ1, textBoxKB1, textBoxDZ1, textBoxTCH, textBoxCZ2, textBoxKB2, textBoxDZ2)) // 根据实际情况传入所有相关的文本框
                {
                    return;
                }
            }


            double 桩径 = Convert.ToDouble(textBoxZJ.Text);
            double 叶片径 = Convert.ToDouble(textBoxYPJ.Text);
            double 桩长 = Convert.ToDouble(textBoxZC.Text);
            double 叶片间距 = Convert.ToDouble(textBoxYPJU.Text);
            double 上片间距 = Convert.ToDouble(textBoxSYPJZJ.Text);

            double 侧摩系数1 = Convert.ToDouble(textBoxCZ1.Text);
            double 抗拔系数1 = Convert.ToDouble(textBoxKB1.Text);
            double 端阻系数1 = Convert.ToDouble(textBoxDZ1.Text);

            double 使用端阻 = Convert.ToDouble(textBoxDZ1.Text);
            double 分割高度 = 0;
            double 侧摩系数2 = 0;
            double 抗拔系数2 = 0;
           

            // 计算所得参数
            double 桩径周长 = Math.PI * 桩径;
            double 叶片周长 = Math.PI * 叶片径;

            // 单层土抗压承载力计算
            double u1 = Math.PI * 桩径;
            double u2 = 0;
            double u3 = Math.PI * 叶片径;
            double u4 = Math.PI * 桩径;
            double u5 = 0;

            double l1 = 桩长 - 上片间距 - 叶片径;
            double l2 = 叶片径;
            double l3 = 3 * 叶片径;
            double l4 = 叶片间距 - 4 * 叶片径;
            double l5 = 叶片径;

            List<double> 计算周长列表 = new List<double>();
            List<double> 计算长度列表 = new List<double>();
            double 侧向阻力 = 0;


            // 抗拔承载力计算
            double v1 = Math.PI * 桩径;
            double v2 = Math.PI * 叶片径;
            double v3 = 0;
            double v4 = Math.PI * 桩径;
            double v5 = Math.PI * 叶片径;

            double m1 = 桩长 - 上片间距 - 2 * 叶片径;
            double m2 = 2 * 叶片径;
            double m3 = 叶片径;
            double m4 = 叶片间距 - 4 * 叶片径;
            double m5 = 3 * 叶片径;
            List<double> 计算周长列表2 = new List<double>();
            List<double> 计算长度列表2 = new List<double>();
            double 抗拔承载力 = 0;

            if (radioSingle.Checked)
            {
                侧摩系数2 = 侧摩系数1;
                抗拔系数2 = 抗拔系数1;
            }
            else if (radioMulty.Checked)
            {
                分割高度 = Convert.ToDouble(textBoxTCH.Text);
                侧摩系数2 = Convert.ToDouble(textBoxCZ2.Text);
                抗拔系数2 = Convert.ToDouble(textBoxKB2.Text);
                使用端阻 = Convert.ToDouble(textBoxDZ2.Text);
            }

            



            if (叶片间距 <= 3 * 叶片径)
            {
                // 使用u1、u2、u6
                double u6 = Math.PI * 叶片径;
                double l6 = 叶片间距;
                计算周长列表 = new List<double>() { u1, u2, u6 };
                计算长度列表 = new List<double>() { l1, l2, l6 };
                侧向阻力 = 计算侧向阻力(计算周长列表, 计算长度列表, 分割高度, 侧摩系数1, 侧摩系数2);

                // 使用v1、v2、v6
                double v6 = Math.PI * 叶片径;
                double m6 = 叶片间距;
                计算周长列表2 = new List<double>() { v1, v2, v6 };
                计算长度列表2 = new List<double>() { m1, m2, m6 };
                抗拔承载力 = 计算抗拔力(计算周长列表2, 计算长度列表2, 分割高度, 抗拔系数1, 抗拔系数2, 侧摩系数1, 侧摩系数2);

                textBoxYYPJFW.Text = $"叶片距{叶片间距}小于等于3倍叶片径{3 * 叶片径}";
                textBoxBYPJFW.Text = $"叶片距{叶片间距}小于等于3倍叶片径{3 * 叶片径}";

            }
            else if (3 * 叶片径 < 叶片间距 && 叶片间距 < 4 * 叶片径)
            {
                // 使用u1、u2、u3、u7
                double u7 = 0;
                double l7 = 叶片间距 - 3 * 叶片径;
                计算周长列表 = new List<double>() { u1, u2, u3, u7 };
                计算长度列表 = new List<double>() { l1, l2, l3, l7 };
                侧向阻力 = 计算侧向阻力(计算周长列表, 计算长度列表, 分割高度, 侧摩系数1, 侧摩系数2);

                // 使用v1、v2、v7、v8
                double v7 = 0;
                double v8 = Math.PI * 叶片径;
                double m7 = 叶片间距 - 3 * 叶片径;
                double m8 = 3 * 叶片径;
                计算周长列表2 = new List<double>() { v1, v2, v7, v8 };
                计算长度列表2 = new List<double>() { m1, m2, m7, m8 };
                抗拔承载力 = 计算抗拔力(计算周长列表2, 计算长度列表2, 分割高度, 抗拔系数1, 抗拔系数2, 侧摩系数1, 侧摩系数2);
                textBoxYYPJFW.Text = $"叶片距{叶片间距}大于3倍叶片径{3 * 叶片径}小于4倍叶片径{4*叶片径}";
                textBoxBYPJFW.Text = $"叶片距{叶片间距}大于3倍叶片径{3 * 叶片径}小于4倍叶片径{4 * 叶片径}";
            }
            else
            {
                // 使用u1、u2、u3、u4、u5
                计算周长列表 = new List<double>() { u1, u2, u3, u4, u5 };
                计算长度列表 = new List<double>() { l1, l2, l3, l4, l5 };
                侧向阻力 = 计算侧向阻力(计算周长列表, 计算长度列表, 分割高度, 侧摩系数1, 侧摩系数2);

                // 使用v1、v2、v3、v4、v5
                计算周长列表2 = new List<double>() { v1, v2, v3, v4, v5 };
                计算长度列表2 = new List<double>() { m1, m2, m3, m4, m5 };
                抗拔承载力 = 计算抗拔力(计算周长列表2, 计算长度列表2, 分割高度, 抗拔系数1, 抗拔系数2, 侧摩系数1, 侧摩系数2);
                textBoxYYPJFW.Text = $"叶片距{叶片间距}大于4倍叶片径{4 * 叶片径}";
                textBoxBYPJFW.Text = $"叶片距{叶片间距}大于4倍叶片径{4 * 叶片径}";
            }
            double 端阻力 = Math.PI/4*叶片径*叶片径*使用端阻;
            double 抗压承载力标准值 = 侧向阻力 + 端阻力;
            double 抗压承载力特征值 = 抗压承载力标准值 * 0.5;
            double 抗拔承载力特征值 = 抗拔承载力 * 0.5;


            textBoxYJXCZ.Text = $"{侧向阻力:F2}";
            textBoxYJXDZ.Text = $"{端阻力:F2}";
            textBoxYKYCZL.Text = $"{抗压承载力标准值:F2}";
            textBoxYKYCZLT.Text = $"{抗压承载力特征值:F2}";

            textBoxBJXCZL.Text = $"{抗拔承载力:F2}";
            textBoxBKBCZL.Text = $"{抗拔承载力特征值:F2}";



        }


        // 页面一计算辅助函数
        // 检查文本框是否输入正确的函数
        private bool AreTextBoxesFilled(params System.Windows.Forms.TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    MessageBox.Show("请将输入参数填写完整", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Focus();
                    return false;
                }
            }
            return true;
        }
        

        // 查找索引函数
        public static (int, int) FindIndices(List<double> values, double target)
        {
            double sum = 0;
            int prevIndex = -1;

            for (int i = 0; i < values.Count; i++)
            {
                sum += values[i];

                if (sum >= target)
                {
                    return (prevIndex, i);
                }

                prevIndex = i;
            }

            return (-1, -1);  // 如果未找到满足条件的情况
        }

        // 计算侧向阻力
        public static double 计算侧向阻力(List<double> 计算周长列表, List<double> 计算长度列表, double 分割高度, double 侧摩系数1, double 侧摩系数2)
        {
            double load = 0;
            int indexcount = 计算周长列表.Count;
            if (分割高度 <= 计算长度列表[0])
            {
                load = 计算周长列表[0] * (分割高度 * 侧摩系数1 + (计算长度列表[0] - 分割高度) * 侧摩系数2);
                for (int i = 1; i < indexcount; i++)
                {
                    load = load + 计算长度列表[i] * 侧摩系数2 * 计算周长列表[i];
                }
            }
            else
            {
                (int, int) index = FindIndices(计算长度列表, 分割高度);
                int preindex = index.Item1;
                for (int i = 0; i < preindex + 1; i++)
                {
                    load = load + 计算长度列表[i] * 侧摩系数1 * 计算周长列表[i];
                }
                load = load + ((分割高度 - 计算长度列表.Take(preindex + 1).Sum()) * 侧摩系数1 + (计算长度列表.Take(preindex + 2).Sum() - 分割高度) * 侧摩系数2) * 计算周长列表[preindex + 1];
                for (int i = preindex + 2; i < indexcount; i++)
                {
                    load = load + 计算长度列表[i] * 侧摩系数2 * 计算周长列表[i];
                }
            }
            return load;
        }

        // 计算抗拔力
        public static double 计算抗拔力(List<double> 计算周长列表2, List<double> 计算长度列表2, double 分割高度, double 抗拔系数1, double 抗拔系数2, double 侧摩系数1, double 侧摩系数2)
        {
            double load = 0;
            int indexcount = 计算周长列表2.Count;
            if (分割高度 <= 计算长度列表2[0])
            {
                load = 计算周长列表2[0] * (分割高度 * 抗拔系数1 * 侧摩系数1 + (计算长度列表2[0] - 分割高度) * 抗拔系数2 * 侧摩系数2);
                for (int i = 1; i < indexcount; i++)
                {
                    load = load + 计算长度列表2[i] * 抗拔系数2 * 侧摩系数2 * 计算周长列表2[i];
                }
            }
            else
            {
                (int, int) index = FindIndices(计算长度列表2, 分割高度);
                int preindex = index.Item1;
                for (int i = 0; i < preindex + 1; i++)
                {
                    load = load + 计算长度列表2[i] * 抗拔系数1 * 侧摩系数1 * 计算周长列表2[i];
                }
                load = load + ((分割高度 - 计算长度列表2.Take(preindex + 1).Sum()) * 抗拔系数1 * 侧摩系数1 + (计算长度列表2.Take(preindex + 2).Sum() - 分割高度) * 抗拔系数2 * 侧摩系数2) * 计算周长列表2[preindex + 1];
                for (int i = preindex + 2; i < indexcount; i++)
                {
                    load = load + 计算长度列表2[i] * 抗拔系数2 * 侧摩系数2 * 计算周长列表2[i];
                }
            }
            return load;
        }



        // 页面二按钮
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 判断输入参数是否为空
            if (!AreTextBoxesFilled(textBoxZMS, textBoxZSC, textBoxZC2, textBoxZJ2, textBoxJQZD, textBoxNMCJ, textBoxCYLXS))
            {
                return;
            }

            double 桩埋深 = Convert.ToDouble(textBoxZMS.Text);
            double 桩伸出地面 = Convert.ToDouble(textBoxZSC.Text);
            double 桩长 = Convert.ToDouble(textBoxZC2.Text);
            double 桩径 = Convert.ToDouble(textBoxZJ2.Text);

            double 加权重度 = Convert.ToDouble(textBoxJQZD.Text);
            double 等代内摩擦角 = Convert.ToDouble(textBoxNMCJ.Text);
            double 侧压力系数 = Convert.ToDouble(textBoxCYLXS.Text);

            double 外摩擦角 = 0;

            外摩擦角 = 加权重度 * Math.Pow( Math.Tan((45+等代内摩擦角/2)*Math.PI/180) , 2);
            textBoxWMCJ.Text = 外摩擦角.ToString("F2");

            double 空间增大系数 = 1 + (2 * 桩埋深 / (3 * 桩径)) * 侧压力系数 * Math.Cos((45 + 等代内摩擦角/2) * (Math.PI / 180)) * Math.Tan(等代内摩擦角*(Math.PI/180));
            textBoxKJZDXS.Text = 空间增大系数.ToString("F2");

            double 计算宽度 = 空间增大系数 * 桩径;
            textBoxJSKD.Text = 计算宽度.ToString("F2");
            double 比值系数 = 桩伸出地面 / 桩埋深;
            textBoxBZ.Text = 比值系数.ToString("F2");

            //double[] coefficients = new double[]
            //{
            //    1,
            //    1.5*比值系数,
            //    -0.75*比值系数,
            //    -0.5
            //};

            var roots = Cubic.RealRoots(-0.75 * 比值系数 - 0.5,0, 1.5 * 比值系数);
            double 扩散角 = roots.Item1;
            textBoxKSXJJ.Text = 扩散角.ToString("F2");

            double 摩阻系数 = 3 / (1-2*扩散角*扩散角*扩散角);
            textBoxMZXS.Text = 摩阻系数.ToString("F2");

            double 水平抗力 = 外摩擦角 * 计算宽度 * 桩埋深 * 桩埋深 / (比值系数*摩阻系数);
            textBoxSPKL.Text = 水平抗力.ToString("F2");



        }


        // 页面三按钮
        // 取消按钮
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // 计算按钮
        private void button5_Click(object sender, EventArgs e)
        {
            if (! AreTextBoxesFilled(textBoxCLCSD3,textBoxPJL,textBoxZWJ,textBoxZNJ,textBoxBHC,textBoxES,textBoxZDEC,textBoxZDFT))
            {
                return;
            }
            double 桩身配筋率 = Convert.ToDouble(textBoxPJL.Text);
            

            if (桩身配筋率 > 0.0065)
            {
                // 位移允许值
                if (!AreTextBoxesFilled(textBoxWYYXZ))
                {
                    return;
                }
            }
            else
            {
                if (!AreTextBoxesFilled(textBoxSXXS,textBoxLLYXXS,textBoxYLYXXS,textBoxSXL3))
                {
                    return;
                }
            }

            if (checkBox1.Checked) 
            {
                if (!AreTextBoxesFilled(textBoxDXSWSD3))
                {
                    return;
                }
            }


            // 验证表格的数据是否填写完整
            List<int> requireColumnIndex = new List<int>() { 1,2,4,5,7 };
            int lastRowCheckColumnIndex = 3;
            bool allrowFilled = true;
            bool lastRowFilled = true;
            // 遍历所有行，检查requireColumnIndex列是否全部填充
            foreach(DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.IsNewRow) continue;
                // 检查requiredColumnIndex列是否为空
                foreach(var i  in requireColumnIndex)
                {
                    if(string.IsNullOrEmpty(row.Cells[i].Value?.ToString()))
                    {
                        allrowFilled = false;
                        break;
                    }
                }
            }
            // 检查最后一行的lastRowlastRowCheckColumnIndex列是否全部填充
            if (dataGridView3.Rows.Count >= 1)
            {
                DataGridViewRow lastcheckrow = dataGridView3.Rows[dataGridView3.Rows.Count - 1];
                if (string.IsNullOrWhiteSpace(lastcheckrow.Cells[lastRowCheckColumnIndex].Value?.ToString()))
                {
                    lastRowFilled = false;

                }
            }
            if(dataGridView3.Rows.Count == 0)
            {
                allrowFilled = false;
                tabControl1.SelectedTab = tabPage4;
            }
           if(!allrowFilled || !lastRowFilled)
            {
                MessageBox.Show("请将土层数据填写完整");
                return;
            }

            // 获取抗力比例系数
            List<double> targetValues = new List<double>();
            List<double> heightValues = new List<double>();

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.IsNewRow) continue;

                if (double.TryParse(row.Cells[7].Value?.ToString(), out double targetValue))
                {
                    targetValues.Add(targetValue);
                }

                if (double.TryParse(row.Cells[5].Value?.ToString(), out double heightValue))
                {
                    heightValues.Add(heightValue);
                }
            }
            double 桩外径 = Convert.ToDouble(textBoxZWJ.Text);
            double 计算抗力比例系数 = 0;
            double threshold = 2 * (桩外径 + 1);
            if (targetValues.Count == 1)
            {
                计算抗力比例系数 = targetValues[0];
            }
            else if (targetValues.Count == 2)
            {
                double h1 = heightValues[0];
                double h2 = heightValues[1];
                double m1 = targetValues[0];
                double m2 = targetValues[1];
                if (threshold < h1)
                {
                    计算抗力比例系数 = m1;
                }
                else
                {
                    计算抗力比例系数 = (m1 * h1 * h1 + m2 * (2 * h1 + h2) * h2)/(threshold*threshold); // 组合值
                }
            }
            else if (targetValues.Count >= 3)
            {
                double h1 = heightValues[0];
                double h2 = heightValues[1];
                double h3 = heightValues[2];
                double m1 = targetValues[0];
                double m2 = targetValues[1];
                double m3 = targetValues[2];
                if (threshold < h1)
                {
                    计算抗力比例系数 = m1;
                }
                else if (threshold < h1 + h2)
                { 
                    计算抗力比例系数 = (m1 * h1 * h1 + m2 * (2 * h1 + h2) * h2) / (threshold * threshold);
                }
                else
                {
                    计算抗力比例系数 = (m1*h1*h1+m2*(2*h1+h2)*h2+m3*(2*h1+2*h2+h3)*h3)/ (threshold * threshold); // 组合值
                }
            }
            



            double 持力层深度 = Convert.ToDouble(textBoxCLCSD3.Text);
            double 桩内径 = Convert.ToDouble(textBoxZNJ.Text);
            double 保护层厚度 = Convert.ToDouble(textBoxBHC.Text);
            double 桩周长 = Math.PI * 桩外径;
            double aj = Math.PI / 4 * (桩外径 * 桩外径 - 桩内径 * 桩内径);
            double ap1 = Math.PI / 4 * 桩内径 * 桩内径;
            double d0 = 桩外径 - 2 * 保护层厚度 / 1000;
            double 桩端土塞效应系数=0;
            double 桩进入土层总长 =0;
            double 桩自重 = 0;
            
            if (持力层深度 / 桩外径 < 5)
            {
                桩端土塞效应系数 = 0.16 * 持力层深度 / 桩外径;
            }
            else
            {
                桩端土塞效应系数 = 0.8;
            }

            // 获取表格中的数据
            List<List<string>> data = new List<List<string>>();
            foreach(DataGridViewRow row in dataGridView3.Rows)
            {
                List<string> rowData = new List<string>();
                foreach(DataGridViewCell cell in row.Cells)
                {
                    rowData.Add(cell.Value?.ToString() ?? string.Empty);
                    
                }
                data.Add(rowData);
            }
            // 单桩竖向极限承载力计算
            // 获取qsik和土层深度乘积,如果是最后一层的时候就采用进行持力层的深度进行计算
            string columnqsikname = "Column3";
            string columnliname = "Column6";
            string columnkbname = "Column5";
            int columnqsikIndex = dataGridView3.Columns[columnqsikname].Index;
            int columnliIndex = dataGridView3.Columns[columnliname].Index;
            int columnkbIndex = dataGridView3.Columns[columnkbname].Index;
            double qsiksum = 0;
            double tuksum = 0;
            for (int i = 0; i < data.Count; i++)
            {
                var row = data[i];
                double value1, value2,value3;
                if (double.TryParse(row[columnqsikIndex], out value1) && double.TryParse(row[columnkbIndex], out value3))
                {
                    if (i == data.Count - 1) // 判断是否是最后一行
                    {
                        桩进入土层总长 += 持力层深度;
                        qsiksum += value1 * 桩周长 * 持力层深度;
                        tuksum += value1*value3 * 桩周长 * 持力层深度;
                    }
                    else if (double.TryParse(row[columnliIndex], out value2))
                    {
                        桩进入土层总长 += value2;
                        qsiksum += value1 * value2 * 桩周长;
                        tuksum += value1 * value2 * value3 * 桩周长;
                    }
                }
            }
            // 获取qpk的累计和
            // 获取qpk
            string columnqpkname = "Column4";
            int columnqpkIndex = dataGridView3.Columns[columnqpkname].Index;
            int lastRowIndex = dataGridView3.Rows.Count - 1;
            double qpkvalue = Convert.ToDouble(dataGridView3.Rows[lastRowIndex].Cells[columnqpkIndex].Value.ToString()); 
            double qpksum = 0;
            qpksum = qpkvalue * (aj + 桩端土塞效应系数 * ap1);
            double 竖向承载力标准值 = qsiksum + qpksum;
            double 竖向承载力特征值 = 竖向承载力标准值 / 2;
            // 单桩竖向抗拔极限承载力的计算
            // tuksum和桩进入土层总长上述同时求得，下面计算自重（看是否考虑地下水位）
            if (checkBox1.Checked)
            {
                double 地下水位深度 = Convert.ToDouble(textBoxDXSWSD3.Text);
                if (地下水位深度 >= 桩进入土层总长)
                {
                    MessageBox.Show("地下水位深度大于桩进入土层总长，请重新输入,或取消勾选");
                    return;
                }
                桩自重 = 地下水位深度 * 15 * aj + (桩进入土层总长 - 地下水位深度) * 25 * aj;
            }
            else
            {
                桩自重 = 桩进入土层总长 * aj * 25;
            }
            double 抗拔特征值 = tuksum / 2 + 桩自重;
            double 混凝土弹性模量 = Convert.ToDouble(textBoxZDEC.Text);
            double 钢筋弹性模量 = Convert.ToDouble(textBoxES.Text);
            double 模量比值 = 钢筋弹性模量 / 混凝土弹性模量;
            double 换算截面模量 = Math.PI * 桩外径 * (桩外径 * 桩外径 + 2 * (模量比值 - 1) * 桩身配筋率 * d0 * d0) / 32;
            double 换算截面惯性矩 = 换算截面模量 * d0 / 2;
            double 钢筋面积 = aj * 1000000 * 桩身配筋率;
            double 桩身抗弯刚度 = 0.85 * 混凝土弹性模量 * 换算截面惯性矩 * 1000;
            double 桩身计算宽度 = 0.9 * (1.5 * 桩外径 + 0.5);
            double 水平抗力比例系数 = 计算抗力比例系数;
            double 桩水平变形系数 = Math.Pow(水平抗力比例系数 * 1000 * 桩身计算宽度 / 桩身抗弯刚度, 1.0 / 5.0);
            double 换算埋深 = 桩进入土层总长 * 桩水平变形系数;
            double 桩顶水平位移系数 = GetSPWYXS(换算埋深, radioButtonYSJJ.Checked);
            double 桩顶最大弯矩系数 = GetWJXS(换算埋深, radioButtonYSJJ.Checked);
            textBoxZZC.Text = 桩周长.ToString();
            textBoxAJ.Text = aj.ToString();
            textBoxAP.Text = ap1.ToString();
            textBoxD0.Text = d0.ToString();
            textBoxW0.Text = 换算截面模量.ToString();
            textBoxI0.Text = 换算截面惯性矩.ToString();
            textBoxAS.Text = 钢筋面积.ToString();
            textBoxSXCZL.Text = 竖向承载力标准值.ToString();
            textBoxSXCZLTZZ.Text = 竖向承载力特征值.ToString();
            textBoxTUK.Text = tuksum.ToString();
            textBoxZZZ.Text = 桩自重.ToString();
            textBoxKBTZZ.Text = 抗拔特征值.ToString();
            textBoxKWGD.Text = 桩身抗弯刚度.ToString();
            textBoxZSJSKD.Text = 桩身计算宽度.ToString();
            textBoxSPBXXS.Text = 桩水平变形系数.ToString();
            textBoxZDSPWYXS.Text = 桩顶水平位移系数.ToString();
            textBoxZDWJXS.Text = 桩顶最大弯矩系数.ToString();
            textBoxKLBLXS.Text = 水平抗力比例系数.ToString();
            double 桩顶水平位移允许值 = 0;
            double 位移控制水平承载力 = 0;
            double ft = Convert.ToDouble(textBoxZDFT.Text);

            double 输入竖向力 = 0;
            double 桩截面塑性系数 = 0;
            double 桩顶压力竖向影响系数 = 0;
            double 桩顶拉力竖向影响系数 = 0;
            double 桩身换算截面积 = 0;
            double 合成水平承载力特征值 = 0;

            if (桩身配筋率 > 0.0065)
            {  
                桩顶水平位移允许值 = Convert.ToDouble(textBoxWYYXZ.Text);
                位移控制水平承载力 = 0.75 * Math.Pow(桩水平变形系数, 3) * 桩身抗弯刚度 * 桩顶水平位移允许值 / 桩顶水平位移系数;
                textBoxSPCZL.Text = 位移控制水平承载力.ToString("F2");
            }
            else
            {
                // 单桩水平承载力特征值
                
                if (输入竖向力 >= 0)
                {
                    输入竖向力 = Convert.ToDouble(textBoxSXL3.Text);
                    桩截面塑性系数 = Convert.ToDouble(textBoxSXXS.Text);
                    桩顶压力竖向影响系数 = Convert.ToDouble(textBoxYLYXXS.Text);
                    桩顶拉力竖向影响系数 = Convert.ToDouble(textBoxLLYXXS.Text);
                    桩身换算截面积 = Math.PI * 桩外径 * 桩外径 * (1 + (模量比值 - 1) * 桩身配筋率) / 4;
                    // 表示拉力
                    合成水平承载力特征值 = 0.75 * 桩水平变形系数 * 桩截面塑性系数 * ft * 1000 * 换算截面模量 * (1.25 + 22 * 桩身配筋率) * (1 - 桩顶拉力竖向影响系数 * 输入竖向力 / (桩截面塑性系数 * ft * 1000 * 桩身换算截面积)) / 桩顶最大弯矩系数;
                   
                }
                else 
                {
                    合成水平承载力特征值 = 0.75 * 桩水平变形系数 * 桩截面塑性系数 * ft * 1000 * 换算截面模量 * (1.25 + 22 * 桩身配筋率) * (1 - 桩顶压力竖向影响系数 * 输入竖向力 / (桩截面塑性系数 * ft * 1000 * 桩身换算截面积)) / 桩顶最大弯矩系数;
                   
                }
                textBoxHSJMJ.Text = 桩身换算截面积.ToString();
                textBoxZSQDSPCZL.Text = 合成水平承载力特征值.ToString();
                
            }


            // 输出计算书
            outresult += $"***********************第{computetime}次计算书*******************************\n";
            outresult += "*****用户输入参数*****\n";
            outresult += $"桩外径:{桩外径}m\n";
            outresult += $"桩内径:{桩内径}m  (0表示实心桩)\n";
            outresult += $"保护层厚度:{保护层厚度}mm\n";
            outresult += $"桩身配筋率:{桩身配筋率}\n";
            outresult += $"混凝土强度等级:{comboBox1.SelectedItem.ToString()}MPa, 混凝土弹性模量Ec={混凝土弹性模量}N/mm^2, ft={ft}N/mm^2\n";
            outresult += $"钢筋弹性模量Es:{钢筋弹性模量}N/mm^2\n";
            //是否考虑地下水位
            if (checkBox1.Checked) 
            { 
                outresult += $"本次计算考虑地下水位：地下水位深度为{Convert.ToDouble(textBoxDXSWSD3.Text)}m\n";
            }
            else
            {
                outresult += $"本次计算不考虑地下水位\n";
            }

            outresult += $"桩进入最后一层持力层深度是:{持力层深度}m\n";
            outresult += "\n";
            outresult += "*****土层信息*****\n";
            string rowContent = string.Empty;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // 获取列名
                    string columnName = dataGridView3.Columns[cell.ColumnIndex].HeaderText;
                    string neirong = cell.Value?.ToString() ?? string.Empty;
                    rowContent += $"{columnName}:{neirong}\t";
                }
                rowContent+= "\n";
            }
            outresult += rowContent;
            outresult += "\n";
            outresult += "*****桩计算参数*****\n";
            outresult += $"桩进入土层总长：{桩进入土层总长}\n";
            outresult += $"桩的周长值：{桩周长}\n";
            outresult += $"桩截面实心面积Aj：{aj}m^2\n";
            outresult += $"桩截面空心面积Ap1：{ap1}m^2\n";
            outresult += $"除去保护层厚度后的桩径d0：{d0}m\n";
            outresult += $"换算截面模量w0：{换算截面模量}m^3\n";
            outresult += $"换算截面惯性矩I0：{换算截面惯性矩}m^4\n";
            outresult += $"钢筋面积As：{钢筋面积}m^2\n";
            outresult += $"柱身抗弯刚度EI：{桩身抗弯刚度}kN*m^2\n";
            outresult += $"柱身计算宽度b0：{桩身计算宽度}m\n";
            outresult += $"桩的水平变形系数：{桩顶水平位移系数}m^-1\n";
            outresult += $"水平抗力比例系数m：{水平抗力比例系数}MN/m^4\n";
            outresult += "\n";
            outresult += "*****桩顶约束情况*****\n";
            if (radioButtonYSJJ.Checked)
            {
                outresult += "本计算选择的桩顶约束情况是:铰接、自由\n";
            }
            else if (radioButtonYSGJ.Checked)
            {
                outresult += "本计算选择的桩顶约束情况是:固接\n";
            }
            outresult += $"桩顶最大弯矩系数Vm：{桩顶最大弯矩系数}\n";
            outresult += $"桩顶水平位移系数Vx：{桩顶水平位移系数}\n";
            outresult += "\n";
            outresult += "**********桩身竖向抗压、抗拔及水平承载力计算**********\n";
            outresult += "*****竖向承载力验算*****\n";
            outresult += $"竖向承载力标准值：{竖向承载力标准值}kN\n";
            outresult += $"竖向承载力特征值：{竖向承载力特征值}kN\n";
            outresult += "\n";
            outresult += "*****抗拔承载力计算*****";
            outresult += $"抗拔承载力标准值Tuk：{tuksum}kN\n";
            outresult += $"考虑水位后桩自重为：{桩自重}kN\n";
            outresult += $"抗拔承载力特征值：{抗拔特征值}kN\n";
            outresult += "\n";
            outresult += "*****水平承载力验算*****\n";
            if (桩身配筋率 > 0.0065)
            {
                outresult += "桩身配筋率大于0.65%，水平承载力由水平位移控制\n";
                outresult += $"桩顶位移允许值：{桩顶水平位移允许值}m\n";
                outresult += $"位移控制的水平承载力为{位移控制水平承载力}kN\n";
            }
            else
            {
                outresult += "桩身配筋率小于0.65%，水平承载力由桩身强度控制\n";
                outresult += $"输入的桩顶竖向力为：{输入竖向力}kN\n";
                outresult += $"截面塑性系数ym：{桩截面塑性系数}\n";
                outresult += $"桩身换算截面积An：{桩身换算截面积}mm^2\n";
                outresult += $"拉力影响系数：{桩顶拉力竖向影响系数}\n";
                outresult += $"压力影响系数：{桩顶压力竖向影响系数}\n";
                outresult += $"桩身强度控制的水平承载力为：{合成水平承载力特征值}kN\n";

            }
            computetime += 1;

        }
        private double GetWJXS(double hsms,bool isjj)
        {
            
            if (isjj)
            {
                // 定义已知点和对应的值
                double[] knownValues = { 2.4, 2.6, 2.8, 3.0, 3.5, 4.0 };
                double[] correspondingResults = { 0.601, 0.639, 0.675, 0.703, 0.750, 0.768 };
                if (hsms >= 4)
                {
                    return 0.768;
                }
                else if (hsms<=2.4)
                {
                    return 0.601;
                }
                else
                {
                    // 进行线性插值
                    for (int i = 0; i < knownValues.Length - 1; i++)
                    {
                        if (hsms >= knownValues[i] && hsms <= knownValues[i + 1])
                        {
                            double t = (hsms - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                            return correspondingResults[i] + t * (correspondingResults[i + 1] - correspondingResults[i]);
                        }
                    }
                }
            }
            else
            {
                // 定义已知点和对应的值
                double[] knownValues = {  2.4, 2.6, 2.8, 3.0, 3.5, 4.0 };
                double[] correspondingResults = {  1.045, 1.018, 0.990, 0.967, 0.934, 0.926 };
                if (hsms >= 4)
                {
                    return 0.926;
                }
                else if (hsms <= 2.4)
                {
                    return 1.045;
                }
                else
                {
                    // 进行线性插值
                    for (int i = 0; i < knownValues.Length - 1; i++)
                    {
                        if (hsms >= knownValues[i] && hsms <= knownValues[i + 1])
                        {
                            double t = (hsms - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                            return correspondingResults[i] + t * (correspondingResults[i + 1] - correspondingResults[i]);
                        }
                    }
                }
            }
            return 0;
        }
        private double GetSPWYXS(double hsms, bool isjj)
        {

            if (isjj)
            {
                // 定义已知点和对应的值
                double[] knownValues = {  2.4, 2.6, 2.8, 3.0, 3.5, 4.0 };
                double[] correspondingResults = { 3.526, 3.163, 2.905, 2.727, 2.502, 2.441 };
                if (hsms >= 4)
                {
                    return 2.441;
                }
                else if (hsms<=2.4)
                {
                    return 3.526;
                }
                else
                {
                    // 进行线性插值
                    for (int i = 0; i < knownValues.Length - 1; i++)
                    {
                        if (hsms >= knownValues[i] && hsms <= knownValues[i + 1])
                        {
                            double t = (hsms - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                            return correspondingResults[i] + t * (correspondingResults[i + 1] - correspondingResults[i]);
                        }
                    }
                }
            }
            else
            {
                // 定义已知点和对应的值
                double[] knownValues = { 2.4, 2.6, 2.8, 3.0, 3.5, 4.0 };
                double[] correspondingResults = { 1.095, 1.079, 1.055, 1.028, 0.970, 0.940 };
                if (hsms >= 4)
                {
                    return 0.940;
                }
                else if (hsms<=2.4)
                {
                    return 1.095;
                }
                else
                {
                    // 进行线性插值
                    for (int i = 0; i < knownValues.Length - 1; i++)
                    {
                        if (hsms >= knownValues[i] && hsms <= knownValues[i + 1])
                        {
                            double t = (hsms - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                            return correspondingResults[i] + t * (correspondingResults[i + 1] - correspondingResults[i]);
                        }
                    }
                }
            }
            return 0;
        }



        private void textBoxPJL_TextChanged(object sender, EventArgs e)
        {
            double 配筋率 = 0;
            try
            {
                配筋率 = Convert.ToDouble(textBoxPJL.Text);
            }
            catch (Exception err)
            {
                
            }

            if (配筋率 <= 0.0065)
            {
                textBoxWYYXZ.BackColor = Color.White;

                textBoxSXL3.Enabled = true;
                textBoxSXXS.Enabled = true;
                textBoxHSJMJ.Enabled = true;
                textBoxLLYXXS.Enabled = true;
                textBoxYLYXXS.Enabled = true;
                textBoxZSQDSPCZL.Enabled = true;

                textBoxSXL3.BackColor = Color.LightSteelBlue;
                textBoxSXXS.BackColor = Color.LightSteelBlue;
                textBoxLLYXXS.BackColor = Color.LightSteelBlue;
                textBoxYLYXXS.BackColor = Color.LightSteelBlue;

                textBoxWYYXZ.Enabled = false;
                textBoxSPCZL.Enabled = false;
            }
            else
            {
                textBoxSXL3.BackColor = Color.White;
                textBoxSXXS.BackColor = Color.White;
                textBoxLLYXXS.BackColor = Color.White;
                textBoxYLYXXS.BackColor = Color.White;
                

                textBoxSXL3.Enabled = false;
                textBoxSXXS.Enabled = false;
                textBoxHSJMJ.Enabled = false;
                textBoxLLYXXS.Enabled = false;
                textBoxYLYXXS.Enabled = false;

                textBoxWYYXZ.Enabled = true;
                textBoxSPCZL.Enabled = true;
                textBoxWYYXZ.BackColor = Color.LightSteelBlue;
            }
        }

        private void label72_Click(object sender, EventArgs e)
        {

        }

        private void textBoxC_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxJD_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxKBXS_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;

            if (!string.IsNullOrEmpty(textBox.Text))
            {
                textBox.BackColor = filledBackgroundColor;  // 设置填入后的背景色
            }
            else
            {
                textBox.BackColor = Color.LightSteelBlue;
            }
        }

        private void ApplyTextBoxColorChange(List<System.Windows.Forms.TextBox> textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.TextChanged += TextBox_TextChanged;
            }
        }


        // 添加一行表格数据
        private void button7_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView3.Rows.Add();
            //DataGridViewRow newRow = dataGridView1.Rows[rowIndex];

            
            //newRow.Cells["Column6"].Value = "0"; // 确保是一个有效的整数
            
            UpdateRowHeaders();
            
        }

        // 更新行头序号
        private void UpdateRowHeaders()
        {
            for (int i = 0;i<dataGridView3.Rows.Count;i++)
            {
                dataGridView3.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        //private void UpdateAccumulatedColumn()
        //{
        //    // 更新累加列的值
        //    int accumulatedValue = 0;
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (int.TryParse(row.Cells["Column6"].Value?.ToString(), out int cellValue))
        //        {
        //            accumulatedValue += cellValue;
        //        }
        //        row.Cells["Column7"].Value = accumulatedValue;
        //    }
        //}

        // 删除当前行的数据
        private void button8_Click(object sender, EventArgs e)
        {
            // 删除选中单元格所在的行
            if (dataGridView3.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dataGridView3.SelectedCells)
                {
                    if (cell.RowIndex >= 0 && cell.RowIndex < dataGridView3.Rows.Count)
                    {
                        dataGridView3.Rows.RemoveAt(cell.RowIndex);
                    }
                }
                UpdateRowHeaders();
                UpdateAccumulatedColumn();


            }
            else
            {
                MessageBox.Show("请先选择要删除的单元格。");
            }
        }

        // 上移按钮
        private void button9_Click(object sender, EventArgs e)
        {
            // 上移选中行
            if (dataGridView3.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView3.SelectedCells[0].RowIndex;
                if (rowIndex > 0)
                {
                    DataGridViewRow selectedRow = dataGridView3.Rows[rowIndex];
                    dataGridView3.Rows.Remove(selectedRow);
                    dataGridView3.Rows.Insert(rowIndex - 1, selectedRow);
                    dataGridView3.ClearSelection();
                    dataGridView3.Rows[rowIndex - 1].Selected = true;
                    dataGridView3.CurrentCell = dataGridView3.Rows[rowIndex - 1].Cells[0];
                    UpdateRowHeaders();
                    UpdateAccumulatedColumn();


                }
            }
            else
            {
                MessageBox.Show("请先选择要上移的单元格。");
            }
        }

        // 下移按钮
        private void button10_Click(object sender, EventArgs e)
        {
            // 下移选中行
            if (dataGridView3.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView3.SelectedCells[0].RowIndex;
                if (rowIndex < dataGridView3.Rows.Count - 1)
                {
                    DataGridViewRow selectedRow = dataGridView3.Rows[rowIndex];
                    dataGridView3.Rows.Remove(selectedRow);
                    dataGridView3.Rows.Insert(rowIndex + 1, selectedRow);
                    dataGridView3.ClearSelection();
                    dataGridView3.Rows[rowIndex + 1].Selected = true;
                    dataGridView3.CurrentCell = dataGridView3.Rows[rowIndex + 1].Cells[0];
                    UpdateRowHeaders();
                    UpdateAccumulatedColumn();


                }
            }
            else
            {
                MessageBox.Show("请先选择要下移的单元格。");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBoxDXSWSD3.Enabled = true;
            }
            else
            {
                textBoxDXSWSD3.Enabled = false;
            }
        }



        private void buttonmrcs3_Click(object sender, EventArgs e)
        {
            
            textBoxBHC.Text = "40";

            if (string.IsNullOrWhiteSpace(textBoxPJL.Text))
            {
                MessageBox.Show("请将配筋率填写完整", "输入参数不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPJL.Focus();
                return;
            }
            double 桩身配筋率 = Convert.ToDouble(textBoxPJL.Text);

            if (桩身配筋率 > 0.0065)
            {
                textBoxWYYXZ.Text = "0.01";
               
            }
            else
            {
                textBoxSXXS.Text = "2";
                textBoxYLYXXS.Text = "0.5";
                textBoxLLYXXS.Text = "1";
            }


        }

        private void checkBoxZDHNT_CheckedChanged(object sender, EventArgs e)
        {
            textBoxZDEC.Enabled = checkBoxZDHNT.Checked;
            textBoxZDFT.Enabled = checkBoxZDHNT.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            if (selectedValue == "C25")
            {
                textBoxZDEC.Text = "28000";
                textBoxZDFT.Text = "1.27";
            }
            else if (selectedValue == "C30")
            {
                textBoxZDEC.Text = "30000";
                textBoxZDFT.Text = "1.43";
            }
            else if (selectedValue == "C35")
            {
                textBoxZDEC.Text = "31500";
                textBoxZDFT.Text = "1.57";
            }
            else if (selectedValue == "C40")
            {
                textBoxZDEC.Text = "32500";
                textBoxZDFT.Text = "1.71";
            }
            else if (selectedValue == "C45")
            {
                textBoxZDEC.Text = "33500";
                textBoxZDFT.Text = "1.8";
            }
            else if (selectedValue == "C50")
            {
                textBoxZDEC.Text = "34500";
                textBoxZDFT.Text = "1.89";
            }
            else if (selectedValue == "C55")
            {
                textBoxZDEC.Text = "35500";
                textBoxZDFT.Text = "1.96";
            }
            else if (selectedValue == "C60")
            {
                textBoxZDEC.Text = "36000";
                textBoxZDFT.Text = "2.04";
            }
            else if (selectedValue == "C65")
            {
                textBoxZDEC.Text = "36500";
                textBoxZDFT.Text = "2.09";
            }
            else if (selectedValue == "C70")
            {
                textBoxZDEC.Text = "37000";
                textBoxZDFT.Text = "2.14";
            }
            else if (selectedValue == "C75")
            {
                textBoxZDEC.Text = "37500";
                textBoxZDFT.Text = "2.18";
            }
            else if (selectedValue == "C80")
            {
                textBoxZDEC.Text = "38000";
                textBoxZDFT.Text = "2.22";
            }
        }

        private void checkBoxZDGJML_CheckedChanged(object sender, EventArgs e)
        {
            textBoxES.Enabled = checkBoxZDGJML.Checked;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var savefiledialog = new System.Windows.Forms.SaveFileDialog();
            savefiledialog.Filter = "文本文件(*.txt)|*.txt";
            savefiledialog.Title = "保存输出结果";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                string outputpath = savefiledialog.FileName;
                using (StreamWriter writer = new StreamWriter(outputpath, true))
                {
                    writer.Write(outresult+"\n"+"\n"+"该程序由福建省电力勘测设计院有限公司发电分公司开发\n");
                }
                MessageBox.Show("结果已经保存至文件" + outputpath);
            }
        }
    }
}
