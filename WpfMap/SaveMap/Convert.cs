using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        public static ShapesBase.BaseLine LineToBase(Line line)
        {
            ShapesBase.BaseLine baseLine = new ShapesBase.BaseLine();
            baseLine.Stroke = line.Stroke;
            baseLine.StrokeThickness = line.StrokeThickness;
            baseLine.StrokeDashArray = line.StrokeDashArray;
            baseLine.StrokeDashCap = line.StrokeDashCap;
            baseLine.Thickness = line.Margin;
            baseLine.X1 = line.X1;
            baseLine.X2 = line.X2;
            baseLine.Y1 = line.Y1;
            baseLine.Y2 = line.Y2;
            return baseLine;
        }
        public static ShapesBase.BaseForkLiePath ForkLiePathToBase(Path path)
        {
            ShapesBase.BaseForkLiePath baseForkLiePath = new ShapesBase.BaseForkLiePath();
            baseForkLiePath.Stroke = path.Stroke;
            baseForkLiePath.StrokeThickness = path.StrokeThickness;
            baseForkLiePath.Thickness = path.Margin;

            //找到圆弧起点
            if (path.Data != null)
            {
                PathGeometry pathGeometry = path.Data as PathGeometry;
                PathFigure figure = pathGeometry.Figures.First();
                ArcSegment arc = figure.Segments.First() as ArcSegment;
                baseForkLiePath.ArcStopPoint = arc.Point;
                baseForkLiePath.FigureStartPoint = figure.StartPoint;
                baseForkLiePath.Radius = arc.Size.Width;
                baseForkLiePath.sweepDirection = arc.SweepDirection;
            }
            return baseForkLiePath;
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
        public static Line BaseToLine(ShapesBase.BaseLine baseLine)
        {
            Line line = new Line();
            line.Stroke = baseLine.Stroke;
            line.StrokeThickness = baseLine.StrokeThickness;
            line.StrokeDashArray = baseLine.StrokeDashArray;
            line.StrokeDashCap = baseLine.StrokeDashCap;
            line.Margin = baseLine.Thickness;
            line.X1 = baseLine.X1;
            line.X2 = baseLine.X2;
            line.Y1 = baseLine.Y1;
            line.Y2 = baseLine.Y2;
            return line;
        }
        public static Path BaseToForkLiePath(ShapesBase.BaseForkLiePath baseForkLiePath)
        {
            Path path = new Path();
            path.Stroke = baseForkLiePath.Stroke;
            path.StrokeThickness = baseForkLiePath.StrokeThickness;
            path.Margin = baseForkLiePath.Thickness;
            //定义圆弧半径
            double radius = baseForkLiePath.Radius;
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure figure = new PathFigure();
            //路径图起点坐标
            figure.StartPoint = baseForkLiePath.FigureStartPoint;
            ArcSegment arc = new ArcSegment(
               baseForkLiePath.ArcStopPoint,
               new Size(radius, radius),
               0,
               false,
               baseForkLiePath.sweepDirection,
               true);
            figure.Segments.Add(arc);
            pathGeometry.Figures.Add(figure);
            path.Data = pathGeometry;
            return path;
        }
        #endregion
    }
}
