using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.DotNetARX
{
    public static partial class Tools
    {
        /// <summary>
        /// 获取当前.NET程序所在的目录
        /// </summary>
        /// <returns>返回当前.NET程序所在的目录</returns>
        public static string GetCurrentPath()
        {
            var moudle = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            return System.IO.Path.GetDirectoryName(moudle.FullyQualifiedName);
        }
        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果字符串为数字，返回true，否则返回false</returns>
        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 将实体添加到模型空间
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="ent">要添加的实体</param>
        /// <returns>返回添加到模型空间中的实体ObjectId</returns>
        public static ObjectId AddToModelSpace(this Database db, Entity ent)
        {
            ObjectId entId;//用于返回添加到模型空间中的实体ObjectId
            //定义一个指向当前数据库的事务处理，以添加直线
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //以读方式打开块表
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                //以写方式打开模型空间块表记录.
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                entId = btr.AppendEntity(ent);//将图形对象的信息添加到块表记录中
                trans.AddNewlyCreatedDBObject(ent, true);//把对象添加到事务处理中
                trans.Commit();//提交事务处理
            }
            return entId; //返回实体的ObjectId
        }

        /// <summary>
        /// 将实体添加到模型空间
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="ents">要添加的多个实体</param>
        /// <returns>返回添加到模型空间中的实体ObjectId集合</returns>
        public static ObjectIdCollection AddToModelSpace(this Database db, params Entity[] ents)
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            var trans = db.TransactionManager;
            BlockTableRecord btr = (BlockTableRecord)trans.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(db), OpenMode.ForWrite);
            foreach (var ent in ents)
            {
                ids.Add(btr.AppendEntity(ent));
                trans.AddNewlyCreatedDBObject(ent, true);
            }
            btr.DowngradeOpen();
            return ids;
        }

    }
}
