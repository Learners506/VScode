using Autodesk.AutoCAD.DatabaseServices;
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
    /// TranslateUI.xaml 的交互逻辑
    /// </summary>
    public partial class TranslateUI : Window
    {
        private static Translateset ThisOptions => Translateset.Default;
        private bool _lastChecked;
        public TranslateUI()
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
                ThisOptions.Save();
            }
            else
            {
                ThisOptions.Reload();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> textStyles = new List<string>();
            onetwo.IsChecked = ThisOptions.modeonetwo;
            oneone.IsChecked = !ThisOptions.modeonetwo;

            twoone.IsChecked = ThisOptions.modetwoone;
            twotwo.IsChecked = !ThisOptions.modetwoone;

            ztysrbn.IsChecked = ThisOptions.ztstyle;
            _lastChecked = ztysrbn.IsChecked ?? false;
            using var tr = new DBTrans();
            foreach(var item in tr.TextStyleTable)
            {
                var textstyle = (TextStyleTableRecord)tr.GetObject(item,OpenMode.ForRead);
                textStyles.Add(textstyle.Name);
            }
            ztyscom.ItemsSource = textStyles;
           
        }

        private void onetwo_Checked(object sender, RoutedEventArgs e)
        {
            ThisOptions.modeonetwo = true;
        }

        private void onetwo_Unchecked(object sender, RoutedEventArgs e)
        {
            ThisOptions.modeonetwo = false;
        }

        private void twoone_Checked(object sender, RoutedEventArgs e)
        {
            ThisOptions.modetwoone = true;
        }

        private void twoone_Unchecked(object sender, RoutedEventArgs e)
        {
            ThisOptions.modetwoone = false;
        }

        private void zkcom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
               zk.Text = zkcom.Text;
               ThisOptions.width = Convert.ToDouble(zkcom.Text);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void zgcom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                zg.Text = zgcom.Text;
                ThisOptions.height = Convert.ToDouble(zgcom.Text);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void hjcom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                hj.Text = hjcom.Text;
                ThisOptions.distance = Convert.ToDouble(hjcom.Text);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void ztyscom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ztstyle.Text = ztyscom.Text;
                ThisOptions.style = ztyscom.Text;
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void zkbt_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
            double twidth = 0;
            while (true)
            {
                // 提示用户选择一个实体
                PromptEntityOptions peo = new PromptEntityOptions("\n请选择一个实体: ");
                PromptEntityResult per = Env.Editor.GetEntity(peo);

                if (per.Status == PromptStatus.Cancel)
                {
                    break;
                }
                else if (per.Status != PromptStatus.OK)
                {
                    Env.Editor.WriteMessage("\n选择取消或失败，请重新选择.");
                    continue;
                    
                }
                using var trans = new DBTrans();
                Entity ent = trans.GetObject(per.ObjectId, OpenMode.ForRead) as Entity;
                
                if (ent is DBText dbText)
                {

                    
                    twidth = dbText.WidthFactor;
                    break;
                }
                else if (ent is MText mText)
                {
                    var mtextstyle = trans.GetObject(mText.TextStyleId, OpenMode.ForRead) as TextStyleTableRecord;
                    twidth = mtextstyle.XScale;
                    break;
                }
                else
                {
                    Env.Editor.WriteMessage("\n选择的不是文字，请重新选择.");
                }
            }
            zk.Text = twidth.ToString();
            ThisOptions.width = twidth;
            new TranslateUI().ShowDialog();
        }

        private void zgbt_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
            double theight = 0;
            while (true)
            {
                // 提示用户选择一个实体
                PromptEntityOptions peo = new PromptEntityOptions("\n请选择一个实体: ");
                PromptEntityResult per = Env.Editor.GetEntity(peo);

                if (per.Status == PromptStatus.Cancel)
                {
                    break;
                }
                else if (per.Status != PromptStatus.OK)
                {
                    Env.Editor.WriteMessage("\n选择取消或失败，请重新选择.");
                    continue;

                }
                using var trans = new DBTrans();
                Entity ent = trans.GetObject(per.ObjectId, OpenMode.ForRead) as Entity;

                if (ent is DBText dbText)
                {


                    theight = dbText.Height;
                    break;
                }
                else if (ent is MText mText)
                {
                    theight = mText.TextHeight;
                    break;
                }
                else
                {
                    Env.Editor.WriteMessage("\n选择的不是文字，请重新选择.");
                }
            }
            zg.Text = theight.ToString();
            ThisOptions.height = theight;
            new TranslateUI().ShowDialog();
        }

        private void hjbt_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
            double thjj = 0;
            while (true)
            {
                // 提示用户选择一个实体
                PromptEntityOptions peo = new PromptEntityOptions("\n请选择一个实体: ");
                PromptEntityResult per = Env.Editor.GetEntity(peo);

                if (per.Status == PromptStatus.Cancel)
                {
                    break;
                }
                else if (per.Status != PromptStatus.OK)
                {
                    Env.Editor.WriteMessage("\n选择取消或失败，请重新选择.");
                    continue;

                }
                using var trans = new DBTrans();
                Entity ent = trans.GetObject(per.ObjectId, OpenMode.ForRead) as Entity;

                if (ent is MText mtext)
                {
                    thjj = mtext.LineSpacingFactor;
                    break;
                }
                else
                {
                    Env.Editor.WriteMessage("\n选择的不是文字，请重新选择.");
                }
            }
            hj.Text = thjj.ToString();
            ThisOptions.distance = thjj;
            new TranslateUI().ShowDialog();
        }

        private void ztysrbn_Checked(object sender, RoutedEventArgs e)
        {
            ThisOptions.ztstyle = true;
        }

        private void ztysrbn_Unchecked(object sender, RoutedEventArgs e)
        {
            ThisOptions.ztstyle = false;
        }

        private void ztysrbn_Click(object sender, RoutedEventArgs e)
        {
            if (_lastChecked)
            {
                ztysrbn.IsChecked = false;
            }
            _lastChecked = ztysrbn.IsChecked ?? false;
        }
    }
}
