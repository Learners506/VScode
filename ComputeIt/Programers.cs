using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeIt
{
    public class Programers
    {

        static void Main(string[] args)
        {
            double 桩径 = 0.076;
            double 叶片径 = 0.17;
            double 桩长 = 1.7;
            double 桩尖长 = 0.15;
            double 叶片间距 = 0.6;
            double 上片间距 = 0.7875;
            double 下片顶距 = 1.510;


            double 分割高度 = 0;
            double 侧摩系数1 = 20;
            double 侧摩系数2 = 20;
            double 抗拔系数1 = 0.7;
            double 抗拔系数2 = 0.7;

            double 桩径周长 = Math.PI * 桩径;
            double 叶片周长 = Math.PI * 叶片径;
            //Console.WriteLine("桩径周长：" + 桩径周长);
            //Console.WriteLine("叶片周长：" + 叶片周长);
            //Console.ReadKey();

            // 单层土抗压承载力计算
            double u1 = Math.PI * 桩径;
            double u2 = 0;
            double u3 = Math.PI * 叶片径;
            double u4 = Math.PI * 桩径;
            double u5 = 0;

            double l1 = 桩长 - 上片间距 - 叶片径;
            double l2 = 叶片径;
            double l3 = 3 * 叶片径;
            double l4 = 叶片间距 - 4 * 叶片径;
            double l5 = 叶片径;

            List<double> 计算周长列表 = new List<double>();
            List<double> 计算长度列表 = new List<double>();
            double 侧向阻力 = 0;

            if (叶片间距 <= 3 * 叶片径)
            {
                // 使用u1、u2、u6
                double u6 = Math.PI * 叶片径;
                double l6 = 叶片间距;
                计算周长列表 = new List<double>() { u1, u2, u6 };
                计算长度列表 = new List<double>() { l1, l2, l6 };
                侧向阻力 = 计算侧向阻力(计算周长列表, 计算长度列表, 分割高度, 侧摩系数1, 侧摩系数2);
            }
            else if (3 * 叶片径 < 叶片间距 && 叶片间距 < 4 * 叶片径)
            {
                // 使用u1、u2、u3、u7
                double u7 = 0;
                double l7 = 叶片间距 - 3 * 叶片径;
                计算周长列表 = new List<double>() { u1, u2, u3, u7 };
                计算长度列表 = new List<double>() { l1, l2, l3, l7 };
                侧向阻力 = 计算侧向阻力(计算周长列表, 计算长度列表, 分割高度, 侧摩系数1, 侧摩系数2);
            }
            else
            {
                // 使用u1、u2、u3、u4、u5
                计算周长列表 = new List<double>() { u1, u2, u3, u4, u5 };
                计算长度列表 = new List<double>() { l1, l2, l3, l4, l5 };
                侧向阻力 = 计算侧向阻力(计算周长列表, 计算长度列表, 分割高度, 侧摩系数1, 侧摩系数2);
            }


            // 抗拔承载力计算
            double v1 = Math.PI * 桩径;
            double v2 = Math.PI * 叶片径;
            double v3 = 0;
            double v4 = Math.PI * 桩径;
            double v5 = Math.PI * 叶片径;

            double m1 = 桩长 - 上片间距 - 2*叶片径;
            double m2 = 2* 叶片径;
            double m3 = 叶片径;
            double m4 = 叶片间距 - 4 * 叶片径;
            double m5 = 3* 叶片径;
            List<double> 计算周长列表2 = new List<double>();
            List<double> 计算长度列表2 = new List<double>();
            double 抗拔承载力 = 0;
            if (叶片间距 <= 3 * 叶片径)
            {
                // 使用v1、v2、v6
                double v6 = Math.PI * 叶片径;
                double m6 = 叶片间距;
                计算周长列表2 = new List<double>() { v1, v2, v6 };
                计算长度列表2 = new List<double>() { m1, m2, m6 };
                抗拔承载力 = 计算抗拔力(计算周长列表2, 计算长度列表2, 分割高度, 抗拔系数1, 抗拔系数2, 侧摩系数1, 侧摩系数2);
            }
            else if (3 * 叶片径 < 叶片间距 && 叶片间距 < 4 * 叶片径)
            {
                // 使用v1、v2、v7、v8
                double v7 = Math.PI * 叶片径;
                double v8 = 0;
                double m7 = 3* 叶片径;
                double m8 = 叶片间距 - 3 * 叶片径;
                计算周长列表2 = new List<double>() { v1, v2, v7, v8 };
                计算长度列表2 = new List<double>() { m1, m2, m7, m8 };
                抗拔承载力 = 计算抗拔力(计算周长列表2, 计算长度列表2, 分割高度, 抗拔系数1, 抗拔系数2, 侧摩系数1, 侧摩系数2);

            }
            else
            {
                // 使用v1、v2、v3、v4、v5
                计算周长列表2 = new List<double>() { v1, v2, v3, v4, v5 };
                计算长度列表2 = new List<double>() { m1, m2, m3, m4, m5 };
                抗拔承载力 = 计算抗拔力(计算周长列表2, 计算长度列表2, 分割高度, 抗拔系数1, 抗拔系数2, 侧摩系数1, 侧摩系数2);
            }
            

            

            
            Console.WriteLine("侧向阻力值："+侧向阻力.ToString());
            Console.WriteLine("抗拔承载力："+抗拔承载力.ToString());
            Console.ReadKey();
        }

        public static double 计算侧向阻力(List<double> 计算周长列表,List<double> 计算长度列表,double 分割高度,double 侧摩系数1,double 侧摩系数2)
        {
            double load = 0;
            int indexcount = 计算周长列表.Count;
            if (分割高度 <= 计算长度列表[0])
            {
                load = 计算周长列表[0] * ( 分割高度 * 侧摩系数1 + (计算长度列表[0]-分割高度)*侧摩系数2 );
                for (int i = 1; i < indexcount; i++)
                {
                    load = load + 计算长度列表[i] * 侧摩系数2 * 计算周长列表[i];
                }
            }
            else
            {
                (int,int) index = FindIndices(计算长度列表, 分割高度);
                int preindex = index.Item1;
                for (int i = 0; i < preindex+1; i++)
                {
                    load = load + 计算长度列表[i] * 侧摩系数1 * 计算周长列表[i];
                }
                load = load + ((分割高度 - 计算长度列表.Take(preindex+1).Sum()) * 侧摩系数1 + (计算长度列表.Take(preindex+2).Sum()- 分割高度) * 侧摩系数2) * 计算周长列表[preindex+1];
                for (int i = preindex + 2; i < indexcount; i++)
                {
                    load = load + 计算长度列表[i] * 侧摩系数2 * 计算周长列表[i];
                }
            }
            return load;
        }

        public static double 计算抗拔力(List<double> 计算周长列表2, List<double> 计算长度列表2, double 分割高度,double 抗拔系数1,double 抗拔系数2,double 侧摩系数1,double 侧摩系数2) 
        {
            double load = 0;
            int indexcount = 计算周长列表2.Count;
            if (分割高度 <= 计算长度列表2[0])
            {
                load = 计算周长列表2[0] * (分割高度 * 抗拔系数1*侧摩系数1 + (计算长度列表2[0] - 分割高度) * 抗拔系数2*侧摩系数2);
                for (int i = 1; i < indexcount; i++)
                {
                    load = load + 计算长度列表2[i] * 抗拔系数2 * 侧摩系数2 * 计算周长列表2[i];
                }
            }
            else
            {
                (int, int) index = FindIndices(计算长度列表2, 分割高度);
                int preindex = index.Item1;
                for (int i = 0; i < preindex+1; i++)
                {
                    load = load + 计算长度列表2[i] * 抗拔系数1*侧摩系数1 * 计算周长列表2[i];
                }
                load = load + ((分割高度 - 计算长度列表2.Take(preindex + 1).Sum()) * 抗拔系数1*侧摩系数1 + (计算长度列表2.Take(preindex + 2).Sum() - 分割高度) * 抗拔系数2*侧摩系数2) * 计算周长列表2[preindex+1];
                for (int i = preindex + 2; i < indexcount; i++)
                {
                    load = load + 计算长度列表2[i] * 抗拔系数2 * 侧摩系数2 * 计算周长列表2[i];
                }
            }
            return load;
        }
        public static double 侧摩阻力值(double height, double 分割高度)
        {
            var 侧摩阻力值 = 0;
            if (height < 分割高度)
            {
                侧摩阻力值 = 20;
            }
            else
            {
                侧摩阻力值 = 40;
            }
            return 侧摩阻力值;
        }
        public static (int, int) FindIndices(List<double> values, double target)
        {
            double sum = 0;
            int prevIndex = -1;

            for (int i = 0; i < values.Count; i++)
            {
                sum += values[i];

                if (sum >= target)
                {
                    return (prevIndex, i);
                }

                prevIndex = i;
            }

            return (-1, -1);  // 如果未找到满足条件的情况
        }
    }
}
