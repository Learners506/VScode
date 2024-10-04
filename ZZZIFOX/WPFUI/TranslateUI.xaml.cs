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
            var p1 = Env.Editor.GetPoint("请指定第一个点");
            var p2key = new PromptPointOptions("请指定第二个点");
            p2key.UseBasePoint = true;
            p2key.BasePoint = p1.Value;
            var p2 = Env.Editor.GetPoint(p2key);
            thjj = Math.Abs(p1.Value.Y - p2.Value.Y) / ThisOptions.height;
            #region
            //while (true)
            //{
            //    // 提示用户选择一个实体
            //    PromptEntityOptions peo = new PromptEntityOptions("\n请选择一个实体: ");
            //    PromptEntityResult per = Env.Editor.GetEntity(peo);

            //    if (per.Status == PromptStatus.Cancel)
            //    {
            //        break;
            //    }
            //    else if (per.Status != PromptStatus.OK)
            //    {
            //        Env.Editor.WriteMessage("\n选择取消或失败，请重新选择.");
            //        continue;

            //    }
            //    using var trans = new DBTrans();
            //    Entity ent = trans.GetObject(per.ObjectId, OpenMode.ForRead) as Entity;

            //    if (ent is MText mtext)
            //    {
            //        thjj = mtext.LineSpacingFactor;
            //        break;
            //    }
            //    else
            //    {
            //        Env.Editor.WriteMessage("\n选择的不是文字，请重新选择.");
            //    }
            //}
            #endregion
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

        //翻译过程数据导出
        private void etresult_Click(object sender, RoutedEventArgs e)
        {
            var savefiledialog = new System.Windows.Forms.SaveFileDialog();
            savefiledialog.Filter = "文本文件(*.txt)|*.txt";
            savefiledialog.Title = "保存输出结果";
            if (savefiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string outputpath = savefiledialog.FileName;
                using (StreamWriter writer = new StreamWriter(outputpath, true))
                {
                    writer.Write(ThisOptions.resulstr);
                }
            }
            System.Windows.MessageBox.Show($"文件已经保存至{savefiledialog.FileName}");


        }

        private void openword_Click(object sender, RoutedEventArgs e)
        {
            string resultPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"transWords.txt");
            if (!File.Exists(resultPath)) 
            { 
                // 文件不存在的时候就创建它
                File.Create(resultPath).Dispose();
            }
            Process.Start(new ProcessStartInfo(resultPath) { UseShellExecute = true });
        }

        private void storageword_Click(object sender, RoutedEventArgs e)
        {
            new translatewords().ShowDialog();
        }

        private void modebut_Click(object sender, RoutedEventArgs e)
        {
            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "翻译设置说明.txt");
            // 检查文件是否存在
            if (!File.Exists(filePath))
            {
                // 如果文件不存在，则创建文件并写入说明文字
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine(getszsm()); // 您想要写入的说明文字
                        
                    }
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"创建文件时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        private void helpbt_Click(object sender, RoutedEventArgs e)
        {
            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "帮助文档.txt");
            // 检查文件是否存在
            if (!File.Exists(filePath))
            {
                // 如果文件不存在，则创建文件并写入说明文字
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine(getztsm()); // 您想要写入的说明文字
                        
                    }
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"创建文件时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        private string getszsm()
        {
            string str = "";
            str += "翻译设置说明\n\n翻译设置选项一：原位替换指的是将翻译后的文字替换到原文字位置上，直接进行替换（原文字消失）。\n" +
                "原位新增指的是将翻译后的文字增加到图纸中（原文字不消失），增加的位置根据新增行距自动向下新增。\n\n" +
                "翻译设置选项二：词句指的是对句子进行翻译，可以结合专业文库和网络翻译对文字进行翻译，也可以选中多个文字对象对其进行批量翻译。\n" +
                "文段翻译指的是对整个段落进行翻译，可选中图纸上按行排版好的单行文字或多行文字，程序自己根据文字坐标进行排序，组合成一段进行翻译，翻译后" +
                "输出的翻译文字为多行文字，用户可指定多行文字的插入点和宽度进行排版。";
            return str;
        }
        private string getztsm()
        {
            string str = "";
            str += "插件帮助说明\n\n" +
                "语言设置：待翻译语言可选择自动识别、中文、英文，目标语言提供了中文和英文互译两种选项\n\n" +
                "翻译设置选项可见设置内说明文档\n\n" +
                "文字排版设置：对于原位新增，设置完字宽、字高，新增翻译文字可按照该设置样式添加，对于原位替换则会修改原文字样式为新增样式，设置适用于词句和文段两种格式。" +
                "新增行距表示使用原位新增时，新增的字体距离原字体的向下的几倍原字高。插件还设置了选择使用系统字体样式的选项，插件会自动识别图纸中已有的文字的字体样式，用户可指定一个字体样式作为翻译语言的字体样式。" +
                "另外用户可以通过拾取的方式来获取这些参数，字宽、字高可通过拾取图纸上已有的文字来设置，新增行距可通过点击图纸上的两个点来自定义翻译文字和源文字之间的距离。\n\n" +
                "翻译结果数据：用户在翻译过程中的数据可通过导出数据按钮导出为txt文档，其中翻译前后语言通过 ，隔开，和专业词库的存储格式相同，方便用户对导出的数据进行筛选修改后存储如专业词库中。" +
                "打开专业词库按钮可以打开存储在本地的专业词库，用户可通过此处来批量添加专业词库，也可通过存储专业词库按钮中去删除、搜索、添加专业词库的词汇。";
            return str;
        }


    }
}
