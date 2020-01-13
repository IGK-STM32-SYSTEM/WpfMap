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
            int StartRFID = 0;
            /// <summary>
            /// 终点标签
            /// </summary>
            int EndRFID = 0;
            /// <summary>
            /// 起点坐标
            /// </summary>
            Point StartPoint = new Point();
            /// <summary>
            /// 终点坐标
            /// </summary>
            Point StopPoint = new Point();
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
            int StartRFID = 0;
            /// <summary>
            /// 终点标签
            /// </summary>
            int EndRFID = 0;
            /// <summary>
            /// 起点坐标
            /// </summary>
            Point StartPoint = new Point();
            /// <summary>
            /// 终点坐标
            /// </summary>
            Point StopPoint = new Point();
            /// <summary>
            /// 四个方向 0，1，2，3
            /// </summary>
            int Dir = 0;
        }
        /// <summary>
        /// 分叉线集合
        /// </summary>
        public static List<MapElement.RouteForkLine> MapForkLineList = new List<MapElement.RouteForkLine>();
        /// <summary>
        /// 绘制RFID列表
        /// </summary>
        public static void DrawRFIDList(Canvas canvas)
        {
            foreach (var item in MapRFIDList)
            {
                //站点
                float Radius = 20;
                //绘制
                item.ellipse.Height = Radius * 2;
                item.ellipse.Width = Radius * 2;
                //item.ellipse.Margin = new Thickness(item.point.X - Radius, item.point.Y - Radius, 0, 0);
                item.ellipse.StrokeThickness = 1;
                item.ellipse.Fill = System.Windows.Media.Brushes.Yellow;
                item.ellipse.Stroke = System.Windows.Media.Brushes.Gray;
                canvas.Children.Add(item.ellipse);
                //显示编号
                CavnvasBase.DrawText(item.ellipse.Margin.Left + 15, item.ellipse.Margin.Top + 10, item.Num.ToString(), Colors.Black, canvas, item.textBlock);
            }
        }

        /// <summary>
        /// 绘制单个RFID
        /// </summary>
        public static void DrawRFID(int index, Canvas canvas)
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
            canvas.Children.Add(MapRFIDList[index].ellipse);
            //显示编号
            CavnvasBase.DrawText(MapRFIDList[index].ellipse.Margin.Left + 15, MapRFIDList[index].ellipse.Margin.Top + 10, MapRFIDList[index].Num.ToString(), Colors.Black, canvas, MapRFIDList[index].textBlock);
        }

        /// <summary>
        /// 绘制画布栅格
        /// </summary>
        /// <param name="width">画布宽度</param>
        /// <param name="height">画布高度</param>
        /// <param name="canvas">画布对象</param>
        public static void DrawGrid(int width, int height, Canvas canvas)
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
                canvas.Children.Add(line);
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
                canvas.Children.Add(line);
            }

        }
    }
}
