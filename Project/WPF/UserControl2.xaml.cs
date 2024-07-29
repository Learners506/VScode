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
using PropertyChanged;

namespace Project.WPF;

    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
public partial class UserControl2 : System.Windows.Controls.UserControl
{
    public UserControl2()
    {
        InitializeComponent();
        this.DataContext = CstfModel.Default;
    }
}
[AddINotifyPropertyChangedInterface]
public class CstfModel
{
    public static CstfModel Default { get;} = new CstfModel();
    private CstfModel() { }
    // 设置参数变量
    public double W { get; set; } = 100;
    public double Dp {  get; set; } = 50;
    public double Ds { get; set; } = 20;
    public double T { get; set; } = 20;
    public double Pa {  get; set; } = 101.325;
    public double Density => 1.293 * (Pa / 101.325) * (273.15 / (273.15 + T));
    public string DensityText => Density.ToString("0.00#");
    public double L => W / (Density * (Dp - Ds));
    public double LText => Math.Ceiling(L);
    public double Coefficient { get; set; } = 1.1;
    public double Volume => Math.Ceiling(L*Coefficient);

}

