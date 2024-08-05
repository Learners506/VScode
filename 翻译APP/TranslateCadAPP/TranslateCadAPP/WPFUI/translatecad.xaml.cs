using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
using static IFoxCAD.Cad.OpFilter;

namespace TranslateCadAPP.WPFUI
{
    /// <summary>
    /// translatecad.xaml 的交互逻辑
    /// </summary>
    public partial class translatecad : Window
    {
        private string selectedLanguage;
        private bool singleTranslation;
        private bool multiTranslation;
        private string textWidth;
        private string textHeight;
        public double setheight;
        public string allstr = "start\n";
        public bool clickflag = false;
        

        private bool shouldClose = true;

        public translatecad()
        {
            InitializeComponent();
        }

        public string savestr(string s1)
        {
            allstr += s1;
            return allstr;
        }
        // 多态重写构造函数
        public translatecad(string selectedLanguage,bool singleTranslation, bool multiTranslation,string textWidth,string textHeight,string allstr2)
        {
            InitializeComponent();
            //设置选择过的语言选项或者是赋予空值
            languagecom.SelectedItem = languagecom.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == selectedLanguage);
            RadioButtonSingle.IsChecked = singleTranslation;
            RadioButtonMulty.IsChecked = multiTranslation;
            textwidth.Text = textWidth;
            textheight.Text = textHeight;
            allstr = allstr2;
        }




        // 确定按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            selectedLanguage = ((ComboBoxItem)languagecom.SelectedItem).Content.ToString();
            singleTranslation = RadioButtonSingle.IsChecked == true;
            multiTranslation = RadioButtonMulty.IsChecked == true;
            textWidth = textwidth.Text;
            textHeight = textheight.Text;
            this.Close();

            if (shouldClose)
            {
                shouldClose = false;
                var newwindow1 = new translatecad(selectedLanguage, singleTranslation, multiTranslation, textWidth, textHeight,allstr);
                newwindow1.ShowDialog();
                shouldClose = true;
            }

        }


        // 取消按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        // 窗口关闭事件
        private void Window_Closed(object sender, EventArgs e)
        {
            if (DialogResult == true)
            {
                
                if (clickflag)
                {
                    Env.Editor.WriteMessage("1111111\n");
                    bool circleflag = true;
                    
                    while (circleflag) 
                    {
                        //System.Windows.Forms.MessageBox.Show("when1");

                        using var tr2 = new DBTrans();
                        var xzs2 = new PromptSelectionOptions()
                        {
                            MessageForAdding = "\n请选择要翻译的文字"
                        };
                        var fil2 = new SelectionFilter(new TypedValue[]
                    {
            new TypedValue(0,"TEXT,MTEXT")
                    });

                        var r22 = Env.Editor.GetSelection(xzs2, fil2);
                        
                        if (r22.Status != PromptStatus.OK)
                        { 
                            circleflag = false;
                            return;
                        }
                        if (r22.Status == PromptStatus.OK)
                        {
                            
                            var selectionset2 = r22.Value;
                            Dictionary<ObjectId, Point3d> textObjectPosition2 = new Dictionary<ObjectId, Point3d>();
                            foreach (var textid in selectionset2.GetObjectIds())
                            {
                                
                                var obj = tr2.GetObject(textid);
                                if (obj is DBText dbtext)
                                {
                                    var insertpoint = dbtext.Position;
                                    textObjectPosition2[textid] = insertpoint;
                                    Env.Editor.WriteMessage(dbtext.TextString);
                                }
                                else if (obj is MText mtext)
                                {
                                    var insertpoint = mtext.Location;
                                    textObjectPosition2[textid] = insertpoint;
                                    Env.Editor.WriteMessage(mtext.Contents);
                                }
                            }
                            

                            // 获取文本
                            var temid2 = textObjectPosition2.Keys.First();
                            var temtext2 = tr2.GetObject(temid2);
                           
                            double height2 = 1;
                            Point3d textpoint = new Point3d(0, 0, 0);
                            string str2 = "";
                            if (temtext2 is DBText dbtext1)
                            {
                                height2 = dbtext1.Height;
                                textpoint = dbtext1.Position;
                                str2 = dbtext1.TextString;
                            }
                            else if (temtext2 is MText mtext1)
                            {
                                height2 = mtext1.TextHeight;
                                textpoint = mtext1.Location;
                                str2 = mtext1.Contents;
                            }
                            //System.Windows.Forms.MessageBox.Show(str2);

                            string translatestr2;
                            translatestr2 = TranslateToCn(str2, languagecom.Text);
                            
                            savestr(str2);
                            savestr(",");
                            savestr(translatestr2);
                            savestr("\n");

                            if (!tr2.TextStyleTable.Has("CustomStyle"))
                            {
                                tr2.TextStyleTable.Add("CustomStyle", ttr =>
                                {

                                    ttr.XScale = Convert.ToDouble(textwidth.Text.ToString());
                                });
                            }
                            else
                            {
                                var ts = tr2.TextStyleTable["CustomStyle"];
                                var tsob = (TextStyleTableRecord)tr2.GetObject(ts, OpenMode.ForWrite);
                                tsob.XScale = Convert.ToDouble(textwidth.Text.ToString());
                            }



                            var mt2 = new MText();
                            mt2.TextStyleId = tr2.TextStyleTable["CustomStyle"];
                            mt2.Contents = translatestr2;
                            mt2.TextHeight = height2;

                            if (RadioButtonMulty.IsChecked == true)
                            {
                                var op2 = Env.Editor.GetPoint("\n请选择翻译后文字插入的位置");
                                if (op2.Status == PromptStatus.OK)
                                {
                                    mt2.Location = op2.Value.Ucs2Wcs();
                                    tr2.CurrentSpace.AddEntity(mt2);
                                }
                                
                            }else if (RadioButtonSingle.IsChecked == true)
                                {
                                    mt2.Location = new Point3d(textpoint.X, textpoint.Y - Convert.ToDouble(textheight.Text.ToString()) * height2, textpoint.Z);
                                    tr2.CurrentSpace.AddEntity(mt2);
                                }
                                else
                                {
                                    return;
                                }


                        }



                     


                    }
                    
                }
                else
                {
                    Env.Editor.WriteMessage("222222222\n");
                    using var tr = new DBTrans();
                    var xzs = new PromptSelectionOptions()
                    {
                        MessageForAdding = "\n请选择要翻译的文字"
                    };
                    // 设置筛选出单行文字和多行文字的筛选集
                    var fil = new SelectionFilter(new TypedValue[]
                    {
            new TypedValue(0,"TEXT,MTEXT")
                    });
                    var r1 = Env.Editor.GetSelection(xzs, fil);
                    if (r1.Status == PromptStatus.OK)
                    {
                        // 包含单行文字和多行文字的选择集
                        var selectionset = r1.Value;

                        // 创建字典，用于存储文字对象和位置点
                        Dictionary<ObjectId, Point3d> textObjectPosition = new Dictionary<ObjectId, Point3d>();

                        foreach (var textid in selectionset.GetObjectIds())
                        {
                            var obj = tr.GetObject(textid);
                            if (obj is DBText dbtext)
                            {
                                var insertpoint = dbtext.Position;
                                textObjectPosition[textid] = insertpoint;
                            }
                            else if (obj is MText mtext)
                            {
                                var insertpoint = mtext.Location;
                                textObjectPosition[textid] = insertpoint;
                            }
                        }

                        var temid = textObjectPosition.Keys.First();
                        var temtext = tr.GetObject(temid);

                        // 获取文字高度
                        double height = 1;
                        if (temtext is DBText dbtext1)
                        {
                            height = dbtext1.Height;
                        }
                        else if (temtext is MText mtext1)
                        {
                            height = mtext1.TextHeight;
                        }
                        setheight = height;
                        double defaulttolerance = height;
                        double tolerance;
                        var p2 = Env.Editor.GetString($"请输入容差值，默认为：{defaulttolerance}");
                        if (p2.Status == PromptStatus.OK)
                        {
                            if (!double.TryParse(p2.StringResult, out tolerance))
                            {
                                Env.Editor.WriteMessage("容差已设置为默认值");
                                tolerance = defaulttolerance;
                            }
                            //System.Windows.Forms.MessageBox.Show(tolerance.ToString());
                        }
                        else
                        {
                            return;
                        }
                        // 根据容差进行分组
                        string str = "";
                        List<DBObject> allobject = new List<DBObject>();
                        var grouptextbyrow = textObjectPosition.GroupBy(t => Math.Round(t.Value.Y / tolerance)).OrderByDescending(g => g.Key);

                        foreach (var group in grouptextbyrow)
                        {
                            var sorttextbyx = group.OrderBy(t => t.Value.X);
                            foreach (var t in sorttextbyx)
                            {
                                var ob = tr.GetObject(t.Key);
                                allobject.Add(ob);
                                if (ob is DBText dbtext2)
                                {
                                    str += dbtext2.TextString;
                                    str += " ";
                                }
                                else if (ob is MText mtext2)
                                {
                                    str += mtext2.Contents;
                                    str += " ";
                                }
                            }

                        }

                        Point3d danpoint = new Point3d(0, 0, 0);
                        var temptob = allobject[0];
                        if (temptob is DBText dbtext3)
                        {
                            danpoint = dbtext3.Position;
                        }
                        else if (temptob is MText mtext3)
                        {
                            danpoint = mtext3.Location;
                        }


                        string translatestr;
                        if (RadioButtonMulty.IsChecked == true)
                        {

                            var translatestr2 = TranslateToCn(str, languagecom.Text);
                            var parts = translatestr2.Split(new[] { "1.", ".2.", "2.", ".3.", "3.", ".4.", "4.", ".5.", "5." }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < parts.Length; i++)
                            {
                                parts[i] = $"{i + 1}.{parts[i]}";
                            }

                            translatestr = string.Join("\n", parts);
                            savestr(str);
                            savestr(",");
                            savestr(translatestr);
                            savestr("\n");

                        }
                        else if (RadioButtonSingle.IsChecked == true)
                        {
                            translatestr = TranslateToCn(str, languagecom.Text);
                            savestr(str);
                            savestr(",");
                            savestr(translatestr);
                            savestr("\n");

                        }
                        else return;


                        if (!tr.TextStyleTable.Has("CustomStyle"))
                        {
                            tr.TextStyleTable.Add("CustomStyle", ttr =>
                            {

                                ttr.XScale = Convert.ToDouble(textwidth.Text.ToString());
                            });
                        }
                        else
                        {
                            var ts = tr.TextStyleTable["CustomStyle"];
                            var tsob = (TextStyleTableRecord)tr.GetObject(ts, OpenMode.ForWrite);
                            tsob.XScale = Convert.ToDouble(textwidth.Text.ToString());
                        }





                        var mt = new MText();
                        mt.TextStyleId = tr.TextStyleTable["CustomStyle"];
                        mt.Contents = translatestr;
                        mt.TextHeight = height;
                        //System.Windows.Forms.MessageBox.Show("单行：" + singleTranslation.ToString() + "多行：" + multiTranslation.ToString());
                        if (RadioButtonMulty.IsChecked == true)
                        {
                            var op = Env.Editor.GetPoint("\n请选择翻译后文字插入的位置");
                            if (op.Status == PromptStatus.OK)
                            {
                                mt.Location = op.Value.Ucs2Wcs();
                                Extents3d box = mt.GeometricExtents;
                                using var j2 = new JigEx((mp, de) =>
                                {
                                    mt.Width = Math.Abs(mp.Z20().X - op.Value.X);
                                    box = mt.GeometricExtents;
                                });
                                j2.DatabaseEntityDraw(worlddraw => worlddraw.Geometry.Draw(mt, ToPolyline(box)));
                                j2.SetOptions("\n请选择文本框右端位置");
                                //var r2 = j2.Drag();
                                //if (r2.Status != PromptStatus.OK)
                                //{
                                //    return;
                                //}
                                //tr.CurrentSpace.AddEntity(mt);



                                var flag = true;
                                while (flag)
                                {
                                    var r2 = j2.Drag();
                                    if (r2.Status == PromptStatus.OK)
                                    {
                                        flag = false;
                                        tr.CurrentSpace.AddEntity(mt);
                                    }

                                }

                            }

                        }
                        else if (RadioButtonSingle.IsChecked == true)
                        {
                            var op = new Point3d(danpoint.X, danpoint.Y - Convert.ToDouble(textheight.Text.ToString()) * height, danpoint.Z);
                            mt.Location = op;
                            tr.CurrentSpace.AddEntity(mt);
                            
                            
                            //Extents3d box = mt.GeometricExtents;
                            //using var j2 = new JigEx((mp, de) =>
                            //{
                            //    mt.Width = Math.Abs(mp.Z20().X - op.X);
                            //    box = mt.GeometricExtents;
                            //});
                            //j2.DatabaseEntityDraw(worlddraw => worlddraw.Geometry.Draw(mt, ToPolyline(box)));
                            //j2.SetOptions("\n请选择文本框右端位置");


                            //var r2 = j2.Drag();
                            //if (r2.Status != PromptStatus.OK)
                            //{
                            //    return;
                            //}
                            //tr.CurrentSpace.AddEntity(mt);



                            //var flag = true;
                            //while (flag)
                            //{
                            //    var r2 = j2.Drag();
                            //    if (r2.Status == PromptStatus.OK)
                            //    {
                            //        flag = false;
                            //        tr.CurrentSpace.AddEntity(mt);
                            //    }

                            //}
                        }
                        else return;

                    }

                    if (shouldClose)
                    {
                        return;

                    }
                } 
            }
        }

        // 获取两个点取的点之间的y差值
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            selectedLanguage = languagecom.SelectedItem != null ? ((ComboBoxItem)languagecom.SelectedItem).Content.ToString() : null;
            singleTranslation = RadioButtonSingle.IsChecked == true;
            multiTranslation = RadioButtonMulty.IsChecked == true;
            textWidth = textwidth.Text;
            textHeight = textheight.Text;
            this.Close();
            StartPointCapture();
            
            
        }
        public void StartPointCapture()
        {
            var p1 = Env.Editor.GetPoint("请指定第一个点：");
            var p2 = Env.Editor.GetPoint("请指定第二个点：");
            var distance = Math.Abs(p1.Value.Y - p2.Value.Y)/setheight;
            var newwindow = new translatecad(selectedLanguage, singleTranslation, multiTranslation, textWidth, distance.ToString(),allstr);
            newwindow.ShowDialog();
        }

        // 附加函数
        // 写一个创建包围盒的函数
        public Polyline ToPolyline(Extents3d extents)
        {
            var polyline = new Polyline();
            polyline.AddVertexAt(0, new Point2d(extents.MinPoint.X, extents.MinPoint.Y), 0, 0, 0);
            polyline.AddVertexAt(1, new Point2d(extents.MaxPoint.X, extents.MinPoint.Y), 0, 0, 0);
            polyline.AddVertexAt(2, new Point2d(extents.MaxPoint.X, extents.MaxPoint.Y), 0, 0, 0);
            polyline.AddVertexAt(3, new Point2d(extents.MinPoint.X, extents.MaxPoint.Y), 0, 0, 0);
            polyline.Closed = true; // 闭合多段线
            return polyline;
        }

        // 写一个添加百度翻译api的翻译函数
        public static string TranslateToCn(string SourceString,string slg)
        {
            // 编码的字符串
            var EncodedString = WebUtility.UrlEncode(SourceString);

            // 设置参数，ID和key可以通过文件上传形式使用自己的api进行翻译
            var id = "20240717002102259";
            var key = "GUBqy_0rlZDFLXVaWPyU";
            // 生成随机数
            var salt = new Random().Next(2, 2333);

            string languageto;
            //System.Windows.Forms.MessageBox.Show(slg);
            if(slg == "日语")
            {
                languageto = "jp";
            }
            else if(slg == "英语")
            {
                languageto = "en";
            }
            else
            {
                languageto = "zh";
            }


            // md5编码并转换16进制-获取sign
            var sign = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(id + SourceString + salt + key))).ToLower().Replace("-", "");
            var FullAddress = $"http://api.fanyi.baidu.com/api/trans/vip/translate?q={EncodedString}&from=auto&to={languageto}&appid={id}&salt={salt}&sign={sign}";
            var WebUser = new WebClient();
            var Result = WebUser.DownloadString(FullAddress);

            // 对结果进行解析
            var jobject = JObject.Parse(Result);
            return jobject["trans_result"][0]["dst"].Value<string>();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RadioButtonSingle.IsChecked = true;
            RadioButtonMulty.IsChecked = false;
            textheight.Text = "1";
            textwidth.Text = "1";
            languagecom.SelectedIndex = 0;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(allstr);
            var savefiledialog = new System.Windows.Forms.SaveFileDialog();
            savefiledialog.Filter = "文本文件(*.txt)|*.txt";
            savefiledialog.Title = "保存输出结果";
            if (savefiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string outputpath = savefiledialog.FileName;
                using (StreamWriter writer = new StreamWriter(outputpath, true))
                {
                    writer.Write(allstr);
                }
                Env.Editor.WriteMessage("文件保存成功\nwrite by zzz");
               
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            clickflag = true;
            DialogResult = true;
            selectedLanguage = ((ComboBoxItem)languagecom.SelectedItem).Content.ToString();
            singleTranslation = RadioButtonSingle.IsChecked == true;
            multiTranslation = RadioButtonMulty.IsChecked == true;
            textWidth = textwidth.Text;
            textHeight = textheight.Text;
            this.Close();
            if (shouldClose)
            {
                shouldClose = false;
                var newwindow1 = new translatecad(selectedLanguage, singleTranslation, multiTranslation, textWidth, textHeight, allstr);
                newwindow1.ShowDialog();
                shouldClose = true;
            }
        }
    }
}
