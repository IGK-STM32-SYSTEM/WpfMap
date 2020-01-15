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
        public static int GridSize = 20;
        //定义画布对象
        public static Canvas CvGrid;//栅格
        public static Canvas CvRFID;//标签
        public static Canvas CvRouteLine;//直路线
        public static Canvas CvForkLine;//分叉路线

        //标签类
        public class RFID
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int Num = 0;
            /// <summary>
            /// 绘图对象
            /// </summary>
            public Ellipse ellipse = new Ellipse();
            /// <summary>
            /// 绘文字对象
            /// </summary>
            public TextBlock textBlock = new TextBlock();
            /// <summary>
            /// 选中框
            /// </summary>
            public Rectangle selectRectangle = new Rectangle();
        }
        /// <summary>
        /// 标签绘图列表
        /// </summary>
        public static List<MapElement.RFID> MapRFIDList = new List<MapElement.RFID>();
        //直线
        public class RouteLine
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int Num = 0;
            /// <summary>
            /// 线条对象
            /// </summary>
            public Line line = new Line();
            /// <summary>
            /// 起点矩形编辑器
            /// </summary>
            public Rectangle StartRect = new Rectangle();
            /// <summary>
            /// 终点矩形编辑器
            /// </summary>
            public Rectangle EndRect = new Rectangle();
            /// <summary>
            /// 选中提示线虚线
            /// </summary>
            public Line SelectLine = new Line();
            /// <summary>
            /// 编号显示文本
            /// </summary>
            public TextBlock textBlock = new TextBlock();
        }
        /// <summary>
        /// 直线集合
        /// </summary>
        public static List<MapElement.RouteLine> MapLineList = new List<MapElement.RouteLine>();
        //分叉线
        public class RouteForkLine
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int Num = 0;
            /// <summary>
            /// 路径
            /// </summary>
            public Path Path = new Path();
            ///// <summary>
            ///// 几何路径
            ///// </summary>
            //public PathGeometry PathGeometry = new PathGeometry();
            //// <summary>
            ///// 路径图
            ///// </summary>
            //public PathFigure Figure = new PathFigure();
            /// <summary>
            /// 起点矩形编辑器
            /// </summary>
            public Rectangle StartRect = new Rectangle();
            /// <summary>
            /// 终点矩形编辑器
            /// </summary>
            public Rectangle EndRect = new Rectangle();
            /// <summary>
            /// 选中提示虚线【路径】
            /// </summary>
            public Path SelectPath = new Path();
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
            public TextBlock textBlock = new TextBlock();
        }

        /// <summary>
        /// 分叉线集合
        /// </summary>
        public static List<MapElement.RouteForkLine> MapForkLineList = new List<MapElement.RouteForkLine>();

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
            MapRFIDList[index].ellipse.Height = Radius * 2;
            MapRFIDList[index].ellipse.Width = Radius * 2;
            //item.ellipse.Margin = new Thickness(item.point.X - Radius, item.point.Y - Radius, 0, 0);
            MapRFIDList[index].ellipse.StrokeThickness = 1;
            MapRFIDList[index].ellipse.Fill = System.Windows.Media.Brushes.Yellow;
            MapRFIDList[index].ellipse.Stroke = System.Windows.Media.Brushes.Gray;
            MapElement.CvRFID.Children.Add(MapRFIDList[index].ellipse);
            //显示编号
            CavnvasBase.DrawText(
                MapRFIDList[index].ellipse.Margin.Left + 15,
                MapRFIDList[index].ellipse.Margin.Top + 10,
                MapRFIDList[index].Num.ToString(),
                Colors.Black,
                MapElement.CvRFID,
                MapRFIDList[index].textBlock
                );
        }

        /// <summary>
        /// 绘制RFID列表
        /// </summary>
        public static void DrawRFIDList()
        {
            if (MapRFIDList.Count == 0)
                return;
            for (int i = 0; i < MapRFIDList.Count; i++)
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
            rfid.Num = MapElement.MapRFIDList.Count + 1;
            MapElement.MapRFIDList.Add(rfid);
            //绘制到界面
            MapElement.DrawRFID(MapElement.MapRFIDList.Count - 1);
            //设置该RFID为当前正在操作的RFID
            return MapElement.MapRFIDList.Count - 1;
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
            MapElement.MapLineList[index].line.Stroke = Brushes.Black;
            MapElement.MapLineList[index].line.StrokeThickness = 3;//线的宽度
            MapElement.MapLineList[index].line.Margin = new Thickness(diff_x, diff_y, 0, 0);

            MapElement.CvRouteLine.Children.Add(MapElement.MapLineList[index].line);

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
            MapElement.MapLineList[index].line.Stroke = Brushes.Black;
            MapElement.MapLineList[index].line.X1 = 0;
            MapElement.MapLineList[index].line.X2 = 0;
            MapElement.MapLineList[index].line.Y1 = 0;
            MapElement.MapLineList[index].line.Y2 = 0;
            MapElement.MapLineList[index].line.StrokeThickness = 3;//线的宽度
            //MapElement.MapLineList[index].line.HorizontalAlignment = HorizontalAlignment.Left;
            //MapElement.MapLineList[index].line.VerticalAlignment = VerticalAlignment.Center;
            MapElement.CvRouteLine.Children.Add(MapElement.MapLineList[index].line);

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
            if (MapLineList.Count == 0)
                return;
            for (int i = 0; i < MapLineList.Count; i++)
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
            line.Num = MapElement.MapLineList.Count + 1;
            MapElement.MapLineList.Add(line);
            //返回索引
            return MapElement.MapLineList.Count - 1;
        }
        /// <summary>
        /// 显示起点编辑器
        /// </summary>
        public static void RouteLineShowStart(int index)
        {
            MapElement.MapLineList[index].StartRect.Fill = CavnvasBase.GetSolid(100, Colors.Green);//半透明
            MapElement.MapLineList[index].StartRect.StrokeThickness = 0.5;
            MapElement.MapLineList[index].StartRect.Stroke = Brushes.Gray;
            MapElement.MapLineList[index].StartRect.Width = MapElement.GridSize;
            MapElement.MapLineList[index].StartRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(MapElement.MapLineList[index].StartRect);
        }
        /// <summary>
        /// 显示终点编辑器
        /// </summary>
        public static void RouteLineShowEnd(int index)
        {
            MapElement.MapLineList[index].EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Green); //半透明
            MapElement.MapLineList[index].EndRect.StrokeThickness = 0.5;
            MapElement.MapLineList[index].EndRect.Stroke = Brushes.Gray;
            MapElement.MapLineList[index].EndRect.Width = MapElement.GridSize;
            MapElement.MapLineList[index].EndRect.Height = MapElement.GridSize;
            MapElement.CvRouteLine.Children.Add(MapElement.MapLineList[index].EndRect);
        }
        /// <summary>
        /// 显示选择状态线
        /// </summary>
        public static void RouteLineShowSelectLine(int index)
        {
            MapElement.MapLineList[index].SelectLine.Stroke = Brushes.Yellow;
            MapElement.MapLineList[index].SelectLine.X1 = MapElement.MapLineList[index].line.X1;
            MapElement.MapLineList[index].SelectLine.X2 = MapElement.MapLineList[index].line.X2;
            MapElement.MapLineList[index].SelectLine.Y1 = MapElement.MapLineList[index].line.Y1;
            MapElement.MapLineList[index].SelectLine.Y2 = MapElement.MapLineList[index].line.Y2;
            MapElement.MapLineList[index].SelectLine.Margin = MapElement.MapLineList[index].line.Margin;
            MapElement.MapLineList[index].SelectLine.StrokeThickness = 1;//线的宽度
            //虚线
            MapElement.MapLineList[index].SelectLine.StrokeDashArray = new DoubleCollection() { 3, 5 };
            //MapElement.MapRFIDList[index].selectRectangle.StrokeDashCap = PenLineCap.Triangle;
            //MapElement.MapLineList[index].SelectLine.HorizontalAlignment = HorizontalAlignment.Left;
            //MapElement.MapLineList[index].SelectLine.VerticalAlignment = VerticalAlignment.Center;
            MapElement.CvRouteLine.Children.Add(MapElement.MapLineList[index].SelectLine);
        }

        /*-------路径分叉线---------------*/
        /// <summary>
        /// 绘制分叉
        /// </summary>
        public static void DrawForkLine(int index, Point StartPoint)
        {
            StartPoint.X -= MapElement.GridSize / 2;
            StartPoint.Y -= MapElement.GridSize / 2;
            double diff_x = StartPoint.X - StartPoint.X % MapElement.GridSize + MapElement.GridSize;
            double diff_y = StartPoint.Y - StartPoint.Y % MapElement.GridSize + MapElement.GridSize;

            ////绘制
            //MapElement.MapLineList[index].line.Stroke = Brushes.Black;
            //MapElement.MapLineList[index].line.StrokeThickness = 5;//线的宽度
            //MapElement.MapLineList[index].line.Margin = new Thickness(diff_x, diff_y, 0, 0);

            //MapElement.CvRouteLine.Children.Add(MapElement.MapLineList[index].line);

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
            MapElement.MapForkLineList[index].Path.Data = pathGeometry;
            MapElement.MapForkLineList[index].Path.Stroke = Brushes.Black;
            MapElement.MapForkLineList[index].Path.StrokeThickness = 3;

            MapElement.MapForkLineList[index].Path.Margin = new Thickness(diff_x, diff_y, 0, 0);
            MapElement.CvForkLine.Children.Add(MapElement.MapForkLineList[index].Path);
        }
        /// <summary>
        /// 绘制单个
        /// </summary>
        public static void DrawForkLine(int index)
        {
            ////站点
            //float Radius = 20;
            ////绘制
            //MapRFIDList[index].ellipse.Height = Radius * 2;
            //MapRFIDList[index].ellipse.Width = Radius * 2;
            ////item.ellipse.Margin = new Thickness(item.point.X - Radius, item.point.Y - Radius, 0, 0);
            //MapRFIDList[index].ellipse.StrokeThickness = 1;
            //MapRFIDList[index].ellipse.Fill = System.Windows.Media.Brushes.Yellow;
            //MapRFIDList[index].ellipse.Stroke = System.Windows.Media.Brushes.Gray;
            //MapElement.CvForkLine.Children.Add(MapRFIDList[index].ellipse);
            ////显示编号
            //CavnvasBase.DrawText(
            //    MapRFIDList[index].ellipse.Margin.Left + 15, 
            //    MapRFIDList[index].ellipse.Margin.Top + 10, 
            //    MapRFIDList[index].Num.ToString(), 
            //    Colors.Black, 
            //    MapElement.CvForkLine, 
            //    MapRFIDList[index].textBlock
            //    );
        }
        /// <summary>
        /// 显示起点编辑器
        /// </summary>
        public static void ForkLineShowStart(int index)
        {
            MapElement.MapForkLineList[index].StartRect.Fill = CavnvasBase.GetSolid(100, Colors.Pink);//半透明
            MapElement.MapForkLineList[index].StartRect.StrokeThickness = 0.5;
            MapElement.MapForkLineList[index].StartRect.Stroke = Brushes.Gray;
            MapElement.MapForkLineList[index].StartRect.Width = MapElement.GridSize;
            MapElement.MapForkLineList[index].StartRect.Height = MapElement.GridSize;
            MapElement.CvForkLine.Children.Add(MapElement.MapForkLineList[index].StartRect);
        }
        /// <summary>
        /// 显示终点编辑器
        /// </summary>
        public static void ForkLineShowEnd(int index)
        {
            MapElement.MapForkLineList[index].EndRect.Fill = CavnvasBase.GetSolid(100, Colors.Pink); //半透明
            MapElement.MapForkLineList[index].EndRect.StrokeThickness = 0.5;
            MapElement.MapForkLineList[index].EndRect.Stroke = Brushes.Gray;
            MapElement.MapForkLineList[index].EndRect.Width = MapElement.GridSize;
            MapElement.MapForkLineList[index].EndRect.Height = MapElement.GridSize;
            MapElement.CvForkLine.Children.Add(MapElement.MapForkLineList[index].EndRect);
        }
        /// <summary>
        /// 绘制列表
        /// </summary>
        public static void DrawForkLineList()
        {
            //if (MapRFIDList.Count == 0)
            //    return;
            //for (int i = 0; i < MapRFIDList.Count; i++)
            //{
            //    DrawRFID(i);
            //}
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
            line.Num = MapElement.MapForkLineList.Count + 1;
            MapElement.MapForkLineList.Add(line);
            //返回索引
            return MapElement.MapForkLineList.Count - 1;
        }
    }
}
