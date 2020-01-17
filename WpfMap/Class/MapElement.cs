using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            public int Num = 0;
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
            public TextBlock textBlock = new TextBlock();
            public BaseTextBlock baseTextBlock = new BaseTextBlock();
            /// <summary>
            /// 选中框
            /// </summary>
            [JsonIgnore]
            public Rectangle SelectRectangle = new Rectangle();
            public BaseRectangle baseSelectRectangle = new BaseRectangle();
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
            public List<MapElement.RFID> MapRFIDList = new List<MapElement.RFID>();
            /// <summary>
            /// 直线集合
            /// </summary>
            public List<MapElement.RouteLine> MapLineList = new List<MapElement.RouteLine>();

            /// <summary>
            /// 分叉线集合
            /// </summary>
            public List<MapElement.RouteForkLine> MapForkLineList = new List<MapElement.RouteForkLine>();
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
                    line.StrokeThickness = 0.2;
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
                    line.StrokeThickness = 0.2;
                line.Stroke = System.Windows.Media.Brushes.LightGray;
                MapElement.CvGrid.Children.Add(line);
            }
        }

        /*-------RFID---------------*/
        /// <summary>
        /// 绘制单个RFID
        /// </summary>
        public static void DrawRFID(int index)
        {
            //站点
            float Radius = MapElement.RFID_Radius;
            //绘制
            MapObject.MapRFIDList[index].ellipse.Height = Radius * 2;
            MapObject.MapRFIDList[index].ellipse.Width = Radius * 2;
            //item.ellipse.Margin = new Thickness(item.point.X - Radius, item.point.Y - Radius, 0, 0);
            MapObject.MapRFIDList[index].ellipse.StrokeThickness = 1;
            MapObject.MapRFIDList[index].ellipse.Fill = System.Windows.Media.Brushes.Yellow;
            MapObject.MapRFIDList[index].ellipse.Stroke = System.Windows.Media.Brushes.Gray;
            MapElement.CvRFID.Children.Add(MapObject.MapRFIDList[index].ellipse);
            //显示编号
            CavnvasBase.DrawText(
              MapObject.MapRFIDList[index].ellipse.Margin.Left + 15,
               MapObject.MapRFIDList[index].ellipse.Margin.Top + 10,
              MapObject.MapRFIDList[index].Num.ToString(),
                Colors.Black,
                MapElement.CvRFID,
              MapObject.MapRFIDList[index].textBlock
                );
        }

        /// <summary>
        /// 绘制RFID列表
        /// </summary>
        public static void DrawRFIDList()
        {
            if (MapObject.MapRFIDList.Count == 0)
                return;
            for (int i = 0; i < MapObject.MapRFIDList.Count; i++)
            {
                DrawRFID(i);
            }
        }

        /// <summary>
        /// 添加一个RFID 并显示
        /// </summary>
        /// <param name="canvas">画布对象</param>
        /// <returns>返回该RFID的索引</returns>
        public static int AddRFIDAndShow()
        {
            //添加一个RFID
            MapElement.RFID rfid = new MapElement.RFID();
            rfid.Num = MapElement.MapObject.MapRFIDList.Count + 1;
            MapElement.MapObject.MapRFIDList.Add(rfid);
            //绘制到界面
            MapElement.DrawRFID(MapElement.MapObject.MapRFIDList.Count - 1);
            //设置该RFID为当前正在操作的RFID
            return MapElement.MapObject.MapRFIDList.Count - 1;
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
            MapElement.MapObject.MapLineList[index].line.Stroke = Brushes.Black;
            MapElement.MapObject.MapLineList[index].line.StrokeThickness = 3;//线的宽度
            MapElement.MapObject.MapLineList[index].line.Margin = new Thickness(diff_x, diff_y, 0, 0);

            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.MapLineList[index].line);

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
            MapElement.MapObject.MapLineList[index].line.Stroke = Brushes.Black;
            MapElement.MapObject.MapLineList[index].line.StrokeThickness = 3;//线的宽度
            //MapElement.MapObject.MapLineList[index].line.HorizontalAlignment = HorizontalAlignment.Left;
            //MapElement.MapObject.MapLineList[index].line.VerticalAlignment = VerticalAlignment.Center;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.MapLineList[index].line);

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
            if (MapObject.MapLineList.Count == 0)
                return;
            for (int i = 0; i < MapObject.MapLineList.Count; i++)
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
            line.Num = MapElement.MapObject.MapLineList.Count + 1;
            MapElement.MapObject.MapLineList.Add(line);
            //返回索引
            return MapElement.MapObject.MapLineList.Count - 1;
        }
        /// <summary>
        /// 显示起点编辑器
        /// </summary>
        public static void RouteLineShowStart(int index)
        {
            MapElement.MapObject.MapLineList[index].StartRect.Fill = CavnvasBase.GetSolid(100, Colors.Green);//半透明
            MapElement.MapObject.MapLineList[index].StartRect.StrokeThickness = 0.5;
            MapElement.MapObject.MapLineList[index].StartRect.Stroke = Brushes.Gray;
            MapElement.MapObject.MapLineList[index].StartRect.Width = MapElement.GridSize;
            MapElement.MapObject.MapLineList[index].StartRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.MapLineList[index].StartRect);
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
            MapElement.MapObject.MapLineList[index].EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Green); //半透明
            MapElement.MapObject.MapLineList[index].EndRect.StrokeThickness = 0.5;
            MapElement.MapObject.MapLineList[index].EndRect.Stroke = Brushes.Gray;
            MapElement.MapObject.MapLineList[index].EndRect.Width = MapElement.GridSize;
            MapElement.MapObject.MapLineList[index].EndRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.MapLineList[index].EndRect);
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
            MapElement.MapObject.MapLineList[index].SelectLine.Stroke = Brushes.Yellow;
            MapElement.MapObject.MapLineList[index].SelectLine.X1 = MapElement.MapObject.MapLineList[index].line.X1;
            MapElement.MapObject.MapLineList[index].SelectLine.X2 = MapElement.MapObject.MapLineList[index].line.X2;
            MapElement.MapObject.MapLineList[index].SelectLine.Y1 = MapElement.MapObject.MapLineList[index].line.Y1;
            MapElement.MapObject.MapLineList[index].SelectLine.Y2 = MapElement.MapObject.MapLineList[index].line.Y2;
            MapElement.MapObject.MapLineList[index].SelectLine.Margin = MapElement.MapObject.MapLineList[index].line.Margin;
            MapElement.MapObject.MapLineList[index].SelectLine.StrokeThickness = 1;//线的宽度
            //虚线
            MapElement.MapObject.MapLineList[index].SelectLine.StrokeDashArray = new DoubleCollection() { 3, 5 };
            //MapElement.MapObject.MapRFIDList[index].selectRectangle.StrokeDashCap = PenLineCap.Triangle;
            //MapElement.MapObject.MapLineList[index].SelectLine.HorizontalAlignment = HorizontalAlignment.Left;
            //MapElement.MapObject.MapLineList[index].SelectLine.VerticalAlignment = VerticalAlignment.Center;
            MapElement.CvRouteLine.Children.Add(MapElement.MapObject.MapLineList[index].SelectLine);
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
            if (MapObject.MapLineList.Count == 0)
                return;
            for (int i = 0; i < MapObject.MapLineList.Count; i++)
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
            MapElement.MapObject.MapForkLineList[index].Path.Data = pathGeometry;
            MapElement.MapObject.MapForkLineList[index].Path.Stroke = Brushes.Black;
            MapElement.MapObject.MapForkLineList[index].Path.StrokeThickness = 3;

            MapElement.MapObject.MapForkLineList[index].Path.Margin = new Thickness(diff_x, diff_y, 0, 0);
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.MapForkLineList[index].Path);
        }
        /// <summary>
        /// 绘制单个
        /// </summary>
        public static void DrawForkLine(int index)
        {
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.MapForkLineList[index].Path);
        }
        /// <summary>
        /// 显示起点编辑器
        /// </summary>
        public static void ForkLineShowStart(int index)
        {
            MapElement.MapObject.MapForkLineList[index].StartRect.Fill = CavnvasBase.GetSolid(100, Colors.Pink);//半透明
            MapElement.MapObject.MapForkLineList[index].StartRect.StrokeThickness = 0.5;
            MapElement.MapObject.MapForkLineList[index].StartRect.Stroke = Brushes.Gray;
            MapElement.MapObject.MapForkLineList[index].StartRect.Width = MapElement.GridSize;
            MapElement.MapObject.MapForkLineList[index].StartRect.Height = MapElement.GridSize;
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.MapForkLineList[index].StartRect);
        }
        /// <summary>
        /// 显示终点编辑器
        /// </summary>
        public static void ForkLineShowEnd(int index)
        {
            //找到圆弧起点和终点坐标
            PathGeometry pathGeometry = MapElement.MapObject.MapForkLineList[index].Path.Data as PathGeometry;
            PathFigure figure = pathGeometry.Figures.First();
            ArcSegment arc = figure.Segments.First() as ArcSegment;
            //圆弧终点
            Point end = arc.Point;
            end.X += MapElement.MapObject.MapForkLineList[index].Path.Margin.Left - MapElement.GridSize / 2;
            end.Y += MapElement.MapObject.MapForkLineList[index].Path.Margin.Top - MapElement.GridSize / 2;
            //终点编辑器的位置和圆弧终点坐标同步
            MapElement.MapObject.MapForkLineList[index].EndRect.Margin = new Thickness(end.X, end.Y, 0, 0);

            MapElement.MapObject.MapForkLineList[index].EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Pink); //半透明
            MapElement.MapObject.MapForkLineList[index].EndRect.StrokeThickness = 0.5;
            MapElement.MapObject.MapForkLineList[index].EndRect.Stroke = Brushes.Gray;
            MapElement.MapObject.MapForkLineList[index].EndRect.Width = MapElement.GridSize;
            MapElement.MapObject.MapForkLineList[index].EndRect.Height = MapElement.GridSize;
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.MapForkLineList[index].EndRect);
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
            MapElement.MapObject.MapForkLineList[index].SelectPath.Data = MapElement.MapObject.MapForkLineList[index].Path.Data.Clone();
            //设置为虚线
            MapElement.MapObject.MapForkLineList[index].SelectPath.StrokeDashArray = new DoubleCollection() { 3, 5 };
            //更改颜色
            MapElement.MapObject.MapForkLineList[index].SelectPath.Stroke = Brushes.Yellow;
            //同步位置
            MapElement.MapObject.MapForkLineList[index].SelectPath.Margin = MapElement.MapObject.MapForkLineList[index].Path.Margin;
            //设置线宽
            MapElement.MapObject.MapForkLineList[index].SelectPath.StrokeThickness = 1;
            //添加到画板
            MapElement.CvForkLine.Children.Add(MapElement.MapObject.MapForkLineList[index].SelectPath);
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
            if (MapObject.MapForkLineList.Count == 0)
                return;
            for (int i = 0; i < MapObject.MapForkLineList.Count; i++)
            {
                DrawForkLine(i);
            }
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
            line.Num = MapElement.MapObject.MapForkLineList.Count + 1;
            MapElement.MapObject.MapForkLineList.Add(line);
            //返回索引
            return MapElement.MapObject.MapForkLineList.Count - 1;
        }
    }
}
