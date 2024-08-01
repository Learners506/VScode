using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZIFOX.ProjectItems
{
    public class RotateOnSelf
    {
        [CommandMethod(nameof(test))]
        public void test()
        {
            List<int> nums = new List<int>() { 1, 2, 3, 4 };
            int n = nums.FirstOrDefault(x => x > 2);
            int n1 = nums.Find(x => x > 2);
            var n2 = nums.Where(x=> x > 2).ToList();
            foreach(var item in n2)
            {
                Env.Editor.WriteMessage($"\n{item.ToString()}\n");
            }


            //Env.Editor.WriteMessage($"\n{n1}");
        }



    }
}
