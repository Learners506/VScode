using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TranslateCadAPP.Code
{
    public class CadTranslate
    {
        [CommandMethod(nameof(ETC))]
        public void ETC()
        {
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
                    MessageBox.Show(tolerance.ToString());
                }
                else
                {
                    return;
                }

                // 根据容差进行分组
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

                var parts = translatestr2.Split(new[] { "1.", ".2.", "2.", ".3.", "3.", ".4.", "4.", ".5.", "5." }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = $"{i + 1}.{parts[i]}";
                }
                var translatestr = string.Join("\n", parts);






                var mt = new MText();
                mt.Contents = translatestr;
                mt.TextHeight = height;
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

            // md5编码并转换16进制-获取sign
            var sign = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(id + SourceString + salt + key))).ToLower().Replace("-", "");
            var FullAddress = $"http://api.fanyi.baidu.com/api/trans/vip/translate?q={EncodedString}&from=en&to=zh&appid={id}&salt={salt}&sign={sign}";
            var WebUser = new WebClient();
            var Result = WebUser.DownloadString(FullAddress);

            // 对结果进行解析
            var jobject = JObject.Parse(Result);
            return jobject["trans_result"][0]["dst"].Value<string>();
        }

    }
}
