using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.DotNetARX
{
    public static class EntTools
    {
        /// <summary>
        /// 复制实体
        /// </summary>
        /// <param name="id">实体的ObjectId</param>
        /// <param name="sourcePt">复制的源点</param>
        /// <param name="targetPt">复制的目标点</param>
        /// <returns>返回复制实体的ObjectId</returns>
        public static ObjectId Copy(this ObjectId id, Point3d sourcePt, Point3d targetPt)
        {
            //构建用于复制实体的矩阵
            Vector3d vector = targetPt.GetVectorTo(sourcePt);
            Matrix3d mt = Matrix3d.Displacement(vector);
            //获取id表示的实体对象
            Entity ent = (Entity)id.GetObject(OpenMode.ForRead);
            //获取实体的拷贝
            Entity entCopy = ent.GetTransformedCopy(mt);
            //将复制的实体对象添加到模型空间
            ObjectId copyId = id.Database.AddToModelSpace(entCopy);
            return copyId; //返回复制实体的ObjectId
        }

        /// <summary>
        /// 复制实体
        /// </summary>
        /// <param name="ent">实体</param>
        /// <param name="sourcePt">复制的源点</param>
        /// <param name="targetPt">复制的目标点</param>
        /// <returns>返回复制实体的ObjectId</returns>
        public static ObjectId Copy(this Entity ent, Point3d sourcePt, Point3d targetPt)
        {
            ObjectId copyId;
            if (ent.IsNewObject) // 如果是还未被添加到数据库中的新实体
            {
                //构建用于复制实体的矩阵
                Vector3d vector = targetPt.GetVectorTo(sourcePt);
                Matrix3d mt = Matrix3d.Displacement(vector);
                //获取实体的拷贝
                Entity entCopy = ent.GetTransformedCopy(mt);
                //将复制的实体对象添加到模型空间
                copyId = ent.Database.AddToModelSpace(entCopy);
            }
            else
            {
                copyId = ent.ObjectId.Copy(sourcePt, targetPt);
            }
            return copyId; //返回复制实体的ObjectId
        }
    }
}
