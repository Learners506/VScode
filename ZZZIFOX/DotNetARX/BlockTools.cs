using Autodesk.AutoCAD.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.DotNetARX
{
    public static class BlockTools
    {
        /// <summary>
        /// 把存储在列表中的实体创建为一个块
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="blockName">块名</param>
        /// <param name="ents">加入块中的实体列表</param>
        /// <returns>返回块表记录的Id</returns>
        public static ObjectId AddBlockTableRecord(this Database db, string blockName, List<Entity> ents)
        {
            //打开块表
            BlockTable bt = (BlockTable)db.BlockTableId.GetObject(OpenMode.ForRead);
            if (!bt.Has(blockName)) //判断是否存在名为blockName的块
            {
                //创建一个BlockTableRecord类的对象，表示所要创建的块
                BlockTableRecord btr = new BlockTableRecord();
                btr.Name = blockName;//设置块名                
                //将列表中的实体加入到新建的BlockTableRecord对象
                ents.ForEach(ent => btr.AppendEntity(ent));
                bt.UpgradeOpen();//切换块表为写的状态
                bt.Add(btr);//在块表中加入blockName块
                db.TransactionManager.AddNewlyCreatedDBObject(btr, true);//通知事务处理
                bt.DowngradeOpen();//为了安全，将块表状态改为读
            }
            return bt[blockName];//返回块表记录的Id
        }

        /// <summary>
        /// 创建一个块表记录并添加到数据库中
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="blockName">块名</param>
        /// <param name="ents">加入块中的实体列表</param>
        /// <returns>返回块表记录的Id</returns>
        public static ObjectId AddBlockTableRecord(this Database db, string blockName, params Entity[] ents)
        {
            return AddBlockTableRecord(db, blockName, ents.ToList());
        }
        /// <summary>
        /// 在AutoCAD图形中插入块参照
        /// </summary>
        /// <param name="spaceId">块参照要加入的模型空间或图纸空间的Id,通过db.CurrentSpaceId</param>
        /// <param name="layer">块参照要加入的图层名</param>
        /// <param name="blockName">块参照所属的块名</param>
        /// <param name="position">插入点</param>
        /// <param name="scale">缩放比例</param>
        /// <param name="rotateAngle">旋转角度</param>
        /// <returns>返回块参照的Id</returns>
        public static ObjectId InsertBlockReference(this ObjectId spaceId, string layer, string blockName, Point3d position, Scale3d scale, double rotateAngle)
        {
            ObjectId blockRefId;//存储要插入的块参照的Id
            Database db = spaceId.Database;//获取数据库对象
            //以读的方式打开块表
            BlockTable bt = (BlockTable)db.BlockTableId.GetObject(OpenMode.ForRead);
            //如果没有blockName表示的块，则程序返回
            if (!bt.Has(blockName)) return ObjectId.Null;
            //以写的方式打开空间（模型空间或图纸空间）
            BlockTableRecord space = (BlockTableRecord)spaceId.GetObject(OpenMode.ForWrite);
            //创建一个块参照并设置插入点
            BlockReference br = new BlockReference(position, bt[blockName]);
            br.ScaleFactors = scale;//设置块参照的缩放比例
            br.Layer = layer;//设置块参照的层名
            br.Rotation = rotateAngle;//设置块参照的旋转角度
            ObjectId btrId = bt[blockName];//获取块表记录的Id
            //打开块表记录
            BlockTableRecord record = (BlockTableRecord)btrId.GetObject(OpenMode.ForRead);
            //添加可缩放性支持
            if (record.Annotative == AnnotativeStates.True)
            {
                ObjectContextCollection contextCollection = db.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
                ObjectContexts.AddContext(br, contextCollection.GetContext("1:1"));
            }
            blockRefId = space.AppendEntity(br);//在空间中加入创建的块参照
            db.TransactionManager.AddNewlyCreatedDBObject(br, true);//通知事务处理加入创建的块参照
            space.DowngradeOpen();//为了安全，将块表状态改为读
            return blockRefId;//返回添加的块参照的Id
        }



    }
}
