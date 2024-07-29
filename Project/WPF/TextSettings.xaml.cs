using Project.Properties;
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
using System.Windows.Shapes;

namespace Project.WPF
{
    /// <summary>
    /// TextSettings.xaml 的交互逻辑
    /// </summary>
    public partial class TextSettings : Window
    {

        private static Settings ThisOptions => Settings.Default;
        public TextSettings()
        {
            InitializeComponent();
            this.DataContext = ThisOptions;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(DialogResult == true)
            {
                // 参数设定
                if (ThisOptions.TextHeight < 0) ThisOptions.TextHeight = 300;
                if (ThisOptions.ColorIndex < 0) ThisOptions.ColorIndex = 7;
                // 如果是确定就将前端设置的参数进行保存,设置为默认参数，这样就实现了参数传递的功能
                ThisOptions.Save();
            }
            else
            {
                // 不是的话就将参数回滚
                ThisOptions.Reload();
            }
        }
    }
}
