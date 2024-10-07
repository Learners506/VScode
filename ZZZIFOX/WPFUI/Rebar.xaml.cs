using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZZZIFOX.WPFUI
{
    /// <summary>
    /// Rebar.xaml 的交互逻辑
    /// </summary>
    public partial class Rebar : System.Windows.Controls.UserControl
    {
        public Rebar()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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
            if (使用年限 == "50" && 混凝土强度 == "小等于C25")
            {
                textBoxBHCHD.Text = Convert.ToString(Convert.ToDouble(最小保护层) + 5);
            }
            else if (使用年限 == "50" && 混凝土强度 == "大于C25")
            {
                textBoxBHCHD.Text = 最小保护层;
            }
            else if (使用年限 == "100" && 混凝土强度 == "小等于C25")
            {
                textBoxBHCHD.Text = Convert.ToString(Convert.ToDouble(最小保护层) * 1.4 + 5);
            }
            else if (使用年限 == "100" && 混凝土强度 == "大于C25")
            {
                textBoxBHCHD.Text = Convert.ToString(Convert.ToDouble(最小保护层) * 1.4);
            }
            else
            {
                textBoxBHCHD.Text = "0"; // 默认值
            }

        }

        
    }
}
