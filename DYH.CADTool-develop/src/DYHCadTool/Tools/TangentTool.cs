namespace DYH.Tools;

public static class TangentTool
{
    #region 获取天正比例

    public static double TgetPscale()
    {
        return DocGetPScale();
    }

    [DllImport("tch_kernal.arx", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "?DocGetPScale@@YANXZ")]
    private static extern double DocGetPScale();

    #endregion

    #region 风口

    /// <summary>
    ///     获取风口信息
    /// </summary>
    /// <param name="ent">对象</param>
    /// <returns></returns>
    public static WindGapInfo GetWindGapInfo(this Entity ent)
    {
        return new WindGapInfo(ent);
    }

    /// <summary>
    ///     判断对象是风口
    /// </summary>
    /// <param name="ent"></param>
    /// <returns></returns>
    public static bool IsWindGap(this Entity ent)
    {
        try
        {
            var _windGap = ent.AcadObject;
            if (_windGap.GetProperty<string>("ObjectName") != WindGapInfo.ObjectName)
                return false;
            if (_windGap.GetProperty<string>("Hvac_S4") != "风口")
                return false;
            return true;
        }
        catch
        {
            return false;
        }
    }

    #region 风口信息

    public class WindGapInfo
    {
        public const string ObjectName = "TDbHvacEquipment";

        /// <summary>
        ///     com图元
        /// </summary>
        private readonly object _windGap;

        public WindGapInfo(Entity ent)
        {
            _windGap = ent.AcadObject;
            ent.IsWindGap();
        }

        /// <summary>
        ///     名字
        /// </summary>
        public string Name => _windGap.GetProperty<string>("Hvac_S5");

        /// <summary>
        ///     是圆形
        /// </summary>
        public bool IsCirCle => _windGap.GetProperty<int>("Hvac_I0") == 1;

        /// <summary>
        ///     是侧风口
        /// </summary>
        public bool IsSideFace => _windGap.GetProperty<int>("Hvac_I11") == 1;

        /// <summary>
        ///     长
        /// </summary>
        public double Length => _windGap.GetProperty<double>("Hvac_R28");

        /// <summary>
        ///     宽
        /// </summary>
        public double Width => _windGap.GetProperty<double>("Hvac_R2");

        /// <summary>
        ///     高
        /// </summary>
        public double Height => _windGap.GetProperty<double>("Hvac_R3");

        /// <summary>
        ///     直径
        /// </summary>
        public double Diam => _windGap.GetProperty<double>("Hvac_R28");

        /// <summary>
        ///     风量
        /// </summary>
        public double AirVolumn => _windGap.GetProperty<double>("Hvac_R27");
    }

    #endregion

    /// <summary>
    ///     获取尺寸
    /// </summary>
    /// <param name="windGapInfo">风口信息</param>
    /// <returns></returns>
    public static string GetSize(this WindGapInfo windGapInfo)
    {
        if (windGapInfo.IsCirCle) return "%%c" + windGapInfo.Diam;

        if (windGapInfo.IsSideFace)
            return windGapInfo.Width.ToString("0") + "x" + windGapInfo.Length.ToString("0");
        return windGapInfo.Length.ToString("0") + "x" + windGapInfo.Width.ToString("0");
    }

    #endregion
}

public static class WindPipeInfoEx
{
    public const string ObjectName = "TDbHvacDuct";

    /// <summary>
    ///     获取风管信息
    /// </summary>
    /// <param name="curve"></param>
    /// <returns></returns>
    public static WindPipeInfo GetWindPipeInfo(this Curve curve)
    {
        return new WindPipeInfo(curve);
    }

    /// <summary>
    ///     是风管
    /// </summary>
    /// <param name="ent">对象</param>
    /// <returns></returns>
    public static bool IsWindPipe(this Entity ent)
    {
        try
        {
            var _windGap = ent.AcadObject;
            if (_windGap.GetProperty<string>("ObjectName") != ObjectName)
                return false;
            if (_windGap.GetProperty<string>("Hvac_S4") != "风口")
                return false;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     风管信息
    /// </summary>
    public class WindPipeInfo
    {
        /// <summary>
        ///     com图元
        /// </summary>
        private readonly object _windPipe;

        public WindPipeInfo(Curve cur)
        {
            _windPipe = cur.AcadObject;
            cur.IsWindGap();
        }

        /// <summary>
        ///     名字
        /// </summary>
        public string Name => _windPipe.GetProperty<string>("Hvac_S5");

        /// <summary>
        ///     是圆形
        /// </summary>
        public bool IsCirCle => _windPipe.GetProperty<int>("Hvac_I0") == 1;

        /// <summary>
        ///     是侧风口
        /// </summary>
        public bool IsSideFace => _windPipe.GetProperty<int>("Hvac_I11") == 1;

        /// <summary>
        ///     长
        /// </summary>
        public double Length => _windPipe.GetProperty<double>("Hvac_R28");

        /// <summary>
        ///     宽
        /// </summary>
        public double Width => _windPipe.GetProperty<double>("Hvac_R2");

        /// <summary>
        ///     高
        /// </summary>
        public double Height => _windPipe.GetProperty<double>("Hvac_R3");

        /// <summary>
        ///     直径
        /// </summary>
        public double Diam => _windPipe.GetProperty<double>("Hvac_R28");
    }
}