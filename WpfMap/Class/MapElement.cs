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
            /// 起点标签
            /// </summary>
            public int StartRFID = 0;
            /// <summary>
            /// 终点标签
            /// </summary>
            public int EndRFID = 0;
            /// <summary>
            /// 起点坐标
            /// </summary>
            public Point StartPoint = new Point();
            /// <summary>
            /// 终点坐标
            /// </summary>
            public Point StopPoint = new Point();
            /// <summary>
            /// 四个方向 0，1，2，3
            /// </summary>
            public int Dir = 0;
            /// <summary>
            /// 选中框
            /// </summary>
            public Rectangle selectRectangle = new Rectangle();
        }
        /// <summary>
        /// 直线集合
        /// </summary>
        public static List<MapElement.RouteLine> MapLineList = new List<MapElement.RouteLine>();
        //分叉线
        public class RouteForkLine
        {
            /// <summary>
            /// 起点标签
            /// </summary>
            public int StartRFID = 0;
            /// <summary>
            /// 终点标签
            /// </summary>
            public int EndRFID = 0;
            /// <summary>
            /// 起点坐标
            /// </summary>
            public Point StartPoint = new Point();
            /// <summary>
            /// 终点坐标
            /// </summary>
            public Point StopPoint = new Point();
            /// <summary>
            /// 四个方向 0，1，2，3
            /// </summary>
            public int Dir = 0;
            /// <summary>
            /// 选中框
            /// </summary>
            public Rectangle selectRectangle = new Rectangle();
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
            float Radius = 20;
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
            return  MapElement.MapRFIDList.Count - 1;
        }
     
        /*-------路径直线---------------*/
        /// <summary>
        /// 绘制单个RFID
        /// </summary>
        public static void DrawRouteLine(int index)
        {
            //站点
            float Radius = 20;
            //绘制
            MapRFIDList[index].ellipse.Height = Radius * 2;
            MapRFIDList[index].ellipse.Width = Radius * 2;
            //item.ellipse.Margin = new Thickness(item.point.X - Radius, item.point.Y - Radius, 0, 0);
            MapRFIDList[index].ellipse.StrokeThickness = 1;
            MapRFIDList[index].ellipse.Fill = System.Windows.Media.Brushes.Yellow;
            MapRFIDList[index].ellipse.Stroke = System.Windows.Media.Brushes.Gray;
            MapElement.CvRouteLine.Children.Add(MapRFIDList[index].ellipse);
            //显示编号
            CavnvasBase.DrawText(
                MapRFIDList[index].ellipse.Margin.Left + 15, 
                MapRFIDList[index].ellipse.Margin.Top + 10, 
                MapRFIDList[index].Num.ToString(), 
                Colors.Black,
                MapElement.CvRouteLine, 
                MapRFIDList[index].textBlock
                );
        }

        /// <summary>
        /// 绘制RFID列表
        /// </summary>
        public static void DrawRouteLineList()
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
        public static int AddRouteLineAndShow()
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

        /*-------路径分叉线---------------*/
        /// <summary>
        /// 绘制单个RFID
        /// </summary>
        public static void DrawForkLine(int index)
        {
            //站点
            float Radius = 20;
            //绘制
            MapRFIDList[index].ellipse.Height = Radius * 2;
            MapRFIDList[index].ellipse.Width = Radius * 2;
            //item.ellipse.Margin = new Thickness(item.point.X - Radius, item.point.Y - Radius, 0, 0);
            MapRFIDList[index].ellipse.StrokeThickness = 1;
            MapRFIDList[index].ellipse.Fill = System.Windows.Media.Brushes.Yellow;
            MapRFIDList[index].ellipse.Stroke = System.Windows.Media.Brushes.Gray;
            MapElement.CvForkLine.Children.Add(MapRFIDList[index].ellipse);
            //显示编号
            CavnvasBase.DrawText(
                MapRFIDList[index].ellipse.Margin.Left + 15, 
                MapRFIDList[index].ellipse.Margin.Top + 10, 
                MapRFIDList[index].Num.ToString(), 
                Colors.Black, 
                MapElement.CvForkLine, 
                MapRFIDList[index].textBlock
                );
        }

        /// <summary>
        /// 绘制RFID列表
        /// </summary>
        public static void DrawForkLineList()
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
        public static int AddForkLineAndShow()
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
    }
}
