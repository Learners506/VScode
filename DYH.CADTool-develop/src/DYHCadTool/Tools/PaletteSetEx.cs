namespace DYHCadTool.Tools;

public static class PaletteSetEx
{
    /// <summary>
    /// 设置侧边栏最小宽度
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    [DllImport("adui23.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "?AdUiSetDockBarMinWidth@@YA_NH@Z")]

    public static extern bool AdUiSetDockBarMinWidth(int width);
}

