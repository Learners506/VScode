using System.Windows.Media;

namespace DYHCadTool.Tools;

public static class ColorTool
{
    public static System.Windows.Media.Brush ToMediaBrush(this Color color)
    {
        var drawingColor = color.ColorValue;
        return new SolidColorBrush(
            System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B));
    }
}