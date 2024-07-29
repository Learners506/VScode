//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

//namespace Project
//{
//    public class demo7
//    {
//        //[CommandMethod("TQSJ")]
//        //public void TQSJ()
//        //{
//        //    //新建事务
//        //    using var tr = new DBTrans();
//        //    var pso1 = new PromptSelectionOptions()
//        //    {
//        //        MessageForAdding = "\n请框选要输出文字的范围"
//        //    };
//        //    var sf = new SelectionFilter(new TypedValue[]
//        //    {
//        //        new TypedValue(0,"TEXT")
//        //    });
//        //    var r1 = Env.Editor.GetSelection(pso1, sf);
//        //    if (r1.Status == PromptStatus.OK)
//        //    {
//        //        MessageBox.Show("开始提取");
//        //        var set1 = r1.Value;
//        //        var textobjects = new List<DBText>();
//        //        foreach (var id in set1.GetObjectIds())
//        //        {
//        //            var t1 = (DBText)tr.GetObject(id);
//        //            textobjects.Add(t1);
//        //            //var text = t1.TextString;
//        //            //MessageBox.Show(text+"\n"+t1.Position.X.ToString()+""+t1.Position.Y.ToString());
//        //        }
//        //        // 设置一般的字高为容许差距
                
//        //        double defaultTolerance = 5; // 默认容差值
//        //        double tolerance;
//        //        var pr = Env.Editor.GetString($"\n请输入容差值（默认为 {defaultTolerance}）：");
//        //        if (pr.Status == PromptStatus.OK && double.TryParse(pr.StringResult, out tolerance))
//        //        {
//        //            if (tolerance <= 0)
//        //            {
//        //                tolerance = defaultTolerance; // 如果用户输入无效值，使用默认值
//        //            }
//        //            string str = "";

//        //            //Env.Editor.WriteMessage(textobjects.First().TextString + textobjects.First().Height.ToString());
//        //            var groupedTextByRow = textobjects.GroupBy(t => Math.Round(t.Position.Y / tolerance)).OrderByDescending(g => g.Key);
//        //            foreach (var group in groupedTextByRow)
//        //            {
//        //                var sortedTextByX = group.OrderBy(t => t.Position.X);
//        //                foreach (var t in sortedTextByX)
//        //                {
//        //                    var text = t.TextString;
//        //                    //Env.Editor.WriteMessage(t.Position.Y.ToString());
//        //                    Env.Editor.WriteMessage(text + " ");
//        //                    str += text;
//        //                    str += " ";
//        //                }
//        //                str += "\n";
//        //                Env.Editor.WriteMessage("\n");
//        //            }

//        //            // 写入文件
//        //            //string cadFilePath = HostApplicationServices.WorkingDatabase.Filename; // 获取当前 CAD 文件路径
//        //            //string outputFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(cadFilePath), "output.txt");
//        //            //using (StreamWriter writer = new StreamWriter(outputFilePath, true))
//        //            //{
//        //            //    writer.Write(str);
//        //            //}
//        //            //Env.Editor.WriteMessage("Write By ZZZ");
//        //            //MessageBox.Show("结果已保存到文件：" + outputFilePath);

//        //            //var folderBrowserDialog = new FolderBrowserDialog();
//        //            //folderBrowserDialog.Description = "选择保存文件的文件夹";
//        //            //if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
//        //            //{
//        //            //    string outputFilePath = Path.Combine(folderBrowserDialog.SelectedPath, "output.txt");
//        //            //    using (StreamWriter writer = new StreamWriter(outputFilePath, true))
//        //            //    {
//        //            //        writer.Write(str);
//        //            //    }
//        //            //    Env.Editor.WriteMessage("Write By ZZZ");
//        //            //    MessageBox.Show("结果已保存到文件：" + outputFilePath);
//        //            //}

//        //            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
//        //            saveFileDialog.Filter = "文本文件 (*.txt)|*.txt";
//        //            saveFileDialog.Title = "保存输出结果";
//        //            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//        //            {
//        //                string outputFilePath = saveFileDialog.FileName;
//        //                using (StreamWriter writer = new StreamWriter(outputFilePath, true))
//        //                {
//        //                    writer.Write(str);
//        //                }
//        //                Env.Editor.WriteMessage("Write By ZZZ");
//        //                MessageBox.Show("结果已保存到文件：" + outputFilePath);
//        //            }

//        //        }
//        //    }
//        //}


//        [CommandMethod("DQSJ")]
//        public void DQSJ()
//        {
//            //新建事务
//            using var tr = new DBTrans();
//            var pso1 = new PromptSelectionOptions()
//            {
//                MessageForAdding = "\n请框选要对其文字的范围"
//            };
//            var sf = new SelectionFilter(new TypedValue[]
//            {
//                new TypedValue(0,"TEXT")
//            });
//            var r1 = Env.Editor.GetSelection(pso1, sf);
//            if(r1.Status == PromptStatus.OK)
//            {
//                var d = r1.Value.GetEntities<DBText>;
                
//            }

//        }
//    }
//}
