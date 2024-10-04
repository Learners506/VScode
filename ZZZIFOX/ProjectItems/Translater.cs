using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZZZIFOX.Settings;
using ZZZIFOX.WPFUI;
using Newtonsoft.Json.Linq;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace ZZZIFOX.ProjectItems
{
    public class RestartLoopException : Exception { }
    public class Translater
    {
        public static string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "transWords.txt");
        private static Translateset ThisOptions => Translateset.Default;
        private static Dictionary<string, string> dictionary; // 用于存储词库数据
        
        // 开始翻译前先加载词库数据
        public static void LoadDictionary()
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); ;

            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        dictionary[parts[0].Trim()] = parts[1].Trim(); // 中文作为Key，英文作为Value
                    }
                }
            }
            else
            {
                // 如果文件不存在，则创建一个空文件
                File.Create(filePath).Close();
            }
        }



        [CommandMethod(nameof(TRANS))]
        public void TRANS()
        {
            ThisOptions.resulstr = ""; //用于存储翻译结果
            ThisOptions.Reload();
            

            // 获取当前加载路径
            //var module = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            //string currentPath = System.IO.Path.GetDirectoryName(module.FullyQualifiedName);
            // 获取桌面路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            while (true)
            {
                using var tr = new DBTrans();
                #region
                try
                {
                    var pko = new PromptSelectionOptions();
                    pko.MessageForAdding = "\n请选择翻译文字或设置[S]";
                    pko.Keywords.Add("S");
                    pko.Keywords.Default = "S";
                    pko.KeywordInput += (sender, e) =>
                    {
                        switch (e.Input)
                        {
                            case "S":
                                new TranslateUI().ShowDialog();
                                throw new RestartLoopException();
                                break;
                        }
                    };
                    var fil = new SelectionFilter(new TypedValue[]
                    {
                    new TypedValue(0,"TEXT,MTEXT")
                    });
                    var pr = Env.Editor.GetSelection(pko, fil);
                    if (pr.Status == PromptStatus.OK)
                    {


                        if (ThisOptions.modetwoone)
                        {
                            // 点词翻译
                            var selectionset = pr.Value;
                            if (ThisOptions.modeonetwo)
                            {
                                // 原位新增
                                foreach (var id in selectionset.GetObjectIds())
                                {
                                    var ent = tr.GetObject(id, OpenMode.ForRead) as Entity;

                                    if (ent is MText mtext)
                                    {
                                        //多行文字
                                        var insertpoint = new Point3d(mtext.Location.X, mtext.Location.Y - (ThisOptions.distance + 1) * mtext.TextHeight, 0);
                                        var dbt = new MText();
                                        //dbt.Contents = TranslateToCn(mtext.Contents);
                                        dbt.Contents = TranslateFromData(mtext.Contents);
                                        dbt.Location = insertpoint;
                                        dbt.Height = ThisOptions.height;

                                        ThisOptions.resulstr += mtext.Contents;
                                        ThisOptions.resulstr += ",";
                                        //ThisOptions.resulstr += TranslateToCn(mtext.Contents);
                                        ThisOptions.resulstr += dbt.Contents;
                                        ThisOptions.resulstr += "\n";

                                        //如果使用字体样式
                                        if (ThisOptions.ztstyle)
                                        {
                                            if (tr.TextStyleTable.Has(ThisOptions.style))
                                            {
                                                var str = tr.TextStyleTable[ThisOptions.style].GetObject(OpenMode.ForWrite) as TextStyleTableRecord;
                                                if (str == null) return;
                                                str.XScale = ThisOptions.width;
                                                str.DowngradeOpen();
                                                dbt.TextStyleId = tr.TextStyleTable[ThisOptions.style];
                                            }
                                        }
                                        else
                                        {
                                            var str = mtext.TextStyleId.GetObject(OpenMode.ForWrite) as TextStyleTableRecord;
                                            if (str == null) return;
                                            str.XScale = ThisOptions.width;
                                            str.DowngradeOpen();
                                            dbt.TextStyleId = mtext.TextStyleId;

                                        }
                                        tr.CurrentSpace.AddEntity(dbt);
                                        tr.Commit();
                                        tr.Dispose();

                                    }
                                    else if (ent is DBText dbtext)
                                    {
                                        var insertpoint = new Point3d(dbtext.Position.X, dbtext.Position.Y - (ThisOptions.distance + 1) * dbtext.Height, 0);
                                        var mtet = new DBText();
                                        //mtet.TextString = TranslateToCn(dbtext.TextString);
                                        mtet.TextString = TranslateFromData(dbtext.TextString);
                                        ThisOptions.resulstr += dbtext.TextString;
                                        ThisOptions.resulstr += ",";
                                        //ThisOptions.resulstr += TranslateToCn(dbtext.TextString);
                                        ThisOptions.resulstr += mtet.TextString;
                                        ThisOptions.resulstr += "\n";

                                        mtet.Position = insertpoint;
                                        mtet.Height = ThisOptions.height;
                                        mtet.WidthFactor = ThisOptions.width;
                                        //如果使用字体样式
                                        if (ThisOptions.ztstyle)
                                        {
                                            if (tr.TextStyleTable.Has(ThisOptions.style))
                                            {
                                                mtet.TextStyleId = tr.TextStyleTable[ThisOptions.style];
                                            }
                                        }
                                        tr.CurrentSpace.AddEntity(mtet);
                                        tr.Commit();
                                        tr.Dispose();
                                    }
                                }

                            }
                            else
                            {
                                // 原位替换
                                foreach (var id in selectionset.GetObjectIds())
                                {
                                    var ent = tr.GetObject(id, OpenMode.ForWrite) as Entity;
                                    if (ent is MText mtext)
                                    {
                                        //mtext.Contents = TranslateToCn(mtext.Contents);
                                        mtext.Contents = TranslateFromData(mtext.Contents);
                                    }
                                    else if (ent is DBText dbtext)
                                    {
                                        //dbtext.TextString = TranslateToCn(dbtext.TextString);
                                        dbtext.TextString = TranslateFromData(dbtext.TextString);
                                    }
                                }
                            }
                        }
                        else
                        {

                            // 句子翻译
                            var selectionset = pr.Value;
                            // 创建字典，用于存储文字对象和位置点
                            Dictionary<ObjectId, Point3d> textObjectPosition = new Dictionary<ObjectId, Point3d>();
                            // 存储文字对象ID和位置点
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

                            // 随机获取文字列表中第一个文字的高度作为容差
                            var temid = textObjectPosition.Keys.First();
                            var temtext = tr.GetObject(temid);
                            double height = 1;
                            if (temtext is DBText dbtext1)
                            {
                                height = dbtext1.Height;
                            }
                            else if (temtext is MText mtext1)
                            {
                                height = mtext1.TextHeight;
                            }



                            // 根据容差进行分组
                            double tolerance = height;
                            string str = "";
                            var grouptextbyrow = textObjectPosition.GroupBy(t => Math.Round(t.Value.Y / tolerance)).OrderByDescending(g => g.Key);
                            foreach (var group in grouptextbyrow)
                            {
                                var sorttextbyx = group.OrderBy(t => t.Value.X);
                                foreach (var t in sorttextbyx)
                                {
                                    var ob = tr.GetObject(t.Key);
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

                            var translatestr2 = TranslateToCn(str);
                            var mt = new MText();
                            var parts = translatestr2.Split(new[] { "1.", ".2.", "2.", ".3.", "3.", ".4.", "4.", ".5.", "5." }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < parts.Length; i++)
                            {
                                parts[i] = $"{i + 1}.{parts[i]}";
                            }
                            var translatestr = string.Join("\n", parts);

                            mt.Contents = translatestr;
                            mt.Height = ThisOptions.height;

                            //如果使用字体样式
                            if (ThisOptions.ztstyle)
                            {
                                if (tr.TextStyleTable.Has(ThisOptions.style))
                                {
                                    var str2 = tr.TextStyleTable[ThisOptions.style].GetObject(OpenMode.ForWrite) as TextStyleTableRecord;
                                    if (str2 == null) return;
                                    str2.XScale = ThisOptions.width;
                                    str2.DowngradeOpen();
                                    mt.TextStyleId = tr.TextStyleTable[ThisOptions.style];
                                }
                            }




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
                    }
                    else if (pr.Status == PromptStatus.Cancel)
                    {
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (RestartLoopException)
                {
                    continue;
                }
                #endregion
            }
        }

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
        public static string TranslateToCn(string SourceString)
        {
            // 编码的字符串
            var EncodedString = WebUtility.UrlEncode(SourceString);

            // 设置参数，ID和key可以通过文件上传形式使用自己的api进行翻译
            var id = "20240717002102259";
            var key = "GUBqy_0rlZDFLXVaWPyU";
            // 生成随机数
            var salt = new Random().Next(2, 2333);

            var sourcelanguage = "en";
            var targetlanguage = "zh";

            if (ThisOptions.FYString=="自动识别")
            {
                sourcelanguage = "auto";
            }else if (ThisOptions.FYString == "中文")
            {
                sourcelanguage = "zh";
            }else if(ThisOptions.FYString == "英文")
            {
                sourcelanguage = "en";
            }

            if (ThisOptions.MBString == "中文")
            {
                targetlanguage = "zh";
            }else if( ThisOptions.MBString == "英文")
            {
                targetlanguage = "en";
            }




            // md5编码并转换16进制-获取sign
            var sign = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(id + SourceString + salt + key))).ToLower().Replace("-", "");
            var FullAddress = $"http://api.fanyi.baidu.com/api/trans/vip/translate?q={EncodedString}&from={sourcelanguage}&to={targetlanguage}&appid={id}&salt={salt}&sign={sign}";
            var WebUser = new WebClient();
            var Result = WebUser.DownloadString(FullAddress);

            // 对结果进行解析
            var jobject = JObject.Parse(Result);
            return jobject["trans_result"][0]["dst"].Value<string>();
        }

        // 写一个添加查找数据库的翻译函数
        public static string TranslateFromData(string SourceString)
        {
            // 加载词汇表
            LoadDictionary();
            var reverseDictionary = dictionary.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
            string translation = null;
            if (dictionary.TryGetValue(SourceString, out translation))
            {
                Env.Editor.WriteMessage("通过词汇库进行翻译");
                return translation; // 找到中文对应的英文翻译
            }
            else if (reverseDictionary.TryGetValue(SourceString, out translation))
            {
                Env.Editor.WriteMessage("通过词汇库进行翻译");
                return translation; // 找到英文对应的中文翻译
            }
            else
            {
                Env.Editor.WriteMessage("通过网络进行翻译");
                translation = TranslateToCn(SourceString);
            }
            return translation;
        }


    }
}
