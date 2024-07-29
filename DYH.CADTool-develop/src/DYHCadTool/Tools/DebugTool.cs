namespace DYH.Tools;

public static class DebugTool
{
    public static void PlotPropertys<T>(this T obj)
    {
        if(obj is null)
            return;
        var pa = obj.GetType().GetProperties().OrderBy(e => e.Name).ToArray();
        pa.Length.Prompt();
        foreach (var pi in pa)
        {
            var a = pi.Name.Prompt();
            Env.Editor.WriteMessage("————");
            if (pi.CanRead)
            {
                try
                {
                    var b = obj.GetProperty(a);
                    Env.Editor.WriteMessage(b.ToString());
                }
                catch
                {
                    Env.Editor.WriteMessage("Wrong");
                }
            }
        }
        var fi = obj.GetType().GetFields();
        fi.Length.Prompt();
        foreach (var item in fi)
        {
            var a = item.Name.Prompt();
            Env.Editor.WriteMessage("————");
            if (item.IsPublic)
            {
                try
                {
                    var b = obj.GetProperty(a);
                    Env.Editor.WriteMessage(b.ToString());
                }
                catch
                {
                    Env.Editor.WriteMessage("Wrong");
                }
            }
        }
    }
}
