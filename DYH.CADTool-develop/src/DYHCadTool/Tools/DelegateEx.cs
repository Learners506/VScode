namespace DYH.Tools;

public static class DelegateEx
{
    /// <summary>
    /// 临时表达式
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <param name="func">委托函数</param>
    /// <returns>返回值</returns>
    public static T R<T>(Func<T> func) => func();
}
