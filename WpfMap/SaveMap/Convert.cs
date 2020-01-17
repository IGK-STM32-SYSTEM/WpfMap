using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfMap.SaveMap
{
    public class Convert
    {
        #region 原始转基本类型
        public static ShapesBase.BaseTextBlock TextBlockToBase(TextBlock textBlock)
        {
            ShapesBase.BaseTextBlock baseText = new ShapesBase.BaseTextBlock();
            baseText.FontSize = textBlock.FontSize;
            baseText.Foreground = textBlock.Foreground;
            baseText.Text = textBlock.Text;
            baseText.Thickness = textBlock.Margin;
            return baseText;
        }
        public static ShapesBase.BaseEllipse EllipseToBase(Ellipse ellipse)
        {
            ShapesBase.BaseEllipse baseEllipse = new ShapesBase.BaseEllipse();
            baseEllipse.Fill = ellipse.Fill;
            baseEllipse.Thickness = ellipse.Margin;
            baseEllipse.Stroke = ellipse.Stroke;
            baseEllipse.StrokeThickness = ellipse.StrokeThickness;
            baseEllipse.Width = ellipse.Width;
            baseEllipse.Height = ellipse.Height;
            return baseEllipse;
        }
        public static ShapesBase.BaseRectangle RectangleToBase(Rectangle rectangle)
        {
            ShapesBase.BaseRectangle baseRectangle = new ShapesBase.BaseRectangle();
            baseRectangle.Height = rectangle.Height;
            baseRectangle.Stroke = rectangle.Stroke;
            baseRectangle.StrokeDashArray = rectangle.StrokeDashArray;
            baseRectangle.StrokeDashCap = rectangle.StrokeDashCap;
            baseRectangle.StrokeThickness = rectangle.StrokeThickness;
            baseRectangle.Thickness = rectangle.Margin;
            baseRectangle.Width = rectangle.Width;
            return baseRectangle;
        }
        #endregion

        #region 基本类型转原始
        public static TextBlock BaseToTextBlock(ShapesBase.BaseTextBlock baseText)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = baseText.FontSize;
            textBlock.Foreground = baseText.Foreground;
            textBlock.Text = baseText.Text;
            textBlock.Margin = baseText.Thickness;
            return textBlock;
        }
        public static Ellipse BaseToEllipse(ShapesBase.BaseEllipse baseEllipse)
        {

            Ellipse ellipse = new Ellipse();
            ellipse.Fill = baseEllipse.Fill;
            ellipse.Margin = baseEllipse.Thickness;
            ellipse.Stroke = baseEllipse.Stroke;
            ellipse.StrokeThickness = baseEllipse.StrokeThickness;
            ellipse.Width = baseEllipse.Width;
            ellipse.Height = baseEllipse.Height;
            return ellipse;
        }
        public static Rectangle BaseToRectangle(ShapesBase.BaseRectangle baseRectangle)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = baseRectangle.Height;
            rectangle.Stroke = baseRectangle.Stroke;
            rectangle.StrokeDashArray = baseRectangle.StrokeDashArray;
            rectangle.StrokeDashCap = baseRectangle.StrokeDashCap;
            rectangle.StrokeThickness = baseRectangle.StrokeThickness;
            rectangle.Margin = baseRectangle.Thickness;
            rectangle.Width = baseRectangle.Width;
            return rectangle;
        }
        #endregion
    }
}
