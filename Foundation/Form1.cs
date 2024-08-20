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

namespace Foundation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 设置初始状态
            radioSingle.Checked = true;
            textBoxTCH.Enabled = false;
            textBoxCZ2.Enabled = false;
            textBoxDZ2.Enabled = false;
            textBoxKB2.Enabled = false;
            radioSingle.CheckedChanged += RadioButton_CheckedChanged;
            radioMulty.CheckedChanged += RadioButton_CheckedChanged;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            // 注册 Form1_Load 事件处理程序
            this.Load += new EventHandler(Form1_Load);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
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


    }
}
