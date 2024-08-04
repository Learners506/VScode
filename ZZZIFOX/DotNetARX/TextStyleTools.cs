using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.DotNetARX
{
    /// <summary>
    /// 字体字符集名称
    /// </summary>
    public enum FontCharSet
    {
        /// <summary>
        /// 英文
        /// </summary>
        Ansi = 0,
        /// <summary>
        /// 与当前操作系统语言有关，如为简体中文，则被设置为GB2312
        /// </summary>
        Default = 1,
        /// <summary>
        /// 简体中文
        /// </summary>
        GB2312 = 134,
        /// <summary>
        /// 繁体中文
        /// </summary>
        Big5 = 136,
        /// <summary>
        /// 与操作系统有关
        /// </summary>
        OEM = 255
    }

    /// <summary>
    /// 字体的字宽
    /// </summary>
    public enum FontPitch
    {
        /// <summary>
        /// 默认字宽
        /// </summary>
        Default = 0,
        /// <summary>
        /// 固定字宽
        /// </summary>
        Fixed = 1,
        /// <summary>
        /// 可变字宽
        /// </summary>
        Variable = 2
    }

    /// <summary>
    /// 字体的语系定义
    /// </summary>
    public enum FontFamily
    {
        /// <summary>
        /// 使用默认字体
        /// </summary>
        Dontcare = 0,
        /// <summary>
        /// 可变的笔划宽度，有衬线，如MS Serif字体
        /// </summary>
        Roman = 16,
        /// <summary>
        /// 可变的笔划宽度，无衬线，如MS Sans Serif字体 
        /// </summary>
        Swiss = 32,
        /// <summary>
        /// 固定笔划宽度，衬线可以有也可以没有,如Courier New字体
        /// </summary>
        Modern = 48,
        /// <summary>
        /// 手写体，如Cursive字体
        /// </summary>
        Script = 64,
        /// <summary>
        /// 小说字体，如旧式英语
        /// </summary>
        Decorative = 80
    }
    public static class TextStyleTools
    {
        /// <summary>
        /// 创建一个新的文字样式
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="styleName">文字样式名</param>
        /// <param name="fontFilename">字体文件名</param>
        /// <param name="bigFontFilename">大字体文件名</param>
        /// <returns>返回添加的文字样式的Id</returns>
        public static ObjectId AddTextStyle(this Database db, string styleName, string fontFilename, string bigFontFilename)
        {
            //打开文字样式表
            TextStyleTable st = (TextStyleTable)db.TextStyleTableId.GetObject(OpenMode.ForRead);
            if (!st.Has(styleName))//如果不存在名为styleName的文字样式，则新建一个文字样式
            {
                //定义一个新的文字样式表记录
                TextStyleTableRecord str = new TextStyleTableRecord();
                str.Name = styleName;//设置文字样式名
                str.FileName = fontFilename;//设置字体文件名
                str.BigFontFileName = bigFontFilename;//设置大字体文件名
                st.UpgradeOpen();//切换文字样式表的状态为写以添加新的文字样式
                st.Add(str);//将文字样式表记录的信息添加到文字样式表中
                //把文字样式表记录添加到事务处理中
                db.TransactionManager.AddNewlyCreatedDBObject(str, true);
                st.DowngradeOpen();//为了安全，将文字样式表的状态切换为读
            }
            return st[styleName];//返回新添加的文字样式表记录的ObjectId
        }



    }
}
