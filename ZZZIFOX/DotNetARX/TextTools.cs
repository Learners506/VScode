using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Autodesk.AutoCAD.DatabaseServices.TextEditorStack;

namespace ZZZIFOX.DotNetARX
{
    /// <summary>
    /// 多行文字堆叠方式
    /// </summary>
    public enum StackType
    {
        /// <summary>
        /// 水平分数(/)
        /// </summary>
        HorizontalFraction,
        /// <summary>
        /// 斜分数(#)
        /// </summary>
        ItalicFraction,
        /// <summary>
        /// 公差(^)
        /// </summary>
        Tolerance
    }



    public static class TextTools
    {
        /// <summary>
        /// 1获取多行文字的真实内容
        /// </summary>
        /// <param name="mtext">多行文字对象</param>
        /// <returns>返回多行文字的真实内容</returns>
        public static string GetText(this MText mtext)
        {
            string content = mtext.Contents;//多行文本内容
            //将多行文本按“\\”进行分割
            string[] strs = content.Split(new string[] { @"\\" }, StringSplitOptions.None);
            //指定不区分大小写
            RegexOptions ignoreCase = RegexOptions.IgnoreCase;
            for (int i = 0; i < strs.Length; i++)
            {
                //删除段落缩进格式
                strs[i] = Regex.Replace(strs[i], @"\\pi(.[^;]*);", "", ignoreCase);
                //删除制表符格式
                strs[i] = Regex.Replace(strs[i], @"\\pt(.[^;]*);", "", ignoreCase);
                //删除堆迭格式
                strs[i] = Regex.Replace(strs[i], @"\\S(.[^;]*)(\^|#|\\)(.[^;]*);", @"$1$3", ignoreCase);
                strs[i] = Regex.Replace(strs[i], @"\\S(.[^;]*)(\^|#|\\);", "$1", ignoreCase);
                //删除字体、颜色、字高、字距、倾斜、字宽、对齐格式
                strs[i] = Regex.Replace(strs[i], @"(\\F|\\C|\\H|\\T|\\Q|\\W|\\A)(.[^;]*);", "", ignoreCase);
                //删除下划线、删除线格式
                strs[i] = Regex.Replace(strs[i], @"(\\L|\\O|\\l|\\o)", "", ignoreCase);
                //删除不间断空格格式
                strs[i] = Regex.Replace(strs[i], @"\\~", "", ignoreCase);
                //删除换行符格式
                strs[i] = Regex.Replace(strs[i], @"\\P", "\n", ignoreCase);
                //删除换行符格式(针对Shift+Enter格式)
                //strs[i] = Regex.Replace(strs[i], "\n", "", ignoreCase);
                //删除{}
                strs[i] = Regex.Replace(strs[i], @"({|})", "", ignoreCase);
                //替换回\\,\{,\}字符
                //strs[i] = Regex.Replace(strs[i], @"\x01", @"\", ignoreCase);
                //strs[i] = Regex.Replace(strs[i], @"\x02", @"{", ignoreCase);
                //strs[i] = Regex.Replace(strs[i], @"\x03", @"}", ignoreCase);
            }
            return string.Join("\\", strs);//将文本中的特殊字符去掉后重新连接成一个字符串
        }

        /// <summary>
        /// 2堆叠多行文字
        /// </summary>
        /// <param name="text">堆叠分数前面的文字</param>
        /// <param name="supText">要堆叠分数的分子</param>
        /// <param name="subText">要堆叠分数的分母</param>
        /// <param name="stackType">堆叠类型</param>
        /// <param name="scale">堆叠文字的缩放比例</param>
        /// <returns>返回堆叠好的文字</returns>
        public static string StackText(string text, string supText, string subText, StackType stackType, double scale)
        {
            //设置堆叠方式所代表的字符，用于将StackType枚举转换为对应的字符
            string[] strs = new string[] { "/", "#", "^" };
            //设置堆叠文字
            return string.Format(
                    "\\A1;{0}{1}\\H{2:0.#}x;\\S{3}{4}{5};{6}",
                    text, "{", scale, supText, strs[(int)stackType],
                    subText, "}");
        }

        /// <summary>
        /// 3设置单行文本的属性为当前文字样式的属性
        /// </summary>
        /// <param name="txt">单行文本对象</param>
        public static void SetFromTextStyle(this DBText txt)
        {
            //打开当前文字样式表记录
            TextStyleTableRecord str = (TextStyleTableRecord)txt.TextStyleId.GetObject(OpenMode.ForRead);
            //必须保证文字为写的状态
            if (!txt.IsWriteEnabled)
                txt.UpgradeOpen();
            txt.Oblique = str.ObliquingAngle;//设置倾斜角(弧度)
            txt.Annotative = str.Annotative;//设置文字的注释性
            //文字方向与布局是否匹配
            txt.SetPaperOrientation(Convert.ToBoolean(str.PaperOrientation));
            txt.WidthFactor = str.XScale;//设置宽度比例
            txt.Height = str.TextSize;//设置高度
            if (str.FlagBits == 2)
            {
                txt.IsMirroredInX = true;//颠倒
            }
            else if (str.FlagBits == 4)
            {
                txt.IsMirroredInY = true;//反向
            }
            else if (str.FlagBits == 6)//颠倒并反向
            {
                txt.IsMirroredInX = txt.IsMirroredInY = true;
            }
            txt.DowngradeOpen();//为了安全切换为读的状态
        }

        /// <summary>
        /// 5设置多行文本的属性为当前文字样式的属性
        /// </summary>
        /// <param name="mtxt">多行文本对象</param>
        public static void SetFromTextStyle(this MText mtxt)
        {
            Database db = mtxt.Database;
            var trans = db.TransactionManager;
            TextStyleTableRecord str = (TextStyleTableRecord)trans.GetObject(mtxt.TextStyleId, OpenMode.ForRead);
            if (!mtxt.IsWriteEnabled)
                mtxt.UpgradeOpen();
            mtxt.Rotation = str.ObliquingAngle;
            mtxt.Annotative = str.Annotative;
            mtxt.SetPaperOrientation(Convert.ToBoolean(str.PaperOrientation));
            mtxt.LineSpacingFactor = str.XScale;
            mtxt.TextHeight = str.TextSize;
        }








    }
}
