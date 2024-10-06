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
    /// TestPanelSet.xaml 的交互逻辑
    /// </summary>
    public partial class TestPanelSet : System.Windows.Controls.UserControl
    {
        public TestPanelSet()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bt = (System.Windows.Controls.Button)sender;
            Acap.DocumentManager.MdiActiveDocument.SendStringToExecute("\u001b" + bt.Tag + "\n", true, false, true);
        }
    }
}
