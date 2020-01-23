using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfMap
{
    public class MapElement
    {
        //画布比例
        public static double Scale = 1;
        //标签半径
        public static int RFID_Radius = 20;
        //分叉圆弧半径
        public static int ForkLineArc_Radius = 20;

        //背景栅格
        public static int GridSize = 10;
        //定义画布对象
        public static Canvas CvGrid;//栅格
        public static Canvas CvRFID;//标签
        public static Canvas CvRouteLine;//直路线
        public static Canvas CvForkLine;//分叉路线
        public static Canvas CvOperate;//操作层

        //标签类
        public class RFID : SaveMap.ShapesBase
        {
            /// <summary>
            /// 编号
            /// </summary>
            [Category("Non-Numeric Editors")]
            [Description("This property is a complex property and has no default editor.")]
            private int num;
            public int Num {
                get { return num; }
                set {
                    num = value;
                    textBlock.Text = value.ToString();
                    //这里如果是创建时触发，会出错，所以加入try catch
                    try
                    {
                    //获取文本的尺寸
                    Size size = CavnvasBase.GetTextBlockSize(textBlock);
                    textBlock.Margin = new Thickness(
                        ellipse.Margin.Left + ellipse.Height / 2 - size.Width / 2,
                        ellipse.Margin.Top + ellipse.Height / 2 - size.Height / 2,
                        0, 0);
                    }
                    catch
                    {
                    }
                }
            }
            [Category("Non-Numeric Editors")]
            [Description("标签颜色")]
            public Color Color { get { return (Color)ColorConverter.ConvertFromString(ellipse.Fill.ToString());} set { ellipse.Fill = new SolidColorBrush(value); } }


            /// <summary>
            /// 绘图对象
            /// </summary>
            [JsonIgnore]
            public Ellipse ellipse = new Ellipse();
            public BaseEllipse baseEllipse = new BaseEllipse();
            /// <summary>
            /// 绘文字对象
            /// </summary>
            [JsonIgnore]
            [Category("Non-Numeric Editors")]
            [Description("This property is a complex property and has no default editor.")]
            public TextBlock textBlock = new TextBlock();
            public BaseTextBlock baseTextBlock = new BaseTextBlock();
            /// <summary>
            /// 选中框
            /// </summary>
            [JsonIgnore]
            public Rectangle SelectRectangle = new Rectangle();
        }
        //直线
        public class RouteLine : SaveMap.ShapesBase
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int Num = 0;
            /// <summary>
            /// 线条对象
            /// </summary>
            [JsonIgnore]
            public Line line = new Line();
            public BaseLine baseLine = new BaseLine();
            /// <summary>
            /// 起点矩形编辑器
            /// </summary>
            [JsonIgnore]
            public Rectangle StartRect = new Rectangle();
            public BaseRectangle baseStartRect = new BaseRectangle();
            /// <summary>
            /// 终点矩形编辑器
            /// </summary>
            [JsonIgnore]
            public Rectangle EndRect = new Rectangle();
            public BaseRectangle baseEndRect = new BaseRectangle();
            /// <summary>
            /// 选中提示线虚线
            /// </summary>
            [JsonIgnore]
            public Line SelectLine = new Line();
            public BaseLine baseSelectLine = new BaseLine();
            /// <summary>
            /// 编号显示文本
            /// </summary>
            [JsonIgnore]
            public TextBlock textBlock = new TextBlock();
            public BaseTextBlock baseTextBlock = new BaseTextBlock();
        }
        //分叉线
        public class RouteForkLine : SaveMap.ShapesBase
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int Num = 0;
            /// <summary>
            /// 路径
            /// </summary>
            [JsonIgnore]
            public Path Path = new Path();
            public BaseForkLiePath basePath = new BaseForkLiePath();
            /// <summary>
            /// 起点矩形编辑器
            /// </summary>
            [JsonIgnore]
            public Rectangle StartRect = new Rectangle();
            public BaseRectangle baseStartRect = new BaseRectangle();
            /// <summary>
            /// 终点矩形编辑器
            /// </summary>
            [JsonIgnore]
            public Rectangle EndRect = new Rectangle();
            public BaseRectangle baseEndRect = new BaseRectangle();
            /// <summary>
            /// 选中提示虚线【路径】
            /// </summary>
            [JsonIgnore]
            public Path SelectPath = new Path();
            public BaseForkLiePath baseSelectPath = new BaseForkLiePath();
            ///// <summary>
            ///// 选中提示路径虚线【几何路径】
            ///// </summary>
            //public PathGeometry SelectPathGeometry = new PathGeometry();
            //// <summary>
            ///// 选中提示路径虚线【路径图】
            ///// </summary>
            //public PathFigure SelectFigure = new PathFigure();
            /// <summary>
            /// 编号显示文本
            /// </summary>
            [JsonIgnore]
            public TextBlock textBlock = new TextBlock();
            public BaseTextBlock baseTextBlock = new BaseTextBlock();
        }

        /// <summary>
        /// 地图对象
        /// </summary>
        public class MapObjectClass
        {
            /// <summary>
            /// 标签绘图列表
            /// </summary>
            public List<MapElement.RFID> RFIDS = new List<MapElement.RFID>();
            /// <summary>
            /// 直线集合
            /// </summary>
            public List<MapElement.RouteLine> Lines = new List<MapElement.RouteLine>();
            /// <summary>
            /// 分叉线集合
            /// </summary>
            public List<MapElement.RouteForkLine> ForkLines = new List<MapElement.RouteForkLine>();
        }
        public static MapObjectClass MapObject = new MapObjectClass();


        /*-------背景栅格---------------*/
        /// <summary>
        /// 绘制画布栅格
        /// </summary>
        /// <param name="width">画布宽度</param>
        /// <param name="height">画布高度</param>
        /// <param name="canvas">画布对象</param>
        public static void DrawGrid(int width, int height)
        {
            //横线数量
            int HorizontalNum = height / MapElement.GridSize;
            //竖线数量
            int VerticalNum = width / MapElement.GridSize;

            //画横线
            for (int i = 0; i < HorizontalNum; i++)
            {
                Line line = new Line();
                Point pointStart = new Point(0, i * MapElement.GridSize);
                Point pointEnd = new Point(width, i * MapElement.GridSize);

                //画方向线
                line.X1 = pointStart.X;
                line.Y1 = pointStart.Y;
                line.X2 = pointEnd.X;
                line.Y2 = pointEnd.Y;
                if (i % 2 == 0)
                    line.StrokeThickness = 0.4;
                else
                {
                    //虚线
                    // line.StrokeDashArray = new DoubleCollection() { 20};
                    line.StrokeThickness = 0.2;
                }
                line.Stroke = System.Windows.Media.Brushes.LightGray;
                MapElement.CvGrid.Children.Add(line);
            }
            //画竖线
            for (int i = 0; i < VerticalNum + 1; i++)
            {
                Line line = new Line();
                Point pointStart = new Point(i * MapElement.GridSize, 0);
                Point pointEnd = new Point(i * MapElement.GridSize, height);

                //画方向线
                line.X1 = pointStart.X;
                line.Y1 = pointStart.Y;
                line.X2 = pointEnd.X;
                line.Y2 = pointEnd.Y;
                if (i % 2 == 0)
                    line.StrokeThickness = 0.4;
                else
                {
                    //虚线
                    //line.StrokeDashArray = new DoubleCollection() { 20 };
                    line.StrokeThickness = 0.2;
                }
                line.Stroke = System.Windows.Media.Brushes.LightGray;
                MapElement.CvGrid.Children.Add(line);
            }
        }

        /*-------RFID---------------*/
        /// <summary>
        /// 绘制单个RFID
        /// </summary>
        public static void ShowRFID(MapElement.RFID rfid)
        {
            MapElement.CvRFID.Children.Add(rfid.ellipse);
            MapElement.CvRFID.Children.Add(rfid.textBlock);
        }
        /// <summary>
        /// 绘制RFID列表
        /// </summary>
        public static void DrawRFIDList()
        {
            foreach (var item in MapObject.RFIDS)
                ShowRFID(item);
        }
        /// <summary>
        /// 添加一个新的RFID并显示
        /// </summary>
        /// <param name="canvas">画布对象</param>
        /// <returns>返回该RFID的索引</returns>
        public static int AddRFIDAndShow()
        {
            /*-------添加对象-------------------*/
            //定义RFID对象
            MapElement.RFID rfid = new MapElement.RFID();
            //设置编号【没有元素从1开始，否则按最后一个的编号加1】
            if (MapElement.MapObject.RFIDS.Count == 0)
                rfid.Num = MapElement.MapObject.RFIDS.Count + 1;
            else
                rfid.Num = MapElement.MapObject.RFIDS.Last().Num + 1;
            //添加RFID到列表
            MapElement.MapObject.RFIDS.Add(rfid);
            /*-------绘制到界面-------------------*/
            //半径
            float Radius = MapElement.RFID_Radius;
            //当前光标位置
            Point point = MapOperate.NowPoint;
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            Thickness thickness = new Thickness();
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //绘制
            rfid.ellipse.Height = Radius * 2;
            rfid.ellipse.Width = Radius * 2;
            rfid.ellipse.Margin = thickness;
            rfid.ellipse.StrokeThickness = 0.5;
            rfid.ellipse.Fill = CavnvasBase.GetSolid(200, Colors.Green);
            rfid.ellipse.Stroke = CavnvasBase.GetSolid(100, Colors.Green);
            MapElement.CvRFID.Children.Add(rfid.ellipse);
            //显示编号
            CavnvasBase.DrawText(
                rfid.ellipse.Margin.Left + Radius,
                rfid.ellipse.Margin.Top + Radius,
                rfid.Num.ToString(),
                Colors.Black,
                MapElement.CvRFID,
                rfid.textBlock
                );
            /*-------设置该RFID为当前正在操作的RFID-------------------*/
            return MapElement.MapObject.RFIDS.Count - 1;
        }

        /*-------路径直线---------------*/
        /// <summary>
        /// 绘制一条直线
        /// </summary>
        public static void DrawRouteLine(int index, Point StartPoint)
        {
            StartPoint.X -= MapElement.GridSize / 2;
            StartPoint.Y -= MapElement.GridSize / 2;
            double diff_x = StartPoint.X - StartPoint.X % MapElement.GridSize + MapElement.GridSize;
            double diff_y = StartPoint.Y - StartPoint.Y % MapElement.GridSize + MapElement.GridSize;

            //绘制
            MapElement.MapObject.Lines[index].line.Stroke = Brushes.Black;
            MapElement.MapObject.Lines[index].line.StrokeThickness = 3;//线的宽度
            MapElement.MapObject.Lines[index].line.Margin = new Thickness(diff_x, diff_y, 0, 0);

            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.Lines[index].line);

            ////显示编号
            //CavnvasBase.DrawText(
            //    MapRFIDList[index].ellipse.Margin.Left + 15,
            //    MapRFIDList[index].ellipse.Margin.Top + 10,
            //    MapRFIDList[index].Num.ToString(),
            //    Colors.Black,
            //    MapElement.CvRouteLine,
            //    MapRFIDList[index].textBlock
            //    );
        }
        /// <summary>
        /// 绘制一条直线
        /// </summary>
        public static void DrawRouteLine(int index)
        {
            //绘制
            MapElement.MapObject.Lines[index].line.Stroke = Brushes.Black;
            MapElement.MapObject.Lines[index].line.StrokeThickness = 3;//线的宽度
            //MapElement.MapObject.MapLineList[index].line.HorizontalAlignment = HorizontalAlignment.Left;
            //MapElement.MapObject.MapLineList[index].line.VerticalAlignment = VerticalAlignment.Center;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.Lines[index].line);

            ////显示编号
            //CavnvasBase.DrawText(
            //    MapRFIDList[index].ellipse.Margin.Left + 15,
            //    MapRFIDList[index].ellipse.Margin.Top + 10,
            //    MapRFIDList[index].Num.ToString(),
            //    Colors.Black,
            //    MapElement.CvRouteLine,
            //    MapRFIDList[index].textBlock
            //    );
        }
        /// <summary>
        /// 绘制直线列表
        /// </summary>
        public static void DrawRouteLineList()
        {
            if (MapObject.Lines.Count == 0)
                return;
            for (int i = 0; i < MapObject.Lines.Count; i++)
            {
                DrawRouteLine(i);
            }
        }
        /// <summary>
        /// 添加一条直线
        /// </summary>
        /// <returns>返回索引</returns>
        public static int AddRouteLine()
        {
            //添加直线
            MapElement.RouteLine line = new MapElement.RouteLine();
            line.Num = MapElement.MapObject.Lines.Count + 1;
            MapElement.MapObject.Lines.Add(line);
            //返回索引
            return MapElement.MapObject.Lines.Count - 1;
        }
        public static void ShowLine(MapElement.RouteLine line)
        {
            if (MapElement.CvRouteLine.Children.Contains(line.line))
                return;
            MapElement.CvRouteLine.Children.Add(line.line);
        }
        /// <summary>
        /// 显示起点编辑器
        /// </summary>
        public static void RouteLineShowStart(int index)
        {
            MapElement.MapObject.Lines[index].StartRect.Fill = CavnvasBase.GetSolid(100, Colors.Green);//半透明
            MapElement.MapObject.Lines[index].StartRect.StrokeThickness = 0.5;
            MapElement.MapObject.Lines[index].StartRect.Stroke = Brushes.Gray;
            MapElement.MapObject.Lines[index].StartRect.Width = MapElement.GridSize;
            MapElement.MapObject.Lines[index].StartRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.Lines[index].StartRect);
        }
        public static void RouteLineShowStart(MapElement.RouteLine routeLine)
        {
            routeLine.StartRect.Fill = CavnvasBase.GetSolid(100, Colors.Green);//半透明
            routeLine.StartRect.StrokeThickness = 0.5;
            routeLine.StartRect.Stroke = Brushes.Gray;
            routeLine.StartRect.Width = MapElement.GridSize;
            routeLine.StartRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(routeLine.StartRect);
        }
        /// <summary>
        /// 显示终点编辑器
        /// </summary>
        public static void RouteLineShowEnd(int index)
        {
            MapElement.MapObject.Lines[index].EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Green); //半透明
            MapElement.MapObject.Lines[index].EndRect.StrokeThickness = 0.5;
            MapElement.MapObject.Lines[index].EndRect.Stroke = Brushes.Gray;
            MapElement.MapObject.Lines[index].EndRect.Width = MapElement.GridSize;
            MapElement.MapObject.Lines[index].EndRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.Lines[index].EndRect);
        }
        public static void RouteLineShowEnd(MapElement.RouteLine routeLinex)
        {
            routeLinex.EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Green); //半透明
            routeLinex.EndRect.StrokeThickness = 0.5;
            routeLinex.EndRect.Stroke = Brushes.Gray;
            routeLinex.EndRect.Width = MapElement.GridSize;
            routeLinex.EndRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(routeLinex.EndRect);
        }
        /// <summary>
        /// 显示选择状态线
        /// </summary>
        public static void RouteLineShowSelectLine(int index)
        {
            MapElement.MapObject.Lines[index].SelectLine.Stroke = Brushes.Yellow;
            MapElement.MapObject.Lines[index].SelectLine.X1 = MapElement.MapObject.Lines[index].line.X1;
            MapElement.MapObject.Lines[index].SelectLine.X2 = MapElement.MapObject.Lines[index].line.X2;
            MapElement.MapObject.Lines[index].SelectLine.Y1 = MapElement.MapObject.Lines[index].line.Y1;
            MapElement.MapObject.Lines[index].SelectLine.Y2 = MapElement.MapObject.Lines[index].line.Y2;
            MapElement.MapObject.Lines[index].SelectLine.Margin = MapElement.MapObject.Lines[index].line.Margin;
            MapElement.MapObject.Lines[index].SelectLine.StrokeThickness = 1;//线的宽度
            //虚线
            MapElement.MapObject.Lines[index].SelectLine.StrokeDashArray = new DoubleCollection() { 3, 5 };
            //MapElement.MapObject.MapRFIDList[index].selectRectangle.StrokeDashCap = PenLineCap.Triangle;
            //MapElement.MapObject.MapLineList[index].SelectLine.HorizontalAlignment = HorizontalAlignment.Left;
            //MapElement.MapObject.MapLineList[index].SelectLine.VerticalAlignment = VerticalAlignment.Center;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.Lines[index].SelectLine);
        }
        public static void RouteLineShowSelectLine(MapElement.RouteLine routeLine)
        {
            routeLine.SelectLine.Stroke = Brushes.Yellow;
            routeLine.SelectLine.X1 = routeLine.line.X1;
            routeLine.SelectLine.X2 = routeLine.line.X2;
            routeLine.SelectLine.Y1 = routeLine.line.Y1;
            routeLine.SelectLine.Y2 = routeLine.line.Y2;
            routeLine.SelectLine.Margin = routeLine.line.Margin;
            routeLine.SelectLine.StrokeThickness = 1;//线的宽度
            //虚线
            routeLine.SelectLine.StrokeDashArray = new DoubleCollection() { 3, 5 };
            //MapElement.MapObject.MapRFIDList[index].selectRectangle.StrokeDashCap = PenLineCap.Triangle;
            //MapElement.MapObject.MapLineList[index].SelectLine.HorizontalAlignment = HorizontalAlignment.Left;
            //MapElement.MapObject.MapLineList[index].SelectLine.VerticalAlignment = VerticalAlignment.Center;
            MapElement.CvRouteLine.Children.Add(routeLine.SelectLine);
        }
        /// <summary>
        /// 绘制直线列表
        /// </summary>
        public static void DrawLineList()
        {
            if (MapObject.Lines.Count == 0)
                return;
            for (int i = 0; i < MapObject.Lines.Count; i++)
            {
                DrawRouteLine(i);
            }
        }
        /*-------路径分叉线---------------*/
        /// <summary>
        /// 绘制分叉【圆弧】
        /// </summary>
        public static void DrawForkLine(int index, Point StartPoint)
        {
            StartPoint.X -= MapElement.GridSize / 2;
            StartPoint.Y -= MapElement.GridSize / 2;
            double diff_x = StartPoint.X - StartPoint.X % MapElement.GridSize + MapElement.GridSize;
            double diff_y = StartPoint.Y - StartPoint.Y % MapElement.GridSize + MapElement.GridSize;

            //定义圆弧半径
            int radius = MapElement.ForkLineArc_Radius;
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure figure = new PathFigure();
            //路径图起点坐标
            figure.StartPoint = new Point();
            ArcSegment arc = new ArcSegment(
               new Point(figure.StartPoint.X + radius, figure.StartPoint.Y + radius),
               new Size(radius, radius),
               0,
               false,
               SweepDirection.Counterclockwise,//逆时针
               true);
            figure.Segments.Add(arc);
            pathGeometry.Figures.Add(figure);
            MapElement.MapObject.ForkLines[index].Path.Data = pathGeometry;
            MapElement.MapObject.ForkLines[index].Path.Stroke = Brushes.Black;
            MapElement.MapObject.ForkLines[index].Path.StrokeThickness = 3;

            MapElement.MapObject.ForkLines[index].Path.Margin = new Thickness(diff_x, diff_y, 0, 0);
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.ForkLines[index].Path);
        }
        /// <summary>
        /// 绘制单个
        /// </summary>
        public static void ShowForkLine(MapElement.RouteForkLine forkLine)
        {
            MapElement.CvForkLine.Children.Add(forkLine.Path);
        }
        /// <summary>
        /// 显示起点编辑器
        /// </summary>
        public static void ForkLineShowStart(int index)
        {
            MapElement.MapObject.ForkLines[index].StartRect.Fill = CavnvasBase.GetSolid(100, Colors.Pink);//半透明
            MapElement.MapObject.ForkLines[index].StartRect.StrokeThickness = 0.5;
            MapElement.MapObject.ForkLines[index].StartRect.Stroke = Brushes.Gray;
            MapElement.MapObject.ForkLines[index].StartRect.Width = MapElement.GridSize;
            MapElement.MapObject.ForkLines[index].StartRect.Height = MapElement.GridSize;
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.ForkLines[index].StartRect);
        }
        /// <summary>
        /// 显示终点编辑器
        /// </summary>
        public static void ForkLineShowEnd(int index)
        {
            //找到圆弧起点和终点坐标
            PathGeometry pathGeometry = MapElement.MapObject.ForkLines[index].Path.Data as PathGeometry;
            PathFigure figure = pathGeometry.Figures.First();
            ArcSegment arc = figure.Segments.First() as ArcSegment;
            //圆弧终点
            Point end = arc.Point;
            end.X += MapElement.MapObject.ForkLines[index].Path.Margin.Left - MapElement.GridSize / 2;
            end.Y += MapElement.MapObject.ForkLines[index].Path.Margin.Top - MapElement.GridSize / 2;
            //终点编辑器的位置和圆弧终点坐标同步
            MapElement.MapObject.ForkLines[index].EndRect.Margin = new Thickness(end.X, end.Y, 0, 0);

            MapElement.MapObject.ForkLines[index].EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Pink); //半透明
            MapElement.MapObject.ForkLines[index].EndRect.StrokeThickness = 0.5;
            MapElement.MapObject.ForkLines[index].EndRect.Stroke = Brushes.Gray;
            MapElement.MapObject.ForkLines[index].EndRect.Width = MapElement.GridSize;
            MapElement.MapObject.ForkLines[index].EndRect.Height = MapElement.GridSize;
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.ForkLines[index].EndRect);
        }
        public static void ForkLineShowEnd(MapElement.RouteForkLine routeForkLine)
        {
            //找到圆弧起点和终点坐标
            PathGeometry pathGeometry = routeForkLine.Path.Data as PathGeometry;
            PathFigure figure = pathGeometry.Figures.First();
            ArcSegment arc = figure.Segments.First() as ArcSegment;
            //圆弧终点
            Point end = arc.Point;
            end.X += routeForkLine.Path.Margin.Left - MapElement.GridSize / 2;
            end.Y += routeForkLine.Path.Margin.Top - MapElement.GridSize / 2;
            //终点编辑器的位置和圆弧终点坐标同步
            routeForkLine.EndRect.Margin = new Thickness(end.X, end.Y, 0, 0);

            routeForkLine.EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Pink); //半透明
            routeForkLine.EndRect.StrokeThickness = 0.5;
            routeForkLine.EndRect.Stroke = Brushes.Gray;
            routeForkLine.EndRect.Width = MapElement.GridSize;
            routeForkLine.EndRect.Height = MapElement.GridSize;
            MapElement.CvForkLine.Children.Add(routeForkLine.EndRect);
        }
        /// <summary>
        /// 显示选择状
        /// </summary>
        public static void ForkLineShowSelect(int index)
        {
            //同步曲线
            MapElement.MapObject.ForkLines[index].SelectPath.Data = MapElement.MapObject.ForkLines[index].Path.Data.Clone();
            //设置为虚线
            MapElement.MapObject.ForkLines[index].SelectPath.StrokeDashArray = new DoubleCollection() { 3, 5 };
            //更改颜色
            MapElement.MapObject.ForkLines[index].SelectPath.Stroke = Brushes.Yellow;
            //同步位置
            MapElement.MapObject.ForkLines[index].SelectPath.Margin = MapElement.MapObject.ForkLines[index].Path.Margin;
            //设置线宽
            MapElement.MapObject.ForkLines[index].SelectPath.StrokeThickness = 1;
            //添加到画板
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.ForkLines[index].SelectPath);
        }
        public static void ForkLineShowSelect(MapElement.RouteForkLine routeForkLine)
        {
            //同步曲线
            routeForkLine.SelectPath.Data = routeForkLine.Path.Data.Clone();
            //设置为虚线
            routeForkLine.SelectPath.StrokeDashArray = new DoubleCollection() { 3, 5 };
            //更改颜色
            routeForkLine.SelectPath.Stroke = Brushes.Yellow;
            //同步位置
            routeForkLine.SelectPath.Margin = routeForkLine.Path.Margin;
            //设置线宽
            routeForkLine.SelectPath.StrokeThickness = 1;
            //添加到画板
            MapElement.CvForkLine.Children.Add(routeForkLine.SelectPath);
        }
        /// <summary>
        /// 绘制列表
        /// </summary>
        public static void DrawForkLineList()
        {
            foreach (var item in MapObject.ForkLines)
                ShowForkLine(item);
        }
        /// <summary>
        /// 添加一个并显示
        /// </summary>
        /// <param name="canvas">画布对象</param>
        /// <returns>返回该RFID的索引</returns>
        public static int AddForkLine()
        {
            //添加
            MapElement.RouteForkLine line = new MapElement.RouteForkLine();
            line.Num = MapElement.MapObject.ForkLines.Count + 1;
            MapElement.MapObject.ForkLines.Add(line);
            //返回索引
            return MapElement.MapObject.ForkLines.Count - 1;
        }
    }
}
