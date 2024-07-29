using System.Drawing;
using System.Management;

namespace DYH.Tools;

public static class SystemTool
{
    /// <summary>
    /// 获取D盘序列号
    /// </summary>
    /// <returns></returns>
    public static int GetDisk_D_SerialNumber()
    {
        var disk = new ManagementObject("win32_logicaldisk.deviceid=\"D:\"");
        disk.Get();
        var number16 = disk.GetPropertyValue("VolumeSerialNumber").ToString();
        return Convert.ToInt32(number16, 16);
    }

    /// <summary>
    /// 关闭进程
    /// </summary>
    /// <param name="procName">进程名</param>
    /// <returns></returns>
    public static bool CloseProc(string procName)
    {
        var result = false;

        foreach (var thisProc in Process.GetProcesses())
        {
            var tempName = thisProc.ProcessName;
            if (tempName == procName)
            {
                //if (!thisProc.CloseMainWindow())
                thisProc.Kill(); //当发送关闭窗口命令无效时强行结束进程                    
                result = true;
            }
        }

        return result;
    }

    public static double GetScreenScale()
    {
        var scale = Graphics.FromHwnd(IntPtr.Zero).DpiX / 96.0f;
        return scale;
    }
}