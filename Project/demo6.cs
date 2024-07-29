//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project
//{
//    public class demo6
//    {
//        [CommandMethod("ZTTH")]
//        public void ZTTH()
//        {
//            // 新建事务
//            using (var tr = new DBTrans())
//            {
//                // 获取当前host，host用来做什么
//                var host = HostApplicationServices.Current;
//                // 获取当前CAD文件中所有文字样式表记录的集合
//                var itst = tr.TextStyleTable.GetRecords();
//                // 遍历每一个文字样式
//                foreach(var tstr in itst)
//                {
//                    // 判断文字样式没有使用windows字体
//                    if (tstr.Font.TypeFace == "")
//                    {
//                        //将文字样式改为可写模式
//                        using (tstr.ForWrite())
//                        {
//                            try
//                            {
//                                //利用host支持文件搜索路径查找本地是否存在该文字样式使用的大字体，如果找不到就会报错进入catch
//                                host.FindFile(tstr.BigFontFileName, Env.Database, FindFileHint.CompiledShapeFile);
//                            }
//                            catch
//                            {
//                                // 如果找不到的话就将它替换成任意一个CAD自带的大字体，这边使用GBHZFS
//                                Env.Editor.WriteMessage("\n文字样式" + tstr.Name + "未找到大字体--<" + tstr.BigFontFileName + "替换为GBHZFS");
//                                tstr.BigFontFileName = "GBHZFS";
//                            }
//                            // shx字体同理
//                            try
//                            {
//                                //利用host支持文件搜索路径查找本地是否存在该文字样式使用的大字体，如果找不到就会报错进入catch
//                                host.FindFile(tstr.FileName, Env.Database, FindFileHint.CompiledShapeFile);
//                            }
//                            catch
//                            {
//                                // 如果找不到的话就将它替换成任意一个CAD自带的大字体，这边使用GBHZFS
//                                Env.Editor.WriteMessage("\n文字样式" + tstr.Name + "未找到大字体--<" + tstr.FileName + "替换为GBHZFS");
//                                tstr.FileName = "SIMPLEX";
//                            }


//                        }
//                    }
//                }
//                // 修改完进行regen刷新一下
//                Env.Editor.Regen();
//            }
//        }
//    }
//}




//using System;
//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.Runtime;

//[assembly: CommandClass(typeof(YourNamespace.YourCommandClass))]

//namespace YourNamespace
//{
//    public class YourCommandClass
//    {
//        [CommandMethod("TQSJ")]
//        public void TQSJ()
//        {
//            // 新建事务
//            using (var tr = new DBTrans())
//            {
//                var pso1 = new PromptSelectionOptions()
//                {
//                    MessageForAdding = "\n请框选要输出文字的范围"
//                };
//                var sf = new SelectionFilter(new TypedValue[]
//                {
//                    new TypedValue(0, "TEXT")
//                });
//                var r1 = Env.Editor.GetSelection(pso1, sf);
//                if (r1.Status == PromptStatus.OK)
//                {
//                    MessageBox.Show("开始提取");
//                    var set1 = r1.Value;
//                    var textobjects = new List<DBText>();
//                    foreach (var id in set1.GetObjectIds())
//                    {
//                        var t1 = (DBText)tr.GetObject(id);
//                        textobjects.Add(t1);
//                    }

//                    // 让用户输入容差值
//                    double defaultTolerance = 5; // 默认容差值
//                    double tolerance;
//                    var pr = Env.Editor.GetString($"\n请输入容差值（默认为 {defaultTolerance}）：");
//                    if (pr.Status == PromptStatus.OK && double.TryParse(pr.StringResult, out tolerance))
//                    {
//                        if (tolerance <= 0)
//                        {
//                            tolerance = defaultTolerance; // 如果用户输入无效值，使用默认值
//                        }

//                        string str = "";

//                        var groupedTextByRow = textobjects.GroupBy(t => Math.Round(t.Position.Y / tolerance)).OrderByDescending(g => g.Key);
//                        foreach (var group in groupedTextByRow)
//                        {
//                            var sortedTextByX = group.OrderBy(t => t.Position.X);
//                            foreach (var t in sortedTextByX)
//                            {
//                                var text = t.TextString;
//                                Env.Editor.WriteMessage(text + " ");
//                                str += text;
//                                str += " ";
//                            }
//                            str += "\n";
//                            Env.Editor.WriteMessage("\n");
//                        }

//                        // 保存到文件
//                        string cadFilePath = HostApplicationServices.WorkingDatabase.Filename;
//                        string outputFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(cadFilePath), "output.txt");
//                        using (StreamWriter writer = new StreamWriter(outputFilePath, true))
//                        {
//                            writer.Write(str);
//                        }
//                        Env.Editor.WriteMessage("Write By ZZZ");
//                        MessageBox.Show("结果已保存到文件：" + outputFilePath);
//                    }
//                }
//            }
//        }
//    }
//}