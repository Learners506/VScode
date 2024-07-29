using System.Windows.Input;
using Autodesk.AutoCAD.Windows;
using Window = System.Windows.Window;

namespace DYH.Tools;

public static class WindowTool
{
    public static void AddEscQuit(this Window window)
    {
        window.KeyDown -= Window_KeyDown_Esc;
        window.KeyDown += Window_KeyDown_Esc;
        window.Closed -= WindowOnClosed;
        window.Closed += WindowOnClosed;
    }

    /// <summary>
    /// 关闭时减掉事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void WindowOnClosed(object sender, EventArgs e)
    {
        if (sender is not Window window)
            return;
        window.KeyDown -= Window_KeyDown_Esc;
        window.Closed -= WindowOnClosed;
    }

    private static void Window_KeyDown_Esc(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key != Key.Escape
            || sender is not Window { IsLoaded: true, IsActive: true } window)
            return;

        // 判断没有按住ctrl或shift或alt才执行
        var keys = Control.ModifierKeys;
        if ((keys & Keys.Control) != 0
            || (keys & Keys.Shift) != 0
            || (keys & Keys.Alt) != 0)
            return;
        window.Close();
    }

    public static bool IsModel(this Window window)
    {
        return (bool)(typeof(Window)
            .GetField("_showingAsDialog", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(window) ?? false);
    }

    /// <summary>
    /// 获取屏幕分辨率
    /// </summary>
    /// <param name="windowHandle">窗口句柄</param>
    /// <returns>屏幕尺寸</returns>
    public static Size GetScreenResolutionFromWindowHandle(IntPtr windowHandle)
    {
        var screen = Screen.FromHandle(windowHandle);
        return new Size(screen.Bounds.Width, screen.Bounds.Height);
    }

    /// <summary>
    /// 通过分辨率设置面板尺寸
    /// </summary>
    /// <param name="paletteSet">侧栏</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    public static void SetSizeByScreenResolution(this PaletteSet paletteSet, int width, int height)
    {
        var size = GetScreenResolutionFromWindowHandle(Acap.MainWindow.Handle);
        var scale = size.Height * 1d / 1080;
        var newWidth = Convert.ToInt32(width * scale);
        if (newWidth > size.Width)
            newWidth = size.Width;
        var newHeight = Convert.ToInt32(height * scale);
        if (newHeight > size.Height)
            newHeight = size.Height;
        paletteSet.SetSize(new Size(newWidth, newHeight));
    }

    #region Win32

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <param name="hWnd"></param>
    public static void CloseWindow(IntPtr hWnd)
    {
        SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
    }
    // 定义 WM_CLOSE 消息
    const UInt32 WM_CLOSE = 0x0010;

    #endregion
}