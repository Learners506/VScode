using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZIFOX.Settings;
using ZZZIFOX.WPFUI;

namespace ZZZIFOX.ProjectItems
{
    public class Translater
    {
        private static Translateset ThisOptions => Translateset.Default;
        

        [CommandMethod(nameof(TRANS))]
        public void TRANS()
        {
            ThisOptions.Reload();
            while (true)
            {
                PromptKeywordOptions pko = new PromptKeywordOptions("\n设置[S]");
                pko.Keywords.Add("S");
                pko.Keywords.Default = "S";
                pko.AppendKeywordsToMessage = false;

                var pr = Env.Editor.GetKeywords(pko);
   
                if (pr.Status == PromptStatus.OK && pr.StringResult == "S")
                {
                    new TranslateUI().ShowDialog();
                    
                }
                else if (pr.Status == PromptStatus.Cancel)
                {
                    // 用户输入ESC退出
                    break;
                }
                else
                {
                    Env.Editor.WriteMessage()
                }

            }




        }
    }
}
