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

namespace ZZZIFOX.ProjectItems
{
    public class RestartLoopException : Exception { }
    public class Translater
    {

        private static Translateset ThisOptions => Translateset.Default;
        [CommandMethod(nameof(TRANS))]
        public void TRANS()
        {
            ThisOptions.Reload();
            using var tr = new DBTrans();
            while (true)
            {
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
                    Env.Editor.WriteMessage(ThisOptions.ztstyle.ToString());

                    var pr = Env.Editor.GetSelection(pko, fil);
                    if (pr.Status == PromptStatus.OK)
                    {

                        if (ThisOptions.modetwoone)
                        {
                            // 点词翻译
                            var selectionset = pr.Value;



                        }
                        else
                        {
                            // 句子翻译
                        }



                        // 将选中到的文字进行翻译
                        Env.Editor.WriteMessage("sb" + pr.Status.ToString());






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
