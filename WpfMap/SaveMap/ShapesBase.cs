using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfMap.SaveMap
{
    /// <summary>
    /// 图形基本属性,用于保存图形
    /// </summary>
    public class ShapesBase
    {
        public class BaseEllipse
        {
            /// <summary>
            /// margin
            /// </summary>
            public Thickness Thickness { get; set; }
            /// <summary>
            /// 高度
            /// </summary>
            public double Height { get; set; }
            /// <summary>
            /// 宽度
            /// </summary>
            public double Width { get; set; }
            /// <summary>
            /// 轮廓厚度
            /// </summary>
            public double StrokeThickness { get; set; }
            /// <summary>
            /// 填充颜色
            /// </summary>
            public Brush Fill { get; set; }
            /// <summary>
            /// 轮廓颜色
            /// </summary>
            public Brush Stroke { get; set; }
        }
        public class BaseTextBlock
        {
            /// <summary>
            /// margin
            /// </summary>
            public Thickness Thickness { get; set; }
            /// <summary>
            /// 颜色
            /// </summary>
            public Brush Foreground { get; set; }
            /// <summary>
            /// 文字
            /// </summary>
            public string Text { get; set; }
            /// <summary>
            /// 字体
            /// </summary>
            public double FontSize { get; set; }
        }
        public class BaseRectangle
        {
            /// <summary>
            /// margin
            /// </summary>
            public Thickness Thickness { get; set; }
            /// <summary>
            /// 填充颜色
            /// </summary>
            public Brush Fill { get; set; }
            /// <summary>
            /// 轮廓厚度
            /// </summary>
            public double StrokeThickness { get; set; }
            /// <summary>
            /// 轮廓颜色
            /// </summary>
            public Brush Stroke { get; set; }
            /// <summary>
            /// 高度
            /// </summary>
            public double Height { get; set; }
            /// <summary>
            /// 宽度
            /// </summary>
            public double Width { get; set; }
            /// <summary>
            /// StrokeDashArray
            /// </summary>
            public DoubleCollection StrokeDashArray { get; set; }
            /// <summary>
            /// StrokeDashCap
            /// </summary>
            public PenLineCap StrokeDashCap { get; set; }
        }
    }
}
