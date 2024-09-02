using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZZZIFOX.DotNetARX;
using ZZZIFOX.Settings;
using ZZZIFOX.WPFUI;

namespace ZZZIFOX.ProjectItems
{
    public static class NewCopycs
    {
        private static NewCopy settings => NewCopy.Default;
        [CommandMethod(nameof(CON))]
        public static void CON()
        {
            //settings主要有两个方法一个是Reload一个是Save
            settings.Reload();
            var w1 = new NewCopyWindow();
            var dr = w1.ShowDialog();
            if (dr != true) return;

            string result = settings.TextString;
            string pattern = @"^\d+\*\d+(\.\d+)?(,\d+\*\d+(\.\d+)?)*$";
            using var tr =new DBTrans();
            // 进行验证
            if (!Regex.IsMatch(result, pattern))
            {
                // 弹出对话框提示用户输入错误
                System.Windows.MessageBox.Show("输入的格式不正确，请确保格式为 'm1*n1,m2*n2,...'，其中 m 为整数，n 为小数。", "输入错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var intList = new List<int>();
                var doubleList = new List<double>();

                string[] pairs = result.Split(',');
                foreach (string pair in pairs)
                {
                    string[] parts = pair.Split('*');
                    if (int.TryParse(parts[0], out int m))
                    {
                        intList.Add(m);
                    }
                    if (double.TryParse(parts[1], out double n))
                    {
                        doubleList.Add(n);
                    }
                }

                var pso1 = new PromptSelectionOptions()
                {
                    MessageForAdding = "\n请框选要复制的对象"
                };
                var r1 = Env.Editor.GetSelection(pso1);
                if (r1.Status != PromptStatus.OK) return;
                var basepoint = Env.Editor.GetPoint("\n请选择复制基点").Value.Wcs2Ucs();

                string directionInput = Env.Editor.GetString("\n请输入方向 (回车: 右, 1: 右, 2: 上, 3: 左, 4: 下): ").StringResult;

                double sum = 0;
                for (int i = 0; i < intList.Count; i++)
                {
                    var number = intList[i]; //数量
                    var distance = doubleList[i]; // Distance to copy

                    Point3d targetPoint = basepoint; // Initialize targetPoint with basePoint
                    for (int j = 0; j < number; j++)
                    {
                        sum += distance; // Update sum for each copy
                        // Update targetPoint based on the direction
                        switch (directionInput)
                        {
                            case "":
                            case "1":
                                targetPoint = new Point3d(basepoint.X - sum, basepoint.Y, basepoint.Z);
                                break;
                            case "2":
                                targetPoint = new Point3d(basepoint.X, basepoint.Y - sum, basepoint.Z);
                                break;
                            case "3":
                                targetPoint = new Point3d(basepoint.X + sum, basepoint.Y, basepoint.Z);
                                break;
                            case "4":
                                targetPoint = new Point3d(basepoint.X, basepoint.Y + sum, basepoint.Z);
                                break;
                        }
                        // Copy selected objects to the target point
                        foreach (var id in r1.Value.GetObjectIds())
                        {
                            id.Copy(basepoint, targetPoint);
                        }
                    }
                }
            }
        }
    }
}
