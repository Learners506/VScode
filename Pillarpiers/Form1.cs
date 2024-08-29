﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using ClosedXML;
using System.IO;
using ClosedXML.Excel;


namespace Pillarpiers
{
    public partial class Form1 : Form
    {
        private DataTable exceldata1;
        public Form1()
        {
            InitializeComponent();
            radioButtonJX.Checked = true;
            textBoxYXBJ1.Enabled = false;
            textBoxYXGD1.Enabled = false;
            textBoxWMGD.Enabled = false;
            comboBox4.Enabled = false;

            radioButtonJX.CheckedChanged += RadioButton_CheckChanged;
            radioButtonYX.CheckedChanged += RadioButton_CheckChanged;

        }
        private void RadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (radioButtonJX.Checked)
            {
                textBoxJXGD1.Enabled = true;
                textBoxJXKD1.Enabled = true;
                textBoxJXCD1.Enabled = true;
                textBoxYXBJ1.Enabled = false;
                textBoxYXGD1.Enabled = false;
            }
            else if (radioButtonYX.Checked)
            {
                textBoxJXGD1.Enabled = false;
                textBoxJXKD1.Enabled = false;
                textBoxJXCD1.Enabled = false;
                textBoxYXBJ1.Enabled = true;
                textBoxYXGD1.Enabled = true;
            }
        }

        // 取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 开始计算按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (!AreTextBoxesFilled(textBoxMCXS1,textBoxQHLZJJ1,textBoxZJZXJJ1,textBoxSPL1,textBoxBL1,textBoxWJ1))
            {
                return;
            }
            if (radioButtonJX.Checked)
            {
                if (!AreTextBoxesFilled(textBoxJXGD1,textBoxJXCD1,textBoxJXKD1))
                {
                    return;
                }
            }
            else
            {
                if (!AreTextBoxesFilled(textBoxYXBJ1,textBoxYXGD1))
                {
                    return;
                }
            }

            double 底面摩擦系数 = Convert.ToDouble(textBoxMCXS1.Text);
            double 前后立柱间距 = Convert.ToDouble(textBoxQHLZJJ1.Text);
            double 支架纵向间距 = Convert.ToDouble(textBoxZJZXJJ1.Text);

            double 矩形基础高度 = 0;
            double 矩形基础宽度 = 0;
            double 矩形基础长度 = 0;
            double 圆形基础高度 = 0;
            double 圆形基础半径 = 0;
            double 基础自重 = 0;
            if (radioButtonJX.Checked)
            {
                矩形基础高度 = Convert.ToDouble(textBoxJXGD1.Text);
                矩形基础宽度 = Convert.ToDouble(textBoxJXKD1.Text);
                矩形基础长度 = Convert.ToDouble(textBoxJXCD1.Text);
                基础自重 = 矩形基础宽度 * 矩形基础长度 * 矩形基础高度 * 24;
            }
            else if (radioButtonYX.Checked)
            {
                圆形基础半径 = Convert.ToDouble(textBoxYXBJ1.Text);
                圆形基础高度 = Convert.ToDouble(textBoxYXGD1.Text);
                基础自重 = Math.PI*圆形基础半径 * 圆形基础半径 * 圆形基础高度 * 24;
            }
            textBoxJCZZ1.Text = 基础自重.ToString();

            double 水平力 = Convert.ToDouble(textBoxSPL1.Text);
            double 拔力 = Convert.ToDouble(textBoxBL1.Text);
            double 弯矩 = Convert.ToDouble(textBoxWJ1.Text);

            double 总拔力 = 拔力;
            textBoxZBL1.Text = 总拔力.ToString("F2");
            double 抗拔稳定系数 = 基础自重 / 总拔力;
            textBoxKBWDXS1.Text = 抗拔稳定系数.ToString("F2");
            string resultKB1 = (拔力>0 && 抗拔稳定系数<1.6) ? "不满足" : "满足";
            textBoxKBMZ1.Text = resultKB1;
            if (resultKB1 == "不满足") 
            {
                textBoxKBMZ1.BackColor = Color.Red;
            }
            double 总抗滑力 = (基础自重 - 总拔力) * 底面摩擦系数;
            textBoxZKHL1.Text = 总抗滑力.ToString("F2");
            double 总滑动力 = 水平力;
            textBoxZHDL1.Text = 总滑动力.ToString("F2");
            double 抗滑稳定系数 = Math.Abs(总抗滑力/总滑动力);
            textBoxKHWDXS1.Text = 抗滑稳定系数.ToString("F2");
            string resultKH1 = (抗滑稳定系数<1.3) ? "不满足" : "满足";
            textBoxKHMZ1.Text = resultKH1;
            if (resultKH1 == "不满足")
            {
                textBoxKHMZ1.BackColor = Color.Red;
            }
            double 总抗倾覆力 = 0;
            double 总倾覆力 = 0;
            if (radioButtonJX.Checked)
            {
                if (拔力 >= 0)
                {
                    总抗倾覆力 = 基础自重 * 矩形基础长度 / 2;
                    总倾覆力 = Math.Abs(拔力) * 矩形基础长度 / 2 + Math.Abs(水平力) * 矩形基础高度 + Math.Abs(弯矩);

                }
                else
                {
                    总抗倾覆力 = 基础自重 * 矩形基础长度 / 2 + 矩形基础长度/2*Math.Abs(拔力);
                    总倾覆力 = Math.Abs(水平力) * 矩形基础高度 + Math.Abs(弯矩);
                }
                
            }
            else if (radioButtonYX.Checked)
            {
                if (拔力 >= 0)
                {
                    总抗倾覆力 = 基础自重 * 圆形基础半径;
                    总倾覆力 = Math.Abs(拔力)*圆形基础半径+Math.Abs(水平力)* 圆形基础高度 + Math.Abs(弯矩);

                }
                else
                {
                    总抗倾覆力 = 基础自重 * 圆形基础半径 + 圆形基础半径 * Math.Abs(拔力);
                    总倾覆力 = Math.Abs(水平力) * 圆形基础高度 + Math.Abs(弯矩);
                }
            }
            textBoxZKQF1.Text = 总抗倾覆力.ToString("F2");
            textBoxZQFL1.Text = 总倾覆力.ToString("F2");

            double 抗倾覆稳定系数 = 总抗倾覆力 / 总倾覆力;
            textBoxKQFWDXS1.Text = 抗倾覆稳定系数.ToString("F2");
            string resultKQF1 = (抗倾覆稳定系数 >= 1.6) ? "满足" : "不满足";
            textBoxKQFMZ1.Text = resultKQF1;
            if (resultKQF1 == "不满足")
            {
                textBoxKQFMZ1.BackColor = Color.Red;
            }
        }

        // 检查参数是否输入完整
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

        // 导入excel文件进行批量验算
        private void loadfile1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                openFileDialog.Title = "请选择传入的Excel文件";
                // 如果文件选择正确
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //获取文件路径
                    string filepath = openFileDialog.FileName;
                    exceldata1 = ReadExcelFile(filepath);  // 这个exceldata1为DataTale数据
                    // 判断传入的excel数据是否正确
                    if (exceldata1 != null) 
                    { 
                        comboBox1.Items.Clear(); //清除选项
                        // 第一行为标题行，不写入
                        for(int i = 1; i < exceldata1.Rows.Count; i++)
                        {
                            // 定义行数据，从读取的datatable的第二行数据开始
                            DataRow row = exceldata1.Rows[i];
                            // 获取第一列和第二列的拼接字符串为下拉框的选项
                            string item = $"{row[0]} {row[1]}";
                            // 将选项添加到下拉框中
                            comboBox1.Items.Add(item);
                        }
                        MessageBox.Show("导入成功，请在下拉框中选择数据");
                    }
                }
            }
        }

        // 导入excel数据为datatable
        private DataTable ReadExcelFile(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using(var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    return result.Tables[0];
                }
            }
        }

        // 页面一下拉框变化时
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >=0 && exceldata1 != null)
            {
                DataRow SelectedRow = exceldata1.Rows[comboBox1.SelectedIndex + 1]; // 根据选择数据的索引，索引到那一行
                textBoxSPL1.Text = Math.Max(Math.Abs(Convert.ToDouble(SelectedRow[4])), Math.Abs(Convert.ToDouble(SelectedRow[5]))).ToString();
                textBoxBL1.Text = SelectedRow[6].ToString();
                textBoxWJ1.Text = SelectedRow[7].ToString();
            }
        }

        // 取消按钮
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 双立柱计算
        private void button5_Click(object sender, EventArgs e)
        {
            if (!AreTextBoxesFilled(textBoxMCXS2, textBoxQHLZJJ2, textBoxZJZXJJ2, textBoxJCGD2, textBoxJCKD2, textBoxJQZWS2, textBoxJHZWS2, textBoxQSPL2, textBoxHSPL2, textBoxQBL2, textBoxHBL2, textBoxQWJ2, textBoxHWJ2)) 
            {
                return;
            }
            double 底面摩擦系数 = Convert.ToDouble(textBoxMCXS2.Text);
            double 前后立柱间距 = Convert.ToDouble(textBoxQHLZJJ2.Text);
            double 支架纵向间距 = Convert.ToDouble(textBoxZJZXJJ2.Text);
            double 条形基础高度 = Convert.ToDouble(textBoxJCGD2.Text);
            double 条形基础宽度 = Convert.ToDouble(textBoxJCKD2.Text);
            double 基础前柱外伸 = Convert.ToDouble(textBoxJQZWS2.Text);
            double 基础后柱外伸 = Convert.ToDouble(textBoxJHZWS2.Text);

            double 前水平力 = Convert.ToDouble(textBoxQSPL2.Text);
            double 后水平力 = Convert.ToDouble(textBoxHSPL2.Text);
            double 前拔力 = Convert.ToDouble(textBoxQBL2.Text);
            double 后拔力 = Convert.ToDouble(textBoxHBL2.Text);
            double 前弯矩 = Convert.ToDouble(textBoxQWJ2.Text);
            double 后弯矩 = Convert.ToDouble(textBoxHWJ2.Text);

            double 基础长度 = 前后立柱间距 + 基础前柱外伸 + 基础后柱外伸;
            textBoxTJZC2.Text = 基础长度.ToString("F2");
            double 基础自重 = 24 * 基础长度 * 条形基础宽度 * 条形基础高度;
            textBoxJCZZ2.Text = 基础自重.ToString("F2");

            double 总拔力 = 前拔力 + 后拔力;
            textBoxZBL2.Text = 总拔力.ToString("F2");
            double 抗拔稳定系数 = 基础自重 / 总拔力;
            textBoxKBWDXS2.Text = 抗拔稳定系数.ToString("F2");
            string resultKB2 = (抗拔稳定系数 >= 1.6) ? "满足" : "不满足";
            textBoxKBMZ2.Text = resultKB2;
            if(resultKB2 == "不满足")
            {
                textBoxKBMZ2.BackColor = Color.Red;
            }
            double 总抗滑力 = (基础自重 - 总拔力) * 底面摩擦系数;
            textBoxZKHL2.Text = 总抗滑力.ToString("F2");
            double 总滑动力 = 前水平力 + 后水平力;
            textBoxZHDL2.Text = 总滑动力.ToString("F2");
            double 抗滑稳定系数 = Math.Abs(总抗滑力 / 总滑动力);
            textBoxKHWDXS2.Text = 抗滑稳定系数.ToString("F2");
            string resultKH2 = (抗滑稳定系数 >= 1.3) ? "满足" : "不满足";
            textBoxKHMZ2.Text = resultKH2;
            if(resultKH2 == "不满足")
            {
                textBoxKHMZ2.BackColor = Color.Red;
            }
            double 总抗倾覆力 = 0;
            double 总倾覆力 = 0;
            if (前拔力>=0)
            {
                总抗倾覆力 = 基础自重 * 基础长度 / 2;
                总倾覆力 = Math.Abs(前弯矩 + 后弯矩) + 条形基础高度 * Math.Abs(前水平力 + 后水平力) + 基础前柱外伸 * Math.Abs(前拔力) + (基础前柱外伸 + 前后立柱间距) * Math.Abs(后拔力);
            }
            else
            {
                总抗倾覆力 = 基础自重 * 基础长度 / 2 + 基础前柱外伸 * Math.Abs(前拔力);
                总倾覆力 = Math.Abs(前弯矩 + 后弯矩)+ 条形基础高度 * Math.Abs(前水平力 + 后水平力) + (基础前柱外伸 + 前后立柱间距) * Math.Abs(后拔力);
            }
            textBoxZKQFL2.Text = 总抗倾覆力.ToString("F2");
            textBoxZQFL2.Text = 总倾覆力.ToString("F2") ;
            double 抗倾覆稳定系数 = 总抗倾覆力 / 总倾覆力;
            textBoxKQFWDXS2.Text = 抗倾覆稳定系数.ToString("F2");
            string resultKQH2 = (抗倾覆稳定系数 >= 1.6) ? "满足" : "不满足";
            textBoxKQFMZ2.Text = resultKQH2 ;
            if(resultKQH2 == "不满足")
            {
                textBoxKQFMZ2.BackColor = Color.Red;
            }
        }

        // 取消按钮
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //三立柱计算
        private void button9_Click(object sender, EventArgs e)
        {
            if (!AreTextBoxesFilled(textBoxMCXS3, textBoxJCGD3, textBoxJCKD3, textBoxQZZJJ3,
                textBoxZHZJJ3, textBoxZJZXJJ3, textBoxJQZWS3, textBoxJHZWS3, textBoxQSPL3, 
                textBoxZLSPL3, textBoxHSPL3, textBoxQBL3, textBoxZLBL3, textBoxHBL3, textBoxQWJ3, textBoxZLWJ3,textBoxHWJ3))
            {
                return;
            }
            double 底面摩擦系数 = Convert.ToDouble(textBoxMCXS3.Text);
            double 条形基础高度 = Convert.ToDouble(textBoxJCGD3.Text);
            double 条形基础宽度 = Convert.ToDouble(textBoxJCKD3.Text);
            double 前中立柱间距 = Convert.ToDouble(textBoxQZZJJ3.Text);
            double 中后立柱间距 = Convert.ToDouble(textBoxZHZJJ3.Text);
            double 支架纵向间距 = Convert.ToDouble(textBoxZJZXJJ3.Text);
            double 条基前柱外伸 = Convert.ToDouble(textBoxJQZWS3.Text);
            double 条基后柱外伸 = Convert.ToDouble(textBoxJHZWS3.Text);
            double 前水平力 = Convert.ToDouble(textBoxQSPL3.Text);
            double 中水平力 = Convert.ToDouble(textBoxZLSPL3.Text);
            double 后水平力 = Convert.ToDouble(textBoxHSPL3.Text);
            double 前拔力 = Convert.ToDouble(textBoxQBL3.Text);
            double 中拔力 = Convert.ToDouble(textBoxZLBL3.Text);
            double 后拔力 = Convert.ToDouble(textBoxHBL3.Text);
            double 前弯矩 = Convert.ToDouble(textBoxQWJ3.Text);
            double 中弯矩 = Convert.ToDouble(textBoxZLWJ3.Text);
            double 后弯矩 = Convert.ToDouble(textBoxHWJ3.Text);

            double 条形基础总长度 = 前中立柱间距 + 中后立柱间距 + 条基前柱外伸 + 条基后柱外伸;
            textBoxTJZC3.Text = 条形基础总长度.ToString("F2");
            double 条形基础自重 = 24 * 条形基础总长度 * 条形基础宽度 * 条形基础高度;
            textBoxJCZZ3.Text = 条形基础自重.ToString("F2");
            double 总拔力 = 前拔力 + 中拔力 + 后拔力;
            textBoxZBL3.Text = 总拔力.ToString("F2");
            double 抗拔稳定系数 = 条形基础自重 / 总拔力;
            textBoxKBWDXS3.Text = 抗拔稳定系数.ToString("F2");
            string resultKB3 = (抗拔稳定系数 >= 1.6) ? "满足" : "不满足";
            textBoxKBMZ3.Text = resultKB3;
            if (resultKB3 == "不满足")
            {
                textBoxKBMZ3.BackColor = Color.Red;
            }
            double 总抗滑力 = (条形基础自重 - 总拔力) * 底面摩擦系数;
            textBoxZKHL3.Text = 总抗滑力.ToString("F2");

            double 总滑动力 = 前水平力 + 中水平力 + 后水平力;
            textBoxZHDL3.Text = 总滑动力.ToString("F2");
            double 抗滑稳定系数 = Math.Abs(总抗滑力/总滑动力);
            textBoxKHWDXS3.Text = 抗滑稳定系数.ToString("F2");
            string resultKH3 = (抗滑稳定系数 >= 1.3) ? "满足" : "不满足";
            textBoxKHMZ3.Text = resultKH3;
            if (resultKH3 == "不满足")
            {
                textBoxKHMZ3.BackColor = Color.Red;
            }
            double 总抗倾覆力 = 条形基础自重 * 条形基础总长度 / 2;
            textBoxZKQF3.Text = 总抗倾覆力.ToString("F2");
            double 总倾覆力 = Math.Abs(前弯矩 + 中弯矩 + 后弯矩) + 条形基础高度 * Math.Abs(前水平力 + 中水平力 + 后水平力) + 条基前柱外伸 * Math.Abs(前拔力) + (前中立柱间距 + 条基前柱外伸) * Math.Abs(中拔力) + (前中立柱间距 + 中后立柱间距 + 条基前柱外伸) * Math.Abs(后拔力);
            textBoxZQFL3.Text = 总倾覆力.ToString("F2");
            double 抗倾覆稳定系数 = 总抗倾覆力 / 总倾覆力;
            textBoxKQFWDXS3.Text = 抗倾覆稳定系数.ToString();
            string resultKQH3 = (抗倾覆稳定系数 >= 1.6) ? "满足" : "不满足";
            textBoxKQFMZ3.Text = resultKQH3;
            if (resultKQH3 == "不满足")
            {
                textBoxKQFMZ3.BackColor = Color.Red;
            }
        }

        // 屋面彩钢瓦计算
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 屋面彩钢瓦开始计算
        private void button3_Click(object sender, EventArgs e)
        {
            if (!AreTextBoxesFilled(textBox25JBFY,textBox50JBFY,textBoxFZXS,textBoxFYTXXS,textBoxFXTXXS,textBoxCZHLCD,textBoxPXHLCD,textBoxGFBZL,textBoxHLZL,textBoxHLKD)) 
            {
                return;
            }
            if (checkBox1.Checked)
            {
                if (!AreTextBoxesFilled(textBoxWMGD))
                {
                    return;
                }
            }
            else 
            {
                if (!AreTextBoxesFilled(textBoxGBBHXS))
                {
                    return;
                }
            }

            double 二五年基本分压 = Convert.ToDouble(textBox25JBFY.Text);
            double 五十年基本分压 = Convert.ToDouble(textBox50JBFY.Text);
            double 风振系数 = Convert.ToDouble(textBoxFZXS.Text);
            double 风压体型系数 = Convert.ToDouble(textBoxFYTXXS.Text);
            double 风吸体型系数 = Convert.ToDouble(textBoxFXTXXS.Text);
            double 风压高度变化系数 = 0;
            if (checkBox1.Checked) 
            { 
                double 屋面高度 = Convert.ToDouble(textBoxWMGD.Text);
                string 粗糙度类别 = comboBox4.Text;
                if (屋面高度 > 30)
                {
                    MessageBox.Show("屋面高度大于30m,请手动输入风压高度变化系数", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                风压高度变化系数 = getgdbhxs(屋面高度, 粗糙度类别);
                textBoxGBBHXS.Text = 风压高度变化系数.ToString();
            }
            else
            {
                风压高度变化系数 = Convert.ToDouble(textBoxGBBHXS.Text);
            }
            
            double 光伏垂直横梁长度 = Convert.ToDouble(textBoxCZHLCD.Text);
            double 光伏平行横梁长度 = Convert.ToDouble(textBoxPXHLCD.Text);
            double 光伏板重量 = Convert.ToDouble(textBoxGFBZL.Text);
            double 横梁重量 = Convert.ToDouble(textBoxHLZL.Text);
            double 横梁跨度 = Convert.ToDouble(textBoxHLKD.Text);

            double 二五年风压标准值 = 二五年基本分压*风振系数*风压高度变化系数*风压体型系数;
            double 二五年风吸标准值 = 二五年基本分压 * 风振系数 * 风压高度变化系数 * 风吸体型系数;
            double 五十年风压标准值 = 五十年基本分压 * 风振系数 * 风压高度变化系数 * 风压体型系数;
            double 五十年风吸标准值 = 五十年基本分压 * 风振系数 * 风压高度变化系数 * 风吸体型系数;

            double 横梁所受恒荷载 = (光伏板重量 * 10 / 1000 / 光伏平行横梁长度) / 2 + 横梁重量 * 10 / 1000;
            double 横梁所受25年风压荷载 = 二五年风压标准值 * 光伏垂直横梁长度 / 2;
            double 横梁所受25年风吸荷载 = 二五年风吸标准值 * 光伏垂直横梁长度 / 2;
            double 横梁所受50年风压荷载 = 五十年风压标准值 * 光伏垂直横梁长度 / 2;
            double 横梁所受50年风吸荷载 = 五十年风吸标准值 * 光伏垂直横梁长度 / 2;

            double 风压基本工况设计值 = 1.3 * 横梁所受恒荷载 + 1.5 * 横梁所受25年风压荷载;
            double 风吸基本工况设计值 = 1 * 横梁所受恒荷载 - 1.5 * 横梁所受25年风吸荷载;
            Console.WriteLine(风压基本工况设计值);
            Console.WriteLine(横梁跨度);
            double 横梁所受弯矩值 =风压基本工况设计值*横梁跨度*横梁跨度/8 ;
            double 单个夹具所受拉力设计值 = 1.5 * 横梁所受50年风吸荷载 * 横梁跨度 - 横梁所受恒荷载 * 横梁跨度;

            textBoxHHZ.Text = 横梁所受恒荷载.ToString();
            textBox25FYHZ.Text = 横梁所受25年风压荷载.ToString();
            textBox25FXHZ.Text = 横梁所受25年风吸荷载.ToString();
            textBox50FYHZ.Text = 横梁所受50年风压荷载.ToString();
            textBox50FXHZ.Text = 横梁所受50年风吸荷载.ToString();
            textBoxFYSJZ.Text = 风压基本工况设计值.ToString();
            textBoxFXSJZ.Text = 风吸基本工况设计值.ToString();
            textBoxWJSJZ.Text = 横梁所受弯矩值.ToString();
            textBoxLLSJZ.Text = 单个夹具所受拉力设计值.ToString();



        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBoxWMGD.Enabled = true;
                comboBox4.Enabled = true;
            }
            else
            {
                textBoxWMGD.Enabled = false;
                comboBox4.Enabled = false;
            }
        }

        private double getgdbhxs(double wmgd,string ccdlb)
        {
            double 高度变化系数 = 0;
            // 定义已知点和对应的值
            double[] knownValues = { 0, 5, 10, 15, 20, 30 };
            double[] correspondingResultsA = { 1.09, 1.09, 1.28, 1.42, 1.52, 1.67 };
            double[] correspondingResultsB = { 1.00, 1.00, 1.00, 1.13, 1.23, 1.39 };
            double[] correspondingResultsC = { 0.65, 0.65, 0.65, 0.65, 0.74, 0.88 };
            double[] correspondingResultsD = { 0.51, 0.51, 0.51, 0.51, 0.51, 0.51 };

            for(int i = 0; i < knownValues.Length - 1; i++)
            {
                if (wmgd >= knownValues[i] && wmgd <= knownValues[i + 1])
                {
                    if (ccdlb == "A")
                    {
                        double t = (wmgd - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                        return correspondingResultsA[i] + t * (correspondingResultsA[i + 1] - correspondingResultsA[i]);
                    }
                    else if (ccdlb == "B")
                    {
                        double t = (wmgd - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                        return correspondingResultsB[i] + t * (correspondingResultsB[i + 1] - correspondingResultsB[i]);
                    }
                    else if (ccdlb == "C")
                    {
                        double t = (wmgd - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                        return correspondingResultsC[i] + t * (correspondingResultsC[i + 1] - correspondingResultsC[i]);
                    }
                    else if (ccdlb == "D")
                    {
                        double t = (wmgd - knownValues[i]) / (knownValues[i + 1] - knownValues[i]);
                        return correspondingResultsD[i] + t * (correspondingResultsD[i + 1] - correspondingResultsD[i]);
                    }
                    
                }
            }
            return 高度变化系数;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            exceldata1.Columns.Add("条形基础自重", typeof(double));
            exceldata1.Columns.Add("总拔力", typeof(double));
            exceldata1.Columns.Add("抗拔稳定系数", typeof(double));
            exceldata1.Columns.Add("是否满足抗拔稳定验算", typeof(string));
            exceldata1.Columns.Add("总抗滑力", typeof(double));
            exceldata1.Columns.Add("总滑动力", typeof(double));
            exceldata1.Columns.Add("抗滑移稳定系数", typeof(double));
            exceldata1.Columns.Add("是否满足抗滑移稳定验算", typeof(string));
            exceldata1.Columns.Add("总抗倾覆力", typeof(double));
            exceldata1.Columns.Add("总倾覆力", typeof(double));
            exceldata1.Columns.Add("抗倾覆稳定系数", typeof(double));
            exceldata1.Columns.Add("是否满足抗倾覆稳定验算", typeof(string));

            for (int i = 1; i < exceldata1.Rows.Count; i++)
            {
                DataRow selectedRow = exceldata1.Rows[i];
                double valuex3 = Math.Abs(Convert.ToDouble(selectedRow[4]));
                double valuex4 = Math.Abs(Convert.ToDouble(selectedRow[5]));


                var spl = Convert.ToDouble(Math.Max(valuex3, valuex4).ToString());
                var zbl = Convert.ToDouble(selectedRow[6].ToString());
                var wj = Convert.ToDouble(selectedRow[7].ToString());

                var height = Convert.ToDouble(textBoxJXGD1.Text);
                var width = Convert.ToDouble(textBoxJXKD1.Text);
                var longth = Convert.ToDouble(textBoxJXCD1.Text);
                var Weight = 24 * height * width * longth;

                var kbxs = Weight / zbl;
                var mcxs = Convert.ToDouble(textBoxMCXS1.Text);
                var khl = (Weight - zbl) * mcxs;
                var hdl = Convert.ToDouble(textBoxSPL1.Text);
                var khxs = khl / hdl;
                double zkqfl;
                double zqfl;
                var jcg = Convert.ToDouble(textBoxJXGD1.Text);
                var jcc = Convert.ToDouble(textBoxJXCD1.Text);
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
                ExportDataTableToExcel(exceldata1, savepath);
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
