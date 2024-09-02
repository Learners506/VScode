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
using ZZZIFOX.Settings;

namespace ZZZIFOX.WPFUI
{
    /// <summary>
    /// NewCopyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewCopyWindow : Window
    {
        private static NewCopy settings => NewCopy.Default;
        public NewCopyWindow()
        {
            InitializeComponent();
            //数据绑定
            this.DataContext = settings;
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
            if (DialogResult == true)
            {
                settings.Save();
            }else
            {
                settings.Reload();
            }
        }

        
    }
}
