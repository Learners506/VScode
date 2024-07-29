namespace DYH.Tools;

public static class Com
{
    /// <summary>
    ///     com接口获取对象属性值，类似VisualLisp的vlax-get-property函数
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="key">属性名称</param>
    /// <returns>属性值</returns>
    public static object GetProperty(this object obj, string key)
    {
        return obj.GetType().InvokeMember(key, BindingFlags.GetProperty, null, obj, null);
    }

    /// <summary>
    ///     com接口获取对象属性值，类似VisualLisp的vlax-get-property函数
    /// </summary>
    /// <typeparam name="T">推断类型</typeparam>
    /// <param name="obj">对象</param>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    public static T GetProperty<T>(this object obj, string key)
    {
        try
        {
            var value = obj.GetProperty(key);
            if (value is T tValue) return tValue;
            return (T)value;
        }
        catch
        {
            throw new Exception("属性" + key + "获取错误");
        }
    }

    /// <summary>
    ///     com接口设置对象属性值，类似VisualLisp的vlax-put-property函数
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="key">属性名称</param>
    /// <param name="value">属性值</param>
    public static void SetProperty(this object obj, string key, object value)
    {
        obj.GetType().InvokeMember(key, BindingFlags.SetProperty, null, obj, new[] { value });
    }

    /// <summary>
    ///     com接口使用com方法，类似VisualLisp的vlax-invoke-method函数
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="method">方法名</param>
    /// <param name="objArray">方法需要的参数</param>
    /// <returns>方法的返回值</returns>
    public static object InvokeMethod(this object obj, string method, params object[] objArray)
    {
        return obj.GetType().InvokeMember(method, BindingFlags.InvokeMethod, null, obj, objArray);
    }
}

public class Menu
{
    public Menu(string menuName)
    {
#if acad
        var _menus = Acap.MenuGroups.InvokeMethod("item", "Acad").GetProperty("menus");
#else
        var _menus = Acap.MenuGroups.InvokeMethod("item", 0).GetProperty("menus");
#endif
        try
        {
            _menus.InvokeMethod("Add", menuName);
        }
        catch
        {
            // ignored
        }

        ComMenu = _menus.InvokeMethod("item", menuName);
    }

    private Menu(object menu)
    {
        ComMenu = menu;
    }

    public object ComMenu { get; }

    public void AddMenuItem(int index, string name, string command)
    {
        ComMenu.InvokeMethod("addmenuitem", index, name, "\u0003\u0003" + command + " ");
    }

    public void AddMenuItem(string name, string command)
    {
        try
        {
            ComMenu.InvokeMethod("addmenuitem", GetCount(), name, "\u0003\u0003" + command + " ");
        }
        catch
        {
            throw new Exception(name + command + "菜单加载错误");
        }
    }

    public Menu AddSubMenu(int index, string name)
    {
        return new Menu(ComMenu.InvokeMethod("addsubmenu", index, name));
    }

    public Menu AddSubMenu(string name)
    {
        return new Menu(ComMenu.InvokeMethod("addsubmenu", GetCount(), name));
    }

    public void AddSeparator(int index)
    {
        ComMenu.InvokeMethod("addseparator", index);
    }

    public void AddSeparator()
    {
        ComMenu.InvokeMethod("addseparator", GetCount());
    }

    public void DeleteItem(int index = 0)
    {
        ComMenu.InvokeMethod("item", index).InvokeMethod("delete");
    }

    public void InsertInMenuBar(int? index = null)
    {
        //this.RemoveFromMenuBar();
        try
        {
            if (index == null) ComMenu.InvokeMethod("InsertInMenuBar", "");
            else ComMenu.InvokeMethod("InsertInMenuBar", index);
        }
        catch
        {
            // ignored
        }
    }

    public void RemoveFromMenuBar()
    {
        try
        {
            ComMenu.InvokeMethod("RemoveFromMenuBar");
        }
        catch
        {
            // ignored
        }
    }

    private int GetCount()
    {
        return Convert.ToInt32(ComMenu.GetProperty("Count"));
    }

    public void Clear()
    {
        while (Convert.ToInt32(ComMenu.GetProperty("Count")) > 0)
            ComMenu.InvokeMethod("item", 0).InvokeMethod("delete");
    }
}