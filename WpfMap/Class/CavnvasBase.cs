using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfMap
{
    //绘图基础类，主要实现基本元素的绘制
    public class CavnvasBase
    {
        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="canvasObj"></param>
        /// <param name="textBlock"></param>
        public static void DrawText(double x, double y, string text, Color color, Canvas canvasObj, TextBlock textBlock)
        {
            textBlock.Text = text;
            textBlock.FontSize = 14;
            textBlock.Foreground = new SolidColorBrush(color);
            //获取文本的尺寸
            Size size = GetTextBlockSize(textBlock);
            textBlock.Margin = new Thickness(x-size.Width/2, y-size.Height/2, 0, 0);
            canvasObj.Children.Add(textBlock);
        }
        /// <summary>
        /// 获取透明度颜色
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static SolidColorBrush GetSolid(byte alpha, Color color)
        {
            return new SolidColorBrush(Color.FromArgb(100, color.R, color.G, color.B));
        }
        /// <summary>
        /// 获取文本长度和高度
        /// </summary>
        /// <param name="textBlock"></param>
        /// <returns></returns>
        public static Size GetTextBlockSize(TextBlock textBlock)
        {
            var formattedText = new FormattedText(
                textBlock.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution());
            return new Size(formattedText.Width, formattedText.Height);
        }
    }
}
