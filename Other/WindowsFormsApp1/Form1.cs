using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取用户输入
                double startValue = double.Parse(txtStartValue.Text);
                double targetValue = double.Parse(txtTargetValue.Text);
                double interval = double.Parse(txtInterval.Text);

                // 设置保存文件对话框
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text File|*.txt";
                saveFileDialog.Title = "保存结果到 TXT 文件";
                saveFileDialog.ShowDialog();

                // 如果用户选择了保存路径
                if (saveFileDialog.FileName != "")
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        double currentValue = startValue;
                        while (currentValue <= targetValue)
                        {
                            writer.WriteLine(Math.Round(currentValue, 3));
                            currentValue += interval;
                        }
                    }
                    MessageBox.Show("文件生成成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("notepad.exe", saveFileDialog.FileName);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
