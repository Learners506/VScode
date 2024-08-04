using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZIFOX.DotNetARX;

namespace ZZZIFOX
{
    public class test
    {
        [CommandMethod(nameof(CS999))]
        public void CS999()
        {
            string str1 = "999";
            string str2 = "aaa";
            Env.Editor.WriteMessage(str1.IsNumeric().ToString());
            Env.Editor.WriteMessage(str2.IsNumeric().ToString());
            using var tr = new DBTrans();
            var db = tr.Database;
            db.AddTextStyle("test", "tssdeng.shx", "hztxt.shx");
        }


    }
}
