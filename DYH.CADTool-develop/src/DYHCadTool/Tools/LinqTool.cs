using System;
using System.Collections.Generic;
using System.Text;

namespace DYH.Tools
{
    public static class LinqTool
    {
        public static List<List<T>> Friends<T>(this IEnumerable<T> list, Func<T, T, bool> func)
        {
            List<List<T>> result = [];
            var tList = list.ToList();
            while (tList.Any())
            {
                var t1 = tList.First();
                tList.RemoveAt(0);
                List<T> tempSet = [t1];
                List<T> chooseList = [t1];
                while (true)
                {
                    chooseList = chooseList.SelectMany(t2 => tList.Where(t3 => func(t2, t3))).ToList();
                    if (!chooseList.Any())
                        break;
                    chooseList.ForEach(t4 =>
                    {
                        tList.Remove(t4);
                        tempSet.Add(t4);
                    });
                }

                result.Add(tempSet);
            }

            return result;
        }
    }
}