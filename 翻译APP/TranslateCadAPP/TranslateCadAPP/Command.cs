using TranslateCadAPP.WPFUI;

namespace TranslateCadAPP
{
    public class Command
    {
        [CommandMethod(nameof(TCADD))]
        public void TCADD()
        {
            var f1 = new translatecad();
            f1.ShowDialog();
        }
    }
}
