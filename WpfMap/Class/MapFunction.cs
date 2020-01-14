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
    public class MapFunction
    {
        public static SolidColorBrush GetSolid(byte alpha, Color color)
        {
            return new SolidColorBrush(Color.FromArgb(100, color.R, color.G, color.B));
        }
        /*-----------------RFID-------------------------------*/
        /// <summary>
        /// 判断坐标是否在某个标签上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static int IsOnRFID(Point point)
        {
            if (MapElement.MapRFIDList.Count == 0)
                return -1;
            //遍历所有标签
            for (int i = 0; i < MapElement.MapRFIDList.Count; i++)
            {
                Point pt = new Point(MapElement.MapRFIDList[i].ellipse.Margin.Left + MapElement.RFID_Radius, MapElement.MapRFIDList[i].ellipse.Margin.Top + MapElement.RFID_Radius);
                var distance = MathHelper.Distance(pt, point);
                //距离小于20,在当前站点上
                if (distance <= 20)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 判断坐标是否在指定标签上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <param name="index">RFID索引</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static bool IsOnOneRFID(Point point, int index)
        {
            if (MapElement.MapRFIDList.Count == 0)
                return false;
            Point pt = new Point(MapElement.MapRFIDList[index].ellipse.Margin.Left + MapElement.RFID_Radius, MapElement.MapRFIDList[index].ellipse.Margin.Top + MapElement.RFID_Radius);
            var distance = MathHelper.Distance(pt, point);
            //距离小于20,在当前站点上
            if (distance <= 20)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 移动RFID到指定位置
        /// </summary>
        /// <param name="index">RFID索引</param>
        /// <param name="point">目标位置</param>
        /// <param name="canvas">画布</param>
        public static void MoveRFIDTo(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //计算xy方向偏差
            Thickness thickness = MapElement.MapRFIDList[index].ellipse.Margin;
            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            double diff_x = thickness.Left - (point.X - point.X % MapElement.GridSize);
            double diff_y = thickness.Top - (point.Y - point.Y % MapElement.GridSize);
            //移动圆
            thickness.Left -= diff_x;
            thickness.Top -= diff_y;
            MapElement.MapRFIDList[index].ellipse.Margin = thickness;

            //选择框跟随
            MapElement.MapRFIDList[index].selectRectangle.Margin = thickness;

            //文字跟随即可
            thickness.Left -= diff_x - 15;
            thickness.Top -= diff_y - 10;
            MapElement.MapRFIDList[index].textBlock.Margin = thickness;

        }
        /// <summary>
        /// 设置标到选中状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRFIDIsSelected(int index)
        {
            if (index == -1)
                return;
            if (MapElement.CvRFID.Children.Contains(MapElement.MapRFIDList[index].selectRectangle))
                return;
            MapElement.MapRFIDList[index].selectRectangle.Fill = null;
            MapElement.MapRFIDList[index].selectRectangle.StrokeThickness = 0.8;
            MapElement.MapRFIDList[index].selectRectangle.Stroke = Brushes.Black;
            MapElement.MapRFIDList[index].selectRectangle.Width = MapElement.RFID_Radius * 2;
            MapElement.MapRFIDList[index].selectRectangle.Height = MapElement.RFID_Radius * 2;
            MapElement.MapRFIDList[index].selectRectangle.Margin = MapElement.MapRFIDList[index].ellipse.Margin;
            //显示虚线
            MapElement.MapRFIDList[index].selectRectangle.StrokeDashArray = new DoubleCollection() { 3, 5 };
            MapElement.MapRFIDList[index].selectRectangle.StrokeDashCap = PenLineCap.Triangle;
            MapElement.CvRFID.Children.Add(MapElement.MapRFIDList[index].selectRectangle);
        }
        /// <summary>
        /// 设置标到正常状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRFIDIsNormal(int index)
        {
            if (index == -1)
                return;
            MapElement.CvRFID.Children.Remove(MapElement.MapRFIDList[index].selectRectangle);
        }
        /*-----------------直线-------------------------------*/
        /// <summary>
        /// 判断坐标是否在某条直线上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static int IsOnRouteLine(Point point)
        {
            if (MapElement.MapLineList.Count == 0)
                return -1;
            //遍历
            for (int i = 0; i < MapElement.MapLineList.Count; i++)
            {
                //拿到直线
                Line line = MapElement.MapLineList[i].line;
                double dis = MathHelper.DistancePointToLine(point, line);
                if (dis < 5)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 更新直线终点位置
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        /// <param name="canvas">画布</param>
        public static void UpdateRouteLineEndPoint(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //计算xy方向偏差
            Thickness thickness = MapElement.MapLineList[index].line.Margin;
            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            double diff_x = thickness.Left - (point.X - point.X % MapElement.GridSize);
            double diff_y = thickness.Top - (point.Y - point.Y % MapElement.GridSize);

            //移动线
            thickness.Left -= diff_x;
            thickness.Top -= diff_y;


            //更新线终点坐标
            MapElement.MapLineList[index].line.X2 = point.X - point.X % MapElement.GridSize + MapElement.GridSize;
            MapElement.MapLineList[index].line.Y2 = point.Y - point.Y % MapElement.GridSize + MapElement.GridSize;
            //MapElement.MapLineList[index].line.Margin = thickness;

            //选择线跟随
            //MapElement.MapLineList[index].SelectLine.X2 = px;
            //MapElement.MapLineList[index].SelectLine.Y2 = py;

            //终点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapLineList[index].EndRect.Margin = thickness;

            //文字跟随即可
            //MapElement.MapLineList[index].textBlock.Margin = thickness;

        }
        /// <summary>
        /// 更新直线到指定位置【添加状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        /// <param name="canvas">画布</param>
        public static void MoveRouteLineForAdd(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //计算xy方向偏差
            Thickness thickness = MapElement.MapLineList[index].line.Margin;
            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            double diff_x = thickness.Left - (point.X - point.X % MapElement.GridSize);
            double diff_y = thickness.Top - (point.Y - point.Y % MapElement.GridSize);

            thickness.Left -= diff_x;
            thickness.Top -= diff_y;

            //移动线
            //MapElement.MapLineList[index].line.Margin = thickness;

            //选择线跟随
            //  MapElement.MapLineList[index].SelectLine.Margin = thickness;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapLineList[index].StartRect.Margin = thickness;

            ////终点编辑器跟随
            //thickness.Left += MapElement.GridSize / 2;
            //thickness.Top += MapElement.GridSize / 2;
            //MapElement.MapLineList[index].EndRect.Margin = thickness;


            ////文字跟随即可
            //thickness.Left -= diff_x - 15;
            //thickness.Top -= diff_y - 10;
            //MapElement.MapLineList[index].textBlock.Margin = thickness;

        }
        /// <summary>
        /// 更新直线到指定位置【编辑状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        /// <param name="canvas">画布</param>
        public static void MoveRouteLineForEdit(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //计算xy方向偏差
            Thickness thickness = MapElement.MapLineList[index].line.Margin;
            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            double diff_x = thickness.Left - (point.X - point.X % MapElement.GridSize);
            double diff_y = thickness.Top - (point.Y - point.Y % MapElement.GridSize);

            thickness.Left -= diff_x;
            thickness.Top -= diff_y;

            //移动线
            MapElement.MapLineList[index].line.Margin = thickness;

            //选择线跟随
            MapElement.MapLineList[index].SelectLine.Margin = thickness;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapLineList[index].StartRect.Margin = thickness;

            //终点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapLineList[index].EndRect.Margin = thickness;


            //文字跟随即可
            thickness.Left -= diff_x - 15;
            thickness.Top -= diff_y - 10;
            MapElement.MapLineList[index].textBlock.Margin = thickness;

        }

        /// <summary>
        /// 设置标到选中状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRouteLineIsSelected(int index)
        {
            if (index == -1)
                return;
            //显示选择虚线
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapLineList[index].SelectLine) == false)
            {
                MapElement.RouteLineShowSelectLine(index);
            }
            //显示起点编辑器
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapLineList[index].StartRect) == false)
            {
                MapElement.RouteLineShowStart(index);
            }
            //显示终点编辑器
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapLineList[index].EndRect) == false)
            {
                MapElement.RouteLineShowEnd(index);
            }
        }
        /// <summary>
        /// 设置标到正常状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRouteLineIsNormal(int index)
        {
            if (index == -1)
                return;
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].SelectLine);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].StartRect);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].EndRect);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].textBlock);
        }

        /*-----------------分叉线-----------------------------*/

        /// <summary>
        /// 设置标到正常状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRouteForkLineIsNormal(int index)
        {
            if (index == -1)
                return;
            MapElement.CvForkLine.Children.Remove(MapElement.MapForkLineList[index].selectRectangle);
        }

        /*-----------------综合-----------------------------*/
        public static void ClearAllSelect()
        {
            switch (GlobalVar.NowType)
            {
                case GlobalVar.EnumElementType.RFID:
                    if (GlobalVar.NowSelectIndex != -1)
                    {
                        MapFunction.SetRFIDIsNormal(GlobalVar.NowSelectIndex);
                    }
                    break;
                case GlobalVar.EnumElementType.RouteLine:
                    if (GlobalVar.NowSelectIndex != -1)
                    {
                        MapFunction.SetRouteLineIsNormal(GlobalVar.NowSelectIndex);
                    }
                    break;
                case GlobalVar.EnumElementType.RouteForkLine:
                    if (GlobalVar.NowSelectIndex != -1)
                    {
                        MapFunction.SetRouteForkLineIsNormal(GlobalVar.NowSelectIndex);
                    }
                    break;
                default: break;
            }
        }


    }
}
